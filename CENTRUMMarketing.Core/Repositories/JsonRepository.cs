using CENTRUMMarketing.Core.Exceptions;
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

            try
            {
                string json = JsonSerializer.Serialize(_items, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(_filePath, json);

            }

            catch (Exception ex)
            {
                throw new RepositoryException(
                    "An error occurred while saving data to the JSON file.", ex);
            }
        }

        private List<T> Load()
        {

            try
            {
                if (!File.Exists(_filePath))
                {
                    List<T> emptyList = new List<T>();

                    string emptyJson = JsonSerializer.Serialize(emptyList, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

                    File.WriteAllText(_filePath, emptyJson);

                    return emptyList;
                }

                string json = File.ReadAllText(_filePath);

                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<T>();
                }

                List<T>? loadedItems = JsonSerializer.Deserialize<List<T>>(json);

                if (loadedItems == null)
                {
                    return new List<T>();
                }

                return loadedItems;

            }

            catch (Exception ex)
            {
                throw new RepositoryException(
                    "An error occurred while loading data from the JSON file.", ex);
            }
        }
    }
}
