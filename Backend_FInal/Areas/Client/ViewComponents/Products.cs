using Backend_Final.Areas.Client.ViewModels;
using Backend_Final.Contracts.File;
using Backend_Final.Database;
using Backend_Final.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final.Areas.Client.ViewComponents
{
    [Area("client")]
    [ViewComponent(Name = "Products")]
    public class Products : ViewComponent
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public Products(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dbContext.Products.OrderByDescending(p => p.CreatedAt).Select(p => new ProductViewModel(
               p.Id,
               p.Name!,
               p.Price,
               p.ProductImages!.Take(1).FirstOrDefault() != null
                   ? _fileService.GetFileUrl(p.ProductImages!.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Product)
                   : String.Empty
               )).ToListAsync();

            return View(model);
        }
    }
}
