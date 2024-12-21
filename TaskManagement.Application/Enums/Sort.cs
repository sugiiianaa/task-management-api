namespace TaskManagement.Application.Enums
{
    public enum Sort
    {
        Ascending,
        Descending
    }

    public static class SortHelper
    {
        private static readonly Dictionary<Sort, string> SortOrder = new Dictionary<Sort, string>
        {
            {Sort.Ascending, "Ascending" },
            {Sort.Descending, "Descending" }
        };

        public static string GetSortOrder(Sort sort)
        {
            return SortOrder[sort];
        }
    }
}
