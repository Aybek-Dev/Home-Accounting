using AutoMapper;
using HomeAccounting.Domain.Common.Exceptions;
using HomeAccounting.Domain.Entities.Users;
using HomeAccounting.Domain.Interfaces.Repositories;
using HomeAccounting.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeAccounting.Infrastructure.Repositories
{
	public class UsersRepository : IUsersRepository
	{
		private readonly HomeAccountingDbContext _context;
		private readonly IMapper _mapper;

		public UsersRepository(
			HomeAccountingDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task AddUserAsync(User user)
		{
			bool isExists = _context.Users.Any(u => u.Email == user.Email);
			ValidateUserNotExists(isExists, user.Email);
			var userEntity = new UserEntity()
			{
				Id = user.Id,
				Name = user.Name,
				Email = user.Email,
				PasswordHash = user.PasswordHash,
				CreatedDate = user.CreatedDate
			};
			await _context.Users.AddAsync(userEntity);
			await _context.SaveChangesAsync();
		}

		public async Task<User> GetUserByEmailAsync(string email)
		{

			var userEntity = await _context.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(u => u.Email == email);
			ValidateUserIsNotNull(userEntity);
			return _mapper.Map<User>(userEntity);
		}


		public async Task<User> GetUserByIdAsync(Guid id)
		{
			var userEntity = await _context.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(u => u.Id == id);
			ValidateUserIsNotNull(userEntity);
			return _mapper.Map<User>(userEntity);
		}
		private void ValidateUserNotExists(bool isExists, string email)
		{
			if (isExists)
			{
				throw new AlreadyExistsException(nameof(User), email);
			}
		}
		private void ValidateUserIsNotNull(UserEntity? userEntity)
		{
			if (userEntity == null)
			{
				throw new NotFoundException(nameof(User), "such a user will not find");
			}
		}
	}
}
