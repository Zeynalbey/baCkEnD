using Backend_Final.Areas.Admin.ViewModels.Tag;
using Backend_Final.Areas.Client.ViewModels;
using Backend_Final.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Backend_Final.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "ShopPageTag")]

    public class ShopPageTag: ViewComponent
    {
        private readonly DataContext _dataContext;

        public ShopPageTag(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.Tags.Select(c => new TagViewModel(c.Id, c.Name)).ToListAsync();

            return View(model);
        }
    }
}
