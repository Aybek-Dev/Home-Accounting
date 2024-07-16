using HomeAccounting.Api.Contract.Categories;
using HomeAccounting.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace HomeAccounting.Api.Contract.Transactions
{
	public record class CreateTransactionRequest(
		[Required] TransactionType TransactionType,
		DateTime CreateDate,
		[Required] GetCategoryRequest Category,
		[Required] decimal Amount,
		string Title);
}
