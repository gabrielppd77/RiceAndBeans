namespace Application.Common.Interfaces.Time;

public interface IDateTimeProvider
{
	DateTime UtcNow { get; }
}