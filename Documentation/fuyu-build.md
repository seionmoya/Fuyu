# Fuyu: Build

## Setup

### Local project

> Terminal

```
git clone https://github.com/project-fika/fuyu
dotnet restore
```

> Visual Studio Code

1. Explorer > Clone repository > https://github.com/project-fika/fuyu
2. Terminal > Run Task... > "dotnet: restore"

### Repository CI/CD

1. Github > Generate new PAT
   1. Go [here](https://github.com/settings/tokens/new)
      - note: `FUYU_WORKFLOWS`
      - permissions: `repo` (all), `workflow` (all)
   2. Once PAT is generated, copy the token to clipboard
2. Github > Repositories > Fuyu > Settings > Secrets > Actions
3. New repository secret
   - name: `FUYU_WORKFLOWS`
   - field: paste your token

## Build

> Terminal

```
dotnet build
```

> Visual Studio Code

Terminal > Run Build Task...

## Test

> Terminal

```
dotnet build
```

> Visual Studio Code

Terminal > Run Task... > Fuyu: Test

## Use (Escape From Tarkov)

> Terminal

1. `dotnet publish`
2. Copy-paste `Fuyu.Backend/bin/Release/net8.0/win-x64/publish/Fuyu.Backend.exe` into `<gamedir>`
3. Copy-paste `Fuyu.Launcher/bin/Release/net8.0/win-x64/publish/Fuyu.Launcher.exe` into `<gamedir>`
4. Create folder `<gamedir>/Fuyu/Mods/Launcher/`
5. Copy-paste `Fuyu.Launcher.Core` into `<gamedir>/Fuyu/Mods/Launcher/`
6. Delete `bin` and `obj` inside `<gamedir>/Fuyu/Mods/Launcher/Fuyu.Launcher.Core`
7. Copy-paste `Fuyu.Modding.NLog/bin/Release/net48/publish/*.dll` into `<gamedir>/EscapeFroMTarkov_data/Managed/`
8. Create folder `<gamedir>/Fuyu/Mods/Client/`
9. Copy-paste `Fuyu.Client.EFT` into `<gamedir>/Fuyu/Mods/Client/`
10. Delete `bin` and `obj` inside `<gamedir>/Fuyu/Mods/Client/Fuyu.Client.EFT`

> Visual Studio Code

1. Terminal > Run Build Task... > dotnet: build publish
2. Copy-paste `Fuyu.Backend/bin/Release/net8.0/win-x64/publish/Fuyu.Backend.exe` into `<gamedir>`
3. Copy-paste `Fuyu.Launcher/bin/Release/net8.0/win-x64/publish/Fuyu.Launcher.exe` into `<gamedir>`
4. Create folder `<gamedir>/Fuyu/Mods/Launcher/`
5. Copy-paste `Fuyu.Launcher.Core` into `<gamedir>/Fuyu/Mods/Launcher/`
6. Delete `bin` and `obj` inside `<gamedir>/Fuyu/Mods/Launcher/Fuyu.Launcher.Core`
7. Copy-paste `Fuyu.Modding.NLog/bin/Release/net48/publish/*.dll` into `<gamedir>/EscapeFroMTarkov_data/Managed/`
8. Create folder `<gamedir>/Fuyu/Mods/Client/`
9. Copy-paste `Fuyu.Client.EFT` into `<gamedir>/Fuyu/Mods/Client/`
10. Delete `bin` and `obj` inside `<gamedir>/Fuyu/Mods/Client/Fuyu.Client.EFT`
