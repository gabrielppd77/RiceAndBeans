namespace Contracts.Services.Time;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}