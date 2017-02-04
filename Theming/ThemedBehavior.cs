using UnityEngine;

namespace Theming
{
    public abstract class ThemedBehavior<TTheme>
        : MonoBehaviour, IThemeable<TTheme>
        where TTheme : ScriptableObject
    {
        void Awake()
        {
            OnAwakePreTheme();
            ThemeManager<TTheme>.RegisterThemeable (this);
            OnAwakePostTheme();
        }

        protected virtual void OnAwakePreTheme()
        {}

        protected virtual void OnAwakePostTheme()
        {
        }

        public abstract void HandleNewTheme(TTheme theme);

        void OnDestroy ()
        {
            ThemeManager<TTheme>.DeregisterThemeable (this);
        }
    }
}
