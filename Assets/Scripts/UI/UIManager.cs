using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RawImage _hijackView;

    public void ToggleHijackView()
    {
        _hijackView.enabled = !_hijackView.enabled;
    }
}
