using System;

public interface IPropertyObject
{
#if UNITY_EDITOR
    event Action OnPropertyChanged;
#endif
}
