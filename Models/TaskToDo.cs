using System.Text.Json.Serialization;

namespace TaskTrackerCS.Models
{
    public class TaskToDo
    {
        public int Id { get; init; }
        public string Description { get; private set; }
    
        public EStatusTask Status { get; set; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; private set; } 

        [JsonConstructor]
        public TaskToDo(int id, string description)
        {
            Id = id;
            Description = description;
            Status = EStatusTask.Todo;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public void Rename(string newDescription)
        {
            Description = newDescription;
            Update();
        }

        public void MarkInProgress()
        {
            Status = EStatusTask.InProgress;
            Update();
        }

        public void MarkDone()
        {
            Status = EStatusTask.Done;
            Update();
        }

        private void Update() => UpdatedAt = DateTime.Now;
    }
}