using FastEndpoints;
using Jobs.Api.Data;
using Jobs.Api.Defaults;
using Microsoft.EntityFrameworkCore;

public class ListApplicationsResponse
{
    public IEnumerable<ApplicationDetail> Applications { get; set; } = [];

    public class ApplicationDetail
    {
        public int Id { get; set; }
        public string ApplicantName { get; set; } = null!;
        public string ApplicantEmail { get; set; } = null!;
        public string JobTitle { get; set; } = null!;
        public int DesiredSalary { get; set; }
        public bool Closed { get; set; }
        public string? ClosingReason { get; set; }
        public IEnumerable<DocumentInfo> Documents { get; set; } = [];
    }

    public class DocumentInfo
    {
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string FilePath { get; set; } = null!;
    }
}

public class ListApplicationsEndpoint : EndpointWithoutRequest<ListApplicationsResponse>
{
    private readonly JobsDbContext _context;

    public ListApplicationsEndpoint(JobsDbContext context) => _context = context;

    public override void Configure()
    {
        Get("/applications");
        Roles(RoleNames.Admin);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var apps = await _context.Applications
            .Include(a => a.Job)
            .Include(a => a.Applicant)
            .Include(a => a.Documents)
            .Select(a => new ListApplicationsResponse.ApplicationDetail
            {
                Id = a.Id,
                ApplicantName = a.Applicant.FirstName + " " + a.Applicant.LastName,
                ApplicantEmail = a.Applicant.Email!,
                JobTitle = a.Job.Title,
                DesiredSalary = a.DesiredSalary,
                Closed = a.Closed,
                ClosingReason = a.ClosingReason,
                Documents = a.Documents.Select(d => new ListApplicationsResponse.DocumentInfo
                {
                    Name = d.Name,
                    Type = d.SymDocumentType.ToString(),
                    FilePath = d.FilePath
                })
            })
            .ToListAsync(ct);

        await Send.OkAsync(new ListApplicationsResponse { Applications = apps }, ct);
    }
}