using Helper_CL.custom;
using Helper_CL.dal;
using Newtonsoft.Json.Linq;
using System.Data;

namespace LiteJiraAPI.Business_Logic.authentication
{
    public class signin_bl
    {
        public JObject userSignIn(JObject data)
        {
            JObject rtn_data = new JObject();
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("user_name", data["username"].ToString());
                parameters.Add("password", _static_general.EncryptedText(data["password"].ToString()));
                DataTable dt = _data_access.GetDataTable(sql_identifier.portal_login.sp_usersignIn.ToString(), parameters);

                if (dt.Rows.Count > 0)
                {
                    _token t = new _token();
                    DataRow dr = dt.Rows[0];
                    t.session_code = Convert.ToInt32(dr["sessionid"]);
                    t.user_code = Convert.ToInt32(dr["userid"]);
                    t.user_name = dr["user_name"] != DBNull.Value ? dr["user_name"].ToString() : "";
                    t.expiry_date = dr["token_expiry"] != DBNull.Value ? Convert.ToDateTime(dr["token_expiry"]) : DateTime.Now.AddMinutes(30);
                    string strEncToken = t.GetTokenSerialized();
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("userid", Convert.ToInt32(dr["userid"]));
                    param.Add("sessionid", Convert.ToInt32(dr["sessionid"]));
                    param.Add("token", strEncToken);
                    _data_access.GetDataTable(sql_identifier.portal_login.sp_update_sessioninfo_token.ToString(), param);
                    dr["Token"] = strEncToken;
                }

                rtn_data["data"] = dt.ToJArray();

                return rtn_data;
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred during login.", ex);
            }
        }

    }
}
