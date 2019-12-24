using Events.Domain.Core.Events;
using MediatR;
using System;

namespace Events.Domain.Core.Commands
{
    public class Command : Message, IRequest 
    {
        public DateTime Timestamp { get; private set; }

        public Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
