namespace Recommendating.Api.Entities;

public class Message
{
    public enum MessageStatus
    {
        Sent,
        Delivered,
        Read
    }

    public Guid Id { get; set; }
    public User Sender { get; set; }
    public User Receiver { get; set; }
    public string Text { get; set; }
    public MessageStatus Status { get; set; }
    public DateTimeOffset SentDate { get; set; }
}