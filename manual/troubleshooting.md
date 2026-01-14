### Common Questions

#### Subtitler does not render on top of my UI when I dont want it to and vice-versa
Change Subtitler's UIDocument Sort Order

#### Can I use Subtitler with VR, XR, UGUI or using world-space canvases?
While not officially supported by UIToolkit yet, you can set the UIToolkit's render output to render into a RenderTexture and use that with regular UGUI by assigning it to an RawImage.

#### Advanced: I wish to change the UI in depth / integrate Subtitler into my own UIToolkit document
For deep UI changes or integration into your UIToolkit document, copy the Subtitler VisualTreeAsset and USS styling sheets. Retain the same classes and overridden values for proper layout. Be aware that Subtitler VisualElement Labels are dynamically instantiated, not copied.

#### I want to create my own transitions using C#
See the documentation page on [customizing transitions](./Transition%20Assets/Custom%20Transitions.md).