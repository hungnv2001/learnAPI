using eShopSolution.Aplication.Catalog.Products;
using eShopSolution.ViewModel.Catalog.Product;
using eShopSolution.ViewModel.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public IPublicProduct _publicProduct { get; }
        public IManagerProductService _managerProduct { get; }

        public ProductsController(IPublicProduct publicProduct, IManagerProductService managerProduct)
        {
            _publicProduct = publicProduct;
            _managerProduct = managerProduct;
        }

      
        [HttpGet("{languageID}")]
        public async Task<ActionResult> Get([FromQuery]string languageID, GetPublicProductPagingRequest request)
        {
            var products = await _publicProduct.GetAllByCategoryID(languageID,request);
            return Ok(products);
        }
        [HttpGet("find/{productId}/{languageID}")]
        public async Task<ActionResult> GetByID([FromQuery]int productId, string languageID)
        {
            var product = await _managerProduct.GetByID(productId, languageID);
            if (product == null) return BadRequest("Sản phẩm không tồn ayij" +
                "");
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _managerProduct.Create(request);
            if (result == 0) return BadRequest();
            var product = await GetByID(result,request.LanguageId);
            return Created(nameof(GetByID), product); ;
        }
        [HttpPut]
        public async Task<ActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _managerProduct.Update(request.Id, request);
            if (result == 0) return BadRequest();
            return Ok(); ;
        }
        [HttpDelete("{productID}")]
        public async Task<ActionResult> Delete(int productID)
        {
            var result = await _managerProduct.Delete(productID);
            if (result == 0) return BadRequest();
            return Ok(); 
        }
        [HttpPatch("{id}/{newPrice}")]
        public async Task<ActionResult> UpdatePrice( int id, decimal newPrice)
        {
            var result = await _managerProduct.UpdatePrice(id, newPrice);
            if (!result) return BadRequest();
            return Ok(); ;
        }
    }
}
