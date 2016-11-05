using UnityEngine;
using System.Collections;
using System;

public class LazyReference<T> where T : class {
    public T Value {
        get {
            value = value ?? getValue ();
            return value;
        }
    }

    T value;

    Func<T> getValue;

    public LazyReference (Func<T> getValue)
    {
        this.getValue = getValue;
    }
  
	
}
