using System.Data;

namespace OTAGenerator
{
    public static class GenerateCRUD
    {


        /// <summary>
        /// Step 1 SQL_StoreProcedure Generation
        /// </summary>
        /// <param name="table_name"></param>
        /// <returns></returns>
        public static Tuple<string, Dictionary<string, string>> GetSQL_StoreProcedure(string table_name, string schema)
        {
            var result = new Dictionary<string, string>();
            string primary_key_columns = "code";
            try
            {

                List<string> sql_object_list = new List<string>();

                // Start Generate SQL Script
                var dic_storeprocedure_info = new Dictionary<string, string>();
                var columninfo = SQLScriptGenerator.GetTable_Designs(table_name, schema);

                // To Generate View 
                string view_name = $"adv_{table_name.ToLower()}_select";
                string entity_view = SQLScriptGenerator.GenerateCreateViewScript(schema, view_name, table_name, columninfo);
                sql_object_list.Add(entity_view);

                // To Generate Listing Procedure
                var list_sp = SQLScriptGenerator.GenerateListingProcedureScript(schema, table_name, columninfo, view_name);
                dic_storeprocedure_info.Add(UtilityScript.view_key, list_sp.Item1);
                sql_object_list.Add(list_sp.Item2);


                // To Generate Insert Or Update Procedure
                var insert_sp = SQLScriptGenerator.GenerateInsertOrUpdateProcedureScript(schema, table_name, columninfo);
                dic_storeprocedure_info.Add(UtilityScript.create_update_key, insert_sp.Item1);
                sql_object_list.Add(insert_sp.Item2);
                primary_key_columns = insert_sp.Item4;
                // To Generate Read Procedure
                var read_sp = SQLScriptGenerator.GenerateReadProcedureScript(schema, table_name, columninfo, view_name);
                dic_storeprocedure_info.Add(UtilityScript.read_key, read_sp.Item1);
                sql_object_list.Add(read_sp.Item2);

                // To Generate Delete Procedure
                var delete_sp = SQLScriptGenerator.GenerateDeleteProcedureScript(schema, table_name, columninfo);
                dic_storeprocedure_info.Add(UtilityScript.delete_key, delete_sp.Item1);
                sql_object_list.Add(delete_sp.Item2);

                //To Generate SQL DB Object Script File
                string _raw_sql = SQLScriptGenerator.GenerateObjectScript(sql_object_list);
                UtilityScript.create_file(System.IO.Path.Combine(UtilityScript.api_codepath, $"{table_name}"), $"{table_name}.sql", _raw_sql);

                // End Generate SQL Script


                result = dic_storeprocedure_info;
            }
            catch (Exception ex)
            {


            }
            return new Tuple<string, Dictionary<string, string>>(primary_key_columns, result);

        }

        public static Dictionary<string, string> Generate_BLL_APIController(string table_name, Dictionary<string, string> dic_storeprocedure_info, string schema)
        {
            var result = new Dictionary<string, string>();
            try
            {
                // To Generate API Bl & Controller
                var _raw_bl = APIControllersGenerator.generate_api_bl(table_name, dic_storeprocedure_info, schema);
                UtilityScript.create_file(System.IO.Path.Combine(UtilityScript.api_codepath, $"{table_name}"), $"{table_name.ToLower()}.cs", _raw_bl.Item1);
                var _raw_controller = APIControllersGenerator.generate_api_controller(table_name, _raw_bl.Item3);
                UtilityScript.create_file(System.IO.Path.Combine(UtilityScript.api_codepath, $"{table_name}"), $"{UtilityScript.ConvertToTitleCase(table_name).ToLower()}Controller.cs", _raw_controller.Item1);
                result = _raw_controller.Item3;

            }
            catch (Exception ex)
            {

            }
            return result;

        }

