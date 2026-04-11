using GreenMind.ServiceAbstraction.DTOs;

public interface IAdminDashboardService
{
    Task<AdminProductDto> CreateProductAsync(CreateUpdateProductDto dto);

    Task<AdminProductDto> UpdateProductAsync(Guid id, CreateUpdateProductDto dto);

    Task DeleteProductAsync(Guid id);

    Task<OrdersResponseDto> GetOrdersAsync();

    Task<AdminHomeSummaryDto> GetHomeSummaryAsync();

    Task<UserActivitiesResponseDto> GetUserActivitiesAsync(string? search);
}