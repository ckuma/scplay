import os
import glob
import dateutil.parser
import re
from datetime import timedelta
import fire

# Get filelist sorted by date
def get_files(search_dir):
    log_files = glob.glob(os.path.join(search_dir, "*.log"))
    root_dir = os.path.dirname(search_dir)
    recent_session_file = glob.glob(os.path.join(root_dir, "*.log"))
    return log_files + recent_session_file

# Compile regular expression for efficiency
date_pattern = re.compile(r'^<(\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}).*')

# Filter for <2022-06-12T19:31:06.090Z> type dates
def extract_dates(contents):
    return [match.group(1) for line in contents if (match := date_pattern.search(line))]

# Converting matches to datetime objects
def convert_to_datetime(s):
    return dateutil.parser.parse(s, fuzzy=True)

def get_totals(path=r'C:\Program Files\Roberts Space Industries\StarCitizen\LIVE\logbackups'):
    total_deltas = timedelta()
    # For each file, extract first and last timestamp and calculate the delta
    for file in get_files(path):
        print("Opening logfile: " + file)
        with open(file, 'r', encoding='utf-8') as f:
            text_content = f.read().split("\n")
        dates = extract_dates(text_content)
        if dates:
            start_log = convert_to_datetime(dates[0])
            end_log = convert_to_datetime(dates[-1])
            delta = end_log - start_log
            total_deltas += delta
            print("\tCalculated delta of ",delta)
    return total_deltas

def just_do_it(path=r'C:\Program Files\Roberts Space Industries\StarCitizen\LIVE\logbackups'):
    totals = get_totals(path)
    print("\nYou've played a total of: ", totals)

if __name__ == '__main__':
    fire.Fire(just_do_it)
