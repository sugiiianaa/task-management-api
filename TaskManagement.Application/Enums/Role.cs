namespace TaskManagement.Application.Enums
{
    public enum Role
    {
        Admin,
        User,
        SuperUser
    }

    public static class RoleHelper
    {
        private static readonly Dictionary<Role, string> RoleNames = new Dictionary<Role, string>
        {
            {Role.SuperUser, "super_user" },
            {Role.Admin, "admin" },
            {Role.User, "user" },
        };

        public static string GetRoleName(Role role)
        {
            return RoleNames[role];
        }
    }
}
