# LZW

A minimal C# implementation of the [LZW](https://en.wikipedia.org/wiki/Lempel%E2%80%93Ziv%E2%80%93Welch) (Lempel-Ziv-Welch) lossless compression algorithm.

## Usage

```bash
dotnet run --project lzw/lzw.csproj
```

Sample output:

```
Original: KwKwKwKwwKwKwwwKwKwwww — Héllo Wörld 🌍
Compressed: 75, 119, 256, 258, 119, 257, 261, 260, 259, 260, 119, 32, 226, 128, 148, 32, 72, 195, 169, 108, 108, 111, 32, 87, 195, 182, 114, 108, 100, 32, 240, 159, 140, 141
Decompressed: KwKwKwKwwKwKwwwKwKwwww — Héllo Wörld 🌍
Success! The data matches. ✅
```

## How it works

Both `Compress` and `Decompress` operate on **UTF-8 bytes**, so any Unicode string is handled correctly with the standard 256-entry initial alphabet.

**Compress** (`string → List<int>`)
- Initialises a dictionary with all 256 single-byte entries (codes 0–255).
- Encodes the input to UTF-8 bytes and iterates over them, greedily extending the current phrase `P` as long as `P+C` is in the dictionary.
- When `P+C` is not found: emits the code for `P`, adds `P+C` as the next dictionary entry, and resets `P = C`.

**Decompress** (`List<int> → string`)
- Initialises the reverse dictionary (code → string) with the same 256 entries.
- For each code, looks up the corresponding string. Handles the classic LZW edge case where the incoming code equals `dictionary.Count` (the entry being added right now) by reconstructing it as `prev + prev[0]`.
- Any other unknown code throws `InvalidOperationException`, making data corruption detectable.
- Collects raw bytes and decodes them from UTF-8 at the end.

## Requirements

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
