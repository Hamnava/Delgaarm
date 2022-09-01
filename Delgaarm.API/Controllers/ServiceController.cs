using Application.Repositories.Interfaces;
using Application.ViewModels.API.Dtos;
using AutoMapper;
using Delgraarm.API.Controllers;
using Infrastracture.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Application.API.Controllers
{
    public class ServiceController : BaseAPIController
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _context;
        public ServiceController(IMapper mapper,
                                   IUnitOfWork context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceDTo>> GetService()
        {
            var services = await _context.ServiceUW.GetEntitiesAsync();
            var data = _mapper
               .Map<IEnumerable<Service>, IEnumerable<ServiceDTo>>(services);

            return Ok(data);
        }

    }
}
