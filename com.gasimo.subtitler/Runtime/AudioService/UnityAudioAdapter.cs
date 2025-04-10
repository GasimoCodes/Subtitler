using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Gasimo.Subtitles
{
    /// <summary>
    /// Unity implementation of the IAudioPlayer interface allowing you to play AudioClips through the AudioSource component using Subtitler.
    /// </summary>
    public class UnityAudioAdapter : IAudioPlayer
    {
        private AudioSource audioSource;
        public AudioListener listener { get; private set; }

        public UnityAudioAdapter(AudioSource audioSource)
        {
            this.audioSource = audioSource;
        }

        public UnityAudioAdapter(AudioSource audioSource, AudioListener listener)
        {
            this.audioSource = audioSource;
            this.listener = listener;
        }

        public bool IsHearable()
        {
            if (audioSource == null) return true;
            if (audioSource.volume <= 0.05f || !audioSource.enabled) return false;
            if (audioSource.spatialBlend == 1 && !IsWithinAudioRange(audioSource)) return false;
            return true;
        }

        private bool IsWithinAudioRange(AudioSource audioSource)
        {
            if (audioSource == null) return true;
            return Vector3.Distance(listener.transform.position, audioSource.transform.position) <= audioSource.maxDistance;
        }

        public void Pause()
        {
            if (audioSource == null) return;
            audioSource.Pause();
        }

        public void PlayOneShot(object audio)
        {
            if (audio is AudioClip clip && clip != null)
            {
                if (audioSource == null) return;
                audioSource.PlayOneShot(clip);
            }
        }

        public void Resume()
        {
            audioSource.UnPause();
        }

        public void SetListener(object listener)
        {
            if (!(listener is AudioListener))
            {
                throw new ArgumentException("Listener must be of type AudioListener");
            }

            this.listener = listener as AudioListener;
        }

        public void SetTimeScale(float timeScale)
        {
            if (audioSource == null) return;

            audioSource.pitch = timeScale;
        }

        public void Stop()
        {
            if (audioSource == null) return;
            audioSource.Stop();
        }
    }
}
