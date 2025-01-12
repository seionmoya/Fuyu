# Fuyu: Dotnet version migration guide

## Steps

1. Update `.github/workflows/*.yml` to use `dotnet-version: 'Version.0.x'`
2. Visual Studio Code > Search > Search: dotnet version, replace: New dotnet version > Replace all
3. Regenerate `.editorconfig` (delete old file > `dotnet new editorconfig` > update entries to `FUYU` section)
4. Update readme
