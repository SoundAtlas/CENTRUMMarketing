using CENTRUMMarketing.Core.Enums;

namespace CENTRUMMarketing.Core.Models
{
    public class TaskItem : BaseEntity
    {
        public int CustomerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public TaskItemStatus Status { get; set; }
        public int? CollaboratorId { get; set; }


        public TaskItem(
            int id,
            int customerId,
            string title,
            string description,
            DateTime deadline,
            TaskItemStatus status) : base(id)
        {
            CustomerId = customerId;
            Title = title;
            Description = description;
            Deadline = deadline;
            Status = status;
        }

        public void UpdateStatus(TaskItemStatus newStatus)
        {
            Status = newStatus;
        }

        public void UpdateDeadline(DateTime newDeadline)
        {
            Deadline = newDeadline;
        }
    }
}
