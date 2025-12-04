using System;
using System.IO;
using System.Linq;

class Program
{
    // --- Directories to ignore ---
    private static readonly string[] IgnoredDirectories =
    {
        "bin", "obj", ".git", ".vs", ".idea", ".vscode", "node_modules", "packages"
    };

    // --- File extensions to ignore ---
    private static readonly string[] IgnoredExtensions =
    {
        ".dll", ".pdb", ".cache", ".tmp", ".log", ".exe"
    };

    static void Main(string[] args)
    {
        bool markdown = false;
        string? outFile = null;
        string? targetArg = null;

        // --- Parse CLI arguments ---
        for (int i = 0; i < args.Length; i++)
        {
            var arg = args[i];

            if (arg.Equals("-md", StringComparison.OrdinalIgnoreCase))
            {
                markdown = true;

                if (i + 1 >= args.Length)
                {
                    Console.WriteLine("Error: -md requires an output filename");
                    Console.WriteLine("Usage: treed <path> -md outputname");
                    return;
                }

                outFile = args[i + 1];
                i++;
            }
            else
            {
                targetArg = arg;
            }
        }

        string targetDir = ResolvePath(targetArg);

        if (!Directory.Exists(targetDir))
        {
            Console.WriteLine($"Directory not found: {targetDir}");
            return;
        }

        if (!markdown)
        {
            Console.WriteLine(Path.GetFileName(targetDir));
            PrintTree(targetDir);
        }
        else
        {
            string fileName = outFile!.EndsWith(".md") ? outFile : outFile + ".md";

            using var writer = new StreamWriter(fileName, false);
            PrintMarkdown(writer, targetDir);

            Console.WriteLine($"Markdown saved to: {fileName}");
        }
    }

    static string ResolvePath(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            return Directory.GetCurrentDirectory();

        if (raw == ".")
            return Directory.GetCurrentDirectory();

        if (raw == "..")
            return Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;

        return Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), raw));
    }

    // ---------- IGNORE LOGIC ----------
    static bool ShouldIgnore(string path)
    {
        string name = Path.GetFileName(path);

        // ignore directories from list
        if (Directory.Exists(path) && IgnoredDirectories.Contains(name, StringComparer.OrdinalIgnoreCase))
            return true;

        // ignore files by extension
        if (File.Exists(path))
        {
            string ext = Path.GetExtension(path);
            if (IgnoredExtensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    // ---------- TREE OUTPUT ----------
    static void PrintTree(string root, string prefix = "")
    {
        var items = Directory.GetFileSystemEntries(root)
            .Where(e => !ShouldIgnore(e)) // ignore here
            .OrderBy(e => Directory.Exists(e) ? 0 : 1)
            .ThenBy(Path.GetFileName)
            .ToArray();

        for (int i = 0; i < items.Length; i++)
        {
            var e = items[i];
            bool isLast = i == items.Length - 1;

            string connector = isLast ? "└── " : "├── ";
            Console.WriteLine(prefix + connector + Path.GetFileName(e));

            if (Directory.Exists(e))
            {
                string newPrefix = prefix + (isLast ? "    " : "│   ");
                PrintTree(e, newPrefix);
            }
        }
    }

    // ---------- MARKDOWN OUTPUT ----------
    static void PrintMarkdown(StreamWriter writer, string root, string indent = "")
    {
        writer.WriteLine($"{indent}- **{Path.GetFileName(root)}/**");

        var items = Directory.GetFileSystemEntries(root)
            .Where(e => !ShouldIgnore(e))
            .OrderBy(e => Directory.Exists(e) ? 0 : 1)
            .ThenBy(Path.GetFileName)
            .ToArray();

        foreach (var e in items)
        {
            string name = Path.GetFileName(e);

            if (Directory.Exists(e))
            {
                PrintMarkdown(writer, e, indent + "  ");
            }
            else
            {
                writer.WriteLine($"{indent}  - {name}");
            }
        }
    }
}
