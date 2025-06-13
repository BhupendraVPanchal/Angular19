using AdminPanelAPI.sql_identifier;
using OTA_HELPER.custom;
using OTA_HELPER.dal;
using Newtonsoft.Json.Linq;
using System.Data;



namespace AdminPanelAPI.BL.task_document
{



    public enum task_document
    {
        adp_task_document_select,
        adp_task_document_insert_or_update,
        adp_task_document_read,
        adp_task_document_delete
    }


    public class task_document_bl
    {
        public task_document_bl()
        { }

        public JObject get_task_document_data(JObject data, _token t)
        {
        var RtnObject =new JObject();
            try
            {
 Dictionary<string, object> para = new Dictionary<string, object>();
 List<string> exclPara = new List<string>();
 data["result_type"] = "1";
 DataTable dtDataCount = _data_access.GetDataTable(_site_config.GetDBConnectionString(t), string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.task_document.adp_task_document_select.ToString()), data, para, exclPara);
 data["result_type"] = "2";
 DataSet dtData = _data_access.GetDataSet(_site_config.GetDBConnectionString(t), string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.task_document.adp_task_document_select.ToString()), data, para, exclPara);
 RtnObject["Data"] = dtData.ToJArray();
 RtnObject["DataCount"] = dtDataCount.Rows[0]["totalRowsCount"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RtnObject;
            }


            public JObject add_update_task_document(JObject data, _token t)
            {
                try
                {
                Dictionary<string, object> para = new Dictionary<string, object>();
                para.Add("login_code", t.user_code);
                bool is_locked = false;
                bool.TryParse(Convert.ToString(data["is_locked"]), out is_locked);
                data["is_locked"] = is_locked ? 1 : 0;
               DataTable ds = _data_access.GetDataTable(_site_config.GetDBConnectionString(t),string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.task_document.adp_task_document_insert_or_update.ToString()), data, para, null);
                return ds.ToJObject();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            }


            public JObject get_task_document(JObject data, _token t)
            {
                try
                {
                Dictionary<string, object> para = new Dictionary<string, object>();
               DataTable ds = _data_access.GetDataTable(_site_config.GetDBConnectionString(t),string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.task_document.adp_task_document_read.ToString()), data, para, null);
                return ds.ToJObject();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            }


            public JObject delete_task_document(JObject data, _token t)
            {
                try
                {
                DataTable dt = new DataTable();
                Dictionary<string, object> para = new Dictionary<string, object>();
                para.Add("login_code", t.user_code);
               dt = _data_access.GetDataTable(_site_config.GetDBConnectionString(t),string.Format("{0}.{1}", DbSchema.dbo, sql_identifier.task_document.adp_task_document_delete.ToString()), data, para, null);
                return dt.ToJObject();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            }


}
}
