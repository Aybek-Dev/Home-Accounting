namespace HomeAccounting.Api.Contract.Categories
{
	public record class GetCategoryRequest(
		Guid Id,
		string Name);
}
