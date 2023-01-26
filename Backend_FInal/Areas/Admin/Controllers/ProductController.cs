using Backend_Final.Areas.Admin.ViewModels.Category;
using Backend_Final.Areas.Admin.ViewModels.Product;
using Backend_Final.Areas.Admin.ViewModels.Product.Add;
using Backend_Final.Areas.Admin.ViewModels.Size;
using Backend_Final.Areas.Admin.ViewModels.Tag;
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
    [Route("admin/product")]
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

        [HttpGet("list", Name = "admin-product-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Products
                .Select(p => new ListItemViewModel(
                    p.Id,
                    p.Name,
                    p.Description!,
                    p.Price,
                    p.Rate,
                    p.ProductCategories!.Select(pc => pc.Category).Select(c => new CategoryViewModel(c.Title)).ToList(),
                    p.ProductTags!.Select(pt => pt.Tag).Select(t => new TagViewModel(t.Name)).ToList(),
                    p.ProductSizes!.Select(ps => ps.Size).Select(s => new SizeViewModel(s.Name)).ToList(),
                    p.ProductColors!.Select(pco => pco.Color).Select(co => new ColorViewModel(co.Name!)).ToList()))
                .ToListAsync();
            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("add", Name = "admin-product-add")]
        public IActionResult Add()
        {
            var model = new AddViewModel
            {
                Categories = _dataContext.Categories
                    .Select(c => new CategoryListItemViewModel(c.Id, c.Title))
                    .ToList(),
                Sizes = _dataContext.Sizes
                    .Select(s => new SizeListItemViewModel(s.Id, s.Name))
                    .ToList(),
                Colors = _dataContext.Colors
                    .Select(co => new ColorListItemViewModel(co.Id, co.Name!))
                    .ToList(),
                Tags = _dataContext.Tags
                    .Select(t => new TagListItemViewModel(t.Id, t.Name))
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
            foreach (var colorId in model.ColorIds)
            {
                if (!_dataContext.Colors.Any(co => co.Id == colorId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({colorId}) not found in db ");
                    return GetView(model);
                }

            }
            foreach (var sizeId in model.SizeIds!)
            {
                if (!_dataContext.Sizes.Any(s => s.Id == sizeId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({sizeId}) not found in db ");
                    return GetView(model);
                }

            }
            foreach (var tagId in model.TagIds!)
            {
                if (!_dataContext.Tags.Any(t => t.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({tagId}) not found in db ");
                    return GetView(model);
                }

            }


            AddProduct();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");




            IActionResult GetView(AddViewModel model)
            {

                model.Categories = _dataContext.Categories
                   .Select(c => new CategoryListItemViewModel(c.Id, c.Title))
                   .ToList();

                model.Sizes = _dataContext.Sizes
                   .Select(s => new SizeListItemViewModel(s.Id, s.Name))
                   .ToList();

                model.Colors = _dataContext.Colors
                   .Select(c => new ColorListItemViewModel(c.Id, c.Name))
                   .ToList();

                model.Tags = _dataContext.Tags
                   .Select(c => new TagListItemViewModel(c.Id, c.Name))
                   .ToList();

                return View(model);
            }

            Product AddProduct()
            {
                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description!,
                    Price = model.Price,
                    Rate = 0,
                };

                _dataContext.Products.Add(product);

                foreach (var categoryId in model.CategoryIds)
                {
                    var ProductCategory = new ProductCategory
                    {
                        CategoryId = categoryId,

                        Product = product,
                    };

                    _dataContext.ProductCategories.Add(ProductCategory);
                }

                foreach (var sizeId in model.SizeIds)
                {
                    var ProductSize = new ProductSize
                    {
                        SizeId = sizeId,

                        Product = product,
                    };

                    _dataContext.ProductSizes.Add(ProductSize);
                }
                foreach (var colorId in model.ColorIds)
                {
                    var ProductColor = new ProductColor
                    {
                        ColorId = colorId,

                        Product = product,
                    };

                    _dataContext.ProductColors.Add(ProductColor);
                }
                foreach (var tagId in model.TagIds)
                {
                    var ProductTag = new ProductTag
                    {
                        TagId = tagId,

                        Product = product,
                    };

                    _dataContext.ProductTags.Add(ProductTag);
                }

                return product;
            }
        }


        #endregion


        #region Update

        [HttpGet("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var product = await _dataContext.Products
                .Include(p => p.ProductCategories)
                .Include(p => p.ProductSizes)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductTags).FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                return NotFound();
            }

            var model = new AddViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description!,
                Price = product.Price,

                CategoryIds = product.ProductCategories!.Select(pc => pc.CategoryId).ToList(),
                SizeIds = product.ProductSizes!.Select(ps => ps.SizeId).ToList(),
                ColorIds = product.ProductColors!.Select(pco => pco.ColorId).ToList(),
                TagIds = product.ProductTags!.Select(pt => pt.TagId).ToList(),

                Categories = await _dataContext.Categories.Select(c => new CategoryListItemViewModel(c.Id, c.Title)).ToListAsync(),
                Sizes = await _dataContext.Sizes.Select(c => new SizeListItemViewModel(c.Id, c.Name)).ToListAsync(),
                Colors = await _dataContext.Colors.Select(c => new ColorListItemViewModel(c.Id, c.Name!)).ToListAsync(),
                Tags = await _dataContext.Tags.Select(c => new TagListItemViewModel(c.Id, c.Name)).ToListAsync(),

            };
            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> UpdateAsync(AddViewModel model)
        {
            var product = await _dataContext.Products
                .Include(p => p.ProductCategories)
                .Include(p => p.ProductSizes)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductTags).FirstOrDefaultAsync(p => p.Id == model.Id);

            if (product is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            foreach (var id in model.CategoryIds)
            {
                if (!await _dataContext.Catagories.AnyAsync(c => c.Id == id))
                {
                    ModelState.AddModelError(String.Empty, "something went wrong");
                    return GetView(model);
                }
            }

            foreach (var id in model.SizeIds)
            {
                if (!await _dataContext.Sizes.AnyAsync(s => s.Id == id))
                {
                    ModelState.AddModelError(String.Empty, "something went wrong");
                    return GetView(model);
                }
            }


            foreach (var id in model.ColorIds)
            {
                if (!await _dataContext.Colors.AnyAsync(c => c.Id == id))
                {
                    ModelState.AddModelError(String.Empty, "something went wrong");
                    return GetView(model);
                }
            }

            foreach (var id in model.TagIds)
            {
                if (!await _dataContext.Tags.AnyAsync(t => t.Id == id))
                {
                    ModelState.AddModelError(String.Empty, "something went wrong");
                    return GetView(model);
                }
            }
            UpdateProductAsync();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");

            async Task UpdateProductAsync()
            {
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.UpdatedAt = DateTime.Now;

                #region Catagory
                var categoriesInDb = product.ProductCategories.Select(bc => bc.CategoryId).ToList();
                var categoriesToRemove = categoriesInDb.Except(model.CategoryIds).ToList();
                var categoriesToAdd = model.CategoryIds.Except(categoriesInDb).ToList();

                product.ProductCategories.RemoveAll(bc => categoriesToRemove.Contains(bc.CategoryId));

                foreach (var categoryId in categoriesToAdd)
                {
                    var productCatagory = new ProductCategory
                    {
                        CategoryId = categoryId,
                        Product = product,
                    };

                    await _dataContext.ProductCatagories.AddAsync(productCatagory);
                }
                #endregion

                #region Color
                var colorInDb = product.ProductColors.Select(bc => bc.ColorId).ToList();
                var colorToRemove = colorInDb.Except(model.ColorIds).ToList();
                var colorToAdd = model.ColorIds.Except(colorInDb).ToList();

                product.ProductColors.RemoveAll(bc => colorToRemove.Contains(bc.ColorId));


                foreach (var colorId in colorToAdd)
                {
                    var productColor = new ProductColor
                    {
                        ColorId = colorId,
                        Product = product,
                    };

                    await _dataContext.ProductColors.AddAsync(productColor);
                }
                #endregion


                #region Size
                var sizeInDb = product.ProductSizes.Select(bc => bc.SizeId).ToList();
                var sizeToRemove = sizeInDb.Except(model.SizeIds).ToList();
                var sizeToAdd = model.SizeIds.Except(sizeInDb).ToList();

                product.ProductSizes.RemoveAll(bc => sizeToRemove.Contains(bc.SizeId));


                foreach (var sizeId in sizeToAdd)
                {
                    var productSize = new ProductSize
                    {
                        SizeId = sizeId,
                        Product = product,
                    };

                    await _dataContext.ProductSizes.AddAsync(productSize);
                }

                #endregion

                #region Tag
                var tagInDb = product.ProductTags.Select(bc => bc.TagId).ToList();
                var tagToRemove = tagInDb.Except(model.TagIds).ToList();
                var tagToAdd = model.TagIds.Except(tagInDb).ToList();

                product.ProductTags.RemoveAll(bc => tagToRemove.Contains(bc.TagId));


                foreach (var tagId in tagToAdd)
                {
                    var productTag = new ProductTag
                    {
                        TagId = tagId,
                        Product = product,
                    };

                    await _dataContext.ProductTags.AddAsync(productTag);
                }
                #endregion
            }


            IActionResult GetView(AddViewModel model)
            {

                model.Categories = _dataContext.Catagories
                   .Select(c => new CategoryListViewModel(c.Id, c.Title))
                   .ToList();

                model.CategoryIds = product.ProductCategories.Select(c => c.CategoryId).ToList();


                model.Sizes = _dataContext.Sizes
                 .Select(c => new SizeListViewModel(c.Id, c.Name))
                 .ToList();

                model.SizeIds = product.ProductSizes.Select(c => c.SizeId).ToList();



                model.Colors = _dataContext.Colors
                 .Select(c => new ColorListViewModel(c.Id, c.Name))
                 .ToList();

                model.ColorIds = product.ProductColors.Select(c => c.ColorId).ToList();



                model.Tags = _dataContext.Tags
                 .Select(c => new TagListViewModel(c.Id, c.Name))
                 .ToList();

                model.TagIds = product.ProductTags.Select(c => c.TagId).ToList();

                return View(model);
            }

        }
        #endregion













        //#endregion

        //#region Delete

        //[HttpPost("delete/{id}", Name = "admin-Product-delete")]
        //public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        //{
        //    var Product = await _dataContext.Products.FirstOrDefaultAsync(b => b.Id == id);
        //    if (Product is null)
        //    {
        //        return NotFound();
        //    }

        //    //await _fileService.DeleteAsync(Product.ImageNameInFileSystem, UploadDirectory.Product);

        //    _dataContext.Products.Remove(Product);
        //    await _dataContext.SaveChangesAsync();

        //    return RedirectToRoute("admin-Product-list");
        //}

        //#endregion
    }
}
