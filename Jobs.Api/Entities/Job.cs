namespace Jobs.Api.Entities;

public class Job
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Location { get; set; }
    public DateTime StartDate { get; set; }
    public required string CompanyName { get; set; }
    public required string Recruiter { get; set; }
    public required string RecruiterEmail { get; set; }
    public int YearlySalary { get; set; }
}