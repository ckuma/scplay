#!/usr/bin/env bash

#===============================================================================
#
#   Star Citizen Playtime Calculator
#   Linux/macOS Launcher Script
#
#   This script sets up a virtual environment and runs the application.
#
#===============================================================================

set -e

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Get script directory
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

#-------------------------------------------------------------------------------
# Helper functions
#-------------------------------------------------------------------------------

print_header() {
    echo -e "${BLUE}"
    echo "╔════════════════════════════════════════════════════════════╗"
    echo "║       Star Citizen Playtime Calculator                     ║"
    echo "╚════════════════════════════════════════════════════════════╝"
    echo -e "${NC}"
}

print_status() {
    echo -e "${GREEN}[✓]${NC} $1"
}

print_warning() {
    echo -e "${YELLOW}[!]${NC} $1"
}

print_error() {
    echo -e "${RED}[✗]${NC} $1"
}

check_python() {
    if command -v python3 &> /dev/null; then
        PYTHON_CMD="python3"
    elif command -v python &> /dev/null; then
        PYTHON_CMD="python"
    else
        print_error "Python is not installed"
        echo "    Please install Python 3.8 or later:"
        echo "    - Ubuntu/Debian: sudo apt install python3 python3-venv python3-pip"
        echo "    - Fedora: sudo dnf install python3"
        echo "    - macOS: brew install python3"
        exit 1
    fi

    # Check Python version
    PYTHON_VERSION=$($PYTHON_CMD -c 'import sys; print(f"{sys.version_info.major}.{sys.version_info.minor}")')
    PYTHON_MAJOR=$($PYTHON_CMD -c 'import sys; print(sys.version_info.major)')
    PYTHON_MINOR=$($PYTHON_CMD -c 'import sys; print(sys.version_info.minor)')

    if [ "$PYTHON_MAJOR" -lt 3 ] || ([ "$PYTHON_MAJOR" -eq 3 ] && [ "$PYTHON_MINOR" -lt 8 ]); then
        print_error "Python 3.8+ required (found $PYTHON_VERSION)"
        exit 1
    fi

    print_status "Python $PYTHON_VERSION detected"
}

check_tkinter() {
    if ! $PYTHON_CMD -c "import tkinter" &> /dev/null; then
        print_error "Tkinter is not installed"
        echo "    Please install Tkinter:"
        echo "    - Ubuntu/Debian: sudo apt install python3-tk"
        echo "    - Fedora: sudo dnf install python3-tkinter"
        echo "    - macOS: brew install python-tk"
        exit 1
    fi
}

setup_venv() {
    if [ ! -d "$SCRIPT_DIR/venv" ]; then
        print_status "Creating virtual environment..."
        $PYTHON_CMD -m venv "$SCRIPT_DIR/venv"
    fi

    # Activate virtual environment
    source "$SCRIPT_DIR/venv/bin/activate"
    print_status "Virtual environment activated"
}

install_dependencies() {
    print_status "Checking dependencies..."
    pip install -q --upgrade pip
    pip install -q -r "$SCRIPT_DIR/requirements.txt"
    print_status "Dependencies installed"
}

run_application() {
    echo ""
    print_status "Starting application..."
    echo ""
    $PYTHON_CMD "$SCRIPT_DIR/sc_main.py"
}

cleanup() {
    if [ -n "$VIRTUAL_ENV" ]; then
        deactivate 2>/dev/null || true
    fi
}

#-------------------------------------------------------------------------------
# Main
#-------------------------------------------------------------------------------

main() {
    trap cleanup EXIT

    print_header

    cd "$SCRIPT_DIR"

    check_python
    check_tkinter
    setup_venv
    install_dependencies
    run_application
}

main "$@"
