using eShopSolution.ViewModel.Catalog.Products.DTO;
using eShopSolution.ViewModel.DTO;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using EShopSolution.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Net.Mime;
using System.Net.Http.Headers;
using eShopSolution.Aplication.Common;
using eShopSolution.ViewModel.Catalog.Product;
using eShopSolution.ViewModel.Catalog.Products.DTO.Public;

namespace eShopSolution.Aplication.Catalog.Products
{
    public class ManageProductService : IManagerProductService
    {
        private readonly EShopDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public ManageProductService(EShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task AddViewCount(int id)
        {
            var product = await _context.Products.FindAsync(id);
            product.ViewCount++;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>
                {
                    new ProductTranslation()
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoAlias = request.SeoAlias,
                        SeoDescription = request.SeoDescription,
                        SeoTitle = request.SeoTitle,
                        LanguageId = request.LanguageId,
                    }
                }
                
            };
           
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }
  
        public async Task<int> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null) throw new EShopException("Can not find this product: " + id);
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<PageResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        select new { p, pt, pic };
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(p => p.pt.Name.Contains(request.Keyword));
            }
            if (request.CatagoryIDs.Count > 0)
            {
                query = query.Where(p => request.CatagoryIDs.Contains(p.pic.CategoryId));
            }
            // Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    Price = x.p.Price,
                    OriginalPrice = x.p.OriginalPrice,
                    DateCreated = x.p.DateCreated,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,
                }).ToListAsync();
            // select àn projecttion
            var pageResult = new PageResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data,
            };
            return pageResult;
        }


        public async Task<int> Update(int id, ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(id);
            var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.Id && x.LanguageId == request.LanguageId);
            if (product != null) throw new EShopException("Can not find this product: " + id);
            productTranslation.Name = request.Name;
            productTranslation.Description = request.Description;
            productTranslation.SeoDescription = request.SeoDescription;
            productTranslation.SeoTitle = request.SeoTitle;
            productTranslation.SeoAlias = request.SeoAlias;
            productTranslation.Details = request.Details;
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int id, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null) throw new EShopException("Can not find this product: " + id);
            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int id, int newStock)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null) throw new EShopException("Can not find this product: " + id);
            product.Stock = newStock;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
