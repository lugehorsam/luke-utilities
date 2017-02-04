using Theming;
using UnityEngine;
using UnityEngine.UI;

public class GorlaxTextThemer : ThemedBehavior<GorlaxTheme>
{
    private Text text;

    protected override void OnAwakePreTheme()
    {
        text = gameObject.GetOrAddComponent<Text>();
        Debug.Log("text is " + text);
    }

    public override void HandleNewTheme(GorlaxTheme theme)
    {
        Debug.Log("handle new theme called");
        text.font = theme.Font;
    }
}
