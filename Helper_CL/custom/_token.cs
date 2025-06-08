using Helper_CL.dal;
using System.Data;

namespace Helper_CL.custom
{
    public class _token
    {
        #region "Variable,Properties,Constructor"
        string separator = "$#%";
        string str_err = "";

        public int user_code { get; set; }
        public string? user_name { get; set; }
        public string? user_mobile { get; set; }
        public int session_code { get; set; }
        public DateTime expiry_date { get; set; }


        public _token()
        {

        }

        public _token(string SerializeString)
        {
            SerializeString = _static_general.DecryptedText(SerializeString);
            string[] str = SerializeString.Split(new string[] { separator }, StringSplitOptions.None);
            user_code = Convert.ToInt32(str[0]);
            user_name = str[1];
            user_mobile = str[2];
            session_code = Convert.ToInt32(str[3]);
            expiry_date = Convert.ToDateTime(str[4]);
        }
        #endregion

        #region "Helpers"
        public string GetTokenSerialized()
        {
            return _static_general.EncryptedText(user_code + separator + user_name + separator + user_mobile + separator + session_code + separator + expiry_date);
        }
        public string GetError()
        {
            return str_err;
        }

        #endregion

        public bool IsValid()
        {
            try
            {
                Dictionary<string, object> inc_para = new Dictionary<string, object>();
                inc_para.Add("user_code", user_code);
                inc_para.Add("token", GetTokenSerialized());
                DataTable dt = _data_access.GetDataTable("sp_validate_token", inc_para);

                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    str_err = "Invalid Token...";
                    return false;
                }
            }
            catch (Exception ex)
            {
                str_err = "Invalid Credentials";
                return false;
            }
        }

    }
}
