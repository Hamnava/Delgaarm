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
    public class DesignController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly INotyfService _notify;
        public DesignController(IUnitOfWork context, IMapper mapper, INotyfService notyf)
        {
            _context = context;
            _mapper = mapper;
            _notify = notyf;

        }



        //list of user
        public async Task<IActionResult> Index()
        {
            var design = await _context.YourDesignUW.GetEntitiesAsync( null,x=> x.OrderByDescending(x=> x.Id), "User");
            return View(design);
        }


        public async Task<IActionResult> UserDesign(int id)
        {
            var user = await _context.UsermanagerUW.GetByIdAsync(id);
            ViewBag.UserId = id;
            ViewBag.Photo = user.Profile;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(YourDesignViewModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserId = new SelectList(await _context.UsermanagerUW.GetEntitiesAsync(), "Id", "User");
                return View(model);
            }

            if (file == null)
            {
                ModelState.AddModelError("Image", "Please choose an image for Design.");
                return View(model);

            }

            var imagename = "Img/YourDesign/" + FileUpload.CreateImg(file, "YourDesign");
            var data = _mapper.Map<YourDesign>(model);
            data.Images = imagename;
           
            await _context.YourDesignUW.Create(data);
            await _context.saveAsync();
            _notify.Success("You Added a new product", 5);
            return RedirectToAction(nameof(Index));

        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.UserId = new SelectList(await _context.UsermanagerUW.GetEntitiesAsync(), "Id", "User");

            Product product = await _context.productUW.GetByIdAsync(id);
            var data = _mapper.Map<ProductViewModel>(product);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(YourDesignViewModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserId = new SelectList(await _context.UsermanagerUW.GetEntitiesAsync(), "Id", "User");
                return View(model);
            }

            if (file != null)
            {
                var imagename = "Img/YourDesign/" + FileUpload.CreateImg(file, "YourDesign");
                bool DeleteImage = FileUpload.DeleteImg("Images", model.Images);
                model.Images = imagename;
            }


            var ser = await _context.YourDesignUW.GetByIdAsync(model.Id);
            var data = _mapper.Map(model, ser);
           
            _context.YourDesignUW.Update(data);
            await _context.saveAsync();
            _notify.Success("You updated  the Design !", 5);
            return RedirectToAction(nameof(Index));

        }






        public async Task<IActionResult> Detials(int id)
        {
            var product = await _context.productUW.GetByIdAsync(id);
            _notify.Information("You checked all the information of product page !", 5);
            return View(product);
        }


    }
}
