using AdminPanelAPI.BL.task;
using OTA_HELPER.custom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;


namespace AdminPanelAPI.Controllers.task
{


public class taskController : baseController
{
        task_bl _bl = new task_bl();


[HttpPost]
public JObject get_task_data(JObject data)
{
_token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
return _bl.get_task_data(data, t);
}


[HttpPost]
public JObject add_update_task(JObject data)
{
_token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
return _bl.add_update_task(data, t);
}


[HttpPost]
public JObject get_task(JObject data)
{
_token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
return _bl.get_task(data, t);
}


[HttpPost]
public JObject delete_task(JObject data)
{
_token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
return _bl.delete_task(data, t);
}


}


}
