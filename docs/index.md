---
_disableBreadcrumb: true
_layout: landing
---

<div style="display: flex; gap: 40px;">
    <div>
    <h1>Welcome to <b>Subtitler</b> docs!</h1>
    <p style="font-size: 18px;">Subtitler provides an easy-to-use solution for Subtitling/Closed-Captions in Unity3D. Report bugs or feature requests [here](https://github.com/GasimoCodes/Subtitler-Public/issues/new/choose).<br>
    </p>
    <a href="./manual/Getting Started/importing.html">Manual</a> | <a href="./api/Gasimo.Subtitles.html">API</a> | <a href="https://assetstore.unity.com/packages/tools/utilities/subtitler-closed-captions-toolkit-256323">Asset Store</a> 
    </div>
    <iframe style="width: 1200px; margin-right: 20px;" width="1200" height="320" src="https://www.youtube-nocookie.com/embed/21mGZe4i70Y?si=7g36yQwpx0-H6OBL" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
</div>


## Features

- Cull subtitles based on Player Distance / Audio Volume
- Define dialogue entries with variable timing offsets and display lengths
- Assign AudioClip to each dialogue entry, or use one big clip for whole dialogue instead (useful with videos or heavily processed sounds which cant be split by lines)
- Rich Text Support
- UI Options (Alignment, background panel visibility, Font size)
- Trigger events (ScriptableEvent) when a certain line gets played
- Fancy Editor Subtitle Authoring tool (>=2023.3, fallbacks to normal Inspector)
- API to hook your custom subtitles scripts or logic
- API to stop and hide currently playing subtitles using their runtime ID
- Timeline Integration
- Unity Localization support
- SRT Files Import Support
  
## Possible Future Features

- Optional Addressables for audioClips support
- FMod Support
