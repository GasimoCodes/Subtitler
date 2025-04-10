#if UNITY_EDITOR && SUBTITLER_LOCALIZATION
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Timeline;
using Gasimo.Subtitles.Localization;
using System;

namespace Gasimo.Subtitles.Timeline
{
    /// <summary>
    /// Custom Editor for the Timeline clip. Takes care of replacing the clip name and assigning duration property.
    /// </summary>
    [CustomTimelineEditor(typeof(SubtitlerLocalizedPlayableAsset))]
    public class SubtitlerLocalizedPlayableClipEditor : ClipEditor
    {

        // Called when a clip value, it's attached PlayableAsset, or an animation curve on a template is changed from the TimelineEditor.
        // This is used to keep the displayName of the clip matching the text of the PlayableAsset.
        public override void OnClipChanged(TimelineClip clip)
        {
            var textPlayableasset = clip.asset as SubtitlerLocalizedPlayableAsset;

            textPlayableasset.entry.displayFor = (float)clip.duration;

            try {
                if (textPlayableasset != null && !string.IsNullOrEmpty(textPlayableasset.entry.getDialogue()))
                    clip.displayName = textPlayableasset.entry.getSpeaker() + ": " + textPlayableasset.entry.getDialogue();
            } 
            catch (Exception e)
            {
                Debug.LogError("Error in SubtitlerLocalizedPlayableClipEditor: " + e.Message);
            }
        }
    }
}
#endif