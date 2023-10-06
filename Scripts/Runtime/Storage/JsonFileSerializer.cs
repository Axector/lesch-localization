using System.IO;
using UnityEngine;

namespace LESCH
{
    public class JsonFileSerializer : IFileSerializer
    {
        public void Save<T>(string path, T serializable)
        {
            string json = JsonUtility.ToJson(serializable);

            File.WriteAllText(path, json);
        }

        public T Load<T>(string path)
        {
            if (!File.Exists(path)) {
                return default;
            }

            string fileContent = File.ReadAllText(path);

            var result = JsonUtility.FromJson<T>(fileContent);

            return result;
        }

        public void Clear(string path)
        {
            File.WriteAllText(path, string.Empty);
        }
    }
}
