using Infra.DTOS;

namespace Infra.Services.Interfaces
{
    public interface IProductAlertService
    {
        Task<List<StockAlertDto>> GetStockAlertsAsync(int quantity = 5, int days = -180);
    }
}