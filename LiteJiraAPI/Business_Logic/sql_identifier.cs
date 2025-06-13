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
            adp_get_companymaster_help,
            adp_get_designation_help
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

        public enum tagmaster
        {
            adp_tagmaster_select,
            adp_tagmaster_insert_or_update,
            adp_tagmaster_read,
            adp_tagmaster_delete,
            adp_get_tagmaster_help
        }

        public enum task
        {
            adp_task_select,
            adp_task_insert_or_update,
            adp_task_read,
            adp_task_delete,
            adp_get_tagmaster_help,
            adp_get_projectmaster_help,
            adp_get_tasktype_help,
            adp_get_prioritymaster_help
        }

    }
}
