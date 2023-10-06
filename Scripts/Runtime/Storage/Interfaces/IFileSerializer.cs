namespace LESCH
{
    public interface IFileSerializer
    {
        public void Save<T>(string path, T serializable);
        public T Load<T>(string path);
        public void Clear(string path);
    }
}
