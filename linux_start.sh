#!/usr/bin/env bash
set -euo pipefail

# Use Python 3.11
PYTHON="python3.11"

# Check if Python 3.11 exists
if ! command -v "$PYTHON" &> /dev/null; then
    echo "ERROR: $PYTHON not found on this system." >&2
    echo "Install it using your package manager, e.g.:" >&2
    echo "  sudo apt install python3.11 python3.11-venv" >&2
    exit 1
fi

# Verify venv availability
if ! "$PYTHON" -m venv --help &> /dev/null; then
    echo "ERROR: venv module not available for Python 3.11" >&2
    echo "Install it with: sudo apt install python3.11-venv" >&2
    exit 1
fi

# Create or reuse virtual environment
if [ ! -d "myenv" ]; then
    echo "Creating virtual environment with $PYTHON..."
    "$PYTHON" -m venv myenv
else
    echo "Reusing existing virtual environment."
fi

# Activate environment
# shellcheck source=/dev/null
. myenv/bin/activate

echo "Using Python: $(python --version)"

# Upgrade pip for safety
python -m pip install --upgrade pip

# Install dependencies if requirements.txt exists
if [ -f requirements.txt ]; then
    echo "Installing dependencies..."
    pip install -r requirements.txt
else
    echo "No requirements.txt found, skipping dependency installation."
fi

# Run main script
echo "Running sc_main.py..."
python sc_main.py
