using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Final.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/dashboard")]
    [Authorize(Roles = "admin, moderator")]
    public class DashboardController : Controller
    {
        [HttpGet(Name = "admin-dashboard-index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
