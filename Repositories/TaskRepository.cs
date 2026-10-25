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
    }
}