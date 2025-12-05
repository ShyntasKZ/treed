# 📄 **README.md**
---

# treed — a clean and developer-friendly directory tree generator

`treed` is a small, fast, cross-platform command-line tool that prints clean directory trees for your projects.
It automatically ignores development artifacts such as build folders, IDE configuration directories, caches, and temporary files.
This makes it useful for documentation, code reviews, onboarding, and understanding unfamiliar repositories.

You can output the tree to the console, or export it as a Markdown file for README files, wikis, or technical documentation.

---

## Why treed?

Most projects contain a lot of noise:

* `bin/`, `obj/` build output
* IDE folders: `.vs/`, `.idea/`, `.vscode/`
* dependency folders like `node_modules/`
* auto-generated files

When exploring a repository, these items distract from the actual code structure.

`treed` hides all of that by default, giving you a clean and readable view of the project layout.

---

## Features

* Clean directory trees (build artifacts and IDE folders automatically excluded)
* Markdown export:

  ```
  treed . -md structure
  ```
* Relative path support: `.`, `..`, `folder/subfolder`
* Single-file, self-contained binaries for Windows, Linux, and macOS
* No runtime required (does not require .NET to be installed)
* Simple syntax, fast execution, small footprint

---

## Installation

Download a release from the GitHub Releases page and place the binary somewhere in your PATH.

### Windows

Move the file:

```
treed.exe → C:\tools\treed\treed.exe
```

Add to PATH if needed:

```
setx PATH "%PATH%;C:\tools\treed"
```

### Linux / macOS

```
sudo mv treed /usr/local/bin/treed
sudo chmod +x /usr/local/bin/treed
```

---

## Usage

Show a tree of the current directory:

```
treed .
```

Show a tree of another folder:

```
treed src
treed ../backend
```

Export as Markdown (`structure.md` will be created):

```
treed . -md structure
```

---

## Example Output

### Console tree

```
src
├── Api
│   ├── Controllers
│   └── Contracts
├── Application
│   └── Services
└── Domain
    ├── Models
    └── Events
```

### Markdown output

```
- **src/**
  - **Api/**
    - Controllers
    - Contracts
  - **Application/**
    - Services
  - **Domain/**
    - Models
    - Events
```

---

## Ignored files and directories

`treed` automatically excludes items that are not part of the actual source code.

### Ignored directories

```
bin/
obj/
.git/
.vs/
.idea/
.vscode/
node_modules/
packages/
```

### Ignored file extensions

```
.dll
.pdb
.cache
.tmp
.log
.exe
```

This makes the output focused, readable, and suitable for documentation.

---

## Building from source

Clone the repository:

```
git clone https://github.com/<your-account>/treed.git
cd treed
```

Build normally:

```
dotnet build -c Release
```

Run locally:

```
dotnet run -- .
```

---

## Publishing self-contained binaries (Windows, Linux, macOS)

`treed` can be published as single-file executables that run without installing .NET.

Run the following commands inside the project:

### Windows (x64)

```
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:PublishTrimmed=true
```

Output:

```
bin/Release/net10.0/win-x64/publish/treed.exe
```

---

### Linux (x64)

```
dotnet publish -c Release -r linux-x64 --self-contained true /p:PublishSingleFile=true /p:PublishTrimmed=true
```

Output:

```
bin/Release/net10.0/linux-x64/publish/treed
```

---

### macOS (Intel)

```
dotnet publish -c Release -r osx-x64 --self-contained true /p:PublishSingleFile=true /p:PublishTrimmed=true
```

### macOS (Apple Silicon / ARM)

```
dotnet publish -c Release -r osx-arm64 --self-contained true /p:PublishSingleFile=true /p:PublishTrimmed=true
```

---

## Notes

* The `PublishTrimmed=true` setting reduces binary size by removing unused .NET components.
* All published outputs are standalone and do not require .NET runtime installation.

---

## License

MIT License.

---
