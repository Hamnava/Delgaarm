using Application.Classes;
using Application.Repositories.Interfaces;
using Application.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Infrastracture.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Delgaarm.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly INotyfService _notify;
        public ProductController(IUnitOfWork context, IMapper mapper, INotyfService notyf)
        {
            _context = context;
            _mapper = mapper;
            _notify = notyf;

        }


        public async Task<IActionResult> ShowProduct()
        {
            return View(await _context.productUW.GetEntitiesAsync(null,null, "Categories,productColor"));
        }


        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            ViewBag.CategoryId = new SelectList(await _context.CategoryUW.GetEntitiesAsync(), "Id", "CategoryName");
            ViewBag.ColorId = new SelectList(await _context.ProductColorUW.GetEntitiesAsync(), "Id", "ColorName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (file == null)
            {
                ModelState.AddModelError("ProductImage", "Please choose an image for product.");
                return View(model);

            }

            var imagename = "Img/Product/" + FileUpload.CreateImg(file, "Product");
            var data = _mapper.Map<Product>(model);
            data.ProductImage = imagename;
            data.IsPrefered = model.IsPrefered;
            data.IsBestSeller = model.IsBestSeller;
            data.IsActive = true;
            await _context.productUW.Create(data);
            await _context.saveAsync();
            _notify.Success("You Added a new product", 5);
            return RedirectToAction(nameof(ShowProduct));

        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.CategoryId = new SelectList(await _context.CategoryUW.GetEntitiesAsync(), "Id", "CategoryName");
            ViewBag.ColorId = new SelectList(await _context.ProductColorUW.GetEntitiesAsync(), "Id", "ColorName");

            Product product = await _context.productUW.GetByIdAsync(id);
            var data = _mapper.Map<ProductViewModel>(product);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId = new SelectList(await _context.CategoryUW.GetEntitiesAsync(), "Id", "CategoryName");
                ViewBag.ColorId = new SelectList(await _context.ProductColorUW.GetEntitiesAsync(), "Id", "ColorName");
                return View(model);
            }

            if (file != null)
            {
                string imgname = "Img/Product/" + FileUpload.CreateImg(file, "Product");
                bool DeleteImage = FileUpload.DeleteImg("Product", model.ProductImage);
                model.ProductImage = imgname;
            }


            var ser = await _context.productUW.GetByIdAsync(model.Id);
            var data = _mapper.Map(model, ser);
            data.IsActive = model.IsActive;
            data.IsPrefered = model.IsPrefered;
            data.IsBestSeller = model.IsBestSeller;
            data.IsNew = model.IsNew;
            _context.productUW.Update(data);
            await _context.saveAsync();
            _notify.Success("You updated  the product !", 5);
            return RedirectToAction(nameof(ShowProduct));

        }






        public async Task<IActionResult> Detials(int id)
        {
            var product = await _context.productUW.GetByIdAsync(id);
            _notify.Information("You checked all the information of product page !", 5);
            return View(product);
        }


    }
}
