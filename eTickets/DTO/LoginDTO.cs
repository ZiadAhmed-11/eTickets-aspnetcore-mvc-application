using System.ComponentModel.DataAnnotations;

namespace eTickets.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage ="Email Adress can't be blank")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Email is not valid")]
        public string? Email {  get; set; }
        [Required(ErrorMessage ="Passord can't be blank")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
