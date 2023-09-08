using ExcessInventoryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExcessInventoryManagement.Controllers
{
    [Route("api/DailyMetrics")]
    [ApiController]
    public class DailyMetricsAPIController : ControllerBase
    {
        [HttpGet]
        [Route("GetDailyMetrics")]
        public List<DailyMetrics> GetDailyMetrics(int markdownPlanId)
        {
            using(ExcessInventoryManagementContext context = new ExcessInventoryManagementContext()) 
            {
                List<DailyMetrics> dailyMetrics = new List<DailyMetrics>();
                // get markdownPlan, product, inventory, and sales data
                var markdownPlan = (from markdown in context.MarkdownPlans
                                    where markdown.MarkdownPlanId == markdownPlanId
                                    select markdown).FirstOrDefault();
                var product = (from myProduct in context.Products
                               where markdownPlan.ProductId == myProduct.ProductId
                               select myProduct).FirstOrDefault();
                var inventory = (from myInventory in context.Inventories
                                 where product.ProductId == myInventory.ProductId
                                 select myInventory).FirstOrDefault();
                var sales = (from mySales in context.Sales
                             where markdownPlan.MarkdownPlanId == mySales.MarkdownPlanId
                             select mySales).FirstOrDefault();

                // setup daily metrics
                if(product != null && inventory != null && sales != null)
                {
                    string[] salesData = sales.SalesData.Split(',');
                    int? currentInventory = inventory.ProductCount;
                    int? remainingInventory;
                    DateTime startDate = markdownPlan.StartDate;
                    DateTime endDate = markdownPlan.EndDate;
                    TimeSpan dateDifference = markdownPlan.EndDate - markdownPlan.StartDate;
                    DateTime midDate = startDate.AddDays(dateDifference.Days / 2);
                    DateTime currentDate = startDate;
                    decimal? margin;
                    decimal? profit;
                    decimal? price;

                    // loop through sales data
                    for (int i = 0; i < salesData.Length; i++)
                    {
                        remainingInventory = currentInventory - Int32.Parse(salesData[i]);
                        if(currentDate >= startDate && currentDate < midDate)
                        {
                            price = product.Price * (1 - (markdownPlan.InitialPriceReduction / 100));
                        }
                        else if(currentDate >= midDate && currentDate < startDate)
                        {
                            price = product.Price * (1 - ((markdownPlan.InitialPriceReduction - markdownPlan.MidwayPriceReduction) / 100));
                        }
                        else
                        {
                            price = product.Price * (1 - ((markdownPlan.InitialPriceReduction - markdownPlan.MidwayPriceReduction - markdownPlan.FinalPriceReduction) / 100));
                        }
                        margin = price * Int32.Parse(salesData[i]) - product.Cost * Int32.Parse(salesData[i]);
                        profit = price * Int32.Parse(salesData[i]);
                        var dailymetric = new DailyMetrics
                        {
                            ProductName = product.ProductName,
                            Description = inventory.Description,
                            ProductCount = currentInventory,
                            UnitsSold = Int32.Parse(salesData[i]),
                            Margin = margin,
                            Profit = profit,
                            RemainingInventory = remainingInventory
                        };
                        dailyMetrics.Add(dailymetric);
                        currentDate.AddDays(1);
                        currentInventory = remainingInventory;
                    }
                }
                return dailyMetrics;
            }
        }
    }
}
