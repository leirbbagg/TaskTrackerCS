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


        public List<TaskToDo> ViewTasks(EStatusTask? statusFilter = null)
        {
            var inquiry = _context.Tasks.AsQueryable();

            if (statusFilter is not null)
                inquiry = inquiry.Where(t => t.Status == statusFilter);

            return inquiry.OrderBy(t => t.Id).ToList();
        }

        public TaskToDo? GetById(int id)
        {
            return _context.Tasks.FirstOrDefault(t => t.Id == id);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}