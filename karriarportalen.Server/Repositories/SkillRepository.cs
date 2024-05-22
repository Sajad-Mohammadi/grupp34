using karriarportalen.Server.Data;
using karriarportalen.Server.Interfaces;
using karriarportalen.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace karriarportalen.Server.Repositories
{
	public class SkillRepository : ISkillRepository
	{
		private readonly AppDbContext _appDbContext;

		public SkillRepository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<List<SkillModel>> GetAllAsync()
		{
			return await _appDbContext.Skills.ToListAsync();
		}

		public Task<SkillModel?> GetByIdAsync(int id)
		{
			var skill = _appDbContext.Skills.FirstOrDefaultAsync(s => s.ID == id);
			return skill;
		}

		public Task<SkillModel> GetSkillByName(string skillName)
		{
			throw new NotImplementedException();
		}
	}
}
