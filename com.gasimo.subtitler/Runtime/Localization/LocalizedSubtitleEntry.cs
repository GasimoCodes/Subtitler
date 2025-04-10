using System;
using UnityEngine;
using UnityEngine.Localization;

namespace Gasimo.Subtitles.Localization
{
    /// <summary>
    /// Localized Subtitle Entry variant
    /// </summary>
    [Serializable]
    public class LocalizedSubtitleEntry : ISubtitleEntry
    {

        /// <summary>
        /// Localized dialogue
        /// </summary>
        [SerializeField]
        public LocalizedString dialogue;

        /// <summary>
        /// Localized Speaker
        /// </summary>
        [SerializeField]
        public LocalizedString speaker;

        /// <summary>
        /// Localized AudioClip
        /// </summary>
        [SerializeField]
        public LocalizedAudioClip audio;

        /// <summary>
        /// Programmable event which gets ivoked when this subtitle gets played. 
        /// Useful to trigger mechanics exactly when player hears a certain line.
        /// </summary>
        public ScriptableEvent subtitleEvent;

        /// <summary>
        /// Delay between last Subtitle line and this one. Leave 0 to play at same time as previous line.
        /// </summary>
        public float waitFor = 1f;

        /// <summary>
        /// How long will this subtitle be displayed for.
        /// </summary>
        public float displayFor = 5;


        public float getDisplayFor()
        {
            return displayFor;
        }


        public ScriptableEvent getSubtitleEvent()
        {
            return subtitleEvent;
        }

        public float getWaitFor()
        {
            return waitFor;
        }

        public object getAudio()
        {
            if (audio != null && audio.IsEmpty != true)
            {
                return audio.LoadAsset();
            }
            else
            {
                return null;
            }

        }

        public string getDialogue()
        {
            if (dialogue != null && dialogue.IsEmpty != true)
            {
                return dialogue.GetLocalizedString();
            }
            else
            {
                return "";
            }
        }

        public string getSpeaker()
        {
            if (speaker != null && speaker.IsEmpty != true)
            {
                return speaker.GetLocalizedString();
            }
            else
            {
                return "";
            }
        }


    }
}
