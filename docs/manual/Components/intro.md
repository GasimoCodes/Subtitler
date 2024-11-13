# Overview
The subtitling system consists of three main components:

## [Subtitler](subtitler.md)
This is the core component responsible for all subtitling UI and logic. Use the singleton `Subtitler.Instance();` for custom subtitle systems.

## [Subtitle Containers](containers.md)
Represents a single subtitle source in the world. Holds a reference to a Subtitle Sequence/Entry.

## [Data](data.md)
A data asset containing closed-captions sequence. Unity 2023.3+ provides a custom editor for enhanced editing.



