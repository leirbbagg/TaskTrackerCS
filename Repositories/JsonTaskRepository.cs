using System.Text.Json;
using System.Text.Json.Serialization;
using TaskTrackerCS.Models;
using TaskTrackerCS.Repositories;
using TaskTrackerCS.Configuration;

public class JsonTaskRepository : ITaskRepository
{
    private readonly string _path;

    private static readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public JsonTaskRepository()
    {
        _path = DbPath.Json();
    }

    public TaskToDo Add(TaskToDo task)
    {
        var tasks = Load();
        var id = tasks.Count == 0 ? 1 : tasks.Max(t => t.Id) + 1;

        task.DefineId(id);
        tasks.Add(task);
        Save(tasks);
        return task;
    }

    public TaskToDo? GetById(int id)
        => Load().FirstOrDefault(t => t.Id == id);

    public List<TaskToDo> ViewTasks(EStatusTask? filter = null)
    {
        var tasks = Load();
        return filter is null
            ? tasks
            : tasks.Where(t => t.Status == filter).ToList();
    }

    public void Update(TaskToDo task)
    {
        var tasks = Load();
        var index = tasks.FindIndex(t => t.Id == task.Id);

        if (index < 0)
            throw new InvalidOperationException($"Task {task.Id} not found.");

        tasks[index] = task;
        Save(tasks);
    }

    public bool Remove(int id)
    {
        var tasks = Load();

        if (tasks.RemoveAll(t => t.Id == id) == 0)
            return false;

        Save(tasks);
        return true;
    }

    // there is no resource to free up
    public void Dispose() { }

    private List<TaskToDo> Load()
    {
        if (!File.Exists(_path))
            return new List<TaskToDo>();

        var json = File.ReadAllText(_path);
        return JsonSerializer.Deserialize<List<TaskToDo>>(json, _options) ?? new();
    }

    public void Refresh(TaskToDo task)
    {
        var tasks = Load();
        var index = tasks.FindIndex(t => t.Id == task.Id);
        if (index < 0)
            throw new InvalidOperationException($"Task {task.Id} not found.");
        tasks[index] = task;
        Save(tasks);
    }
    private void Save(List<TaskToDo> tarefas)
    {
        var json = JsonSerializer.Serialize(tarefas, _options);
        File.WriteAllText(_path, json);
    }
}