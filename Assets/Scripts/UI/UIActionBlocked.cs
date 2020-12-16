using System.Collections;
using UnityEngine;

public class UIActionBlocked : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Wrapper enabled/disabled as needed")]
    private GameObject wrapper;

    [SerializeField]
    [Range(0.5f, 2f)]
    private float timeShowned = 1f;

    private WaitForSeconds showForTime;
    private bool isBeingShowned;

    private void Awake()
    {
        wrapper.SetActive(false);
        isBeingShowned = false;
        showForTime = new WaitForSeconds(timeShowned);
    }

    public void Show()
    {
        if (!isBeingShowned)
        {
            StartCoroutine(DisplayCR());
        }
    }

    private IEnumerator DisplayCR()
    {
        wrapper.SetActive(true);

        isBeingShowned = true;

        yield return showForTime;

        wrapper.SetActive(false);

        isBeingShowned = false;
    }
}
