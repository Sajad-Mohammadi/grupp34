using karriarportalen.Server.Data;
using karriarportalen.Server.DTOs.CV;
using karriarportalen.Server.Helpers;
using karriarportalen.Server.Interfaces;
using karriarportalen.Server.Mappers;
using karriarportalen.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace karriarportalen.Server.Repositories
{
	public class CVRepository : ICVRepository
	{
		private readonly AppDbContext _appDbContext;

		public CVRepository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public Task<bool> CVExists(string userId)
		{
			return _appDbContext.CVs.AnyAsync(c => c.AppUserId == userId);
		}

		public async Task<List<CVModel>> GetAllAsync(QueryObject queryObject, string userId)
		{
			string searchString = queryObject.Search.ToLower();
			if (userId != null)
			{
				var cvs = _appDbContext.CVs
					.Include(ed => ed.Educations)
					.Include(ex => ex.Experiences)
					.Where(cv => cv.AppUser.Firstname.ToLower().Contains(searchString) ||
						cv.AppUser.Lastname.ToLower().Contains(searchString) ||
						cv.ShortBio.ToLower().Contains(searchString) ||
						cv.City.ToLower().Contains(searchString) ||
						cv.Country.ToLower().Contains(searchString) ||
						cv.PostalCode.ToLower().Contains(searchString) ||
						cv.Educations.Any(ed => ed.Title.ToLower().Contains(searchString) ||
							ed.Description.ToLower().Contains(searchString) ||
							ed.Institution.ToLower().Contains(searchString) ||
							ed.City.ToLower().Contains(searchString) ||
							ed.Country.ToLower().Contains(searchString)) ||
						cv.Experiences.Any(ex => ex.Title.ToLower().Contains(searchString) ||
							ex.Description.ToLower().Contains(searchString) ||
							ex.Company.ToLower().Contains(searchString) ||
							ex.City.ToLower().Contains(searchString) ||
							ex.Country.ToLower().Contains(searchString)))
					.AsQueryable();
				return await cvs.ToListAsync();
			}
			else
			{
				var cvs = _appDbContext.CVs
					.Where(c => c.IsPublic)
					.Include(ed => ed.Educations)
					.Include(ex => ex.Experiences)
					.Where(cv => cv.AppUser.Firstname.ToLower().Contains(searchString) ||
						cv.AppUser.Lastname.ToLower().Contains(searchString) ||
						cv.ShortBio.ToLower().Contains(searchString) ||
						cv.City.ToLower().Contains(searchString) ||
						cv.Country.ToLower().Contains(searchString) ||
						cv.PostalCode.ToLower().Contains(searchString) ||
						cv.Educations.Any(ed => ed.Title.ToLower().Contains(searchString) ||
							ed.Description.ToLower().Contains(searchString) ||
							ed.Institution.ToLower().Contains(searchString) ||
							ed.City.ToLower().Contains(searchString) ||
							ed.Country.ToLower().Contains(searchString)) ||
						cv.Experiences.Any(ex => ex.Title.ToLower().Contains(searchString) ||
							ex.Description.ToLower().Contains(searchString) ||
							ex.Company.ToLower().Contains(searchString) ||
							ex.City.ToLower().Contains(searchString) ||
							ex.Country.ToLower().Contains(searchString)))
					.AsQueryable();
				return await cvs.ToListAsync();
			}
		}
		//var cvs = _appDbContext.CVs
		//		.Where(c => c.IsPublic)
		//		.Include(ed => ed.Educations)
		//		.Include(ex => ex.Experiences)
		//		.AsQueryable();

		//	if (!string.IsNullOrWhiteSpace(queryObject.Search))
		//	{
		//		cvs = cvs.Where(c => c.ShortBio.Contains(queryObject.Search));
		//	}

		//	return await cvs.ToListAsync();
		//}

		public async Task<CVModel?> GetByIdAsync(string userId)
		{
			return await _appDbContext.CVs
				.Include(ed => ed.Educations)
				.Include(ex => ex.Experiences)
				.FirstOrDefaultAsync(c => c.AppUserId == userId);
		}

		public async Task<CVModel?> UpdateAsync(string userID, UpdateCVDTO updateCVDTO)
		{
			var existingCV = await _appDbContext.CVs.FirstOrDefaultAsync(c => c.AppUserId == userID);
			if (existingCV == null)
			{
				return null;
			}
			existingCV.Firstname = updateCVDTO.Firstname;
			existingCV.Lastname = updateCVDTO.Lastname;
			existingCV.ShortBio = updateCVDTO.ShortBio;
			existingCV.Email = updateCVDTO.Email;
			existingCV.PhoneNumber = updateCVDTO.PhoneNumber;
			existingCV.Website = updateCVDTO.Website;
			existingCV.City = updateCVDTO.City;
			existingCV.Country = updateCVDTO.Country;
			existingCV.PostalCode = updateCVDTO.PostalCode;
			existingCV.IsPublic = updateCVDTO.IsPublic;
			_appDbContext.CVs.Update(existingCV);
			await _appDbContext.SaveChangesAsync();
			return existingCV;
		}
	}
}
