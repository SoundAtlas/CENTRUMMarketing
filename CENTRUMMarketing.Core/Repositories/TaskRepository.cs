using CENTRUMMarketing.Core.Interfaces;
using CENTRUMMarketing.Core.Models;

namespace CENTRUMMarketing.Core.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly JsonRepository<TaskItem> _jsonRepository;

        public TaskRepository()
        {
            _jsonRepository = new JsonRepository<TaskItem>(@"..\..\..\..\CENTRUMMarketing.Core\Data\tasks.json");
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

        public bool Delete(int id)
        {
            TaskItem? task = GetById(id);

            if (task == null) return false;

            return _jsonRepository.Remove(task);
        }

        public void Save()
        {
            _jsonRepository.Save();
        }
    }
}
