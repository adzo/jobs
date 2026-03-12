using FastEndpoints;
using Jobs.Api.Data;
using Jobs.Api.Entities;

public class GetJobRequest
{
    public int Id { get; set; }
}

public class GetJobEndpoint : Endpoint<GetJobRequest, Job>
{
    private readonly JobsDbContext _context;

    public GetJobEndpoint(JobsDbContext context) => _context = context;

    public override void Configure()
    {
        Get("/jobs/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetJobRequest req, CancellationToken ct)
    {
        var job = await _context.Jobs.FindAsync([req.Id], ct);
        if (job is null) { await Send.NotFoundAsync(ct); return; }
        await Send.OkAsync(job, ct);
    }
}