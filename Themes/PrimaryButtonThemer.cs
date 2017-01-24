using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrimaryButtonThemer : ThemedGameBehavior<GorlaxThemeManager, GorlaxTheme>
{
    [SerializeField] private Button button;

    public override void HandleNewTheme(GorlaxTheme theme)
    {
        ColorBlock newBlock = new ColorBlock();
        newBlock.normalColor = theme.PrimaryButtonNormal;
        newBlock.highlightedColor = theme.PrimaryButtonNormal;
        newBlock.pressedColor = theme.PrimaryButtonDown;
        newBlock.colorMultiplier = 1f;
        button.colors = newBlock;
    }
}
