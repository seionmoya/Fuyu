# Fuyu: Modding tutorial

## Background info

### Mod targets:

The following things can be modded:

- Backend: for modding database, game server interactions
- Launcher: for adding and changing pages, adding tools
- Client: for changing game code

Depending on target, you install your mod in a different place:

- Backend: `./Fuyu/Mods/Backend/`
- Launcher: `./Fuyu/Mods/Launcher/`
- Client: `./Fuyu/Mods/Client`

### Mod loaders

There are two type of mods you can write:

- Script: for write mods without compilation
- DLL: for writing complex mods

### Complex?

Luckily, it's easier than it seems! Thanks to Fuyu unified modding framework,
it's easy to get started and to scale your mod over time.

## Hello, mod!

### Requirements

Let's write a mod together. For your first mod, it's a good idea to start with
a scripting mod as you can make them without installing developer tools.

However, it is a **MUST** that you use either `Visual Studio Code` or
`Visual Studio 2022` (or newer). For writing script mods, I recommend using
`Visual Studio Code`. For advanced mods, use `Visual Studio 2022` (or newer).

Having prior experience with C# will definetly help, but is not required.

### Setting up folder structure

For starters, create a new folder inside `./Fuyu/Mods/Server/` called
`Senko.HelloWorld`:

```
Fuyu.Backend.exe
Fuyu/
    Mods/
        Backend/
            Senko.HelloWorld/
```

`Senko.HelloWorld` will be your mod's root folder. The name of the folder is
important. The convention to follow is `<Author>.<ModName>`, for example
`JoshStrifeHayes.Replays`.

Inside of it, make a new folder: `src`:

```
Fuyu.Backend.exe
Fuyu/
    Mods/
        Backend/
            Senko.HelloWorld/
                src/
```

The `src` folder will contains all scripts for your mod. 

### Writing our first code

Inside the `src` folder, create a new file called `ModEntry.cs`:

```
Fuyu.Backend.exe
Fuyu/
    Mods/
        Backend/
            Senko.HelloWorld/
                src/
                    HelloWorldMod.cs
```

`.cs` files are scripts and contain C# code. `HelloWorldMod.cs` is the file
which will contain your mod's entry point.

An entry point is the first code that will run when the mod gets loaded.

A good convention for naming this file is `<ModName>Mod.cs`, for example
`ReplaysMod.cs`.

Open `HelloWorldMod.cs` in `Visual Studio Code` and _WRITE_ (don't copy paste!)
the following from below:

```cs
// -- required
using System.Threading.Tasks;
using Fuyu.DependencyInjection;
using Fuyu.Modding;
// -- others
using Fuyu.Common.IO;

namespace Senko.HelloWorld
{
    public class HelloWorldMod : Mod
    {
        public override string Id { get; } = "Senko.HelloWorld";
        public override string Name { get; } = "Senko - HelloWorld";
        public override string[] Dependencies { get; } = [];

        public override Task OnLoad(DependencyContainer container)
        {
            // do something here

            // done loading the mod!
            return Task.CompletedTask;
        }
    }
}
```

Whoah, that's a bit overwhelming! Alright, let's go over each thing one by one.

```cs
namespace Senko.HelloWorld
```

This is for organizing our code. If others want to work with our mod, then this
is what they would want to import.

```cs
// -- required
using System.Threading.Tasks;
using Fuyu.DependencyInjection;
using Fuyu.Modding;
// -- others
using Fuyu.Common.IO;
```

This imports things we can use in our mod. Some of them are required by `Mod`
to function.

```cs
public class HelloWorldMod : Mod
```

This is a `class` and also the heart of our mod. Here we can include
functionality to the mod. Think of `class` as a tiny program. It can depend on
other programs (inheritance).

```cs
: Mod
```

This means we inherit from `Mod`, telling the mod loader that this is the place
to start looking for information of our mod.

```cs
public override string Id { get; } = "Senko.HelloWorld";
```

Here we give our mod an internal tag; this is used by other mods to depend on
your mod. Good pratice is to make this the same as your mod's root folder.
For example, `JoshStrifeHayes.Replays`.

```cs
public override string Name { get; } = "Senko - HelloWorld";
```

This is the name that will show up in the logs. If you want something more
descriptive than your `Id` as name, you can change it. I would recommend
sticking with `Senko.HelloWorld` however.

```cs
public override string[] Dependencies { get; } = [];
```
