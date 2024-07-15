using HomeAccounting.Api.Extensions;
using HomeAccounting.Application.Services;
using HomeAccounting.Infrastructure;
using HomeAccounting.Infrastructure.Authentication;
using HomeAccounting.Infrastructure.Mappings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;

namespace HomeAccounting.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var services = builder.Services;
			var configuration = builder.Configuration;
			// Add services to the container.

			services.AddControllers();

			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			services.AddApiAuthentication(configuration);
			services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
			services.Configure<AuthorizationOptions>(configuration.GetSection(nameof(AuthorizationOptions)));

			services.AddScoped<UserService>();
			services.AddScoped<CategoryService>();
			services.AddScoped<TransactionService>();

			services
				.AddInfrastructure(configuration);
			services.AddAutoMapper(typeof(DataBaseMappings));

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseHttpsRedirection();

			app.UseCookiePolicy(new CookiePolicyOptions
			{
				MinimumSameSitePolicy = SameSiteMode.Strict,
				HttpOnly = HttpOnlyPolicy.Always,
				Secure = CookieSecurePolicy.Always
			});

			app.UseAuthentication();
			app.UseAuthorization();

			app.AddMappedEndpoints();

			app.Run();
		}
	}
}
