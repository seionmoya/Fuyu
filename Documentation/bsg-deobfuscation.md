# BSG: Deobfuscation

This guide is useful for you if you need to inspect the client's source or need
to make modifications to it.

## Requirements

- de4dot ([link](https://github.com/seionmoya/de4dot/releases))
- dnspy ([link](https://github.com/dnSpyEx/dnSpy/releases))
- `Assembly-CSharp.dll` (from live EFT / Arena)

### 1. Deobfuscating the client

Make sure in dnsy search that "Search in framework assemblies" is enabled.

1. dnspy > file > open > `Assembly-CSharp.dll`
2. dnspy > search > `CurrentDomain` > select `AppDomain.CurrentDomain { get; }`
4. scroll up until you find:

```cs
public extern object GetData(string name);
```

5. right-click on `GetData` > `Analyze` > Used by > The second one with the unicode hash

You should now see this:

```cmd
internal sealed class \uEFB1
{
	// Token: 0x06012F31 RID: 77617 RVA: 0x00588C3C File Offset: 0x00586E3C
	public static string \uE000(int \uE001\uEE35)
	{
		return (string)((Hashtable)AppDomain.CurrentDomain.GetData(\uEFB1.\uE002))[\uE001\uEE35];
	}
}
```

5. Clean the assembly

- Change `PATH` to `path/to/Assembly-CSharp.dll`
- Change `0x06012F31` to the token from `\uE000`

Make sure that `Assembly-CSharp.dll` is located in a folder with all the other
DLLs inside `Managed` along side it to prevent `ResolutionScope` error and
other deobfuscation issues. 

```cmd
de4dot.exe --un-name "!^<>[a-z0-9]$&!^<>[a-z0-9]__.*$&![A-Z][A-Z]\$<>.*$&^[a-zA-Z_<{$][a-zA-Z_0-9<>{}$.`-]*$" "PATH" --strtyp delegate --strtok 0x06012F31
pause
```
