using Application.API.Repository;
using Application.Classes;
using Application.Repositories.Interface;
using Application.Repositories.Interfaces;
using Application.ViewModels;
using Application.ViewModels.API.Dtos;
using AutoMapper;
using Delgaarm.API.Helpers;
using Delgraarm.API.Controllers;
using Infrastracture.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Delgaarm.API.Controllers
{
    
    public class AccountController : BaseAPIController
    {
        private readonly ITokenService _tokenService;
        private readonly UserInterface _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(ITokenService tokenService, IMapper mapper,
                                  UserManager<ApplicationUser> userManager,
                                  SignInManager<ApplicationUser> signInManager,
                                  RoleManager<IdentityRole> roleManager,
                                  UserInterface context,
                                  IUnitOfWork unit)
        {
            _tokenService = tokenService;
            _context = context;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _unit = unit;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(UserRegisterDTO registerDTO)
        {
            if (await UserExist(registerDTO.Email))
            {
                return BadRequest("Username already exists!");
            }
            var user = _mapper.Map<ApplicationUser>(registerDTO);
            user.UserName = registerDTO.Email;
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            var role = _roleManager.FindByNameAsync("Customer");
            if(role.Result == null)
            {
                var customer = new IdentityRole
                {
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                };
                await _roleManager.CreateAsync(customer);
            }
            
            
            var roleResult = await _userManager.AddToRoleAsync(user, "Customer");
            if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

            return new UserDTO
            {
                Id = user.Id,
                FullName = registerDTO.FullName,
                Email = registerDTO.Email,
                Token = await _tokenService.GetToken(user),
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == loginDTO.Username);

            if (user == null) return Unauthorized("Invalid Username!");
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if (!result.Succeeded) return Unauthorized();

            return new UserDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = loginDTO.Username,
                Token = await _tokenService.GetToken(user),
            };
        }

        [HttpPut("update-userinfo")]
        public async Task<ActionResult> fooo(EditUserDto dto)
        {
            var user = await _context.GetUserByUsernameAsync(User.GetUsername());

           var mapper = _mapper.Map(dto,user);
            _context.UpdateUser(mapper);

            if (await _context.SaveAllAsync()) return Ok();

            return BadRequest("Faild to Update user!");
        }
        [HttpPut("update-profile")]
        public async Task<ActionResult> UpdateMember(IFormFile file = null)
        {
            //var user = await _userManager.FindByIdAsync(model.Id);
            var user = await _context.GetUserByUsernameAsync(User.GetUsername());
            //update
            if (file != null)
            {
                string imgname = "Img/Profile/" + FileUpload.CreateImg(file, "Profile");
                bool DeleteImage = FileUpload.DeleteImg("Profile", user.Profile);
                user.Profile = imgname;
            }

            _context.UpdateUser(user);

            if (await _context.SaveAllAsync()) return Ok();

            return BadRequest("Faild to Update user!");
        }


        [HttpGet("Profile/{id}")]
        public async Task<ActionResult> Profile(string id)
        {
            var userId = User.FindFirstValue(id);
            var user = await _userManager.FindByIdAsync(userId);

            var data = _mapper.Map<UserViewModel>(user);
            return Ok(data);
        }

        private async Task<bool> UserExist(string username)
        {
            return await _userManager.Users.AnyAsync(u => u.UserName == username);
        }

        [HttpGet("Size/{id}")]
        public async Task<ActionResult> Size(string id)
        {
            var data = await _unit.TialorSizeUW.GetByIdAsync(id);

            return Ok(data);
        }


        [HttpPost]
        public async Task<IActionResult> AddSize(TailorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Your data is not validated");
            }

            var data = _mapper.Map<TailorSize>(model);

            await _unit.TialorSizeUW.Create(data);
            await _unit.saveAsync();

            return Ok();

        }


    }
}
