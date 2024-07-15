using HomeAccounting.Domain.Entities.Categories;

namespace HomeAccounting.Domain.Interfaces.Services
{
	public interface ICategoryService
	{
		Task CreateCategoryAsync(string token, string name);
		Task DeleteCategoryAsync(string token, Guid id);
		Task<Category> GetCategoryByIdAsync(string token, Guid id);
		Task<IList<Category>> GetAllCategoriesAsync(string token);
		Task UpdateAsync(string token, Guid id, string name);
	}
}
