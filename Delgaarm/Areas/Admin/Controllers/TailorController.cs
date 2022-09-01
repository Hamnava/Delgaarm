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
    public class TailorController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly INotyfService _notify;
        public TailorController(IUnitOfWork context, IMapper mapper, INotyfService notyf)
        {
            _context = context;
            _mapper = mapper;
            _notify = notyf;

        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.TialorSizeUW.GetEntitiesAsync(null, null, "User"));
        }


        [HttpGet]
        public async Task<IActionResult> AddSize()
        {
            ViewBag.UserId = new SelectList(await _context.UsermanagerUW.GetEntitiesAsync(), "Id", "User");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSize(TailorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var data = _mapper.Map<TailorSize>(model);
          
            await _context.TialorSizeUW.Create(data);
            await _context.saveAsync();
            _notify.Success("You Added a new TailorSize", 5);
            return RedirectToAction(nameof(Index));

        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.UserId = new SelectList(await _context.UsermanagerUW.GetEntitiesAsync(), "Id", "User");
           
            TailorSize size = await _context.TialorSizeUW.GetByIdAsync(id);
            var data = _mapper.Map<TailorViewModel>(size);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserId = new SelectList(await _context.UsermanagerUW.GetEntitiesAsync(), "Id", "User");
                return View(model);
            }

            var ser = await _context.TialorSizeUW.GetByIdAsync(model.Id);
            var data = _mapper.Map(model, ser);
           
            _context.TialorSizeUW.Update(data);
            await _context.saveAsync();
            _notify.Success("You updated  the Size !", 5);
            return RedirectToAction(nameof(Index));

        }


    }
}
