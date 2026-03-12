using FastEndpoints;
using FastEndpoints.Security;
using Jobs.Api.Entities;
using Microsoft.AspNetCore.Identity;

namespace Jobs.Api.Features.Auth.Commands;


public class LoginRequest
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class LoginResponse
{
    public string Token { get; set; } = default!;
}

public class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _config;

    public LoginEndpoint(UserManager<AppUser> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    public override void Configure()
    {
        Post("/auth/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var user = await _userManager.FindByEmailAsync(req.Email);

        if (user is null || !await _userManager.CheckPasswordAsync(user, req.Password))
        {
            await Send.UnauthorizedAsync(ct);
            return;
        }

        var roles = await _userManager.GetRolesAsync(user);

        var token = JwtBearer.CreateToken(o =>
        {
            o.SigningKey = _config["Jwt:SigningKey"]!;
            o.ExpireAt = DateTime.UtcNow.AddHours(8);
            o.User.Roles.Add(roles.ToArray());
            o.User.Claims.Add(("userId", user.Id));
            o.User.Claims.Add(("email", user.Email!));
            o.User.Claims.Add(("firstname", user.FirstName));
            o.User.Claims.Add(("lastname", user.LastName));
        });

        await Send.ResponseAsync(new LoginResponse { Token = token }, cancellation: ct);
    }
}