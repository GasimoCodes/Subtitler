# Events
Subtitler allows subscription to simple C# events within subtitle entries. These events trigger when their corresponding entry appears.

## ScriptableEvent
A [ScriptableObject](https://docs.unity3d.com/Manual/class-ScriptableObject.html) file. Can be created using the *Gasimo/Subtitler/ScriptableEvent* context menu. 
You can subscribe to the ScriptableEvent by calling 

```csharp
onEventRaised += (your_action);
```

