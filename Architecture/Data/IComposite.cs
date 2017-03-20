using System.Collections.Generic;

public interface IComposite<T> {
    void SetCompositeData (IList<T> data);
    T[] GetCompositeData ();
}
