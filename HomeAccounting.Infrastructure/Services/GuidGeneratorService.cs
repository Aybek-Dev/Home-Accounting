using HomeAccounting.Application.Interfaces;

namespace HomeAccounting.Infrastructure.Services
{
	public class GuidGeneratorService : IGuidGenerator
	{
		public Guid Guid => Guid.NewGuid();
	}
}
