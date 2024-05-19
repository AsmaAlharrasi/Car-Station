using System;
namespace CarWash.Brokers.DateTimes
{
	public class DateTimeBroker: IDateTimeBroker
    {
        public DateTimeOffset GetCurrentDateTime() => DateTimeOffset.UtcNow;

    }
}

