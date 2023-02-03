using Backend_Final.Areas.Client.ViewModels.Blog;
using Backend_Final.Areas.Client.ViewModels.Home;
using Backend_Final.Contracts.File;
using Backend_Final.Database;
using Backend_Final.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Xml.Linq;

namespace Backend_Final.Areas.Client.ViewComponents
{
   [ViewComponent(Name = "Blog")]

    public class Blog : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public Blog(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model =
               await _dataContext.Blogs
               .Include(b => b.BlogAndTags)
               .Include(b => b.BlogAndCategories)
               .Include(b => b.BlogDisplays)
                .Select(b => new BlogListViewModel(b.Id, b.Title, b.Content,
                b.BlogDisplays!.Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(b.BlogDisplays!.Take(1).FirstOrDefault()!.FileNameInSystem, UploadDirectory.Blog)
                : String.Empty,
                b.BlogDisplays!.FirstOrDefault()!.IsImage != null ?
                 b.BlogDisplays!.FirstOrDefault()!.IsImage : default,
                b.BlogDisplays!.FirstOrDefault()!.IsVidio != null ?
                 b.BlogDisplays!.FirstOrDefault()!.IsVidio : default,
                b.BlogAndTags!.Select(b => b.Tag).Select(b => new BlogListViewModel.TagViewModel(b.Title)).ToList(),
                b.BlogAndCategories!.Select(b => b.Category)
                .Select(b => new BlogListViewModel.CategoryViewModeL(b.Title)).ToList()
                )).ToListAsync();

            return View(model);
        }
    }//bax zeynal 37 setr sene null gele biler birde 38 ordani yoxlamlisan 38 nullsdu gozle
}
//ikiside false gelit yoxdu axi sekil nede video sekil blogdisplayde olmalidi.