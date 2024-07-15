using HomeAccounting.Application.Interfaces;
using HomeAccounting.Domain.Entities.Categories;
using HomeAccounting.Domain.Interfaces.Repositories;
using HomeAccounting.Domain.Interfaces.Services;

namespace HomeAccounting.Application.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoriesRepository _categoriesRepositiry;
		private readonly IGuidGenerator _guidGenerator;
		private readonly IDateTime _dateTime;
		private readonly IJwtProvider _jwtProvider;

		public CategoryService(
			ICategoriesRepository categoriesRepository,
			IGuidGenerator guidGenerator,
			IDateTime dateTime,
			IJwtProvider jwtProvider)
		{
			_categoriesRepositiry = categoriesRepository;
			_guidGenerator = guidGenerator;
			_dateTime = dateTime;
			_jwtProvider = jwtProvider;
		}
		public async Task CreateCategoryAsync(string token, string name)
		{
			var userId = _jwtProvider.GetUserIdFromToken(token);
			var category = Category.Create(userId, _guidGenerator.Guid, _dateTime.Now, name);
			await _categoriesRepositiry.Create(category);
		}

		public async Task DeleteCategoryAsync(string token, Guid id)
		{
			var userId = _jwtProvider.GetUserIdFromToken(token);
			await _categoriesRepositiry.Delete(id, userId);
		}

		public Task<IList<Category>> GetAllCategoriesAsync(string token)
		{
			var userId = _jwtProvider.GetUserIdFromToken(token);
			var categories = _categoriesRepositiry.GetAll(userId);
			return categories;
		}

		public async Task<Category> GetCategoryByIdAsync(string token, Guid id)
		{
			var userId = _jwtProvider.GetUserIdFromToken(token);
			return await _categoriesRepositiry.GetById(id, userId);
		}

		public async Task UpdateAsync(string token, Guid id, string name)
		{
			var userId = _jwtProvider.GetUserIdFromToken(token);
			await _categoriesRepositiry.Update(userId, id, name, _dateTime.Now);
		}
	}
}
