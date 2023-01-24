using Backend_Final.Database;
using Backend_Final.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Backend_Final.Areas.Client.ViewComponents
{
    [ViewComponent(Name ="Navbar")]
    public class Navbar : ViewComponent
    {
        private readonly DataContext _datacontext;
        public Navbar(DataContext datacontext)
        {
            _datacontext = datacontext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {


            var model = _datacontext.Navbars.Include(n => n.Subnavbars.OrderBy(sn => sn.Order)).OrderBy(n => n.Order).ToList();

            return View(model);
        }
    }
}
