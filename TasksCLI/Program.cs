using System;
using System.Collections.Generic;

namespace TasksCLI
{
    class Program
    {
        public static List<Task> tasks = new List<Task>();
        public const string tasksCliVersion = "1.0.0";

        private static int taskID = 0;

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
                    tasks.Add(task);
                    Console.WriteLine("Task '{0}' created sucessfully!", task.name);
                    Console.WriteLine();
                    task.taskID = ++taskID;
                    break;
                case 2:
                    Console.WriteLine("Edit a task");
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
                    var id = Console.ReadLine();
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
                    break;
                case 6:
                    Console.WriteLine("Show a Task");
                    Console.WriteLine("Enter task id to display: ");
                    id = Console.ReadLine();
                    task = Task.GetTaskById(tasks, id);
                    if(task != null)
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
