using ExcessInventoryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExcessInventoryManagement.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("GetProducts")]
        public List<Product> GetProducts()
        {
            using (ExcessInventoryManagementContext context = new ExcessInventoryManagementContext())
            {
                var productList = (from product in context.Products
                                   select product).ToList();
                return productList;
            }
        }
    }
}

