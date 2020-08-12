using System;
using System.Collections.Generic;

namespace TasksCLI
{
    public class Task
    {
        public int taskID;
        public string name;
        public List<SubTask> subTasks = new List<SubTask>();
        public DateTime dateCreated;

        public static Task CreateTask(string taskName)
        {
            var task = new Task()
            {
                name = taskName,
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

            Console.WriteLine();
            Console.WriteLine("----------------------------");
            Console.WriteLine("Task");
            Console.WriteLine("----------------------------");
            Console.WriteLine(task.name);
            if (task.subTasks.Count > 0)
            {
                foreach (var subTask in task.subTasks)
                {
                    Console.WriteLine("-{0}", subTask.taskDetails);
                }
            }
            Console.WriteLine("----------------------------");
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
            Console.WriteLine("----------------------------");
            Console.WriteLine("Tasks");
            Console.WriteLine("----------------------------");
            foreach (var task in tasks)
            {
                Console.WriteLine(task.name);
                if (task.subTasks.Count > 0)
                {
                    foreach (var subTask in task.subTasks)
                    {
                        Console.WriteLine("-{0}", subTask.taskDetails);
                    }
                }
                Console.WriteLine("----------------------------");
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
