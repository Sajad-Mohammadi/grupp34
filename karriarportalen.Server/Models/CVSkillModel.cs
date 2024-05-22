namespace karriarportalen.Server.Models
{
	public class CVSkillModel
	{
		public string CVId { get; set; }
		public int SkillId { get; set; }
		public CVModel Cv { get; set; }
		public SkillModel Skill { get; set; }
	}
}
