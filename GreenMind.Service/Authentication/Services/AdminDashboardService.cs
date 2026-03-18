using GreenMind.Domain.Entities;
using GreenMind.Presistance.Data.DbContexts;
using GreenMind.ServiceAbstraction.DTOs;
using GreenMind.ServiceAbstraction.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GreenMind.Service.Services
{
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly ApplicationDbContext _context;

        public AdminDashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AdminProductDto> CreateProductAsync(CreateUpdateProductDto dto)
        {
            ValidateProduct(dto);

            var entity = new Product
            {
                Name = dto.Name.Trim(),
                Desc = dto.Description.Trim(),
                CategoryId = dto.CategoryId,
                Price = dto.Price,
                Img = dto.Image.Trim()
            };

            _context.Products.Add(entity);
            await _context.SaveChangesAsync();

            return new AdminProductDto
            {
                Id = entity.Id.ToString(),
                Name = entity.Name,
                Description = entity.Desc,
                Image = entity.Img,
                Category = entity.Category != null ? entity.Category.Name : "",
                Price = $"{entity.Price}$"
            };
        }

        public async Task<AdminProductDto> UpdateProductAsync(int id, CreateUpdateProductDto dto)
        {
            ValidateProduct(dto);

            var entity = await _context.Products
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("Product not found");

            entity.Name = dto.Name.Trim();
            entity.Desc = dto.Description.Trim();
            entity.CategoryId = dto.CategoryId;
            entity.Price = dto.Price;
            entity.Img = dto.Image.Trim();

            await _context.SaveChangesAsync();

            return new AdminProductDto
            {
                Id = entity.Id.ToString(),
                Name = entity.Name,
                Description = entity.Desc,
                Image = entity.Img,
                Category = entity.Category != null ? entity.Category.Name : "",
                Price = $"{entity.Price}$"
            };
        }

        public async Task DeleteProductAsync(int id)
        {
            var entity = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("Product not found");

            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<UserActivitiesResponseDto> GetUserActivitiesAsync(string? search)
        {
            var query = _context.UserActivityLogs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(x =>
                    x.UserName.Contains(search) ||
                    x.ActionType.Contains(search));

            var rows = await query
                .OrderByDescending(x => x.StartedAt.Date)
                .ThenByDescending(x => x.StartedAt)
                .ToListAsync();

            var activities = rows
                .GroupBy(x => x.StartedAt.Date)
                .Select(g => new ActivityDayDto
                {
                    Date = g.Key.ToString("yyyy/M/d"),
                    Logs = g.Select(x => new ActivityLogDto
                    {
                        Time = x.EndedAt.HasValue
                            ? $"{x.StartedAt:hh:mm tt} - {x.EndedAt.Value:hh:mm tt}"
                            : x.StartedAt.ToString("hh:mm tt"),
                        Action = $"User: {x.UserName} - {x.ActionType}"
                    }).ToList()
                })
                .ToList();

            return new UserActivitiesResponseDto { Activities = activities };
        }
        public async Task<OrdersResponseDto> GetOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(x => x.User)
                .OrderByDescending(x => x.OrderDate)
                .Select(x => new AdminOrderDto
                {
                    Id = x.Id.ToString(),
                    Customer = x.User.Name,
                    Date = x.OrderDate.ToString("dd/MM/yyyy"),
                    Price = x.TotalAmount.ToString("0.##"),
                    Status = x.Status
                })
                .ToListAsync();

            return new OrdersResponseDto { Orders = orders };
        }

        private static void ValidateProduct(CreateUpdateProductDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new Exception("Name is required");

            if (dto.CategoryId <= 0)
                throw new Exception("Category is required");

            if (dto.Price <= 0)
                throw new Exception("Price is required");

            if (string.IsNullOrWhiteSpace(dto.Image))
                throw new Exception("Image is required");
        }
    }
}