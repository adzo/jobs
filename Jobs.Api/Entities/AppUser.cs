using Microsoft.AspNetCore.Identity;

namespace Jobs.Api.Entities;

public class AppUser: IdentityUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime Birthdate { get; set; }
}