import glob
import os
import platform
import re
from datetime import timedelta

import dateutil.parser

# Compile regular expression for efficiency
date_pattern = re.compile(r'^<(\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}).*')

def get_default_paths():
    """Get default Star Citizen installation paths based on OS."""
    system = platform.system()
    paths = {}

    if system == 'Windows':
        # Check common installation drives
        drives = ['C', 'D', 'E', 'F']
        base_paths = []

        for drive in drives:
            base_paths.append(rf'{drive}:\Program Files\Roberts Space Industries\StarCitizen')
            base_paths.append(rf'{drive}:\Roberts Space Industries\StarCitizen')

        # Also check user's AppData
        base_paths.append(os.path.expanduser(r'~\AppData\Local\Roberts Space Industries\StarCitizen'))

        environments = ['LIVE', 'PTU', 'EPTU', 'TECH-PREVIEW']

        for base in base_paths:
            for env in environments:
                path = os.path.join(base, env, 'logbackups')
                if os.path.exists(path):
                    # Add drive letter to environment name if not on C:
                    if not base.startswith('C:'):
                        drive_letter = base[0]
                        key = f"{env} ({drive_letter}:)"
                    else:
                        key = env
                    if key not in paths:  # Don't overwrite if already found
                        paths[key] = path

    elif system == 'Linux':
        # Wine default prefix
        wine_paths = [
            os.path.expanduser('~/.wine/drive_c/Program Files/Roberts Space Industries/StarCitizen'),
            os.path.expanduser('~/.local/share/lutris/runners/wine/*/drive_c/Program Files/Roberts Space Industries/StarCitizen'),
        ]
        # Proton/Steam
        steam_paths = glob.glob(os.path.expanduser('~/.steam/steam/steamapps/compatdata/*/pfx/drive_c/Program Files/Roberts Space Industries/StarCitizen'))

        environments = ['LIVE', 'PTU', 'EPTU', 'TECH-PREVIEW']

        all_bases = wine_paths + steam_paths
        for base in all_bases:
            for env in environments:
                path = os.path.join(base, env, 'logbackups')
                if os.path.exists(path):
                    paths[env] = path

    elif system == 'Darwin':  # macOS
        # CrossOver bottles
        crossover_base = os.path.expanduser('~/Library/Application Support/CrossOver/Bottles')
        if os.path.exists(crossover_base):
            for bottle in os.listdir(crossover_base):
                base = os.path.join(crossover_base, bottle, 'drive_c/Program Files/Roberts Space Industries/StarCitizen')
                environments = ['LIVE', 'PTU', 'EPTU', 'TECH-PREVIEW']
                for env in environments:
                    path = os.path.join(base, env, 'logbackups')
                    if os.path.exists(path):
                        paths[f"{env} ({bottle})"] = path

    return paths

def get_files(search_dir):
    """Get all log files from the search directory and parent."""
    log_files = glob.glob(os.path.join(search_dir, "*.log"))
    root_dir = os.path.dirname(search_dir)
    recent_session_file = glob.glob(os.path.join(root_dir, "*.log"))
    return sorted(log_files + recent_session_file)

def extract_dates(contents):
    """Extract timestamps from log file contents."""
    return [match.group(1) for line in contents if (match := date_pattern.search(line))]

def convert_to_datetime(s):
    """Convert timestamp string to datetime object."""
    return dateutil.parser.parse(s, fuzzy=True)

def format_timedelta(td):
    """Format timedelta as human-readable string."""
    total_seconds = int(td.total_seconds())

    days = total_seconds // 86400
    hours = (total_seconds % 86400) // 3600
    minutes = (total_seconds % 3600) // 60
    seconds = total_seconds % 60

    parts = []
    if days > 0:
        parts.append(f"{days} day{'s' if days != 1 else ''}")
    if hours > 0:
        parts.append(f"{hours} hour{'s' if hours != 1 else ''}")
    if minutes > 0:
        parts.append(f"{minutes} minute{'s' if minutes != 1 else ''}")
    if seconds > 0 or not parts:
        parts.append(f"{seconds} second{'s' if seconds != 1 else ''}")

    return ", ".join(parts)

def get_totals(path):
    """Calculate total playtime from all log files in path."""
    total_deltas = timedelta()
    log_output = ""
    file_count = 0

    files = get_files(path)

    for file in files:
        filename = os.path.basename(file)
        log_output += f"Processing: {filename}\n"

        try:
            with open(file, 'r', encoding='utf-8') as f:
                text_content = f.read().split("\n")
        except UnicodeDecodeError:
            try:
                with open(file, 'r', encoding='latin-1') as f:
                    text_content = f.read().split("\n")
            except Exception as e:
                log_output += f"  Error reading file: {e}\n"
                continue

        dates = extract_dates(text_content)
        if dates:
            start_log = convert_to_datetime(dates[0])
            end_log = convert_to_datetime(dates[-1])
            delta = end_log - start_log
            total_deltas += delta
            file_count += 1
            log_output += f"  Session: {format_timedelta(delta)}\n"
        else:
            log_output += "  No timestamps found\n"

    return total_deltas, log_output, file_count

def just_do_it(path):
    """Main function to calculate playtime."""
    if not os.path.exists(path):
        return "Error: Path does not exist.", timedelta(), 0

    totals, log_output, file_count = get_totals(path)
    log_output += f"\n{'='*50}\n"
    log_output += f"Processed {file_count} log file{'s' if file_count != 1 else ''}\n"
    log_output += f"Total playtime: {format_timedelta(totals)}\n"

    return log_output, totals, file_count

if __name__ == '__main__':
    # For testing purposes
    paths = get_default_paths()
    if paths:
        first_path = list(paths.values())[0]
        log, total_time, count = just_do_it(first_path)
        print(log)
    else:
        print("No Star Citizen installation found.")
