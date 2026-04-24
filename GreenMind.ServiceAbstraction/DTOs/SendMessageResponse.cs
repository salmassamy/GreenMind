using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GreenMind.ServiceAbstraction.DTOs
{
    public class SendMessageResponse
    {
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; } = string.Empty;

        [JsonPropertyName("reply")]
        public string Reply { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = "success";

        // حقل التحليل (يظهر فقط لو الـ AI بعته)
        [JsonPropertyName("analysis")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AnalysisResult? Analysis { get; set; }

        // حقل سؤال المتابعة (يظهر فقط لو الـ AI بعته)
        [JsonPropertyName("follow_up_question")]
       // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FollowUpQuestion { get; set; }
    }

    // الكلاس الخاص بتفاصيل التحليل
    public class AnalysisResult
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("result")]
        public string Result { get; set; } = string.Empty;
    }
}