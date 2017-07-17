namespace Utilities
{
    public class InfiniteLerp<TProperty>
    {
        public TProperty UnitsPerSecond
        {
            get;
            private set;
        }
        public InfiniteLerp(TProperty unitsPerSecond)
        {
            UnitsPerSecond = unitsPerSecond;
        }
    }    
}
