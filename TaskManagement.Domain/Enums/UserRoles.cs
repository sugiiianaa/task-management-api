namespace TaskManagement.Domain.Enums
{
    public enum UserRoles
    {
        User = 0,
        Admin = 1,
        SuperUser = 2
    }

    public static class RoleHelper
    {
        // Mapping from enum to string 
        private static readonly Dictionary<UserRoles, string> RolesToString = new Dictionary<UserRoles, string>
        {
            { UserRoles.User, "user" },
            { UserRoles.Admin, "admin" },
            { UserRoles.SuperUser, "super_user" }
        };

        // Mapping from string to enum
        private static readonly Dictionary<string, UserRoles> StringToRole = RolesToString
            .ToDictionary(kvp => kvp.Value.ToLower(), kvp => kvp.Key);

        // Get the string representation for a UserRole enum
        public static string GetRole(UserRoles role)
        {
            return RolesToString[role];
        }

        // Convert string to a UserRol enum value
        public static UserRoles? GetRoleFromString(string role)
        {
            if (string.IsNullOrEmpty(role)) return null;

            var normalizedRole = role.ToLower();

            return StringToRole.TryGetValue(normalizedRole, out UserRoles value) ? value : null;
        }
    }
};
