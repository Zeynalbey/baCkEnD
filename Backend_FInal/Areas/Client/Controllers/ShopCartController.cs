//using Backend_Final.Areas.Client.ViewModels.ShopCart;
//using Backend_Final.Contracts.File;
//using Backend_Final.Database;
//using Backend_Final.Services.Abstracts;
//using Backend_Final.Services.Concretes;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Linq;

//namespace Backend_Final.Areas.Client.Controllers
//{
//    [Area("client")]
//    [Route("shopping-cart")]
//    public class ShopCartController : Controller
//    {
//        private readonly DataContext _dataContext;
//        private readonly IUserService _userService;
//        private readonly IFileService _fileService;

//        public ShopCartController
//            (DataContext dataContext,
//            IUserService userService,
//            IFileService fileService)
//        {
//            _dataContext = dataContext;
//            _userService = userService;
//            _fileService = fileService;
//        }

//        [HttpGet("list", Name = "client-shopping-cart-list")]
//        public async Task<IActionResult> List()
//        {
//            ListViewModel model;

//            if (_userService.IsAuthenticated)
//            {
//                model = new ListViewModel
//                {
//                    Products = await _dataContext.BasketProducts
//                    .Where(bp => bp.Basket!.UserId == _userService.CurrentUser.Id)
//                    .Select(bp => new ListViewModel.ItemViewModel
//                    {
//                        Id = bp.Id,
//                        ImageUrl = bp.Product!.ProductImages!.Take(1)!.FirstOrDefault()! != null
//                            ? _fileService.GetFileUrl(bp.Product.ProductImages!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.Product)
//                            : string.Empty,
//                        Price = bp.Product.Price,
//                        ProductName = bp.Product.Title,
//                        Quantity = bp.Quantity,
//                        Total = bp.Product.Price * bp.Quantity,
//                    }).ToListAsync(),
//                    Summary = new ListViewModel.SummaryViewModel
//                    {
//                        Total = await _dataContext.BasketProducts
//                            .Where(bp => bp.Basket.UserId == _userService.CurrentUser.Id)
//                            .SumAsync(bp => bp.Product!.Price * bp.Quantity)
//                    }
//                };
//            }
//            else
//            {
//                //Read from cookie 

//                //Mock data
//                model = new ListViewModel
//                {
//                    Products = new List<ListViewModel.ItemViewModel>(),
//                    Summary = new ListViewModel.SummaryViewModel()
//                };

//            }

//            return View(model);
//        }

//    }
//}
