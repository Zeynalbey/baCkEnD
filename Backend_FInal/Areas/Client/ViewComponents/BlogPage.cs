using Backend_Final.Areas.Client.ViewModels.Blog;
using Backend_Final.Database;
using Backend_Final.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "BlogPage")]

    public class BlogPage :ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public BlogPage(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model =
               await _dataContext.Blogs
               .Include(b => b.BlogAndTags).Include(b => b.BlogAndCategories).Include(b => b.BlogDisplays)
                .Select(b => new BlogListViewModel(b.Id, b.Title, b.Content,
                b.BlogDisplays!.Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(b.BlogDisplays!.Take(1).FirstOrDefault()!.FileNameInSystem, Contracts.File.UploadDirectory.Blog)
                : String.Empty,
                b.BlogDisplays!.FirstOrDefault()!.IsImage,
                b.BlogDisplays!.FirstOrDefault()!.IsVidio,
                b.BlogAndTags!.Select(b => b.Tag).Select(b => new BlogListViewModel.TagViewModel(b.Title)).ToList(),
                b.BlogAndCategories!.Select(b => b.Category)
                .Select(b => new BlogListViewModel.CategoryViewModeL(b.Title)).ToList()
                )).ToListAsync();

            return View(model);
        }
    }
}
