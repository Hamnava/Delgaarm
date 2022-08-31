using Application.Repositories.Interfaces;
using Application.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Infrastracture.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Delgaarm.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly INotyfService _notify;

        public ColorController(IUnitOfWork uow, IMapper mapper, INotyfService noty)
        {
            _context = uow;
            _mapper = mapper;
            _notify = noty;
        }

        public async Task<IActionResult> Index()
        {
            var color = await _context.ProductColorUW.GetEntitiesAsync();
            return View(color);
        }

        [HttpGet]
        public async Task<IActionResult> AddColor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddColor(ProductColorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
           
            var data = _mapper.Map<ProductColor>(model);
            await _context.ProductColorUW.Create(data);
            await _context.saveAsync();
            _notify.Success("You successfuly added a new Color!", 5);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Details(int id)
        {
            var color = await _context.ProductColorUW.GetByIdAsync(id);
            return View(color);
        }


        [HttpGet]
        public async Task<IActionResult> EditColor(int id)
        {
            var color = await _context.ProductColorUW.GetByIdAsync(id);
            var data = _mapper.Map<EditColorViewModel>(color);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditColor(EditColorViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
           

            var color = await _context.ProductColorUW.GetByIdAsync(model.Id);
            var mapModel = _mapper.Map(model, color);
            _context.ProductColorUW.Update(mapModel);
            await _context.saveAsync();
            _notify.Success("You successfuly Edited a Color!", 5);
            return RedirectToAction(nameof(Index));

        }

    }
}
