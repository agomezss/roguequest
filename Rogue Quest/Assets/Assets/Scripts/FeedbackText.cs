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
        InfoText.enabled = false;
        NameText.enabled = false;
    }

    public void LoadTexts()
    {
        if (transform.parent && transform.parent.gameObject)
            parent = transform.parent.gameObject;

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