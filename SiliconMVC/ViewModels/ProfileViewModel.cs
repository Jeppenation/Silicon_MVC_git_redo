namespace SiliconMVC.ViewModels
{
    public partial class AccountDetailsViewModel
    {
        public class ProfileViewModel
        {
            public string FirstName { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public string Email { get; set; } = null!;

            public bool IsExternalAccount { get; set; }

            public string ProfileImage { get; set; } = "Profile-image.svg";
        }


    }
}
