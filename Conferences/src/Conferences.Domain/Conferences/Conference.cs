using Conferences.Domain.Core.Models;
using Conferences.Domain.Organizers;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace Conferences.Domain.Conferences
{
    public class Conference : Entity<Conference>
    {
        public Conference(
            string nome,
            DateTime startDate,
            DateTime endDate,
            bool free,
            decimal value,
            bool online,
            string companyName)
        {
            Id = Guid.NewGuid();
            Name = nome;
            StartDate = startDate;
            EndDate = endDate;
            Free = free;
            Value = value;
            Online = online;
            CompanyName = companyName;
        }

        private Conference() { }

        public string Name { get; private set; }
        public string ShortDescription { get; private set; }
        public string LongDescription { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public bool Free { get; private set; }
        public decimal Value { get; private set; }
        public bool Online { get; private set; }
        public string CompanyName { get; private set; }
        public bool Excluded { get; private set; }
        public ICollection<Tags> Tags { get; private set; }

        public Guid? CategoryId { get; private set; }
        public Guid? AddressId { get; private set; }
        public Guid? OrganizerId { get; private set; }

        // EF Property Navegation
        public virtual Category Category { get; private set; }
        public virtual Address Address { get; private set; }
        public virtual Organizer Organizer { get; private set; }


        public void AddAddress(Address address)
        {
            if (!address.IsValid()) return;
            Address = address;
        }

        public void AddCategory(Category category)
        {
            if (!category.IsValid()) return;
            Category = category;
        }

        public void ExcludeConference()
        {
            // TODO: should validate something?
            Excluded = true;
        }

        public void SwitchToPresencial()
        {
            // TODO: Apply some business validation
            Online = false;
        }

        public override bool IsValid()
        {
            RunValidations();
            return ValidationResult.IsValid;
        }

        #region Validations

        private void RunValidations()
        {
            ValidateName();
            ValidateValue();
            ValidateDate();
            ValidateLocal();
            ValidateCompanyName();

            ValidationResult = Validate(this);

            // Adicional validates
            ValidateAddress();
        }

        private void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("OName is required")
                .Length(2, 150).WithMessage("The name needs to be between 2 and 150 characters");
        }

        private void ValidateValue()
        {
            if (!Free)
                RuleFor(r => r.Value)
                    .ExclusiveBetween(1, 50000)
                    .WithMessage("The value should be between 1.00 and 50.000");

            if (Free)
                RuleFor(r => r.Value)
                    .Equal(0).When(v => v.Free)
                    .WithMessage("The value should be 0 for a free conference");
        }

        private void ValidateDate()
        {
            RuleFor(r => r.StartDate)
                .LessThan(r => r.EndDate)
                .WithMessage("The Start Date should be less than the End Date");

            RuleFor(r => r.StartDate)
                .GreaterThan(DateTime.Now)
                .WithMessage("The Start Date should be greater than current date");
        }

        private void ValidateLocal()
        {
            if (Online)
                RuleFor(r => r.Address)
                    .Null().When(r => r.Online)
                    .WithMessage("The conference should not have an address if it is online");

            if (!Online)
                RuleFor(r => r.Address)
                    .NotNull().When(r => r.Online == false)
                    .WithMessage("The conference should have an address");
        }

        private void ValidateCompanyName()
        {
            RuleFor(r => r.CompanyName)
                .NotEmpty().WithMessage("The Company Name is required")
                .Length(2, 150).WithMessage("The Company Name needs to be between 2 and 150 characters");
        }

        private void ValidateAddress()
        {
            if (Online) return;
            if (Address.IsValid()) return;

            foreach (var error in Address.ValidationResult.Errors)
            {
                ValidationResult.Errors.Add(error);
            }
        }

        #endregion


        public static class ConferenceFactory
        {
            public static Conference NewConferenceComplety(Guid id, string name, string shortDescription, string longDescription, 
                DateTime startDate, DateTime endDate, bool free, decimal value, bool online, string companyName, 
                Guid? organizerId, Address address, Guid categoryId)
            {
                var conference = new Conference
                {
                    Id = id,
                    Name = name,
                    ShortDescription = shortDescription,
                    LongDescription = longDescription,
                    StartDate = startDate,
                    EndDate = endDate,
                    Free = free,
                    Value = value,
                    Online = online,
                    CompanyName = companyName,
                    Address = address,
                    AddressId = address?.Id,
                    CategoryId = categoryId
                };

                if (organizerId.HasValue)
                    conference.OrganizerId = organizerId.Value;

                if (online)
                {
                    conference.Address = null;
                    conference.AddressId = null;
                }

                return conference;
            }
        }
    }
}
