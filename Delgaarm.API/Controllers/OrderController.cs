using Application.Repositories.Interfaces;
using Application.ViewModels.API.Dtos;
using AutoMapper;
using Delgraarm.API.Controllers;
using Infrastracture.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Applicaton.API.Controllers
{
    public class OrderController :  BaseAPIController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _context;
        public OrderController(IMapper mapper,
                                   IUnitOfWork context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> AddContactMessage(OrderDto order)
        {
            var data = _mapper.Map<Order>(order);
            data.OrderStatus = 1;
            await _context.OrderUW.Create(data);
            await _context.saveAsync();
            return Ok(data);
        }

    }
}
