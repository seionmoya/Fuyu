# FUYU: Version migration guide

This is a short-written guide with the steps required for updating Fuyu to a
new EFT version.

## Requirements

- vscode
- game client (version you want to migrate to)
- obtained packets (see eft-packet-dumper.md)
- batchhollower ([link](https://github.com/seionmoya/batchhollower/releases))

## Steps

The process is roughtly:

1. Generate hollowed DLLS and use them
2. Replacing the obtained packets in the database
3. Adding missing packets and paths

### EFT

#### 1. Hollowing

1. Hollow `<client>/EscapeFromTarkov_Data/Managed/` using BatchHollower
2. Place `Assembly-CSharp.dll` into `Fuyu.Client.EFT/References/`

#### 2. Updating existing database

1. In `HTTP_DATA/Responses/`, copy-paste from each response that also exists in
   `Fuyu.Backend.EFT/Resources/database/` into that folder. In case you got
   multiple dumps for the same file (exp. `client.items.json`), copy a single
   file.
2. Delete the original files from `Fuyu.Backend.EFT/Resources/database/`
   (the ones without a timestamp at the end of the file).
3. Remove the timestamp from the filenames
4. For locale files, change the last `.` to a `-`
   (exp. `client.locale.en.json` becomes `client.locale-en.json`)
5. vscode > open workspace from file... `fuyu.code-workspace`
6. vscode > terminal > run task... > dotnet: test
7. If you see errors or it fails for whatever reason, now is the time to fix
those. Usually it's model changes, check `Fuyu.Backend.BSG/Models/` or
`Fuyu.Backend.EFT/Models/`.
8. Start the server and launcher, launch the game. Reached the main menu?
   Success! if you don't, check if the issue is models. If the server reports
   a HTTP path not being handled, it's time for step 3.

#### 3. Adding missing data

TODO

### EFT: Arena

#### 1. Hollowing

1. Hollow `<client>/EscapeFromTarkov_Data/Managed/` using BatchHollower
2. Place `Assembly-CSharp.dll` into `Fuyu.Client.Arena/References/`

#### 2. Updating existing database

TODO

#### 3. Adding missing data

TODO

## FAQ

