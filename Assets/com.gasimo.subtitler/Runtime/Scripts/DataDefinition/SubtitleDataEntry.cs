using System;
using UnityEngine;

namespace Gasimo.Subtitles
{
    [Serializable]
    public class SubtitleDataEntry
    {

        [TextArea]
        public string dialogue;

        public string speaker;
        public AudioClip audio;

        public float waitFor = 1f;
        public int displayFor = 5;



        

    }
}