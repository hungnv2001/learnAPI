using eShopSolution.ViewModel.Catalog.Product;
using eShopSolution.ViewModel.Catalog.Products.DTO;
using eShopSolution.ViewModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModel.Catalog.Products
{
    public interface IPublicProduct
    {
       Task< PageResult<ProductViewModel> >GetAllByCategoryID(string languageID, GetPublicProductPagingRequest request);
        Task<List<ProductViewModel>> GetAll(string languageID);
    }
}
