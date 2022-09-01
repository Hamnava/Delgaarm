using Application.Repositories.Interfaces;
using Application.ViewModels.API.Dtos;
using AutoMapper;
using Delgraarm.API.Controllers;
using Infrastracture.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Application.API.Controllers
{
    public class CategoryController : BaseAPIController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _context;
        public CategoryController(IMapper mapper,
                                   IUnitOfWork context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<CategoryDto>> GetCategory()
        {
            var categories = await _context.CategoryUW.GetEntitiesAsync();
            var data = _mapper
               .Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(categories);

            return Ok(data);
        }
    }
}
