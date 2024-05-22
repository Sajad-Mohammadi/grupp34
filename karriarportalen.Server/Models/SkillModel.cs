using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace karriarportalen.Server.Models
{
	public class SkillModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public string? Name { get; set; }
		public List<CVSkillModel> CVSkills { get; set; } = new List<CVSkillModel>();
	}
}
