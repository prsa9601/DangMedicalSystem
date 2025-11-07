using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.NotificationAgg.Events
{
    public class AuthNotificationEvent : BaseDomainEvent
    {
        public Guid UserId { get; set; }
    }
}
