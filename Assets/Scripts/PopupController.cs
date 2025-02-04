using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    public Transform popupChild;

    public virtual void OnEnable()
    {
        popupChild.DOScale(new Vector3(1.1f, 1.1f, 1.1f), .2f).OnComplete(delegate
        {
            popupChild.DOScale(Vector3.one, .2f);
        });
    }

    private void OnDisable()
    {
        popupChild.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    public void ClosePopup()
    {
        popupChild.DOScale(new Vector3(1.1f, 1.1f, 1.1f), .2f).OnComplete(delegate
        {
            popupChild.DOScale(new Vector3(0.5f, 0.5f, 0.5f), .2f).OnComplete(delegate
            {
                gameObject.SetActive(false);
            });
        });
       
    }
}