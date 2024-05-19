using System;
namespace CarWash.Brokers.DateTimes
{
	public interface IDateTimeBroker
	{
        DateTimeOffset GetCurrentDateTime();
    }
}

