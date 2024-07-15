using System.ComponentModel.DataAnnotations;

namespace HomeAccounting.Api.Contract.Categories
{
	public record class UpdateCategoryRequest([Required] string name);
}
