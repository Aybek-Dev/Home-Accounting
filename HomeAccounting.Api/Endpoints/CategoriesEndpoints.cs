using HomeAccounting.Api.Contract.Categories;
using HomeAccounting.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeAccounting.Api.Endpoints
{
	public static class CategoriesEndpoints
	{
		public static IEndpointRouteBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder app)
		{
			var endpiont = app.MapGroup("category").RequireAuthorization();
			endpiont.MapPost(string.Empty, CreateCategory);
			endpiont.MapGet(string.Empty, GetCategories);
			endpiont.MapGet("{id:guid}", GetCategoryById);
			endpiont.MapPut("{id:guid}", UpdateCategory);
			endpiont.MapDelete("{id:guid}", DeleteCategory);
			return app;
		}

		private static async Task<IResult> GetCategoryById(
			[FromRoute] Guid id,
			CategoryService categoryService,
			HttpContext context)
		{
			if (context.Request.Cookies.TryGetValue("tasty-cookies", out string token))
			{
				var category = await categoryService.GetCategoryByIdAsync(token, id);
				var response = new GetCategoryRequest(category.Id, category.Name);
				return Results.Ok(response);
			}
			throw new Exception("Token not found");
		}

		private static async Task<IResult> DeleteCategory(
			[FromRoute] Guid id,
			CategoryService categoryService,
			HttpContext context)
		{
			if (context.Request.Cookies.TryGetValue("tasty-cookies", out string token))
			{
				await categoryService.DeleteCategoryAsync(token, id);
				return Results.Ok();
			}
			throw new Exception("Token not found");
		}

		private static async Task<IResult> UpdateCategory(
			[FromRoute] Guid id,
			[FromBody] UpdateCategoryRequest request,
			CategoryService categoryService,
			HttpContext context)
		{
			if (context.Request.Cookies.TryGetValue("tasty-cookies", out string token))
			{
				await categoryService.UpdateAsync(token, id, request.name);
				return Results.Ok();
			}
			throw new Exception("Token not found");
		}

		private static async Task<IResult> GetCategories(
			HttpContext context,
			CategoryService categoryService)
		{
			if (context.Request.Cookies.TryGetValue("tasty-cookies", out string token))
			{
				var categories = await categoryService.GetAllCategoriesAsync(token);
				var response = categories
					.Select(c => new GetCategoryRequest(c.Id, c.Name));
				return Results.Ok(response);
			}
			throw new Exception("Token not found");

		}

		private static async Task<IResult> CreateCategory(
			[FromBody] CreateCategoryRequest request,
			CategoryService categoryService,
			HttpContext context)
		{
			if (context.Request.Cookies.TryGetValue("tasty-cookies", out string token))
			{
				await categoryService.CreateCategoryAsync(token, request.Name);
				return Results.Ok();
			}
			throw new Exception("Token not found");
		}
	}
}
