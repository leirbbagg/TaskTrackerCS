using System;
using System.Text.Json;
using TaskTrackerCS.Repo;
using TaskTrackerCS.Models;

namespace TaskTrackerCS.Services
{
    public class TaskHandler 
    {
        private readonly TaskRepository _repo;

        public TaskHandler(TaskRepository repo)
        {
            _repo = repo;
        }

        public TaskToDo Add(string description)
        {
            var tasks = _repo.Load();
            var id = tasks.Count == 0 ? 1 : tasks.Max(t => t.Id) + 1;

            var task = new TaskToDo(id, description);
            tasks.Add(task);
            _repo.Save(tasks);
            return task;
        }
    }
}