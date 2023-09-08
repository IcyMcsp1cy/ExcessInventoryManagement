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
            
            
            using (ExcessInventoryManagementContext context = new ExcessInventoryManagementContext())
            {
                var getInventory = (from markdown in context.MarkdownPlans
                                    join inventory in context.Inventories on markdown.ProductId equals inventory.ProductId
                                    where markdown.MarkdownPlanId == sales.MarkdownPlanId
                                    select inventory).FirstOrDefault();
                var getMarkdown = (from markdown in context.MarkdownPlans
                                   where markdown.MarkdownPlanId == sales.MarkdownPlanId
                                   select markdown).FirstOrDefault();

                // split sales data on commas
                string[] salesData = sales.SalesData.Split(',');
                int totalInventorySold = 0;
                int dailyInventorySold;
                // loop through sales data to determine inventory sold
                for (int i = 0; i < salesData.Length; i++)
                {
                    dailyInventorySold = Int32.Parse(salesData[i]);
                    totalInventorySold += dailyInventorySold;
                }
                TimeSpan dateDifference = getMarkdown.EndDate - getMarkdown.StartDate;
                
                // check if inventory sold is less than or equal to our inventory
                // check if our days of sales is equal to the number of days between our start and end date
                if (totalInventorySold <= getInventory.ProductCount && salesData.Length == dateDifference.Days)
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
