using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTapExit : MonoBehaviour
{
    [SerializeField]
    GameObject exitCanvas;

    [SerializeField]
    float waitTime;

    bool backPressedOnce;

    IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        exitCanvas.SetActive(false);
        backPressedOnce = false;

    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape) && !backPressedOnce)
        {
            ShowMessage();
        }
    }
    void ShowMessage()
    {
        backPressedOnce = true;
        exitCanvas.SetActive(true);
        coroutine = waitCountDown();
        StartCoroutine(coroutine); 
    }
    void HideMessage()
    {
        backPressedOnce = false;
        exitCanvas.SetActive(false);
        StopCoroutine(coroutine);
    }
    IEnumerator waitCountDown()
    {
        yield return null;
        var tempTime = 0f;
        while(tempTime < waitTime)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
                Debug.Log("Quit is Done");
            }
            else if (Input.touchCount > 0)
            {
                HideMessage();
            }
            tempTime +=Time.deltaTime;
            yield return null;
        }
        HideMessage();
    }
}
