using Backend_Final.Areas.Client.ViewModels.OrderProducts;
using Backend_Final.Areas.Client.ViewModels.ShopCart;
using Backend_Final.Contracts.File;
using Backend_Final.Database;
using Backend_Final.Database.Models;
using Backend_Final.Services.Abstracts;
using Backend_Final.Services.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Backend_Final.Areas.Client.Controllers
{
    [Area("client")]
    [Route("checkout")]
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly INotificationService _notificationService;

        public CheckoutController
            (DataContext dataContext,
            IUserService userService,
            IFileService fileService,
            IOrderService orderService,
            INotificationService notificationService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _orderService = orderService;
            _notificationService = notificationService;
        }

        [HttpGet("order-products", Name = "client-checkout-order-products")]
        public async Task<IActionResult> OrderProducts()
        {
            var model = new OrdersProductsViewModel
            {
                Products = await _dataContext.BasketProducts
                    .Where(bp => bp.Basket!.UserId == _userService.CurrentUser.Id)
                    .Select(bp => new OrdersProductsViewModel.ItemViewModel
                    {
                        Name = bp.Product!.Name,
                        Price = bp.Product.Price,
                        Quantity = bp.Quantity,
                        Total = bp.Product.Price * bp.Quantity
                    }).ToListAsync(),
                Summary = new OrdersProductsViewModel.SummaryViewModel
                {
                    Total = await _dataContext.BasketProducts
                        .Where(bp => bp.Basket!.UserId == _userService.CurrentUser.Id)
                        .SumAsync(bp => bp.Product!.Price * bp.Quantity)
                }
            };

            return View(model);
        }

        [HttpPost("place-order", Name = "client-checkout-place-order")]
        public async Task<IActionResult> PlaceOrder()
        {

            var basketProducts = await _dataContext.BasketProducts
                   .Include(bp => bp.Product)
                   .Where(bp => bp.Basket!.UserId == _userService.CurrentUser.Id)
                   .ToListAsync();

            if (basketProducts is null)
            {
                return NotFound();
            }
            var order = new Order
            {
                Id = await _orderService.GenerateUniqueTrackingCodeAsync(),
                UserId = _userService.CurrentUser.Id,
                Status = Database.Models.Enums.OrderStatus.Created,
            };

            await _dataContext.Orders.AddAsync(order);

            foreach (var basketProduct in basketProducts)
            {
                var orderProduct = new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = basketProduct.ProductId,
                    Price = basketProduct.Product!.Price,
                    Quantity = basketProduct.Quantity,
                    Total = basketProduct.Quantity * basketProduct.Product!.Price,
                };

                await _dataContext.AddAsync(orderProduct);
            }

            order.Total = order.OrderProducts!.Sum(op => op.Total);

            _dataContext.RemoveRange(basketProducts);

            await _dataContext.SaveChangesAsync();

            await _notificationService.SenOrderCreatedToAdmin(order.Id);

            return RedirectToRoute("client-account-orders");
        }
    }
}
