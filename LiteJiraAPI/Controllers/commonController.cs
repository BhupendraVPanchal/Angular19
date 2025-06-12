using Helper_CL.custom;
using LiteJiraAPI.Controllers;
using LJAPI.Business_Logic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace LJAPI.Controllers
{
    public class commonController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private common_bl _bl = new common_bl();
        public commonController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        public JObject upload_files(string path)
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
                            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, path, fileName);

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

        [HttpPost]
        public JObject updatestatus_by_table([FromBody] JObject data)
        {
            JObject RtnObj = new JObject();
            _token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
            try
            {

                RtnObj["Data"] = _bl.updatestatus_by_table(data, t);
            }
            catch (Exception ex)
            {

                if (ex.Message.ToString().Contains("UDE-"))
                {
                    RtnObj["Error"] = ex.Message.ToString().Replace("UDE-", "");
                }
                else
                {
                    throw;
                }
            }
            return RtnObj;

        }


        [HttpPost]
        public JObject updatestatus_with_reason_by_table([FromBody] JObject data)
        {
            JObject RtnObj = new JObject();
            _token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
            try
            {

                RtnObj["Data"] = _bl.updatestatus_with_reason_by_table(data, t);
            }
            catch (Exception ex)
            {

                if (ex.Message.ToString().Contains("UDE-"))
                {
                    RtnObj["Error"] = ex.Message.ToString().Replace("UDE-", "");
                }
                else
                {
                    throw;
                }
            }
            return RtnObj;

        }

    }
}
