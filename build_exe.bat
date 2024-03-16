@echo off
echo Building the executable...
pyinstaller --onefile --windowed sc_main.py
echo Build complete. Check the dist folder for the executable.
pause