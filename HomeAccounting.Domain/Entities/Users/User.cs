using HomeAccounting.Domain.Common.BaseEntities.BaseGuidEntities;
using HomeAccounting.Domain.Entities.Categories;
using System.Transactions;

namespace HomeAccounting.Domain.Entities.Users
{
	public class User : BaseGuidAuditableEntity
	{
		private User() { }
		public string Name { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }
		public ICollection<Transaction>? Transactions { get; set; }
		public ICollection<Category>? Categories {get;set;}
		private User(Guid id, DateTimeOffset createTime, string name, string passwordHash, string email)
		{
			Id = id;
			CreatedDate = createTime;
			Name = name;
			PasswordHash = passwordHash;
			Email = email;
		}
		public static User Create(Guid id, DateTimeOffset createTime, string name, string passwordHash, string email) =>
			new(id, createTime, name, passwordHash, email);
	}
}
