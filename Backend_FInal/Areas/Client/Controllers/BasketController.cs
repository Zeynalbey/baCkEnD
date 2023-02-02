using Backend_Final.Areas.Client.ViewComponents;
using Backend_Final.Areas.Client.ViewModels.Basket;
using Backend_Final.Database;
using Backend_Final.Database.Models;
using Backend_Final.Services.Abstracts;
using Backend_Final.Services.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Xml;

namespace Backend_Final.Areas.Client.Controllers
{
    [Area("client")]
    [Route("basket")]
    public class BasketController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;

        public BasketController(DataContext dataContext, IBasketService basketService, IUserService userService)
        {
            _dataContext = dataContext;
            _basketService = basketService;
            _userService = userService;
        }

        [HttpGet("add/{id}", Name = "client-basket-add")]
        public async Task<IActionResult> AddProductAsync([FromRoute] int id)
        {
            var product = await _dataContext.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (product is null)
            {
                return NotFound();
            }

            var productsCookieViewModel = await _basketService.AddBasketProductAsync(product);
            if (productsCookieViewModel.Any())
            {
                return ViewComponent(nameof(BasketMini), productsCookieViewModel);
            }

            return ViewComponent(nameof(BasketMini));
        }

        [HttpGet("delete/{id}", Name = "client-basket-delete")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] int id)
        {
            var productsCookieViewModel = new List<ProductCookieViewModel>();

            if (_userService.IsAuthenticated)
            {
                var basketProduct = await _dataContext.BasketProducts
                   .Include(b => b.Basket).FirstOrDefaultAsync(bp => bp.Basket!.UserId == _userService.CurrentUser.Id && bp.ProductId == id);

                if (basketProduct is null)
                {
                    return NotFound();
                }
                _dataContext.BasketProducts.Remove(basketProduct);
                await _dataContext.SaveChangesAsync();
            }
            else
            {
                var product = await _dataContext.Products.FirstOrDefaultAsync(b => b.Id == id);
                if (product is null)
                {
                    return NotFound();
                }

                var productCookieValue = HttpContext.Request.Cookies["products"];
                if (productCookieValue is null)
                {
                    return NotFound();
                }

                productsCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productCookieValue);
                productsCookieViewModel!.RemoveAll(pcvm => pcvm.Id == id);

                HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productsCookieViewModel));



            }
            return ViewComponent(nameof(BasketMini), productsCookieViewModel);

        }
    }
}
