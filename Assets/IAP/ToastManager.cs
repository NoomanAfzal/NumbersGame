using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ToastManager : MonoBehaviour
{
    public static ToastManager instance;

    public Text toastText;
    public float displayDuration = 2f;

    private void Awake()
    {
        instance = this;
    }

    public void ShowToast(string message)
    {
        if (toastText != null)
        {
            toastText.text = message;
            StartCoroutine(ShowAndHideToast());
        }
        else
        {
            Debug.LogError("Toast text element is not assigned in the inspector.");
        }
    }

    private IEnumerator ShowAndHideToast()
    {
        toastText.gameObject.SetActive(true);

        yield return new WaitForSeconds(displayDuration);

        toastText.gameObject.SetActive(false);
    }
}

