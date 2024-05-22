using karriarportalen.Server.DTOs.CV;
using karriarportalen.Server.Models;

namespace karriarportalen.Server.Mappers
{
	public static class CVMapper
	{
		public static CVDTO ToCVDTO(this CVModel cvModel)
		{
			return new CVDTO
			{
				AppUserId = cvModel.AppUserId,
				Firstname = cvModel.Firstname,
				Lastname = cvModel.Lastname,
				ShortBio = cvModel.ShortBio,
				Email = cvModel.Email,
				PhoneNumber = cvModel.PhoneNumber,
				Website = cvModel.Website,
				City = cvModel.City,
				Country = cvModel.Country,
				PostalCode = cvModel.PostalCode,

				IsPublic = cvModel.IsPublic,

				Educations = cvModel.Educations.Select(ed => ed.ToEducationDTO()).ToList(),
				Experiences = cvModel.Experiences.Select(ex => ex.ToExperienceDTO()).ToList(),
			};
		}
	}
}
