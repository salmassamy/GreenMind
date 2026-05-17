using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GreenMind.ServiceAbstraction.DTOs
{
    public class ChatHistoryResponse
    {
        [JsonPropertyName("chats")]
        public List<ChatSessionDto> Chats { get; set; } = new();
    }

    public class ChatSessionDto
    {
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("messages")]
        public List<ChatMessageDto> Messages { get; set; } = new();
    }

    public class ChatMessageDto
    {
        [JsonPropertyName("sender")]
        public string Sender { get; set; } = string.Empty;

        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; } = string.Empty;
    }

    // الكلاس ده اللي بيبعت الطلب لـ Hugging Face
    public class AiChatRequest
    {
        [JsonPropertyName("session_id")]
        public string? SessionId { get; set; }

        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("history")]
        public List<AiHistoryMessage> History { get; set; } = new();
    }

    public class AiHistoryMessage
    {
        [JsonPropertyName("role")] // لازم role عشان الـ AI يعرف مين بيتكلم
        public string? Role { get; set; }

        [JsonPropertyName("content")] // لازم content عشان يشوف نص الرسالة
        public string? Content { get; set; }
    }

    public class GenerateTitleRequest
    {
        [JsonPropertyName("messages")]
        public List<AiTitleMessage> Messages { get; set; } = new();
    }

    public class AiTitleMessage
    {
        [JsonPropertyName("role")]
        public string? Role { get; set; }

        [JsonPropertyName("content")]
        public string? Content { get; set; }
    }

    public class GenerateTitleResponse
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
    }
}