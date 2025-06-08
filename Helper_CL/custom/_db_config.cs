using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper_CL.custom
{
    internal class _db_config
    {
        public string instance { get; set; }
        public string name { get; set; }
        public string user_id { get; set; }
        public string password { get; set; }

    }

    public class _common
    {
        public enum roles
        {
            admin
        }
    }
}
