using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public enum TaskCategory {
    Personal,
    Work,
    Errands,
    Family
}

public class Task {
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskCategory Category { get; set; }
    public bool IsCompleted { get; set; }
}

public class TaskManager {
    public List<Task> Tasks { get; set; }
    public string filepath;
    public TaskManager(string path) {
        filepath = path;
        Tasks = LoadTask().Result; 
    }

    private async Task<bool> SaveTasks(List<Task> tasks) {
        List<string> lines = new List<string>();
        foreach (var task in tasks) {
            lines.Add(TaskToString(task));
        }
        await File.WriteAllLinesAsync(filepath, lines);
        return true;
    }

    public async Task<List<Task>> LoadTask() {
        var taskfromfile = await File.ReadAllLinesAsync(filepath);
        List<Task> loaded = new List<Task>();
        foreach (string line in taskfromfile) {
            loaded.Add(filetotask(line));
        }
        return loaded;
    }

    public Task filetotask(string line) {
        string[] taskproperties = line.Split("|");
        Task task = new Task() {
            Name = taskproperties[0],
            Description = taskproperties[1],
            Category = Enum.TryParse(taskproperties[2], out TaskCategory category)
                        ? category
                        : TaskCategory.Personal,
            IsCompleted = bool.Parse(taskproperties[3])
        };
        return task;
    }

    public string TaskToString(Task task) {
        return $"{task.Name}|{task.Description}|{task.Category}|{task.IsCompleted}";
    }

    public async Task<List<Task>> FilterTasks(TaskCategory category) {
        var tasks = await LoadTask();
        var filteredTasks = tasks.Where(task => task.Category == category).ToList();
        return filteredTasks;
    }

    public async Task<int> AddTask(Task task) {
        var tasks = await LoadTask();
        tasks.Add(task);
        await SaveTasks(tasks); 
        return 0;
    }

    public async Task<int> UpdateTask(string taskName, string updatedName, string taskDescription, TaskCategory category, bool isCompleted) {
        var tasks = await LoadTask();
        foreach (var task in tasks) {
            if (task.Name == taskName) {
                task.Name = updatedName;
                task.Description = taskDescription;
                task.Category = category;
                task.IsCompleted = isCompleted;
                break;
            }
        }
        await SaveTasks(tasks);
        return 0;
    }

    public void DisplayTasks() {
        foreach (var task in Tasks) {
            Console.WriteLine($"Name: {task.Name}, Description: {task.Description}, Category: {task.Category}, Is Completed: {task.IsCompleted}");
        }
    }
}

class Program {
    static async Task<int> Main(string[] args) {
        TaskManager taskManager;
        try {
            taskManager = new TaskManager("lists.txt");
            Console.Write("Enter the number of tasks to add: ");
            int numberOfTasks = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfTasks; i++) {
                Console.WriteLine($"Enter details for Task {i + 1}:");
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Description: ");
                string description = Console.ReadLine();
                Console.Write("Category (Personal, Work, Errands, Family): ");
                Enum.TryParse(Console.ReadLine(), out TaskCategory category);
                Console.Write("Is Completed (True/False): ");
                bool.TryParse(Console.ReadLine(), out bool isCompleted);

                await taskManager.AddTask(new Task { Name = name, Description = description, Category = category, IsCompleted = isCompleted });
            }
            taskManager.DisplayTasks();
           Console.Write("Do you want to update a task? (yes/no): ");
            string confirm = Console.ReadLine();

            if (confirm.ToLower() == "yes") {
                Console.WriteLine("Enter the name of the task to update:");
                string name = Console.ReadLine();

                Console.WriteLine("Enter the updated task name:");
                string upname = Console.ReadLine();

                Console.WriteLine("Enter the updated task description:");
                string udesc = Console.ReadLine();

                Console.WriteLine("Enter the updated task Category (Personal, Work, Errands, Family):");
                Enum.TryParse(Console.ReadLine(), out TaskCategory ucategory);

                Console.WriteLine("Is the task completed? (true/false):");
                bool.TryParse(Console.ReadLine(), out bool uiscompleted);

                await taskManager.UpdateTask(name, upname, udesc, ucategory, uiscompleted);
            } 
            Console.WriteLine("Enter a category you want the task to be filterd(Personal, Work, Errands, Family)");
            Enum.TryParse(Console.ReadLine(), out TaskCategory cat);
            List<Task> filteredTasks = await taskManager.FilterTasks(cat);
            foreach (var t in filteredTasks) {
            Console.WriteLine($"Name: {t.Name}, Description: {t.Description}, Category: {t.Category}, Is Completed: {t.IsCompleted}");
        }

        } catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
        return 0;
    }
}
