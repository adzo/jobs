namespace Jobs.Api.Services;

public interface IFileStorageService
{
    Task<string> SaveAsync(IFormFile file, CancellationToken ct);
}

public class FileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _env;

    public FileStorageService(IWebHostEnvironment env) => _env = env;

    public async Task<string> SaveAsync(IFormFile file, CancellationToken ct)
    {
        var folder = Path.Combine(_env.WebRootPath, "uploads");
        Directory.CreateDirectory(folder);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var fullPath = Path.Combine(folder, fileName);

        await using var stream = File.Create(fullPath);
        await file.CopyToAsync(stream, ct);

        return $"uploads/{fileName}";
    }
}