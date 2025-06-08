using System.Data;
using Helper_CL.custom;
using Helper_CL.dal;
using Newtonsoft.Json.Linq;

namespace LiteJiraAPI.Business_Logic.authentication
{
    public class signup_bl
    {
        public JObject userSignUp(JObject data)
        {
            JObject rtn_data = new JObject();
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("name", data["name"].ToString());
                parameters.Add("emailid", data["email"].ToString());
                parameters.Add("password", _static_general.EncryptedText(data["password"].ToString()));
                DataTable dt = _data_access.GetDataTable(sql_identifier.portal_login.sp_upsert_projectmember.ToString(), parameters);
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
