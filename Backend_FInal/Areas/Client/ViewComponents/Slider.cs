using Backend_Final.Areas.Admin.ViewModels.Slider;
using Backend_Final.Areas.Client.ViewModels;
using Backend_Final.Contracts.File;
using Backend_Final.Database;
using Backend_Final.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final.Areas.Client.ViewComponents
{
    [Area("client")]
    [ViewComponent(Name = "Slider")]
    public class Slider : ViewComponent
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public Slider(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dbContext.Sliders.Select(s => new ListViewModel(
                    s.Id,
                    s.MainTitle!,
                    s.Content!,
                    s.SecondTitle!,
                    s.Button!,
                    s.ButtonRedirectUrl!,
                    s.Order,
                    _fileService.GetFileUrl(s.İmageInSystem, UploadDirectory.Slider),
                    s.CreatedAt)).ToListAsync();

            return View(model);
        }
    }
}
