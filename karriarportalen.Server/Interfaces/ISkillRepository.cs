using karriarportalen.Server.Models;

namespace karriarportalen.Server.Interfaces
{
	public interface ISkillRepository
	{
		Task<List<SkillModel>> GetAllAsync();
		Task<SkillModel?> GetByIdAsync(int id);
		Task<SkillModel> GetSkillByName(string skillName);
	}
}
