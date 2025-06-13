using Helper_CL.custom;
using LiteJiraAPI.Business_Logic.master;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace LiteJiraAPI.Controllers.master
{

    public class masterController : BaseController
    {
        private master_bl _bl;
        public masterController(master_bl _master)
        {
            _bl = _master;

        }

        [HttpPost]
        public JObject get_companymaster_data(JObject data)
        {
            _token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
            return _bl.get_companymaster_data(data, t);
        }


        [HttpPost]
        public JObject add_update_companymaster(JObject data)
        {
            _token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
            return _bl.add_update_companymaster(data, t);
        }


        [HttpPost]
        public JObject get_companymaster(JObject data)
        {
            _token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
            return _bl.get_companymaster(data, t);
        }


        [HttpPost]
        public JObject delete_companymaster(JObject data)
        {
            _token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
            return _bl.delete_companymaster(data, t);
        }

        [HttpPost]
        public JObject get_companymaster_help(JObject data)
        {
            _token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
            return _bl.get_companymaster_help(data, t);
        }

        [HttpPost]
        public JObject get_designation_help(JObject data)
        {
            _token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
            return _bl.get_designation_help(data, t);
        }


        /// <summary>
        /// For Project Members
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        [HttpPost]
        public JObject get_projectmember_data(JObject data)
        {
            _token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
            return _bl.get_projectmember_data(data, t);
        }


        [HttpPost]
        public JObject add_update_projectmember(JObject data)
        {
            _token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
            return _bl.add_update_projectmember(data, t);
        }


        [HttpPost]
        public JObject get_projectmember(JObject data)
        {
            _token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
            return _bl.get_projectmember(data, t);
        }


        [HttpPost]
        public JObject delete_projectmember(JObject data)
        {
            _token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
            return _bl.delete_projectmember(data, t);
        }


        /// <summary>
        /// 
        [HttpPost]
        public JObject get_projectmaster_data(JObject data)
        {
            _token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
            return _bl.get_projectmaster_data(data, t);
        }


        [HttpPost]
        public JObject add_update_projectmaster(JObject data)
        {
            _token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
            return _bl.add_update_projectmaster(data, t);
        }


        [HttpPost]
        public JObject get_projectmaster(JObject data)
        {
            _token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
            return _bl.get_projectmaster(data, t);
        }


        [HttpPost]
        public JObject delete_projectmaster(JObject data)
        {
            _token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
            return _bl.delete_projectmaster(data, t);
        }




    }
}