        public static Dictionary<string, string> Generate_Angular_Components(string table_name, string controller_name, string primary_key_columns, Dictionary<string, string> dic_api_endpoint_info, DataTable control_config, string schema_name)
        {
            var result = new Dictionary<string, string>();
            try
            {
                // To Generate Component Folder
                string component_folder_path = System.IO.Path.Combine(UtilityScript.angular_codepath, $"{table_name}");
                string component_name = table_name.ToLower();

                // To Generate Service file

                // {entity_name}.service.ts
                string _service_file_name = $"{component_name}.service";
                string _entityservice_name = $"{table_name.ToLower()}Service";

                var _raw_service_ts = AngularScriptGenerator.generate_component_service_ts(table_name, table_name, _entityservice_name, dic_api_endpoint_info);
                UtilityScript.create_file(component_folder_path, $"{_service_file_name}.ts", _raw_service_ts.Item2);

                // {entity_name}.service.spec.ts
                string _raw_service_spec_ts_content = AngularScriptGenerator.generate_component_service_spec_ts(table_name, _entityservice_name);
                UtilityScript.create_file(component_folder_path, $"{_service_file_name}.spec.ts", _raw_service_spec_ts_content);

                // To Generate Add & Edit Component file
                string add_edit_component = $"{table_name}-add-edit";
                string _edit_component_name = $"{UtilityScript.ConvertToTitleCase(table_name)}AddEditComponent";
                string add_edit_component_folder_path = System.IO.Path.Combine(component_folder_path, add_edit_component);

                //{entity_name}-add-edit.component.html
                var _raw_add_edit_component = AngularScriptGenerator.generate_add_edit_component_html(table_name, table_name, primary_key_columns, control_config, schema_name);
                UtilityScript.create_file(add_edit_component_folder_path, $"{add_edit_component}.component.html", _raw_add_edit_component.Item1);

                //{entity_name}-add-edit.component.scss
                string _raw_add_edit_component_scss_content = AngularScriptGenerator.generate_add_edit_component_scss(table_name, table_name);
                UtilityScript.create_file(add_edit_component_folder_path, $"{add_edit_component}.component.scss", _raw_add_edit_component_scss_content);

                //{entity_name}-add-edit.component.spec.ts
                string _raw_add_edit_component_spec_ts_content = AngularScriptGenerator.generate_add_edit_component_spec_ts(table_name, _edit_component_name, table_name, _edit_component_name);
                UtilityScript.create_file(add_edit_component_folder_path, $"{add_edit_component}.component.spec.ts", _raw_add_edit_component_spec_ts_content);

                //{entity_name}-add-edit.component.ts
                string _raw_add_edit_component_ts_content = AngularScriptGenerator.generate_add_edit_component_ts(table_name, _edit_component_name, _entityservice_name, primary_key_columns, dic_api_endpoint_info, _raw_add_edit_component.Item2);
                UtilityScript.create_file(add_edit_component_folder_path, $"{add_edit_component}.component.ts", _raw_add_edit_component_ts_content);

                // To Generate List Component file

                // {entity_name}.component.html
                string _raw_component_html_content = AngularScriptGenerator.generate_list_component_html(table_name, table_name);
                UtilityScript.create_file(component_folder_path, $"{component_name}.component.html", _raw_component_html_content);

                // {entity_name}.component.scss
                string _raw_component_scss_content = AngularScriptGenerator.generate_list_component_scss(table_name, table_name);
                UtilityScript.create_file(component_folder_path, $"{component_name}.component.scss", _raw_component_scss_content);

                // {entity_name}.component.ts
                var _raw_component_ts = AngularScriptGenerator.generate_list_component_ts(table_name, table_name, _entityservice_name, primary_key_columns, _raw_service_ts.Item3, add_edit_component, control_config);
                UtilityScript.create_file(component_folder_path, $"{component_name}.component.ts", _raw_component_ts.Item2);

                // {entity_name}.component.spec.ts
                string _raw_component_spec_ts_content = AngularScriptGenerator.generate_list_component_spec_ts(table_name, table_name, _raw_component_ts.Item1);
                UtilityScript.create_file(component_folder_path, $"{component_name}.component.spec.ts", _raw_component_spec_ts_content);




            }
            catch (Exception ex)
            {

            }
            return result;

        }


    }

