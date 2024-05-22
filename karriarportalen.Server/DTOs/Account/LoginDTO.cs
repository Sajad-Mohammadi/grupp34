using System.ComponentModel.DataAnnotations;

namespace karriarportalen.Server.DTOs.Account
{
	public class LoginDTO
	{
		[Required]
		public string? Email { get; set; }
		[Required]
		public string? Password { get; set; }
	}
}
