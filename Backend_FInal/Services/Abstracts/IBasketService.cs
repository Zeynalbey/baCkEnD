using Backend_Final.Areas.Client.ViewModels.Authentication;
using Backend_Final.Areas.Client.ViewModels.Basket;
using Backend_Final.Database.Models;

namespace Backend_Final.Services.Abstracts
{
    public interface IBasketService
    {
        Task<List<ProductCookieViewModel>> AddBasketProductAsync(Product Product);
    }
}
