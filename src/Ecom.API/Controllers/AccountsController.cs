using AutoMapper;
using Ecom.API.Errors;
using Ecom.API.Extensions;
using Ecom.Core.Dtos;
using Ecom.Core.Entities;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenServices _tokenServices;
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;

        public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenServices tokenServices, IMapper mapper, DataContext dataContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenServices = tokenServices;
            _mapper = mapper;
            _dataContext = dataContext;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized(new BaseCommonResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (result == null || result.Succeeded == false) return Unauthorized(new BaseCommonResponse(401));
            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenServices.CreateToken(user)
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (CheckEmailExist(registerDto.Email).Result.Value)
            {
                return BadRequest(new ApiValidationErrorResponse
                {
                    Errors = new[] { "This Email is already token" }
                });
            }
            var user = new User
            {
                DisplayName = registerDto.DisplayName,
                UserName = registerDto.Email,
                Email = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded == false) return BadRequest(new BaseCommonResponse(400));
            return Ok(new UserDto
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                Token = _tokenServices.CreateToken(user)
            });

        }

        [Authorize]
        [HttpGet("get-current-user")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            //var user = await _userManager.FindByEmailAsync(email);
            var user = await _userManager.FindEmailByClaimPrincipal(HttpContext.User);

            if (user == null) return Unauthorized(new BaseCommonResponse(401));
            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenServices.CreateToken(user)
            });
        }

        [HttpGet("check-email-exist")]
        public async Task<ActionResult<bool>> CheckEmailExist([FromQuery] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is not null) return true;
            return false;
        }

        [Authorize]
        [HttpGet("get-user-address")]
        public async Task<IActionResult> GetUserAddress()
        {
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            //var user = await _userManager.Users.Include(i => i.Address).SingleOrDefaultAsync(f => f.Email == email);
            var user = await _userManager.FindUserByClaimPrincipalWithAddress(HttpContext.User);

            if (user == null) return Unauthorized(new BaseCommonResponse(401));
            var _resutlt = _mapper.Map<Address, AddressDto>(user.Address);
            return Ok(_resutlt);
        }

        //[Authorize]
        //[HttpPut("update-user-address")]
        //public async Task<IActionResult> UpdateUserAddress(AddressDto addressDto)
        //{
        //    var user = await _userManager.FindUserByClaimPrincipalWithAddress(HttpContext.User);
        //    user.Address = _mapper.Map<AddressDto, Address>(addressDto);
        //    var result = await _userManager.UpdateAsync(user);
        //    if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));

        //    return BadRequest($"Problem while updating this user {HttpContext.User}");
        //}

        [Authorize]
        [HttpPut("update-user-address")]
        public async Task<IActionResult> UpdateUserAddress(AddressDto addressDto)
        {
            var user = await _userManager.FindUserByClaimPrincipalWithAddress(HttpContext.User);

            if (user.Address == null)
            {
                user.Address = _mapper.Map<AddressDto, Address>(addressDto);
            }
            else
            {
                // Update the existing address instance in-place
                _mapper.Map(addressDto, user.Address);
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));

            return BadRequest($"Problem while updating this user {HttpContext.User}");
        }


    }
}
