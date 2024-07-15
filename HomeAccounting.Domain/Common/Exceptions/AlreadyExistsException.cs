namespace HomeAccounting.Domain.Common.Exceptions
{
	public class AlreadyExistsException : Exception
	{
		public AlreadyExistsException()
		{
		}

		public AlreadyExistsException(string? message, string email) : base(message)
		{
		}

		public AlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
