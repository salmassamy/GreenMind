using GreenMind.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PasswordResetToken : BaseEntity
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string Role { get; set; } = null!; // "User" or "Admin"
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; }
}
