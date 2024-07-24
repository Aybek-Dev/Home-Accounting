using HomeAccounting.Api.Contract.Transactions;
using HomeAccounting.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeAccounting.Api.Endpoints
{
	public static class TransactionEndpoints
	{
		public static IEndpointRouteBuilder MapTransactionsEndpoints(this IEndpointRouteBuilder app)
		{
			var endpoints = app.MapGroup("transaction").RequireAuthorization();
			endpoints.MapPost(string.Empty, CreateTransaction);
			endpoints.MapGet(string.Empty, GetTransactions);
			endpoints.MapGet("{id:guid}", GetTransactionById);
			endpoints.MapPost("filter", GetTransactionsByFilter);
			endpoints.MapPut("{id:guid}", UpdateTransaction);
			endpoints.MapDelete("{id:guid}", DeleteTransaction);
			return app;
		}

		private async static Task<IResult> GetTransactionsByFilter(
			HttpContext context,
		    [FromBody] GetFilterTransactionRequest request,
			TransactionService transactionService)
		{
			if (context.Request.Cookies.TryGetValue("tasty-cookies", out string token))
			{
				var transaction = await transactionService.GetTransactionByFiltres(token, request.StartDate, request.EndDate, request.Category.Id, request.TransactionType); ;
				var response = transaction
					.Select(t => new GetTransactionRequest(t.Id, t.CreatedDate.Date, t.Type, t.Category.Name, t.Amount, t.Title));
				return Results.Ok(response);
			}
			throw new Exception("Token not found");
		}

		private async static Task<IResult> DeleteTransaction(
			[FromRoute] Guid id,
			TransactionService transactionService,
			HttpContext context)
		{
			if (context.Request.Cookies.TryGetValue("tasty-cookies", out string token))
			{
				await transactionService.DeleteTransaction(token, id);
				return Results.Ok();
			}
			throw new Exception("Token not found");
		}

		private static async Task<IResult> UpdateTransaction(
		[FromRoute] Guid id,
		[FromBody] UpdateTransactionRequest request,
		TransactionService transactionService,
		HttpContext context)
		{
			if (context.Request.Cookies.TryGetValue("tasty-cookies", out string token))
			{
				await transactionService.Update(token, id, request.TransactionType, request.Category.Id, request.Amount, request.Title);
				return Results.Ok();
			}
			throw new Exception("Token not found");
		}

		private async static Task<IResult> GetTransactionById(
			[FromRoute] Guid id,
			TransactionService transactionService,
			HttpContext context)
		{
			if (context.Request.Cookies.TryGetValue("tasty-cookies", out string token))
			{
				var transaction = await transactionService.GetTransactionById(token, id);
				var response = new GetTransactionRequest(transaction.Id, transaction.CreatedDate.Date, transaction.Type,transaction.Category.Name, transaction.Amount, transaction.Title);
				return Results.Ok(response);
			}
			throw new Exception("Token not found");
		}

		private async static Task<IResult> GetTransactions(
			TransactionService transactionService,
			HttpContext context)
		{
			if (context.Request.Cookies.TryGetValue("tasty-cookies", out string token))
			{
				var transaction = await transactionService.GetAllTransactions(token);
				var response = transaction
					.Select(t => new GetTransactionRequest(t.Id, t.CreatedDate.Date,t.Type, t.Category.Name, t.Amount, t.Title));
				return Results.Ok(response);
			}
			throw new Exception("Token not found");
		}

		private static async Task<IResult> CreateTransaction(
			[FromBody] CreateTransactionRequest request,
			TransactionService transactionService,
			HttpContext context)
		{
         //   await Console.Out.WriteLineAsync($"{request.CreateDate} {request.TransactionType} {request.Category.Id} {request.Amount} {request.Title}");
            if (context.Request.Cookies.TryGetValue("tasty-cookies", out string token))
			{
				await transactionService.CreateTransaction(token,request.CreateDate, request.TransactionType, request.Category.Id, request.Amount, request.Title);
				return Results.Ok();
			}
			throw new Exception("Token not found");

		}
	}
}
