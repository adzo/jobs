using FastEndpoints;
using Jobs.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Api.Features.Jobs;

public class ListJobsResponse
{
    public IEnumerable<JobListItem> Jobs { get; set; }
    
    public class JobListItem
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Location { get; set; }
        public required string Company { get; set; }
    }
}

public class ListJobs: EndpointWithoutRequest<ListJobsResponse>
{
    private readonly JobsDbContext _context;

    public ListJobs(JobsDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/jobs");
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _context.Jobs
            .Select(x => new ListJobsResponse.JobListItem
            {
                Id = x.Id,
                Title = x.Title,
                Location = x.Location,
                Company = x.CompanyName
            })
            .ToListAsync(ct);

        await Send.OkAsync(new ListJobsResponse()
        {
            Jobs = result
        }, ct);
    }
}