using System;
using FluentValidation.Results;
using MediatR;

namespace InvestorStore.Core.Messages
{
    public abstract class Command : Message, IRequest<bool>
    {
        public DateTimeOffset TimeStamp { get; set; }
        public ValidationResult ValidationResult { get; set; }

        public Command()
        {
            TimeStamp = DateTimeOffset.Now;
        }
        
        public virtual bool IsValid()
        {
            throw new InvalidOperationException();
        }
    }
}