using ExcessInventoryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExcessInventoryManagement.Controllers
{
    [Route("api/MarkdownPlan")]
    [ApiController]
    public class MarkdownPlanController : ControllerBase
    {
        [HttpGet]
        [Route("GetMarkdownPlans")]
        public List<MarkdownPlan> GetMarkdownPlans()
        {
            using (ExcessInventoryManagementContext context = new ExcessInventoryManagementContext())
            {
                var markdownList = (from markdown in context.MarkdownPlans
                                   select markdown).ToList();
                return markdownList;
            }
        }

        [HttpPost]
        [Route("CreateMarkdownPlan")]
        public bool CreateMarkdownPlan(MarkdownPlan markdownPlan)
        {
            TimeSpan dateDifference = markdownPlan.EndDate - markdownPlan.StartDate;
            if(dateDifference.Days >= 7)
            {
                using (ExcessInventoryManagementContext context = new ExcessInventoryManagementContext())
                {
                    var existingRecord = (from markdown in context.MarkdownPlans
                                          where markdown.MarkdownPlanId == markdownPlan.MarkdownPlanId
                                          select markdown).FirstOrDefault();
                    if (existingRecord == null)
                    {
                        context.Add(markdownPlan);
                        context.SaveChanges();
                    }
                    else
                    {
                        if(existingRecord.ProductId == markdownPlan.ProductId)
                        {
                            existingRecord.ProductId = markdownPlan.ProductId;
                        }
                        existingRecord.PlanName = markdownPlan.PlanName;
                        existingRecord.StartDate = markdownPlan.StartDate;
                        existingRecord.EndDate = markdownPlan.EndDate;
                        existingRecord.InitialPriceReduction = markdownPlan.InitialPriceReduction;
                        existingRecord.MidwayPriceReduction = markdownPlan.MidwayPriceReduction;
                        existingRecord.FinalPriceReduction = markdownPlan.FinalPriceReduction;
                        context.Update(existingRecord);
                        context.SaveChanges();
                    }
                    return true;
                }
            }
            return false;
        }

        [HttpPost]
        [Route("DeleteMarkdownPlan")]
        public bool DeleteMarkdownPlan(int markdownPlanId)
        {
            using (ExcessInventoryManagementContext context = new ExcessInventoryManagementContext())
            {
                var recordExists = (from markdown in context.MarkdownPlans
                                    where markdown.MarkdownPlanId == markdownPlanId
                                    select markdown).FirstOrDefault();
                if (recordExists != null) 
                { 
                    context.Remove(recordExists);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
