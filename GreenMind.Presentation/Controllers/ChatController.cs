using GreenMind.ServiceAbstraction.DTOs;
using GreenMind.ServiceAbstraction.Interfaces;
using GreenMind.Service;
using Microsoft.AspNetCore.Mvc;
using GreenMind.Services;

namespace GreenMind.Controllers
{
    [ApiController]
    [Route("chat")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        private readonly string _aiBaseUrl = "https://overnight-substance-tiptop.ngrok-free.dev";

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
        {
            var response = await _chatService.ProcessMessageAsync(request, _aiBaseUrl);
            return Ok(response);
        }

        [HttpGet("history/{userId}")]
        public IActionResult GetHistory(string userId)
        {
            var response = _chatService.GetUserHistory(userId);
            return Ok(response);
        }

        [HttpPost("new")]
        public IActionResult CreateNewChat([FromBody] CreateNewChatRequest request)
        {
            var response = _chatService.CreateNewChat(request.UserId);
            return Ok(response);
        }
        [HttpGet("generate-title/{sessionId}")]
        public async Task<IActionResult> GenerateTitle(string sessionId)
        {
            var result = await _chatService.GenerateChatTitleAsync(sessionId, _aiBaseUrl);
            return Ok(result);
        }
    }
}