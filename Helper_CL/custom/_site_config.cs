using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Data.Common;

namespace Helper_CL.custom
{
    public static class _site_config
    {


        private static IConfiguration _config;
        private static IConfiguration _configuration
        {
            get
            {
                var builder = new ConfigurationBuilder()
                    //.SetBasePath(Directory.GetCurrentDirectory())
                    //.SetBasePath(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true);

                _config = builder.Build();
                return _config;
            }
        }


        public static string GetDBConnectionString()
        {
            return GetDBConnectionString("Connection");
        }

        public static string GetDBConnectionString(string strWhichConn)
        {
            try
            {
                return _configuration.GetConnectionString(strWhichConn);
            }
            catch
            {
                throw new Exception("Could not find [" + strWhichConn + "] in <connectionStrings> section of your web.config file." + Environment.NewLine + "Please check your web.config file.");
            }
        }

        public static string GetDBConnectionString(string strWhichConn, string strDBName)
        {
            try
            {
                string strConn = _configuration.GetConnectionString(strWhichConn);
                DbConnectionStringBuilder _connStr = new DbConnectionStringBuilder();
                _connStr.ConnectionString = strConn;
                _connStr["Initial Catalog"] = strDBName.ToString();
                return _connStr.ConnectionString.ToString();
            }
            catch
            {
                throw new Exception("Could not find [" + strWhichConn + "] in <connectionStrings> section of your web.config file." + Environment.NewLine + "Please check your web.config file.");
            }
        }


        public static string GetDBConnectionString(JObject _db_instance)
        {
            try
            {
                string strConn = _configuration.GetConnectionString("Connection");
                DbConnectionStringBuilder _connStr = new DbConnectionStringBuilder();
                _connStr.ConnectionString = strConn;
                _connStr["server"] = _db_instance["db_instance_name"].ToString();
                _connStr["database"] = _db_instance["db_name"].ToString();
                _connStr["user id"] = _db_instance["db_user_id"].ToString();
                _connStr["password"] = _db_instance["db_password"].ToString();
                return _connStr.ConnectionString.ToString();
            }
            catch
            {
                throw new Exception("Could not find Connection in <connectionStrings> section of your web.config file." + Environment.NewLine + "Please check your web.config file.");
            }
        }
        public static string GetDBConnectionString(_token _db_instance)
        {
            try
            {
                string strConn = _configuration.GetConnectionString("Connection");
                DbConnectionStringBuilder _connStr = new DbConnectionStringBuilder();
                _connStr.ConnectionString = strConn;
                //_connStr["Server"] = _db_instance.db_instance_name.ToString();
                //_connStr["Database"] = _db_instance.db_name.ToString();
                //_connStr["User Id"] = _db_instance.db_user_id.ToString();
                //_connStr["Password"] = _db_instance.db_password.ToString();
                return _connStr.ConnectionString.ToString();
            }
            catch
            {
                throw new Exception("Could not find Connection in <connectionStrings> section of your web.config file." + Environment.NewLine + "Please check your web.config file.");
            }
        }

        public static string GetConfigValue(string key)
        {
            try
            {
                return _configuration.GetSection("appSettings").GetChildren().FirstOrDefault(config => config.Key == key).Value;
            }
            catch
            {
                throw new Exception("Could not find key [" + key + "] in your web.config file." + Environment.NewLine + "Please check your web.config file.");
            }
        }

        public static string GetConfigValue(string section, string key)
        {
            try
            {
                return _configuration.GetSection(section).GetChildren().FirstOrDefault(config => config.Key == key).Value;
            }
            catch
            {
                throw new Exception("Could not find key [" + key + "] in your web.config file." + Environment.NewLine + "Please check your web.config file.");
            }
        }

        //public static string GetConfigValue(string key)
        //{
        //    try
        //    {
        //        return _configuration.GetSection(key).ToString();
        //    }
        //    catch
        //    {
        //        throw new Exception("Could not find key [" + key + "] in your web.config file." + Environment.NewLine + "Please check your web.config file.");
        //    }
        //}

        public static string get_ftp_full_path(string section)
        {
            string result = string.Empty;
            try
            {
                string ftp_endpoint = GetConfigValue("ftp", "ftp_endpoint");
                string section_path = GetConfigValue("ftp", section);
                result = string.Format("{0}/{1}", ftp_endpoint, section_path);
            }
            catch (Exception ex)
            {

            }
            return result;
        }

    }

}
