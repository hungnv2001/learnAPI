using eShopSolution.ViewModel.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public IPublicProduct _publicProduct { get; }
        public ProductController(IPublicProduct publicProduct)
        {
            _publicProduct = publicProduct;
        }


        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var products = await _publicProduct.GetAll();
            return Ok(products);
        }
    }
}
