using Application.API.Repository;
using Application.ViewModels;
using AutoMapper;
using Delgraarm.API.Controllers;
using Infrastracture.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Delgaarm.API.Controllers
{
    
    public class AccountController : BaseAPIController
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(ITokenService tokenService, IMapper mapper,
                                  UserManager<ApplicationUser> userManager,
                                  SignInManager<ApplicationUser> signInManager,
                                  RoleManager<IdentityRole> roleManager)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;

        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(UserRegisterDTO registerDTO)
        {
            if (await UserExist(registerDTO.Email))
            {
                return BadRequest("Username already exists!");
            }
            var user = _mapper.Map<ApplicationUser>(registerDTO);

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
                Email = loginDTO.Username,
                Token = await _tokenService.GetToken(user),
            };
        }

        private async Task<bool> UserExist(string username)
        {
            return await _userManager.Users.AnyAsync(u => u.Email == username);
        }
    }
}
