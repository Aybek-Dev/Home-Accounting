using AutoMapper;
using HomeAccounting.Domain.Entities.Categories;
using HomeAccounting.Domain.Entities.Transactions;
using HomeAccounting.Domain.Entities.Users;
using HomeAccounting.Infrastructure.Entities;

namespace HomeAccounting.Infrastructure.Mappings
{
	public class DataBaseMappings : Profile
	{
		public DataBaseMappings()
		{
			CreateMap<UserEntity, User>();
			CreateMap<CategoryEntity, Category>();
			CreateMap<TransactionEntity, Transaction>();
		}
	}
}
