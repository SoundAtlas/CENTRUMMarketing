using CENTRUMMarketing.Core.Exceptions;
using CENTRUMMarketing.Core.Interfaces;
using System.Text.Json;

namespace CENTRUMMarketing.Core.Repositories
{
    public class JsonRepository<T> : IRepository<T>
    {
        private readonly string _filePath;
        private readonly List<T> _items;

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };

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

        public bool Remove(T item)
        {
            bool removed = _items.Remove(item);

            if (removed)
            {
                Save();
            }
            return removed;
        }

        public void Save()
        {
            try
            {

                try
                {
                    EnsureDirectoryExists();

                    string json = JsonSerializer.Serialize(_items, _jsonOptions);
                    File.WriteAllText(_filePath, json);
                }
                catch (Exception ex)
                {
                    throw new RepositoryException(
                        "An error occurred while saving data to the JSON file.", ex);
                }
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
                EnsureFileExists();

                string json = File.ReadAllText(_filePath);

                if (string.IsNullOrWhiteSpace(json))
                {
                    ResetFileToEmptyArray();
                    return new List<T>();
                }

                List<T>? loadedItems = JsonSerializer.Deserialize<List<T>>(json);

                if (loadedItems == null)
                {
                    ResetFileToEmptyArray();
                    return new List<T>();
                }

                return loadedItems;

            }
            catch (JsonException)
            {
                BackupCorruptFile();
                ResetFileToEmptyArray();

                return new List<T>();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(
                    "An error occurred while loading data from the JSON file.", ex);

            }
        }

        private void EnsureFileExists()
        {
            EnsureDirectoryExists();

            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        private void ResetFileToEmptyArray()
        {
            EnsureDirectoryExists();
            File.WriteAllText(_filePath, "[]");
        }

        private void BackupCorruptFile()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return;
                }

                string backupPath = _filePath + ".backup";
                File.Copy(_filePath, backupPath, overwrite: true);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(
                    "An error occurred while backing up the corrupt JSON file.", ex);
            }
        }

        private void EnsureDirectoryExists()
        {
            string? directory = Path.GetDirectoryName(_filePath);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
