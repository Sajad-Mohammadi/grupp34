using karriarportalen.Server.Data;
using karriarportalen.Server.DTOs.CV;
using karriarportalen.Server.Helpers;
using karriarportalen.Server.Interfaces;
using karriarportalen.Server.Mappers;
using karriarportalen.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace karriarportalen.Server.Controllers.CV
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CVController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICVRepository _cVRepository;

        public CVController(AppDbContext appDbContext, UserManager<AppUser> userManager, ICVRepository cVRepository)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _cVRepository = cVRepository;
        }



        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAllCVs([FromQuery] QueryObject queryObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
			}
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var cvs = await _cVRepository.GetAllAsync(queryObject, userId);
            var cvDTOs = cvs.Select(c => c.ToCVDTO()).ToList();
            return Ok(cvDTOs);
        }



        [HttpGet]
		[Route("{cvId}")]
		public async Task<IActionResult> GetCVById(string cvId)
        {
            var cv = await _cVRepository.GetByIdAsync(cvId);
            if (cv == null)
            {
                return NotFound();
            }
            return Ok(cv.ToCVDTO());
        }



        [HttpPut("update")]
        public async Task<IActionResult> UpdateCV([FromBody] UpdateCVDTO updateCVDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cv = await _cVRepository.UpdateAsync(userId, updateCVDTO);
            if (cv == null)
            {
                return NotFound("CV not found");
            }
            return Ok(cv.ToCVDTO());
        }
    }
}
