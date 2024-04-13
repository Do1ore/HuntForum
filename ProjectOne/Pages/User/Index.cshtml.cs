using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectFuse.Areas.Identity.Data;

namespace ProjectFuse.Pages.User
{
    [Authorize(Roles = "admin")]
    public class UserModel : PageModel
    {
        private readonly UserManager<ProjectOneUser> _userManager;


        public UserModel(UserManager<ProjectOneUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty] public List<ProjectOneUser> Users { get; set; } = default!;

        public async Task OnGet()
        {
            Users = await _userManager.Users.ToListAsync();
        }
    }
}