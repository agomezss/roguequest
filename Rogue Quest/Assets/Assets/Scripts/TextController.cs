using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public void CreateText(Transform location)
    {
        FeedbackText popupText = Resources.Load<FeedbackText>("FeedbackText");
        Vector2 screenPos = UnityEngine.Camera.main.WorldToScreenPoint(location.position);

        var instance = Instantiate(popupText);
        instance.transform.SetParent(location.transform, false);
        instance.transform.position = screenPos;

        instance.LoadTexts(location);
    }
}