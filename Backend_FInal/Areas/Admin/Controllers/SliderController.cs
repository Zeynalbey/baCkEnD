using Backend_Final.Database;
using Backend_Final.Services.Abstracts;
using Backend_Final.Areas.Admin.ViewModels.Slider;
using Backend_Final.Contracts.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend_Final.Database.Models;

namespace Backend_Final.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/slider")]
    [Authorize(Roles = "admin")]
    public class SliderController : Controller
    {
        private readonly DataContext _dataContext;

        private readonly IFileService _fileService;

        public SliderController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        [HttpGet("list", Name = "admin-slider-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Sliders.Select(s => new ListViewModel(
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

        [HttpGet("add", Name = "admin-slider-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }
        [HttpPost("add", Name = "admin-slider-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var imageNameInSystem = await _fileService.UploadAsync(model.İmage!, UploadDirectory.Slider);


            var slider = new Slider
            {
                MainTitle = model.MainTitle,
                Content = model.Content,
                İmage = model.İmage!.FileName,
                İmageInSystem = imageNameInSystem,
                Button = model.Button,
                ButtonRedirectUrl = model.ButtonRedirectUrl,
                Order = model.Order,
                CreatedAt = DateTime.Now,
            };

            _dataContext.Sliders.Add(slider);

            _dataContext.SaveChanges();

            return RedirectToRoute("admin-slider-list");

        }


        [HttpGet("update/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(n => n.Id == id);

            if (slider is null)
            {
                return NotFound();
            }

            var model = new AddViewModel()
            {
                Id = slider.Id,
                MainTitle = slider.MainTitle!,
                Content = slider.Content!,
                SecondTitle = slider.SecondTitle,
                Button = slider.Button,
                ButtonRedirectUrl = slider.ButtonRedirectUrl, 
                Order = slider.Order,
                İmageUrl = _fileService.GetFileUrl(slider.İmageInSystem, UploadDirectory.Slider)
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> UpdateAsync(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (slider is null)
            {
                return NotFound();
            }

            var imageNameInSystem = await _fileService.UploadAsync(model.İmage!, UploadDirectory.Slider);

            slider.MainTitle = model.MainTitle;
            slider.Content = model.Content;
            slider.İmage = model.İmage!.FileName;
            slider.İmageInSystem = imageNameInSystem;


            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-slider-list");
        }

        [HttpPost("delete/{id}", Name = "admin-slider-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(b => b.Id == id);
            if (slider is null)
            {
                return NotFound();
            }
            await _fileService.DeleteAsync(slider.İmageInSystem, UploadDirectory.Slider);
            _dataContext.Sliders.Remove(slider);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-slider-list");
        }
    }
}
