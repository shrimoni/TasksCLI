using System;

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

        public static void UpdateSubTaskDetails(SubTask task, string taskDetails)
        {
            if (task == null)
                return;

            task.taskDetails = taskDetails;
        }

        public static void UpdateSubTaskPriority(SubTask task, string priority)
        {
            if (task == null)
                return;

            task.priority= (Priority)Enum.Parse(typeof(Priority), priority);
        }

        public static void UpdateSubTaskStatus(SubTask task, string status)
        {
            if (task == null)
                return;

            task.status= (Status)Enum.Parse(typeof(Status), status);
        }
    }
}
