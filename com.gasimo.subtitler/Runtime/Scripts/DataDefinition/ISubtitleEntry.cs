using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gasimo.Subtitles
{
    /// <summary>
    /// Interface for describing subtitles usable in Subtitler
    /// </summary>
    public interface ISubtitleEntry
    {
        /// <summary>
        /// AudioClip of the subtitle
        /// </summary>
        /// <returns>Object containing the sound for this entry to be played with an IAudioSource</returns>
        public object getAudio();

        /// <summary>
        /// Dialogue string of the subtitle
        /// </summary>
        /// <returns>Dialogue</returns>
        public string getDialogue();
        /// <summary>
        /// Speaker name for the subtitle, will prefix the dialogue in this format: speaker:dialogue (Optional)
        /// </summary>
        /// <returns>Speaker Name</returns>
        public string getSpeaker();

        /// <summary>
        /// How long should subtitler pause before playing this line? (If in sequence, relative to last played subtitle)
        /// </summary>
        /// <returns></returns>
        public float getWaitFor();

        /// <summary>
        /// How long should the subtitle be visible for?
        /// </summary>
        /// <returns></returns>
        public float getDisplayFor();

        public ScriptableEvent getSubtitleEvent();

        
    }
}
