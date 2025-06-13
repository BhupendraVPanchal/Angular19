using AdminPanelAPI.BL.task_document;
using OTA_HELPER.custom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;


namespace AdminPanelAPI.Controllers.task_document
{


public class task_documentController : baseController
{
        task_document_bl _bl = new task_document_bl();


[HttpPost]
public JObject get_task_document_data(JObject data)
{
_token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
return _bl.get_task_document_data(data, t);
}


[HttpPost]
public JObject add_update_task_document(JObject data)
{
_token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
return _bl.add_update_task_document(data, t);
}


[HttpPost]
public JObject get_task_document(JObject data)
{
_token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
return _bl.get_task_document(data, t);
}


[HttpPost]
public JObject delete_task_document(JObject data)
{
_token t = (HttpContext.Items[_site_config.GetConfigValue("login_key")]) as _token;
return _bl.delete_task_document(data, t);
}


}


}
