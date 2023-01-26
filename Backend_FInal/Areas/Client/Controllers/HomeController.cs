using Backend_Final.Areas.Admin.ViewModels.Payment;
using Backend_Final.Areas.Admin.ViewModels.Slider;
using Backend_Final.Areas.Client.ViewModels.Home.Contact;
using Backend_Final.Areas.Client.ViewModels.Home.Index;
using Backend_Final.Contracts.File;
using Backend_Final.Database;
using Backend_Final.Database.Models;
using Backend_Final.Services.Abstracts;
using Backend_Final.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using IndexViewModel = Backend_Final.Areas.Client.ViewModels.Home.Index.IndexViewModel;


namespace Backend_Final.Areas.Client.Controllers
{
    [Area("client")]
    [Route("home")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public HomeController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;

        }

        [HttpGet("~/", Name = "client-home-index")]
        [HttpGet("index")]
        public async Task<IActionResult> IndexAsync([FromServices] IFileService fileService)
        {
            var model = new IndexViewModel
            {

                Sliders = await _dbContext.Sliders.Select(s => new ListViewModel(
                    s.Id,
                    s.MainTitle!,
                    s.Content!,
                    s.SecondTitle!,
                    s.Button!,
                    s.ButtonRedirectUrl!,
                    s.Order,
                    _fileService.GetFileUrl(s.İmageInSystem, UploadDirectory.Slider),
                    s.CreatedAt)).ToListAsync(),

                Payments = await _dbContext.Payments.Select(p => new PaymentListViewModel(
                    p.Id, p.Title!, p.Content!, _fileService.GetFileUrl(p.IconİmageInSystem, UploadDirectory.Payment))).ToListAsync()

            };



            return View(model);
        }

        [HttpGet("contact")]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public ActionResult Contact([FromForm] CreateViewModel contactViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _dbContext.Contacts.Add(new Contact
            {
                Name = contactViewModel.Name,
                Email = contactViewModel.Email,
                Message = contactViewModel.Message,
                Phone = contactViewModel.PhoneNumber,
            });

            return RedirectToAction(nameof(Index));
        }
    }
}
