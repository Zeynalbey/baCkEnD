//using Backend_Final.Areas.Admin.ViewModels.ProductImage;
//using Backend_Final.Contracts.File;
//using Backend_Final.Database;
//using Backend_Final.Database.Models;
//using Backend_Final.Services.Abstracts;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using static System.Net.Mime.MediaTypeNames;

//namespace Backend_Final.Areas.Admin.Controllers
//{
//    [Area("admin")]
//    [Route("admin/Products")]
//    [Authorize(Roles = "admin")]
//    public class ProductImageController : Controller
//    {
//        private readonly DataContext _dataContext;
//        private readonly IFileService _fileService;

//        public ProductImageController(
//            DataContext dataContext,
//            IFileService fileService)
//        {
//            _dataContext = dataContext;
//            _fileService = fileService;
//        }

//        #region List

//        [HttpGet("{ProductId}/image/list", Name = "admin-Product-image-list")]
//        public async Task<IActionResult> ListAsync([FromRoute] int ProductId)
//        {
//            var Product = await _dataContext.Products
//                .Include(b => b.ProductImages)
//                .FirstOrDefaultAsync(b => b.Id == ProductId);

//            if (Product is null)
//            {
//                return NotFound();
//            }

//            var model = new ProductImagesViewModel { ProductId = Product.Id };

//            model.Images = Product.ProductImages!.Select(bi => new ProductImagesViewModel.ListItem
//            {
//                Id = bi.Id,
//                ImageUrL = _fileService.GetFileUrl(bi.ImageNameInFileSystem, UploadDirectory.Product),
//                CreatedAt = bi.CreatedAt
//            }).ToList();

//            return View(model);
//        }

//        #endregion

//        #region Add

//        [HttpGet("{ProductId}/image/add", Name = "admin-Product-image-add")]
//        public async Task<IActionResult> AddAsync()
//        {
//            return View(new AddViewModel());
//        }

//        [HttpPost("{ProductId}/image/add", Name = "admin-Product-image-add")]
//        public async Task<IActionResult> AddAsync([FromRoute] int ProductId, [FromForm] AddViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(model);
//            }

//            var Product = await _dataContext.Products.FirstOrDefaultAsync(b => b.Id == ProductId);
//            if (Product is null)
//            {
//                return NotFound();
//            }

//            var imageNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.Product);

//            var ProductImage = new ProductImage
//            {
//                Product = Product,
//                ImageName = model.Image.FileName,
//                ImageNameInFileSystem = imageNameInSystem
//            };

//            await _dataContext.AddAsync(ProductImage);

//            await _dataContext.SaveChangesAsync();

//            return RedirectToRoute("admin-Product-image-list", new { ProductId = ProductId });
//        }

//        #endregion


//        #region Delete

//        [HttpPost("{ProductId}/image/{ProductImageId}/delete", Name = "admin-Product-image-delete")]
//        public async Task<IActionResult> DeleteAsync(int ProductId, int ProductImageId)
//        {
//            var ProductImage = await _dataContext.ProductImages
//                .FirstOrDefaultAsync(bi => bi.Id == ProductImageId && bi.ProductId == ProductId);
//            if (ProductImage is null)
//            {
//                return NotFound();
//            }

//            await _fileService.DeleteAsync(ProductImage.ImageNameInFileSystem, UploadDirectory.Product);

//            _dataContext.ProductImages.Remove(ProductImage);
//            await _dataContext.SaveChangesAsync();

//            return RedirectToRoute("admin-Product-image-list", new { ProductId = ProductId });
//        }

//        #endregion
//    }
//}
