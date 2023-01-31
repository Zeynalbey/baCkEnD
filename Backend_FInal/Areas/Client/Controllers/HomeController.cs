using Backend_Final.Areas.Admin.ViewModels.Payment;
using Backend_Final.Areas.Admin.ViewModels.Product;
using Backend_Final.Areas.Admin.ViewModels.Product.Add;
using Backend_Final.Areas.Admin.ViewModels.Slider;
using Backend_Final.Areas.Client.ViewComponents;
using Backend_Final.Areas.Client.ViewModels.Home.Contact;
using Backend_Final.Areas.Client.ViewModels.Home.Index;
using Backend_Final.Contracts.File;
using Backend_Final.Database;
using Backend_Final.Database.Models;
using Backend_Final.Services.Abstracts;
using Backend_Final.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using IndexViewModel = Backend_Final.Areas.Client.ViewModels.Home.Index.IndexViewModel;


namespace Backend_Final.Areas.Client.Controllers
{
    [Area("client")]
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public HomeController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;

        }

        [HttpGet("~/", Name = "client-home-index")]
        [HttpGet("index")]
        public async Task<IActionResult> IndexAsync([FromServices] IFileService fileService)
        {
            return View();
        }
        // p.ProductImages.Take(1).FirstOrDefault()

        [HttpGet("modal/{id}", Name = "plant-modal")]
        public async Task<ActionResult> ModalAsync(int id)
        {

            var product = await _dbContext.Products.Include(p => p.ProductImages)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductSizes).FirstOrDefaultAsync(p => p.Id == id);


            if (product is null)
            {
                return NotFound();
            }

            var model = new IndexViewModel(product.Id, product.Name, product.Description, product.Price,
                product.ProductImages!.Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(product.ProductImages.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Product)
            : String.Empty,
                _dbContext.ProductColors.Include(pc => pc.Color).Where(pc => pc.ProductId == product.Id)
                .Select(pc => new IndexViewModel.ColorViewModeL(pc.Color.Name, pc.Color.Id)).ToList(),
                _dbContext.ProductSizes.Include(ps => ps.Size).Where(ps => ps.ProductId == product.Id)
                .Select(ps => new IndexViewModel.SizeViewModeL(ps.Size.Name, ps.Size.Id)).ToList()
                );

            return PartialView("~/Areas/Client/Views/Shared/Partials/_ModalPartial.cshtml", model);
        }


    }
}
