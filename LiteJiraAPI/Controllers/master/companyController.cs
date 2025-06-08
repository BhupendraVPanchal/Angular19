using Helper_CL.custom;
using LiteJiraAPI.Business_Logic.master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace LiteJiraAPI.Controllers.master
{
    
    public class companyController : BaseController
    {
        companymaster_bl _bl = new companymaster_bl();


        [HttpPost]
        public JObject GetLocationData([FromBody] JObject data)
        {
            try
            {
                return _bl.GetLocationData(data);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during insert or update operation.", ex);
            }
        }

        [HttpPost]
        public JObject getCompanyDetails([FromBody] JObject data)
        {
            try
            {
                return _bl.getCompanyDetails(data);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during insert or update operation.", ex);
            }
        }
    }
}
