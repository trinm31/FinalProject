using AutoMapper;
using ICSharpCode.SharpZipLib.Zip;
using Management.Services.Dtos;
using Management.Services.Services.IRepository;
using Management.Services.Utiliy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.Services.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = SD.Admin + "," + SD.Staff)]
public class DownloadsController : ControllerBase
{
   private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IMapper _mapper;

    public DownloadsController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
        _mapper = mapper;
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        var downloads = await _unitOfWork.Download.GetAllAsync();
        return Ok(_mapper.Map<DownloadDto>(downloads));
    }

    [HttpGet("[action]/{id:int}")]
    public async  Task<FileResult> Download(int? id)
    {
        var webRootPath = _webHostEnvironment.WebRootPath;
        string fileZipName;
        try
        {
            var download = await _unitOfWork.Download.GetFirstOrDefaultAsync(s => s.Id == id);
            fileZipName = download.ZipPath;
        }
        catch (Exception)
        {
            fileZipName = "studentQR.zip";
        }

        var fileRoot = webRootPath + "/qr/" + fileZipName + "/";
        var tempOutPut = webRootPath + "/qr/zip/" + fileZipName;

        await using (ZipOutputStream zipOutputStream = new ZipOutputStream(System.IO.File.Create(tempOutPut)))
        {
            zipOutputStream.SetLevel(9);

            byte[] buffer = new byte[4096];

            var qrList = new List<string>();

            foreach (var files in Directory.GetFiles(fileRoot))
            {
                FileInfo info = new FileInfo(files);
                var fileName = Path.GetFileName(info.FullName);
                qrList.Add(webRootPath + "/qr/" + fileZipName + "/" + fileName);
            }

            foreach (var i in qrList)
            {
                ZipEntry entry = new ZipEntry(Path.GetFileName(i));
                entry.DateTime = DateTime.Now;
                entry.IsUnicodeText = true;
                zipOutputStream.PutNextEntry(entry);

                await using (FileStream fileStream = System.IO.File.OpenRead(i))
                {
                    int sourceBytes;
                    do
                    {
                        sourceBytes = fileStream.Read(buffer, 0, buffer.Length);
                        zipOutputStream.Write(buffer, 0, sourceBytes);
                    } while (sourceBytes > 0);
                }
            }
            zipOutputStream.Finish();
            zipOutputStream.Flush();
            zipOutputStream.Close();
        }

        byte[] finalResult = await System.IO.File.ReadAllBytesAsync(tempOutPut);
        if (System.IO.File.Exists(tempOutPut))
        {
            System.IO.File.Delete(tempOutPut);
        }

        if (finalResult == null || !finalResult.Any())
        {
            throw new Exception("Nothing Found");
        }
        fileZipName += ".zip";
        return File(finalResult, "application/zip", fileZipName);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromBody] int id)
    {
        await _unitOfWork.Download.RemoveAsync(id);
        _unitOfWork.Save();
        return NoContent();
    }
}