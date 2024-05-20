using ClinicSystem.Web.Utilities;
using System.ComponentModel.DataAnnotations;

namespace ClinicSystem.Web.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Roles is required.")]
        public Role Roles { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmation password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
