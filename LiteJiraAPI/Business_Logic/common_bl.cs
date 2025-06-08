using Helper_CL.custom;
using Helper_CL.dal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Xml;

namespace LJAPI.Business_Logic
{
    public class common_bl
    {
        public common_bl()
        {

        }

        void SetTableNames(DataSet ds)
        {
            ds.Tables[0].TableName = "menu";
            ds.Tables[1].TableName = "submenu";
            ds.Relations.Add(new DataRelation("submenu", new DataColumn[] { ds.Tables[0].Columns["menu_code"] }, new DataColumn[] { ds.Tables[1].Columns["parent_menu"] }));
            ds.Relations[0].Nested = true;
            ds.AcceptChanges();
        }

        public JObject getMenus(_token t)
        {
            JObject RtnObject = new JObject();
            try
            {

                Dictionary<string, object> IncPara = new Dictionary<string, object>();
                IncPara.Add("user_code", t.user_code);
                DataSet dsData = _data_access.GetDataSet("adp_get_menus", IncPara);
                if (dsData.Tables.Count > 0)
                {
                    SetTableNames(dsData);
                    StringWriter sw = new StringWriter();
                    dsData.WriteXml(sw);

                    string xmlString = sw.ToString();
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlString);
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.TypeNameHandling = TypeNameHandling.All;

                    string jsonResult = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented, true);


                    RtnObject["Data"] = jsonResult;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RtnObject;
        }

        public JObject updatestatus_by_table(JObject data, _token t)
        {
            JObject RtnObject = new JObject();
            try
            {
                Dictionary<string, object> IncPara = new Dictionary<string, object>();
                DataTable dsData = _data_access.GetDataTable("adp_updatestatus_by_table", data, IncPara);
                if (dsData.Rows.Count > 0)
                {
                    RtnObject["Data"] = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RtnObject;
        }

        public JObject updatestatus_with_reason_by_table(JObject data, _token t)
        {
            JObject RtnObject = new JObject();
            try
            {
                Dictionary<string, object> IncPara = new Dictionary<string, object>();
                IncPara.Add("user_code", t.user_code);
                DataTable dsData = _data_access.GetDataTable("adp_updatestatus_with_reason_by_table", data, IncPara);
                if (dsData.Rows.Count > 0)
                {
                    RtnObject["Data"] = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RtnObject;
        }
    }
}
