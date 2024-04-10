// Todo list Application


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TodoListApp
{
    class Program
    {
        static List<TaskItem> tasks = new List<TaskItem>();
        static string filePath = "tasks.txt";

        static void Main(string[] args)
        {
            LoadTasksFromFile();

            while (true)
            {
                Console.WriteLine("Todo List Application");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. Edit Task");
                Console.WriteLine("3. Mark Task as Done");
                Console.WriteLine("4. Remove Task");
                Console.WriteLine("5. Display Tasks");
                Console.WriteLine("6. Sort Tasks by Date");
                Console.WriteLine("7. Sort Tasks by Project");
                Console.WriteLine("8. Save and Quit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        EditTask();
                        break;
                    case "3":
                        MarkTaskAsDone();
                        break;
                    case "4":
                        RemoveTask();
                        break;
                    case "5":
                        DisplayTasks();
                        break;
                    case "6":
                        SortTasksByDate();
                        break;
                    case "7":
                        SortTasksByProject();
                        break;
                    case "8":
                        SaveTasksToFile();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }
        // Created Methods
        static void LoadTasksFromFile()
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] data = line.Split(',');
                    tasks.Add(new TaskItem(data[0], DateTime.Parse(data[1]), bool.Parse(data[2]), data[3]));
                }
            }
        }

        static void AddTask()
        {
            Console.Write("Enter task title: ");
            string title = Console.ReadLine();
            Console.Write("Enter due date (yyyy-MM-dd): ");
            DateTime dueDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Is task completed? (true/false): ");
            bool status = bool.Parse(Console.ReadLine());
            Console.Write("Enter project: ");
            string project = Console.ReadLine();

            tasks.Add(new TaskItem(title, dueDate, status, project));
            Console.WriteLine("Task added successfully.");
        }

        static void EditTask()
        {
            DisplayTasks();
            Console.Write("Enter the index of the task to edit: ");
            int index = int.Parse(Console.ReadLine());

            if (index >= 0 && index < tasks.Count)
            {
                TaskItem task = tasks[index];
                Console.Write("Enter new task title: ");
                task.Title = Console.ReadLine();
                Console.Write("Enter new due date (yyyy-MM-dd): ");
                task.DueDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Is task completed? (true/false): ");
                task.Status = bool.Parse(Console.ReadLine());
                Console.Write("Enter new project: ");
                task.Project = Console.ReadLine();
                Console.WriteLine("Task edited successfully.");
            }
            else
            {
                Console.WriteLine("Invalid task index.");
            }
        }

        static void MarkTaskAsDone()
        {
            DisplayTasks();
            Console.Write("Enter the index of the task to mark as done: ");
            int index = int.Parse(Console.ReadLine());

            if (index >= 0 && index < tasks.Count)
            {
                tasks[index].Status = true;
                Console.WriteLine("Task marked as done successfully.");
            }
            else
            {
                Console.WriteLine("Invalid task index.");
            }
        }

        static void RemoveTask()
        {
            DisplayTasks();
            Console.Write("Enter the index of the task to remove: ");
            int index = int.Parse(Console.ReadLine());

            if (index >= 0 && index < tasks.Count)
            {
                tasks.RemoveAt(index);
                Console.WriteLine("Task removed successfully.");
            }
            else
            {
                Console.WriteLine("Invalid task index.");
            }
        }

        static void DisplayTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i}. {tasks[i]}");
            }
        }

        static void SortTasksByDate()
        {
            tasks = tasks.OrderBy(t => t.DueDate).ToList();
            Console.WriteLine("Tasks sorted by date.");
        }

        static void SortTasksByProject()
        {
            tasks = tasks.OrderBy(t => t.Project).ToList();
            Console.WriteLine("Tasks sorted by project.");
        }

        static void SaveTasksToFile()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var task in tasks)
                {
                    writer.WriteLine($"{task.Title},{task.DueDate},{task.Status},{task.Project}");
                }
            }
            Console.WriteLine("Tasks saved to file successfully.");
        }
    }

    class TaskItem
    {
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool Status { get; set; }
        public string Project { get; set; }

        // Created constructor 

        public TaskItem(string title, DateTime dueDate, bool status, string project)
        {
            Title = title;
            DueDate = dueDate;
            Status = status;
            Project = project;
        }

        public override string ToString()
        {
            return $"Title: {Title}, Due Date: {DueDate:yyyy-MM-dd}, Status: {(Status ? "Completed" : "Pending")}, Project: {Project}";
        }
    }
}
