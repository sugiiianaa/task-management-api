namespace TaskManagement.Domain.Enums
{
    public enum UserTaskStatus
    {
        Todo = 0,
        WorkingOn = 1,
        Finished = 2
    }

    public static class TaskHelper
    {
        // Mapping from enum to string
        private static readonly Dictionary<UserTaskStatus, string> StatusToString = new Dictionary<UserTaskStatus, string>
        {
            {UserTaskStatus.Todo, "to_do" },
            {UserTaskStatus.WorkingOn, "working_on" },
            {UserTaskStatus.Finished, "finished" },
        };

        // Mapping from string to enum
        private static readonly Dictionary<string, UserTaskStatus> StringToStatus = StatusToString
            .ToDictionary(kvp => kvp.Value.ToLower(), kvp => kvp.Key);

        // Get the string representation for a UserTaskStatus enum
        public static string GetStatus(UserTaskStatus status)
        {
            return StatusToString[status];
        }

        // Convert string to a UserTaskStatus enum value
        public static UserTaskStatus? GetStatusFromString(string status)
        {
            if (string.IsNullOrEmpty(status)) return null;

            var normalizedStatus = status.ToLower();

            return StringToStatus.TryGetValue(normalizedStatus, out UserTaskStatus value) ? value : (UserTaskStatus?)null;
        }
    }
}
