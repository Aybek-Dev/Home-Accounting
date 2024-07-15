using HomeAccounting.Domain.Entities.Categories;
using HomeAccounting.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace HomeAccounting.Api.Contract.Transactions
{
	public record class UpdateTransactionRequest(
		[Required] Guid Id,
		[Required] TransactionType TransactionType,
		[Required] Category Category,
		[Required] decimal Amount,
		string Title);
}
