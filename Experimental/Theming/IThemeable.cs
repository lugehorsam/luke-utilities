using UnityEngine;

public interface IThemeable<TTheme>
{
    void HandleNewTheme (TTheme theme);
}
