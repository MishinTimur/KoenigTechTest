using System.ComponentModel.DataAnnotations;

namespace KoenigTechTest.Models.AccountViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}