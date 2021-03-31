using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIActionBlocked : MonoBehaviour
{
    [Tooltip("Wrapper enabled/disabled as needed")]
    [SerializeField] private GameObject _wrapper;

    [Range(0.5f, 5f)]
    [SerializeField] private float _timeShowned = 1f;

    [SerializeField] private UIColors _colors;

    private WaitForSeconds _showForTime;
    private bool _isBeingShowned;

    private Image _background;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _background = _wrapper.GetComponent<Image>();
        _text = _wrapper.GetComponentInChildren<TextMeshProUGUI>();

        _wrapper.SetActive(false);
        _isBeingShowned = false;
        _showForTime = new WaitForSeconds(_timeShowned);
    }

    public void Show()
    {
        if (!_isBeingShowned)
        {
            _background.color = _colors.MainColorABackground;
            _text.color = _colors.MainColorA;

            StartCoroutine(DisplayCR());
        }
    }

    private IEnumerator DisplayCR()
    {
        _wrapper.SetActive(true);

        _isBeingShowned = true;

        yield return _showForTime;

        _wrapper.SetActive(false);

        _isBeingShowned = false;
    }
}
