using HomeAccounting.Domain.Entities.Users;

namespace HomeAccounting.Domain.Interfaces.Repositories
{
	public interface IUsersRepository
	{
		Task AddUserAsync(User user);
		Task<User> GetUserByEmailAsync(string email);
		Task<User> GetUserByIdAsync(Guid id);
	}
}
