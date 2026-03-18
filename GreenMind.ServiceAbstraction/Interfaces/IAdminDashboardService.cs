using GreenMind.ServiceAbstraction.DTOs;

public interface IAdminDashboardService
{
    Task<AdminProductDto> CreateProductAsync(CreateUpdateProductDto dto);
    Task<AdminProductDto> UpdateProductAsync(int id, CreateUpdateProductDto dto);
    Task DeleteProductAsync(int id);
    Task<OrdersResponseDto> GetOrdersAsync();
    Task<UserActivitiesResponseDto> GetUserActivitiesAsync(string? search);
}
