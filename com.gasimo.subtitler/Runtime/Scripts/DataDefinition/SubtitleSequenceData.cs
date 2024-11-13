using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gasimo.Subtitles
{
    /// <summary>
    /// Contains data holding a single Closed Caption sequence. For individual lines, see SubtitleDataEntry.
    /// </summary>
    [CreateAssetMenu(fileName = "SubtitleFile", menuName = "Gasimo/Subtitler/SubtitleFile")]
    [Serializable]
    [HelpURL(Subtitler.siteURL + "manual/Components/data.html#subtitler-sequence")]
    public class SubtitleSequenceData : ScriptableObject, ISubtitleSequence
    {
        /// <summary>
        /// Sequence of Subtitles
        /// </summary>
        [SerializeField]
        public SubtitleDataEntry[] Subtitles;

        public ISubtitleEntry[] GetSubtitleEntries()
        {
            return Subtitles;
        }
    }

}