using Infra.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ProductAlertTests
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var dateTime = DateTime.Now.AddDays(-180);
            var readFile = File.ReadAllText("C:\\Users\\louag\\source\\repos\\WarehouseStockAlertSystemApi\\WarehouseStockAlertSystemApi\\products.json");
            var data = JsonConvert.DeserializeObject<List<ProductEntity>>(readFile);
            // Assert
            var firstProduct = data.FirstOrDefault();
            Assert.Less(firstProduct.LastRestocked, dateTime, "Product should be overdue for restocking");
        }
    }
}