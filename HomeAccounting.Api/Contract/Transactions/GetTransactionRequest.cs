using HomeAccounting.Domain.Enums;

namespace HomeAccounting.Api.Contract.Transactions
{
	public record class GetTransactionRequest(
		Guid Id,
		DateTimeOffset CreatedAt,
		TransactionType Type,
		string CategoryName,
		decimal Amount,
		string Title);
}
