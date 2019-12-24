using Conferences.Domain.Core.Events;
using MediatR;
using System;

namespace Conferences.Domain.Core.Commands
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
