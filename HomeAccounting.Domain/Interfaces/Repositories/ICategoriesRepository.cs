using HomeAccounting.Domain.Entities.Categories;

namespace HomeAccounting.Domain.Interfaces.Repositories
{
	public interface ICategoriesRepository
	{
		Task Create(Category category);
		Task Delete(Guid id, Guid userId);
		Task<IList<Category>> GetAll(Guid userId);
		Task<Category> GetById(Guid id,Guid userId);
		Task Update(Guid userId, Guid id, string name,DateTimeOffset updateDate);
	}
}
