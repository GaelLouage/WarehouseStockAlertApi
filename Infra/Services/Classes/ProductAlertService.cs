using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infra.DTOS;
using Infra.Enums;
using Infra.Models;
using Infra.Services.Interfaces;
using Newtonsoft.Json;

namespace Infra.Services.Classes
{
    public class ProductAlertService : IProductAlertService
    {
        private readonly string _filePath;
        public ProductAlertService(string filePath)
        {
            _filePath = filePath;
        }
        public async Task<List<StockAlertDto>> GetStockAlertsAsync(int quantity = 5, int days = -180)
        {
            var stockAlerts = new List<StockAlertDto>();
            var products = await ReadFile();

            foreach (var product in products)
            {
                var reasons = new List<string>();

                AddLowStockAlert(quantity, product, reasons);

                AddRestockOverdueMessage(days, product, reasons);

                AddStockAlert(stockAlerts, product, reasons);
            }

            return stockAlerts;
        }

        private static void AddRestockOverdueMessage(int days, ProductEntity product, List<string> reasons)
        {
            if (product.LastRestocked < DateTime.Now.AddDays(days))
            {
                reasons.Add("Restock overdue");
            }
        }

        private static void AddLowStockAlert(int quantity, ProductEntity product, List<string> reasons)
        {
            if (product.Quantity < quantity)
            {
                reasons.Add("Low stock");
            }
        }

        private async Task<List<ProductEntity>> ReadFile()
        {
            var fileReader = await File.ReadAllTextAsync(_filePath);
            var products = JsonConvert.DeserializeObject<List<ProductEntity>>(fileReader) ?? new List<ProductEntity>();
            return products;
        }

        private void AddStockAlert(List<StockAlertDto> stockAlerts, ProductEntity product, List<string> reasons)
        {
            if (reasons.Any())
            {
                stockAlerts.Add(new StockAlertDto
                {
                    ProductName = product.Name,
                    Reason = string.Join(", ", reasons),
                    Severity = GetSeverityType(reasons)
                });
            }
        }

        private SeverityType GetSeverityType(List<string> reasons)
        {
            return reasons.Count switch
            {
                1 => SeverityType.MEDIUM,
                2 => SeverityType.HIGH,
                _ => SeverityType.LOW
            };
        }
    }
}
