### Templates for making Slay the Spire 2 mods using ModDot, a Godot 4.5.1 fork.

Contains 3 templates.


ModDot Sts2 Project - Empty : Sets up an empty mod with BaseLib as a dependency. If you don't need it, remove it from the csproj and mod manifest json.

ModDot Sts2 Project - Content : Sets up a content mod.

ModDot Sts2 Project - Character : Sets up a character mod.

When creating a solution using these templates, make sure to enable "Put solution and project in same directory". This is required for it to work as-is with ModDot.

---

## Setup after project creation

*This assumes you are familiar with setting up Sts2 mods via Alchyrs original template. This only describes changes and addition.*

1. In `Directory.Build.props` set the `<GodotPath>` to a ModDot installations `.exe`. Point `<GodotModsPath>` to a subdirectory in the ModDot installation called `mods`.
2. In `override.cfg` set the `mounted_assemblies/paths` to the `sts2.dll`. Set the `mounted_packs/paths` to `SlayTheSpire2.pck` and optional to the pcks of all mods your mod depends on.
3. Use [GDRE Tools](https://github.com/GDRETools/gdsdecomp/releases) to extract the `SlayTheSpire2.pck`. Copy the `addons` folder into your project. It should already be gitignored.

---

This is a fork of [Alchyrs](https://github.com/Alchyr) [Sts2ModTemplate](https://github.com/Alchyr/ModTemplate-StS2).

Licensed under the MIT License, see [LICENSE](LICENSE). The original templates are MIT-licensed by Alchyr.
