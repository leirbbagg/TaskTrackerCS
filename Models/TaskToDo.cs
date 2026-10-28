using System.Text.Json.Serialization;

namespace TaskTrackerCS.Models
{
    public class TaskToDo
    {
        [JsonInclude] public int Id { get; init; }
        public string Description { get; private set; }
    
        [JsonInclude] public EStatusTask Status { get; set; }
        [JsonInclude] public DateTime CreatedAt { get; init; }
        [JsonInclude] public DateTime UpdatedAt { get; private set; } 

        
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