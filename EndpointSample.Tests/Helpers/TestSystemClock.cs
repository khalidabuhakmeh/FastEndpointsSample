using Microsoft.AspNetCore.Authentication;

namespace EndpointSample.Tests.Helpers;

public class TestSystemClock : ISystemClock
{
    public DateTimeOffset UtcNow { get; set; }
        = DateTimeOffset.UtcNow;

    public void SetTime(int hour, int minutes, int seconds = 0)
    {
        var now = DateTimeOffset.UtcNow.Date;
        UtcNow = new DateTimeOffset(
            now.Year,
            now.Month,
            now.Day,
            hour,
            minutes,
            seconds,
            TimeSpan.Zero
        );
    }
}