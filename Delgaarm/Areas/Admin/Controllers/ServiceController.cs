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
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly INotyfService _notify;

        public ServiceController(IUnitOfWork uow, IMapper mapper, INotyfService noty)
        {
            _context = uow;
            _mapper = mapper;
            _notify = noty;
        }

        public async Task<IActionResult> Index()
        {
            var category = await _context.ServiceUW.GetEntitiesAsync();
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> AddService()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddService(ServiceViewModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (file == null)
            {
                ModelState.AddModelError("ImageUrl", "لطفا یک عکس برای کاربر انتخاب نمایید.");
                return View(model);

            }
            var imagename = "Img/Service/" + FileUpload.CreateImg(file, "Service");
            var data = _mapper.Map<Service>(model);
            data.ImageUrl = imagename;
            data.IsDelete = model.IsDelete;
            await _context.ServiceUW.Create(data);
            await _context.saveAsync();
            _notify.Success("You successfuly added a new Service!", 5);
            return RedirectToAction("Index");
        }


      

        [HttpGet]
        public async Task<IActionResult> EditService(int id)
        {
            var category = await _context.ServiceUW.GetByIdAsync(id);
            var mapCategory = _mapper.Map<ServiceViewModel>(category);
            return View(mapCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditService(ServiceViewModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
                return View(model);
            //update
            if (file != null)
            {
                string imgname = "Img/Service/" + FileUpload.CreateImg(file, "Service");
                bool DeleteImage = FileUpload.DeleteImg("Service", model.ImageUrl);
                model.ImageUrl = imgname;
            }

            var category = await _context.ServiceUW.GetByIdAsync(model.Id);
            var mapModel = _mapper.Map(model, category);
            mapModel.IsDelete = model.IsDelete;
            _context.ServiceUW.Update(mapModel);
            await _context.saveAsync();
            _notify.Success("You successfuly Edited a Service!", 5);
            return RedirectToAction(nameof(Index));

        }

    }
}
