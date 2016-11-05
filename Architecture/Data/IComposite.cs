using System.Collections.Generic;

public interface IComposite<T> where T : struct {
    void SetCompositeData (IList<T> data);
    T[] GetCompositeData ();
}
