using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

[NotMapped]
public abstract class BaseEvent : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}