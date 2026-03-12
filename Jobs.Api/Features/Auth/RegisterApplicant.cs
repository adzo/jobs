using FastEndpoints;
using Jobs.Api.Entities;
using Microsoft.AspNetCore.Identity;

namespace Jobs.Api.Features.Auth.Commands;

public class RegisterApplicantRequest
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;    
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime Birthdate { get; set; }
}

public class RegisterApplicantResponse
{
    public string Message { get; set; } = default!;
}

public class RegisterApplicant : Endpoint<RegisterApplicantRequest, RegisterApplicantResponse>
{
    private readonly UserManager<AppUser> _userManager;

    public RegisterApplicant(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }


    public override void Configure()
    {
        Post("/auth/Applicant/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterApplicantRequest req, CancellationToken ct)
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
        
        await _userManager.AddToRoleAsync(user, Defaults.RoleNames.Applicant);

        await Send.ResponseAsync(new RegisterApplicantResponse { Message = "Registration successful" }, 201, ct);
    }
}