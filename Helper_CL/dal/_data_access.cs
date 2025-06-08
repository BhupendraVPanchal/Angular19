﻿using Helper_CL.custom;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper_CL.dal
{
    public static class _data_access
    {

        #region "DataTable Related"
        private static DataTable GetDataTable(SqlCommand cmd, int TimeOut = -1)
        {
            if (cmd.Connection == null) cmd.Connection = new SqlConnection(_site_config.GetDBConnectionString());
            if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            using (DataTable dt = new DataTable())
            {
                sda.Fill(dt);
                //cmd.Connection.Close();
                return dt;
            }
        }

        private static DataTable GetDataTable(SqlCommand cmd, string strConn, int TimeOut = -1)
        {
            if (cmd.Connection == null) cmd.Connection = new SqlConnection(strConn);
            if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            using (DataTable dt = new DataTable())
            {
                sda.Fill(dt);
                //cmd.Connection.Close();
                return dt;
            }
        }

        public static DataTable GetDataTable(string qry, bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                return GetDataTable(cmd);
            }
        }

        public static DataTable GetDataTable(string qry, JObject data, bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                List<SqlParameter> sp = GetParameterList(data, null, null);
                cmd.Parameters.AddRange(sp.ToArray());
                return GetDataTable(cmd);
            }
        }

        public static DataTable GetDataTable(string qry, JObject data, List<string> ExcPara, bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                List<SqlParameter> sp = GetParameterList(data, ExcPara, null);
                cmd.Parameters.AddRange(sp.ToArray());
                return GetDataTable(cmd);
            }
        }

        public static DataTable GetDataTable(string qry, Dictionary<string, object> IncPara, bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                List<SqlParameter> sp = GetParameterList(null, null, IncPara);
                cmd.Parameters.AddRange(sp.ToArray());
                return GetDataTable(cmd);
            }
        }

        public static DataTable GetDataTable(string qry, JObject data, Dictionary<string, object> IncPara, bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                List<SqlParameter> sp = GetParameterList(data, null, IncPara);
                cmd.Parameters.AddRange(sp.ToArray());
                return GetDataTable(cmd);
            }
        }

        public static DataTable GetDataTable(string qry, JObject data, Dictionary<string, object> IncPara, List<string> ExcPara, bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                List<SqlParameter> sp = GetParameterList(data, ExcPara, IncPara);
                cmd.Parameters.AddRange(sp.ToArray());
                return GetDataTable(cmd);
            }
        }

        public static DataTable GetDataTable(string qry, JObject data, Dictionary<string, object> IncPara, DataSet ds, List<string> ExcPara,
            bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                List<SqlParameter> sp = GetParameterList(data, ExcPara, IncPara, ds);
                cmd.Parameters.AddRange(sp.ToArray());
                return GetDataTable(cmd);
            }
        }

        public static DataTable GetDataTable(string qry, JObject data, Dictionary<string, object> IncPara, List<string> ExcPara,
             string whichConn, bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                List<SqlParameter> sp = GetParameterList(data, ExcPara, IncPara);
                cmd.Parameters.AddRange(sp.ToArray());
                return GetDataTable(cmd, _site_config.GetDBConnectionString(whichConn));
            }
        }

        public static DataTable GetDataTable(string connection, string qry, JObject data, Dictionary<string, object> IncPara, List<string> ExcPara, bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                List<SqlParameter> sp = GetParameterList(data, ExcPara, IncPara);
                cmd.Parameters.AddRange(sp.ToArray());
                return GetDataTable(cmd, connection);
            }
        }

        public static DataTable GetDataTable(string qry, JObject data, Dictionary<string, object> IncPara, DataSet ds, List<string> ExcPara,
             string whichConn, bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                List<SqlParameter> sp = GetParameterList(data, ExcPara, IncPara, ds);
                cmd.Parameters.AddRange(sp.ToArray());
                return GetDataTable(cmd, _site_config.GetDBConnectionString(whichConn));
            }
        }

        public static DataTable GetDataTable(string DBName, string qry, JObject data, Dictionary<string, object> IncPara, DataSet ds, List<string> ExcPara,
             string whichConn, bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                List<SqlParameter> sp = GetParameterList(data, ExcPara, IncPara, ds);
                cmd.Parameters.AddRange(sp.ToArray());
                return GetDataTable(cmd, _site_config.GetDBConnectionString(whichConn, DBName));
            }
        }

        #endregion

        #region "DataSet Related"

        private static DataSet GetDataSet(SqlCommand cmd, int TimeOut = -1)
        {
            if (cmd.Connection == null) cmd.Connection = new SqlConnection(_site_config.GetDBConnectionString());
            if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            using (DataSet ds = new DataSet())
            {
                sda.Fill(ds);
                //cmd.Connection.Close();
                return ds;
            }
        }

        private static DataSet GetDataSet(SqlCommand cmd, string strConn, int TimeOut = -1)
        {
            if (cmd.Connection == null) cmd.Connection = new SqlConnection(strConn);
            if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            using (DataSet ds = new DataSet())
            {
                sda.Fill(ds);
                //cmd.Connection.Close();
                return ds;
            }
        }

        public static DataSet GetDataSet(string qry, bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                return GetDataSet(cmd);
            }
        }

        public static DataSet GetDataSet(string connection, string qry, JObject data, Dictionary<string, object> IncPara, List<string> ExcPara, bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                List<SqlParameter> sp = GetParameterList(data, ExcPara, IncPara);
                cmd.Parameters.AddRange(sp.ToArray());
                return GetDataSet(cmd, connection);
            }
        }

        public static DataSet GetDataSet(string qry, JObject data, bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                List<SqlParameter> sp = GetParameterList(data, null, null);
                cmd.Parameters.AddRange(sp.ToArray());
                return GetDataSet(cmd);
            }
        }

        public static DataSet GetDataSet(string qry, JObject data, List<string> ExcPara, bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                List<SqlParameter> sp = GetParameterList(data, ExcPara, null);
                cmd.Parameters.AddRange(sp.ToArray());
                return GetDataSet(cmd);
            }
        }

        public static DataSet GetDataSet(string qry, Dictionary<string, object> IncPara, bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                List<SqlParameter> sp = GetParameterList(null, null, IncPara);
                cmd.Parameters.AddRange(sp.ToArray());
                return GetDataSet(cmd);
            }
        }

        public static DataSet GetDataSet(string qry, JObject data, Dictionary<string, object> IncPara, List<string> ExcPara, bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                List<SqlParameter> sp = GetParameterList(data, ExcPara, IncPara);
                cmd.Parameters.AddRange(sp.ToArray());
                return GetDataSet(cmd);
            }
        }

        public static DataSet GetDataSet(string qry, JObject data, Dictionary<string, object> IncPara, DataSet ds, List<string> ExcPara,
            bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                List<SqlParameter> sp = GetParameterList(data, ExcPara, IncPara, ds);
                cmd.Parameters.AddRange(sp.ToArray());
                return GetDataSet(cmd);
            }
        }

        public static DataSet GetDataSet(string qry, JObject data, Dictionary<string, object> IncPara, List<string> ExcPara,
             string whichConn, bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                List<SqlParameter> sp = GetParameterList(data, ExcPara, IncPara);
                cmd.Parameters.AddRange(sp.ToArray());
                return GetDataSet(cmd, _site_config.GetDBConnectionString(whichConn));
            }
        }

        public static DataSet GetDataSet(string DBName, string qry, JObject data, Dictionary<string, object> IncPara, List<string> ExcPara,
             string whichConn, bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                List<SqlParameter> sp = GetParameterList(data, ExcPara, IncPara);
                cmd.Parameters.AddRange(sp.ToArray());
                return GetDataSet(cmd, _site_config.GetDBConnectionString(whichConn, DBName));
            }
        }

        public static DataSet GetDataSet(string qry, JObject data, Dictionary<string, object> IncPara, DataSet ds, List<string> ExcPara,
             string whichConn, bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                List<SqlParameter> sp = GetParameterList(data, ExcPara, IncPara, ds);
                cmd.Parameters.AddRange(sp.ToArray());
                return GetDataSet(cmd, _site_config.GetDBConnectionString(whichConn));
            }
        }

        public static DataSet GetDataSet(string DBName, string qry, JObject data, Dictionary<string, object> IncPara, DataSet ds, List<string> ExcPara,
             string whichConn, bool isSP = true, int TimeOut = -1)
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                if (isSP) cmd.CommandType = CommandType.StoredProcedure;
                if (TimeOut != -1) cmd.CommandTimeout = TimeOut;
                List<SqlParameter> sp = GetParameterList(data, ExcPara, IncPara, ds);
                cmd.Parameters.AddRange(sp.ToArray());
                return GetDataSet(cmd, _site_config.GetDBConnectionString(whichConn, DBName));
            }
        }

        #endregion

        #region "get Parameter List"
        private static List<SqlParameter> GetParameterList(JObject data, List<string> ExcludeParams = null, Dictionary<string, object> IncludeParams = null, DataSet ds = null)
        {
            List<SqlParameter> Rtn = new List<SqlParameter>();
            try
            {

                if (ExcludeParams == null)
                {
                    ExcludeParams = new List<string>();
                }

                if (IncludeParams != null)
                {
                    foreach (var obj in IncludeParams)
                    {
                        if (ExcludeParams.Where(x => x.ToLower() == obj.Key.ToLower()).Count() > 0)
                        {
                            continue;
                        }

                        if (obj.Value == null)
                        {
                            SqlParameter dbPara1 = new SqlParameter(obj.Key.ToString().Trim(), DBNull.Value);
                            Rtn.Add(dbPara1);
                        }
                        else if (obj.Value.ToString() == "null" || obj.Value.ToString() == "undefined" || obj.Value.ToString() == "''" || obj.Value.ToString() == "" || obj.Value.ToString() == "{}")
                        {
                            SqlParameter dbPara1 = new SqlParameter(obj.Key.ToString().Trim(), DBNull.Value);
                            Rtn.Add(dbPara1);
                        }
                        else
                        {
                            SqlParameter dbPara1 = new SqlParameter(obj.Key.ToString().Trim(), obj.Value.ToString());
                            Rtn.Add(dbPara1);
                        }
                    }
                }

                if (data != null)
                {
                    foreach (var obj in data)
                    {
                        if (ExcludeParams.Where(x => x.ToLower() == obj.Key.ToLower()).Count() > 0)
                        {
                            continue;
                        }

                        if (obj.Value == null)
                        {
                            SqlParameter dbPara1 = new SqlParameter(obj.Key.ToString().Trim(), DBNull.Value);
                            Rtn.Add(dbPara1);
                        }
                        else if (obj.Value.ToString() == "null" || obj.Value.ToString() == "undefined" || obj.Value.ToString() == "''" || obj.Value.ToString() == "" || obj.Value.ToString() == "{}")
                        {
                            SqlParameter dbPara1 = new SqlParameter(obj.Key.ToString().Trim(), DBNull.Value);
                            Rtn.Add(dbPara1);
                        }
                        else
                        {
                            SqlParameter dbPara1 = new SqlParameter(obj.Key.ToString().Trim(), obj.Value.ToString());
                            Rtn.Add(dbPara1);
                        }
                    }
                }

                if (ds != null)
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            SqlParameter dbPara1 = new SqlParameter(dt.TableName, dt);
                            Rtn.Add(dbPara1);
                        }
                    }
                }
                return Rtn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Rtn = null;
            }
        }
        #endregion

    }
}
