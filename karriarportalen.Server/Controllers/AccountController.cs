using karriarportalen.Server.Data;
using karriarportalen.Server.DTOs.Account;
using karriarportalen.Server.Interfaces;
using karriarportalen.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace karriarportalen.Server.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class AccountController : ControllerBase
	{
		private readonly AppDbContext _appDbContext;
		private readonly UserManager<AppUser> _userManager;
		private readonly ITokenService _tokenService;
		private readonly SignInManager<AppUser> _signInManager;

		public AccountController(AppDbContext appDbContext, UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
		{
			_appDbContext = appDbContext;
			_userManager = userManager;
			_tokenService = tokenService;
			_signInManager = signInManager;
		}

		[HttpPost("register"), AllowAnonymous]
		public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var existingUser = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == registerDTO.Email);
					if (existingUser != null)
					{
						return BadRequest(new { Status = "Error", Message = "User already exists" });
					}

					var user = new AppUser
					{
						Firstname = registerDTO.Firstname,
						Lastname = registerDTO.Lastname,
						Email = registerDTO.Email,
						UserName = registerDTO.Email
					};

					var result = await _userManager.CreateAsync(user, registerDTO.Password);
					if (result.Succeeded)
					{
						if (registerDTO.UserRole == "JobSeeker")
						{
							var roleResult = await _userManager.AddToRoleAsync(user, "JobSeeker");

							//create cv
							var cv = new CVModel
							{
								AppUserId = user.Id,
								Firstname = user.Firstname,
								Lastname = user.Lastname,
								ShortBio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
							};
							_appDbContext.CVs.Add(cv);
							await _appDbContext.SaveChangesAsync();
						}
						else if (registerDTO.UserRole == "Employer")
						{
							var roleResult = await _userManager.AddToRoleAsync(user, "Employer");
						}

						return Ok(new
						{
							Firstname = user.Firstname,
							Lastname = user.Lastname,
							Email = user.Email,
							PhoneNumber = user.PhoneNumber,
							UserID = user.Id,
							UserRole = await _userManager.GetRolesAsync(user),
							Token = _tokenService.CreateToken(user),
						});

					}
					return BadRequest(result.Errors);
				}
				return BadRequest("Some properties are not valid");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost("login"), AllowAnonymous]
		public async Task<IActionResult> Login(LoginDTO loginDTO)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(loginDTO.Email);
				if (user == null) return Unauthorized("Invalid email or password");

				var result = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, true, false);
				if (result.Succeeded)
				{
					return Ok(new
					{
						Firstname = user.Firstname,
						Lastname = user.Lastname,
						Email = user.Email,
						UserID = user.Id,
						UserRole = await _userManager.GetRolesAsync(user),
						Token = _tokenService.CreateToken(user),

					});
				}
				return Unauthorized(new
				{
					errors = "Invalid email or password"
				});
			}
			return Unauthorized(ModelState);
		}

		[HttpGet("getCurrentUser")]
		public async Task<IActionResult> GetCurrentUser()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = await _userManager.FindByEmailAsync(email);
			if (user != null)
			{
				return Ok(new
				{
					Firstname = user.Firstname,
					Lastname = user.Lastname,
					Email = user.Email,
					UserID = user.Id,
					UserRole = await _userManager.GetRolesAsync(user),
				});
			}
			return Unauthorized("User not found");
		}


		[HttpPost("deleteUser")]
		public async Task<IActionResult> DeleteUser([FromBody] string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user != null)
			{
				var result = await _userManager.DeleteAsync(user);
				if (result.Succeeded)
				{
					return Ok("User deleted");
				}
				return BadRequest(result.Errors);
			}
			return BadRequest("User not found");
		}
	}
}