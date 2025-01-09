# Contributing

## Code convention

- Extension method: allowed, but must bind to "static" implementation (allows for reusing)
- Use `var` wherever possible, use `<specific type>` if it improved readabilty (example: `MongoId[] ids;` instead of `var ids = new MongoId[]();` if the array will be lazy initialized)
- Naming: private: `_lowerCase`, protected: `CamelCase`, public: `CamelCase`, type: `CamelCase`, constants: `SNAKE_CASE`, scoped variables: `lowerCase`
- Prefer reducing abbriviations in names (intellisense completes better)
- Models: no methods unless you are very sure
- Linq: use where elegant, but make it readable (one operation per line instead of chaining on the same line)
- Reflection: only use when no better solution exists, or no better elegant solution. Always include documentation.
- Closures: avoid where possible (hard to inspect MSIL)
- Labdas: fine unless it's a closure
- Try be verbose
- NEVER chain initialize variables (`var a = 10, b = 20, c = "myvalue";`)
- Follow "One instruction per line" (easier to debug in vscode)
- Enums are prefixed with `E` (example: `EPatchType`)
- Abstract classes are prefixed with `Abstract` (example: `AbstractHttpController`)
- Interfaces are prefixed with `I` (example: `IMod`)
- Avoid `struct` type (otherwise note in your PR why it uses it > structs have weird edge cases in .NET)
- When in doubt, follow .NET Code Convention (https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)

## Converting BSG data

### Type conversion table

TS type          | C# type
---------------- | -----------------------------
`number`         | `int`/`long`/`float`/`double`
`string`         | `string`
`object`         | `class`
`Record<T1, T2>` | `Dictionary<T1, T2>`

### Notes

- If the entry might be missing, use `[DataMember(EmitDefaultValue = false)]`
  and make it a nullable type (example: `HideoutInfo? HideoutInfo`).
```cs
[DataMember("prestigeLevel", EmitDefaultValue = false)]
public int PrestigeLevel? { get; set; }
```
- If you do not know the correct type, use `object` and add the comment
  `// TODO: proper type`.
```cs
// TODO: proper type
// -- seionmoya, 2024-01-09
[DataMember("exits")]
public object[] Exits { get; set; }
```
- If the member can be two possible types, use `Union<T1, T2>`. For example:
```cs
/* version 1:
{
    "TraderInfo": {
        "totalSalesSum": 10
    }
}
*/
/* Version 2:
{
    "TraderInfo": []
}
*/

[DataMember("TraderInfo")]
public Union<TraderInfo, object[]> TraderInfo { get; set; }
```