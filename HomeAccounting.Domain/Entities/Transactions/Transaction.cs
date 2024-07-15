using HomeAccounting.Domain.Common.BaseEntities.BaseGuidEntities;
using HomeAccounting.Domain.Entities.Categories;
using HomeAccounting.Domain.Entities.Users;
using HomeAccounting.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace HomeAccounting.Domain.Entities.Transactions
{
	public class Transaction : BaseGuidAuditableEntity
	{
		private Transaction(Guid id, Guid userId, Guid categoruId, DateTimeOffset createTime, TransactionType transactionType, decimal amount, string title)
		{
			Id = id;
			UserId = userId;
			CategoryId = categoruId;
			CreatedDate = createTime;
			Type = transactionType;
			Amount = amount;
			Title = title;
		}

		[Required]
		public Guid UserId { get; set; }
		public TransactionType Type { get; set; }
		[Required]
		public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public decimal Amount { get; set; }
		public string? Title { get; set; }
		public static Transaction Create(
			Guid id,
			Guid userId,
			Guid categoruId,
			DateTimeOffset createTime,
			TransactionType transactionType,
			decimal amount,
			string? title) => new(id, userId, categoruId, createTime, transactionType, amount, title);
		private Transaction() { }
	}
}
