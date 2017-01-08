using UnityEngine;

public abstract class ThemedGameBehavior<TThemeManager, TTheme>
    : GameBehavior, IThemeable<TTheme>
    where TThemeManager: ThemeManager<TTheme>
    where TTheme : Theme
{

    [SerializeField]
    TThemeManager themeManager;

    protected sealed override void OnAwake()
    {
        themeManager.RegisterThemeable (this);
        OnAwakePostTheme();
    }

    protected virtual void OnAwakePostTheme()
    {
    }

    public abstract void HandleNewTheme(TTheme theme);

    void OnDestroy ()
    {
        themeManager.DeregisterThemeable (this);
    }
}
