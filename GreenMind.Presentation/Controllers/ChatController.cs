using GreenMind.Service;
using GreenMind.ServiceAbstraction.DTOs;
using GreenMind.ServiceAbstraction.Interfaces;
using GreenMind.Services;
using Microsoft.AspNetCore.Mvc;

namespace GreenMind.Controllers
{
    [ApiController]
    [Route("chat")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        // الرابط بتاع Hugging Face
        private readonly string _aiBaseUrl = "https://shroukyasser-greenmind-chatbot.hf.space";

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
        {
            // 1. معالجة الـ SessionId: لو "string" أو فاضي خليه null عشان السيرفس تعمل GUID جديد
            if (request.SessionId == "string" || string.IsNullOrWhiteSpace(request.SessionId))
            {
                request.SessionId = null;
            }

            // 2. التحقق من وجود رسالة (Validation بسيط)
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest("الرسالة لا يمكن أن تكون فارغة.");
            }

            var response = await _chatService.ProcessMessageAsync(request, _aiBaseUrl);
            return Ok(response);
        }

        [HttpGet("history/{userId}")]
        public IActionResult GetHistory(string userId)
        {
            // التأكد إن الـ userId مبعوت صح
            if (string.IsNullOrWhiteSpace(userId)) return BadRequest("UserId مطلوب.");

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
            // لو الـ sessionId جاي بكلمة "string" من Swagger مش هنبعته للسيرفس
            if (string.IsNullOrWhiteSpace(sessionId) || sessionId == "string")
                return BadRequest("SessionId غير صحيح.");

            var result = await _chatService.GenerateChatTitleAsync(sessionId, _aiBaseUrl);
            return Ok(result);
        }
    }
}