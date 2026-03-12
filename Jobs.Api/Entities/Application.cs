
namespace Jobs.Api.Entities;

public class Application
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public int DesiredSalary { get; set; }
    public virtual ICollection<Document> Documents { get; set; } = [];
    public virtual Job Job { get; set; } = null!;
    public string ApplicantId { get; set; } = null!;
    public virtual AppUser Applicant { get; set; } = null!;
    public bool Closed { get; set; }
    public string ClosingReason { get; set; }
}

public class Document
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string ContentType { get; set; }
    public DocumentType SymDocumentType { get; set; }
    public int ApplicationId { get; set; }
    public required string FilePath { get; set; }
    public virtual Application Application { get; set; }
}

public enum DocumentType
{
    Resume,
    CoverLetter,
    Other
}