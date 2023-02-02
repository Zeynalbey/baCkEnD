
using Backend_Final.Areas.Client.ViewModels.Home;
using Backend_Final.Areas.Client.ViewModels.Home.Index;
using Backend_Final.Contracts.File;
using Backend_Final.Database;
using Backend_Final.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "ClientFeedback")]
    public class ClientFeedback : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public ClientFeedback(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new IndexListViewModel
            {
                ClientFeedbacks = await _dataContext.Feedbacks
                .Select(f => new ClientFeedbackViewModel(f.Id, f.User.FirstName!, f.User.LastName!, f.User.Role!.Name!, f.Content,
                _fileService.GetFileUrl(f.ImageNameInFileSystem, UploadDirectory.FeedBack)))
                .ToListAsync()
            };


            return View(model);
        }
    }
}
