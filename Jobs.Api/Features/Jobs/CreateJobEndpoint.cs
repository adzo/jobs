using FastEndpoints;
using Jobs.Api.Data;
using Jobs.Api.Entities;

public class CreateJobRequest
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Location { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public string CompanyName { get; set; } = null!;
    public string Recruiter { get; set; } = null!;
    public string RecruiterEmail { get; set; } = null!;
    public int YearlySalary { get; set; }
}

public class CreateJobEndpoint : Endpoint<CreateJobRequest, Job>
{
    private readonly JobsDbContext _context;

    public CreateJobEndpoint(JobsDbContext context) => _context = context;

    public override void Configure()
    {
        Post("/jobs");
        Roles(Jobs.Api.Defaults.RoleNames.Admin);
    }

    public override async Task HandleAsync(CreateJobRequest req, CancellationToken ct)
    {
        var job = new Job
        {
            Title = req.Title,
            Description = req.Description,
            Location = req.Location,
            StartDate = req.StartDate,
            CompanyName = req.CompanyName,
            Recruiter = req.Recruiter,
            RecruiterEmail = req.RecruiterEmail,
            YearlySalary = req.YearlySalary
        };

        _context.Jobs.Add(job);
        await _context.SaveChangesAsync(ct);

        await Send.CreatedAtAsync<GetJobEndpoint>(new { id = job.Id }, job, cancellation: ct);
    }
}