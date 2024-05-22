using karriarportalen.Server.Models;

namespace karriarportalen.Server.Interfaces
{
	public interface IEducationRepository
	{
		Task<IEnumerable<EducationModel>> GetAllAsync(string cvId);
		Task<EducationModel?> GetByIdAsync(int educationId);
		Task<EducationModel> CreateAsync(EducationModel educationModel);
		Task<EducationModel?> UpdateAsync(int educationId,EducationModel educationModel);
		Task<EducationModel?> DeleteAsync(int educationId);
	}
}
