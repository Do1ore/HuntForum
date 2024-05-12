namespace ProjectFuse.Models.Forum;

public class UserView
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? MidlleName { get; set; }
    public string? MiddleName { get; set; }
    public bool? HasWeaponLicence { get; set; } = false;
    public int? Age { get; set; }
    
}