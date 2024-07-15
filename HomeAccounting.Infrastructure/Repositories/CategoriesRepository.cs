using AutoMapper;
using HomeAccounting.Domain.Common.Exceptions;
using HomeAccounting.Domain.Entities.Categories;
using HomeAccounting.Domain.Interfaces.Repositories;
using HomeAccounting.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeAccounting.Infrastructure.Repositories
{
	public class CategoriesRepository : ICategoriesRepository
	{
		private readonly HomeAccountingDbContext _context;
		private readonly IMapper _mapper;

		public CategoriesRepository(
			HomeAccountingDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;

		}
		public async Task Create(Category category)
		{
			var isExist = await _context.Categories
				.AsNoTracking()
				.AnyAsync(c => c.Name == category.Name && c.UserId == category.UserId);
			ValidateCategoryNotExists(isExist, category.Name);
			var categoryEntity = new CategoryEntity()
			{
				Id = category.Id,
				Name = category.Name,
				UserId = category.UserId,
				CreatedDate = category.CreatedDate,
			};
			await _context.Categories.AddAsync(categoryEntity);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(Guid id, Guid userId)
		{
			await GetById(id, userId);
			await _context.Categories
			.Where(c => c.Id == id && c.UserId == userId)
			.ExecuteDeleteAsync();
		}

		public async Task<IList<Category>> GetAll(Guid id)
		{
			var categories = await _context.Categories.
				Where(u => u.UserId == id)
				.AsNoTracking()
				.ToListAsync();
			return _mapper.Map<IList<Category>>(categories);
		}

		public async Task<Category> GetById(Guid id, Guid userId)
		{
			var entity = await _context.Categories
				.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
			ValidateCategoryIsNotNull(entity);
			return _mapper.Map<Category>(entity);
		}

		public async Task Update(Guid userId, Guid id, string name, DateTimeOffset updateDate)
		{
			await GetById(id, userId);
			await _context.Categories
				.Where(c => c.Id == id && c.UserId == userId)
				.ExecuteUpdateAsync(c => c
					.SetProperty(n => n.Name, name)
					.SetProperty(n => n.UpdateDate, updateDate));
		}
		private void ValidateCategoryNotExists(bool isExists, string category)
		{
			if (isExists)
			{
				throw new AlreadyExistsException(nameof(Category), category);
			}
		}
		private void ValidateCategoryIsNotNull(CategoryEntity? category)
		{
			if (category == null)
			{
				throw new NotFoundException(nameof(Category), "such a category will not find");
			}
		}
	}
}
