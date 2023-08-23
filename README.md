# NBTParser

Parses NBT (minecraft .dat files) into C# objects.
Can also convert to JSON format.

BTW don't use this, it's rushily made and not checked for any errors, but it can parse minecraft's level.dat

## Parsing

Create a new instance of Parser

```cs
Parser parser = new Parser(fileName);
```

fileName in which is a path to a file.

Then use the `Parse` method:

```cs
BaseTag tag = parser.Parse();
```

You will then be able to traverse the tag by checking the `Type` of the tag.

## Converting to JSON

Use the `parse.ToJSON();` method

```cs
string json = parser.ToJSON(baseTag);
```
