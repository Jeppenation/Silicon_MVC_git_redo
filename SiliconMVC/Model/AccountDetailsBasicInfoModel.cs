using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace SiliconMVC.Model
{
    public class AccountDetailsBasicInfoModel
    {

        public string userId { get; set; } = null!;


        [DataType(DataType.ImageUrl)]
        public string? ProfileImage { get; set; }





        [Display(Name = "First Name", Prompt = "Enter your first name", Order = 0)]
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name", Prompt = "Enter your last name", Order = 1)]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = null!;

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address", Prompt = "Enter your email address", Order = 2)]
        [Required(ErrorMessage = "Invalid email address")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email address")]
        public string EmailAddress { get; set; } = null!;

        [Display(Name = "Phone", Prompt = "Enter your phone", Order = 3)]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [Display(Name = "Bio", Prompt = "Add a short bio..", Order = 4)]
        [DataType(DataType.MultilineText)]
        public string? Bio { get; set; }
    }
}
