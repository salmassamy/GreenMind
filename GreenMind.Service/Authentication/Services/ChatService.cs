using GreenMind.Domain.Entities;
using GreenMind.Presistance.Data.DbContexts;
using GreenMind.ServiceAbstraction.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
        }

        // 1. إنشاء جلسة جديدة
        public CreateNewChatResponse CreateNewChat(string? userId)
        {
            return new CreateNewChatResponse
            {
                SessionId = Guid.NewGuid().ToString(),
                Message = "New session started"
            };
        }

        // 2. توليد عنوان ذكي للمحادثة
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

            if (!messages.Any()) return new GenerateTitleResponse { Title = "محادثة جديدة" };

            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{aiBaseUrl.TrimEnd('/')}/generate-title", new { messages });
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<GenerateTitleResponse>();
                    if (result != null && !string.IsNullOrWhiteSpace(result.Title)) return result;
                }
            }
            catch { /* Fallback Logic */ }

            var firstMsg = messages.FirstOrDefault()?.Content ?? "";
            return new GenerateTitleResponse { Title = firstMsg.Length > 25 ? firstMsg.Substring(0, 25) + "..." : firstMsg };
        }

        // 3. جلب تاريخ المحادثات (Persistence Logic)
        public ChatHistoryResponse GetUserHistory(string userId)
        {
            if (!int.TryParse(userId, out int userIdInt)) return new ChatHistoryResponse();

            // سحب البيانات وترتيبها لضمان ظهورها بشكل صحيح في الموبايل
            var chatLogs = _context.ChatLogs
                .Where(log => log.UserId == userIdInt)
                .OrderBy(log => log.Timestamp)
                .ToList();

            var chatGroups = chatLogs.GroupBy(log => log.SessionId);
            var response = new ChatHistoryResponse();

            foreach (var group in chatGroups)
            {
                var firstMsg = group.FirstOrDefault();
                response.Chats.Add(new ChatSessionDto
                {
                    SessionId = group.Key,
                    // العنوان هو أول رسالة كتبها اليوزر في السيشن دي
                    Title = firstMsg?.MessageText.Length > 30 ? firstMsg.MessageText.Substring(0, 30) + "..." : firstMsg?.MessageText ?? "محادثة",
                    Messages = group.Select(m => new ChatMessageDto
                    {
                        Sender = m.IsFromUser ? "user" : "bot",
                        Text = m.MessageText,
                        Timestamp = m.Timestamp.ToString("yyyy-MM-dd HH:mm:ss")
                    }).ToList()
                });
            }
            return response;
        }

        // 4. معالجة الرسالة والرد على الأسئلة المقترحة
        public async Task<SendMessageResponse> ProcessMessageAsync(SendMessageRequest request, string aiBaseUrl)
        {
            var sessionId = (string.IsNullOrWhiteSpace(request.SessionId) || request.SessionId == "string")
                            ? Guid.NewGuid().ToString()
                            : request.SessionId;

            if (!int.TryParse(request.UserId, out int userIdInt) || userIdInt <= 0) userIdInt = 6;

            // أ. حفظ رسالة اليوزر الحالية (سواء كانت سؤال عادي أو ضغطة على سؤال مقترح)
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

            // ب. سحب الهيستوري شامل "ردود الـ AI السابقة" عشان يفهم السؤال المقترح
            var historyFromDb = await _context.ChatLogs
                .Where(log => log.SessionId == sessionId && log.Id != userMessageLog.Id)
                .OrderByDescending(log => log.Timestamp)
                .Take(6) // قللنا العدد لـ 6 لزيادة سرعة استجابة Hugging Face
                .OrderBy(log => log.Timestamp)
                .Select(log => new AiHistoryMessage
                {
                    Role = log.IsFromUser ? "user" : "assistant",
                    Content = log.MessageText
                }).ToListAsync();

            var aiRequest = new AiChatRequest
            {
                SessionId = sessionId,
                UserId = userIdInt.ToString(),
                Message = request.Message,
                History = historyFromDb
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{aiBaseUrl.TrimEnd('/')}/chat/send", aiRequest);

                if (!response.IsSuccessStatusCode)
                    return new SendMessageResponse { SessionId = sessionId, Status = "error", Reply = "عذراً، سيرفر الذكاء الاصطناعي لا يستجيب." };

                var result = await response.Content.ReadFromJsonAsync<SendMessageResponse>();
                if (result != null)
                {
                    // ج. مزامنة الـ SessionId في حال تغير من طرف الـ AI
                    if (!string.IsNullOrEmpty(result.SessionId) && result.SessionId != sessionId)
                    {
                        sessionId = result.SessionId;
                        userMessageLog.SessionId = sessionId;
                    }

                    // د. حفظ رد الـ AI الأساسي
                    _context.ChatLogs.Add(new ChatLog
                    {
                        SessionId = sessionId,
                        MessageText = result.Reply,
                        IsFromUser = false,
                        Timestamp = DateTime.UtcNow,
                        UserId = userIdInt
                    });

                    // هـ. حفظ سؤال المتابعة (عشان يظهر في الهيستوري لما نرجع له)
                    if (!string.IsNullOrEmpty(result.FollowUpQuestion))
                    {
                        _context.ChatLogs.Add(new ChatLog
                        {
                            SessionId = sessionId,
                            MessageText = result.FollowUpQuestion,
                            IsFromUser = false,
                            Timestamp = DateTime.UtcNow.AddMilliseconds(50),
                            UserId = userIdInt
                        });
                    }

                    await _context.SaveChangesAsync();
                    result.SessionId = sessionId;
                }
                return result;
            }
            catch
            {
                return new SendMessageResponse { SessionId = sessionId, Status = "error", Reply = "حدث خطأ أثناء الاتصال بالخادم." };
            }
        }
    }
}