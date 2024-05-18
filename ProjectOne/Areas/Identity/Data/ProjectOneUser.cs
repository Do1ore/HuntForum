using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ProjectFuse.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ProjectOneUser class
public class ProjectOneUser : IdentityUser
{
    public int Age { get; set; }
    public bool HasWeaponLicence { get; set; }
    [Length(2, 255)]
    public string? FirstName { get; set; }
    [Length(2, 255)]
    public string? LastName { get; set; }
    
    [Length(2, 255)]
    public string? MiddleName { get; set; }
    
    public string? Address { get; set; }
    
}

