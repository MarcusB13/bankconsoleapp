using System.Text.Json;

namespace DAL
{
    public class JsonHandler
    {
        private string _path;
        public JsonHandler(string path)
        {
            _path = path;
        }

        public List<T>? Read<T>()
        {
            string content = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<List<T>>(content);
        }

        public void Write<T>(List<T> data)
        {
            File.WriteAllText(_path, JsonSerializer.Serialize(data));
        }
    }

}

