
using Backend_Final.Areas.Admin.ViewModels.BlogDisplay;
using Backend_Final.Contracts.File;
using Backend_Final.Database;
using Backend_Final.Database.Models;
using Backend_Final.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/blogDisplay")]
    [Authorize(Roles = "admin")]
    public class BlogDisplayController : Controller
    {
        private readonly DataContext _dataContext;

        private readonly IFileService _fileService;

        public BlogDisplayController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }


        #region List'

        [HttpGet("{blogId}/blogDisplay/list", Name = "admin-blogDisplay-list")]
        public async Task<IActionResult> List([FromRoute] int blogId)
        {
            var product = await _dataContext.Blogs
                .Include(p => p.BlogDisplays).FirstOrDefaultAsync(p => p.Id == blogId);

            if (product == null) return NotFound();

            var model = new BlogDisplayViewModel { BlogId = product.Id };

            model.Files = product.BlogDisplays.Select(p => new BlogDisplayViewModel.ListItem
            {
                Id = p.Id,
                FileUrl = _fileService.GetFileUrl(p.FileNameInSystem, UploadDirectory.Blog),
                IsImage = p.IsImage,
                IsVideo = p.IsVidio
            }).ToList();

            return View(model);

        }
        #endregion


        #region Add'

        [HttpGet("{blogId}/blogDisplay/add", Name = "admin-blogDisplay-add")]
        public async Task<IActionResult> Add()
        {
            return View(new AddViewModel());
        }

        [HttpPost("{blogId}/blogDisplay/add", Name = "admin-blogDisplay-add")]
        public async Task<IActionResult> Add([FromRoute] int blogId, AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var product = await _dataContext.Blogs.FirstOrDefaultAsync(p => p.Id == blogId);

            if (product is null) return NotFound();
          

            var imageNameInSystem = await _fileService.UploadAsync(model.File, UploadDirectory.Blog);

            var productImage = new BlogDisplay
            {
                Blog = product,
                FileName = model.File.FileName,
                FileNameInSystem = imageNameInSystem,
                IsImage = model.IsImage,
                IsVidio = model.IsVideo
            };

            await _dataContext.BlogDisplays.AddAsync(productImage);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blogDisplay-list", new { blogId = blogId });

        }
        #endregion

        #region Delete'
        [HttpPost("{blogId}/blogDisplay/{blogFileId}/delete", Name = "admin-blogDisplay-delete")]
        public async Task<IActionResult> Delete([FromRoute] int blogId, [FromRoute] int blogFileId)
        {

            var productImage = await _dataContext.BlogDisplays
                .FirstOrDefaultAsync(p => p.BlogId == blogId && p.Id == blogFileId);

            if (productImage is null) return NotFound();
          

            await _fileService.DeleteAsync(productImage.FileNameInSystem, UploadDirectory.Blog);

            _dataContext.BlogDisplays.Remove(productImage);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blogDisplay-list", new { blogId = blogId });

        }
        #endregion
    }
}
