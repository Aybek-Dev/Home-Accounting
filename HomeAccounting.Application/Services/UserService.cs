using HomeAccounting.Application.Interfaces;
using HomeAccounting.Domain.Common.Exceptions;
using HomeAccounting.Domain.Entities.Users;
using HomeAccounting.Domain.Interfaces.Repositories;
using HomeAccounting.Domain.Interfaces.Services;

namespace HomeAccounting.Application.Services
{
	public class UserService : IUserService
	{
		private readonly IPasswordHasher _passwordHasher;
		private readonly IUsersRepository _userRepository;
		private readonly IJwtProvider _jwtProvider;
		private readonly IGuidGenerator _guidGenerator;
		private readonly IDateTime _dateTime;

		public UserService(
			IPasswordHasher passwordHasher,
			IGuidGenerator guidGenerator,
			IUsersRepository userRepository,
			IJwtProvider jwtProvider,
			IDateTime dateTime)
		{
			_passwordHasher = passwordHasher;
			_userRepository = userRepository;
			_jwtProvider = jwtProvider;
			_guidGenerator = guidGenerator;
			_dateTime = dateTime;
		}

		public async Task<string> Login(string email, string password)
		{
			var user = await _userRepository.GetUserByEmailAsync(email);
			if(user==null) throw new NotFoundException(nameof(User),email);
			var userVerify = _passwordHasher.Verify(password, user.PasswordHash);
			if (!userVerify) throw new Exception("Failed to login");
			var token = _jwtProvider.Generate(user);
			return token;
		}

		public async Task Register(string userName, string email, string password)
		{
			var hashedPassword = _passwordHasher.Generate(password);
			var user = User.Create(_guidGenerator.Guid, _dateTime.Now, userName, hashedPassword, email);
			await _userRepository.AddUserAsync(user);
		}
	}
}
