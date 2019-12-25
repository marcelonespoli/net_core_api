using System;
using System.Collections.Generic;
using System.Text;

namespace Conferences.Domain.Conferences.Commands
{
    public class ExcludeConferenceCommand : BaseConferenceCommand
    {
        public ExcludeConferenceCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
