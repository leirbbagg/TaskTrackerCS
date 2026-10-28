using System.Text.Json.Serialization;

namespace TaskTrackerCS.Models
{
    public class TaskToDo
    {
        [JsonInclude] public int Id { get; private set; }
        public string Description { get; private set; }
    
        [JsonInclude] public EStatusTask Status { get; set; }
        [JsonInclude] public DateTime CreatedAt { get; init; }
        [JsonInclude] public DateTime UpdatedAt { get; private set; } 

        
        public TaskToDo(string description)
        {
            Description = description;
            Status = EStatusTask.Todo;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        // a useful method for defining IDs when using JSON for a database
        internal void DefineId(int id) => Id = id;

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