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
            adp_companymaster_delete
        }




    }
}
