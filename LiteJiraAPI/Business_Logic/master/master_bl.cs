using Helper_CL.custom;
using Helper_CL.dal;
using Newtonsoft.Json.Linq;
using System.Data;
using static LiteJiraAPI.Business_Logic.sql_identifier;

namespace LiteJiraAPI.Business_Logic.master
{
    public class master_bl
    {


        // For companymaster
        public JObject get_companymaster_data(JObject data, _token t)
        {
            var RtnObject = new JObject();
            try
            {
                Dictionary<string, object> para = new Dictionary<string, object>();
                List<string> exclPara = new List<string>();
                data["result_type"] = "1";
                DataTable dtDataCount = _data_access.GetDataTable(_site_config.GetDBConnectionString(t), string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.companymaster.adp_companymaster_select.ToString()), data, para, exclPara);
                data["result_type"] = "2";
                DataSet dtData = _data_access.GetDataSet(_site_config.GetDBConnectionString(t), string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.companymaster.adp_companymaster_select.ToString()), data, para, exclPara);
                RtnObject["Data"] = dtData.ToJArray();
                RtnObject["DataCount"] = dtDataCount.Rows[0]["totalRowsCount"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RtnObject;
        }
        public JObject add_update_companymaster(JObject data, _token t)
        {
            try
            {
                Dictionary<string, object> para = new Dictionary<string, object>();
                para.Add("login_code", t.user_code);
                bool is_locked = false;
                bool.TryParse(Convert.ToString(data["is_locked"]), out is_locked);
                data["is_locked"] = is_locked ? 1 : 0;
                DataTable ds = _data_access.GetDataTable(_site_config.GetDBConnectionString(t), string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.companymaster.adp_companymaster_insert_or_update.ToString()), data, para, null);
                return ds.ToJObject();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JObject get_companymaster(JObject data, _token t)
        {
            JObject RtnObject = new JObject();
            try
            {
                Dictionary<string, object> para = new Dictionary<string, object>();
                DataTable ds = _data_access.GetDataTable(_site_config.GetDBConnectionString(t), string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.companymaster.adp_companymaster_read.ToString()), data, para, null);
                RtnObject["data"] = ds.ToJArray();
                RtnObject["file_path"] = _site_config.get_ftp_full_path("company");
                return RtnObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JObject delete_companymaster(JObject data, _token t)
        {
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> para = new Dictionary<string, object>();
                para.Add("login_code", t.user_code);
                dt = _data_access.GetDataTable(_site_config.GetDBConnectionString(t), string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.companymaster.adp_companymaster_delete.ToString()), data, para, null);
                return dt.ToJObject();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JObject get_companymaster_help(JObject data, _token t)
        {
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> para = new Dictionary<string, object>();
                dt = _data_access.GetDataTable(_site_config.GetDBConnectionString(t), string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.companymaster.adp_get_companymaster_help.ToString()), data, para, null);
                return dt.ToJObject();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JObject get_designation_help(JObject data, _token t)
        {
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> para = new Dictionary<string, object>();
                dt = _data_access.GetDataTable(_site_config.GetDBConnectionString(t), string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.companymaster.adp_get_designation_help.ToString()), data, para, null);
                return dt.ToJObject();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // For projectmember
        public JObject get_projectmember_data(JObject data, _token t)
        {
            var RtnObject = new JObject();
            try
            {
                Dictionary<string, object> para = new Dictionary<string, object>();
                List<string> exclPara = new List<string>();
                data["result_type"] = "1";
                DataTable dtDataCount = _data_access.GetDataTable(_site_config.GetDBConnectionString(t), string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.projectmember.adp_projectmember_select.ToString()), data, para, exclPara);
                data["result_type"] = "2";
                DataSet dtData = _data_access.GetDataSet(_site_config.GetDBConnectionString(t), string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.projectmember.adp_projectmember_select.ToString()), data, para, exclPara);
                RtnObject["Data"] = dtData.ToJArray();
                RtnObject["DataCount"] = dtDataCount.Rows[0]["totalRowsCount"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RtnObject;
        }
        public JObject add_update_projectmember(JObject data, _token t)
        {
            try
            {
                data["password"] = _static_general.EncryptedText(data["password"].ToString());
                Dictionary<string, object> para = new Dictionary<string, object>();
                para.Add("login_code", t.user_code);
                bool is_locked = false;
                bool.TryParse(Convert.ToString(data["is_locked"]), out is_locked);
                data["is_locked"] = is_locked ? 1 : 0;
                DataTable ds = _data_access.GetDataTable(_site_config.GetDBConnectionString(t), string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.projectmember.adp_projectmember_insert_or_update.ToString()), data, para, null);
                return ds.ToJObject();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JObject get_projectmember(JObject data, _token t)
        {
            JObject RtnObject = new JObject();
            try
            {
                Dictionary<string, object> para = new Dictionary<string, object>();
                DataTable ds = _data_access.GetDataTable(_site_config.GetDBConnectionString(t), string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.projectmember.adp_projectmember_read.ToString()), data, para, null);
                if (ds != null && ds.Rows.Count != 0)
                {
                    ds.Rows[0]["password"] = _static_general.DecryptedText(Convert.ToString(ds.Rows[0]["password"]));
                }
                RtnObject["data"] = ds.ToJArray();
                RtnObject["file_path"] = _site_config.get_ftp_full_path("projectmember");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RtnObject;
        }
        public JObject delete_projectmember(JObject data, _token t)
        {
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> para = new Dictionary<string, object>();
                para.Add("login_code", t.user_code);
                dt = _data_access.GetDataTable(_site_config.GetDBConnectionString(t), string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.projectmember.adp_projectmember_delete.ToString()), data, para, null);
                return dt.ToJObject();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // For projectmaster

        public JObject get_projectmaster_data(JObject data, _token t)
        {
            var RtnObject = new JObject();
            try
            {
                Dictionary<string, object> para = new Dictionary<string, object>();
                List<string> exclPara = new List<string>();
                data["result_type"] = "1";
                DataTable dtDataCount = _data_access.GetDataTable(_site_config.GetDBConnectionString(t), string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.projectmaster.adp_projectmaster_select.ToString()), data, para, exclPara);
                data["result_type"] = "2";
                DataSet dtData = _data_access.GetDataSet(_site_config.GetDBConnectionString(t), string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.projectmaster.adp_projectmaster_select.ToString()), data, para, exclPara);
                RtnObject["Data"] = dtData.ToJArray();
                RtnObject["DataCount"] = dtDataCount.Rows[0]["totalRowsCount"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RtnObject;
        }


        public JObject add_update_projectmaster(JObject data, _token t)
        {
            try
            {
                Dictionary<string, object> para = new Dictionary<string, object>();
                para.Add("login_code", t.user_code);
                bool is_locked = false;
                bool.TryParse(Convert.ToString(data["is_locked"]), out is_locked);
                data["is_locked"] = is_locked ? 1 : 0;
                DataTable ds = _data_access.GetDataTable(_site_config.GetDBConnectionString(t), string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.projectmaster.adp_projectmaster_insert_or_update.ToString()), data, para, null);
                return ds.ToJObject();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public JObject get_projectmaster(JObject data, _token t)
        {
            JObject RtnObject = new JObject();
            try
            {
                Dictionary<string, object> para = new Dictionary<string, object>();
                DataTable ds = _data_access.GetDataTable(_site_config.GetDBConnectionString(t), string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.projectmaster.adp_projectmaster_read.ToString()), data, para, null);
                RtnObject["data"] = ds.ToJArray();
                RtnObject["file_path"] = _site_config.get_ftp_full_path("project");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RtnObject;
        }


        public JObject delete_projectmaster(JObject data, _token t)
        {
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> para = new Dictionary<string, object>();
                para.Add("login_code", t.user_code);
                dt = _data_access.GetDataTable(_site_config.GetDBConnectionString(t), string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.projectmaster.adp_projectmaster_delete.ToString()), data, para, null);
                return dt.ToJObject();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
