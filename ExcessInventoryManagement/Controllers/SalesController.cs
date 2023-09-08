using Microsoft.AspNetCore.Mvc;

namespace ExcessInventoryManagement.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
