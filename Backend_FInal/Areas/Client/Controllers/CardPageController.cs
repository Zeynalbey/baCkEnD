using Backend_Final.Areas.Client.ViewComponents;
using Backend_Final.Areas.Client.ViewModels.Basket;
using Backend_Final.Database;
using Backend_Final.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Backend_Final.Areas.Client.Controllers
{
    [Area("client")]
    [Route("CardPage")]
    public class CardPageController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;
        private readonly IBasketService _basketService;

        public CardPageController(DataContext dbContext, IUserService userService, IBasketService basketService)
        {
            _dataContext = dbContext;
            _userService = userService;
            _basketService = basketService;
        }

        #region Index'

        [HttpGet("cardpageindex", Name = "client-card-page-index")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        #endregion

        #region Add'

        [HttpGet("add/{id}", Name = "client-cardpagebasket-add")]
        public async Task<IActionResult> AddProduct([FromRoute] int id)
        {
            var product = await _dataContext.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
            {
                return NotFound();
            }
            var productCookiViewModel = await _basketService.AddBasketProductAsync(product);
            if (productCookiViewModel.Any())
            {
                return ViewComponent(nameof(CardPage), productCookiViewModel);
            }

            return ViewComponent(nameof(CardPage));
        }
        #endregion

        #region Delete'

        [HttpGet("card-page-delete/{productId}", Name = "client-shop-basket-delete")]
        public async Task<IActionResult> DeleteBaketProductAsync([FromRoute] int productId)
        {
            if (_userService.IsAuthenticated)
            {
                var basketProduct = await _dataContext.BasketProducts.Include(p => p.Basket)
                    .FirstOrDefaultAsync(bp => bp.Basket!.UserId == _userService.CurrentUser.Id && bp.ProductId == productId);


                if (basketProduct is null) return NotFound();


                _dataContext.BasketProducts.Remove(basketProduct);
            }
            else
            {

                var product = await _dataContext.Products.FirstOrDefaultAsync(b => b.Id == productId);

                if (product is null) { return NotFound(); }
               

                var productCookieValue = HttpContext.Request.Cookies["products"];

                if (productCookieValue is null) { return NotFound(); }
           

                var productsCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productCookieValue);
                productsCookieViewModel!.RemoveAll(pcvm => pcvm.Id == productId);

                HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productsCookieViewModel));
            }


            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("client-card-page-index");
        }
        #endregion

        #region Individual Delete'

        [HttpGet("basket-individual-delete/{id}", Name = "client-individual-basket-delete")]
        public async Task<IActionResult> DeleteIndividualProduct([FromRoute] int id)
        {

            var productCookieViewModel = new List<ProductCookieViewModel>();
            if (_userService.IsAuthenticated)
            {

                var basketProduct = await _dataContext.BasketProducts
                    .Include(p => p.Basket)
                    .FirstOrDefaultAsync(bp => bp.Basket!.UserId == _userService.CurrentUser.Id && bp.ProductId == id);

                if (basketProduct is null) { return NotFound(); }
           

                if (basketProduct.Quantity > 1)
                {
                    basketProduct.Quantity -= 1;
                }
                else
                {
                    _dataContext.BasketProducts.Remove(basketProduct);
                }
            }
            else
            {
                var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product is null)
                {
                    return NotFound();
                }
                var productCookieValue = HttpContext.Request.Cookies["products"];
                if (productCookieValue is null)
                {
                    return NotFound();
                }

                productCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productCookieValue);

                foreach (var cookieItem in productCookieViewModel)
                {
                    if (cookieItem.Quantity > 1)
                    {
                        cookieItem.Quantity -= 1;
                        cookieItem.Total = cookieItem.Quantity * cookieItem.Price;
                    }
                    else
                    {
                        productCookieViewModel.RemoveAll(p => p.Id == cookieItem.Id);
                        break;
                    }
                }
                HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productCookieViewModel));
            }
            await _dataContext.SaveChangesAsync();
            return ViewComponent(nameof(CardPage), productCookieViewModel);
        }
        #endregion

        #region Update'

        [HttpGet("update", Name = "client-cardpagebasket-update")]
        public async Task<IActionResult> UpdateProduct()
        {
            return ViewComponent(nameof(BasketMini));
        } 
        #endregion
    }
}
