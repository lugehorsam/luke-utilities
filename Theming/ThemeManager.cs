using UnityEngine;
using System.Collections.Generic;

namespace Theming
{
    public static class ThemeManager<TTheme> where TTheme : ScriptableObject
    {
        private const string THEMES_DIRECTORY = "Themes";

        static TTheme CurrentTheme => themes [currentThemeIndex];

        private static readonly TTheme[] themes;

        static int currentThemeIndex;

        static readonly HashSet<IThemeable<TTheme>> themeables = new HashSet<IThemeable<TTheme>>();

        static ThemeManager()
        {
            themes = Resources.LoadAll<TTheme>(THEMES_DIRECTORY);
        }

        public static void SetThemeIndex (int index)
        {
            currentThemeIndex = index;
            ApplyCurrentTheme ();
        }

        public static void RegisterThemeable(IThemeable<TTheme> themeable)
        {
            if (!themeables.Add (themeable)) {
                Diagnostics.Report ("trying to add a duplicate theamble");
            } else {
                themeable.HandleNewTheme (CurrentTheme);
            }
        }

        public static void DeregisterThemeable(IThemeable<TTheme> themeable)
        {
            if (!themeables.Remove (themeable)) {
                Diagnostics.Report ("trying to remove a nonexistant theamble");
            }
        }

        static void ApplyCurrentTheme ()
        {
            foreach (IThemeable<TTheme> themeable in themeables) {
                themeable.HandleNewTheme (CurrentTheme);
            }
        }
    }
}