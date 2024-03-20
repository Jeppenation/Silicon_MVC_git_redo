using Infrastructure.Entities;
using SiliconMVC.Model;
using SiliconMVC.Model.Views;

namespace SiliconMVC.ViewModels
{
    public partial class AccountDetailsViewModel
    {
        //public string Title { get; set; } = "Account Details";
        //public UserEntity User { get; set; } = null!;
        public AccountDetailsBasicInfoModel? BasicInfo { get; set; }
        public AccountDetailsAddressInfoModel? AddressInfo { get; set; } 
        public ProfileViewModel? ProfileInfo { get; set; } 





    }
}
