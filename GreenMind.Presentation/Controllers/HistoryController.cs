using GreenMind.Domain.Entities;
using GreenMind.Presistance.Data.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GreenMind.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. GET /api/history/{type} -> لجلب الهيستوري حسب النوع (disease, crop, etc.)
        [HttpGet("{type}")]
        public async Task<IActionResult> GetHistoryByType(string type)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "1"; // "1" للتجربة بدون توكن
            int userId = int.Parse(userIdStr);

            var history = await _context.UserActivityHistory
                .Where(h => h.UserId == userId && h.Type.ToLower() == type.ToLower())
                .OrderByDescending(h => h.Id)
                .Select(h => new
                {
                    h.Id,
                    h.Text,
                    h.Date,
                    h.Image
                }) // الفورمات اللي محمد طلبها بالظبط
                .ToListAsync();

            return Ok(history);
        }

        // 2. DELETE /api/history/{type}/{id} -> لمسح سجل واحد فقط
        [HttpDelete("{type}/{id}")]
        public async Task<IActionResult> DeleteSingleItem(string type, int id)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "1";
            int userId = int.Parse(userIdStr);

            var item = await _context.UserActivityHistory
                .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId && h.Type.ToLower() == type.ToLower());

            if (item == null) return NotFound(new { message = "Item not found" });

            _context.UserActivityHistory.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted successfully" }); // الرد المطلوب
        }

        // 3. DELETE /api/history/{type} -> لمسح كل السجلات لنوع معين (مثلاً مسح كل الـ Crop history)
        [HttpDelete("{type}")]
        public async Task<IActionResult> DeleteAllByType(string type)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "1";
            int userId = int.Parse(userIdStr);

            var items = await _context.UserActivityHistory
                .Where(h => h.UserId == userId && h.Type.ToLower() == type.ToLower())
                .ToListAsync();

            if (!items.Any()) return Ok(new { message = "History is already empty" });

            _context.UserActivityHistory.RemoveRange(items);
            await _context.SaveChangesAsync();

            return Ok(new { message = "All records deleted" }); // الرد المطلوب
        }
    }
}