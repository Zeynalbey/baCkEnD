using Backend_Final.Database;
using Backend_Final.Services.Abstracts;
using Backend_Final.Areas.Admin.ViewModels.Slider;
using Backend_Final.Contracts.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoApplication.Database.Models;

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

            //foreach (var entity in _dataContext.Sliders)
            //    _dataContext.Sliders.Remove(entity);



            _dataContext.Sliders.Add(slider);

            _dataContext.SaveChanges();

            return RedirectToRoute("admin-slider-list");

        }

        //[HttpPost("delete/{id}", Name = "admin-slider-delete")]
        //public async Task<IActionResult> Delete([FromRoute] int id)
        //{
        //    var slider = await _dataContext.Sliders.FirstOrDefaultAsync(b => b.Id == id);
        //    if (slider is null)
        //    {
        //        return NotFound();
        //    }
        //    await _fileService.DeleteAsync(slider.BackgroundİmageInFileSystem, UploadDirectory.Slider);
        //    _dataContext.Sliders.Remove(slider);
        //    await _dataContext.SaveChangesAsync();

        //    return RedirectToRoute("admin-slider-list");
        //}
    }
}
