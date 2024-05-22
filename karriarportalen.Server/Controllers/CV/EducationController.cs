using karriarportalen.Server.DTOs.CV.Education;
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
	public class EducationController : ControllerBase
	{
		private readonly IEducationRepository _educationRepository;
		private readonly ICVRepository _cvRepository;

		public EducationController(IEducationRepository educationRepository, ICVRepository cvRepository)
		{
			_educationRepository = educationRepository;
			_cvRepository = cvRepository;
		}


		//get all educations
		[HttpGet]
		public async Task<IActionResult> GetAll(string cvId)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var educations = await _educationRepository.GetAllAsync(cvId);
			if (educations == null)
			{
				return NotFound();
			}
			var educationDTOs = educations.Select(e => e.ToEducationDTO());
			return Ok(educationDTOs);
		}



		[HttpGet]
		[Route("{educationId:int}")]
		public async Task<IActionResult> GetById(int educationId)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var education = await _educationRepository.GetByIdAsync(educationId);
			if (education == null)
			{
				return NotFound();
			}
			return Ok(education.ToEducationDTO());
		}


		//create
		[HttpPost]
		public async Task<IActionResult> Create(CreateEducationDTO createEducationDTO)
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

			var education = createEducationDTO.ToEducationModelFromCreate(cvId);
			await _educationRepository.CreateAsync(education);
			return CreatedAtAction(nameof(GetById), new { educationId = education.ID }, education.ToEducationDTO());
		}


		//update
		[HttpPut]
		[Route("{educationId:int}")]
		public async Task<IActionResult> Update([FromRoute] int educationId,[FromBody] UpdateEducationDTO updateEducationDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var cvId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var education = await _educationRepository.UpdateAsync(educationId, updateEducationDTO.ToEducationModelFromUpdate(cvId));
			if (education == null)
			{
				return NotFound("Education not found");
			}
			return Ok(education.ToEducationDTO());
		}


		//delete
		[HttpDelete]
		[Route("{educationId:int}")]
		public async Task<IActionResult> Delete([FromRoute] int educationId)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var education = await _educationRepository.DeleteAsync(educationId);
			if (education == null)
			{
				return NotFound("Education not found");
			}
			return Ok(education.ToEducationDTO());
		}
	}
}
