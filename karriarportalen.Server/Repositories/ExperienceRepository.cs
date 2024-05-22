using karriarportalen.Server.Data;
using karriarportalen.Server.Interfaces;
using karriarportalen.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace karriarportalen.Server.Repositories
{
	public class ExperienceRepository : IExperienceRepository
	{
		private readonly AppDbContext _appDbContext;

		public ExperienceRepository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<ExperienceModel> CreateAsync(ExperienceModel experienceModel)
		{
			await _appDbContext.Experiences.AddAsync(experienceModel);
			await _appDbContext.SaveChangesAsync();
			return experienceModel;
		}

		public async Task<ExperienceModel?> DeleteAsync(int experienceId)
		{
			var experience = await _appDbContext.Experiences.FirstOrDefaultAsync(ex => ex.ID == experienceId);
			if (experience == null)
			{
				return null;
			}
			_appDbContext.Experiences.Remove(experience);
			await _appDbContext.SaveChangesAsync();
			return experience;
		}

		public async Task<IEnumerable<ExperienceModel>?> GetAllAsync(string cvId)
		{
			return await _appDbContext.Experiences.Where(e => e.CVId == cvId).ToListAsync();
		}

		public async Task<ExperienceModel?> GetByIdAsync(int experienceId)
		{
			var experience = await _appDbContext.Experiences.FindAsync(experienceId);
			if (experience == null)
			{
				return null;
			}
			return experience;
		}

		public async Task<ExperienceModel?> UpdateAsync(int experienceId, ExperienceModel experienceModel)
		{
			var existingExperience = await _appDbContext.Experiences.FindAsync(experienceId);
			if (existingExperience == null)
			{
				return null;
			}
			existingExperience.Title = experienceModel.Title;
			existingExperience.Description = experienceModel.Description;
			existingExperience.StartDate = experienceModel.StartDate;
			existingExperience.EndDate = experienceModel.EndDate;
			existingExperience.Company = experienceModel.Company;
			existingExperience.City = experienceModel.City;
			existingExperience.Country = experienceModel.Country;
			existingExperience.CVId = experienceModel.CVId;
			await _appDbContext.SaveChangesAsync();
			return existingExperience;
		}
	}
}
