using Microsoft.AspNetCore.Mvc;

namespace Backend_Final.Areas.Client.Controllers
{
    [Area("client")]
    [Route("Blog")]
    public class BlogController : Controller
    {
        [HttpGet("blogindex",Name ="client-blog-index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
