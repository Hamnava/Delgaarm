using Application.Classes;
using Application.Repositories.Interfaces;
using Application.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Infrastracture.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delgaarm.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly INotyfService _notify;
        
        public CategoryController(IUnitOfWork uow, IMapper mapper, INotyfService noty)
        {
            _context = uow;
            _mapper = mapper;
            _notify = noty;
        }

        public async Task<IActionResult> Index()
        {
            var category = await _context.CategoryUW.GetEntitiesAsync();
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> AddCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(CategoryViewModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (file == null)
            {
                ModelState.AddModelError("CategoryPhoto", "لطفا یک عکس برای کاربر انتخاب نمایید.");
                return View(model);

            }
            var imagename = "Img/Category/" + FileUpload.CreateImg(file, "Category");
            var data = _mapper.Map<Category>(model);
            data.CategoryPhoto = imagename;
            data.IsDelete = model.IsDelete;
            await _context.CategoryUW.Create(data);
            await _context.saveAsync();
            _notify.Success("You successfuly added a new Category!", 5);
                return RedirectToAction("Index");
        }


     


        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _context.CategoryUW.GetByIdAsync(id);
            var mapCategory = _mapper.Map<EditCategoryViewModel>(category);
            return View(mapCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(EditCategoryViewModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
                return View(model);
            //update
            if (file != null)
            {
                string imgname = "Img/Category/" + FileUpload.CreateImg(file, "Category");
                bool DeleteImage = FileUpload.DeleteImg("Category", model.CategoryPhoto);
                model.CategoryPhoto = imgname;
            }

            var category = await _context.CategoryUW.GetByIdAsync(model.Id);
            var mapModel = _mapper.Map(model, category);
            mapModel.IsDelete = model.IsDelete;
            _context.CategoryUW.Update(mapModel);
            await _context.saveAsync();
            _notify.Success("You successfuly Edited a Category!", 5);
            return RedirectToAction(nameof(Index));
           
        }

    }
}
