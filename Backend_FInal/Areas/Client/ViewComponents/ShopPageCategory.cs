using Backend_Final.Areas.Admin.ViewModels.Category;
using Backend_Final.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "ShopPageCategory")]

    public class ShopPageCategory: ViewComponent
    {
        private readonly DataContext _dataContext;

        public ShopPageCategory(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.Categories.Select(c => new CategoryViewModel(c.Id, c.Title)).ToListAsync();

            return View(model);
        }
    }
}
