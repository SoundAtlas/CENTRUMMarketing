using CENTRUMMarketing.Core.Models;

namespace CENTRUMMarketing.Core.Repositories
{
    public class TaskRepository
    {
        private readonly JsonRepository<TaskItem> _jsonRepository;

        public TaskRepository()
        {
            _jsonRepository = new JsonRepository<TaskItem>("tasks.json");
        }

        public List<TaskItem> GetAll()
        {
            return _jsonRepository.GetAll();
        }

        public void Add(TaskItem task)
        {
            _jsonRepository.Add(task);
        }

        public TaskItem? GetById(int id)
        {
            foreach (TaskItem task in _jsonRepository.GetAll())
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
