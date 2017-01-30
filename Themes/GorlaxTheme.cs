using Theming;
using UnityEngine;

public class GorlaxTheme : ScriptableObject
{
    public Color PrimaryButtonNormal
    {
        get { return _primaryButtonNormal; }
    }

    [SerializeField] private Color _primaryButtonNormal;

    public Color PrimaryButtonDown
    {
        get { return _primaryButtonDown; }
    }
    [SerializeField] private Color _primaryButtonDown;

    public Font Font
    {
        get { return font; }
    }
    [SerializeField] Font font;
}
