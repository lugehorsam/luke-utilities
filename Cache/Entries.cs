namespace Utilities.Cache
{
    public class Entries<T> : Entry<SerializableList<T>>
    {
        public Entries(string key) : base(key) { }

        protected override SerializableList<T> GetDefaultValue()
        {
            return new SerializableList<T>();
        }
    }
}
