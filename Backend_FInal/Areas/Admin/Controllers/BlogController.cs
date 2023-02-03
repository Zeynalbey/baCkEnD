
using Backend_Final.Areas.Admin.ViewModels.Blog;
using Backend_Final.Areas.Client.ViewModels.Blog;
using Backend_Final.Database;
using Backend_Final.Database.Models;
using Backend_Final.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DemoApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/blog")]
    [Authorize(Roles = "admin")]
    public class BlogController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public readonly ILogger<BlogController> _logger;

        public BlogController(DataContext dataContext, IFileService fileService, ILogger<BlogController> logger)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _logger = logger;
        }

        #region List'

        [HttpGet("list", Name = "admin-blog-list")]
        public async Task<IActionResult> List()
        {

            var model = await _dataContext.Blogs.OrderByDescending(p => p.CreatedAt)
                .Select(p => new BlogListItemViewModel(p.Id, p.Title, p.Content, p.CreatedAt,
                p.BlogAndTags!.Select(ps => ps.Tag)
                .Select(s => new BlogListItemViewModel.TagViewModel(s.Title)).ToList(),
                p.BlogAndCategories!.Select(pc => pc.Category)
                .Select(c => new BlogListItemViewModel.CategoryViewModeL(c.Title, c.Parent!.Title)).ToList()
              
                )).ToListAsync();


            return View(model);
        }

        #endregion

        #region Add' 

        [HttpGet("add", Name = "admin-blog-add")]
        public async Task<IActionResult> Add()
        {
            var model = new AddViewModel
            {
                Categories = await _dataContext.BlogCategories
                    .Select(c => new BlogCategoryViewModel(c.Id, c.Title))
                    .ToListAsync(),
                Tags = await _dataContext.BlogTags
                    .Select(t => new BlogTagViewModel(t.Id, t.Title))
                    .ToListAsync()
            };

            return View(model);
        }

        [HttpPost("add", Name = "admin-blog-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            foreach (var categoryId in model.CategoryIds)
            {
                if (!await _dataContext.BlogCategories.AnyAsync(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with ({categoryId}) not found in database ");
                    return GetView(model);
                }

            }

            foreach (var tagId in model.TagIds)
            {
                if (!await _dataContext.BlogTags.AnyAsync(c => c.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Tag with ({tagId}) not found in database ");
                    return GetView(model);
                }

            }



            AddProduct();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blog-list");



            IActionResult GetView(AddViewModel model)
            {

                model.Categories = _dataContext.BlogCategories
                   .Select(c => new BlogCategoryViewModel(c.Id, c.Title))
                   .ToList();

                model.Tags = _dataContext.BlogTags
                 .Select(c => new BlogTagViewModel(c.Id, c.Title))
                 .ToList();


                return View(model);
            }


            async void AddProduct()
            {
                var product = new Blog
                {
                    Title = model.Title,
                    Content = model.Content,
                    CreatedAt = DateTime.Now,
                };


                await _dataContext.Blogs.AddAsync(product);


                foreach (var catagoryId in model.CategoryIds)
                {
                    var productCatagory = new BlogAndBlogCategory
                    {
                        BlogCategoryId = catagoryId,
                        Blog = product,
                    };

                    await _dataContext.BlogAndBlogCategories.AddAsync(productCatagory);
                }

                foreach (var tagId in model.TagIds)
                {
                    var productTag = new BlogAndBlogTag

                    {
                        BlogTagId = tagId,
                        Blog = product

                    };

                    await _dataContext.BlogAndBlogTags.AddAsync(productTag);
                }


            }
        } 
        #endregion


        #region Delete'

        [HttpPost("delete/{id}", Name = "admin-blog-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var products = await _dataContext.Blogs.FirstOrDefaultAsync(p => p.Id == id);

            if (products is null) return NotFound();
           

            _dataContext.Blogs.Remove(products);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-blog-list");
        }


        #endregion
    }
}
