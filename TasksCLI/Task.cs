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

        public static void ShowTask(Task task)
        {
            if (task == null)
            {
                Console.WriteLine("Task not found! Try to add a task and try again!");
                return;
            }

            var columns = new string[] { "Task", "Task ID", "Date Created" };
            var table = new ConsoleTable(columns, 20);
            table.CreateColumn();
            table.CreateRow(new string[] { task.name, task.taskID.ToString(), task.dateCreated }, true);

            if (task.subTasks.Count > 0)
            {
                foreach (var subTask in task.subTasks)
                {
                    Console.WriteLine("-{0}", subTask.taskDetails);
                }
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
            var columns = new string[] { "Task", "Task ID", "Date Created" };
            var table = new ConsoleTable(columns, 20);
            table.CreateColumn();
            for (var i = 0; i < tasks.Count; i++)
            {
                var isLastRow = false;

                if (i == tasks.Count - 1)
                    isLastRow = true;

                table.CreateRow(new string[] { tasks[i].name, tasks[i].taskID.ToString(), tasks[i].dateCreated}, isLastRow);
                //if (task.subTasks.Count > 0)
                //{
                //    foreach (var subTask in task.subTasks)
                //    {
                //        Console.WriteLine("-{0}", subTask.taskDetails);
                //    }
                //}
            }
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
    }
}
