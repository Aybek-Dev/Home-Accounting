using System.ComponentModel.DataAnnotations;

namespace HomeAccounting.Api.Contract.Users
{
	public record class RegisterUserRequest(
	[Required] string UserName,
	[Required] string Password,
	[Required] string Email);
}
