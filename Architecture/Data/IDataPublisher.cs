using System;

public interface IDataPublisher<TDatum> where TDatum : struct  {

    void Push(TDatum[] newData);
}
