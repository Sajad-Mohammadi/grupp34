using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using karriarportalen.Server.Models;
using System.Reflection.Emit;

namespace karriarportalen.Server.Data
{
	public class AppDbContext : IdentityDbContext<AppUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		public DbSet<AppUser> AppUsers { get; set; }
		public DbSet<CVModel> CVs { get; set; }
		public DbSet<EducationModel> Educations { get; set; }
		public DbSet<ExperienceModel> Experiences { get; set; }
		public DbSet<SkillModel> Skills { get; set; }
		public DbSet<CVSkillModel> CVSkills { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			List<IdentityRole> roles = new List<IdentityRole>
			{
				new IdentityRole { Name = "JobSeeker", NormalizedName = "JOBSEEKER" },
				new IdentityRole { Name = "Employer", NormalizedName = "EMPLOYER" }
			};
			builder.Entity<IdentityRole>().HasData(roles);

			builder.Entity<CVSkillModel>(x => x.HasKey(cs => new { cs.CVId, cs.SkillId }));
			builder.Entity<CVSkillModel>()
				.HasOne(cs => cs.Cv)
				.WithMany(cv => cv.CVSkills)
				.HasForeignKey(cs => cs.CVId);
			builder.Entity<CVSkillModel>()
				.HasOne(cs => cs.Skill)
				.WithMany(s => s.CVSkills)
				.HasForeignKey(cs => cs.SkillId);
		}
	}
}
