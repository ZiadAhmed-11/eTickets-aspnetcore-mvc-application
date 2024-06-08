using eTickets.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace eTickets.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage ="Name Can't be blank.")]
        public string PersonName { get; set; }
        [Required(ErrorMessage = "Email can't be blank.")]
        [EmailAddress(ErrorMessage = "Email Address Not Valid.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone can't be blank.")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Password can't be blank.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage ="Confirm Password can't be blank.")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password and confirm password do not match")]
        public string ConfirmPassword { get; set; }

        public UserTypeOptions UserType { get; set; } = UserTypeOptions.User;
    }
}
