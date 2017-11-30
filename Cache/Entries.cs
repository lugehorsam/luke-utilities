namespace Utilities.Cache
{
    public class Entries<T> : Entry<Stash<T>>
    {
        public Entries(string key) : base(key) { }

        protected override Stash<T> GetDefaultValue()
        {
            return new Stash<T>();
        }
    }
}
