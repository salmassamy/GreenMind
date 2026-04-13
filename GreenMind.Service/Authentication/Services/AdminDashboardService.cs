using GreenMind.Domain.Entities;
using GreenMind.Presistance.Data.DbContexts;
using GreenMind.ServiceAbstraction.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace GreenMind.Service.Services
{
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminDashboardService(ApplicationDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }
        private async Task<AdminProductDto> MapToDto(Guid productId)
        {
            var entity = await _context.Products
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == productId);

            if (entity == null)
                throw new Exception("Product not found");

            return new AdminProductDto
            {
                Id = entity.Id.ToString(),
                Name = entity.Name,
                Description = entity.Description,
                Image = entity.ImageURL,
                Category = entity.Category?.Name ?? "",
                Price = $"{entity.Price}$"
            };
        }
        // ================= CREATE =================
        public async Task<AdminProductDto> CreateProductAsync(CreateUpdateProductDto dto)
        {
            ValidateProduct(dto);

            var category = await GetOrCreateCategoryAsync(dto.CategoryName);

            var imageUrl = await SaveImageAsync(dto.Image!);

            var entity = new Product
            {
                Name = dto.Name.Trim(),
                Description = dto.Description.Trim(),
                CategoryId = category.Id, // int ✔
                Price = dto.Price,
                ImageURL = imageUrl
            };

            _context.Products.Add(entity);
            await _context.SaveChangesAsync();

            return await MapToDto(entity.Id); // Guid ✔
        }

        // ================= UPDATE =================
        public async Task<AdminProductDto> UpdateProductAsync(Guid id, CreateUpdateProductDto dto)
        {
            ValidateProduct(dto, true);

            var entity = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id); // Guid ✔

            if (entity == null)
                throw new Exception("Product not found");

            var category = await GetOrCreateCategoryAsync(dto.CategoryName);

            entity.Name = dto.Name.Trim();
            entity.Description = dto.Description.Trim();
            entity.CategoryId = category.Id; // int ✔
            entity.Price = dto.Price;

            if (dto.Image != null && dto.Image.Length > 0)
            {
                entity.ImageURL = await SaveImageAsync(dto.Image);
            }

            await _context.SaveChangesAsync();

            return await MapToDto(entity.Id);
        }

        // ================= DELETE =================
        public async Task DeleteProductAsync(Guid id)
        {
            var entity = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id); // Guid ✔

            if (entity == null)
                throw new Exception("Product not found");

            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
        }

        // ================= GET ORDERS =================
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

        // ================= USER ACTIVITIES =================
        public async Task<UserActivitiesResponseDto> GetUserActivitiesAsync(string? search)
        {
            var query = _context.UserActivityLogs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>
                    x.UserName.Contains(search) ||
                    x.ActionType.Contains(search));
            }

            var rows = await query.ToListAsync();

            var activities = rows
                .GroupBy(x => x.StartedAt.Date)
                .Select(g => new ActivityDayDto
                {
                    Date = g.Key.ToString("yyyy/M/d"),
                    Logs = g.Select(x => new ActivityLogDto
                    {
                        Time = x.StartedAt.ToString("hh:mm tt"),
                        Action = $"User: {x.UserName} - {x.ActionType}"
                    }).ToList()
                })
                .ToList();

            return new UserActivitiesResponseDto
            {
                Activities = activities
            };
        }
        public async Task<List<AdminProductDto>> GetProductsAsync()
        {
            var products = await _context.Products
                .Include(x => x.Category)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return products.Select(x => new AdminProductDto
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description,
                Image = x.ImageURL,
                Category = x.Category != null ? x.Category.Name : "",
                Price = $"{x.Price}$"
            }).ToList();
        }
        // ================= HOME =================
        public async Task<AdminHomeSummaryDto> GetHomeSummaryAsync()
        {
            var totalUsers = await _context.Users.CountAsync();
            var totalProducts = await _context.Products.CountAsync();
            var totalOrders = await _context.Orders.CountAsync();

            return new AdminHomeSummaryDto
            {
                Stats = new List<AdminStatDto>
                {
                    new AdminStatDto { Title = "Orders", Value = totalOrders.ToString() },
                    new AdminStatDto { Title = "Products", Value = totalProducts.ToString() },
                    new AdminStatDto { Title = "Users", Value = totalUsers.ToString() }
                }
            };
        }

        // ================= CATEGORY =================
        private async Task<Category> GetOrCreateCategoryAsync(string name)
        {
            var normalized = name.Trim().ToLower();

            var category = await _context.Categories
                .FirstOrDefaultAsync(x => x.Name.ToLower() == normalized); // string ✔

            if (category == null)
            {
                category = new Category
                {
                    Name = name.Trim()
                };

                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
            }

            return category;
        }

        // ================= IMAGE =================
        private async Task<string> SaveImageAsync(IFormFile image)
        {
            var extension = Path.GetExtension(image.FileName).ToLower();

            var allowed = new[] { ".jpg", ".jpeg", ".png" };

            if (!allowed.Contains(extension))
                throw new Exception("Invalid image");

            var fileName = Guid.NewGuid() + extension;

            var path = Path.Combine("wwwroot", "images");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var fullPath = Path.Combine(path, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await image.CopyToAsync(stream);

            // 🔥 أهم تعديل هنا
            var request = _httpContextAccessor.HttpContext!.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";

            return $"{baseUrl}/images/{fileName}";
        }

        // ================= VALIDATION =================
        private static void ValidateProduct(CreateUpdateProductDto dto, bool isUpdate = false)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new Exception("Name required");

            if (string.IsNullOrWhiteSpace(dto.CategoryName))
                throw new Exception("Category required");

            if (dto.Price <= 0)
                throw new Exception("Price invalid");

            if (!isUpdate && dto.Image == null)
                throw new Exception("Image required");
        }
    }
}