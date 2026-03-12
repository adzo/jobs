using FastEndpoints;
using Jobs.Api.Data;
using Jobs.Api.Defaults;

public class DeleteJobRequest
{
    public int Id { get; set; }
}

public class DeleteJobEndpoint : Endpoint<DeleteJobRequest>
{
    private readonly JobsDbContext _context;

    public DeleteJobEndpoint(JobsDbContext context) => _context = context;

    public override void Configure()
    {
        Delete("/jobs/{Id}");
        Roles(RoleNames.Admin);
    }

    public override async Task HandleAsync(DeleteJobRequest req, CancellationToken ct)
    {
        var job = await _context.Jobs.FindAsync([req.Id], ct);
        if (job is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        _context.Jobs.Remove(job);
        await _context.SaveChangesAsync(ct);
        await Send.NoContentAsync(ct);
    }
}