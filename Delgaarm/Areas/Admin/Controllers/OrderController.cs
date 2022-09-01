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
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly INotyfService _notify;
        public OrderController(IUnitOfWork context, IMapper mapper, INotyfService notyf)
        {
            _context = context;
            _mapper = mapper;
            _notify = notyf;

        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderUW.GetEntitiesAsync(null, x=> x.OrderByDescending(x=> x.Id), "User,Product"));
        }


        [HttpGet]
        public async Task<IActionResult> AddOrder()
        {
            ViewBag.UserId = new SelectList(await _context.UsermanagerUW.GetEntitiesAsync(), "Id", "FullName");
            ViewBag.ProductId = new SelectList(await _context.productUW.GetEntitiesAsync(), "Id", "ProductName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserId = new SelectList(await _context.UsermanagerUW.GetEntitiesAsync(), "Id", "FullName");
                ViewBag.ProductId = new SelectList(await _context.productUW.GetEntitiesAsync(), "Id", "ProductName");

                return View(model);
            }
            var data = _mapper.Map<Order>(model);
        
            await _context.OrderUW.Create(data);
            await _context.saveAsync();
            _notify.Success("You Added a new Order", 5);
            return RedirectToAction(nameof(Index));

        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.UserId = new SelectList(await _context.UsermanagerUW.GetEntitiesAsync(), "Id", "FullName");
            ViewBag.ProductId = new SelectList(await _context.productUW.GetEntitiesAsync(), "Id", "ProductName");

            Order order = await _context.OrderUW.GetByIdAsync(id);
            var data = _mapper.Map<OrderViewModel>(order);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserId = new SelectList(await _context.UsermanagerUW.GetEntitiesAsync(), "Id", "FullName");
                ViewBag.ProductId = new SelectList(await _context.productUW.GetEntitiesAsync(), "Id", "ProductName");
                return View(model);
            }

            var ser = await _context.OrderUW.GetByIdAsync(model.Id);
            var data = _mapper.Map(model, ser);
           
            _context.OrderUW.Update(data);
            await _context.saveAsync();
            _notify.Success("You updated  the Order !", 5);
            return RedirectToAction(nameof(Index));

        }






        public async Task<IActionResult> Detials(int id)
        {
            var product = await _context.OrderUW.GetByIdAsync(id);
            _notify.Information("You checked all the information of product page !", 5);
            return View(product);
        }


    }
}
