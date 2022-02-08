namespace Management.Services.Dtos;

public class FileModel
{
    public string FileName { get; set; }
    public List<IFormFile> FormFile { get; set; }
}