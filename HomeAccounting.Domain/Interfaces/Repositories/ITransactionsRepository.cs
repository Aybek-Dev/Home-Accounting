using HomeAccounting.Domain.Entities.Categories;
using HomeAccounting.Domain.Entities.Transactions;
using HomeAccounting.Domain.Enums;

namespace HomeAccounting.Domain.Interfaces.Repositories
{
	public interface ITransactionsRepository
	{
		Task Create(Transaction transaction);
		Task Delete(Guid guid,Guid userId);
		Task<IList<Transaction>> GetAll(Guid userId);
		Task<Transaction> GetById(Guid id, Guid userId);
		Task Update(Guid userId,Guid id, TransactionType type, Guid categoryId, decimal amount, string? title,DateTimeOffset updateTime);
		Task<IList<Transaction>> GetTransactionByFilter(Guid userId, DateTime? startDate, DateTime? endDate, Guid categoryId, TransactionType? transactionType);
	}
}
