
namespace PatternsLab.Problems.Builder;

public sealed class CourseRegistration
{
    public string StudentEmail { get; }
    public string CourseCode { get; }
    public string AccessMode { get; }
    public string? GroupCode { get; }
    public string? DiscountCode { get; }
    public bool SendWhatsApp { get; }
    public bool SendEmailWelcome { get; }
    public string? MentorNote { get; }
    public DateOnly? PreferredStart { get; }

    private CourseRegistration(Builder builder)
    {
        if (string.IsNullOrWhiteSpace(builder.StudentEmail))
            throw new ArgumentException("email required");

        if (string.IsNullOrWhiteSpace(builder.CourseCode))
            throw new ArgumentException("course required");

        if (builder.AccessMode == "LiveGroup" &&
            string.IsNullOrWhiteSpace(builder.GroupCode))
            throw new InvalidOperationException("LiveGroup requires GroupCode");

        if (builder.AccessMode == "VideosOnly" &&
            !string.IsNullOrWhiteSpace(builder.GroupCode))
            throw new InvalidOperationException("VideosOnly cannot have GroupCode");

        StudentEmail = builder.StudentEmail;
        CourseCode = builder.CourseCode;
        AccessMode = builder.AccessMode;
        GroupCode = builder.GroupCode;
        DiscountCode = builder.DiscountCode;
        SendWhatsApp = builder.SendWhatsApp;
        SendEmailWelcome = builder.SendEmailWelcome;
        MentorNote = builder.MentorNote;
        PreferredStart = builder.PreferredStart;
    }

    public override string ToString()
        => $"{StudentEmail} → {CourseCode} [{AccessMode}] group={GroupCode ?? "-"} discount={DiscountCode ?? "-"} wa={SendWhatsApp} mail={SendEmailWelcome}";

    public sealed class Builder
    {
        public string StudentEmail { get; private set; } = "";
        public string CourseCode { get; private set; } = "";
        public string AccessMode { get; private set; } = "";
        public string? GroupCode { get; private set; }
        public string? DiscountCode { get; private set; }
        public bool SendWhatsApp { get; private set; }
        public bool SendEmailWelcome { get; private set; }
        public string? MentorNote { get; private set; }
        public DateOnly? PreferredStart { get; private set; }

        public Builder WithStudentEmail(string email)
        {
            StudentEmail = email;
            return this;
        }

        public Builder WithCourseCode(string courseCode)
        {
            CourseCode = courseCode;
            return this;
        }

        public Builder WithAccessMode(string accessMode)
        {
            AccessMode = accessMode;
            return this;
        }

        public Builder WithGroupCode(string? groupCode)
        {
            GroupCode = groupCode;
            return this;
        }

        public Builder WithDiscountCode(string? discountCode)
        {
            DiscountCode = discountCode;
            return this;
        }

        public Builder WithWhatsApp(bool sendWhatsApp)
        {
            SendWhatsApp = sendWhatsApp;
            return this;
        }

        public Builder WithEmailWelcome(bool sendEmailWelcome)
        {
            SendEmailWelcome = sendEmailWelcome;
            return this;
        }

        public Builder WithMentorNote(string? mentorNote)
        {
            MentorNote = mentorNote;
            return this;
        }

        public Builder WithPreferredStart(DateOnly? preferredStart)
        {
            PreferredStart = preferredStart;
            return this;
        }

        public CourseRegistration Build()
        {
            return new CourseRegistration(this);
        }
    }
}

public static class RegistrationCallSites
{
    public static CourseRegistration CreateLiveStudentUgly()
    {
        return new CourseRegistration.Builder()
            .WithStudentEmail("sara@mail.com")
            .WithCourseCode("SEF-101")
            .WithAccessMode("LiveGroup")
            .WithGroupCode("G1")
            .WithDiscountCode("EARLY10")
            .WithWhatsApp(true)
            .WithEmailWelcome(true)
            .WithMentorNote("Needs evening slot")
            .WithPreferredStart(new DateOnly(2026, 10, 1))
            .Build();
    }

    public static CourseRegistration CreateVideosOnlyUgly()
    {
        return new CourseRegistration.Builder()
            .WithStudentEmail("ali@mail.com")
            .WithCourseCode("SEF-101")
            .WithAccessMode("VideosOnly")
            .WithGroupCode(null)
            .WithDiscountCode(null)
            .WithWhatsApp(false)
            .WithEmailWelcome(true)
            .WithMentorNote(null)
            .WithPreferredStart(null)
            .Build();
    }
}
