using System.Data;
using Helper_CL.custom;
using Helper_CL.dal;
using Newtonsoft.Json.Linq;

namespace LiteJiraAPI.Business_Logic.master
{
    public class companymaster_bl
    {
        public JObject SaveCompanyDetails(JObject data, _token t)
        {
            JObject rtn_data = new JObject();
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("created_by", t.user_code);
                DataTable dt = _data_access.GetDataTable("sp_InsertOrUpdateCompany", data, parameters);
                rtn_data["data"] = dt.ToJArray();
                return rtn_data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JObject GetLocationData(JObject data)
        {
            JObject rtn_data = new JObject();
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                DataTable dt = _data_access.GetDataTable("GetLocationData", data);
                rtn_data["data"] = dt.ToJArray();
                return rtn_data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JObject getCompanyDetails(JObject data)
        {
            JObject rtn_data = new JObject();
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                DataTable dt = _data_access.GetDataTable("GetCompanyDetails", data);
                rtn_data["data"] = dt.ToJArray();
                return rtn_data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
