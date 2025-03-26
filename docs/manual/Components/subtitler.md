# Subtitler Prefab
To use Subtitler, drag the Subtitler Prefab from the package into your scene. Alternatively, copy it from the provided Samples.

![Prefab](../../images/Screens/Prefab.PNG)


## Changing Visuals
Subtitler exposes some commonly changed properties like font, font-size, text alignment (centered/left-aligned), background color and speaker highlight color.

## Transition Animations

### Transition ScriptableObject Assets
You can control text transitions by changing the `Transition Data`'s asset field. Several built-in transitions are available such as fade-in/out or a typewriter effect.
Full list of effects and their documentations are available [here](../Transition%20Assets/Intro.md).

### Using UIToolkit
Subtitler allows you to customize appearance using the UIToolkit document. You can modify transition animations by editing the Animations field of the `.Label_Hide` class or the `Label` objects.


