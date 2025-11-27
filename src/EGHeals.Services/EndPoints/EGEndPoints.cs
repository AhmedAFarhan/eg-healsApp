namespace EGHeals.Services.EndPoints
{
    public static class EGEndPoints
    {
        private const string baseAuthApi = "api/Auth";
        private const string baseUsersApi = "api/users";
        private const string baseRolesApi = "api/roles";

        public static class Auth
        {
            public const string Login = $"{baseAuthApi}/Login";
        }

        public static class Users
        {
           
            public const string RegisterSubUser = $"{baseUsersApi}/RegisterSubUser";
            public const string GetSubUsers = $"{baseUsersApi}/GetSubUsers";

            public static string GetSubUserPermissions(Guid id) => $"{baseUsersApi}/{id}/GetSubUserPermissions";
            public static string Delete(Guid id) => $"{baseUsersApi}/{id}";
            public static string Activate(Guid id) => $"{baseUsersApi}/{id}/Activate";
            public static string Deactivate(Guid id) => $"{baseUsersApi}/{id}/Deactivate";
            public static string UpdateSubUser(Guid id) => $"{baseUsersApi}/{id}/UpdateSubUser";
            public static string UpdateSubUserPermissions(Guid id) => $"{baseUsersApi}/{id}/UpdateSubUserPermissions";            
        }

        public static class Roles
        {
            public const string GetAll = $"{baseRolesApi}";
        }
    }
}
