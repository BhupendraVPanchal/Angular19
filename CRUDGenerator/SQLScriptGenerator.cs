using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;

namespace OTAGenerator
{
    public class SQLScriptGenerator
    {
        //static string connectionString = "Server=appsrv-ver22;Database=goatourism;Trusted_Connection=false;User Id=sa;Password=Goldstar;";
        //static string connectionString = "Server=208.91.198.59;Database=goatourism;Trusted_Connection=false;User Id=ota;Password=$Ota@2024;";
        //static string connectionString = "Server=202.143.96.166,1433;Database=goatourism;Encrypt=True;TrustServerCertificate=True;User Id=sa;Password=Ccj3cr4zifUdra1lc?0!$;";
        //static string connectionString = "Server=13.201.15.80;Database=goatourism;Encrypt=True;TrustServerCertificate=True;User Id=sa;Password=Sagar@55555;";
        static string connectionString = "Server=MT-L-05\\MSSQLSERVER1;Database=test;Trusted_Connection=True;";

        public static List<string> sys_column_lst = new List<string>() { "created_on", "created_by", "updated_on", "updated_by", "update_count", "deleted_on", "deleted_by" };
        public static string user_login_code_parametername = "login_code";
        public static string user_login_code_parametername_data_type = "INT";
        public SQLScriptGenerator() { }

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

        public static string GenerateCreateViewScript(string schema, string viewName, string tableName, List<table_design> columnDefinitions)
        {
            int table_count = 0;
            string tablename_ = string.Format("{0}.{1}", schema, tableName);
            Dictionary<string, string> _dic_table_alise = new Dictionary<string, string>();

            _dic_table_alise.Add(tableName, string.Format("{0}{1}", "t", table_count));
            StringBuilder scriptBuilder = new StringBuilder();

            scriptBuilder.AppendLine($"CREATE  VIEW [{schema}].{viewName} AS");
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

            scriptBuilder.AppendLine($"  FROM {tablename_} {_dic_table_alise[tableName]}");

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
            scriptBuilder.AppendLine("Where t0.deleted_on IS NULL");
            return scriptBuilder.ToString();
        }

