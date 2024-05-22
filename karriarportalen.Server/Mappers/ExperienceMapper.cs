using karriarportalen.Server.DTOs.CV.Experience;
using karriarportalen.Server.Models;

namespace karriarportalen.Server.Mappers
{
	public static class ExperienceMapper
	{
		public static ExperienceDTO ToExperienceDTO(this ExperienceModel experienceModel)
		{
			return new ExperienceDTO
			{
				ID = experienceModel.ID,
				Title = experienceModel.Title,
				Description = experienceModel.Description,
				Company = experienceModel.Company,
				City = experienceModel.City,
				Country = experienceModel.Country,
				StartDate = experienceModel.StartDate,
				EndDate = experienceModel.EndDate,
				CVId = experienceModel.CVId
			};
		}

		public static ExperienceModel ToExperienceModelFromCreate(this CreateExperienceDTO createExperienceDTO, string cvId)
		{
			return new ExperienceModel
			{
				Title = createExperienceDTO.Title,
				Description = createExperienceDTO.Description,
				Company = createExperienceDTO.Company,
				City = createExperienceDTO.City,
				Country = createExperienceDTO.Country,
				StartDate = createExperienceDTO.StartDate,
				EndDate = createExperienceDTO.EndDate,
				CVId = cvId
			};
		}

		public static ExperienceModel ToExperienceModelFromUpdate(this UpdateExperienceDTO updateExperienceDTO, string cvId)
		{
			return new ExperienceModel
			{
				Title = updateExperienceDTO.Title,
				Description = updateExperienceDTO.Description,
				Company = updateExperienceDTO.Company,
				City = updateExperienceDTO.City,
				Country = updateExperienceDTO.Country,
				StartDate = updateExperienceDTO.StartDate,
				EndDate = updateExperienceDTO.EndDate,
				CVId = cvId
			};
		}
	}
}
