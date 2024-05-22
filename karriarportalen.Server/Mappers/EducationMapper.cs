using karriarportalen.Server.DTOs.CV.Education;
using karriarportalen.Server.Models;

namespace karriarportalen.Server.Mappers
{
	public static class EducationMapper
	{
		public static EducationDTO ToEducationDTO(this EducationModel educationModel)
		{
			return new EducationDTO
			{
				ID = educationModel.ID,
				Title = educationModel.Title,
				Description = educationModel.Description,
				Institution = educationModel.Institution,
				City = educationModel.City,
				Country = educationModel.Country,
				StartDate = educationModel.StartDate,
				EndDate = educationModel.EndDate,
				CVId = educationModel.CVId
			};
		}

		public static EducationModel ToEducationModelFromCreate(this CreateEducationDTO createEducationDTO, string cvId)
		{
			return new EducationModel
			{
				Title = createEducationDTO.Title,
				Description = createEducationDTO.Description,
				Institution = createEducationDTO.Institution,
				City = createEducationDTO.City,
				Country = createEducationDTO.Country,
				StartDate = createEducationDTO.StartDate,
				EndDate = createEducationDTO.EndDate,
				CVId = cvId
			};
		}

		public static EducationModel ToEducationModelFromUpdate(this UpdateEducationDTO updateEducationDTO, string cvId)
		{
			return new EducationModel
			{
				Title = updateEducationDTO.Title,
				Description = updateEducationDTO.Description,
				Institution = updateEducationDTO.Institution,
				City = updateEducationDTO.City,
				Country = updateEducationDTO.Country,
				StartDate = updateEducationDTO.StartDate,
				EndDate = updateEducationDTO.EndDate,
				CVId = cvId,
			};
		}
	}
}
