using HomeAccounting.Domain.Entities.Users;

namespace HomeAccounting.Application.Interfaces
{
	public interface IJwtProvider
	{
		string Generate(User user);
		Guid GetUserIdFromToken(string token);
	}
}
