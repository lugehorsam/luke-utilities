using UnityEngine;

[System.Serializable]
public class JsonArray<T>
{
    public T [] Data {
        get {
            return data;
        }
    }
    [SerializeField]
    T[] data;

    public JsonArray (T [] data)
    {
        this.data = data;
    }
}
