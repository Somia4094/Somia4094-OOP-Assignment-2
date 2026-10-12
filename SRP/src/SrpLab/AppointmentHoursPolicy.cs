
namespace SrpLab;

public sealed class AppointmentHoursPolicy
{
	private readonly TimeOnly _open;
	private readonly TimeOnly _close;
	private readonly int _slotMinutes;

	public AppointmentHoursPolicy(
		TimeOnly open,
		TimeOnly close,
		int slotMinutes)
	{
		_open = open;
		_close = close;
		_slotMinutes = slotMinutes;
	}

	public bool IsWithinBusinessHours(DateTimeOffset when)
	{
		if (when.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday)
			return false;

		var time = TimeOnly.FromDateTime(when.DateTime);

		return time >= _open &&
			   time.AddMinutes(_slotMinutes) <= _close;
	}
}
