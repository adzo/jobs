using FastEndpoints;
using Jobs.Api.Data;
using Jobs.Api.Defaults;

public class CloseApplicationRequest
{
    // Route
    public int Id { get; set; }

    // Body
    public string Reason { get; set; } = null!;
}

public class CloseApplicationEndpoint : Endpoint<CloseApplicationRequest>
{
    private readonly JobsDbContext _context;

    public CloseApplicationEndpoint(JobsDbContext context) => _context = context;

    public override void Configure()
    {
        Patch("/applications/{Id}/close");
        Roles(RoleNames.Admin);
    }

    public override async Task HandleAsync(CloseApplicationRequest req, CancellationToken ct)
    {
        var application = await _context.Applications.FindAsync([req.Id], ct);

        if (application is null) { await Send.NotFoundAsync(ct); return; }
        if (application.Closed)  { await Send.OkAsync(ct); return; }  // idempotent

        application.Closed = true;
        application.ClosingReason = req.Reason;

        await _context.SaveChangesAsync(ct);
        await Send.NoContentAsync(ct);
    }
}