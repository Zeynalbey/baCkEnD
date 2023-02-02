using Backend_Final.Areas.Client.ViewModels.Shop;
using Backend_Final.Contracts.File;
using Backend_Final.Database;
using Backend_Final.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final.Areas.Client.Controllers
{
    [Area("client")]
    [Route("shop")]
    public class ShopController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public ShopController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }


        [HttpGet("index/{id}", Name = "client-shop-index")]
        public async Task<IActionResult> Index(int id)
        {
            var product = await _dataContext.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductSizes)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null) return NotFound();          

            var model = new ShopViewModel(product.Name!, product.Description!, product.Price,

                _dataContext.ProductImages.Where(p => p.ProductId == product.Id)
                .Select(p => new ShopViewModel.ImageViewModeL(_fileService.GetFileUrl(p.ImageNameInFileSystem, UploadDirectory.Product))).ToList(),
                
                _dataContext.ProductCategories.Include(ps => ps.Category).Where(ps => ps.ProductId == product.Id)
                .Select(ps => new ShopViewModel.CatagoryViewModeL(ps.Category!.Title!, ps.Category.Id)).ToList(),

                _dataContext.ProductColors.Include(pc => pc.Color).Where(pc => pc.ProductId == product.Id)
                .Select(pc => new ShopViewModel.ColorViewModeL(pc.Color!.Name, pc.Color.Id)).ToList(),

                _dataContext.ProductSizes.Include(ps => ps.Size).Where(ps => ps.ProductId == product.Id)
                .Select(ps => new ShopViewModel.SizeViewModeL(ps.Size!.Name, ps.Size.Id)).ToList(),

                _dataContext.ProductTags.Include(ps => ps.Tag).Where(ps => ps.ProductId == product.Id)
                .Select(ps => new ShopViewModel.TagViewModeL(ps.Tag!.Name, ps.Tag.Id)).ToList()
                );

            return View(model);
        }
    }
}
