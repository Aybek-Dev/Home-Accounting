using HomeAccounting.Domain.Entities.Categories;
using HomeAccounting.Domain.Entities.Transactions;
using HomeAccounting.Domain.Enums;

namespace HomeAccounting.Domain.Interfaces.Services
{
	public interface ITransactionService
	{
		Task CreateTransaction(string token, DateTime createDate, TransactionType type, Guid categoryId, decimal amount, string? title);
		Task DeleteTransaction(string token, Guid id);
		Task<Transaction> GetTransactionById(string token, Guid id);
		Task<IList<Transaction>> GetAllTransactions(string token);
		Task Update(string token, Guid id, TransactionType type, Guid categoryId, decimal amount, string? title);
	}
}
