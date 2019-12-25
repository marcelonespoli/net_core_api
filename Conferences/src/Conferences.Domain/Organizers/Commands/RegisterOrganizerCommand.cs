using Conferences.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conferences.Domain.Organizers.Commands
{
    public class RegisterOrganizerCommand : Command
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string DocumentId { get; private set; }

        public RegisterOrganizerCommand(Guid id, string name, string email, string documentId)
        {
            Id = id;
            Name = name;
            Email = email;
            DocumentId = documentId;
        }
    }
}
