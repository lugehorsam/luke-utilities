using System;

public interface IDataPublisher<TData> where TData : struct  {

    void Push(TData[] newData);
}
