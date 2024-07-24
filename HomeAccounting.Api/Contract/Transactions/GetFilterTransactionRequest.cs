using HomeAccounting.Api.Contract.Categories;
using HomeAccounting.Domain.Enums;

namespace HomeAccounting.Api.Contract.Transactions
{
	public record class GetFilterTransactionRequest(DateTime? StartDate,
		DateTime? EndDate, GetCategoryRequest Category, TransactionType? TransactionType);
}
