using Application.Repositories.Interfaces;
using Application.ViewModels.API.Dtos;
using AutoMapper;
using Delgraarm.API.Controllers;
using Infrastracture.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Application.API.Controllers
{
    public class ContactController : BaseAPIController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _context;
        public ContactController(IMapper mapper,
                                   IUnitOfWork context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<ContactDto>> AddContactMessage(ContactDto contact)
        {
            var data = _mapper.Map<ContactUs>(contact);
            await _context.ContactUsUW.Create(data);
            await _context.saveAsync();
            return Ok(data);
        }

        [HttpPost("subscribe")]
        public async Task<ActionResult<SubscribeDto>> Subsbrice(SubscribeDto subscribe)
        {
            var data = _mapper.Map<EmailRegister>(subscribe);
            await _context.Subscribe.Create(data);
            await _context.saveAsync();
            return Ok(data);
        }
    }
}
