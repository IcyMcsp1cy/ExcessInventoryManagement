using Microsoft.AspNetCore.Mvc;

namespace ExcessInventoryManagement.Controllers
{
    public class DailyMetricsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
