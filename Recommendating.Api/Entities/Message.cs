using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recommendating.Api.Entities;

public class Message
{
    public enum MessageStatus
    {
        Sent,
        Delivered,
        Read
    }

    [Key] public Guid Id { get; set; }
    [ForeignKey("MessageFrom")]public User SenderId { get; set; }
    [ForeignKey("MessageTo")]public User ReceiverId { get; set; }
    public string Text { get; set; }
    public MessageStatus Status { get; set; }
    public DateTimeOffset SentDate { get; set; }
}