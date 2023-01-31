using Backend_Final.Areas.Client.ViewModels;
using Backend_Final.Areas.Client.ViewModels.ShopCart;
using Backend_Final.Contracts.File;
using Backend_Final.Database;
using Backend_Final.Database.Models;
using Backend_Final.Services.Abstracts;
using Backend_Final.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static Backend_Final.Areas.Client.ViewModels.ProductListViewModel;

namespace Backend_Final.Areas.Client.Controllers
{
    [Area("client")]
    [Route("shopping-cart")]
    public class ShopCartController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public ShopCartController
            (DataContext dataContext,
            IUserService userService,
            IFileService fileService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _fileService = fileService;
        }

        [HttpGet("list", Name = "client-shop-index")]
        public async Task<IActionResult> Index([FromQuery] int? categoryId, [FromQuery] int? colorId)
        {

            var model = await _dataContext.Products.Include(p => p.ProductColors).Include(p => p.ProductCategories).Where(
            p => categoryId == null || p.ProductCategories!.Any(pc => pc.CategoryId == categoryId))
                   .Where(p => colorId == null || p.ProductColors!.Any(pc => pc.ColorId == colorId))
           .Select(p => new ProductListViewModel(
               p.Id,
               p.Name!,
               p.Description!,
               p.Price,
               p.ProductImages!.Take(1).FirstOrDefault() != null
                   ? _fileService.GetFileUrl(p.ProductImages!.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Product)
                   : String.Empty,
               p.ProductCategories!.Select(pc => pc.Category).Select(c => new CategoryViewModeL(c.Title, c.Parent!.Title)).ToList(),
               p.ProductColors!.Select(pc => pc.Color).Select(c => new ColorViewModeL(c.Name!)).ToList(),
               p.ProductSizes!.Select(ps => ps.Size).Select(s => new SizeViewModeL(s.Name)).ToList(),
               p.ProductTags!.Select(ps => ps.Tag).Select(s => new TagViewModel(s.Name)).ToList()
               )).ToListAsync();

            return View(model);

        }

    }
}
