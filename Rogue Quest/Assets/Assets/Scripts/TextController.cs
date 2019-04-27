using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public static FeedbackText popupText;
    private static GameObject canvas;

    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
        popupText = Resources.Load<FeedbackText>("Prefabs/FeedbackText");
        var instance = Instantiate(popupText);
        instance.transform.SetParent(gameObject.transform);
    }
}