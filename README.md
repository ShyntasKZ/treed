# 📄 **README.md**

# **treed — A clean and developer-friendly directory tree generator**

**treed** is a lightweight cross-platform CLI tool that generates clean directory tree views for your projects.
It removes build artifacts, IDE folders, and other noise by default — showing only what developers actually care about: the structure of their source code.

It can output either a standard console tree or a Markdown-formatted version suitable for documentation.

---

## ✨ Features

* **Clean directory trees**
  Automatically ignores common build folders and temporary files:

  * Directories: `bin`, `obj`, `.git`, `.vs`, `.idea`, `.vscode`, `node_modules`, `packages`
  * Files: `.dll`, `.pdb`, `.cache`, `.tmp`, `.log`, `.exe`

* **Markdown export**
  Generate documentation-ready `.md` files using:

  ```bash
  treed . -md structure
  ```

* **Relative path support**
  Just like in any shell:

  ```
  treed .
  treed ..
  treed src/Services
  ```

* **Cross-platform**
  Ships as a self-contained single-file binary for Windows, Linux, and macOS.
  No runtime installation required.

* **Zero dependencies**
  The resulting binary contains everything it needs.

---

## 📦 Installation

Download the latest release from GitHub:

```
treed-win-x64.exe
treed-linux-x64
treed-osx-arm64
...
```

Make it available globally by placing it in a folder included in your PATH
(e.g., `C:\tools\` on Windows or `/usr/local/bin` on Linux/macOS).

---

## 🚀 Usage

### Show tree of the current directory

```bash
treed .
```

### Show tree of a relative path

```bash
treed src
treed ../backend
```

### Export tree as Markdown

```bash
treed . -md structure
# generates structure.md
```

---

## 📘 Example output

### Console mode

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

### Markdown mode

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

## 🧹 What treed ignores automatically

Treed hides files and directories that are normally not part of source control:

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

This keeps the output focused on **source code** and nothing else.

---

## 🛠 How it works (summary)

treed recursively walks the target directory, filters out noise, and prints a clean tree with:

* `├──` / `└──` connectors (console mode)
* nested Markdown lists (markdown mode)

The output is deterministic, alphabetically sorted, and consistent across all platforms.

---

## 📄 License

MIT License.

---

Если хочешь, могу:

✔ добавить раздел *Building from source*
✔ написать *Contributing Guidelines*
✔ подготовить логотип и badges
✔ собрать полный релиз-чейнджлог под версию 1.0.
