using RiceAndBeans.Application.Common.Interfaces.Services;

namespace RiceAndBeans.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
	public DateTime UtcNow => DateTime.UtcNow;
}