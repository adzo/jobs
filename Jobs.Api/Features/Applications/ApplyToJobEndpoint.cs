using FastEndpoints;
using FastEndpoints.Security;
using Jobs.Api.Data;
using Jobs.Api.Defaults;
using Jobs.Api.Entities;
using Jobs.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

public class ApplyToJobRequest
{
    // Route
    public int JobId { get; set; }

    // Form fields
    public int DesiredSalary { get; set; }

    // File uploads — all optional except CV
    public IFormFile? Resume { get; set; }
    public IFormFile? CoverLetter { get; set; }
    public IFormFile? OtherDocument { get; set; }
}

public class ApplyToJobEndpoint : Endpoint<ApplyToJobRequest>
{
    private readonly JobsDbContext _context;
    private readonly IFileStorageService _storage;

    public ApplyToJobEndpoint(JobsDbContext context, IFileStorageService storage)
    {
        _context = context;
        _storage = storage;
    }

    public override void Configure()
    {
        Post("/jobs/{JobId}/apply");
        Roles(RoleNames.Applicant);
        AllowFileUploads();  // enables multipart/form-data
    }

    public override async Task HandleAsync(ApplyToJobRequest req, CancellationToken ct)
    {
        var jobExists = await _context.Jobs.AnyAsync(j => j.Id == req.JobId, ct);
        if (!jobExists) { await Send.NotFoundAsync(ct); return; }

        var applicantId = User.ClaimValue("userId")!;

        var application = new Application
        {
            JobId = req.JobId,
            ApplicantId = applicantId,
            DesiredSalary = req.DesiredSalary,
            Closed = false,
            ClosingReason = string.Empty
        };

        // Upload files and attach documents atomically
        var uploads = new[]
        {
            (File: req.Resume,        Type: DocumentType.Resume),
            (File: req.CoverLetter,   Type: DocumentType.CoverLetter),
            (File: req.OtherDocument, Type: DocumentType.Other),
        };

        foreach (var (file, docType) in uploads)
        {
            if (file is null || file.Length == 0) continue;

            var path = await _storage.SaveAsync(file, ct);

            application.Documents.Add(new Document
            {
                Name = file.FileName,
                ContentType = file.ContentType,
                SymDocumentType = docType,
                FilePath = path
            });
        }

        _context.Applications.Add(application);
        await _context.SaveChangesAsync(ct);

        await Send.CreatedAtAsync<MyApplicationsEndpoint>(
            null,
            new { applicationId = application.Id },
            cancellation: ct);
    }
}