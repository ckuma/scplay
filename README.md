# Star Citizen Playtime Calculator (SCPlay)

Calculate your total Star Citizen playtime by analyzing game log files. Since Star Citizen doesn't expose playtime statistics in-game, this tool parses your log files to compute cumulative play sessions.

## Features

- **Automatic detection** of Star Citizen installations (LIVE, PTU, EPTU, TECH-PREVIEW)
- **Cross-platform support** - Python version works on Windows, Linux (Wine), and macOS (CrossOver)
- **Multiple output formats** - Default, Hours, Minutes, Seconds, Days
- **Session breakdown** - See playtime per log file
- **Clipboard support** - Copy results with one click

## Available Versions

This repository contains two implementations:

| Version | Location | Platform | UI Framework |
|---------|----------|----------|--------------|
| **Python** | `/python` | Windows, Linux, macOS | Tkinter |
| **C#** | `/csharp` | Windows | Windows Forms |

---

## Python Version

### Requirements

- Python 3.8+
- Dependencies: `python-dateutil`, `pillow`

### Installation

```bash
cd python
pip install -r requirements.txt
```

### Running

```bash
python sc_main.py
```

Or on Linux:
```bash
./linux_start.sh
```

### Building Executable (Windows)

```bash
cd python
build_exe.bat
```

### Features

- Auto-detects all SC environments (LIVE, PTU, EPTU, TECH-PREVIEW)
- Environment selector dropdown
- Status bar with feedback
- Cross-platform path detection:
  - Windows: Standard RSI installation paths
  - Linux: Wine prefixes, Lutris, Proton/Steam
  - macOS: CrossOver bottles

---

## C# Version

### Requirements

- .NET Framework 4.8.1
- Visual Studio 2022 (Community Edition works)

### Building

1. Open `csharp/StarCitizenPlaytimeCalculator.sln` in Visual Studio
2. Build the solution (Ctrl+Shift+B)
3. Run from Debug or Release folder

### Features

- Windows Forms UI
- Default path auto-detection
- Multiple output formats (Default, Hours)
- Clipboard integration

---

## Default Installation Paths

The tool automatically searches for Star Citizen in these locations:

### Windows
```
C:\Program Files\Roberts Space Industries\StarCitizen\{ENV}\logbackups
```
Where `{ENV}` is: LIVE, PTU, EPTU, or TECH-PREVIEW

### Linux (Wine)
```
~/.wine/drive_c/Program Files/Roberts Space Industries/StarCitizen/{ENV}/logbackups
```

### macOS (CrossOver)
```
~/Library/Application Support/CrossOver/Bottles/{BOTTLE}/drive_c/Program Files/Roberts Space Industries/StarCitizen/{ENV}/logbackups
```

---

## How It Works

1. Scans all `*.log` files in the logbackups folder
2. Includes current session `Game.log` from parent directory
3. Extracts timestamps matching `<YYYY-MM-DDTHH:MM:SS...>` format
4. Calculates session duration: `last_timestamp - first_timestamp`
5. Sums all sessions to get total playtime

### Example Log Entry
```
<2024-11-19T14:30:00.123Z> [INFO] Client started...
<2024-11-19T16:45:30.456Z> [INFO] Client closing...
```
**Result:** 2 hours, 15 minutes, 30 seconds

---

## Screenshots

### Python Version
![Python Version](https://github.com/ckuma/scplay/assets/51863237/a9332bc2-0b20-46b7-893b-50551317728f)

---

## Releases

Pre-built executables are available in the [Releases](https://github.com/ckuma/scplay/releases) section.

**Note:** You can always build from source if you prefer not to run pre-built binaries.

---

## License

MIT License (but really more [WTFPL](http://en.wikipedia.org/wiki/WTFPL) in spirit)

Do whatever you want with it - it's not mine, it's everybody's. Cheers!

---

## Contributing

Contributions are welcome! Feel free to:
- Report bugs
- Suggest features
- Submit pull requests

See you in the 'verse! o7
