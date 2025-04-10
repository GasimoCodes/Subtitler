using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gasimo.Subtitles
{
    /// <summary>
    /// Interface for Subtitler AudioPlayer, allowing you to use your own audio backend.
    /// </summary>
    public interface IAudioPlayer
    {

        /// <summary>
        /// Play a single audio clip.
        /// </summary>
        /// <param name="audio"></param>
        void PlayOneShot(object audio);

        /// <summary>
        /// Set the listener used for calculating audio occlusion and spatialization.
        /// </summary>
        /// <param name="listener"></param>
        void SetListener(object listener);


        /// <summary>
        /// Check whether this audio source is hearable right now.
        /// </summary>
        /// <returns> True if the audio source is hearable depending on volume, enabled, spatial mix and distance from listener, false otherwise.</returns>
        bool IsHearable();

        /// <summary>
        /// Set the TimeScale of this audio source. This is used to pause the audio source when the game is paused.
        /// </summary>
        /// <param name="timeScale"></param>
        void SetTimeScale(float timeScale);

        /// <summary>
        /// Pause this audio source.
        /// </summary>
        void Pause();

        /// <summary>
        /// Resume this audio source.
        /// </summary>
        void Resume();

        /// <summary>
        /// Stop this audio source.
        /// </summary>
        void Stop();
    }
}
