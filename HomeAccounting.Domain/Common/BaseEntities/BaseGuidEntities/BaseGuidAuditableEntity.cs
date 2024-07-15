namespace HomeAccounting.Domain.Common.BaseEntities.BaseGuidEntities
{
	public abstract class BaseGuidAuditableEntity : BaseGuidEntity
	{
		public DateTimeOffset CreatedDate { get; set; }
		public DateTimeOffset UpdateDate { get; set; }
	}
}
