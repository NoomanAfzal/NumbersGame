using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardScriptableObject : MonoBehaviour
{

    public static RewardScriptableObject instance;

    private void Awake()
    {
        instance = this;
    }

    public int tipLightCount
    {
        get
        {
            return PlayerPrefs.GetInt("LightTip", 5);
        }
        set
        {
            PlayerPrefs.SetInt("LightTip", value);
        }
    }
    public int tipRemoveCount
    {
        get
        {
            return PlayerPrefs.GetInt("RemoveTip", 5);
        }
        set
        {
            PlayerPrefs.SetInt("RemoveTip", value);
        }
    }
    public int tipUndoCount
    {
        get
        {
            return PlayerPrefs.GetInt("UndoTip", 5);
        }
        set
        {
            PlayerPrefs.SetInt("UndoTip", value);
        }
    }
}
