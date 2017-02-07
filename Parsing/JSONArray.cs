using UnityEngine;

[System.Serializable]
public class JsonArray<T>
{
    public T [] Array {
        get {
            return array;
        }
    }
    [SerializeField]
    T[] array;

    public JsonArray (T [] array)
    {
        this.array = array;
    }

    public JsonArray(){}
}
