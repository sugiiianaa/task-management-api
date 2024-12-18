namespace TaskManagement.Domain.Enums
{
    public enum TaskStatus
    {
        Todo,
        WorkingOn,
        Finished
    }

    public static class TaskHelper
    {
        private static readonly Dictionary<TaskStatus, string> Status = new Dictionary<TaskStatus, string>
        {
            {TaskStatus.Todo, "to_do" },
            {TaskStatus.WorkingOn, "working_on" },
            {TaskStatus.Finished, "finished" },
        };

        public static string GetStatus(TaskStatus status)
        {
            return Status[status];
        }
    }
}
