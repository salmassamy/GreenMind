using System;
using System.ComponentModel.DataAnnotations;

namespace GreenMind.Domain.Entities
{
    public class UserActivityHistory
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; } // الربط مع المستخدم
        public User? User { get; set; }

        [Required]
        public string? Type { get; set; } // "disease", "crop", "fertilizer", "orders"

        [Required]
        public string? Text { get; set; } // "You uploaded a wheat leaf. Diseased."

        [Required]
        public string? Date { get; set; } // محمد طلب التاريخ string "2024-05-10"

        public string? Image { get; set; } // رابط الصورة URL
    }
}