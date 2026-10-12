
namespace SrpLab;

public sealed class AppointmentReminderFormatter
{
    public string FormatReminder(
        DateTimeOffset slot,
        string clinicPhone)
    {
        return $"Reminder: appointment {slot:MMM dd HH:mm}. Call {clinicPhone} to reschedule.";
    }
}
