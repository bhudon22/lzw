# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Build the project
dotnet build lzw/lzw.csproj

# Run the program
dotnet run --project lzw/lzw.csproj

# Build in release mode
dotnet build lzw/lzw.csproj -c Release
```

## Architecture

This is a single-file C# .NET 9.0 console application implementing the LZW (Lempel-Ziv-Welch) lossless compression algorithm.

**Entry point:** `lzw/Program.cs` — contains the `LZWTool` class with three static methods:

- `Main`: hardcodes an input string, runs compress → decompress, verifies round-trip correctness.
- `Compress(string input) → List<int>`: Encodes using a `Dictionary<string, int>`. Initialized with all 256 single-byte ASCII entries. Iterates characters, extending the current phrase `P` until `P+C` is not in the dictionary, then emits the code for `P`, adds `P+C` as a new entry, and resets `P = C`.
- `Decompress(List<int> compressed) → string`: Decodes using a `Dictionary<int, string>` (reverse mapping). Handles the special LZW edge case where the incoming code equals `dictionary.Count` (not yet added) by reconstructing the entry as `dictionary[previousCode] + dictionary[previousCode][0]`.

Both dictionaries start at size 256 (standard LZW initialization). New entries are appended at `dictionary.Count` during both compression and decompression, keeping the two dictionaries in sync.

The empty `lzw.cs` at the solution root is unused.
