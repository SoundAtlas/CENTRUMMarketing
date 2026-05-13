using CENTRUMMarketing.Core.Models;

namespace CENTRUMMarketing.Core.Repositories
{
    public class TaskRepository
    {
        private readonly List<TaskItem> _tasks = new();

        public List<TaskItem> GetAll()
        {
            return _tasks;
        }

        public void Add(TaskItem task)
        {
            _tasks.Add(task);
        }

        public TaskItem? GetById(int id)
        {
            foreach (TaskItem task in _tasks)
            {
                if (task.Id == id)
                {
                    return task;
                }
            }

            return null;
        }
    }
}
