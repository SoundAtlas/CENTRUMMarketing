using TaskStatus = CENTRUMMarketing.Core.Enums.TaskStatus;

namespace CENTRUMMarketing.Core.Models
{
    public class TaskItem : BaseEntity
    {
        public int CustomerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public TaskStatus Status { get; set; }

        public TaskItem(
            int id,
            int customerId,
            string title,
            string description,
            DateTime deadline) : base(id)
        {
            CustomerId = customerId;
            Title = title;
            Description = description;
            Deadline = deadline;
            Status = TaskStatus.ToDo;
        }

        public void UpdateStatus(TaskStatus newStatus)
        {
            Status = newStatus;
        }

        public void UpdateDeadline(DateTime newDeadline)
        {
            Deadline = newDeadline;
        }
    }
}
