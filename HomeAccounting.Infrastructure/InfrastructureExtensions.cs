using HomeAccounting.Application.Interfaces;
using HomeAccounting.Domain.Interfaces.Repositories;
using HomeAccounting.Infrastructure.Repositories;
using HomeAccounting.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomeAccounting.Infrastructure
{
	public static class InfrastructureExtensions
	{
		public static IServiceCollection AddInfrastructure(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddDbContext<HomeAccountingDbContext>(options =>
			{
				options.UseNpgsql(configuration.GetConnectionString(nameof(HomeAccountingDbContext)));
			});
			services.AddScoped<IUsersRepository, UsersRepository>();
			services.AddScoped<ICategoriesRepository, CategoriesRepository>();
			services.AddScoped<ITransactionsRepository, TransactionsRepository>();
			services.AddScoped<IJwtProvider, JwtProviderService>();
			services.AddScoped<IDateTime, DateTimeService>();
			services.AddScoped<IGuidGenerator, GuidGeneratorService>();
			services.AddScoped<IPasswordHasher, PasswordHasherService>();

			return services;
		}
	}
}
