# Dependencies - An open-source modern Dependency Walker

[![Build status](https://ci.appveyor.com/api/projects/status/wtr5v8ksndbkkqxg?svg=true)](https://ci.appveyor.com/project/lucasg/dependencies)

### [Download here](https://github.com/himeshsameera/Dependencies/releases/download/v2.2.0/Dependencies_v2.2.0_x64.zip)

NB : due to [limitations on /clr compilation](https://msdn.microsoft.com/en-us/library/ffkc918h.aspx), `Dependencies` needs [Visual C++  Redistributable](https://support.microsoft.com/en-us/help/2977003/the-latest-supported-visual-c-downloads) installed to run properly.

<p align="center">
<img alt="Usage Exemple" src="screenshots/UsageExemple.gif"/>
</p>

## Overview

`Dependencies` is a rewrite of the legacy software [Dependency Walker](http://www.dependencywalker.com/) which was shipped along Windows SDKs, but whose development stopped around 2006.
`Dependencies` can help Windows developers troubleshooting their dll load dependencies issues.

## Releases

- [v2.2.0](https://github.com/himeshsameera/Dependencies/releases/tag/v2.2.0)
  
  - Fixed Issue [#272](https://github.com/lucasg/Dependencies/issues/272). When showing the Import list, missing imports will be shown at the top. 
  - Fixed Issue [#285](https://github.com/lucasg/Dependencies/issues/285) Copying dll files from the dependencies GUI.
  - Improved resolving dependancies for CLR dlls that use mono cecil
  - Fixed issues with CI system that came up after upgrading to VS2022. Thank you [xiaoweiChen](https://github.com/xiaoweiChen) for the code change [code change](https://github.com/bb107/MemoryModulePP/pull/54/commits/7f37dc56f7b9f799ac11b733153c4ef1d14e71df) which I used as a reference.

- [v2.1.0](https://github.com/himeshsameera/Dependencies/releases/download/v2.1.0/Dependencies_v2.1.0_x64.zip) :
  
  - Mearged several pull requests. Following are the fixes,
    - Pull request 266 - Now the root module data is displayed
    - Pull request 269 - No duplicate items in recent menu.
    - Pull request 265 - Binary Cache perfomace is improved.
    - Pull request 246 - Potential memory leak is fixed.
    - Pull request 260 - Copy file path and open in explorer options are added.
  - Updated code to use VS2022, Platform toolset V143 Windows SDK 10
  - Thank You all the contributors of the pull requests [viruscamp](https://github.com/viruscamp), [wekoil](https://github.com/wekoil) and [DawmosTomie](https://github.com/DawmosTomie). I'll review and merge the other pull requests as the time permits.
* [v2.0-alpha](https://github.com/himeshsameera/Dependencies/releases/download/V2.0-alpha/Dependencies_V2.0-alpha_x64.zip) :
  
  * Changes from HimeshSameera repository
  * Fixed some issues with loading .NET dlls (mostly x64 ones) to Dependencies.
  
  Below are releases from the original main Dependancies repository from [lucasg/Dependencies](https://github.com/lucasg/Dependencies)

* [v1.11](https://github.com/lucasg/Dependencies/releases/download/v1.11.1/Dependencies_x64_Release.zip) :
  
  * lots of bugfixes and incremental improvements
  * covid pandemic

* [v1.10](https://github.com/lucasg/Dependencies/releases/download/v1.10/Dependencies_x64_Release.zip) :
  
  * lots of bugfixes and incremental improvements
  * support of Windows 8.1 apisets parsing

* [v1.9](https://github.com/lucasg/Dependencies/releases/download/v1.9/Dependencies_x64_Release.zip) :
  
  * Display imports and exports the way Depends.exe does.
  * Added user customization for search folders and working directory
  * Added LLVM demangler to availables symbol demangling
  * Fixed Wow64 FsRedirection bugs
  * F5 can now refresh the analysis
  * Added CLR assembly dependencies enumeration
  * Added a packaging option without Peview.exe (which triggers some AV).

* [v1.8](https://github.com/lucasg/Dependencies/releases/download/v1.8/Dependencies_x64_Release.zip) :
  
  * Add x86/x64 variants for Dependencies

* [v1.7](https://github.com/lucasg/Dependencies/releases/download/v1.7/Dependencies.zip) :
  
  * Add CLI tool "dependencies.exe"

* [v1.6](https://github.com/lucasg/Dependencies/releases/download/v1.6/Dependencies.zip) :
  
  * Add appx packaging

* [v1.5](https://github.com/lucasg/Dependencies/releases/download/v1.5/Dependencies.zip) :
  
  * Support of Sxs parsing
  * Support of api set schema parsing
  * API and Modules list can be filtered

* [v1.0](https://github.com/lucasg/Dependencies/releases/download/v1.0/Dependencies.zip) -- Initial release

## Installation and Usage

`Dependencies` is currently shipped as two binaries (no installer present) : `Dependencies.exe` as a CLI tool and `DependenciesGui.exe` for its GUI counterpart (see screenshot). Just click on one of the release numbers above (preferably the latest), download and uncompress the archive and run `DependenciesGui.exe`.
Since the binary is not signed, `SmartScreen` might scream at runtime. `Dependencies` also bundle `ClrPhTester.exe`, a dumpbin-like executable used to test for non-regressions.

`Dependencies` currently does not recursively resolve child imports when parsing a new PE since it can be really memory-hungry to do so ( it can over a GB even for "simple" PEs ). This behavior can be overridden (app-wide) via a property located in "Options->Properties->Tree build behaviour".

<p align="center">
<img alt="User options" src="screenshots/UserOptions.png"/>
</p>

Tree build behaviours available :

* `ChildOnly` (default) : only process PE child imports and nothing beyond.
* `RecursiveOnlyOnDirectImports`  : do not process delayload dlls.
* `Recursive` : Full recursive analysis. You better have time and RAM on your hands if you activate this setting :

<p align="center">
<img alt="Yes that's 7 GB of RAM being consumed. I'm impressed the application didn't even crash" src="screenshots/RamEater.PNG"/>
</p>

## Limitations

At the moment, `Dependencies` recreates features and "features" of `depends.exe`, which means :

* Only direct, forwarded and delay load dependencies are supported. Dynamic loading via `LoadLibrary` are not supported (and probably won't ever be).
* Support of api set schema redirection since 1.5
* Checks between Api Imports and Exports. 
* Minimal support of sxs private manifests search only.

## Building

Building is pretty straightforward.
The only caveat is you need to select the "Debug" or "Release" configuration and "x64" or "x86" platform which may not be the default.

## Credits and licensing

Special thanks to :

* [ProcessHacker2](https://github.com/processhacker2/processhacker) for :
  * `phlib`, which does the heavy lifting for processing PE informations.
  * `peview`, a powerful and lightweight PE informations viewer.
* [Dragablz](https://github.com/ButchersBoy/Dragablz) a C#/XAML library which implement dockable and dragable UI elements, and can recreate the [MDI programming model](https://en.wikipedia.org/wiki/Multiple_document_interface) in `WPF`.
* @aionescu, @zodiacon and Quarkslab for their public infos on ApiSets schema.
* [Thomas levesque's blog](https://www.thomaslevesque.com) which pretty much solved all my `WPF` programming issues. His [`AutoGridSort`](http://www.thomaslevesque.com/2009/08/04/wpf-automatically-sort-a-gridview-continued/) is used in this project 
* Venkatesh Mookkan [for it's `FilterControl` for ListView used in this project](https://www.codeproject.com/Articles/170095/WPF-Custom-Control-FilterControl-for-ListBox-ListV)
* [demumble](https://github.com/nico/demumble) for demangling GCC symbols on Windows
