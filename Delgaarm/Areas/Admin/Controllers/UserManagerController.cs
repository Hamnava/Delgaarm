using Application.Repositories.Interfaces;
using Application.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Infrastracture.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Delgaarm.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserManagerController : Controller
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly INotyfService _notify;
        //private readonly IEmailRepository _emailRepository;
        public UserManagerController(IUnitOfWork uow, UserManager<ApplicationUser> userManager, IMapper mapper, INotyfService noty)
        {
            _context = uow;
            _usermanager = userManager;
            _mapper = mapper;
            //_emailRepository = emailRepository;
            _notify = noty;
        }

        //list of user
        public async Task<IActionResult> Index()
        {

            var user = await _context.UsermanagerUW.GetEntitiesAsync();
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(UserViewModel model)
        {

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                IsActive = model.IsActive,
                IsAdmin = model.IsAdmin,
            };
            await _context.UsermanagerUW.Create(user);
            await _context.saveAsync();
            _notify.Success("You successfuly added a new user!", 5);
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Detials(string id)
        {
            var user = await _context.UsermanagerUW.GetByIdAsync(id);
            return View(user);
        }


        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _context.UsermanagerUW.GetByIdAsync(id);
            var mapUser = _mapper.Map<UserViewModel>(user);
            return View(mapUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(UserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            //update
            var user = await _usermanager.FindByIdAsync(model.Id);
            IdentityResult result = await _usermanager.UpdateAsync(_mapper.Map(model, user));
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordByAdminViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _usermanager.FindByIdAsync(model.Id);
            user.PasswordHash = _usermanager.PasswordHasher.HashPassword(user, model.NewPassword);
            var result = await _usermanager.UpdateAsync(user);
            await _context.saveAsync();
            _notify.Information("You  changed the User Password!", 5);
            return RedirectToAction(nameof(Index));
        }

    }

}