    public static class GenerateCRUD1
    {


        /// <summary>
        /// Step 1 SQL_StoreProcedure Generation
        /// </summary>
        /// <param name="table_name"></param>
        /// <returns></returns>
        public static Tuple<string, Dictionary<string, string>> GetSQL_StoreProcedure(string table_name, string schema)
        {
            var result = new Dictionary<string, string>();
            string primary_key_columns = "code";
            try
            {

                List<string> sql_object_list = new List<string>();

                // Start Generate SQL Script
                var dic_storeprocedure_info = new Dictionary<string, string>();
                var columninfo = SQLScriptGenerator.GetTable_Designs(table_name, schema);

                // To Generate View 
                string view_name = $"adv_{table_name.ToLower()}_select";
                string entity_view = SQLScriptGenerator.GenerateCreateViewScript(schema, view_name, table_name, columninfo);
                sql_object_list.Add(entity_view);

                // To Generate Listing Procedure
                var list_sp = SQLScriptGenerator.GenerateListingProcedureScript(schema, table_name, columninfo, view_name);
                dic_storeprocedure_info.Add(UtilityScript.view_key, list_sp.Item1);
                sql_object_list.Add(list_sp.Item2);


                // To Generate Insert Or Update Procedure
                var insert_sp = SQLScriptGenerator.GenerateInsertOrUpdateProcedureScript(schema, table_name, columninfo);
                dic_storeprocedure_info.Add(UtilityScript.create_update_key, insert_sp.Item1);
                sql_object_list.Add(insert_sp.Item2);
                primary_key_columns = insert_sp.Item4;
                // To Generate Read Procedure
                var read_sp = SQLScriptGenerator.GenerateReadProcedureScript(schema, table_name, columninfo, view_name);
                dic_storeprocedure_info.Add(UtilityScript.read_key, read_sp.Item1);
                sql_object_list.Add(read_sp.Item2);

                // To Generate Delete Procedure
                var delete_sp = SQLScriptGenerator.GenerateDeleteProcedureScript(schema, table_name, columninfo);
                dic_storeprocedure_info.Add(UtilityScript.delete_key, delete_sp.Item1);
                sql_object_list.Add(delete_sp.Item2);

                //To Generate SQL DB Object Script File
                string _raw_sql = SQLScriptGenerator.GenerateObjectScript(sql_object_list);
                UtilityScript.create_file(System.IO.Path.Combine(UtilityScript.api_codepath, $"{table_name}"), $"{table_name}.sql", _raw_sql);

                // End Generate SQL Script


                result = dic_storeprocedure_info;
            }
            catch (Exception ex)
            {


            }
            return new Tuple<string, Dictionary<string, string>>(primary_key_columns, result);

        }

        public static Dictionary<string, string> Generate_BLL_APIController(string table_name, Dictionary<string, string> dic_storeprocedure_info, string schema)
        {
            var result = new Dictionary<string, string>();
            try
            {
                // To Generate API Bl & Controller
                var _raw_bl = APIControllersGenerator.generate_api_bl(table_name, dic_storeprocedure_info, schema);
                UtilityScript.create_file(System.IO.Path.Combine(UtilityScript.api_codepath, $"{table_name}"), $"{table_name.ToLower()}.cs", _raw_bl.Item1);
                var _raw_controller = APIControllersGenerator.generate_api_controller(table_name, _raw_bl.Item3);
                UtilityScript.create_file(System.IO.Path.Combine(UtilityScript.api_codepath, $"{table_name}"), $"{UtilityScript.ConvertToTitleCase(table_name).ToLower()}Controller.cs", _raw_controller.Item1);
                result = _raw_controller.Item3;

            }
            catch (Exception ex)
            {

            }
            return result;

        }

