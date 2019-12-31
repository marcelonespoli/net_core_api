using System;
using System.ComponentModel.DataAnnotations;

namespace Conferences.Application.ViewModels
{
    public class AddressViewModel
    {
        public AddressViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public string Address1 { get; set; }
        
        public string Address2 { get; set; }
        
        public string Address3 { get; set; }
        
        public string Number { get; set; }
        
        public string Postcode { get; set; }
        
        public string City { get; set; }
        
        public string County { get; set; }
        
        public Guid ConferenceId { get; set; }

        public override string ToString()
        {
            return Address1 + ", " + Address2 + ", " + Address3 + ", " + Number + ", " + City + ", " + County;
        }
    }
}
