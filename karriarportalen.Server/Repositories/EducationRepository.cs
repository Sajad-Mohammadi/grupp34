using karriarportalen.Server.Data;
using karriarportalen.Server.Interfaces;
using karriarportalen.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace karriarportalen.Server.Repositories
{
	public class EducationRepository : IEducationRepository
	{
		private readonly AppDbContext _appDbContext;

		public EducationRepository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<EducationModel> CreateAsync(EducationModel education)
		{
			await _appDbContext.Educations.AddAsync(education);
			await _appDbContext.SaveChangesAsync();
			return education;
		}

		public async Task<EducationModel?> DeleteAsync(int educationId)
		{
			var education = await _appDbContext.Educations.FirstOrDefaultAsync(ed => ed.ID == educationId);
			if (education == null)
			{
				return null;
			}
			_appDbContext.Educations.Remove(education);
			await _appDbContext.SaveChangesAsync();
			return education;
		}

		public async Task<IEnumerable<EducationModel>?> GetAllAsync(string cvId)
		{
			return await _appDbContext.Educations.Where(e => e.CVId == cvId).ToListAsync();
		}

		public async Task<EducationModel?> GetByIdAsync(int educationId)
		{
			var education = await _appDbContext.Educations.FindAsync(educationId);
			if (education == null)
			{
				return null;
			}
			return education;
		}

		public async Task<EducationModel?> UpdateAsync(int educationId, EducationModel educationModel)
		{
			var existingEducation = await _appDbContext.Educations.FindAsync(educationId);
			if (existingEducation == null)
			{
				return null;
			}
			existingEducation.Title = educationModel.Title;
			existingEducation.Description = educationModel.Description;
			existingEducation.StartDate = educationModel.StartDate;
			existingEducation.EndDate = educationModel.EndDate;
			existingEducation.Institution = educationModel.Institution;
			existingEducation.City = educationModel.City;
			existingEducation.Country = educationModel.Country;
			existingEducation.CVId = educationModel.CVId;
			await _appDbContext.SaveChangesAsync();
			return existingEducation;
		}
	}
}
