using Conferences.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conferences.Domain.Conferences
{
    public class Category : Entity<Category>
    {
        public string Name { get; private set; }

        // EF Property Navegation
        public virtual ICollection<Conference> Conferences { get; set; }
        
        // EF Constructor
        protected Category() { }


        public Category(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
