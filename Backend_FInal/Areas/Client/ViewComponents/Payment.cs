using Backend_Final.Areas.Admin.ViewModels.Payment;
using Backend_Final.Areas.Client.ViewModels;
using Backend_Final.Contracts.File;
using Backend_Final.Database;
using Backend_Final.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final.Areas.Client.ViewComponents
{
    [Area("client")]
    [ViewComponent(Name = "Payment")]
    public class Payment : ViewComponent
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public Payment(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dbContext.Payments.Select(p => new PaymentListViewModel(
                  p.Id, p.Title!, p.Content!, _fileService.GetFileUrl(p.IconİmageInSystem, UploadDirectory.Payment))).ToListAsync();

            return View(model);
        }
    }
}
