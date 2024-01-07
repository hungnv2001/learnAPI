using eShopSolution.ViewModel.DTO;
using eShopSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModel.Catalog.Products.DTO.Public
{
    public class GetManageProductPagingRequest : PagingRequestBase
    {
       public string Keyword {  get; set; }
        public List<int> CatagoryIDs { get; set; }
    }
}
