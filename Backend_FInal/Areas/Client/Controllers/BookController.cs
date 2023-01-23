using Backend_Final.Areas.Client.ViewModels.Product;
using Backend_Final.Areas.Client.ViewModels.Product.Update;
using Backend_Final.Database;
using Backend_Final.Database.Models;
using Backend_Final.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Backend_Final.Areas.Client.Controllers
{
    [Area("client")]
    [Route("Product")]
    public class ProductController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IEmailService _emailService;

        public ProductController(DataContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }

        #region List

        [HttpGet("list", Name = "Product-list")]
        public ActionResult List()
        {
            var model = _dbContext.Products
                .Select(b => new ListItemViewModel(b.Id, b.Title, b.Price, b.CreatedAt))
                .ToList();

            return View(model);
        }

        [HttpGet("detail/{id}", Name = "Product-details")]
        public ActionResult Details([FromRoute] int id)
        {
            var Product = _dbContext.Products.Where(b => b.Id == id).FirstOrDefault();
            if (Product == null)
            {
                return NotFound();
            }

            var model = new DetailsViewModel(Product.Title, string.Empty, Product.Price, Product.CreatedAt);
            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("add", Name = "Product-add")]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost("add", Name = "Product-add")]
        public ActionResult Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _dbContext.Products.Add(new Product
            {
                Title = model.Title,
                //Author = model.Author,
                Price = model.Price.Value,
            });

            return RedirectToAction(nameof(List));
        }

        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "Product-update")]
        public ActionResult Update([FromRoute] int id)
        {
            var Product = _dbContext.Products.FirstOrDefault(b => b.Id == id);
            if (Product is null)
            {
                return NotFound();
            }

            return View(new UpdateResponseViewModel { Id = Product.Id, Title = Product.Title, /*Author = Product.Author, */Price = Product.Price });
        }

        [HttpPost("update/{id}", Name = "Product-update")]
        public ActionResult Update([FromRoute] int id, [FromForm] UpdateRequestViewModel model)
        {
            var Product = _dbContext.Products.FirstOrDefault(b => b.Id == id);
            if (Product is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Product.Title = model.Title;
            //Product.Author = model.Author;
            Product.Price = model.Price.Value;

            return RedirectToAction(nameof(List));
        }


        #endregion

        #region Delete

        [HttpGet("delete", Name = "Product-delete-bulk")]
        public ActionResult Delete()
        {
            //_dbContext.Products.re

            return RedirectToAction(nameof(List));
        }

        [HttpGet("delete/{id}", Name = "Product-delete-individual")]
        public ActionResult Delete(int id)
        {
            var Product = _dbContext.Products.FirstOrDefault(b => b.Id == id);
            if (Product is null)
            {
                return NotFound();
            }

            _dbContext.Products.Remove(Product);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(List));
        }

        #endregion
    }
}

