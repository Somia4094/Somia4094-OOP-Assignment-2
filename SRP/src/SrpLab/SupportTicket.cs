
namespace SrpLab;

/// <summary>
/// Manages support ticket data and coordinates specialized services.
/// </summary>
public sealed class SupportTicket
{
    public string Id { get; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public DateTimeOffset OpenedAt { get; }
    public string Priority { get; private set; } = "P3";

    private readonly TicketPriorityClassifier _priorityClassifier = new();
    private readonly TicketSlaPolicy _slaPolicy = new();
    private readonly PublicReplyFormatter _replyFormatter = new();
    private readonly InternalEscalationFormatter _escalationFormatter = new();

    public SupportTicket(
        string id,
        string subject,
        string body,
        DateTimeOffset openedAt)
    {
        Id = id;
        Subject = subject;
        Body = body;
        OpenedAt = openedAt;

        RecalculatePriorityFromText();
    }

    public void AppendCustomerMessage(string text)
    {
        Body += "\n---\n" + text;
        RecalculatePriorityFromText();
    }

    public void RecalculatePriorityFromText()
    {
        Priority = _priorityClassifier.Classify(Subject, Body);
    }

    public DateTimeOffset SlaDeadline()
    {
        return _slaPolicy.GetDeadline(Priority, OpenedAt);
    }

    public bool IsBreached(DateTimeOffset now)
    {
        return _slaPolicy.IsBreached(SlaDeadline(), now);
    }

    public string DraftPublicReply(string agentName)
    {
        return _replyFormatter.Format(
            Id,
            Priority,
            agentName,
            SlaDeadline());
    }

    public string InternalEscalationBlurb()
    {
        return _escalationFormatter.Format(
            Id,
            Priority,
            SlaDeadline());
    }
}
