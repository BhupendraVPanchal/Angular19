using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace OTAGenerator
{
    public partial class Form1 : Form
    {
        SQLScriptGenerator sQLScriptGenerator = new SQLScriptGenerator();
        public Form1()
        {
            InitializeComponent();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private DataTable GetDataTableFromDataGridView(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // Add columns to DataTable
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                dt.Columns.Add(column.Name);
            }

            // Add rows to DataTable
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue; // Skip the last row if it's the new row placeholder
                DataRow dr = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dr[cell.ColumnIndex] = cell.Value ?? DBNull.Value;
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        private void btn_generate_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable userDatatable = GetDataTableFromDataGridView(dataGridView1);

                string schema_name = txt_schema_name.Text.ToLower().Trim();
                string table_name = txt_table_name.Text.ToLower().Trim();
                string form_name = txt_from_name.Text.ToLower().Trim();
                string module_name = txt_module_name.Text.ToLower().Trim();

                // To Generate Component Folder
                string component_folder_path = System.IO.Path.Combine(UtilityScript.angular_codepath, $"{table_name}");
                string component_name = table_name.ToLower();

                var sp_dic = GenerateCRUD.GetSQL_StoreProcedure(table_name, schema_name);
                var dic_api_endpoint = GenerateCRUD.Generate_BLL_APIController(table_name, sp_dic.Item2, schema_name);
                GenerateCRUD.Generate_Angular_Components(table_name, table_name, sp_dic.Item1, dic_api_endpoint, userDatatable, schema_name);





                // To Generate Add & Edit Component file
                string add_edit_component = $"{table_name}-add-edit";
                string _edit_component_name = $"{table_name}AddEditComponent";
                string add_edit_component_folder_path = System.IO.Path.Combine(component_folder_path, add_edit_component);

                ////{entity_name}-add-edit.component.html
                //string _raw_add_edit_component_html_content = AngularScriptGenerator.generate_add_edit_component_html(table_name, table_name);
                //UtilityScript.create_file(add_edit_component_folder_path, $"{add_edit_component}.component.html", _raw_add_edit_component_html_content);

                ////{entity_name}-add-edit.component.scss
                //string _raw_add_edit_component_scss_content = AngularScriptGenerator.generate_add_edit_component_scss(table_name, table_name);
                //UtilityScript.create_file(add_edit_component_folder_path, $"{add_edit_component}.component.scss", _raw_add_edit_component_scss_content);

                ////{entity_name}-add-edit.component.spec.ts
                //string _raw_add_edit_component_spec_ts_content = AngularScriptGenerator.generate_add_edit_component_spec_ts(table_name, table_name);
                //UtilityScript.create_file(add_edit_component_folder_path, $"{add_edit_component}.component.spec.ts", _raw_add_edit_component_spec_ts_content);

                ////{entity_name}-add-edit.component.ts
                //string _raw_add_edit_component_ts_content = AngularScriptGenerator.generate_add_edit_component_ts(table_name, _service_file_name, _edit_component_name);
                //UtilityScript.create_file(add_edit_component_folder_path, $"{add_edit_component}.component.ts", _raw_add_edit_component_ts_content);









            }
            catch (Exception)
            {

                throw;
            }
        }


        private void btn_generate1_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable userDatatable = GetDataTableFromDataGridView(dataGridView1);

                string schema_name = txt_schema_name.Text.ToLower().Trim();
                string table_name = txt_table_name.Text.ToLower().Trim();
                string form_name = txt_from_name.Text.ToLower().Trim();
                string module_name = txt_module_name.Text.ToLower().Trim();

                // To Generate Component Folder
                string component_folder_path = System.IO.Path.Combine(UtilityScript.angular_codepath, $"{table_name}");
                string component_name = table_name.ToLower();

                var sp_dic = GenerateCRUD.GetSQL_StoreProcedure(table_name, schema_name);
                var dic_api_endpoint = GenerateCRUD.Generate_BLL_APIController(table_name, sp_dic.Item2, schema_name);
                GenerateCRUD.Generate_Angular_Components(table_name, table_name, sp_dic.Item1, dic_api_endpoint, userDatatable, schema_name);





                // To Generate Add & Edit Component file
                string add_edit_component = $"{table_name}-add-edit";
                string _edit_component_name = $"{table_name}AddEditComponent";
                string add_edit_component_folder_path = System.IO.Path.Combine(component_folder_path, add_edit_component);

                ////{entity_name}-add-edit.component.html
                //string _raw_add_edit_component_html_content = AngularScriptGenerator.generate_add_edit_component_html(table_name, table_name);
                //UtilityScript.create_file(add_edit_component_folder_path, $"{add_edit_component}.component.html", _raw_add_edit_component_html_content);

                ////{entity_name}-add-edit.component.scss
                //string _raw_add_edit_component_scss_content = AngularScriptGenerator.generate_add_edit_component_scss(table_name, table_name);
                //UtilityScript.create_file(add_edit_component_folder_path, $"{add_edit_component}.component.scss", _raw_add_edit_component_scss_content);

                ////{entity_name}-add-edit.component.spec.ts
                //string _raw_add_edit_component_spec_ts_content = AngularScriptGenerator.generate_add_edit_component_spec_ts(table_name, table_name);
                //UtilityScript.create_file(add_edit_component_folder_path, $"{add_edit_component}.component.spec.ts", _raw_add_edit_component_spec_ts_content);

                ////{entity_name}-add-edit.component.ts
                //string _raw_add_edit_component_ts_content = AngularScriptGenerator.generate_add_edit_component_ts(table_name, _service_file_name, _edit_component_name);
                //UtilityScript.create_file(add_edit_component_folder_path, $"{add_edit_component}.component.ts", _raw_add_edit_component_ts_content);









            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btn_generate2_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable userDatatable = GetDataTableFromDataGridView(dataGridView1);

                string schema_name = txt_schema_name.Text.ToLower().Trim();
                string table_name = txt_table_name.Text.ToLower().Trim();
                string form_name = txt_from_name.Text.ToLower().Trim();
                string module_name = txt_module_name.Text.ToLower().Trim();

                // To Generate Component Folder
                string component_folder_path = System.IO.Path.Combine(UtilityScript.angular_codepath, $"{table_name}");
                string component_name = table_name.ToLower();

                var sp_dic = GenerateCRUD1.GetSQL_StoreProcedure(table_name, schema_name);
                var dic_api_endpoint = GenerateCRUD1.Generate_BLL_APIController(table_name, sp_dic.Item2, schema_name);
                GenerateCRUD1.Generate_Angular_Components(table_name, table_name, sp_dic.Item1, dic_api_endpoint, userDatatable, schema_name);














            }
            catch (Exception)
            {

                throw;
            }
        }


        private KeyValuePair<string, string> generate_CURD()
        {
            var result = new KeyValuePair<string, string>();
            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txt_from_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            DataTable field_dt = new DataTable();
            string table_name = txt_table_name.Text.ToLower().Trim();
            string schema_name = txt_schema_name.Text.ToLower().Trim();
            field_dt = SQLScriptGenerator.GetGridTableMetadata(table_name, schema_name);
            dataGridView1.DataSource = field_dt;
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
            for (int i = 0; i < field_dt.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell must = new DataGridViewCheckBoxCell();
                must.Value = false;
                DataGridViewCheckBoxCell Visible = new DataGridViewCheckBoxCell();
                Visible.Value = false;
                DataGridViewComboBoxCell Control = new DataGridViewComboBoxCell();
                Control.Items.Add("txtbox");
                Control.Items.Add("date-picker");
                Control.Items.Add("switch");
                Control.Items.Add("select");
                Control.Items.Add("checkbox");
                dataGridView1.Rows[i].Cells[3] = Control;
                dataGridView1.Rows[i].Cells[4] = must;
                dataGridView1.Rows[i].Cells[5] = Visible;
                dataGridView1.Rows[i].Cells[6].Value = string.Empty;
                dataGridView1.Rows[i].Cells[7].Value = string.Empty;
                dataGridView1.Rows[i].Cells[8].Value = string.Empty;
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Your custom code here
            if (e.ColumnIndex == 6)
            {
                int RowIndex = e.RowIndex;
                string tableName = Convert.ToString(dataGridView1.Rows[RowIndex].Cells[6].Value);
                string schema_name = txt_schema_name.Text.ToLower().Trim();
                if (tableName.Length > 0)
                {
                    DataTable field_dt = SQLScriptGenerator.GetTableMetadata(tableName, schema_name);
                    DataGridViewComboBoxCell display_field = new DataGridViewComboBoxCell();
                    DataGridViewComboBoxCell value_field = new DataGridViewComboBoxCell();
                    for (int i = 0; i < field_dt.Rows.Count; i++)
                    {
                        display_field.Items.Add(Convert.ToString(field_dt.Rows[i]["COLUMN_NAME"]));
                        value_field.Items.Add(Convert.ToString(field_dt.Rows[i]["COLUMN_NAME"]));
                    }
                    dataGridView1.Rows[RowIndex].Cells[7] = display_field;
                    dataGridView1.Rows[RowIndex].Cells[8] = value_field;
                }

            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //var keyValuePairs = new Dictionary<string, string>
            //{
            //    {"p_company_list", "adp_company_select"},
            //    {"p_company_insert_or_update", "adp_company_insert_or_update"},
            //    {"p_company_read", "adp_company_read"},
            //    {"p_company_delete", "adp_company_delete"},
            //    {"p_messages_list", "adp_messages_select"},
            //    {"p_messages_insert_or_update", "adp_messages_insert_or_update"},
            //    {"p_messages_read", "adp_messages_read"},
            //    {"p_messages_delete", "adp_messages_delete"},
            //    {"p_users_Insert", "adp_users_insert"},
            //    {"p_users_Delete", "adp_users_delete"},
            //    {"p_users_Update", "adp_users_update"},
            //    {"p_users_list", "adp_users_select"},
            //    {"p_users_read", "adp_users_read"},
            //    {"p_menus_list", "adp_menus_select"},
            //    {"p_adm_menus_select", "adp_menus_select"},
            //    {"p_menus_insert_or_update", "adp_menus_insert_or_update"},
            //    {"p_menus_read", "adp_menus_read"},
            //    {"p_menus_delete", "adp_menus_delete"},
            //    {"p_get_menus_help", "adp_menus_help"},
            //    {"p_adm_sections_select", "adp_sections_select"},
            //    {"p_sections_insert_or_update", "adp_sections_insert_or_update"},
            //    {"p_sections_read", "adp_sections_read"},
            //    {"p_sections_delete", "adp_sections_delete"},
            //    {"p_adm_popups_select", "adp_popups_select"},
            //    {"p_popups_Insert", "adp_popups_insert"},
            //    {"p_popups_Update", "adp_popups_update"},
            //    {"p_popups_read", "adp_popups_read"},
            //    {"p_popups_delete", "adp_popups_delete"},
            //    {"p_adm_dynamic_pages_select", "adp_dynamic_pages_select"},
            //    {"p_dynamic_pages_insert_or_update", "adp_dynamic_pages_insert_or_update"},
            //    {"p_dynamic_pages_read", "adp_dynamic_pages_read"},
            //    {"p_dynamic_pages_delete", "adp_dynamic_pages_delete"},
            //    {"p_adm_advertisements_select", "adp_advertisements_select"},
            //    {"p_advertisements_Insert", "adp_advertisements_insert"},
            //    {"p_advertisements_Update", "adp_advertisements_update"},
            //    {"p_advertisements_read", "adp_advertisements_read"},
            //    {"p_advertisements_delete", "adp_advertisements_delete"},
            //    {"p_adm_footer_sections_select", "adp_footer_sections_select"},
            //    {"p_footer_sections_insert_or_update", "adp_footer_sections_insert_or_update"},
            //    {"p_footer_sections_read", "adp_footer_sections_read"},
            //    {"p_footer_sections_delete", "adp_footer_sections_delete"},
            //    {"p_adm_country_state_city_Insert", "adp_country_state_city_insert"},
            //    {"p_adm_country_state_city_Update", "adp_country_state_city_update"},
            //    {"p_adm_country_state_city_Delete", "adp_country_state_city_delete"},
            //    {"p_adm_country_state_city_select", "adp_country_state_city_select"},
            //    {"p_adm_get_country_state_city_by_type", "adp_country_state_city_by_type"},
            //    {"p_adm_advertisement_bookings_select", "adp_advertisement_bookings_select"},
            //    {"p_advertisement_bookings_insert_or_update", "adp_advertisement_bookings_insert_or_update"},
            //    {"p_advertisement_bookings_read", "adp_advertisement_bookings_read"},
            //    {"p_advertisement_bookings_delete", "adp_advertisement_bookings_delete"},
            //    {"p_get_advertisement_help", "adp_advertisement_help"}
            //};
            //var keyValuePairs = new Dictionary<string, string>();
            //keyValuePairs.Add("p_business_roles_list", "adp_business_roles_select");
            //keyValuePairs.Add("p_business_roles_read", "adp_business_roles_read");
            //keyValuePairs.Add("p_business_roles_delete", "adp_business_roles_delete");
            //keyValuePairs.Add("p_business_roles_Insert", "adp_business_roles_Insert");
            //keyValuePairs.Add("p_business_roles_Update", "adp_business_roles_Update");
            //keyValuePairs.Add("p_get_roles_help", "adp_get_roles_help");
            //keyValuePairs.Add("give_business_role_menu_access", "adp_give_business_role_menu_access");
            //keyValuePairs.Add("give_all_business_role_menu_access", "adp_give_all_business_role_menu_access");
            //keyValuePairs.Add("p_get_business_roles_help", "adp_get_business_roles_help");
            //keyValuePairs.Add("get_business_role_menu_map_data", "adp_get_business_role_menu_map_data");
            //keyValuePairs.Add("p_user_help", "adp_user_help");
            //keyValuePairs.Add("get_user_business_role_map_data", "adp_get_user_business_role_map_data");
            //keyValuePairs.Add("give_business_role_access", "adp_give_business_role_access");
            //keyValuePairs.Add("give_all_business_role_access", "adp_give_all_business_role_access");

            //foreach (var item in keyValuePairs)
            //{
            //    SQLScriptGenerator.RenameStoredProcedure(item.Key, item.Value);
            //}
        }

        private void txt_schema_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            btn_generate2_Click(sender, e);
        }
    }
}
public class SQLScriptGenerator
{
    public static string GenerateCreateTableScript(string tableName, List<table_design> columns)
    {
        StringBuilder createTableScriptBuilder = new StringBuilder();
        createTableScriptBuilder.AppendLine($"CREATE TABLE {tableName} (");
        foreach (var column in columns)
        {
            createTableScriptBuilder.AppendLine($"    {column._column_name} {column._data_type}  {(column._is_primary_key == true ? " PRIMARY KEY " : "")}");
            if (column._column_relation != null && column._column_relation._table_name != null)
            {
                if (column._column_relation._table_name != null && Convert.ToString(column._column_relation._table_name).Length > 3)
                {
                    createTableScriptBuilder.AppendLine($"    FOREIGN KEY ({column._column_name}) REFERENCES {column._column_relation._table_name}({column._column_relation._target_refer_column_name}),");
                }
            }
            else
            {
                createTableScriptBuilder.AppendLine(",");
            }
        }
        createTableScriptBuilder.Length -= 3;
        createTableScriptBuilder.AppendLine(");");
        return createTableScriptBuilder.ToString();
    }

