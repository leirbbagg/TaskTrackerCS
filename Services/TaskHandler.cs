using System;
using System.Text.Json;
using TaskTrackerCS.Repo;
using TaskTrackerCS.Models;

namespace TaskTrackerCS.Services
{
    public class TaskHandler
    {
        private readonly TaskRepositoryJson _repo;

        public TaskHandler(TaskRepositoryJson repo)
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

        public void Delete(int id)
        {
            var tasks = _repo.Load();
            var taskDelete = tasks.RemoveAll(t => t.Id == id);

            if (taskDelete == 0)
                throw new InvalidOperationException($"Task {id} not found");

            _repo.Save(tasks);
        }

        public List<TaskToDo> List(EStatusTask? status = null)
        {
            List<TaskToDo> tasks = _repo.Load();
            return status is null ? tasks : tasks.Where(t => t.Status == status).ToList();
        }

        public void Rename(int id, string newDescription)
            => Update(id, t => t.Rename(newDescription));

        public void MarkProgress(int id)
            => Update(id, t => t.MarkInProgress());

        public void MarkDone(int id)
            => Update(id, t => t.MarkDone());


        private void Update(int id, Action<TaskToDo> action)
        {
            var tasks = _repo.Load();
            var taskEdit = tasks.FirstOrDefault(t => t.Id == id) ??
                throw new InvalidOperationException($"Task {id} not found");

            action(taskEdit);
            _repo.Save(tasks);
        }
    }
}