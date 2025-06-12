using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IO.Compression;
using System.Net;

namespace LJAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class DirectoryController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public DirectoryController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [AllowAnonymous]
        [HttpPost("upload_files")]
        public JObject UploadFiles(string path)
        {
            try
            {
                JObject RtnObject = new JObject();

                path = "wwwroot\\uploads\\" + path;

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var files = Request.Form.Files;

                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var filePath = Path.Combine(_env.ContentRootPath, path, fileName);

                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyTo(fileStream);
                            }
                        }
                    }
                }

                return RtnObject;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("structure")]
        public IActionResult GetDirectoryStructure()
        {
            string rootPath = Path.Combine(_env.WebRootPath);
            if (!Directory.Exists(rootPath))
                return NotFound("wwwroot does not exist.");

            var structure = BuildDirectoryTree(rootPath);
            return Ok(structure);
        }

        [HttpPost("structurepost")]
        public IActionResult GetDirectoryStructure([FromBody] JObject data1)
        {
            var extraFolderPath = data1["extraFolderPath"]?.ToString();
            string extraPath = Path.Combine(_env.WebRootPath, extraFolderPath); // Example subdirectory
            if (!Directory.Exists(extraPath))
                return NotFound("wwwroot does not exist.");

            var structure = BuildDirectoryTree(extraPath);
            return Ok(structure);
        }

        private object BuildDirectoryTree(string path)
        {
            var dirInfo = new DirectoryInfo(path);
            string relativePath = Path.GetRelativePath(_env.WebRootPath, path).Replace("\\", "/");

            return new
            {
                Name = dirInfo.Name,
                RelativePath = relativePath,
                Files = Directory.GetFiles(path).Select(file => new
                {
                    Name = Path.GetFileName(file),
                    PublicUrl = "/" + Path.Combine(relativePath, Path.GetFileName(file)).Replace("\\", "/"),
                }).ToList(),
                Directories = Directory.GetDirectories(path).Select(BuildDirectoryTree).ToList(),

            };

        }

        [HttpPost]
        [Route("createzip")]
        public async Task<IActionResult> CreateZipAsync([FromBody] JObject filePaths)
        {
            try
            {
                if (filePaths == null || filePaths["filePaths"] == null)
                    return BadRequest("No folder path provided for zipping.");

                string relativeFolderPath = filePaths["filePaths"].ToString();

                relativeFolderPath = relativeFolderPath.Replace("/", Path.DirectorySeparatorChar.ToString())
                                                       .Replace("\\", Path.DirectorySeparatorChar.ToString());

                string rootPath = _env.WebRootPath;
                string targetDirectory = Path.Combine(rootPath, relativeFolderPath);
                string normalizedFullPath = Path.GetFullPath(targetDirectory);

                if (!normalizedFullPath.StartsWith(rootPath, StringComparison.OrdinalIgnoreCase))
                    return BadRequest("Invalid folder path.");

                if (!Directory.Exists(normalizedFullPath))
                    return BadRequest($"Directory not found: {relativeFolderPath}");

                var memoryStream = new MemoryStream();

                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var files = Directory.GetFiles(normalizedFullPath, "*.*", SearchOption.AllDirectories);
                    foreach (var file in files)
                    {
                        var relativePathInZip = Path.GetRelativePath(normalizedFullPath, file)
                                                    .Replace("\\", "/");
                        var entry = archive.CreateEntry(relativePathInZip, CompressionLevel.Fastest);
                        using var entryStream = entry.Open();
                        byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(file);
                        await entryStream.WriteAsync(fileBytes, 0, fileBytes.Length);
                    }
                }

                memoryStream.Seek(0, SeekOrigin.Begin);
                return File(memoryStream, "application/zip", "folder-download.zip");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            finally
            {

            }
        }
    }
}
