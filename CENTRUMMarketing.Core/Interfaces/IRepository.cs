

namespace CENTRUMMarketing.Core.Interfaces
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        void Add(T item);
        void Save();
    }
}
