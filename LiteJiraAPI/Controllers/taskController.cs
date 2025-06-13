using Helper_CL.custom;
using LiteJiraAPI.Controllers;
using LJAPI.Business_Logic.task;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace LJAPI.Controllers
{
    public class taskController : BaseController
    {
        private task_bl _bl;

        public taskController(task_bl bl)
        {
            _bl = bl;
        }


        [HttpPost]
        public JObject get_projectmaster_help(JObject data)
        {
            _token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
            return _bl.get_projectmaster_help(data, t);
        }


        [HttpPost]
        public JObject get_tasktype_help(JObject data)
        {
            _token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
            return _bl.get_tasktype_help(data, t);
        }


        [HttpPost]
        public JObject get_prioritymaster_help(JObject data)
        {
            _token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
            return _bl.get_prioritymaster_help(data, t);
        }

        [HttpPost]
        public JObject get_tagmaster_help(JObject data)
        {
            _token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
            return _bl.get_tagmaster_help(data, t);
        }

        [HttpPost]
        public JObject get_task_data(JObject data)
        {
            _token t = HttpContext.Items[_site_config.GetConfigValue("login_key")] as _token;
            return _bl.get_task_data(data, t);
        }


        [HttpPost]
        public JObject add_update_task(JObject data)
        {
            _token t = HttpContext.Items[_site_config.GetConfigValue("login_key")] as _token;
            return _bl.add_update_task(data, t);
        }


        [HttpPost]
        public JObject get_task(JObject data)
        {
            _token t = HttpContext.Items[_site_config.GetConfigValue("login_key")] as _token;
            return _bl.get_task(data, t);
        }


        [HttpPost]
        public JObject delete_task(JObject data)
        {
            _token t = HttpContext.Items[_site_config.GetConfigValue("login_key")] as _token;
            return _bl.delete_task(data, t);
        }
    }
}
