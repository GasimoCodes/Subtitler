using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

namespace Gasimo.Subtitles
{
    /// <summary>
    /// Interface to define transition animations for subtitle lines.
    /// </summary>
    public interface ISubtitlerTransition
    {
        /// <summary>
        /// Called when a subtitle entry is about to be displayed.
        /// </summary>
        /// <param name="subtitle">Label containing this subtitle</param>
        /// <param name="cancellationToken">Token which gets called when the entry is interrupted.</param>
        /// <returns>Task containing the animaion</returns>
        public abstract Task AnimateSubtitleEntrance(Label subtitle, ISubtitleEntry entry, CancellationToken cancellationToken);


        /// <summary>
        /// Called when a subtitle entry is about to be hidden.
        /// </summary>
        /// <param name="subtitle"> Label containing this subtitle</param>
        /// <param name="cancellationToken"> Token which gets called when the entry is interrupted.</param>
        /// <returns></returns>
        public abstract Task AnimateSubtitleExit(Label subtitle, ISubtitleEntry entry, CancellationToken cancellationToken);

        /// <summary>
        /// Called when a label is created in the Subtitler Pool. Use this to initialize the label with any custom settings required.
        /// Subtitler already sets any properties exposed in the Subtitler component.
        /// </summary>
        /// <param name="subtitle"></param>
        public virtual void OnLabelCreated(Label subtitle)
        {
            subtitle.AddToClassList("Label_Hide");

        }
        

    }
}
