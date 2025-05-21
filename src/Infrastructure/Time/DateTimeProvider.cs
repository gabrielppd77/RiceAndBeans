using Application.Common.Interfaces.Time;

namespace Infrastructure.Time;

public class DateTimeProvider : IDateTimeProvider
{
	public DateTime UtcNow => DateTime.UtcNow;
}