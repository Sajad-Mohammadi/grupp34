using karriarportalen.Server.DTOs.CV;
using karriarportalen.Server.Helpers;
using karriarportalen.Server.Models;

namespace karriarportalen.Server.Interfaces
{
	public interface ICVRepository
	{
		//get all CVs
		Task<List<CVModel>> GetAllAsync(QueryObject queryObject, string userId);
		Task<CVModel?> GetByIdAsync(string userId);
		Task<CVModel?> UpdateAsync(string userID, UpdateCVDTO updateCVDTO);
		Task<bool> CVExists(string userId);
	}
}
