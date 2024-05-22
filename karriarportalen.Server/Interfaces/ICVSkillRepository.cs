using karriarportalen.Server.Models;

namespace karriarportalen.Server.Interfaces
{
	public interface ICVSkillRepository
	{
		Task<List<SkillModel>> GetCVSkills(CVModel cv);
		Task<CVSkillModel> CreateAsync(CVSkillModel cVSkillModel);
		Task<CVSkillModel> DeleteAsync(CVModel cVModel, int skillId);
	}
}
