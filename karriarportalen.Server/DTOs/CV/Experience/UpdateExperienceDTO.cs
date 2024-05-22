using System.ComponentModel.DataAnnotations;

namespace karriarportalen.Server.DTOs.CV.Experience
{
	public class UpdateExperienceDTO
	{
		[Required]
		[MinLength(2, ErrorMessage = "Title most be 2 characters")]
		[MaxLength(100, ErrorMessage = "Title most be 100 characters")]
		public string Title { get; set; } = string.Empty;
		public string? Description { get; set; } = string.Empty;
		public string Company { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string Country { get; set; } = string.Empty;
		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
		[Display(Name = "Start Date")]
		public DateTime? StartDate { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
		[Display(Name = "End Date")]
		public DateTime? EndDate { get; set; }
	}
}
