using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using M_TV_Info.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M_TV_Info.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Display(Name = "Birthday")]
            public DateTime Birthday { get; set; }

            [Display(Name = "Country")]
            public string Country { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var asyncUser = await _userManager.GetUserAsync(User);
            var username = ((User)user).UserName;
            var name = ((User)user).Name;
            var email = ((User)user).Email;
            var birthday = ((User)user).Birthday;
            var country = ((User)user).Country;

            Input = new InputModel
            {
                Username = username,
                Name = name,
                Email = email,
                Birthday = birthday,
                Country = country
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
            var updateUser = false;
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

            if (Input.Username != ((User)user).UserName)
            {
                if (Input.Username == null)
                {
                    StatusMessage = "Username can't be empty.";
                    return RedirectToPage();
                }

                var userNameExists = await _userManager.FindByNameAsync(Input.Username);
                if (userNameExists != null)
                {
                    StatusMessage = "User name already taken. Select a different username.";
                    return RedirectToPage();
                }

                var setUserName = await _userManager.SetUserNameAsync(user, Input.Username);
                if (!setUserName.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set user name.";
                    return RedirectToPage();
                }
                else
                {
                    ((User)user).UserName = Input.Username;
                    updateUser = true;
                }
            }

            if (Input.Name != ((User)user).Name)
            {
                if (Input.Name == null)
                {
                    StatusMessage = "Name can't be empty.";
                    return RedirectToPage();
                }
                else
                { 
                    ((User)user).Name = Input.Name;
                    updateUser = true;
                }
            }

            if (Input.Email != ((User)user).Email)
            {
                if (Input.Email == null)
                {
                    StatusMessage = "Email can't be empty.";
                    return RedirectToPage();
                }

                var emailExists = await _userManager.FindByEmailAsync(Input.Email);
                if (emailExists != null)
                {
                    StatusMessage = "Email already taken. Select a different email.";
                    return RedirectToPage();
                }

                if (Input.Email == null)
                {
                    StatusMessage = "Email can't be empty.";
                    return RedirectToPage();
                }
                else
                {
                    ((User)user).Email = Input.Email;
                    updateUser = true;
                }
            }

            if (Input.Birthday != ((User)user).Birthday)
            {
                ((User)user).Birthday = Input.Birthday;
                updateUser = true;
            }

            if (Input.Country != ((User)user).Country)
            {
                ((User)user).Country = Input.Country;
                updateUser = true;
            }

            if (updateUser)
            {
                await _userManager.UpdateAsync(user);
                StatusMessage = "Your profile has been updated!";
            }
            else
            {
                StatusMessage = "Nothing changed.";
            }

            await _signInManager.RefreshSignInAsync(user);
            return RedirectToPage();
        }
    }
}
