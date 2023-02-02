using Backend_Final.Contracts.File;
using Backend_Final.Database;
using Backend_Final.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend_Final.Areas.Client.ViewModels.ShopPage;
using static Backend_Final.Areas.Client.ViewModels.ShopPage.ListItemViewModel;

namespace Backend_Final.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "ShopPageProduct")]

    public class ShopPageProduct :ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public ShopPageProduct(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? searchBy = null, string? search = null, int? minPrice = null, int? maxPrice = null,
            [FromQuery] int? categoryId = null, [FromQuery] int? colorId = null, [FromQuery] int? tagId = null)
        {
            var productsQuery = _dataContext.Products.AsQueryable();

            if (searchBy == "Name")
            {
                productsQuery = productsQuery.Where(p => p.Name!.StartsWith(search!) || Convert.ToString(p.Price).StartsWith(search!) || search == null);
            }
            else if (minPrice is not null && maxPrice is not null)
            {
                productsQuery = productsQuery.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
            }
            else if (categoryId is not null || colorId is not null || tagId is not null)
            {
                productsQuery = productsQuery.Include(p => p.ProductCategories)
                    .Include(p => p.ProductColors)
                    .Include(p => p.ProductTags)
                    .Where(p => categoryId == null || p.ProductCategories!.Any(pc => pc.CategoryId == categoryId))
                    .Where(p => colorId == null || p.ProductColors!.Any(pc => pc.ColorId == colorId))
                    .Where(p => tagId == null || p.ProductTags!.Any(pt => pt.TagId == tagId));

            }
            else
            {
                productsQuery = productsQuery.OrderBy(p => p.Price);
            }

            var newProduct = await productsQuery.Select(p => new ListItemViewModel(p.Id, p.Name!, p.Description!, p.Price,p.CreatedAt,
                             p.ProductImages!.Take(1).FirstOrDefault() != null
                             ? _fileService.GetFileUrl(p.ProductImages!.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Product)
                             : String.Empty,
                              p.ProductImages!.Skip(1).Take(1).FirstOrDefault() != null
                             ? _fileService.GetFileUrl(p.ProductImages!.Skip(1).Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Product)
                             : String.Empty,
                              p.ProductCategories!.Select(p => p.Category).Select(p => new CategoryViewModeL(p.Title!, p.Parent!.Title!)).ToList(),
                              p.ProductColors!.Select(p => p.Color).Select(p => new ColorViewModeL(p!.Name)).ToList(),
                              p.ProductSizes!.Select(p => p.Size).Select(p => new SizeViewModeL(p!.Name)).ToList(),
                              p.ProductTags!.Select(p => p.Tag).Select(p => new TagViewModel(p!.Name)).ToList()
                              )).ToListAsync();

            return View(newProduct);

        }

    }
}
