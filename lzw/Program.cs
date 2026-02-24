using System;
using System.Collections.Generic;
using System.Text;

class LZWTool
{
    static void Main()
    {
        string input = "KwKwKwKwwKwKwwwKwKwwww — Héllo Wörld 🌍";
        Console.WriteLine($"Original: {input}");

        // 1. Run Compression
        List<int> compressed = Compress(input);
        Console.WriteLine("Compressed: " + string.Join(", ", compressed));

        // 2. Run Decompression
        string decompressed = Decompress(compressed);
        Console.WriteLine($"Decompressed: {decompressed}");

        // 3. Verify
        if (input == decompressed)
        {
            Console.WriteLine("Success! The data matches. ✅");
        }
    }

    static List<int> Compress(string input)
    {
        Dictionary<string, int> dictionary = new Dictionary<string, int>();
        for (int i = 0; i < 256; i++)
        {
            dictionary.Add(((char)i).ToString(), i);
        }

        // Encode to UTF-8 bytes so any Unicode input fits the 256-entry alphabet.
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);

        string P = "";
        List<int> compressedOutput = new List<int>();

        foreach (byte b in inputBytes)
        {
            string PC = P + (char)b;
            if (dictionary.ContainsKey(PC))
            {
                P = PC;
            }
            else
            {
                compressedOutput.Add(dictionary[P]);
                dictionary.Add(PC, dictionary.Count);
                P = ((char)b).ToString();
            }
        }

        if (!string.IsNullOrEmpty(P))
        {
            compressedOutput.Add(dictionary[P]);
        }

        return compressedOutput;
    }

    static string Decompress(List<int> compressed)
    {
        Dictionary<int, string> dictionary = new Dictionary<int, string>();
        for (int i = 0; i < 256; i++)
        {
            dictionary.Add(i, ((char)i).ToString());
        }

        // Collect raw bytes; decode from UTF-8 at the end to mirror Compress.
        List<byte> outputBytes = new List<byte>();
        int previousCode = -1;

        foreach (int code in compressed)
        {
            string entry;
            if (dictionary.ContainsKey(code))
            {
                entry = dictionary[code];
            }
            else if (code == dictionary.Count)
            {
                // Classic LZW edge case: the code being decoded is the one about to be
                // added (e.g. cScSc pattern). Reconstruct as prev + prev[0].
                entry = dictionary[previousCode] + dictionary[previousCode][0];
            }
            else
            {
                throw new InvalidOperationException(
                    $"Invalid code {code}: not in dictionary (size {dictionary.Count}) and not the LZW edge case.");
            }

            foreach (char c in entry)
                outputBytes.Add((byte)c);

            if (previousCode != -1)
            {
                dictionary.Add(dictionary.Count, dictionary[previousCode] + entry[0]);
            }

            previousCode = code;
        }

        return Encoding.UTF8.GetString(outputBytes.ToArray());
    }
}


