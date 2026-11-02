### Templates for making Slay the Spire 2 mods using MoDot, a Godot 4.5.1 fork.

Contains 3 templates.


MoDot Sts2 Project - Empty : Sets up an empty mod with BaseLib as a dependency. If you don't need it, remove it from the csproj and mod manifest json.

MoDot Sts2 Project - Content : Sets up a content mod.

MoDot Sts2 Project - Character : Sets up a character mod.

When creating a solution using these templates, make sure to enable "Put solution and project in same directory". This is required for it to work as-is with MoDot.

---

## Setup after project creation

*This assumes you are familiar with setting up Sts2 mods via Alchyrs original template. This only describes changes and addition.*

1. In `Directory.Build.props` set the `<ModotPath>` to a MoDot installations `.exe`. Point `<ModotModsPath>` to a subdirectory in the MoDot installation called `mods`.
2. In `override.cfg` set the `mounted_assemblies/paths` to the `sts2.dll`. Set the `mounted_packs/paths` to the `SlayTheSpire2.pck` in the game's folder and optional to the pcks of all mods your mod depends on.
   Use `/` in paths (or `\\`); a single `\` is an escape character in this file. (When you change these paths in Project Settings, MoDot saves them to `override.cfg`, never to `project.godot`.)
3. Put the mods the game needs when you run it from the editor into MoDot's `mods` folder:
   - `mods/_Always/` for mods every project uses, e.g. BaseLib.
   - `mods/<ProjectName>Mods/` for mods only this project uses. Debug builds of your mod are copied to `mods/<ProjectName>Mods/<ProjectName>/` automatically.

   Other folders in `mods/` are ignored when running from the editor. Release builds and Publish still go to the game's own `mods` folder.

There is no `addons` folder to set up: the game's addons come from its pack (see below).

## The game's addons

The project has no copy of the game's addons. MoDot takes them from the mounted `SlayTheSpire2.pck`, so they are always the installed game's version. Which ones is set in `project.godot`, in `editor/mounted_packs/addons` (restart the editor after changing it):

- `res://addons/spine` and `res://addons/fmod`: Spine animations and FMOD audio. Their native libraries are the ones next to the game's `.exe`.
- `res://addons/mega_text`, `res://addons/dev_tools`, `res://addons/megacontentcreator`: the game's editor plugins. They are enabled as usual in *Project > Project Settings > Plugins* (`editor_plugins/enabled`), where they are marked with the pack they come from.

The game's Sentry addon (crash reporting) is deliberately not listed.

You can still add addons of your own to `res://addons/`. A file on disk always wins over the pack's file at the same path.

### Optional: FMOD's and Spine's editor tools

The game only ships the release builds of the FMOD and Spine libraries. Playback works with them, but some editor tools need the editor builds: without them, MoDot leaves out the FMOD plugin (the FMOD button, the bank explorer, the event pickers in the Inspector) and says so in the Output panel. To use them, put these files into a folder `gdextension_libraries` next to the MoDot `.exe`:

- FMOD: `libGodotFmod.windows.editor.x86_64.dll`, `fmodL.dll`, `fmodstudioL.dll`
- Spine (only needed for Spine's editor tools, e.g. importing your own Spine files): `libspine_godot.windows.editor.x86_64.dll`

They come with the addons' own releases ([fmod-gdextension](https://github.com/utopia-rise/fmod-gdextension), [spine-godot](https://esotericsoftware.com/spine-godot)); use the versions the game uses. Restart the editor afterwards.

## Packing the mod

Publish packs the mod's `.pck` with a mod packer that ships with MoDot (`mod_packers/` next to the MoDot `.exe`), using `--pack-mod`. There is no packing script in the project. The default packer, `modot.default`, packs:

- the mod folder (`<ProjectName>/`), with the imported files the game needs instead of their sources;
- the C# scripts in `<ProjectName>Code/` with their `.uid` files;
- project files outside the mod folder that the mod's files use (e.g. a shader in `res://shaders/`), but never the game's own files.

Godot's own export isn't suited for mods: it always adds the project's settings and caches to the pack, which would replace the game's when the mod loads.

You can also pack from the editor with *Project > Pack Mod...*: choose the packer and the output path (saved in `override.cfg`). That makes only the `.pck`; the `.dll` and `.json` still come from building the C# project.

## Pre-patches

The templates enable two pre-patches that ship with MoDot (`pre_patches/` next to the MoDot `.exe`). They patch the game before it starts, only when it runs from the editor:

- `modot.native.separateprojectmods`: loads mods only from `mods/_Always/` and `mods/<ProjectName>Mods/` (see step 3), instead of everything in `mods/`.
- `modot.baselib.modeldbpreload`: makes the game run `ModelDb.Preload()` in the editor too, which BaseLib relies on.

You can see and change them in *Project > Project Settings > Pre-Patches*. The enabled list is saved in `project.godot`, so everyone working on the project uses the same pre-patches.

## Moving an existing project to the addons from the pack

For projects made with an earlier version of these templates, with the game's `addons` copied into the project:

1. Close the editor. Delete the project's `addons` folder (or the link to it).
2. Delete the project's `.godot/imported` folder. It still has copies of the game's imported files, which would hide the current game's files; the editor reimports your own files.
3. In `project.godot`, add the `mounted_packs/addons` line to the `[editor]` section, as in the templates.
4. In the `.csproj`, remove `<Compile Remove="addons/**"/>`, `<EmbeddedResource Remove="addons/**"/>`, and the `Sentry.Godot.props` import with the `PackageReference Remove="Sentry"` after it. In `.gitignore`, remove `/addons/`.
5. Optional: delete `editor/IsEditorPatches.cs`, if you have it; the pre-patch `modot.baselib.modeldbpreload` does the same.

---

This is a fork of [Alchyrs](https://github.com/Alchyr) [Sts2ModTemplate](https://github.com/Alchyr/ModTemplate-StS2).

Licensed under the MIT License, see [LICENSE](LICENSE). The original templates are MIT-licensed by Alchyr.
