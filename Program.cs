using System;
using System.Collections.Generic;

namespace SimpleCalculator
{
    class Task
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
    }

    class TaskManager
    {
        private List<Task> tasks;
        private int nextTaskId;

        public TaskManager()
        {
            tasks = new List<Task>();
            nextTaskId = 1;
        }

        public void AddTask(string description, DateTime dueDate)
        {
            Task newTask = new Task
            {
                Id = nextTaskId,
                Description = description,
                DueDate = dueDate,
                IsCompleted = false
            };
            tasks.Add(newTask);
            nextTaskId++;
        }

        public int RemoveTask(int id)
        {
            Task taskToRemove = tasks.Find(task => task.Id == id);
            if (taskToRemove != null)
            {
                tasks.Remove(taskToRemove);
                Console.WriteLine($"{id} Task Removed Successfully");
                return 1;
            }
            Console.WriteLine($"Task with ID {id} not found.");
            return 0;
        }

        public void ListAllTasks()
        {
            Console.WriteLine("\nAll Tasks:");
            foreach (Task task in tasks)
            {
                string status = task.IsCompleted ? "Completed" : "Not Completed";
                Console.WriteLine($"ID: {task.Id}, Description: {task.Description}, Due Date: {task.DueDate:yyyy-MM-dd}, Status: {status}");
            }
            Console.WriteLine();
        }

        public void MarkTaskAsComplete(int taskId)
        {
            Task taskToComplete = tasks.Find(task => task.Id == taskId);
            if (taskToComplete != null)
            {
                taskToComplete.IsCompleted = true;
                Console.WriteLine($"Task with ID {taskId} marked as complete.");
            }
            else
            {
                Console.WriteLine($"Task with ID {taskId} not found.");
            }
        }
    }

    internal class Program
    {
        public static void DisplayMenu()
        {
            Console.WriteLine("================= Welcome =================");
            Console.WriteLine("=========== To Do List APP ===========");
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Remove Task");
            Console.WriteLine("3. List all Tasks");
            Console.WriteLine("4. Mark Task as Completed");
            Console.WriteLine("5. Exit");
        }

        static int GetUserChoice()
        {
            Console.Write("Your choice is: ");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
            {
                Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                Console.Write("Your choice is: ");
            }
            return choice;
        }

        static void Main(string[] args)
        {
            TaskManager taskManager = new TaskManager();
            bool shouldExit = false;

            while (!shouldExit)
            {
                DisplayMenu();
                int userInput = GetUserChoice();

                switch (userInput)
                {
                    case 1:
                        Console.Write("Enter task description: ");
                        string description = Console.ReadLine();
                        Console.Write("Enter due date (yyyy-mm-dd): ");
                        DateTime dueDate;
                        while (!DateTime.TryParse(Console.ReadLine(), out dueDate))
                        {
                            Console.WriteLine("Invalid date format. Please enter a valid date (yyyy-mm-dd).");
                            Console.Write("Enter due date (yyyy-mm-dd): ");
                        }
                        taskManager.AddTask(description, dueDate);
                        Console.WriteLine("Task added successfully.");
                        break;

                    case 2:
                        Console.Write("Enter task ID to remove: ");
                        int removeId;
                        while (!int.TryParse(Console.ReadLine(), out removeId))
                        {
                            Console.WriteLine("Invalid ID. Please enter a valid task ID.");
                            Console.Write("Enter task ID to remove: ");
                        }
                        taskManager.RemoveTask(removeId);
                        break;

                    case 3:
                        taskManager.ListAllTasks();
                        break;

                    case 4:
                        Console.Write("Enter task ID to mark as complete: ");
                        int completeId;
                        while (!int.TryParse(Console.ReadLine(), out completeId))
                        {
                            Console.WriteLine("Invalid ID. Please enter a valid task ID.");
                            Console.Write("Enter task ID to mark as complete: ");
                        }
                        taskManager.MarkTaskAsComplete(completeId);
                        break;

                    case 5:
                        Console.WriteLine("Bye Bye!");
                        shouldExit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}
