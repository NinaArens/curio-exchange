using System.ComponentModel.DataAnnotations;

namespace CurioExchange.Models
{
    public class ContactModel : BaseModel
    {
        [Display(Name = "Character name")]
        [Required(ErrorMessage = "The character name is required")]
        public string Name { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "The message is required")]
        public string Message { get; set; }
    }
}