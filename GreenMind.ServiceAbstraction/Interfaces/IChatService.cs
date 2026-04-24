using GreenMind.ServiceAbstraction.DTOs;
using System.Threading.Tasks;

namespace GreenMind.Services
{
    public interface IChatService
    {
        // معالجة الرسالة: تشمل الحفظ في الداتابيز، استدعاء الـ AI، وحفظ الرد وسؤال المتابعة
        Task<SendMessageResponse> ProcessMessageAsync(SendMessageRequest request, string aiBaseUrl);

        // جلب تاريخ المحادثات الفعلي للمستخدم من جدول ChatLogs
        ChatHistoryResponse GetUserHistory(string userId);

        // إنشاء جلسة محادثة جديدة وتوليد SessionId فريد
        CreateNewChatResponse CreateNewChat(string? userId);
        Task<GenerateTitleResponse> GenerateChatTitleAsync(string sessionId, string aiBaseUrl);
    }
}