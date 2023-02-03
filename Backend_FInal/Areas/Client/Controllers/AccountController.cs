using Backend_Final.Areas.Client.ViewModels.Authentication;
using Backend_Final.Areas.Client.ViewModels.OrderProducts;
using Backend_Final.Database;
using Backend_Final.Database.Models;
using Backend_Final.Services.Abstracts;
using Backend_Final.Services.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final.Areas.Client.Controllers
{
    [Area("client")]
    [Route("account")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly INotificationService _notificationService;

        public AccountController(
            DataContext dataContext,
            IUserService userService,
            IOrderService orderService,
            INotificationService notificationService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _orderService = orderService;
            _notificationService = notificationService;
        }

        [HttpGet("dashboard", Name = "client-account-dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }


        [HttpGet("orders", Name = "client-account-orders")]
        public async Task<IActionResult> Order()
        {
            var model = new OrdersProductsViewModel
            {
                Products = await _dataContext.OrderProducts
                  .Select(p => new OrdersProductsViewModel.ItemViewModel
                  {
                      Name = p.Product!.Name,
                      Price = p.Product.Price,
                      Quantity = p.Quantity,
                      Total = p.Product.Price * p.Quantity,
                  }).ToListAsync(),

                Summary = new OrdersProductsViewModel.SummaryViewModel
                {
                    Total = await _dataContext.OrderProducts
                    .SumAsync(bp => bp.Product!.Price * bp.Quantity)
                }
            };

            return View(model);
        }

    }
}
