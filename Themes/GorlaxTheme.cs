using UnityEngine.UI;
using UnityEngine;

public class GorlaxTheme : Theme
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

}
