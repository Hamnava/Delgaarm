using Application.Repositories.Interfaces;
using Application.Classes;
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
        public async Task<IActionResult> AddUser(UserViewModel model, IFormFile file)
        {

            if (file == null)
            {
                ModelState.AddModelError("Profile", "لطفا یک عکس برای کاربر انتخاب نمایید.");
                return View(model);

            }
            var imagename = "Img/Profile/" + FileUpload.CreateImg(file, "Profile");
            var data = _mapper.Map<ApplicationUser>(model);
            data.Profile = imagename;
            data.IsAdmin = model.IsAdmin;
            data.IsActive = model.IsActive;
            IdentityResult result = await _usermanager.CreateAsync(data, "123@d_F");

            if (result.Succeeded)
            {

                _notify.Success("You successfuly added a new user!", 5);
                return RedirectToAction("Index");
            }
            return View(model);

        }


        public async Task<IActionResult> Details(string id)
        {
            var user = await _context.UsermanagerUW.GetByIdAsync(id);
            return View(user);
        }


        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _context.UsermanagerUW.GetByIdAsync(id);
            var mapUser = _mapper.Map<EditUserViewModel>(user);
            return View(mapUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
                return View(model);
            //update
            if(file != null)
            {
                string imgname = "Img/Profile/" + FileUpload.CreateImg(file, "Profile");
                bool DeleteImage = FileUpload.DeleteImg("Product", model.Profile);
                model.Profile = imgname;
            }
           
            var user = await _context.UsermanagerUW.GetByIdAsync(model.Id);
           user.IsAdmin = model.IsAdmin;
            user.IsDelete = model.IsDelete;
            user.IsActive = user.IsActive;
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



        // For Active or DeActive User
        [HttpGet]
        public async Task<IActionResult> ActiveDeActiveUser(string userId, bool IsActive)
        {
            if (userId == null)
            {
                return RedirectToAction("ErrorView", "Home");
            }
            var user = await _context.UsermanagerUW.GetByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("ErrorView", "Home");
            }

            if (user.IsActive)
            {
                // Should DeActivate
                ViewBag.theme = "darkred";
                ViewBag.ViewTitle = "غیرفعال کردن کاربر";
                return PartialView("_ActiveOrDeActiveUser", user);

            }
            else
            {
                // Should Activate
                ViewBag.theme = "darkgreen";
                ViewBag.ViewTitle = "فعال کردن کاربر";
                return PartialView("_ActiveOrDeActiveUser", user);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActiveDeActiveUserPost(string Id, bool IsActive)
        {
            if (Id == null)
            {
                return RedirectToAction("ErrorView", "Home");
            }
            else
            {
                try
                {
                    if (IsActive)
                    {
                        // DeActivate
                        var user = await _context.UsermanagerUW.GetByIdAsync(Id);
                        user.IsActive = false;
                        _context.UsermanagerUW.Update(user);
                        await _context.saveAsync();
                    }
                    else
                    {
                        // Activate
                        var user =await _context.UsermanagerUW.GetByIdAsync(Id);
                        user.IsActive = true;
                        _context.UsermanagerUW.Update(user);
                        await _context.saveAsync();
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return RedirectToAction("ErrorView", "Home");
                }
            }
        }

        [HttpGet]
        public IActionResult ChangePasswordbyAdmin(string userId, string FullName)
        {
            if (userId == null)
            {
                return RedirectToAction("ErrorView", "Home");
            }
            ViewBag.userId = userId;
            ViewBag.FullName = FullName;
            return PartialView("_ChangePasswordbyAdmin");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassByAdmin(ChangePasswordByAdminViewModel model)
        {
            try
            {
                var user = await _usermanager.FindByIdAsync(model.Id);
                user.PasswordHash = _usermanager.PasswordHasher.HashPassword(user, model.NewPassword);
                var result = await _usermanager.UpdateAsync(user);
                await _context.saveAsync();
                return Json(new { status = "ok" });
            }
            catch
            {
                return Json(new { status = "error" });
            }
        }

    }
}
