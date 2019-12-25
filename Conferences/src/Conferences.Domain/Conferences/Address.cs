using Conferences.Domain.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conferences.Domain.Conferences
{
    public class Address : Entity<Address>
    {
        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string Address3 { get; private set; }
        public string Number { get; private set; }
        public string Postcode { get; private set; }
        public string City { get; private set; }
        public string County { get; private set; }        
        public Guid? ConferenceId { get; private set; }

        // EF Property Navegation
        public virtual Conference Conference { get; private set; }

        // EF Contructor
        protected Address() { }

        public Address(Guid id, string address1, string address2, string address3, string number, string postcode, string city, string county, Guid? conferenceId)
        {
            Id = id;
            Address1 = address1;
            Address2 = address2;
            Address3 = address3;
            Number = number;
            Postcode = postcode;
            City = city;
            County = county;
            ConferenceId = conferenceId;
        }

        public override bool IsValid()
        {
            RuleFor(r => r.Address1)
                .NotEmpty().WithMessage("Address is required")
                .Length(2, 150).WithMessage("Address needs to be between 2 and 150 characters");

            RuleFor(r => r.Number)
                .NotEmpty().WithMessage("Number is required")
                .Length(1, 10).WithMessage("Number needs to be between 1 and 10 characteres");

            RuleFor(r => r.Postcode)
                .NotEmpty().WithMessage("Postcode is required")
                .MinimumLength(5).WithMessage("Postcode needs to have at least 5 characters");

            RuleFor(r => r.City)
                .NotEmpty().WithMessage("City is required")
                .Length(2, 150).WithMessage("City needs to be between 2 and 150 characters");

            RuleFor(r => r.County)
                .NotEmpty().WithMessage("County is required")
                .Length(2, 150).WithMessage("County needs to be between 2 and 150 characters");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
