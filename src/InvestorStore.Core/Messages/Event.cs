using System;
using MediatR;

namespace InvestorStore.Core.Messages
{
    public abstract class Event : Message, INotification
    {
        public DateTimeOffset TimeStamp { get; }

        protected Event()
        {
            TimeStamp = DateTimeOffset.Now;
        }
    }
}