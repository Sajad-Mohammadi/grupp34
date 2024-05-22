namespace karriarportalen.Server.Helpers
{
	public class QueryObject
	{
		public string? Search { get; set; } = "";
		public string? SortBy { get; set; } = "";
		public bool IsSortAscending { get; set; } = true;
	}
}
