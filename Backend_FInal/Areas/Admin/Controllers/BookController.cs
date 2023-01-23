using Backend_Final.Areas.Admin.ViewModels.Product;
using Backend_Final.Areas.Admin.ViewModels.Product.Add;
using Backend_Final.Contracts.File;
using Backend_Final.Database;
using Backend_Final.Database.Models;
using Backend_Final.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Backend_Final.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/Product")]
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<ProductController> _logger;
        private readonly IFileService _fileService;

        public ProductController(
            DataContext dataContext,
            ILogger<ProductController> logger,
            IFileService fileService)
        {
            _dataContext = dataContext;
            _logger = logger;
            _fileService = fileService;
        }


        #region List

        [HttpGet("list", Name = "admin-Product-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Products
                .Select(b => new ListItemViewModel(
                        b.Id,
                        b.Title,
                        b.Price,
                        b.CreatedAt,
                        b.ProductCategories
                            .Select(bc => bc.Category)
                                .Select(c => new ListItemViewModel.CategoryViewModeL(c.Title, c.Parent.Title)).ToList()))
                .ToListAsync();

            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("add", Name = "admin-Product-add")]
        public IActionResult Add()
        {
            var model = new AddViewModel
            {
                Categories = _dataContext.Categories
                    .Select(c => new CategoryListItemViewModel(c.Id, c.Title))
                    .ToList(),
            };

            return View(model);
        }

        [HttpPost("add", Name = "admin-Product-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            foreach (var categoryId in model.CategoryIds)
            {
                if (!_dataContext.Categories.Any(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({categoryId}) not found in db ");
                    return GetView(model);
                }

            }


            var Product = AddProduct();

            await _dataContext.SaveChangesAsync();  

            return RedirectToRoute("admin-Product-list");




            IActionResult GetView(AddViewModel model)
            {

                model.Categories = _dataContext.Categories
                   .Select(c => new CategoryListItemViewModel(c.Id, c.Title))
                   .ToList();

                return View(model);
            }

            Product AddProduct()
            {
                var Product = new Product
                {
                    Title = model.Title,
                    Price = model.Price
                };

                _dataContext.Products.Add(Product);

                foreach (var categoryId in model.CategoryIds)
                {
                    var ProductCategory = new ProductCategory
                    {
                        CategoryId = categoryId,
                        Product = Product,
                    };

                    _dataContext.ProductCategories.Add(ProductCategory);
                }

                return Product;
            }
        }


        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "admin-Product-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var Product = await _dataContext.Products.Include(b => b.ProductCategories).FirstOrDefaultAsync(b => b.Id == id);
            if (Product is null)
            {
                return NotFound();
            }

            var model = new AddViewModel
            {
                Id = Product.Id,
                Title = Product.Title,
                Price = Product.Price,
                Categories = _dataContext.Categories
                    .Select(c => new CategoryListItemViewModel(c.Id, c.Title))
                    .ToList(),
                CategoryIds = Product.ProductCategories.Select(bc => bc.CategoryId).ToList(),
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-Product-update")]
        public async Task<IActionResult> UpdateAsync(AddViewModel model)
        {
            var Product = await _dataContext.Products.Include(b => b.ProductCategories).FirstOrDefaultAsync(b => b.Id == model.Id);
            if (Product is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return GetView(model);
            }


            foreach (var categoryId in model.CategoryIds)
            {
                if (!_dataContext.Categories.Any(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({categoryId}) not found in db ");
                    return GetView(model);
                }

            }


            //await _fileService.DeleteAsync(Product.ImageNameInFileSystem, UploadDirectory.Product);
            //var imageFileNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.Product);

            await UpdateProductAsync();

            return RedirectToRoute("admin-Product-list");




            IActionResult GetView(AddViewModel model)
            {
                model.Categories = _dataContext.Categories
                   .Select(c => new CategoryListItemViewModel(c.Id, c.Title))
                   .ToList();

                model.CategoryIds = Product.ProductCategories.Select(bc => bc.CategoryId).ToList();

                return View(model);
            }

            async Task UpdateProductAsync()
            {
                Product.Title = model.Title;
                Product.Price = model.Price;

                var categoriesInDb = Product.ProductCategories.Select(bc => bc.CategoryId).ToList();
                var categoriesToRemove = categoriesInDb.Except(model.CategoryIds).ToList();
                var categoriesToAdd = model.CategoryIds.Except(categoriesInDb).ToList();

                Product.ProductCategories.RemoveAll(bc => categoriesToRemove.Contains(bc.CategoryId));

                foreach (var categoryId in categoriesToAdd)
                {
                    var ProductCategory = new ProductCategory
                    {
                        CategoryId = categoryId,
                        Product = Product,
                    };

                    _dataContext.ProductCategories.Add(ProductCategory);
                }

                _dataContext.SaveChanges();
            }
        }

        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-Product-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var Product = await _dataContext.Products.FirstOrDefaultAsync(b => b.Id == id);
            if (Product is null)
            {
                return NotFound();
            }

            //await _fileService.DeleteAsync(Product.ImageNameInFileSystem, UploadDirectory.Product);

            _dataContext.Products.Remove(Product);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-Product-list");
        }

        #endregion
    }
}
