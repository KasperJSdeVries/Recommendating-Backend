using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recommendating.Messaging.Api.Models;

public enum MessageStatus
{
    Sent,
    Delivered,
    Read
}

public class Message
{
    [Key] public Guid Id { get; set; }
    [ForeignKey("MessageFrom")] public Guid Sender { get; set; }
    [ForeignKey("MessageTo")] public Guid Receiver { get; set; }
    public string Text { get; set; }
    public MessageStatus Status { get; set; }
    public DateTimeOffset SentDate { get; set; }
}