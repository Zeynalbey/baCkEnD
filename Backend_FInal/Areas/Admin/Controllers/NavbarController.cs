using Backend_Final.Areas.Admin.ViewModels.Navbar;
using Backend_Final.Database;
using Backend_Final.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final.Controllers.Admin
{
    [Area("admin")]
    [Route("admin/navbars")]
    [Authorize(Roles = "admin")]
    public class NavbarController : Controller
    {
        private readonly DataContext _dataContext;
     

        public NavbarController(DataContext dataContext)
        {
            _dataContext = dataContext;
          
        }

        [HttpGet("list", Name = "admin-navbar-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Navbars
                .Select(n => new ListViewModel(
                   n.Id, n.Title!, n.Order, n.Url!, n.IsBold)).ToListAsync();

            return View(model);
        }

        [HttpGet("add", Name = "admin-navbar-add")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost("add", Name = "admin-navbar-add")]
        public async Task <IActionResult> AddAsync(Backend_Final.Areas.Admin.ViewModels.Navbar.AddViewModel model)
        {          
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var newModel = new Navbar()
            {
                Title = model.Title,
                Url = model.Url,
                Order = model.Order,
                IsBold = model.IsBold,
            };
            await _dataContext.Navbars.AddAsync(newModel);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-navbar-list");
        }

        [HttpGet("update/{id}", Name = "admin-navbar-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var model = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == id);
            if (model is null)
            {
                return NotFound();
            }

            var newModel = new UpdateViewModel()
            {
                Id = model.Id,
                Title = model.Title!,
                Url = model.Url!,
                Order = model.Order,
                IsBold = model.IsBold
            };
      

            return View(newModel);
        }

        [HttpPost("update/{id}", Name = "admin-navbar-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (navbar is null)
            {
                return NotFound();
            }

            navbar.Title = model.Title;
            navbar.Url = model.Url;
            navbar.Order = model.Order;
            navbar.IsBold = model.IsBold;

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-navbar-list");
        }

        [HttpPost("delete/{id}", Name = "admin-navbar-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var model = await _dataContext.Navbars.FirstOrDefaultAsync(b => b.Id == id);
            if (model is null)
            {
                return NotFound();
            }

            _dataContext.Navbars.Remove(model);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-navbar-list");
        }
    }
}
