import os
import glob
import dateutil.parser
import re
from datetime import timedelta
# GUI-related imports
import fire

# Get filelist sorted by date
def get_files(search_dir):
    # Grabbing all log files in logbackup folder
    os.chdir(search_dir)
    files = glob.glob("*.log")
    files = [os.path.join(search_dir, f) for f in files] # add path to each file
    # Grabbing the extra game log file (most recent session, not archived yet)
    os.chdir("..")
    rootfile = glob.glob("*.log")
    recent_session_file = [os.path.join(os.path.abspath(os.curdir), f) for f in rootfile]
    files += recent_session_file
    return files

# filter for <2022-06-12T19:31:06.090Z> type dates
# regexp: \d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}
def extract_dates(contents):
    dates = []
    for line in contents:
        match= re.search(r'^<(\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}).*',line)
        if match:
            dates.append(match.group(1))
    return dates

# Converting matches to datetime objects
def convert_to_datetime(s):
    return dateutil.parser.parse(s, fuzzy=True)

def get_totals(path='C:\Program Files\Roberts Space Industries\StarCitizen\LIVE\logbackups'):
    total_deltas = timedelta()
    # For each file, extract first and last timestamp and calculate the delta
    for file in get_files(path):
        fhandle = open(file,'r')
        text_content = fhandle.read().split("\n")
        dates = extract_dates(text_content)
        if len(dates) > 0:
            start_log = convert_to_datetime(dates[0])
            end_log = convert_to_datetime(dates[-1])
            delta = end_log-start_log
            #print("Start: ",start_log," - End:",end_log," - Diff: ",delta.seconds)
            total_deltas += delta
    return total_deltas

def just_do_it(path='C:\Program Files\Roberts Space Industries\StarCitizen\LIVE\logbackups'):
    totals = get_totals(path)
    print("\nCongrats, you've played "+str(totals))

if __name__ == '__main__':
    fire.Fire(just_do_it)