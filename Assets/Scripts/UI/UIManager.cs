using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private RawImage hijackView;

    public void ToggleHijackView()
    {
        hijackView.enabled = !hijackView.enabled;
    }
}
