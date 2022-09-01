using Application.Repositories.Interfaces;
using Application.ViewModels.API.Dtos;
using AutoMapper;
using Delgraarm.API.Controllers;
using Infrastracture.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Application.API.Controllers
{
    public class SliderController : BaseAPIController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _context;
        public SliderController(IMapper mapper,
                                   IUnitOfWork context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<SliderDto>> GetSliders()
        {
            var sliders = await _context.SliderUW.GetEntitiesAsync();
            var data = _mapper
                .Map<IEnumerable<Slider>, IEnumerable<SliderDto>>(sliders);

            return Ok(data);
        }
    }
}
