using System.IO;
using System.Xml.Serialization;

namespace LESCH
{
    public class XmlFileSerializer : IFileSerializer
    {
        public void Save<T>(string path, T serializable)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var stream = new FileStream(path, FileMode.Create)) {
                serializer.Serialize(stream, serializable);
            }
        }

        public T Load<T>(string path)
        {
            if (!File.Exists(path)) {
                return default;
            }

            var serializer = new XmlSerializer(typeof(T));

            using (var stream = new FileStream(path, FileMode.Open)) {
                return (T)serializer.Deserialize(stream);
            }
        }

        public void Clear(string path)
        {
            File.WriteAllText(path, string.Empty);
        }
    }
}
