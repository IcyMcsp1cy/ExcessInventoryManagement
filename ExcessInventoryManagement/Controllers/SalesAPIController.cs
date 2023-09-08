using ExcessInventoryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExcessInventoryManagement.Controllers
{
    [Route("api/Sales")]
    [ApiController]
    public class SalesAPIController : ControllerBase
    {
        [HttpPost]
        [Route("CreateSalesData")]
        public bool CreateMarkdownPlan(Sales sales)
        {
            
            // first, we split sales data on commas
            // next, we sum up the data to determine if it is less than or equal to inventory
            // finally, 
            using (ExcessInventoryManagementContext context = new ExcessInventoryManagementContext())
            {
                var getInventory = (from markdown in context.MarkdownPlans
                                    join inventory in context.Inventories on markdown.ProductId equals inventory.ProductId
                                    where markdown.MarkdownPlanId == sales.MarkdownPlanId
                                    select inventory).FirstOrDefault();
                string[] salesData = sales.SalesData.Split(',');
                int salesCount = 0;
                int todaysSales;
                for (int i = 0; i < salesData.Length; i++)
                {
                    todaysSales = Int32.Parse(salesData[i]);
                    salesCount += todaysSales;
                }
                if(salesCount <= getInventory.ProductCount)
                {
                    context.Add(sales);
                    context.SaveChanges();
                    return true;
                }
                
            }
            return false;
        }
    }
}
