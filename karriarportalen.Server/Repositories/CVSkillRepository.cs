using karriarportalen.Server.Data;
using karriarportalen.Server.Interfaces;
using karriarportalen.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace karriarportalen.Server.Repositories
{
	public class CVSkillRepository : ICVSkillRepository
	{
		private readonly AppDbContext _appDbContext;

		public CVSkillRepository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<CVSkillModel> CreateAsync(CVSkillModel cVSkillModel)
		{
			await _appDbContext.CVSkills.AddAsync(cVSkillModel);
			await _appDbContext.SaveChangesAsync();
			return cVSkillModel;
		}

		public async Task<CVSkillModel> DeleteAsync(CVModel cVModel, int skillId)
		{
			var cvSkill = await _appDbContext.CVSkills.FirstOrDefaultAsync(cvSkill => cvSkill.CVId == cVModel.AppUserId && cvSkill.SkillId == skillId);

			if (cvSkill == null) return null;

			_appDbContext.CVSkills.Remove(cvSkill);
			await _appDbContext.SaveChangesAsync();

			return cvSkill;
		}

		public async Task<List<SkillModel>> GetCVSkills(CVModel cv)
		{
			return await _appDbContext.CVSkills
				.Where(cvs => cvs.CVId == cv.AppUserId)
				.Select(skill => new SkillModel
				{
					ID = skill.SkillId,
					Name = skill.Skill.Name
				}).ToListAsync();
		}
	}
}
