using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gasimo.Subtitles
{
    [CreateAssetMenu(fileName = "Typewriter Transition", menuName = SubtitlerTransitionBase.ScriptableObjectMenuPath + "Typewriter")]
    public class SubtitlerTypewriter : SubtitlerTransitionBase
    {

        public override async Task AnimateSubtitleEntrance(Label subtitle, ISubtitleEntry entry, CancellationToken cancellationToken)
        {
            string fullText = subtitle.text;
            subtitle.text = "";

            await base.AnimateSubtitleEntrance(subtitle, entry, cancellationToken);

            float typeDelay = entry.getDisplayFor() / fullText.Length / 2;

            // Regex to find rich text tags and text segments
            var pattern = new System.Text.RegularExpressions.Regex(@"(<[^>]+>)|([^<]+)");
            var matches = pattern.Matches(fullText);

            // Prepare a list to track visible and invisible parts
            var segments = new List<(string content, bool isTag)>();

            // Categorize matches into segments
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                if (match.Groups[1].Success)
                {
                    // It's a tag
                    segments.Add((match.Value, true));
                }
                else if (match.Groups[2].Success)
                {
                    // It's text content
                    segments.Add((match.Value, false));
                }
            }

            // Rebuild the text with typing effect
            var visibleBuilder = new System.Text.StringBuilder();

            foreach (var segment in segments)
            {
                if (segment.isTag)
                {
                    // Always include tags
                    visibleBuilder.Append(segment.content);
                }
                else
                {
                    // Type out text segments character by character
                    for (int j = 0; j < segment.content.Length; j++)
                    {
                        visibleBuilder.Append(segment.content[j]);

                        // Find the index of the current visible text within the full text
                        int visibleIndex = fullText.IndexOf(visibleBuilder.ToString());

                        // Construct the text with everything to the left being invisible
                        var constructedText = (visibleIndex > 0 ? $"<alpha=#00>{fullText.Substring(0, visibleIndex)}</alpha>" : "") + visibleBuilder.ToString();

                        subtitle.text = constructedText;

                        await Task.Delay((int)(typeDelay * 1000), cancellationToken);

                    }
                }
            }

            // Ensure final text is fully visible
            subtitle.text = fullText;

            
        }
    }
}
