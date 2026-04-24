using GreenMind.Domain.Entities;
using GreenMind.Presistance.Data.DbContexts;
using GreenMind.ServiceAbstraction.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using GreenMind.ServiceAbstraction.Interfaces;

namespace GreenMind.Services
{
    public class ChatService : IChatService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;

        public ChatService(HttpClient httpClient, ApplicationDbContext context)
        {
            _httpClient = httpClient;
            _context = context;

            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("ngrok-skip-browser-warning", "true");
        }

        public async Task<SendMessageResponse> ProcessMessageAsync(SendMessageRequest request, string aiBaseUrl)
        {
            var sessionId = request.SessionId ?? Guid.NewGuid().ToString();

            int userIdInt;
            if (!int.TryParse(request.UserId, out userIdInt) || userIdInt <= 0)
            {
                userIdInt = 6;
            }

            var userExists = await _context.Users.AnyAsync(u => u.Id == userIdInt);
            if (!userExists)
            {
                throw new Exception($"تنبيه: يجب وجود مستخدم بـ Id = {userIdInt} في قاعدة البيانات لدعم المحادثات.");
            }

            var userMessageLog = new ChatLog
            {
                SessionId = sessionId,
                MessageText = request.Message,
                IsFromUser = true,
                Timestamp = DateTime.UtcNow,
                UserId = userIdInt
            };
            _context.ChatLogs.Add(userMessageLog);
            await _context.SaveChangesAsync();

            var historyFromDb = await _context.ChatLogs
                .Where(log => log.SessionId == sessionId && log.Id != userMessageLog.Id)
                .OrderByDescending(log => log.Timestamp)
                .Take(10)
                .OrderBy(log => log.Timestamp)
                .Select(log => new AiHistoryMessage
                {
                    Sender = log.IsFromUser ? "user" : "assistant",
                    Text = log.MessageText
                }).ToListAsync();

            var aiRequest = new AiChatRequest
            {
                SessionId = sessionId,
                UserId = userIdInt.ToString(),
                Message = request.Message,
                History = historyFromDb
            };

            var response = await _httpClient.PostAsJsonAsync($"{aiBaseUrl}/chat/send", aiRequest);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<SendMessageResponse>();

            if (result != null)
            {
                var botReplyLog = new ChatLog
                {
                    SessionId = sessionId,
                    MessageText = result.Reply,
                    IsFromUser = false,
                    Timestamp = DateTime.UtcNow,
                    UserId = userIdInt
                };
                _context.ChatLogs.Add(botReplyLog);
                
                if (!string.IsNullOrEmpty(result.FollowUpQuestion))
                {
                    var followUpLog = new ChatLog
                    {
                        SessionId = sessionId,
                        MessageText = result.FollowUpQuestion,
                        IsFromUser = false,
                        Timestamp = DateTime.UtcNow.AddSeconds(1),
                        UserId = userIdInt
                    };
                    _context.ChatLogs.Add(followUpLog);
                }

                await _context.SaveChangesAsync();
            }

            return result ?? new SendMessageResponse { SessionId = sessionId, Status = "error" };
        }

        public async Task<GenerateTitleResponse> GenerateChatTitleAsync(string sessionId, string aiBaseUrl)
        {
            var messages = await _context.ChatLogs
                .Where(log => log.SessionId == sessionId)
                .OrderBy(log => log.Timestamp)
                .Take(2)
                .Select(log => new AiTitleMessage
                {
                    Role = log.IsFromUser ? "user" : "assistant",
                    Content = log.MessageText
                }).ToListAsync();

            if (!messages.Any())
                return new GenerateTitleResponse { Title = "محادثة جديدة" };

            var titleRequest = new GenerateTitleRequest { Messages = messages };
            var response = await _httpClient.PostAsJsonAsync($"{aiBaseUrl}/generate-title", titleRequest);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GenerateTitleResponse>()
                       ?? new GenerateTitleResponse { Title = "محادثة زراعية" };
            }

            return new GenerateTitleResponse { Title = "محادثة زراعية" };
        }

        public ChatHistoryResponse GetUserHistory(string userId)
        {
            var userIdInt = int.Parse(userId);

            var chatGroups = _context.ChatLogs
                .Where(log => log.UserId == userIdInt)
                .AsEnumerable()
                .GroupBy(log => log.SessionId)
                .ToList();

            var response = new ChatHistoryResponse();

            foreach (var group in chatGroups)
            {
                var firstMsg = group.OrderBy(m => m.Timestamp).FirstOrDefault();
                response.Chats.Add(new ChatSessionDto
                {
                    SessionId = group.Key,
                    Title = firstMsg != null && firstMsg.MessageText.Length > 25
                            ? firstMsg.MessageText.Substring(0, 25) + "..."
                            : firstMsg?.MessageText ?? "محادثة زراعية",
                    Messages = group.OrderBy(m => m.Timestamp).Select(m => new ChatMessageDto
                    {
                        Sender = m.IsFromUser ? "user" : "bot",
                        Text = m.MessageText,
                        Timestamp = m.Timestamp.ToString("O")
                    }).ToList()
                });
            }

            return response;
        }

        public CreateNewChatResponse CreateNewChat(string? userId)
        {
            return new CreateNewChatResponse
            {
                SessionId = Guid.NewGuid().ToString(),
                Message = "New chat created (Guest support active)"
            };
        }
    }
}