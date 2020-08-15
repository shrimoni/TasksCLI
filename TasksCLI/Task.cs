using System;
using System.Collections.Generic;
using TasksCLI.Utils;

namespace TasksCLI
{
    public class Task
    {
        public int taskID;
        public string name;
        public List<SubTask> subTasks = new List<SubTask>();
        public int subTaskLastID;
        public string dateCreated;

        public static Task CreateTask(string taskName)
        {
            var task = new Task()
            {
                name = taskName,
                dateCreated = DateTime.Now.ToString("MM/dd/yyyy HH:mm")
            };

            return task;
        }

        public static void AddSubTaskToTask(Task task, string subTaskDetails)
        {
            var subTask = new SubTask();
            subTask.taskDetails = subTaskDetails;
            task.subTasks.Add(subTask);
            subTask.subTaskID = ++task.subTaskLastID;
            Console.WriteLine("Sub tasks added successfully to {0}!", task.name);
        }

        public static void ShowTask(Task task)
        {
            Console.WriteLine("Showing task");
            if (task == null)
            {
                Console.WriteLine("Task not found! Try to add a task and try again!");
                return;
            }

            var columns = new List<string> { "Task", "Task ID", "Date Created" };
            var table = new ConsoleTable();
            table.AddColumn(columns);
            table.AddRow(new List<string>()
            {
                task.name,
                task.taskID.ToString(),
                task.dateCreated.ToString()
            });
            table.ShowTable();
            if (task.subTasks.Count > 0)
            {
                var cols = new List<string> { "Task Details", "Task ID", "Priority", "Status" };
                var subTaskTable = new ConsoleTable();
                subTaskTable.AddColumn(cols);
                foreach (var subTask in task.subTasks)
                {
                    var rowData = new List<string>();
                    rowData.Add(subTask.taskDetails);
                    rowData.Add(subTask.subTaskID.ToString());
                    rowData.Add(subTask.priority.ToString());
                    rowData.Add(subTask.status.ToString());
                    subTaskTable.AddRow(rowData);
                }
                subTaskTable.ShowTable();
            }
            Console.WriteLine();
        }

        public static void ShowAllTasks(List<Task> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks data to show!");
                return;
            }
            Console.WriteLine();
            var columns = new List<string> { "Task", "Task ID", "Date Created" };
            var table = new ConsoleTable();
            table.AddColumn(columns);
            for (var i = 0; i < tasks.Count; i++)
            {
                table.AddRow(new List<string>()
                {
                    tasks[i].name,
                    tasks[i].taskID.ToString(),
                    tasks[i].dateCreated
                });
            }
            table.ShowTable();
            Console.WriteLine();
        }

        public static void DeleteTask(List<Task> tasks, Task task)
        {
            if (tasks.Count == 0 || !tasks.Contains(task) || task == null)
            {
                Console.WriteLine("Either the task is not available or no tasks are available!");
                return;
            }

            Console.WriteLine("Removing task: " + task.name);
            tasks.Remove(task);
            Console.WriteLine("Task removed successfully.");
        }

        public static Task GetTaskByName(List<Task> tasks, string taskName)
        {
            Task task = null;

            if (tasks.Count == 0 || string.IsNullOrWhiteSpace(taskName))
            {
                Console.WriteLine("Cannot return the task! Invalid data!");
                return task;
            }

            for (var i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].name == taskName)
                    task = tasks[i];
            }
            return task;
        }

        public static Task GetTaskById(List<Task> tasks, string taskID)
        {
            Task task = null;

            if (tasks.Count == 0)
            {
                Console.WriteLine("Cannot return the task! Invalid data!");
                return task;
            }

            for (var i = 0; i < tasks.Count; i++)
            {
                try
                {
                    if (tasks[i].taskID == Convert.ToInt32(taskID))
                        task = tasks[i];
                }
                catch (Exception e)
                {
                    Console.WriteLine("Enter valid task id");
                }

            }

            return task;
        }
    }
}
