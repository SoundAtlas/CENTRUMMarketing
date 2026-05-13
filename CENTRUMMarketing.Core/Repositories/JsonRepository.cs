using CENTRUMMarketing.Core.Interfaces;
using System.Text.Json;

namespace CENTRUMMarketing.Core.Repositories
{
    public class JsonRepository<T> : IRepository<T>
    {
        private readonly string _filePath;
        private readonly List<T> _items;

        public JsonRepository(string filePath)
        {
            _filePath = filePath;
            _items = Load();
        }

        public List<T> GetAll()
        {
            return _items;
        }

        public void Add(T item)
        {
            _items.Add(item);
            Save();
        }

        public void Save()
        {
            string json = JsonSerializer.Serialize(_items, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_filePath, json);
        }

        private List<T> Load()
        {
            if (!File.Exists(_filePath))
            {
                return new List<T>();
            }

            string json = File.ReadAllText(_filePath);

            List<T>? loadedItems = JsonSerializer.Deserialize<List<T>>(json);

            if (loadedItems == null)
            {
                return new List<T>();
            }

            return loadedItems;
        }
    }
}
