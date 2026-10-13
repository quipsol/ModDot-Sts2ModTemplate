### Templates for making Slay the Spire 2 mods using MoDot, a Godot 4.5.1 fork.

Contains 3 templates.


MoDot Sts2 Project - Empty : Sets up an empty mod with BaseLib as a dependency. If you don't need it, remove it from the csproj and mod manifest json.

MoDot Sts2 Project - Content : Sets up a content mod.

MoDot Sts2 Project - Character : Sets up a character mod.

When creating a solution using these templates, make sure to enable "Put solution and project in same directory". This is required for it to work as-is with MoDot.

---

## Setup after project creation

*This assumes you are familiar with setting up Sts2 mods via Alchyrs original template. This only describes changes and addition.*

1. In `Directory.Build.props` set the `<GodotPath>` to a MoDot installations `.exe`. Point `<GodotModsPath>` to a subdirectory in the MoDot installation called `mods`.
2. In `override.cfg` set the `mounted_assemblies/paths` to the `sts2.dll`. Set the `mounted_packs/paths` to `SlayTheSpire2.pck` and optional to the pcks of all mods your mod depends on.
   Use `/` in paths (or `\\`); a single `\` is an escape character in this file. (When you change these paths in Project Settings, MoDot saves them to `override.cfg`, never to `project.godot`.)
3. Use [GDRE Tools](https://github.com/GDRETools/gdsdecomp/releases) to extract the `SlayTheSpire2.pck`. Copy the `addons` folder into your project. It should already be gitignored.
4. Put the mods the game needs when you run it from the editor into MoDot's `mods` folder:
   - `mods/_Always/` for mods every project uses, e.g. BaseLib.
   - `mods/<ProjectName>Mods/` for mods only this project uses. Debug builds of your mod are copied to `mods/<ProjectName>Mods/<ProjectName>/` automatically.

   Other folders in `mods/` are ignored when running from the editor. Release builds and Publish still go to the game's own `mods` folder.

## Pre-patches

The templates enable two pre-patches that ship with MoDot (`pre_patches/` next to the MoDot `.exe`). They patch the game before it starts, only when it runs from the editor:

- `modot.native.separateprojectmods`: loads mods only from `mods/_Always/` and `mods/<ProjectName>Mods/` (see step 4), instead of everything in `mods/`.
- `modot.baselib.modeldbpreload`: makes the game run `ModelDb.Preload()` in the editor too, which BaseLib relies on.

You can see and change them in *Project > Project Settings > Pre-Patches*. The enabled list is saved in `project.godot`, so everyone working on the project uses the same pre-patches.

---

This is a fork of [Alchyrs](https://github.com/Alchyr) [Sts2ModTemplate](https://github.com/Alchyr/ModTemplate-StS2).

Licensed under the MIT License, see [LICENSE](LICENSE). The original templates are MIT-licensed by Alchyr.
