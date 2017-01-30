using UnityEngine;

namespace Theming
{
    public abstract class ThemedBehavior<TThemeManager, TTheme>
        : MonoBehaviour, IThemeable<TTheme>
        where TThemeManager: ThemeManager<TTheme>
        where TTheme : ScriptableObject
    {

        [SerializeField]
        TThemeManager themeManager;

        void Awake()
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
}
