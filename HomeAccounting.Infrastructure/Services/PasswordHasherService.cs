using HomeAccounting.Application.Interfaces;

namespace HomeAccounting.Infrastructure.Services
{
	public class PasswordHasherService : IPasswordHasher
	{
		public string Generate(string password) =>
			BCrypt.Net.BCrypt.EnhancedHashPassword(password);


		public bool Verify(string password, string passwordHash) =>
			BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
	}
}
