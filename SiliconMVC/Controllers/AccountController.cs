using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiliconMVC.Model;
using SiliconMVC.Model.Views;
using SiliconMVC.ViewModels;
using static SiliconMVC.ViewModels.AccountDetailsViewModel;

namespace SiliconMVC.Controllers
{

    //[Authorize]
    public class AccountController(UserManager<UserEntity> userManager, AddressService addressService) : Controller
    {
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly AddressService _addressService = addressService;

        #region Details [HttpGet]
        [Route("/account")]
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var viewModel = new AccountDetailsViewModel();
            viewModel.ProfileInfo = await PopulateProfileInfoAsync();
            viewModel.BasicInfo ??= await PopulateBasicInfoAsync();
            viewModel.AddressInfo ??= await PopulateAddressInfoAsync();


            return View(viewModel);
        }

        #endregion

        #region Details [HttpPost]
        [Route("/account")]
        [HttpPost]
        public async Task<IActionResult> Details(AccountDetailsViewModel viewModel)
        {

            if (viewModel.BasicInfo != null)
            {
                if (viewModel.BasicInfo.FirstName != null && viewModel.BasicInfo.LastName != null && viewModel.BasicInfo.EmailAddress != null)
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user != null)
                    {
                        user.FirstName = viewModel.BasicInfo.FirstName;
                        user.LastName = viewModel.BasicInfo.LastName;
                        user.Email = viewModel.BasicInfo.EmailAddress;
                        user.PhoneNumber = viewModel.BasicInfo.Phone;
                        user.Bio = viewModel.BasicInfo.Bio;

                        var result = await _userManager.UpdateAsync(user);

                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("IncorrectValues", "Failed to update user details");
                            ViewData["ErrorMessage"] = "Failed to update user details";
                        }

                    }
                }
            }

            if (viewModel.AddressInfo != null)
            {
                if (viewModel.AddressInfo.AddressLine_1 != null && viewModel.AddressInfo.PostalCode != null && viewModel.AddressInfo.City != null)
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user != null)
                    {
                        var address = await _addressService.GetAddressAsync(user.Id);
                        if (address != null)
                        {
                            address.StreetName = viewModel.AddressInfo.AddressLine_1;
                            address.PostalCode = viewModel.AddressInfo.PostalCode;
                            address.City = viewModel.AddressInfo.City;

                            var addressResult = await _addressService.UpdateAddressAsync(address);

                            if (!addressResult)
                            {
                                ModelState.AddModelError("IncorrectValues", "Failed to update user details");
                                ViewData["ErrorMessage"] = "Failed to update user details";
                            }
                        }
                        else
                        {
                            address = new AddressEntity
                            {
                                UserId = user.Id,
                                StreetName = viewModel.AddressInfo.AddressLine_1,
                                PostalCode = viewModel.AddressInfo.PostalCode,
                                City = viewModel.AddressInfo.City
                            };

                           var addressResult = await _addressService.CreateAddressAsync(address);

                            if (!addressResult)
                            {
                                ModelState.AddModelError("IncorrectValues", "Failed to create user details");
                                ViewData["ErrorMessage"] = "Failed to create user details";
                            }
                        }

                      


                        var result = await _userManager.UpdateAsync(user);

                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("IncorrectValues", "Failed to update user details");
                            ViewData["ErrorMessage"] = "Failed to update user details";
                        }

                    }
                }
            }

            viewModel.ProfileInfo = await PopulateProfileInfoAsync();
            viewModel.BasicInfo ??= await PopulateBasicInfoAsync();
            viewModel.AddressInfo ??= await PopulateAddressInfoAsync();

            return View(viewModel);
        }
        #endregion






        private async Task<AccountDetailsBasicInfoModel> PopulateBasicInfoAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            return new AccountDetailsBasicInfoModel
            {
                userId = user!.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.Email!,
                Phone = user.PhoneNumber,
                Bio = user.Bio,

            };
        }

        private async Task<ProfileViewModel> PopulateProfileInfoAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            return new ProfileViewModel
            {

                FirstName = user!.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                IsExternalAccount = user.IsExternalAccount,

            };
        }

        private async Task<AccountDetailsAddressInfoModel> PopulateAddressInfoAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var address = await _addressService.GetAddressAsync(user.Id);
                if (address != null)
                {
                    return new AccountDetailsAddressInfoModel
                    {
                        AddressLine_1 = address.StreetName,
                        PostalCode = address.PostalCode,
                        City = address.City
                    };
                }
            }

            return new AccountDetailsAddressInfoModel();
        }

    }
}
