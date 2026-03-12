using FastEndpoints;
using Jobs.Api.Entities;
using Microsoft.AspNetCore.Identity;

namespace Jobs.Api.Features.Auth.Commands;

public class RegisterAdminRequest
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;    
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime Birthdate { get; set; }
}

public class RegisterAdminResponse
{
    public string Message { get; set; } = default!;
}

public class RegisterAdmin : Endpoint<RegisterAdminRequest, RegisterAdminResponse>
{
    private readonly UserManager<AppUser> _userManager;

    public RegisterAdmin(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }


    public override void Configure()
    {
        Post("/auth/admin/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterAdminRequest req, CancellationToken ct)
    {
        var user = new AppUser
        {
            UserName = req.Email,
            Email = req.Email,
            FirstName = req.FirstName,
            LastName = req.LastName,
            Birthdate = req.Birthdate,
        };

        var result = await _userManager.CreateAsync(user, req.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                AddError(error.Description);
            await Send.ErrorsAsync(400, ct);
            return;
        }
        
        await _userManager.AddToRoleAsync(user, Defaults.RoleNames.Admin);

        await Send.ResponseAsync(new RegisterAdminResponse { Message = "Registration successful" }, 201, ct);
    }
}
