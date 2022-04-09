using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recommendating.Api.Entities;

public enum MessageStatus
{
    Sent,
    Delivered,
    Read
}

public class Message
{
    [Key] public Guid Id { get; set; }
    [ForeignKey("MessageFrom")] public User Sender { get; set; }
    [ForeignKey("MessageTo")] public User Receiver { get; set; }
    [MaxLength(4096)] public string Text { get; set; }
    public MessageStatus Status { get; set; }
    public DateTimeOffset SentDate { get; set; }
}