    public static string GenerateCreateViewScript(string viewName, string tableName, List<table_design> columnDefinitions)
    {
        int table_count = 0;

        Dictionary<string, string> _dic_table_alise = new Dictionary<string, string>();

        _dic_table_alise.Add(tableName, string.Format("{0}{1}", "t", table_count));
        StringBuilder scriptBuilder = new StringBuilder();

        scriptBuilder.AppendLine($"CREATE VIEW {viewName} AS");
        scriptBuilder.AppendLine("SELECT");

        foreach (var column in columnDefinitions)
        {
            scriptBuilder.AppendLine($"    {_dic_table_alise[tableName]}.{column._column_name} AS {column._column_name}{((column._column_name.ToLower().Trim().Contains("value") || column._is_primary_key == false) ? string.Empty : ((column._is_value_field_required == true) ? "_value" : ""))},");
            if (column._column_relation != null)
            {
                if (column._column_relation._table_name != null && Convert.ToString(column._column_relation._table_name).Length > 3)
                {
                    scriptBuilder.AppendLine($"    {column._column_relation._table_alias}.{column._column_relation._target_refer_column_name} AS {column._column_relation._target_refer_column_name}_display,");
                }
            }
        }

        scriptBuilder.Length -= 3;

        scriptBuilder.AppendLine($"  FROM {tableName} {_dic_table_alise[tableName]}");

        foreach (var column in columnDefinitions)
        {
            if (column._column_relation != null)
            {
                if (column._column_relation._table_name != null && Convert.ToString(column._column_relation._table_name).Length > 3)
                {
                    scriptBuilder.AppendLine($"    LEFT OUTER JOIN {column._column_relation._table_name} {column._column_relation._table_alias} ON ");
                    scriptBuilder.AppendLine($"        {_dic_table_alise[tableName]}.{column._column_name} = {column._column_relation._table_alias}.{column._column_relation._target_refer_column_name}");
                }
            }
        }
        return scriptBuilder.ToString();
    }



}


