using Theming;
using UnityEngine;

public class GorlaxTheme : ScriptableObject
{
    public Color PrimaryButtonNormal => _primaryButtonNormal;

    [SerializeField] private Color _primaryButtonNormal;

    public Color PrimaryButtonDown => _primaryButtonDown;
    [SerializeField] private Color _primaryButtonDown;

    public Font Font => font;
    [SerializeField] Font font;
}
