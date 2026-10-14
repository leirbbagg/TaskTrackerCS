namespace TaskTrackerCS.Models
{
    public class TaskToDo
    {
        public int Id { get; init; }
        public string Description { get; private set; }
        public EStatusTask Status { get; private set; } = EStatusTask.Todo;
        public DateTime CreatedAt { get; init; } = DateTime.Now;
        public DateTime UpdatedAt { get; private set; } = DateTime.Now;

        public TaskToDo(int id, string description)
        {
            Id = id;
            Description = description;
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