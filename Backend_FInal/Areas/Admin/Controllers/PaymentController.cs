using Backend_Final.Database;
using Backend_Final.Services.Abstracts;
using Backend_Final.Areas.Admin.ViewModels.Payment;
using Backend_Final.Contracts.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend_Final.Services.Concretes;
using BackendFinal.Migrations;

namespace Backend_Final.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/payment")]
    [Authorize(Roles = "admin")]
    public class PaymentController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public PaymentController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        [HttpGet("list", Name = "admin-payment-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Payments.Select(p => new PaymentListViewModel(
                p.Id,
                p.Title!,
                p.Content!,
                _fileService.GetFileUrl(p.IconImage, UploadDirectory.Payment))).ToListAsync();

            return View(model);
        }

        [HttpGet("add", Name = "admin-payment-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }
        [HttpPost("add", Name = "admin-payment-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var imageNameInSystem = await _fileService.UploadAsync(model.Iconİmage!, UploadDirectory.Payment);

            var payment = new Database.Models.Payment
            {
                Title = model.Title,
                Content = model.Content,
                IconImage = model.Iconİmage!.FileName,
                IconİmageInSystem = imageNameInSystem
            };

            await _dataContext.Payments.AddAsync(payment);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-payment-list");
        }

        [HttpGet("update/{id}", Name = "admin-payment-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var payment = await _dataContext.Payments.FirstOrDefaultAsync(n => n.Id == id);

            if (payment is null)
            {
                return NotFound();
            }

            var model = new AddViewModel()
            {
                Id = payment.Id,
                Title = payment.Title!,
                Content = payment.Content!,
                IconImageUrl = _fileService.GetFileUrl(payment.IconİmageInSystem, UploadDirectory.Payment)
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-payment-update")]
        public async Task<IActionResult> UpdateAsync(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var payment = await _dataContext.Payments.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (payment is null)
            {
                return NotFound();
            }

            var imageNameInSystem = await _fileService.UploadAsync(model.Iconİmage!, UploadDirectory.Payment);

            payment.Title = model.Title;
            payment.Content = model.Content;
            payment.IconImage = model.Iconİmage!.FileName;
            payment.IconİmageInSystem = imageNameInSystem;

 
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-payment-list");
        }


        [HttpPost("delete/{id}", Name = "admin-payment-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var payment = await _dataContext.Payments.FirstOrDefaultAsync(b => b.Id == id);
            if (payment is null)
            {
                return NotFound();
            }
            await _fileService.DeleteAsync(payment.IconİmageInSystem, UploadDirectory.Payment);
            _dataContext.Payments.Remove(payment);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-payment-list");
        }
    }
}
