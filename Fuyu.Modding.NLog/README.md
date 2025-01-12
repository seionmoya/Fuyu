# Fuyu.Modding.NLog

Fuyu mod loader for clients using NLog.

## Installation

1. Build the project
2. Copy-paste `bin/<configuration>/net48/*.dll` except `Newtonsoft.Json.dll` inside `<game data>/Managed/`, override when prompted
3. Add the following sections (marked `<!-- FUYU -->`) to `NLog/NLog.config`:

```xml
<?xml version="1.0" encoding="utf-8"?>

<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
      xsi:schemaLocation="http://www.nlog-project.org/schemas/NLog.xsd NLog.xsd"
      autoReload="true"
      throwExceptions="true"
      internalLogLevel="Off" internalLogFile="c:\temp\nlog-internal.log">

    <!-- FUYU -->
    <extensions> 
        <add assembly="Fuyu.Modding.NLog"/> 
    </extensions> 
    <!-- FUYU -->

    <targets async="true">
        <!-- FUYU -->
        <target name="FuyuClient" xsi:type="FuyuClient" />
        <!-- FUYU -->

        <!-- ... -->
    </targets>
</nlog>
```

## FAQ

> Why not BepInEx / Monomod / Unity Mod Manager / MelonLoader?

Fuyu is designed to function with as little dependencies as possible to run,
especially not depending on external projects. With it's own loader, Fuyu also
gains more control over mod loading.

> Why does the project include a NLog version?

EFT removed `LoadNLogExtensionAssemblies` in their build, thus we have to use
the version from the reference folder (NLog 4.7.15, netstandard2.0).
