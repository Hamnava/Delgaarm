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
    public class SliderController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly INotyfService _notify;

        public SliderController(IUnitOfWork uow, IMapper mapper, INotyfService noty)
        {
            _context = uow;
            _mapper = mapper;
            _notify = noty;
        }

        public async Task<IActionResult> Index()
        {
            var slider = await _context.SliderUW.GetEntitiesAsync();
            return View(slider);
        }

        [HttpGet]
        public async Task<IActionResult> AddSlider()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSlider(SliderViewModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (file == null)
            {
                ModelState.AddModelError("sliderImage", "لطفا یک عکس برای کاربر انتخاب نمایید.");
                return View(model);

            }
            var imagename = "Img/Slider/" + FileUpload.CreateImg(file, "Slider");
            var data = _mapper.Map<Slider>(model);
            data.sliderImage = imagename;
            data.IsDelete = model.IsDelete;
            await _context.SliderUW.Create(data);
            await _context.saveAsync();
            _notify.Success("You successfuly added a new Slider!", 5);
            return RedirectToAction("Index");
        }


      


        [HttpGet]
        public async Task<IActionResult> EditSlider(int id)
        {
            var slider = await _context.SliderUW.GetByIdAsync(id);
            var data = _mapper.Map<SliderViewModel>(slider);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSlider(SliderViewModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
                return View(model);
            //update
            if (file != null)
            {
                string imgname = "Img/Slider/" + FileUpload.CreateImg(file, "Slider");
                bool DeleteImage = FileUpload.DeleteImg("Slider", model.sliderImage);
                model.sliderImage = imgname;
            }

            var slider = await _context.SliderUW.GetByIdAsync(model.SliderId);
            var mapModel = _mapper.Map(model, slider);
            mapModel.IsDelete = model.IsDelete;
            _context.SliderUW.Update(mapModel);
            await _context.saveAsync();
            _notify.Success("You successfuly Edited a Slider!", 5);
            return RedirectToAction(nameof(Index));

        }

    }
}
