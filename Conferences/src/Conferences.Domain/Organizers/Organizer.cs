using Conferences.Domain.Conferences;
using Conferences.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conferences.Domain.Organizers
{
    public class Organizer : Entity<Organizer>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string DocumentId { get; set; }

        public Organizer(Guid id, string name, string email, string documentId)
        {
            Id = id;
            Name = name;
            Email = email;
            DocumentId = documentId;
        }

        // EF Constructor
        protected Organizer() { }

        // EF Property Navegation
        public virtual ICollection<Conference> Conferences { get; set; }

        public override bool IsValid()
        {
            return true;
        }
    }
}
