//using Backend_Final.Areas.Client.ViewModels.Basket;
//using Backend_Final.Database;
//using Backend_Final.Extensions;
//using Backend_Final.Services.Abstracts;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Text.Json;

//namespace Backend_Final.Areas.Client.ViewComponents
//{
//    public class ShopCart : ViewComponent
//    {
//        private readonly DataContext _dataContext;
//        private readonly IUserService _userService;

//        public ShopCart(DataContext dataContext, IUserService userService)
//        {
//            _dataContext = dataContext;
//            _userService = userService;
//        }

//        public async Task<IViewComponentResult> InvokeAsync(List<ProductCookieViewModel>? viewModels = null)
//        {
//            // Case 1 : Qeydiyyat kecilib, o zaman bazadan gotur
//            if (_userService.IsAuthenticated)
//            {
//                var model = await _dataContext.BasketProducts
//                    .Where(bp => bp.Basket.UserId == _userService.CurrentUser.Id)
//                    .Select(bp =>
//                        new ProductCookieViewModel(
//                            bp.ProductId,
//                            bp.Product!.Title,
//                            string.Empty,
//                            bp.Quantity,
//                            bp.Product.Price,
//                            bp.Product.Price * bp.Quantity))
//                    .ToListAsync();

//                return View(model);
//            }

//            //Case 2: Argument olaraq actiondan gonderilib
//            if (viewModels is not null)
//            {
//                return View(viewModels);
//            }

//            //Case 3: Argument gonderilmeyib bu zaman cookiden oxu
//            var productsCookieValue = HttpContext.Request.Cookies["products"];
//            var productsCookieViewModel = new List<ProductCookieViewModel>();
//            if (productsCookieValue is not null)
//            {
//                productsCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productsCookieValue);
//            }

//            return View(productsCookieViewModel);
//        }
//    }
//}
