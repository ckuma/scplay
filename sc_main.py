import tkinter as tk
from tkinter import ttk, filedialog
import tkinter.font as tkfont
import os
import sc_playtime
from PIL import Image, ImageTk

# Use high DPI awareness for better rendering on Windows
try:
    from ctypes import windll
    windll.shcore.SetProcessDpiAwareness(1)
except Exception as e:
    pass  # Fails on non-Windows systems

time_delta = None
path_entry = None
log_text = None
unit_combobox = None
time_delta_entry = None
copy_button = None
copy_icon = None
mainframe = None

########################
# UI-related functions
########################
def select_directory():
    initial_dir = path_entry.get()
    directory = tk.filedialog.askdirectory(initialdir=initial_dir, mustexist=True)
    if directory:
        path_entry.delete(0, tk.END)
        path_entry.insert(0, directory)

def on_submit(path_entry):
    global time_delta, log_text
    path = path_entry.get()
    if os.path.exists(path):
        log_output, time_delta = sc_playtime.just_do_it(path)
        log_text.insert(tk.END, log_output + '\n')
        log_text.see(tk.END)  # Scroll to the bottom
        update_time_delta()
    else:
        log_text.insert(tk.END, "Error: Path does not exist.\n")
        log_text.see(tk.END)  # Scroll to the bottom

def copy_to_clipboard():
    global mainframe
    root.clipboard_clear()
    root.clipboard_append(time_delta_entry.get())
    copy_message = ttk.Label(mainframe, text="Copied to clipboard!", foreground="green")
    copy_message.grid(column=1, row=5, sticky=(tk.W, tk.E))
    root.after(3000, copy_message.destroy)  # Message disappears after 3 seconds

########################
# Output-related functions
########################
def calculate_time_delta(unit, time_delta):
    if time_delta is None:
        return 0
    if unit == 'Seconds':
        return time_delta.total_seconds()
    elif unit == 'Minutes':
        return time_delta.total_seconds() / 60
    elif unit == 'Hours':
        return time_delta.total_seconds() / 3600
    elif unit == 'Days':
        return time_delta.total_seconds() / 86400
    else:  # default
        return time_delta
    
def update_time_delta():
    unit = unit_combobox.get()
    recalculated_delta = calculate_time_delta(unit, time_delta)
    time_delta_entry.delete(0, tk.END)
    if unit == 'Default':
        time_delta_entry.insert(0, recalculated_delta)
    else:
        time_delta_entry.insert(0, f"{recalculated_delta:.2f} {unit}")

########################
# MAIN
########################
def main():
    global root, path_entry, log_text, unit_combobox, time_delta_entry, copy_button, copy_icon, mainframe, time_delta
    root = tk.Tk()
    root.title("Star Citizen Playtime Calculator")

    # Calculate the center coordinates
    screen_width = root.winfo_screenwidth()
    screen_height = root.winfo_screenheight()
    window_width = 800  # Set your window width here
    window_height = 600  # Set your window height here
    x = (screen_width // 2) - (window_width // 2)
    y = (screen_height // 2) - (window_height // 2)
    # Set minimum window size
    root.minsize(window_width, window_height)
    # Set the window geometry
    root.geometry(f"{window_width}x{window_height}+{x}+{y}")

    mainframe = ttk.Frame(root, padding="15 15 15 15")
    mainframe.grid(column=0, row=0, sticky=(tk.N, tk.W, tk.E, tk.S))
    root.columnconfigure(0, weight=1)
    root.rowconfigure(0, weight=1)

    # Configure column weights for resizing
    mainframe.columnconfigure(1, weight=1)
    mainframe.rowconfigure(2, weight=1)

    path_label = ttk.Label(mainframe, text="Path to logbackups folder:")
    path_label.grid(column=0, row=0, sticky=tk.W)

    default_path = r'C:\Program Files\Roberts Space Industries\StarCitizen\LIVE\logbackups'
    fallback_path = r'C:\path\to\starcitizen\LIVE\logbackups'

    path_entry = ttk.Entry(mainframe, width=50)
    path_entry.insert(0, default_path if os.path.exists(default_path) else fallback_path)
    path_entry.grid(column=1, row=0, sticky=(tk.W, tk.E))

    browse_button = ttk.Button(mainframe, text="Browse", command=select_directory)
    browse_button.grid(column=2, row=0, sticky=tk.W)

    submit_button = ttk.Button(mainframe, text="Submit", command=lambda: on_submit(path_entry))
    submit_button.grid(column=3, row=0, sticky=tk.W)

    log_label = ttk.Label(mainframe, text="Program Log:")
    log_label.grid(column=0, row=1, sticky=tk.W, columnspan=4)

    log_text = tk.Text(mainframe, height=15, width=60, wrap=tk.WORD)
    log_text.grid(column=0, row=2, sticky=(tk.W, tk.E, tk.N, tk.S), columnspan=4)

    scrollbar = ttk.Scrollbar(mainframe, orient=tk.VERTICAL, command=log_text.yview)
    scrollbar.grid(column=4, row=2, sticky=(tk.N, tk.S))
    log_text.config(yscrollcommand=scrollbar.set)

    totals_label = ttk.Label(mainframe, text="Totals as:")
    totals_label.grid(column=0, row=3, sticky=tk.W)

    unit_combobox = ttk.Combobox(mainframe, values=["Default", "Seconds", "Minutes", "Hours", "Days"], state='readonly')
    unit_combobox.set("Default")
    unit_combobox.grid(column=1, row=3, sticky=(tk.W, tk.E))

    result_label = ttk.Label(mainframe, text="Result:")
    result_label.grid(column=0, row=4, sticky=tk.W)

    time_delta_entry = ttk.Entry(mainframe)
    time_delta_entry.grid(column=1, row=4, sticky=(tk.W, tk.E))

    copy_button = ttk.Button(mainframe, image=None, text="Copy to clipboard", command=copy_to_clipboard)
    copy_button.grid(column=2, row=4, sticky=tk.W)

    unit_combobox.bind("<<ComboboxSelected>>", lambda event: update_time_delta())

    def load_icon():
        entry_height = time_delta_entry.winfo_height()
        if entry_height > 0:
            # Get the font of the time_delta_entry widget
            font = tkfont.Font(font=time_delta_entry['font'])
            # Calculate the width based on the height of one line of text
            icon_width = int(font.measure("A") * 2)  # Set a multiplier to adjust the width as needed
            icon = Image.open("resources\clipboard.png")
            icon = icon.resize((icon_width, icon_width), Image.LANCZOS)
            global copy_icon
            copy_icon = ImageTk.PhotoImage(icon)
            copy_button.config(image=copy_icon)

    root.after(150, load_icon)  # Load the icon after the main loop has started
    root.mainloop()

if __name__ == "__main__":
    main()
