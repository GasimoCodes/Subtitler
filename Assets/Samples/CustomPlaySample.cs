using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gasimo.Subtitles;


public class CustomPlaySample : MonoBehaviour
{
    public SubtitleDataEntry entry;

    public void DisplaySubtitle(string sub)
    {
        Subtitler.Instance.PlaySubtitleEntry(entry, null);
    }


}
