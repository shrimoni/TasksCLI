namespace TasksCLI
{
    public enum Priority
    {
        Low = 0,
        Medium = 1,
        High = 2
    }

    public enum Status
    {
        Todo = 0,
        InProgress = 1,
        Done = 2
    }

    public class SubTask
    {
        public int subTaskID;
        public string taskDetails;
        public Priority priority;
        public Status status;

    }
}