        public static Dictionary<string, string> Generate_Angular_Components(string table_name, string controller_name, string primary_key_columns, Dictionary<string, string> dic_api_endpoint_info, DataTable control_config, string schema_name)
        {
            var result = new Dictionary<string, string>();
            try
            {
                // To Generate Component Folder
                string component_folder_path = System.IO.Path.Combine(UtilityScript.angular_codepath, $"{table_name}");
                string component_name = table_name.ToLower();

                // To Generate Service file

                // {entity_name}.service.ts
                string _service_file_name = $"{component_name}.service";
                string _entityservice_name = $"{table_name.ToLower()}Service";

                var _raw_service_ts = AngularScriptGenerator1.generate_component_service_ts(table_name, table_name, _entityservice_name, dic_api_endpoint_info);
                UtilityScript.create_file(component_folder_path, $"{_service_file_name}.ts", _raw_service_ts.Item2);

                // {entity_name}.service.spec.ts
                string _raw_service_spec_ts_content = AngularScriptGenerator1.generate_component_service_spec_ts(table_name, _entityservice_name);
                UtilityScript.create_file(component_folder_path, $"{_service_file_name}.spec.ts", _raw_service_spec_ts_content);

                // To Generate Add & Edit Component file
                string add_edit_component = $"{table_name}-add-edit";
                string _edit_component_name = $"{UtilityScript.ConvertToTitleCase(table_name)}AddEditComponent";
                string add_edit_component_folder_path = System.IO.Path.Combine(component_folder_path, add_edit_component);

                //{entity_name}-add-edit.component.html
                var _raw_add_edit_component = AngularScriptGenerator1.generate_add_edit_component_html(table_name, table_name, primary_key_columns, control_config, schema_name);
                UtilityScript.create_file(add_edit_component_folder_path, $"{add_edit_component}.component.html", _raw_add_edit_component.Item1);

                //{entity_name}-add-edit.component.scss
                string _raw_add_edit_component_scss_content = AngularScriptGenerator1.generate_add_edit_component_scss(table_name, table_name);
                UtilityScript.create_file(add_edit_component_folder_path, $"{add_edit_component}.component.scss", _raw_add_edit_component_scss_content);

                //{entity_name}-add-edit.component.spec.ts
                string _raw_add_edit_component_spec_ts_content = AngularScriptGenerator1.generate_add_edit_component_spec_ts(table_name, _edit_component_name, table_name, _edit_component_name);
                UtilityScript.create_file(add_edit_component_folder_path, $"{add_edit_component}.component.spec.ts", _raw_add_edit_component_spec_ts_content);

                //{entity_name}-add-edit.component.ts
                string _raw_add_edit_component_ts_content = AngularScriptGenerator1.generate_add_edit_component_ts(table_name, _edit_component_name, _entityservice_name, primary_key_columns, dic_api_endpoint_info, _raw_add_edit_component.Item2);
                UtilityScript.create_file(add_edit_component_folder_path, $"{add_edit_component}.component.ts", _raw_add_edit_component_ts_content);

                // To Generate List Component file

                // {entity_name}.component.html
                string _raw_component_html_content = AngularScriptGenerator1.generate_list_component_html(table_name, table_name);
                UtilityScript.create_file(component_folder_path, $"{component_name}.component.html", _raw_component_html_content);

                // {entity_name}.component.scss
                string _raw_component_scss_content = AngularScriptGenerator1.generate_list_component_scss(table_name, table_name);
                UtilityScript.create_file(component_folder_path, $"{component_name}.component.scss", _raw_component_scss_content);

                // {entity_name}.component.ts
                var _raw_component_ts = AngularScriptGenerator1.generate_list_component_ts(table_name, table_name, _entityservice_name, primary_key_columns, _raw_service_ts.Item3, add_edit_component, control_config);
                UtilityScript.create_file(component_folder_path, $"{component_name}.component.ts", _raw_component_ts.Item2);

                // {entity_name}.component.spec.ts
                string _raw_component_spec_ts_content = AngularScriptGenerator1.generate_list_component_spec_ts(table_name, table_name, _raw_component_ts.Item1);
                UtilityScript.create_file(component_folder_path, $"{component_name}.component.spec.ts", _raw_component_spec_ts_content);




            }
            catch (Exception ex)
            {

            }
            return result;

        }


    }
}
