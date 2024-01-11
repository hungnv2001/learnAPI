using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopSolution.ViewModel.Catalog.Product;
using eShopSolution.ViewModel.Catalog.Products.DTO;
using eShopSolution.ViewModel.Catalog.Products.DTO.Public;
using eShopSolution.ViewModel.DTO;

namespace eShopSolution.Aplication.Catalog.Products
{
    public interface IManagerProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(int id, ProductUpdateRequest request);
        Task<bool> UpdatePrice(int id, decimal newPrice);
        Task<bool> UpdateStock(int id, int newStock);
        Task AddViewCount(int id);
        Task<int> Delete(int productID);


        Task<PageResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);
        Task<ProductViewModel> GetByID(int productID, string languageID);
    }
}
