using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackText : MonoBehaviour
{
    public string Text;
    public string Info;
    public bool StatsOrCollectibleName = true;
    public Text NameText;
    public Text InfoText;

    public GameObject parent;

    public void ShowText(string txt)
    {
        NameText.text = txt;
        NameText.enabled = true;
    }

    public void HideText()
    {
        NameText.text = string.Empty;
        NameText.enabled = false;
    }

    public void ShowInfo(string info, float time)
    {
        StartCoroutine(ShowInfoAsync(info, time));
    }

    public void HideInfo()
    {
        InfoText.text = string.Empty;
        InfoText.enabled = false;
    }

    IEnumerator ShowInfoAsync(string info, float time)
    {
        InfoText.text = info;
        InfoText.enabled = true;
        yield return new WaitForSeconds(time);

        InfoText.enabled = false;
    }

    private void Awake()
    {
        if (transform.parent && transform.parent.gameObject)
            parent = transform.parent.gameObject;

        InfoText.enabled = false;
        NameText.enabled = false;

        InitialLoad();
    }

    private void Update()
    {
        float offsetPosY = parent.transform.position.y + 1.5f;
        Vector3 offsetPos = new Vector3(parent.transform.position.x, offsetPosY, parent.transform.position.z);

        // Calculate *screen* position (note, not a canvas/recttransform position)
        Vector2 canvasPos;
        
        Vector2 screenPoint = UnityEngine.Camera.main.WorldToScreenPoint(offsetPos);

        // Convert screen position to Canvas / RectTransform space <- leave camera null if Screen Space Overlay
        var canvasRect = GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, null, out canvasPos);

        // Set
        transform.localPosition = canvasPos;
    }

    private void InitialLoad()
    {
        if (StatsOrCollectibleName)
        {
            var stats = parent.GetComponent<Stats>();

            if (stats)
            {
                ShowText(stats.Name);
                return;
            }

            var collectible = parent.GetComponent<Collectible>();

            if (collectible)
            {
                ShowText(collectible.Name);
                return;
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(Text))
            {
                ShowText(Text);
            }
        }
    }

}