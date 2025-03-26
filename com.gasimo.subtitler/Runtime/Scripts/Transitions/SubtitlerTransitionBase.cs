using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

namespace Gasimo.Subtitles
{
    public abstract class SubtitlerTransitionBase : ScriptableObject, ISubtitlerTransition
    {
        public const string ScriptableObjectMenuPath = "Gasimo/Subtitler/Transitions/";

        public virtual async Task AnimateSubtitleEntrance(Label subtitle, ISubtitleEntry entry, CancellationToken cancellationToken)
        {
            // Animate height + (Fade)
            subtitle.style.maxHeight = 0;
            subtitle.experimental.animation
                .Start(0, 999, 100, (element, value) => element.style.maxHeight = value)
                .Ease(Easing.InSine);

            await Task.Delay(50, cancellationToken);
            subtitle.RemoveFromClassList("Label_Hide");
            await Task.Delay(50, cancellationToken);
        }
        
        public virtual async Task AnimateSubtitleExit(Label subtitle, ISubtitleEntry entry, CancellationToken cancellationToken)
        {
            // Hide

            subtitle.AddToClassList("Label_Hide");

            subtitle.experimental.animation
                .Start(1000, 0, 100, (element, value) => element.style.maxHeight = value)
                .Ease(Easing.Linear);

            await Task.Delay(100, cancellationToken);
        }
    }
}
