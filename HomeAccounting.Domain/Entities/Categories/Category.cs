using HomeAccounting.Domain.Common.BaseEntities.BaseGuidEntities;
using System.ComponentModel.DataAnnotations;

namespace HomeAccounting.Domain.Entities.Categories
{
	public class Category : BaseGuidAuditableEntity
	{
		private Category() { }
		private Category(Guid userId, Guid id, DateTimeOffset createTime, string name)
		{
			UserId = userId;
			Id = id;
			CreatedDate = createTime;
			Name = name;
		}
		[Required]
		public string Name { get; set; }
		[Required]
		public Guid UserId { get; set; }
		public static Category Create(Guid userId, Guid id, DateTimeOffset createTime, string name) =>
			new(userId, id, createTime, name);
	}
}
