@echo off
setlocal enabledelayedexpansion

::=============================================================================
::
::  Star Citizen Playtime Calculator
::  Windows Executable Builder
::
::  This script builds a standalone Windows executable using PyInstaller.
::
::=============================================================================

echo.
echo ================================================================
echo    Star Citizen Playtime Calculator - Build Script
echo ================================================================
echo.

:: Check Python installation
python --version >nul 2>&1
if errorlevel 1 (
    echo [ERROR] Python is not installed or not in PATH
    echo         Please install Python 3.8+ from https://python.org
    goto :error
)

:: Get Python version
for /f "tokens=2" %%i in ('python --version 2^>^&1') do set PYTHON_VERSION=%%i
echo [INFO] Python %PYTHON_VERSION% detected

:: Check if we're in the right directory
if not exist "sc_main.py" (
    echo [ERROR] sc_main.py not found
    echo         Please run this script from the python directory
    goto :error
)

:: Install/upgrade pip
echo.
echo [INFO] Upgrading pip...
python -m pip install --upgrade pip --quiet

:: Install dependencies
echo [INFO] Installing dependencies...
pip install -r requirements.txt --quiet
if errorlevel 1 (
    echo [ERROR] Failed to install dependencies
    goto :error
)

:: Install PyInstaller if needed
echo [INFO] Checking PyInstaller...
pip install pyinstaller==6.21.0 --quiet
if errorlevel 1 (
    echo [ERROR] Failed to install PyInstaller
    goto :error
)

:: Clean previous builds
echo [INFO] Cleaning previous builds...
if exist "dist" rmdir /s /q "dist"
if exist "build" rmdir /s /q "build"
if exist "*.spec" del /q "*.spec"

:: Build executable
echo.
echo [INFO] Building executable...
echo.

pyinstaller ^
    --onefile ^
    --windowed ^
    --name "SCPlaytime" ^
    --add-data "resources;resources" ^
    --hidden-import "PIL._tkinter_finder" ^
    --clean ^
    --noconfirm ^
    sc_main.py

if errorlevel 1 (
    echo.
    echo [ERROR] Build failed
    goto :error
)

:: Check if build succeeded
if exist "dist\SCPlaytime.exe" (
    echo.
    echo ================================================================
    echo    BUILD SUCCESSFUL
    echo ================================================================
    echo.
    echo    Output: dist\SCPlaytime.exe
    echo.

    :: Show file size
    for %%A in ("dist\SCPlaytime.exe") do (
        set size=%%~zA
        set /a sizeMB=!size!/1048576
        echo    Size: !sizeMB! MB
    )
    echo.
) else (
    echo.
    echo [ERROR] Build completed but executable not found
    goto :error
)

goto :end

:error
echo.
echo Build failed. Please check the errors above.
echo.
pause
exit /b 1

:end
pause
exit /b 0
