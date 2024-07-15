using System.ComponentModel.DataAnnotations;

namespace HomeAccounting.Api.Contract.Users
{
	public record class LoginUserRequest(
		[Required] string Email,
		[Required] string Password);
}
