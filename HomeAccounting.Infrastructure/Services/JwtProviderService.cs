using HomeAccounting.Application.Interfaces;
using HomeAccounting.Domain.Entities.Users;
using HomeAccounting.Infrastructure.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HomeAccounting.Infrastructure.Services
{
	public class JwtProviderService : IJwtProvider
	{
		private readonly JwtOptions _options;

		public JwtProviderService(IOptions<JwtOptions> options)
		{
			_options = options.Value;
		}
		public string Generate(User user)
		{
			byte[] convertKeyToBytes =
					Encoding.UTF8.GetBytes(_options.SecretKey);

			var signinCredentials = new SigningCredentials(
				new SymmetricSecurityKey(convertKeyToBytes),
				SecurityAlgorithms.HmacSha256);

			Claim[] claims = [new("userId", user.Id.ToString())];

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.UtcNow.AddHours(_options.ExpiresHours),
				signingCredentials: signinCredentials);

			var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

			return tokenValue;
		}
		public Guid GetUserIdFromToken(string token)
		{
			var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            var key = Encoding.UTF8.GetBytes(_options.SecretKey);
            var parameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            };

            SecurityToken validatedToken;
            var principal = handler.ValidateToken(token, parameters, out validatedToken);
            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == "userId");
            
            return Guid.Parse(userIdClaim?.Value);
		}
	}
}
