using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTAGenerator
{

    public static class UtilityScript
    {


        public const string create_update_key = "create_update";
        public const string read_key = "read";
        public const string delete_key = "delete";
        public const string view_key = "view";
        

        public static string physicalPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Angular");

        public static string angular_codepath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Angular");

        public static string api_codepath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Controllers");

        public static void create_file(string file_path, string file_name, string file_content)
        {
            try
            {
                if (!Directory.Exists(file_path))
                {
                    Directory.CreateDirectory(file_path);
                }
                string fullPath = Path.Combine(file_path, file_name);
                File.WriteAllText(fullPath, file_content);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static string ConvertToTitleCase(string input)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(input.ToLower());
        }

        public static string ConvertToSentenceCase(string input)
        {
            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }

        

    }
}
