using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace karriarportalen.Server.Models
{
	public class EducationModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string Institution { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string Country { get; set; } = string.Empty;
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string? CVId { get; set; } // FK
		public CVModel? CV { get; set; }
	}
}
