<p align="center">
  <h1 align="center">Star Citizen Playtime Calculator</h1>
  <p align="center">
    Calculate your total Star Citizen playtime by analyzing game log files
    <br />
    <a href="https://github.com/ckuma/scplay/releases"><strong>Download Latest Release</strong></a>
    <br />
    <br />
    <a href="https://github.com/ckuma/scplay/issues">Report Bug</a>
    &middot;
    <a href="https://github.com/ckuma/scplay/issues">Request Feature</a>
  </p>
</p>

<p align="center">
  <a href="https://github.com/ckuma/scplay/actions/workflows/build.yml">
    <img src="https://github.com/ckuma/scplay/actions/workflows/build.yml/badge.svg" alt="Build Status">
  </a>
  <a href="https://github.com/ckuma/scplay/releases">
    <img src="https://img.shields.io/github/v/release/ckuma/scplay" alt="Latest Release">
  </a>
  <a href="LICENSE.txt">
    <img src="https://img.shields.io/badge/license-MIT-blue.svg" alt="License">
  </a>
</p>

---

## About

Since Star Citizen doesn't expose playtime statistics in-game, SCPlay parses your game log files to compute cumulative play sessions. It automatically detects your Star Citizen installation and calculates total time played across all sessions.

### Key Features

- **Auto-detection** - Finds SC installations on multiple drives (C:, D:, E:, F:)
- **Multi-environment** - Supports LIVE, PTU, EPTU, and TECH-PREVIEW
- **Cross-platform** - Python version runs on Windows, Linux, and macOS
- **Multiple formats** - View playtime as days, hours, minutes, or seconds
- **Session details** - See individual session durations

---

## Quick Start

### Download Pre-built Executable

1. Go to [Releases](https://github.com/ckuma/scplay/releases)
2. Download the latest `.zip` for your preferred version
3. Extract and run

### Available Versions

| Version | Platform | Framework | Best For |
|---------|----------|-----------|----------|
| **Python** | Windows, Linux, macOS | Tkinter | Cross-platform users |
| **C#** | Windows | WinForms | Windows-only users |

---

## Installation

### Python Version

**Requirements:** Python 3.8+

```bash
# Clone the repository
git clone https://github.com/ckuma/scplay.git
cd scplay/python

# Install dependencies
pip install -r requirements.txt

# Run the application
python sc_main.py
```

**Linux/macOS:**
```bash
chmod +x linux_start.sh
./linux_start.sh
```

### C# Version

**Requirements:** .NET Framework 4.8.1, Visual Studio 2022

1. Open `csharp/StarCitizenPlaytimeCalculator.sln`
2. Build the solution (`Ctrl+Shift+B`)
3. Run from `bin/Release/`

---

## Usage

1. **Launch** the application
2. **Select** your Star Citizen environment from the dropdown (auto-detected)
3. **Click** "Calculate" to process log files
4. **View** your total playtime and per-session breakdown
5. **Copy** the result to clipboard if needed

---

## Supported Installation Paths

SCPlay automatically searches for Star Citizen in these locations:

### Windows

```
{DRIVE}:\Program Files\Roberts Space Industries\StarCitizen\{ENV}\logbackups
{DRIVE}:\Roberts Space Industries\StarCitizen\{ENV}\logbackups
```

- **Drives:** C:, D:, E:, F:
- **Environments:** LIVE, PTU, EPTU, TECH-PREVIEW

### Linux

```bash
# Wine
~/.wine/drive_c/Program Files/Roberts Space Industries/StarCitizen/{ENV}/logbackups

# Lutris
~/.local/share/lutris/runners/wine/*/drive_c/Program Files/Roberts Space Industries/StarCitizen/{ENV}/logbackups

# Steam/Proton
~/.steam/steam/steamapps/compatdata/*/pfx/drive_c/Program Files/Roberts Space Industries/StarCitizen/{ENV}/logbackups
```

### macOS

```bash
# CrossOver
~/Library/Application Support/CrossOver/Bottles/{BOTTLE}/drive_c/Program Files/Roberts Space Industries/StarCitizen/{ENV}/logbackups
```

---

## How It Works

SCPlay analyzes Star Citizen log files to calculate playtime:

1. Scans all `*.log` files in the `logbackups` folder
2. Includes the current session's `Game.log`
3. Extracts timestamps using pattern: `<YYYY-MM-DDTHH:MM:SS...>`
4. Calculates each session: `last_timestamp - first_timestamp`
5. Sums all sessions for total playtime

### Example

```
<2024-11-19T14:30:00.123Z> [INFO] Client started...
...
<2024-11-19T16:45:30.456Z> [INFO] Client closing...
```

**Session duration:** 2 hours, 15 minutes, 30 seconds

---

## Building from Source

### Python Executable (Windows)

```bash
cd python
pip install pyinstaller
pyinstaller --onefile --windowed --name "SCPlaytime" --add-data "resources;resources" sc_main.py
```

Output: `dist/SCPlaytime.exe`

### C# Release Build

```bash
cd csharp
msbuild StarCitizenPlaytimeCalculator.sln /p:Configuration=Release
```

Output: `bin/Release/StarCitizenPlaytimeCalculator.exe`

---

## CI/CD

This repository uses GitHub Actions to automatically build releases:

- **On tag push** (`v*`) - Creates a GitHub Release with both executables
- **On PR** - Validates builds

To create a new release:

```bash
git tag v4.0
git push origin v4.0
```

---

## Screenshots

<p align="center">
  <img src="https://github.com/ckuma/scplay/assets/51863237/a9332bc2-0b20-46b7-893b-50551317728f" alt="SCPlay Screenshot" width="600">
</p>

---

## Contributing

Contributions are welcome! Please feel free to submit issues and pull requests.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

---

## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

---

## Acknowledgments

- Star Citizen community
- All contributors

---

<p align="center">
  See you in the 'verse! o7
</p>
