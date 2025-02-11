using Usuario.Application.Abstractions.Time;

namespace Usuario.Infrastructure.Abstractions.Time;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime CurrentTime => DateTime.UtcNow;
}