using System.ComponentModel.DataAnnotations;

namespace eTickets.DTO
{
    public class UserEditDTO
    {
        [Required(ErrorMessage = "Email Adress can't be blank")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Phone can't be blank.")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "Name Can't be blank.")]
        public string? PersonName { get; set; }
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
