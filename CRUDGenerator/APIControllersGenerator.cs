using System.Text;

namespace OTAGenerator
{
    public static class APIControllersGenerator
    {





        public static Tuple<string, string, Dictionary<string, string>> generate_api_bl(string entity_name, Dictionary<string, string> storeprocedurename, string schema = "dbo")
        {
            string bl_name = $"{entity_name}_bl";
            Dictionary<string, string> dic_storeprocedurename = new Dictionary<string, string>();
            StringBuilder api_controllers_Builder = new StringBuilder();
            if (storeprocedurename == null) { storeprocedurename = new Dictionary<string, string>(); }

            // To Create BL File For the Given {entity_name}

            api_controllers_Builder.AppendLine($"using AdminPanelAPI.sql_identifier;");
            api_controllers_Builder.AppendLine($"using OTA_HELPER.custom;");
            api_controllers_Builder.AppendLine($"using OTA_HELPER.dal;");
            api_controllers_Builder.AppendLine($"using Newtonsoft.Json.Linq;");
            api_controllers_Builder.AppendLine($"using System.Data;");
            api_controllers_Builder.AppendLine("\n\n");
            api_controllers_Builder.AppendLine($"namespace AdminPanelAPI.BL.{entity_name}");
            api_controllers_Builder.AppendLine("{");
            api_controllers_Builder.AppendLine("\n\n");

            // To Declare Enum
            api_controllers_Builder.AppendLine($"    public enum {entity_name}");
            api_controllers_Builder.AppendLine("    {");
            foreach (string key in storeprocedurename.Keys)
            {
                api_controllers_Builder.AppendLine($"        {storeprocedurename[key]}{((storeprocedurename.LastOrDefault().Key != key) ? "," : "")}");
                //if (storeprocedurename.LastOrDefault().Key != key)
                //{
                //    api_controllers_Builder.Append($",");
                //}
            }
            api_controllers_Builder.AppendLine("    }");
            api_controllers_Builder.AppendLine("\n");

            api_controllers_Builder.AppendLine($"    public class {bl_name}");
            api_controllers_Builder.AppendLine("    {");
            api_controllers_Builder.AppendLine($"        public {bl_name}()");
            api_controllers_Builder.Append("        { }");
            api_controllers_Builder.AppendLine("\n");

            // To Get {entity_name} Data
            if (storeprocedurename.ContainsKey(UtilityScript.view_key))
            {
                dic_storeprocedurename.Add(UtilityScript.view_key, $"get_{entity_name}_data");
                api_controllers_Builder.AppendLine($"        public JObject get_{entity_name}_data(JObject data, _token t)");
                api_controllers_Builder.AppendLine("        {");
                api_controllers_Builder.AppendLine("        var RtnObject =new JObject();");
                api_controllers_Builder.AppendLine("            try");
                api_controllers_Builder.AppendLine("            {");
                //api_controllers_Builder.AppendLine("                DataTable dt = new DataTable();");
                //api_controllers_Builder.AppendLine("                DataSet ds = new DataSet();");
                //api_controllers_Builder.AppendLine("                Dictionary<string, object> para = new Dictionary<string, object>();");
                ////api_controllers_Builder.AppendLine("                para.Add(\"login_code\", t.UserCode);");
                //api_controllers_Builder.AppendLine($"               ds = _data_access.GetDataSet(_site_config.GetDBConnectionString(t), sql_identifier.sql_identifier.{entity_name}.{storeprocedurename[UtilityScript.view_key]}.ToString(), data, para, null);");
                //api_controllers_Builder.AppendLine("                return ds.ToJObject();");
                api_controllers_Builder.AppendLine(" Dictionary<string, object> para = new Dictionary<string, object>();");
                api_controllers_Builder.AppendLine(" List<string> exclPara = new List<string>();");
                api_controllers_Builder.AppendLine(" data[\"result_type\"] = \"1\";");
                api_controllers_Builder.AppendLine($" DataTable dtDataCount = _data_access.GetDataTable(_site_config.GetDBConnectionString(t), string.Format(\"{{0}}.{{1}}\", DbSchema.{schema}, sql_identifier.{entity_name}.{storeprocedurename[UtilityScript.view_key]}.ToString()), data, para, exclPara);");
                api_controllers_Builder.AppendLine(" data[\"result_type\"] = \"2\";");
                api_controllers_Builder.AppendLine($" DataSet dtData = _data_access.GetDataSet(_site_config.GetDBConnectionString(t), string.Format(\"{{0}}.{{1}}\", DbSchema.{schema}, sql_identifier.{entity_name}.{storeprocedurename[UtilityScript.view_key]}.ToString()), data, para, exclPara);");
                api_controllers_Builder.AppendLine(" RtnObject[\"Data\"] = dtData.ToJArray();");
                api_controllers_Builder.AppendLine(" RtnObject[\"DataCount\"] = dtDataCount.Rows[0][\"totalRowsCount\"].ToString();");
                api_controllers_Builder.AppendLine("            }");
                api_controllers_Builder.AppendLine("            catch (Exception ex)");
                api_controllers_Builder.AppendLine("            {");
                api_controllers_Builder.AppendLine("                throw ex;");
                api_controllers_Builder.AppendLine("            }");
                api_controllers_Builder.AppendLine("            return RtnObject;");
                api_controllers_Builder.AppendLine("            }");
            }

            api_controllers_Builder.AppendLine("\n");
            // To Insert & Update {entity_name} Data
            if (storeprocedurename.ContainsKey(UtilityScript.create_update_key))
            {
                dic_storeprocedurename.Add(UtilityScript.create_update_key, $"add_update_{entity_name}");
                api_controllers_Builder.AppendLine($"            public JObject add_update_{entity_name}(JObject data, _token t)");
                api_controllers_Builder.AppendLine("            {");
                api_controllers_Builder.AppendLine("                try");
                api_controllers_Builder.AppendLine("                {");
                api_controllers_Builder.AppendLine("                Dictionary<string, object> para = new Dictionary<string, object>();");
                api_controllers_Builder.AppendLine("                para.Add(\"login_code\", t.user_code);");
                api_controllers_Builder.AppendLine("                bool is_locked = false;");
                api_controllers_Builder.AppendLine("                bool.TryParse(Convert.ToString(data[\"is_locked\"]), out is_locked);");
                api_controllers_Builder.AppendLine("                data[\"is_locked\"] = is_locked ? 1 : 0;");
                api_controllers_Builder.AppendLine($"               DataTable ds = _data_access.GetDataTable(_site_config.GetDBConnectionString(t),string.Format(\"{{0}}.{{1}}\", DbSchema.{schema}, sql_identifier.{entity_name}.{storeprocedurename[UtilityScript.create_update_key]}.ToString()), data, para, null);");
                api_controllers_Builder.AppendLine("                return ds.ToJObject();");
                api_controllers_Builder.AppendLine("            }");
                api_controllers_Builder.AppendLine("            catch (Exception ex)");
                api_controllers_Builder.AppendLine("            {");
                api_controllers_Builder.AppendLine("                throw ex;");
                api_controllers_Builder.AppendLine("            }");
                api_controllers_Builder.AppendLine("            }");
            }
            api_controllers_Builder.AppendLine("\n");
            // To Read {entity_name} Data
            if (storeprocedurename.ContainsKey(UtilityScript.read_key))
            {
                dic_storeprocedurename.Add(UtilityScript.read_key, $"get_{entity_name}");
                api_controllers_Builder.AppendLine($"            public JObject get_{entity_name}(JObject data, _token t)");
                api_controllers_Builder.AppendLine("            {");
                api_controllers_Builder.AppendLine("                try");
                api_controllers_Builder.AppendLine("                {");
                api_controllers_Builder.AppendLine("                Dictionary<string, object> para = new Dictionary<string, object>();");
                //api_controllers_Builder.AppendLine("                para.Add(\"login_code\", t.UserCode);");
                api_controllers_Builder.AppendLine($"               DataTable ds = _data_access.GetDataTable(_site_config.GetDBConnectionString(t),string.Format(\"{{0}}.{{1}}\", DbSchema.{schema}, sql_identifier.{entity_name}.{storeprocedurename[UtilityScript.read_key]}.ToString()), data, para, null);");
                api_controllers_Builder.AppendLine("                return ds.ToJObject();");
                api_controllers_Builder.AppendLine("            }");
                api_controllers_Builder.AppendLine("            catch (Exception ex)");
                api_controllers_Builder.AppendLine("            {");
                api_controllers_Builder.AppendLine("                throw ex;");
                api_controllers_Builder.AppendLine("            }");
                api_controllers_Builder.AppendLine("            }");
                api_controllers_Builder.AppendLine("\n");
            }

            // To Delete {entity_name} Data
            if (storeprocedurename.ContainsKey(UtilityScript.delete_key))
            {
                dic_storeprocedurename.Add(UtilityScript.delete_key, $"delete_{entity_name}");
                api_controllers_Builder.AppendLine($"            public JObject delete_{entity_name}(JObject data, _token t)");
                api_controllers_Builder.AppendLine("            {");
                api_controllers_Builder.AppendLine("                try");
                api_controllers_Builder.AppendLine("                {");
                api_controllers_Builder.AppendLine("                DataTable dt = new DataTable();");
                api_controllers_Builder.AppendLine("                Dictionary<string, object> para = new Dictionary<string, object>();");
                api_controllers_Builder.AppendLine("                para.Add(\"login_code\", t.user_code);");
                api_controllers_Builder.AppendLine($"               dt = _data_access.GetDataTable(_site_config.GetDBConnectionString(t),string.Format(\"{{0}}.{{1}}\", DbSchema.{schema}, sql_identifier.{entity_name}.{storeprocedurename[UtilityScript.delete_key]}.ToString()), data, para, null);");
                api_controllers_Builder.AppendLine("                return dt.ToJObject();");
                api_controllers_Builder.AppendLine("            }");
                api_controllers_Builder.AppendLine("            catch (Exception ex)");
                api_controllers_Builder.AppendLine("            {");
                api_controllers_Builder.AppendLine("                throw ex;");
                api_controllers_Builder.AppendLine("            }");
                api_controllers_Builder.AppendLine("            }");
            }
            api_controllers_Builder.AppendLine("\n");
            api_controllers_Builder.AppendLine("}");
            api_controllers_Builder.AppendLine("}");
            return new Tuple<string, string, Dictionary<string, string>>(api_controllers_Builder.ToString(), bl_name, dic_storeprocedurename);

        }

