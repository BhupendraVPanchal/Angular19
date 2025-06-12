namespace LiteJiraAPI.Business_Logic
{
    public static class sql_identifier
    {
        public enum DbSchema
        {
            sys,
            dbo
        }

        public enum portal_login
        {
            sp_usersignIn,
            sp_update_sessioninfo_token,
            sp_upsert_projectmember
        }

        public enum companymaster
        {
            adp_companymaster_select,
            adp_companymaster_insert_or_update,
            adp_companymaster_read,
            adp_companymaster_delete,
            adp_get_companymaster_help
        }


        public enum projectmember
        {
            adp_projectmember_select,
            adp_projectmember_insert_or_update,
            adp_projectmember_read,
            adp_projectmember_delete
        }

        public enum projectmaster
        {
            adp_projectmaster_select,
            adp_projectmaster_insert_or_update,
            adp_projectmaster_read,
            adp_projectmaster_delete
        }



    }
}
