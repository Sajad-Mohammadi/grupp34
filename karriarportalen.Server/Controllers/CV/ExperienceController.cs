using karriarportalen.Server.DTOs.CV.Experience;
using karriarportalen.Server.Interfaces;
using karriarportalen.Server.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace karriarportalen.Server.Controllers.CV
{
	[Authorize]
	[Route("[controller]")]
	[ApiController]
	public class ExperienceController : ControllerBase
	{
		private readonly IExperienceRepository _experienceRepository;
		private readonly ICVRepository _cvRepository;

		public ExperienceController(IExperienceRepository experienceRepository, ICVRepository cvRepository)
		{
			_experienceRepository = experienceRepository;
			_cvRepository = cvRepository;
		}


		//get all experiences
		[HttpGet]
		public async Task<IActionResult> GetAll(string cvId)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var experiences = await _experienceRepository.GetAllAsync(cvId);
			if (experiences == null)
			{
				return NotFound();
			}
			var experienceDTOs = experiences.Select(e => e.ToExperienceDTO());
			return Ok(experienceDTOs);
		}



		[HttpGet]
		[Route("{experienceId:int}")]
		public async Task<IActionResult> GetById(int experienceId)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var experience = await _experienceRepository.GetByIdAsync(experienceId);
			if (experience == null)
			{
				return NotFound();
			}
			return Ok(experience.ToExperienceDTO());
		}


		//create
		[HttpPost]
		public async Task<IActionResult> Create(CreateExperienceDTO createExperienceDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var cvId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (!await _cvRepository.CVExists(cvId))
			{
				return BadRequest("CV does not exist");
			}

			var experience = createExperienceDTO.ToExperienceModelFromCreate(cvId);
			await _experienceRepository.CreateAsync(experience);
			return CreatedAtAction(nameof(GetById), new { experienceId = experience.ID }, experience.ToExperienceDTO());
		}


		//update
		[HttpPut]
		[Route("{experienceId:int}")]
		public async Task<IActionResult> Update([FromRoute] int experienceId,[FromBody] UpdateExperienceDTO updateExperienceDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}


			var cvId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var experience = await _experienceRepository.UpdateAsync(experienceId, updateExperienceDTO.ToExperienceModelFromUpdate(cvId));
			if (experience == null)
			{
				return NotFound();
			}
			return Ok(experience.ToExperienceDTO());
		}


		//delete
		[HttpDelete("{experienceId:int}")]
		public async Task<IActionResult> Delete([FromRoute] int experienceId)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var experience = await _experienceRepository.DeleteAsync(experienceId);
			if (experience == null)
			{
				return NotFound();
			}
			return Ok(experience.ToExperienceDTO());
		}
	}
}
