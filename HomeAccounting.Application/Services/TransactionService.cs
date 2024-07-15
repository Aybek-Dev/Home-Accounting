using HomeAccounting.Application.Interfaces;
using HomeAccounting.Domain.Common.Exceptions;
using HomeAccounting.Domain.Entities.Transactions;
using HomeAccounting.Domain.Enums;
using HomeAccounting.Domain.Interfaces.Repositories;
using HomeAccounting.Domain.Interfaces.Services;

namespace HomeAccounting.Application.Services
{
	public class TransactionService : ITransactionService
	{
		private readonly ITransactionsRepository _transactionRepository;
		private readonly IJwtProvider _jwtProvider;
		private readonly IDateTime _dateTime;
		private readonly IGuidGenerator _guidGenerator;

		public TransactionService(
			ITransactionsRepository transactionRepository,
			IJwtProvider jwtProvider,
			IDateTime dateTime,
			IGuidGenerator guidGenerator)
		{
			_transactionRepository = transactionRepository;
			_jwtProvider = jwtProvider;
			_dateTime = dateTime;
			_guidGenerator = guidGenerator;
		}
		public async Task CreateTransaction(string token, DateTime createDate, TransactionType type, Guid categoryId, decimal amount, string? title)
		{
			var userId = _jwtProvider.GetUserIdFromToken(token);
			var transaction = Transaction.Create(_guidGenerator.Guid, userId, categoryId, _dateTime.Now, type, amount, title);
			await _transactionRepository.Create(transaction);
		}

		public async Task DeleteTransaction(string token, Guid id)
		{
			var userId = _jwtProvider.GetUserIdFromToken(token);
			var maybeTransaction = await _transactionRepository.GetById(id, userId);
			ValidateNotNull(maybeTransaction, id);
			await _transactionRepository.Delete(id, userId);
		}

		public async Task<IList<Transaction>> GetAllTransactions(string token)
		{
			var userId = _jwtProvider.GetUserIdFromToken(token);
			return await _transactionRepository.GetAll(userId);
		}

		public async Task<Transaction> GetTransactionById(string token, Guid id)
		{
			var userId = _jwtProvider.GetUserIdFromToken(token);
			var maybeTransaction = await _transactionRepository.GetById(id, userId);
			ValidateNotNull(maybeTransaction, id);
			return maybeTransaction;
		}

		public async Task Update(string token, Guid id, TransactionType type, Guid categoryId, decimal amount, string? title)
		{
			var userId = _jwtProvider.GetUserIdFromToken(token);
			await _transactionRepository.Update(userId, id, type, categoryId, amount, title, _dateTime.Now);
		}
		private void ValidateNotNull(Transaction maybeTransaction, Guid id)
		{
			if (maybeTransaction == null)
				throw new NotFoundException(nameof(Transaction), id);
		}
	}
}
