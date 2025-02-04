using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveAdsPopup : PopupController
{
    public static RemoveAdsPopup instance;

    public Text bodyText;

    private void Awake()
    {
        instance = this;
    }
}
