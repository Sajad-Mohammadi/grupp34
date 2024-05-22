using karriarportalen.Server.Models;

namespace karriarportalen.Server.Interfaces
{
	public interface ITokenService
	{
		string CreateToken(AppUser user);
	}
}
