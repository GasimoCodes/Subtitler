using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gasimo.Subtitles
{
    /// <summary>
    /// Component representing one subtitle source in the world. 
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    [HelpURL(Subtitler.siteURL + "manual/Components/containers.html")]
    public class SubtitleContainer : MonoBehaviour
    {
        /// <summary>
        /// Play automatically on awake?
        /// </summary>
        public bool autoPlay = true;

        /// <summary>
        /// Subtitle Data to play
        /// </summary>
        public SubtitleSequenceData subtitleData;

        private int subtitleId;

        // Start is called before the first frame update
        void Start()
        {
            if (autoPlay)
            {
                Play();
            }
        }

        /// <summary>
        /// Plays the attached Subtitle Data
        /// </summary>
        public void Play()
        {
            subtitleId = Subtitler.Instance.PlaySubtitleSequence(subtitleData, this.GetComponent<AudioSource>());
        }

        /// <summary>
        /// Stops the sequence assigned to this Container
        /// </summary>
        public void Stop()
        {
            Subtitler.Instance.RemoveSubtitle(subtitleId);
        }
    }
}