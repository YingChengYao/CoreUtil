using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreUtil.Controllers
{
    /// <summary>
    /// 上传下载文件例子
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FileUploadController : ControllerBase
    {
        /// <summary>
        /// 单文件上传
        /// </summary>
        /// <param name="iFormFile"></param>
        /// <returns></returns>
        [HttpPost("UploadFile")]
        public async Task<string>UploadFile(IFormFile iFormFile)
        {
            if (iFormFile == null || iFormFile.Length == 0)
                return "No file selected for upload.";

            var filePath = Path.GetTempFileName();//如果这个路径下的临时文件超过 65535 的话将会抛出异常需指定文件夹

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await iFormFile.CopyToAsync(stream);
            }

            return "Sucess";
        }

        /// <summary>
        /// 多文件上传
        /// </summary>
        /// <param name="iFormFiles"></param>
        /// <returns></returns>
        [HttpPost("UploadFiles")]
        public async Task UploadFiles(List<IFormFile> iFormFiles)
        {
            var filePath = Path.GetTempFileName();

            foreach (var iFormFile in iFormFiles)
            {
                if (iFormFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await iFormFile.CopyToAsync(stream);
                    }
                }
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<IActionResult> DownloadFile(string path, string filename)
        {
            if (filename == null || filename.Length == 0)
                return Content("No file selected for download.");

            var filePath = Path.Combine(path, filename);
            var memoryStream = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memoryStream);
            }

            memoryStream.Position = 0;

            return File(memoryStream, "text/plain", Path.GetFileName(path));
        }
    }
}
