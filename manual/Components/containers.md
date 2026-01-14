# Subtitle Containers
Subtitle Containers are world objects that store data about which subtitles should be played. They do not contain playback logic. An example implementation can be found in the SubtitleContainer component.

## SubtitleContainer Component

![SubtitleContainer](../images/Screens/SubtitleContainer.PNG)

This component either triggers the Subtitler to play its SubtitleSequence automatically on `Start()`, or you can manually invoke it using `SubtitleContainer.Play();`.


#### Setting up Range-Limiting and Occlusion SubtitleContainer
Subtitler can automatically hide subtitles if they're difficult to hear. This does not mean the subtitles are not 'played'; they are just not displayed. This process evaluates properties of the AudioSource linked to the SubtitleContainer:


| AudioSource | Description |
| --- | --- |
| Spatial Blend | Value of 1 forces range-limiting. |
| Max Distance | If the player distance surpasses this value (and 3D Mix is set to 1), subtitles will not be visible. |
| Volume | If the current volume is low, subtitles will not display. | 
| Enabled | Subtitles will not show if the AudioSource is disabled. | 