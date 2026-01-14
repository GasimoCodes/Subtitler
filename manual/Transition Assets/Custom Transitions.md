# Custom Transition Logic
Transition assets receive signals from Subtitler which indicate which label should be animated or hidden.

Assets need to inherit from the **SubtitlerTransitionBase** class and may override the following methods:

```csharp
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
```

All the overriding is optional. By default, the parent class contains all logic necessary for
1) Animating the text scale to smoothly push text above and below to make space.
2) Adding or removing the `.Label_Hide` tag to trigger fade-in-out transitions.

By overriding the parent methods you will need to handle the transition yourself, or optionally, you may call the parent method in your methods and then hook up some custom code like the typewriter effect does which mixes fading it with letter insertion.

> Subtitler automatically adjusts the display time of an entry based on the time it took to perform `AnimateSubtitleEntrance`. That means you do not need to pad out your animations to accomodate entry displayFor lengths or track durations. For this calculation to work properly however, the whole duration of your entry transition should be awaited. Please pass the `cancellationToken` along to any awaits you have.