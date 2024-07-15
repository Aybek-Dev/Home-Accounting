using System.ComponentModel.DataAnnotations;

namespace HomeAccounting.Api.Contract.Categories
{
	public record class CreateCategoryRequest([Required] string Name);
}
