# Changelog
All notable changes to this package will be documented in this file.


## [0.6.1] - 04/12/2024
**Bugfixes**
- Fixed an issue which prevented specific 3D or recently unmuted audioSources from playing sound when occlussion criteria begins true but changes mid-line (such as player teleporting mid-subtitle). AudioSources now play sound before the occlussion criteria for showing UI are checked.

## [0.6.0] - 13/11/2024
- Subtitler font and background color can now be changed from the inspector menu. 

## [0.5.0] - 26/09/2024
- Asset Store Release Version
- Improved Localization support. Localized Assets can now coexist with regular ones.

## [0.4.0] - 23/09/2024
- SRT File Support (Unity ScriptedImporter)
- Remove CySharp UniTask Dependency (All code moved to Native Async)
- SubtitleEntry() now supports CancellationTokens
- KillSubtitleById has been replaced with RemoveSubtitle(Id)
- RemoveSubtitle(Id) hides even ongoing subtitles immediately instead of just cancelling new ones in sequence.
- Improved Exit timings accuracy
- Added icon for Subtitler
- Improved text anchor customization

## [0.3.0] - 19/02/2024
- Unity Localization experimental support
- Subtitler now accepts ISubtitleEntry instead of a specific class
- SubtitleEntryData now implements ISubtitleEntry


## [0.2.1] - 04/02/2024
- Layouting performance improvement by marking VisualElements usageHints. (Pending benchmark)


## [0.2.0] - 31/01/2024
- UI Rework, Subtitler is now using UIToolkit instead of Unity.UI for runtime rendering and layouting.
- Removed dependency on DOTWeen
- Removed dependency on Addressables
- Removed dependency on TextMeshPro
- Fixed invalid layouting issues caused by previous GameObjects based solution.

## [0.1.4] - 26/01/2024
- Added Icon for ScriptableEvent
- Migrated ScriptableEvent to UnityAction instead of C# Delegates
- Added null check to ScriptableEvent to prevent exceptions
- Fixed build errors with Timeline due to Editor scripts not getting excluded


## [0.1.3] - 25/12/2023

### Timeline Support

- Added optional Timeline support (using #if TIMELINE_PRESENT)
- Separated subtitle sequences from entries

## [0.1.2] - 23/12/2023

### General Polish and Bugfixes

- Polished exit animations for the subtitles, which now smoothly push the background panel even on exit
- Minor cleanup in the code
- Added gizmos
- Added custom editor for the subtitle datas

## [0.1.1] - 12/12/2023

- Begin of changelog
