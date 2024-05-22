using karriarportalen.Server.Models;

namespace karriarportalen.Server.Interfaces
{
	public interface IExperienceRepository
	{
		Task<IEnumerable<ExperienceModel>> GetAllAsync(string cvId);
		Task<ExperienceModel?> GetByIdAsync(int experienceId);
		Task<ExperienceModel> CreateAsync(ExperienceModel experienceModel);
		Task<ExperienceModel?> UpdateAsync(int experienceId,ExperienceModel experienceModel);
		Task<ExperienceModel?> DeleteAsync(int experienceId);
	}
}
