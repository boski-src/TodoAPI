namespace TodoAPI.Contexts.Todos
{
    public static class TodoFilter
    {
        public const string Today = "today";
        public const string Tommorow = "tommorow";
        public const string Week = "week";

        public static bool IsValid(string value) => value == Today || value == Tommorow || value == Week;
    }
}