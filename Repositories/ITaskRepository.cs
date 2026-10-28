using TaskTrackerCS.Models;

namespace TaskTrackerCS.Repositories
{
    public interface ITaskRepository : IDisposable
    {
        TaskToDo Add (TaskToDo task);
        bool Remove(int id);
        List<TaskToDo> ViewTasks(EStatusTask? statusFilter = null);
    }
}