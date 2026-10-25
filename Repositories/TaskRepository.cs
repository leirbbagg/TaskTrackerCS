using TaskTrackerCS.Data;
using TaskTrackerCS.Models;

namespace TaskTrackerCS.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;
        public TaskRepository(TaskDbContext context)
        {
            _context = context;
        }
        public TaskToDo Add(TaskToDo task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task;
        }

        public bool Remove(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task is null) return false;

            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return true;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}