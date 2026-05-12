using CENTRUMMarketing.Core.Models;

namespace CENTRUMMarketing.Core.Repositories
{
    public class TaskRepository
    {
        private readonly List<TaskItem> _tasks = new();

        public IEnumerable<TaskItem> GetAll()
        {
           return _tasks;
        }

        public void Add(TaskItem task)
        {
            _tasks.Add(task);
        }
    }
}