        public static DataTable GetDataTable(string _connectionString, string _query)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(_query, connection);
                DataTable schemaTable = new DataTable();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    schemaTable.Load(reader);
                }
                return schemaTable;
            }
        }

        public static DataSet GetDataSet(string _connectionString, string _query, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(_query, connection);
                command.Parameters.AddRange(parameters);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet resultDataSet = new DataSet();
                adapter.Fill(resultDataSet);
                return resultDataSet;
            }
        }

        public static DataTable GetTableMetadata(string tableName, string schema)
        {
            string query = $"SELECT ";
            query += $"c.COLUMN_NAME ";
            query += $",c.DATA_TYPE ";
            query += $",c.IS_NULLABLE ";
            query += $",c.CHARACTER_MAXIMUM_LENGTH ";
            query += $",c.NUMERIC_PRECISION ";
            query += $",c.NUMERIC_SCALE ";
            query += $",(CASE ";
            query += $"WHEN kcu.COLUMN_NAME IS NOT NULL THEN 1 ";
            query += $"ELSE 0 ";
            query += $"END) AS IS_PRIMARY_KEY ";

            query += $",c.DATA_TYPE AS[datatype] ";
            query += $", c.DATA_TYPE AS[data_type_description] ";
            query += $",(CASE WHEN kcu.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END) as IsMandetory ";
            query += $",c.COLUMN_NAME AS[label] ";
            query += $", NULL AS[value] ";
            query += $",0 AS[text_min_length] ";
            query += $",c.CHARACTER_MAXIMUM_LENGTH AS[text_max_length] ";
            query += $",0 AS[number_min_value] ";
            query += $",0 AS[num_max_value] ";
            query += $",NULL AS[text_type] ";
            query += $", NULL  AS[text_case] ";
            query += $",c.NUMERIC_SCALE AS[digits_after_decimal] ";
            query += $",'dd/MM/yyyy'  AS[DateFormat] ";
            query += $",'' AS[min_max_text_length_message] ";
            query += $",'' AS[min_max_number_value_message] ";
            query += $",CASE WHEN ISNULL(c.NUMERIC_SCALE, '') <> '' THEN ";
            query += $"c.COLUMN_NAME ";
            query += $"+ CASE WHEN c.NUMERIC_SCALE = 0 THEN ";
            query += $"' should not contain any decimal point ' ";
            query += $"ELSE ";
            query += $"' should contain max ' + CONVERT(VARCHAR(10), c.NUMERIC_SCALE) + ' digits after decimal point ' ";
            query += $"END ";
            query += $"END AS[number_decimal_message] ";
            query += $",CASE WHEN  c.DATA_TYPE = 'bit' THEN ";
            query += $"CONCAT(c.COLUMN_NAME, '', ' is bit field , only 1 and 0 is allowed to enter , 1 = yes , 0 = no ') ";
            query += $"ELSE NULL ";
            query += $"END AS[bit_message] ";
            query += $", CASE WHEN ISNULL(c.COLUMN_DEFAULT,'')<> ''  ";
            query += $"THEN ";
            query += $"CONCAT(c.COLUMN_NAME, '', ' has a default value of ', c.COLUMN_DEFAULT) ";
            query += $"ELSE c.COLUMN_DEFAULT ";
            query += $"END ";
            query += $"AS [default_message] ";
            query += $",'' AS[text_case_message] ";
            query += $",CASE WHEN ISNULL(c.DATA_TYPE, '') <> '' THEN ";
            query += $"CASE WHEN c.DATA_TYPE = 'int' THEN ";

            query += $"CONCAT(c.COLUMN_NAME, '', ' is set as Numeric Only ') ";
            query += $"WHEN c.DATA_TYPE = 'varchar' THEN ";

            query += $"CONCAT(c.COLUMN_NAME, '', ' is set as Alpha Numeric (no symbols) ') ";
            query += $"END ";
            query += $"END AS [text_type_message] ";


            query += $"FROM ";
            query += $"INFORMATION_SCHEMA.COLUMNS c ";
            query += $"LEFT JOIN ";
            query += $"INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu ON c.COLUMN_NAME = kcu.COLUMN_NAME AND c.TABLE_NAME = kcu.TABLE_NAME ";
            query += $"LEFT JOIN ";
            query += $"INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc ON kcu.CONSTRAINT_NAME = tc.CONSTRAINT_NAME AND tc.CONSTRAINT_TYPE = 'PRIMARY KEY' ";

            query += $"WHERE ";
            query += $"c.TABLE_NAME = '{tableName}' ";
            query += $" AND c.TABLE_SCHEMA='{schema}';";
            return GetDataTable(connectionString, query);
        }


        public static DataTable GetGridTableMetadata(string tableName, string schema)
        {
            DataTable resultdt = new DataTable();
            try
            {

                string query = $" SELECT ";
                query += $"COLUMN_NAME AS[Column Name], ";
                query += $"DATA_TYPE AS[Type], ";
                query += $"COLUMN_NAME AS[Caption], ";
                query += $" 'txtbox' AS[Control Name], ";
                query += $"CASE WHEN IS_NULLABLE = 'False' THEN 'True' ELSE 'False' END AS[Must], ";
                query += $"'true' AS [Visible], ";
                query += $"TABLE_NAME AS[DataSouce Table], ";
                query += $"COLUMN_NAME AS[Display Column], ";
                query += $"COLUMN_NAME AS[Value Column], ";
                query += $"NULL AS[SP Name] ";
                query += $"FROM INFORMATION_SCHEMA.COLUMNS ";
                query += $"WHERE TABLE_NAME = '{tableName}'";
                query += $" AND TABLE_SCHEMA='{schema}';";

                resultdt = GetDataTable(connectionString, query);

            }
            catch (Exception ex)
            {


            }
            return resultdt;

        }

        public static List<table_design> GetTable_Designs(string table_name, string schema)
        {
            List<table_design> table_designs = new List<table_design>();
            var table_design_obj = new table_design();
            try
            {
                var tablemetadata_dt = GetTableMetadata(table_name, schema);
                if (tablemetadata_dt != null)
                {
                    table_designs = table_design_obj.GetTable_Designs(tablemetadata_dt);
                }
            }
            catch (Exception ex)
            {


            }
            return table_designs;
        }

        // To Generate Insert , Update & Delete Procedure 

        public static Tuple<string, string, string> GenerateListingProcedureScript(string schema, string tableName, List<table_design> columns, string view_name)
        {
            string tablename_ = string.Format("{0}.{1}", schema, tableName);
            StringBuilder scriptBuilder = new StringBuilder();
            string error = string.Empty;
            string sp_name = $"adp_{tableName}_select";
            //string sp_description = $"exec sp_generate_dynamic_query '{tableName}',0,100,NULL,NULL,NULL,NULL,NULL,1,'{sp_name}'";

            scriptBuilder.AppendLine($"CREATE proc [{schema}].[{sp_name}]");
            scriptBuilder.AppendLine($"(");
            scriptBuilder.AppendLine($"@result_type int, --1:total_row_count , 2:actual data");
            scriptBuilder.AppendLine($"@page_number int,");
            scriptBuilder.AppendLine($"@page_size int ,");
            scriptBuilder.AppendLine($"@search_column varchar(50),          ");
            scriptBuilder.AppendLine($"@search_text varchar(50)  ,          ");
            scriptBuilder.AppendLine($"@sort_col_name varchar(50),          ");
            scriptBuilder.AppendLine($"@sort_type varchar(15)          ");
            scriptBuilder.AppendLine($"--@extra_whereclause VARCHAR(50) = null ");
            scriptBuilder.AppendLine($")            ");
            scriptBuilder.AppendLine($"as ");
            scriptBuilder.AppendLine($"begin ");
            scriptBuilder.AppendLine($"set nocount on ");
            scriptBuilder.AppendLine($"set xact_abort on ");
            scriptBuilder.AppendLine($"declare @ErrorDetails varchar(max),@ErrorSeverity smallint, @ErrorState smallint ");
            scriptBuilder.AppendLine($"begin try ");
            scriptBuilder.AppendLine($"begin tran ");
            scriptBuilder.AppendLine($"\n\n");

            scriptBuilder.AppendLine($"IF(@result_type = 2)");
            scriptBuilder.AppendLine($"BEGIN");
            scriptBuilder.AppendLine($"Select* from(");
            scriptBuilder.AppendLine($"SELECT");
            scriptBuilder.AppendLine($"CASE WHEN c.name = 'type_code' THEN 'type'");
            scriptBuilder.AppendLine($"ELSE c.[name] END AS  ColumnName");
            scriptBuilder.AppendLine($",CASE WHEN c.name = 'created_on' THEN 'Created On'");
            scriptBuilder.AppendLine($"WHEN c.name = 'created_by' THEN 'Created By'");
            scriptBuilder.AppendLine($"WHEN c.name = 'updated_on' THEN 'Updated On'");
            scriptBuilder.AppendLine($"WHEN c.name = 'updated_by' THEN 'Updated By'");
            scriptBuilder.AppendLine($"WHEN c.name = 'update_count' THEN 'Update Count'");
            scriptBuilder.AppendLine($"WHEN c.name = 'is_locked' THEN 'Locked'");
            scriptBuilder.AppendLine($"ELSE c.[name] END AS ColumnCaption ");
            scriptBuilder.AppendLine($",CASE WHEN c.name = 'created_on' THEN 100");
            scriptBuilder.AppendLine($"WHEN c.name = 'created_by' THEN 101");
            scriptBuilder.AppendLine($"WHEN c.name = 'updated_on' THEN 102");
            scriptBuilder.AppendLine($"WHEN c.name = 'updated_by' THEN 103");
            scriptBuilder.AppendLine($"WHEN c.name = 'update_count' THEN 104");
            scriptBuilder.AppendLine($"WHEN c.name = 'is_locked' THEN 97");
            scriptBuilder.AppendLine($"ELSE c.column_id END  AS ColumnOrder");
            scriptBuilder.AppendLine($",CONVERT(BIT, 0) IsEnable ");
            scriptBuilder.AppendLine($",CASE WHEN c.name in ('name','short_name') THEN 150 WHEN c.name in ('Edit','Delete','is_locked') THEN 80 ELSE 150 END AS width");
            scriptBuilder.AppendLine($", CASE WHEN c.name in ('is_locked') THEN 'checkbox' WHEN c.name in ('Edit','Delete') THEN 'button' ELSE 'textbox' END  [control]");
            scriptBuilder.AppendLine($", CASE WHEN c.name in ('row_no', 'created_on', 'created_by', 'updated_on', 'updated_by', 'update_count', 'deleted_on', 'deleted_by') THEN 0 ELSE 1 END AS IsVisible");
            scriptBuilder.AppendLine($", CASE WHEN c.name = 'Code' THEN 1 ELSE 0 END AS [is_primary]");
            scriptBuilder.AppendLine($",1 AS[sorting]");
            scriptBuilder.AppendLine($",(CASE WHEN c.name = 'Edit' THEN '<i class=\"bi bi-pencil-square\"></i>'");
            scriptBuilder.AppendLine($"WHEN c.name = 'Delete' THEN '<i class=\"bi bi-trash\"></i>'");
            scriptBuilder.AppendLine($"ELSE NULL END)  AS control_content");
            scriptBuilder.AppendLine($",null AS control_tooltip");
            scriptBuilder.AppendLine($",null AS[shortcut_key]");

            scriptBuilder.AppendLine($"FROM( ");
            scriptBuilder.AppendLine($"Select c1.name, c1.column_id, c1.user_type_id ");
            scriptBuilder.AppendLine($"From sys.columns c1  ");
            scriptBuilder.AppendLine($"JOIN sys.types t ON t.user_type_id = c1.user_type_id ");
            scriptBuilder.AppendLine($"WHERE c1.OBJECT_ID = OBJECT_ID('{tablename_}')  ");
            scriptBuilder.AppendLine($"Union All ");
            scriptBuilder.AppendLine($"Select 'Edit', 200, -1 ");
            scriptBuilder.AppendLine($"Union All ");
            scriptBuilder.AppendLine($"Select 'Delete', 201, -1 ");
            scriptBuilder.AppendLine($") c ");
            scriptBuilder.AppendLine($") tbl ");
            scriptBuilder.AppendLine($"Order By ColumnOrder ");


            scriptBuilder.AppendLine($"END");



            scriptBuilder.AppendLine($"exec p_adm_common_select ");
            scriptBuilder.AppendLine($"@result_type ,--@result_type ");
            scriptBuilder.AppendLine($"@page_number,--@page_number ");
            scriptBuilder.AppendLine($"@page_size,--@page_size ");
            scriptBuilder.AppendLine($"@search_column,--@search_column ");
            scriptBuilder.AppendLine($"@search_text,--@search_text ");
            scriptBuilder.AppendLine($"@sort_col_name,--@sort_col_name ");
            scriptBuilder.AppendLine($"@sort_type,--@sort_type ");
            scriptBuilder.AppendLine($"'[{schema}].{view_name}',--@table_name ");
            scriptBuilder.AppendLine($"'*',--@select_columns ");
            scriptBuilder.AppendLine($"'' ");
            scriptBuilder.AppendLine($"\n\n");

            scriptBuilder.AppendLine($"commit tran");
            scriptBuilder.AppendLine($"end try ");
            scriptBuilder.AppendLine($"begin catch");
            scriptBuilder.AppendLine($"rollback tran ");
            scriptBuilder.AppendLine($"select @ErrorDetails = ERROR_Message(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE()");
            scriptBuilder.AppendLine($"raiserror(@ErrorDetails, @ErrorSeverity, @ErrorState)");
            scriptBuilder.AppendLine($"end catch");
            scriptBuilder.AppendLine($"end");
            // Create procedure header
            scriptBuilder.AppendLine("GO");
            return new Tuple<string, string, string>(sp_name, scriptBuilder.ToString(), error);
        }

        public static Tuple<string, string, string, string> GenerateInsertOrUpdateProcedureScript(string schema, string tableName, List<table_design> columns, Dictionary<string, string> includeColumns = null, List<string> excludeColumns = null)
        {

            string tablename_ = string.Format("[{0}].{1}", schema, tableName);
            StringBuilder scriptBuilder = new StringBuilder();

            string error = string.Empty;
            string sp_name = $"adp_{tableName}_insert_or_update";

            // Create procedure header
            scriptBuilder.AppendLine($"CREATE PROCEDURE [{schema}].{sp_name}");

            var allcolumns = columns.Select(b => b._column_name.ToLower().Trim()).ToArray();

            var finalcolumns = sys_column_lst?.Count > 0 ? allcolumns.Except(sys_column_lst).ToList() : allcolumns.ToList();

            var pk_columninfo = columns.Where(g => (g._is_primary_key != null ? g._is_primary_key : false).Equals(true)).ToArray();

            string pk_column = pk_columninfo != null && pk_columninfo.Length > 0 ? pk_columninfo.FirstOrDefault()._column_name : columns.FirstOrDefault()._column_name;

            // Add parameters
            foreach (var column in columns)
            {
                if (finalcolumns.Contains(column._column_name.ToLower().Trim()))
                {
                    scriptBuilder.AppendLine($"    @{column._column_name.ToLower().Trim()} {column._data_type}{(column.IsNullable ? " = NULL" : "")},");
                }
            }

            scriptBuilder.AppendLine($"    @{user_login_code_parametername.ToLower().Trim()} {user_login_code_parametername_data_type},");
            // Customer Include parameters

            // Remove the trailing comma
            scriptBuilder.Length -= 3;

            scriptBuilder.AppendLine();
            scriptBuilder.AppendLine("AS");
            scriptBuilder.AppendLine("BEGIN");
            scriptBuilder.AppendLine("SET NOCOUNT ON");
            scriptBuilder.AppendLine("SET XACT_ABORT ON");
            scriptBuilder.AppendLine("DECLARE @ErrorDetails VARCHAR(MAX)");
            scriptBuilder.AppendLine("DECLARE @ErrorSeverity SMALLINT");
            scriptBuilder.AppendLine("DECLARE @ErrorState SMALLINT");
            scriptBuilder.AppendLine("BEGIN TRY");
            scriptBuilder.AppendLine("BEGIN TRAN");
            scriptBuilder.AppendLine();


            scriptBuilder.AppendLine($" IF(@{pk_column} = 0)");
            scriptBuilder.AppendLine("BEGIN");

            //
            scriptBuilder.AppendLine($"Select @{pk_column} = ISNULL(MAX({pk_column}), 0) + 1 from {tablename_}");
            // Build Start Update  statement
            string primaryKeyColumn_name = string.Empty;
            string primaryKeyColumn_data_type = string.Empty;
            var primaryKeyColumn = columns.Where(n => n._is_primary_key.Equals(true)).FirstOrDefault();
            if (primaryKeyColumn != null)
            {
                primaryKeyColumn_name = primaryKeyColumn._column_name;
                primaryKeyColumn_data_type = primaryKeyColumn._data_type;
            }
            else
            {
                primaryKeyColumn_name = columns.FirstOrDefault()._column_name;
                primaryKeyColumn_data_type = columns.FirstOrDefault()._data_type;
            }


            // Build Start INSERT INTO statement
            scriptBuilder.AppendLine($"    INSERT INTO {tablename_} (");
            foreach (var column in columns)
            {
                if (finalcolumns.Contains(column._column_name.ToLower().Trim()))
                {
                    scriptBuilder.AppendLine($"        {column._column_name},");
                }

            }
            scriptBuilder.AppendLine($"        created_on,");
            scriptBuilder.AppendLine($"        created_by,");
            scriptBuilder.Length -= 3; // Remove the trailing comma
            scriptBuilder.AppendLine("    )");
            scriptBuilder.AppendLine("    VALUES (");
            foreach (var column in columns)
            {
                if (finalcolumns.Contains(column._column_name.ToLower().Trim()))
                {
                    scriptBuilder.AppendLine($"        @{column._column_name},");
                }
            }
            scriptBuilder.AppendLine($"        GETDATE(),");
            scriptBuilder.AppendLine($"        @{user_login_code_parametername},");
            scriptBuilder.Length -= 3; // Remove the trailing comma

            scriptBuilder.AppendLine("    );");
            // Build End INSERT INTO statement
            //scriptBuilder.AppendLine($"Set @{primaryKeyColumn_name} = SCOPE_IDENTITY();");
            scriptBuilder.AppendLine("END");
            scriptBuilder.AppendLine("ELSE");
            scriptBuilder.AppendLine("BEGIN");

            // Build UPDATE statement
            scriptBuilder.AppendLine($"    UPDATE {tablename_}");
            scriptBuilder.AppendLine("    SET");
            foreach (var column in columns)
            {
                if (column._column_name != primaryKeyColumn_name && finalcolumns.Contains(column._column_name.ToLower().Trim()))
                {
                    scriptBuilder.AppendLine($"        {column._column_name} = @{column._column_name},");
                }
            }
            //scriptBuilder.Length -= 3; // Remove the trailing comma
            scriptBuilder.AppendLine("updated_on = GETDATE(),");
            scriptBuilder.AppendLine($"updated_by = @{user_login_code_parametername},");
            scriptBuilder.AppendLine("update_count = ISNULL(update_count, 0) + 1");
            scriptBuilder.AppendLine();
            scriptBuilder.AppendLine($"    WHERE {primaryKeyColumn_name} = @{primaryKeyColumn_name};");

            // Build End Update  statement
            scriptBuilder.AppendLine("END");


            scriptBuilder.AppendLine($"Select * from {tablename_} Where {pk_column}= @{pk_column}");


            scriptBuilder.AppendLine("COMMIT TRAN");
            scriptBuilder.AppendLine("END TRY");
            scriptBuilder.AppendLine("BEGIN CATCH");
            scriptBuilder.AppendLine("ROLLBACK TRAN");
            scriptBuilder.AppendLine("SELECT @ErrorDetails = ERROR_Message(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE()");
            scriptBuilder.AppendLine("RAISERROR(@ErrorDetails, @ErrorSeverity, @ErrorState)");
            scriptBuilder.AppendLine("END CATCH");
            scriptBuilder.AppendLine("END");
            scriptBuilder.AppendLine("GO");

            return new Tuple<string, string, string, string>(sp_name, scriptBuilder.ToString(), error, pk_column);
        }

        public static Tuple<string, string, string> GenerateDeleteProcedureScript(string schema, string tableName, List<table_design> columns)
        {
            string error = string.Empty;
            string sp_name = $"adp_{tableName}_delete";
            string tablename_ = string.Format("{0}.{1}", schema, tableName);
            StringBuilder scriptBuilder = new StringBuilder();

            string primaryKeyColumn_name = string.Empty;
            string primaryKeyColumn_data_type = string.Empty;
            var primaryKeyColumn = columns.Where(n => n._is_primary_key.Equals(true)).FirstOrDefault();
            if (primaryKeyColumn != null)
            {
                primaryKeyColumn_name = primaryKeyColumn._column_name;
                primaryKeyColumn_data_type = primaryKeyColumn._data_type;
            }
            else
            {
                primaryKeyColumn_name = columns.FirstOrDefault()._column_name;
                primaryKeyColumn_data_type = columns.FirstOrDefault()._data_type;
            }

            // Create procedure header
            scriptBuilder.AppendLine($"CREATE PROCEDURE [{schema}].{sp_name}");
            scriptBuilder.AppendLine($"    @{primaryKeyColumn_name} {primaryKeyColumn_data_type},");
            scriptBuilder.AppendLine($"    @{user_login_code_parametername.ToLower().Trim()} {user_login_code_parametername_data_type}");

            scriptBuilder.AppendLine("AS");
            scriptBuilder.AppendLine("BEGIN");
            scriptBuilder.AppendLine("SET NOCOUNT ON");
            scriptBuilder.AppendLine("SET XACT_ABORT ON");
            scriptBuilder.AppendLine("DECLARE @ErrorDetails VARCHAR(MAX),@ErrorSeverity SMALLINT, @ErrorState SMALLINT");
            scriptBuilder.AppendLine("BEGIN TRY");
            scriptBuilder.AppendLine("BEGIN TRAN");

            scriptBuilder.AppendLine();

            // Build DELETE statement
            scriptBuilder.AppendLine($"   UPDATE {tablename_}");
            scriptBuilder.AppendLine("    SET");
            scriptBuilder.AppendLine($"   deleted_on=GETDATE(),deleted_by=@{user_login_code_parametername}");
            scriptBuilder.AppendLine($"    WHERE {primaryKeyColumn_name} = @{primaryKeyColumn_name};");
            // Build DELETE statement

            scriptBuilder.AppendLine($"COMMIT TRAN");
            scriptBuilder.AppendLine($"END TRY");
            scriptBuilder.AppendLine($"BEGIN CATCH");
            scriptBuilder.AppendLine($"ROLLBACK TRAN");
            scriptBuilder.AppendLine($"SELECT @ErrorDetails = ERROR_Message(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE()");
            scriptBuilder.AppendLine($"RAISERROR(@ErrorDetails, @ErrorSeverity, @ErrorState)");
            scriptBuilder.AppendLine($"END CATCH");
            scriptBuilder.AppendLine("END");
            scriptBuilder.AppendLine("GO");

            return new Tuple<string, string, string>(sp_name, scriptBuilder.ToString(), error);
        }


        public static Tuple<string, string, string> GenerateReadProcedureScript(string schema, string tableName, List<table_design> columns, string view_name)
        {
            string error = string.Empty;
            string sp_name = $"adp_{tableName}_read";
            string tablename_ = string.Format("{0}.{1}", schema, tableName);

            int table_count = 0;

            Dictionary<string, string> _dic_table_alise = new Dictionary<string, string>();

            _dic_table_alise.Add(tableName, string.Format("{0}{1}", "t", table_count));

            StringBuilder scriptBuilder = new StringBuilder();

            string primaryKeyColumn_name = string.Empty;
            string primaryKeyColumn_data_type = string.Empty;
            var primaryKeyColumn = columns.Where(n => n._is_primary_key.Equals(true)).FirstOrDefault();
            if (primaryKeyColumn != null)
            {
                primaryKeyColumn_name = primaryKeyColumn._column_name;
                primaryKeyColumn_data_type = primaryKeyColumn._data_type;
            }
            else
            {
                primaryKeyColumn_name = columns.FirstOrDefault()._column_name;
                primaryKeyColumn_data_type = columns.FirstOrDefault()._data_type;
            }

            // Create procedure header
            scriptBuilder.AppendLine($"CREATE PROCEDURE [{schema}].{sp_name}");
            scriptBuilder.AppendLine($"    @{primaryKeyColumn_name} {primaryKeyColumn_data_type}");
            //scriptBuilder.AppendLine($"    @{user_login_code_parametername.ToLower().Trim()} {user_login_code_parametername_data_type}=NULL");

            scriptBuilder.AppendLine("AS");
            scriptBuilder.AppendLine("BEGIN");
            scriptBuilder.AppendLine("SET NOCOUNT ON");
            scriptBuilder.AppendLine("SET XACT_ABORT ON");
            scriptBuilder.AppendLine("DECLARE @ErrorDetails VARCHAR(MAX),@ErrorSeverity SMALLINT, @ErrorState SMALLINT");
            scriptBuilder.AppendLine("BEGIN TRY");
            scriptBuilder.AppendLine("BEGIN TRAN");
            scriptBuilder.AppendLine();

            // Build SELECT statement
            scriptBuilder.AppendLine("SELECT * ");
            //foreach (var column in columns)
            //{
            //    scriptBuilder.AppendLine($"    {_dic_table_alise[tableName]}.{column._column_name} AS {column._column_name}{((column._column_name.ToLower().Trim().Contains("value") || column._is_primary_key == false) ? string.Empty : ((column._is_value_field_required == true) ? "_value" : ""))},");
            //    if (column._column_relation != null)
            //    {
            //        if (column._column_relation._table_name != null && Convert.ToString(column._column_relation._table_name).Length > 3)
            //        {
            //            scriptBuilder.AppendLine($"    {column._column_relation._table_alias}.{column._column_relation._target_refer_column_name} AS {column._column_relation._target_refer_column_name}_display,");
            //        }
            //    }
            //}

            //scriptBuilder.Length -= 3;

            //scriptBuilder.AppendLine($"  FROM {view_name} {_dic_table_alise[tableName]}");
            scriptBuilder.AppendLine($"  FROM {view_name} ");

            //foreach (var column in columns)
            //{
            //    if (column._column_relation != null)
            //    {
            //        if (column._column_relation._table_name != null && Convert.ToString(column._column_relation._table_name).Length > 3)
            //        {
            //            scriptBuilder.AppendLine($"    LEFT OUTER JOIN {column._column_relation._table_name} {column._column_relation._table_alias} ON ");
            //            scriptBuilder.AppendLine($"        {_dic_table_alise[tableName]}.{column._column_name} = {column._column_relation._table_alias}.{column._column_relation._target_refer_column_name}");
            //        }
            //    }
            //}
            //scriptBuilder.AppendLine($"    WHERE {_dic_table_alise[tableName]}.{primaryKeyColumn_name} = @{primaryKeyColumn_name};");
            scriptBuilder.AppendLine($"    WHERE {primaryKeyColumn_name} = @{primaryKeyColumn_name};");



            scriptBuilder.AppendLine("COMMIT TRAN");
            scriptBuilder.AppendLine("END TRY");
            scriptBuilder.AppendLine("BEGIN CATCH");
            scriptBuilder.AppendLine("ROLLBACK TRAN");
            scriptBuilder.AppendLine("SELECT @ErrorDetails = ERROR_Message(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE()");
            scriptBuilder.AppendLine("RAISERROR(@ErrorDetails, @ErrorSeverity, @ErrorState)");
            scriptBuilder.AppendLine("END CATCH");
            scriptBuilder.AppendLine("END");
            scriptBuilder.AppendLine("GO");

            return new Tuple<string, string, string>(sp_name, scriptBuilder.ToString(), error);
        }


        // To Generate Insert , Update & Delete Procedure 

        public static string GenerateObjectScript(List<string> object_lists)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                foreach (var item in object_lists)
                {
                    result.AppendLine("GO");
                    result.AppendLine("");
                    result.AppendLine("GO");
                    result.AppendLine("");
                    result.AppendLine(item);
                    result.AppendLine("");
                    result.AppendLine("GO");
                    result.AppendLine("");
                    result.AppendLine("GO");
                }

            }
            catch (Exception)
            {

                throw;
            }
            return result.ToString();

        }

        public static void ExecuteNonQuery(SqlConnection connection, string sql)
        {
            try
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }

        }

        public static string GetProcedureDefinition(SqlConnection connection, string procedureName)
        {
            try
            {
                string query = $"sp_helptext '{procedureName}'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return null;
                        }

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        while (reader.Read())
                        {
                            sb.Append(reader.GetString(0));
                        }

                        return sb.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public static void RenameStoredProcedure(string oldProcedureName, string newProcedureName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Step 1: Retrieve the definition of the existing stored procedure
                string procedureDefinition = GetProcedureDefinition(connection, oldProcedureName);
                if (procedureDefinition == null)
                {
                    Console.WriteLine("Stored procedure not found.");
                    return;
                }
                // Step 2: Replace the old stored procedure name with the new one
                string newProcedureDefinition = procedureDefinition.Replace(oldProcedureName, newProcedureName);

                // Step 3: Create the new stored procedure
                string NewprocedureDefinition = GetProcedureDefinition(connection, newProcedureName);
                if (NewprocedureDefinition == null)
                {
                    ExecuteNonQuery(connection, newProcedureDefinition);
                }
                else
                {
                    Console.WriteLine("Stored procedure not found.");
                    return;
                }


                // Step 4: Drop the old stored procedure
                string dropOldProcedure = $"DROP PROCEDURE {oldProcedureName}";
                ExecuteNonQuery(connection, dropOldProcedure);

                Console.WriteLine("Stored procedure renamed successfully.");
            }
        }
    }


    public class table_design
    {

        public table_design() { }

        public table_design(string column_name, string data_type, column_relation column_relation_ = null, bool? is_primary_key = false, bool? is_value_field_required = false)
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

        public bool IsNullable { get; set; }
        public bool? _is_primary_key { get; set; }
        public bool? _is_display_field_required { get; set; }
        public bool? _is_value_field_required { get; set; }

        public column_relation _column_relation { get; set; }

        public string _preferred_controls { get; set; }

        public table_design(string column_name, string data_type, string max_length, string numeric_precision, string numeric_scale)
        {
            this._column_name = column_name;
            this._data_type = GetSqlType(data_type, max_length, numeric_precision, numeric_scale);
            this._preferred_controls = Get_preferred_controls(data_type, max_length, max_length, numeric_scale);
        }

        static string GetSqlType(string dataType, string max_length, string numeric_precision, string numeric_scale)
        {
            switch (dataType.ToLower())
            {
                case "int":
                    return "INT";
                case "nvarchar":
                case "varchar":
                case "char":
                    return "NVARCHAR(" + (string.IsNullOrEmpty(max_length) ? "MAX" : max_length) + ")";
                case "datetime":
                    return "DATETIME";
                case "decimal":
                    if (!string.IsNullOrEmpty(numeric_precision) && !string.IsNullOrEmpty(numeric_scale))
                    {
                        int precision = int.Parse(numeric_precision);
                        int scale = int.Parse(numeric_scale);
                        if (precision <= 9 && scale == 0)
                            return "INT";
                        else if (precision <= 19 && scale == 0)
                            return "BIGINT";
                        else
                            return "DECIMAL(" + numeric_precision + ", " + numeric_scale + ")";
                    }
                    else
                    {
                        return "DECIMAL";
                    }
                case "float":
                    return "FLOAT";
                case "bit":
                    return "BIT";
                default:
                    return dataType.ToUpper();
            }
        }

        static string Get_preferred_controls(string dataType, string max_length, string numeric_precision, string numeric_scale)
        {
            switch (dataType.ToLower())
            {
                case "int":
                    return "txtbox";
                case "datetime":
                    return "date-picker";
                case "bit":
                    return "switch";
                case "select":
                    return "select";
                default:
                    return "txtbox";
            }
        }


        public List<table_design> GetTable_Designs(DataTable dataTabledt)
        {
            var result = new List<table_design>();
            try
            {
                if (dataTabledt != null)
                {
                    for (int i = 0; i < dataTabledt.Rows.Count; i++)
                    {
                        result.Add(new table_design(Convert.ToString(dataTabledt.Rows[i]["COLUMN_NAME"]), Convert.ToString(dataTabledt.Rows[i]["DATA_TYPE"]), Convert.ToString(dataTabledt.Rows[i]["CHARACTER_MAXIMUM_LENGTH"]), Convert.ToString(dataTabledt.Rows[i]["NUMERIC_PRECISION"]), Convert.ToString(dataTabledt.Rows[i]["NUMERIC_SCALE"])));
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;


        }



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
}
