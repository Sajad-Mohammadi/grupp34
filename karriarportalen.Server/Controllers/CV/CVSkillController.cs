using karriarportalen.Server.Data;
using karriarportalen.Server.Extensions;
using karriarportalen.Server.Interfaces;
using karriarportalen.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace karriarportalen.Server.Controllers.CV
{
	[Route("[controller]")]
	[ApiController]
	[Authorize]
	public class CVSkillController : ControllerBase
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly ICVRepository _cVRepository;
		private readonly ISkillRepository _skillRepository;
		private readonly ICVSkillRepository _cVSkillRepository;


		public CVSkillController(UserManager<AppUser> userManager, ICVRepository cVRepository, ISkillRepository skillRepository, ICVSkillRepository cVSkillRepository)
		{
			_userManager = userManager;
			_cVRepository = cVRepository;
			_skillRepository = skillRepository;
			_cVSkillRepository = cVSkillRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetCVSkills(string cvId)
		{
			var cv = await _cVRepository.GetByIdAsync(cvId);
			var CVSkills = await _cVSkillRepository.GetCVSkills(cv);
			return Ok(CVSkills);
		}

		[HttpPost]
		public async Task<IActionResult> AddCVSkill(string cvId, int skillId)
		{
			var cv = await _cVRepository.GetByIdAsync(cvId);
			var skill = await _skillRepository.GetByIdAsync(skillId);

			if (cv == null || skill == null) return NotFound();

			var cvSkills = await _cVSkillRepository.GetCVSkills(cv);

			if (cvSkills.Any(s => s.ID == skillId)) return BadRequest("Skill already exists");

			var cvSkill = new CVSkillModel
			{
				CVId = cv.AppUserId,
				SkillId = skillId
			};

			await _cVSkillRepository.CreateAsync(cvSkill);

			if (cvSkill == null)
			{
				return StatusCode(500, "Could not create");
			}
			else
			{
				return Created();
			}
		}


		[HttpDelete]
		public async Task<IActionResult> DeleteCVSkill(string cvId, int skillId)
		{
			var cv = await _cVRepository.GetByIdAsync(cvId);
			var skill = await _skillRepository.GetByIdAsync(skillId);

			if (cv == null || skill == null) return NotFound();

			var cvSkills = await _cVSkillRepository.GetCVSkills(cv);

			var filteredSkills = cvSkills.Where(s => s.ID == skillId).ToList();

			if(filteredSkills.Count() == 1)
			{
				await _cVSkillRepository.DeleteAsync(cv, skillId);
			}
			else
			{
				return BadRequest("Skill does not exist");
			}
			return Ok();
		}
	}
}
