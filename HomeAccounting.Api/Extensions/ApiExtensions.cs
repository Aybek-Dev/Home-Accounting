using HomeAccounting.Api.Endpoints;
using HomeAccounting.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HomeAccounting.Api.Extensions
{
	public static class ApiExtensions
	{
		public static void AddMappedEndpoints(this IEndpointRouteBuilder app)
		{
			app.MapUsersEndpoints();
			app.MapCategoriesEndpoints();
			app.MapTransactionsEndpoints();
		}
		public static void AddApiAuthentication(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
				{
					options.TokenValidationParameters = new()
					{
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(
							Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
					};
					options.Events = new JwtBearerEvents
					{
						OnMessageReceived = context =>
						{
							context.Token = context.Request.Cookies["tasty-cookies"];
							return Task.CompletedTask;
						}
					};
				});
			services.AddAuthorization();
		}
	}
}
