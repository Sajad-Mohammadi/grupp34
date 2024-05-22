namespace karriarportalen.Server.DTOs.CV
{
	public class UpdateCVDTO
	{
		public string? Firstname { get; set; } = string.Empty;
		public string? Lastname { get; set; } = string.Empty;
		public string? ShortBio { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string Website { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string Country { get; set; } = string.Empty;
		public string PostalCode { get; set; } = string.Empty;

		public bool IsPublic { get; set; } = false;
	}
}
