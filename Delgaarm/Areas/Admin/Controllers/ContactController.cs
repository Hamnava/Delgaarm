using Application.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delgaarm.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IUnitOfWork _context;
       

        public ContactController(IUnitOfWork uow)
        {
            _context = uow;
        }

        public async Task<IActionResult> Index()
        {
            var category = await _context.ContactUsUW.GetEntitiesAsync(null, x=> x.OrderByDescending(x=> x.Id));
            return View(category);
        }


        public async Task<IActionResult> Details(int id)
        {
            var category = await _context.ContactUsUW.GetByIdAsync(id);
            return View(category);
        }

    }
}
