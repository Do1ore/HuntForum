// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectFuse.Areas.Identity.Data;

namespace ProjectFuse.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ProjectOneUser> _userManager;
        private readonly SignInManager<ProjectOneUser> _signInManager;

        public IndexModel(
            UserManager<ProjectOneUser> userManager,
            SignInManager<ProjectOneUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        ///
        [Display(Name = "Никнейм")]
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Номер телефона")]
            public string PhoneNumber { get; set; }
    
            [Range(0, 150)]
            [Display(Name = "Возраст")]
            public int Age { get; set; }
    
            [Display(Name = "Наличие лицензии на оружие")]
            public bool HasWeaponLicence { get; set; }
    
            [StringLength(255, MinimumLength = 2)]
            [Display(Name = "Имя")]
            public string FirstName { get; set; }
    
            [StringLength(255, MinimumLength = 2)]
            [Display(Name = "Фамилия")]  
            public string LastName { get; set; }
    
            [StringLength(255, MinimumLength = 2)]
            [Display(Name = "Отчество")]
            public string MiddleName { get; set; }
    
            [Required]
            [Display(Name = "Адрес")]
            public string Address { get; set; }
        }

        private async Task LoadAsync(ProjectOneUser user)
        {

            Username = user.UserName;
            Input = new InputModel
            {
                PhoneNumber = user.PhoneNumber,
                Age = user.Age,
                Address = user.Address,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                HasWeaponLicence = user.HasWeaponLicence
                
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (user is ProjectOneUser projectOneUser)
            {
                projectOneUser.Age = Input.Age;
                projectOneUser.HasWeaponLicence = Input.HasWeaponLicence;
                projectOneUser.FirstName = Input.FirstName;
                projectOneUser.LastName = Input.LastName;
                projectOneUser.MiddleName = Input.MiddleName;
                projectOneUser.Address = Input.Address;

                var updateResult = await _userManager.UpdateAsync(projectOneUser);
                if (!updateResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to update user data.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
