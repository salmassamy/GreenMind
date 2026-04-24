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

    public class AiChatRequest
    {
        public string? SessionId { get; set; }
        public string? UserId { get; set; }
        public string? Message { get; set; }
        public List<AiHistoryMessage> History { get; set; } = new();
    }

    public class AiHistoryMessage
    {
        public  string? Sender { get; set; } 
        public string? Text { get; set; }
    }

    public class GenerateTitleRequest
    {
        public List<AiTitleMessage> Messages { get; set; } = new();
    }

    public class AiTitleMessage
    {
        public string? Role { get; set; }
        public string? Content { get; set; }
    }

    public class GenerateTitleResponse
    {
        public string Title { get; set; } = string.Empty;
    }
}