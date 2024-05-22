using System.ComponentModel.DataAnnotations;

namespace karriarportalen.Server.DTOs.Account
{
	public class RegisterDTO
	{
		[Required]
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
		[Required]
		[EmailAddress]
		public string? Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string? Password { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Passwords do not match.")]
		public string? ConfirmPassword { get; set; }
		public string? UserRole { get; set; }
	}
}
