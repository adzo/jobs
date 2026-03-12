using FastEndpoints;
using FastEndpoints.Security;
using Jobs.Api.Data;
using Jobs.Api.Defaults;
using Microsoft.EntityFrameworkCore;

public class MyApplicationsResponse
{
    public IEnumerable<ApplicationSummary> Applications { get; set; } = [];

    public class ApplicationSummary
    {
        public int Id { get; set; }
        public string JobTitle { get; set; } = null!;
        public string Company { get; set; } = null!;
        public int DesiredSalary { get; set; }
        public bool Closed { get; set; }
        public string? ClosingReason { get; set; }
        public IEnumerable<string> Documents { get; set; } = [];
    }
}

public class MyApplicationsEndpoint : EndpointWithoutRequest<MyApplicationsResponse>
{
    private readonly JobsDbContext _context;

    public MyApplicationsEndpoint(JobsDbContext context) => _context = context;

    public override void Configure()
    {
        Get("/applications/mine");
        Roles(RoleNames.Applicant);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var applicantId = User.ClaimValue("userId")!;

        var apps = await _context.Applications
            .Where(a => a.ApplicantId == applicantId)
            .Include(a => a.Job)
            .Include(a => a.Documents)
            .Select(a => new MyApplicationsResponse.ApplicationSummary
            {
                Id = a.Id,
                JobTitle = a.Job.Title,
                Company = a.Job.CompanyName,
                DesiredSalary = a.DesiredSalary,
                Closed = a.Closed,
                ClosingReason = a.ClosingReason,
                Documents = a.Documents.Select(d => d.Name)
            })
            .ToListAsync(ct);

        await Send.OkAsync(new MyApplicationsResponse { Applications = apps }, ct);
    }
}