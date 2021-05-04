using System;
using MediatR;

namespace InvestorStore.Core.Messages.CommonMessages.Notifications
{
    public class DomainNotification : Message, INotification
    {
        public DateTimeOffset TimeStamp { get; private set; }
        public Guid DomainNotificationId { get; private  set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }

        public DomainNotification(string key, string value)
        {
            TimeStamp = DateTimeOffset.Now;
            DomainNotificationId = Guid.NewGuid();
            Version = 1;
            Key = key;
            Value = value;
        }
    }
}