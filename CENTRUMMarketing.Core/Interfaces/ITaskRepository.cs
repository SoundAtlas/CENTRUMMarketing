using CENTRUMMarketing.Core.Models;

namespace CENTRUMMarketing.Core.Interfaces
{
    public interface ITaskRepository
    {
        List<TaskItem> GetAll();
        TaskItem? GetById(int id);
        void Add(TaskItem task);
        void Save();
    }
}
