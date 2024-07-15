using AutoMapper;
using HomeAccounting.Domain.Common.Exceptions;
using HomeAccounting.Domain.Entities.Transactions;
using HomeAccounting.Domain.Enums;
using HomeAccounting.Domain.Interfaces.Repositories;
using HomeAccounting.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeAccounting.Infrastructure.Repositories
{
	public class TransactionsRepository : ITransactionsRepository
	{
		private readonly HomeAccountingDbContext _context;
		private readonly IMapper _mapper;

		public TransactionsRepository(
			HomeAccountingDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task Create(Transaction transaction)
		{
			var transactionEntity = new TransactionEntity()
			{
				Id = transaction.Id,
				CategoryId = transaction.CategoryId,
				UserId = transaction.UserId,
				CreatedDate = transaction.CreatedDate,
				Type = transaction.Type,
				Amount = transaction.Amount,
				Title = transaction.Title,
			};
			await _context.Transactions.AddAsync(transactionEntity);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(Guid id, Guid userId)
		{
			await GetById(id, userId);
			await _context.Transactions
			.Where(l => l.Id == id && l.UserId == userId)
			.ExecuteDeleteAsync();
		}

		public async Task<IList<Transaction>> GetAll(Guid userId)
		{
			var transactions = await _context.Transactions
				.Where(l => l.UserId == userId)
				.AsNoTracking()
				.Include(c => c.Category)
				.ToListAsync();
			return _mapper.Map<IList<Transaction>>(transactions);
		}

		public async Task<Transaction> GetById(Guid id, Guid userId)
		{
			var transaction = await _context.Transactions
				.Include(c => c.Category)
				.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
			ValidateUserIsNotNull(transaction);
			return _mapper.Map<Transaction>(transaction);
		}

		public async Task Update(Guid userId, Guid id, TransactionType type, Guid categoryId, decimal amount, string? title, DateTimeOffset updateTime)
		{
			await GetById(id, userId);
			await _context.Transactions.
				Where(t => t.Id == id && t.UserId == userId)
				.ExecuteUpdateAsync(t => t
					.SetProperty(u => u.Type, type)
					.SetProperty(u => u.CategoryId, categoryId)
					.SetProperty(u => u.Amount, amount)
					.SetProperty(u => u.Title, title)
					.SetProperty(u => u.UpdateDate, updateTime));
			await _context.SaveChangesAsync();
		}
		private void ValidateUserIsNotNull(TransactionEntity? userEntity)
		{
			if (userEntity == null)
			{
				throw new NotFoundException(nameof(Transaction), "such a user will not find");
			}
		}
	}
}
