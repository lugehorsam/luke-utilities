using Theming;
using UnityEngine;
using UnityEngine.UI;

public class GorlaxTextThemer : ThemedBehavior<GorlaxThemeManager, GorlaxTheme>
{

    [SerializeField] private Text text;

    public override void HandleNewTheme(GorlaxTheme theme)
    {
        text.font = theme.Font;
    }
}
