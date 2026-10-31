using System;
using System.Text.Json;
using TaskTrackerCS.Repositories;
using TaskTrackerCS.Models;

namespace TaskTrackerCS.Services
{
    public class TaskHandler
    {
        private readonly ITaskRepository _repo;

        public TaskHandler(ITaskRepository repo)
        {
            _repo = repo;
        }

        public TaskToDo Add(string description)
        {
            return _repo.Add(new TaskToDo(description));
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
            // not implemented
        }

        public void Rename(int id, string newDescription)
            => Update(id, t => t.Rename(newDescription));

        public void MarkProgress(int id)
            => Update(id, t => t.MarkInProgress());

        public void MarkDone(int id)
            => Update(id, t => t.MarkDone());


        private void Update(int id, Action<TaskToDo> action)
        {
            var task = _repo.GetById(id) ?? throw new InvalidOperationException($"Task {id} not found.");

            action(task);
            _repo.Refresh(task);

        }
    }
}