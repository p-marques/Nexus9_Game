using UnityEngine;

[CreateAssetMenu(fileName = "UIColors", menuName = "Game/UI/UI Colors")]
public class UIColors : ScriptableObject
{
    [Header("Colors")]
    [SerializeField] private Color _warningColor; // Color(255, 46, 39)
    [SerializeField] private Color _mainColorA; // Color(255, 96, 88);
    [SerializeField] private Color _mainColorB; // Color(94, 246, 255);
    [SerializeField] private Color _textBodyColor; // Color(221, 221, 221);
    [SerializeField] private Color _successColor; // Color(27, 215, 119);
    
    [Header("Factors")]
    [Tooltip("Alpha value for background colors. " +
        "The background color used is the color of the foreground" +
        " but with this alpha.")]
    [Range(.1f, .9f)]
    [SerializeField] private float _backgroundAlpha = 0.2f;

    public Color WarningColor => _warningColor;
    public Color MainColorA => _mainColorA;
    public Color MainColorB => _mainColorB;
    public Color TextBodyColor => _textBodyColor;
    public Color SuccessColor => _successColor;
    public Color MainColorABackground => new Color(_mainColorA.r,
        _mainColorA.g, _mainColorA.b, _backgroundAlpha);
    public Color MainColorBBackground => new Color(_mainColorB.r,
        _mainColorB.g, _mainColorB.b, _backgroundAlpha);
}
