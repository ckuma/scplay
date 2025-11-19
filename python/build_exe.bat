@echo off
echo ====================================
echo Star Citizen Playtime Calculator
echo Building Windows Executable...
echo ====================================
echo.

REM Check if PyInstaller is installed
pip show pyinstaller >nul 2>&1
if errorlevel 1 (
    echo PyInstaller not found. Installing...
    pip install pyinstaller
)

echo Building executable...
pyinstaller --onefile --windowed --name "SCPlaytime" ^
    --add-data "resources;resources" ^
    --icon "resources\clipboard.png" ^
    sc_main.py

echo.
if exist "dist\SCPlaytime.exe" (
    echo Build complete!
    echo Executable: dist\SCPlaytime.exe
) else (
    echo Build may have encountered issues. Check output above.
)
echo.
pause
