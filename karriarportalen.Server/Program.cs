using karriarportalen.Server.Data;
using karriarportalen.Server.Interfaces;
using karriarportalen.Server.Models;
using karriarportalen.Server.Repositories;
using karriarportalen.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(option =>
{
	option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
	option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter a valid token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer"
	});
	option.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type=ReferenceType.SecurityScheme,
					Id="Bearer"
				}
			},
			new string[]{}
		}
	});
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
	options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidateAudience = true,
		ValidAudience = builder.Configuration["Jwt:Audience"],
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
		ValidateLifetime = true,
	};
});

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICVRepository, CVRepository>();
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<ICVSkillRepository, CVSkillRepository>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
