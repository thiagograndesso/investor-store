using System;
using System.Threading;
using System.Threading.Tasks;
using InvestorStore.Core.Messages;
using MediatR;

namespace InvestorStore.Sales.Application.Commands
{
    public class OrderCommandHandler : IRequestHandler<AddOrderItemCommand, bool>
    {
        public async Task<bool> Handle(AddOrderItemCommand command, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(command))
            {
                return false;
            }

            return true;
        }

        private bool ValidateCommand(Command command)
        {
            if (command.IsValid())
            {
                return true;
            }

            foreach (var error in command.ValidationResult.Errors)
            {
                // Throw event error
            }

            return false;
        }
    }
}