using UnityEngine;
using System;

public interface IThemeable<T>
{
    void HandleNewTheme (T theme);
}
