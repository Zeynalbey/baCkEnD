using Backend_Final.Areas.Client.ViewComponents;
using Backend_Final.Database;
using Backend_Final.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Final.Areas.Client.Controllers
{
    [Area("client")]
    [Route("shoppage")]
    public class ShopPageController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;


        public ShopPageController(DataContext dataContext, IBasketService basketService,
            IUserService userService, IFileService fileService)
        {
            _dataContext = dataContext;
            _basketService = basketService;
            _userService = userService;
            _fileService = fileService;
        }

       

        [HttpGet("index", Name = "client-shoppage-index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Filter", Name = "client-shoppage-filter")]
        public async Task<IActionResult> Filter(string? searchBy = null,
          string? search = null, int? MinPrice = null,
          int? MaxPrice = null, [FromQuery] int? categoryId = null,
          [FromQuery] int? colorId = null, [FromQuery] int? tagId = null)
        {

            return ViewComponent(nameof(ShopPageProduct), new
            {
                searchBy = searchBy,
                search = search,
                MinPrice = MinPrice,
                MaxPrice = MaxPrice,
                categoryId = categoryId,
                colorId = colorId,
                tagId = tagId
            });

        }

    }
}
