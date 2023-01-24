using Backend_Final.Database;
using Backend_Final.Database.Models;
using Backend_Final.ViewModels.Admin.Navbar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AddViewModel = Backend_Final.ViewModels.Admin.Navbar.AddViewModel;

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
                   n.Id, n.Title, n.Order, n.IsBold, n.IsHeader,  n.IsFooter)).ToListAsync();

            return View("~/Views/Admin/Navbar/List.cshtml", model);
        }

        [HttpGet("add", Name = "admin-navbar-add")]
        public IActionResult Add()
        {
            return View("~/Views/Admin/Navbar/Add.cshtml");
        }
        [HttpPost("add", Name = "admin-navbar-add")]
        public IActionResult Add(AddViewModel model)
        {
            if (!model.IsFooter && !model.IsHeader)
            {
                ModelState.AddModelError(String.Empty, "You Must Choose Header or Footer");
                return View("~/Views/Admin/Navbar/Add.cshtml", model);
            }
            
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Navbar/Add.cshtml",model);
            }

            var newModel = new Navbar()
            {
                Title = model.Title,
                Url = model.Url,
                Order = model.Order,
                IsBold = model.IsBold,
                IsHeader = model.IsHeader,
                IsFooter = model.IsFooter,
            };
            _dataContext.Navbars.Add(newModel);
            _dataContext.SaveChanges();

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
                Title = model.Title,
                Url = model.Url,
                Order = model.Order,
                IsBold=model.IsBold,
                IsHeader=model.IsHeader,
                IsFooter=model.IsFooter,    
            };
      

            return View("~/Views/Admin/Navbar/Update.cshtml",newModel);
        }

        [HttpPost("update/{id}", Name = "admin-navbar-update")]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            if (!model.IsFooter && !model.IsHeader)
            {
                ModelState.AddModelError(String.Empty, "You Must Choose Header or Footer");
                return View("~/Views/Admin/Navbar/Update.cshtml", model);
            }
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Navbar/Update.cshtml", model);
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
            navbar.IsHeader = model.IsHeader;
            navbar.IsFooter = model.IsFooter;

            _dataContext.SaveChanges();

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
