using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
namespace TasksCLI
{
    class Program
    {
        public static List<Task> tasks = new List<Task>();
        public const string tasksCliVersion = "1.0.0";

        private static string dataFolderPath = "";
        private static string dataFilePath = "";
        private static int taskID;

        static void PerformOpertaionBasedOnUserChoice(int userChoice)
        {
            switch (userChoice)
            {
                case 1:
                    Console.WriteLine("Enter task name to create: ");
                    var name = Console.ReadLine();
                    var task = Task.CreateTask(name);
                    if (task == null)
                    {
                        Console.WriteLine("Unable to create task! Please try again");
                        return;
                    }
                    if (tasks.Count > 1)
                        task.taskID = tasks[tasks.Count - 1].taskID + 1;
                    else
                        task.taskID = ++taskID;
                    tasks.Add(task);
                    Console.WriteLine("Task '{0}' created sucessfully!", task.name);
                    Console.WriteLine();
                    break;
                case 2:
                    Console.WriteLine("Edit a task");
                    Console.WriteLine("Enter the task id to change task name: ");
                    var id = Console.ReadLine();
                    Console.WriteLine("Enter the new task name: ");
                    name = Console.ReadLine();
                    task = Task.GetTaskById(tasks, id);
                    if (task != null && !string.IsNullOrWhiteSpace(name))
                        Task.ChangeTaskName(task, name);
                    break;
                case 3:
                    Console.WriteLine("Delete a task");
                    Console.WriteLine("Enter the task name to delete: ");
                    var taskName = Console.ReadLine();
                    var taskToDelete = Task.GetTaskByName(tasks, taskName);
                    Task.DeleteTask(tasks, taskToDelete);
                    break;
                case 4:
                    Console.WriteLine("Add SubTasks");
                    Console.WriteLine("Enter the task id to add sub tasks: ");
                    id = Console.ReadLine();
                    var taskToAddSubTasks = Task.GetTaskById(tasks, id);
                    Console.WriteLine("Add a subtask or enter to exit");
                    var subTaskDetails = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(subTaskDetails))
                    {
                        break;
                    }

                    Task.AddSubTaskToTask(taskToAddSubTasks, subTaskDetails);
                    break;
                case 5:
                    Console.WriteLine("Edit a SubTask");
                    EditSubTasks();
                    break;
                case 6:
                    Console.WriteLine("Show a Task");
                    Console.WriteLine("Enter task id to display: ");
                    id = Console.ReadLine();
                    task = Task.GetTaskById(tasks, id);
                    if (task != null)
                        Task.ShowTask(task);
                    else
                        Console.WriteLine("Invalid Task ID");
                    break;
                case 7:
                    Console.WriteLine("Show all tasks");
                    Task.ShowAllTasks(tasks);
                    break;
                default:
                    Console.WriteLine("Not a valid option!");
                    break;
            }

            if (tasks.Count != 0 && !string.IsNullOrWhiteSpace(dataFilePath))
            {
                var json = JsonConvert.SerializeObject(tasks);
                //Console.WriteLine(json);
                File.WriteAllText(dataFilePath, json);
            }
        }

        public static void EditSubTasks()
        {
            Console.WriteLine("Enter the task id to edit subtasks");
            var id = Console.ReadLine();
            var task = Task.GetTaskById(tasks, id);
            Console.WriteLine("How do you want to edit subtasks?");
            Console.WriteLine("1. Update task details, Press 1");
            Console.WriteLine("2. Update task priority, Press 2");
            Console.WriteLine("3. Update task status, Press 3");
            Console.WriteLine("4. Delete a subtask, Press 4");
            var input = Console.ReadLine();
            if (input == "1")
            {
                Console.WriteLine("Enter subtask id");
                var subTaskId = Console.ReadLine();
                var subTask = Task.GetSubTaskByID(task, subTaskId);
                Console.WriteLine("Enter new task details");
                var taskDetails = Console.ReadLine();
                SubTask.UpdateSubTaskDetails(subTask, taskDetails);
                Console.WriteLine("Task updated successfully");
            }
            else if (input == "2")
            {
                Console.WriteLine("Enter subtask id");
                var subTaskId = Console.ReadLine();
                var subTask = Task.GetSubTaskByID(task, subTaskId);
                Console.WriteLine("Enter task priority");
                var taskPriority = Console.ReadLine();
                SubTask.UpdateSubTaskPriority(subTask, taskPriority);
                Console.WriteLine("Task priority updated successfully");
            }
            else if (input == "3")
            {
                Console.WriteLine("Enter subtask id");
                var subTaskId = Console.ReadLine();
                var subTask = Task.GetSubTaskByID(task, subTaskId);
                Console.WriteLine("Enter task status");
                var taskStatus = Console.ReadLine();
                SubTask.UpdateSubTaskStatus(subTask, taskStatus);
                Console.WriteLine("Task status updated successfully");
            }
            else if (input == "4")
            {
                Console.WriteLine("Enter subtask id to delete");
                var subTaskId = Console.ReadLine();
                var subTask = Task.GetSubTaskByID(task, subTaskId);
                Task.DeleteSubTask(task, subTask);
            }
            else
            {
                Console.WriteLine("Not a valid input!");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to TasksCLI-v{0}", tasksCliVersion);
            //Console.WriteLine("Hey!, Enter your beautiful name so that I can recognize you next time!");
            //var username = Console.ReadLine();

            //if (string.IsNullOrWhiteSpace(username))
            //{
            //    Console.WriteLine("Invalid Name! Please Restart the application");
            //    return;
            //}
            Console.WriteLine("Hey! User");
            var projectDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Console.WriteLine(projectDirectory);

            dataFolderPath = Path.Combine(projectDirectory, "data");
            dataFilePath = Path.Combine(dataFolderPath, "tasks_data.json");

            if (!Directory.Exists(dataFolderPath))
            {
                Directory.CreateDirectory(dataFolderPath);
            }

            if(File.Exists(dataFilePath))
            {
                var data = File.ReadAllText(dataFilePath);
                if(!string.IsNullOrWhiteSpace(data))
                {
                    tasks = new List<Task> (JsonConvert.DeserializeObject<List<Task>> (data));
                }
            }

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("What do you wanna do today?");
                Console.WriteLine("1. Create a Task");
                Console.WriteLine("2. Edit a Task");
                Console.WriteLine("3. Delete a Task");
                Console.WriteLine("4. Add SubTasks to task");
                Console.WriteLine("5. Edit SubTasks");
                Console.WriteLine("6. Show Task");
                Console.WriteLine("7. Show All Tasks");
                Console.WriteLine("Enter the number/option from above");

                try
                {
                    var input = Console.ReadLine();
                    var option = Convert.ToInt32(input);
                    PerformOpertaionBasedOnUserChoice(option);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid Input! Please Try again" + e);
                }
            }
        }
    }
}
