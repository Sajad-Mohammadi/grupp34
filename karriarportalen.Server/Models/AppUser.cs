using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace karriarportalen.Server.Models
{
	public class AppUser : IdentityUser
	{
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
		[JsonIgnore]
		public CVModel? CV { get; set; }
	}
}