public class table_design
{

    public table_design() { }

    public table_design(string column_name, string data_type, column_relation column_relation_, bool? is_primary_key = false, bool? is_value_field_required = false)
    {
        this._column_name = column_name;
        this._data_type = data_type;
        this._column_relation = column_relation_;
        this._is_primary_key = is_primary_key;
        this._is_value_field_required = is_value_field_required;
        this._is_display_field_required = is_value_field_required;
    }
    public string _column_name { get; set; }
    public string _data_type { get; set; }

    public bool? _is_primary_key { get; set; }
    public bool? _is_display_field_required { get; set; }
    public bool? _is_value_field_required { get; set; }

    public column_relation _column_relation { get; set; }



}

public class column_relation
{
    public column_relation() { }
    public column_relation(string table_name, string target_refer_column_name, string target_refer_display_column_name)
    {
        this._table_name = table_name;
        this._table_alias = string.Format("{0}_{1}", table_name[0], Convert.ToString(Guid.NewGuid()).Substring(0, 4));
        this._target_refer_column_name = target_refer_column_name;
        this._target_refer_display_column_name = target_refer_display_column_name;
    }

    public column_relation(string mt_column_format_str)
    {
        Regex regex = new Regex(@"RefCol\((?<tableName>[\w\s]+),(?<referencedColumnName>[\w\s]+),(?<displayColumnName>[\w\s]+)\)");
        Match match = regex.Match(mt_column_format_str);
        if (match.Success)
        {
            this._table_name = match.Groups["tableName"].Value.Trim();
            this._target_refer_column_name = match.Groups["referencedColumnName"].Value.Trim();
            this._target_refer_display_column_name = match.Groups["displayColumnName"].Value.Trim();
        }
    }
    public string _table_name { get; set; }

    public string _table_alias { get; set; }
    public string _target_refer_column_name { get; set; }
    public string _target_refer_display_column_name { get; set; }

}