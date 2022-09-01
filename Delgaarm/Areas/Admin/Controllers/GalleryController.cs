using Application.Classes;
using Application.Repositories.Interfaces;
using Application.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Infrastracture.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Delgaarm.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class GalleryController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly INotyfService _notify;
        public GalleryController(IUnitOfWork context, IMapper mapper, INotyfService notyf)
        {
            _context = context;
            _mapper = mapper;
            _notify = notyf;

        }
        public async Task<IActionResult> Index()
        {
            var product = await _context.productUW.GetEntitiesAsync(null, null, "Categories,productColor");
          
            return View(product);

        }

        public async Task<IActionResult> AddPhoto(int id)
        {
            var product = await _context.productUW.GetByIdAsync(id);
            ViewBag.ProductId = id;
            ViewBag.Photo = product.ProductImage;
            ViewBag.Alt = product.Alt;
            ViewBag.title = product.Title;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddPhoto(IFormFile file, ProductGalleryViewModel gallery)
        {


            if (file != null)
            {
                string imgname = "Img/Gallery/" + FileUpload.CreateImg(file, "Gallery");
                if (imgname == "false")
                {
                    return View(gallery);
                }
                var photo = new ProductGallery
                {
                    ImageUrl = imgname,
                    ProductId = gallery.ProductId,
                    Alt = gallery.Alt,
                    Title = gallery.Title,
                };

                await _context.productGalleryUW.Create(photo);
                await _context.saveAsync();
                _notify.Success("You add a photo for product Gallery  !", 5);
                return RedirectToAction(nameof(AddPhoto));
            }
            return View(gallery);
        }


        public async Task<IActionResult> Delete(int id, string url, int productId)
        {
            var pic = await _context.productGalleryUW.GetByIdAsync(id);
            FileUpload.DeleteImg("Product", pic.ImageUrl);
            await _context.productGalleryUW.DeleteByIdAsync(id);
            await _context.saveAsync();
            ViewBag.ProductId = productId;
            ViewBag.Photo = url;
            _notify.Error("You deleted a photo from product Gallery  !", 5);
            return RedirectToAction(nameof(Index));
        }



    }
}
