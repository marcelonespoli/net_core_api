using System;
using System.ComponentModel.DataAnnotations;

namespace Conferences.Application.ViewModels
{
    public class OrganizerViewModel 
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The em-mail is required")]
        [EmailAddress(ErrorMessage = "E-mail format is invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Document ID is required")]
        [StringLength(30)]
        public string DocumentId { get; set; }
    }
}
