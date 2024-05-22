using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace karriarportalen.Server.Models
{
	public class CVModel
	{
		[Key]
		public string? AppUserId { get; set; } // PK and FK
		public string Firstname { get; set; } = string.Empty;
		public string Lastname { get; set; } = string.Empty;
		public string ShortBio { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string Website { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string Country { get; set; } = string.Empty;
		public string PostalCode { get; set; } = string.Empty;

		public bool IsPublic { get; set; } = false;

		public List<EducationModel> Educations { get; set; } = new List<EducationModel>();

		public List<ExperienceModel> Experiences { get; set; } = new List<ExperienceModel>();

		public List<CVSkillModel> CVSkills { get; set; } = new List<CVSkillModel>();

		public AppUser? AppUser { get; set; }
	}
}
