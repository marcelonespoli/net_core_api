using System;
using System.ComponentModel.DataAnnotations;

namespace Conferences.Application.ViewModels
{
    public class ConferenceViewModel
    {
        public ConferenceViewModel()
        {
            Id = Guid.NewGuid();
            Address = new AddressViewModel();
            Category = new CategoryViewModel();
            Address.ConferenceId = Id;
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Minimum size is {1}")]
        [MaxLength(150, ErrorMessage = "Maximum size is {1}")]
        [Display(Name = "Conference Name")]
        public string Name { get; set; }
        
        [Display(Name = "Short Description of Conference")]
        public string ShortDescription { get; set; }
        
        [Display(Name = "Long Description of COnference")]
        public string LongDescription { get; set; }

        [Display(Name = "Start od the Conference")]
        [Required(ErrorMessage = "Date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Of Conference")]
        [Required(ErrorMessage = "The date is required")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Is the conference free?")]
        public bool Free { get; set; }

        [Display(Name = "Value")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency, ErrorMessage = "Money format invalid")]
        public decimal Value { get; set; }
        
        public bool Online { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        public bool Excluded { get; set; }

        public Guid CategoryId { get; set; }
        
        public Guid OrganizerId { get; set; }

        public CategoryViewModel Category { get; set; }
        public AddressViewModel Address { get; set; }

    }
}