        public static Tuple<string, string, Dictionary<string, string>> generate_api_controller(string entity_name, Dictionary<string, string> bl_method_name)
        {

            string controller_name = $"{entity_name}Controller";

            StringBuilder api_controllers_Builder = new StringBuilder();


            if (bl_method_name == null) { bl_method_name = new Dictionary<string, string>(); }
            // To Create API_controller File For the Given {entity_name}
            api_controllers_Builder.AppendLine($"using AdminPanelAPI.BL.{entity_name};");
            api_controllers_Builder.AppendLine($"using OTA_HELPER.custom;");
            api_controllers_Builder.AppendLine($"using Microsoft.AspNetCore.Http;");
            api_controllers_Builder.AppendLine($"using Microsoft.AspNetCore.Mvc;");
            api_controllers_Builder.AppendLine($"using Newtonsoft.Json.Linq;");
            api_controllers_Builder.AppendLine($"\n");
            api_controllers_Builder.AppendLine($"namespace AdminPanelAPI.Controllers.{entity_name}");
            api_controllers_Builder.AppendLine("{");
            api_controllers_Builder.AppendLine($"\n");
            api_controllers_Builder.AppendLine($"public class {controller_name} : baseController");
            api_controllers_Builder.AppendLine("{");
            api_controllers_Builder.AppendLine($"        {entity_name}_bl _bl = new {entity_name}_bl();");
            api_controllers_Builder.AppendLine($"\n");
            api_controllers_Builder.AppendLine($"[HttpPost]");
            // To Get {entity_name} Data
            if (bl_method_name.ContainsKey(UtilityScript.view_key))
            {
                api_controllers_Builder.AppendLine($"public JObject {bl_method_name[UtilityScript.view_key]}(JObject data)");
                api_controllers_Builder.AppendLine("{");
                api_controllers_Builder.AppendLine($"_token t = (HttpContext.Items[_site_config.GetConfigValue(\"login_key\")]) as _token;");
                api_controllers_Builder.AppendLine($"return _bl.{bl_method_name[UtilityScript.view_key]}(data, t);");
                api_controllers_Builder.AppendLine("}");

            }
            api_controllers_Builder.AppendLine($"\n");
            // To Insert Update {entity_name} Data
            if (bl_method_name.ContainsKey(UtilityScript.create_update_key))
            {
                api_controllers_Builder.AppendLine($"[HttpPost]");
                api_controllers_Builder.AppendLine($"public JObject {bl_method_name[UtilityScript.create_update_key]}(JObject data)");
                api_controllers_Builder.AppendLine("{");
                api_controllers_Builder.AppendLine($"_token t = (HttpContext.Items[_site_config.GetConfigValue(\"login_key\")]) as _token;");
                api_controllers_Builder.AppendLine($"return _bl.{bl_method_name[UtilityScript.create_update_key]}(data, t);");
                api_controllers_Builder.AppendLine("}");
            }
            api_controllers_Builder.AppendLine($"\n");

            // To Insert Update {entity_name} Data
            if (bl_method_name.ContainsKey(UtilityScript.read_key))
            {
                api_controllers_Builder.AppendLine($"[HttpPost]");
                api_controllers_Builder.AppendLine($"public JObject {bl_method_name[UtilityScript.read_key]}(JObject data)");
                api_controllers_Builder.AppendLine("{");
                api_controllers_Builder.AppendLine($"_token t = (HttpContext.Items[_site_config.GetConfigValue(\"login_key\")]) as _token;");
                api_controllers_Builder.AppendLine($"return _bl.{bl_method_name[UtilityScript.read_key]}(data, t);");
                api_controllers_Builder.AppendLine("}");
            }

            api_controllers_Builder.AppendLine("\n");

            if (bl_method_name.ContainsKey(UtilityScript.delete_key))
            {
                api_controllers_Builder.AppendLine($"[HttpPost]");
                api_controllers_Builder.AppendLine($"public JObject {bl_method_name[UtilityScript.delete_key]}(JObject data)");
                api_controllers_Builder.AppendLine("{");
                api_controllers_Builder.AppendLine($"_token t = (HttpContext.Items[_site_config.GetConfigValue(\"login_key\")]) as _token;");
                api_controllers_Builder.AppendLine($"return _bl.{bl_method_name[UtilityScript.delete_key]}(data, t);");
                api_controllers_Builder.AppendLine("}");
            }
            api_controllers_Builder.AppendLine("\n");
            api_controllers_Builder.AppendLine("}");
            api_controllers_Builder.AppendLine("\n");
            api_controllers_Builder.AppendLine("}");
            //
            return new Tuple<string, string, Dictionary<string, string>>(api_controllers_Builder.ToString(), controller_name, bl_method_name);



        }
    }
}
