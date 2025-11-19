import tkinter as tk
from tkinter import ttk, filedialog, messagebox
import tkinter.font as tkfont
import os
import sys
import sc_playtime
from PIL import Image, ImageTk

# Use high DPI awareness for better rendering on Windows
try:
    from ctypes import windll
    windll.shcore.SetProcessDpiAwareness(1)
except Exception:
    pass  # Fails on non-Windows systems


class SCPlaytimeCalculator:
    def __init__(self):
        self.root = tk.Tk()
        self.root.title("Star Citizen Playtime Calculator")
        self.time_delta = None
        self.copy_icon = None
        self.detected_paths = {}

        self._setup_window()
        self._create_widgets()
        self._detect_installations()

    def _setup_window(self):
        """Configure the main window."""
        # Calculate center position
        screen_width = self.root.winfo_screenwidth()
        screen_height = self.root.winfo_screenheight()
        window_width = 850
        window_height = 650
        x = (screen_width // 2) - (window_width // 2)
        y = (screen_height // 2) - (window_height // 2)

        self.root.geometry(f"{window_width}x{window_height}+{x}+{y}")
        self.root.minsize(700, 500)

        # Configure style
        style = ttk.Style()
        style.configure('Header.TLabel', font=('Helvetica', 12, 'bold'))
        style.configure('Status.TLabel', font=('Helvetica', 9))
        style.configure('Result.TEntry', font=('Helvetica', 10, 'bold'))

    def _create_widgets(self):
        """Create all UI widgets."""
        # Main container
        self.mainframe = ttk.Frame(self.root, padding="15")
        self.mainframe.grid(column=0, row=0, sticky=(tk.N, tk.W, tk.E, tk.S))
        self.root.columnconfigure(0, weight=1)
        self.root.rowconfigure(0, weight=1)

        # Configure grid weights
        self.mainframe.columnconfigure(1, weight=1)
        self.mainframe.rowconfigure(4, weight=1)

        # Row 0: Environment selector
        env_label = ttk.Label(self.mainframe, text="Environment:", style='Header.TLabel')
        env_label.grid(column=0, row=0, sticky=tk.W, pady=(0, 5))

        self.env_combobox = ttk.Combobox(self.mainframe, state='readonly', width=20)
        self.env_combobox.grid(column=1, row=0, sticky=tk.W, pady=(0, 5))
        self.env_combobox.bind("<<ComboboxSelected>>", self._on_environment_change)

        detect_btn = ttk.Button(self.mainframe, text="Refresh", command=self._detect_installations)
        detect_btn.grid(column=2, row=0, sticky=tk.W, padx=(5, 0), pady=(0, 5))

        # Row 1: Path selection
        path_label = ttk.Label(self.mainframe, text="Log folder path:")
        path_label.grid(column=0, row=1, sticky=tk.W, pady=(5, 5))

        self.path_entry = ttk.Entry(self.mainframe)
        self.path_entry.grid(column=1, row=1, sticky=(tk.W, tk.E), pady=(5, 5))

        path_btn_frame = ttk.Frame(self.mainframe)
        path_btn_frame.grid(column=2, row=1, columnspan=2, sticky=tk.W, pady=(5, 5))

        browse_btn = ttk.Button(path_btn_frame, text="Browse", command=self._select_directory)
        browse_btn.pack(side=tk.LEFT, padx=(5, 2))

        calculate_btn = ttk.Button(path_btn_frame, text="Calculate", command=self._calculate_playtime)
        calculate_btn.pack(side=tk.LEFT, padx=2)

        # Row 2: Separator
        separator = ttk.Separator(self.mainframe, orient='horizontal')
        separator.grid(column=0, row=2, columnspan=4, sticky=(tk.W, tk.E), pady=10)

        # Row 3: Log label
        log_label = ttk.Label(self.mainframe, text="Processing Log:")
        log_label.grid(column=0, row=3, sticky=tk.W, columnspan=4)

        # Row 4: Log text area with scrollbar
        log_frame = ttk.Frame(self.mainframe)
        log_frame.grid(column=0, row=4, columnspan=4, sticky=(tk.W, tk.E, tk.N, tk.S), pady=(5, 10))
        log_frame.columnconfigure(0, weight=1)
        log_frame.rowconfigure(0, weight=1)

        self.log_text = tk.Text(log_frame, height=15, wrap=tk.WORD, font=('Consolas', 9))
        self.log_text.grid(column=0, row=0, sticky=(tk.W, tk.E, tk.N, tk.S))

        scrollbar = ttk.Scrollbar(log_frame, orient=tk.VERTICAL, command=self.log_text.yview)
        scrollbar.grid(column=1, row=0, sticky=(tk.N, tk.S))
        self.log_text.config(yscrollcommand=scrollbar.set)

        # Row 5: Results section
        results_frame = ttk.Frame(self.mainframe)
        results_frame.grid(column=0, row=5, columnspan=4, sticky=(tk.W, tk.E), pady=(0, 10))
        results_frame.columnconfigure(1, weight=1)

        # Format selector
        format_label = ttk.Label(results_frame, text="Display as:")
        format_label.grid(column=0, row=0, sticky=tk.W, padx=(0, 5))

        self.unit_combobox = ttk.Combobox(
            results_frame,
            values=["Default", "Hours", "Minutes", "Seconds", "Days"],
            state='readonly',
            width=12
        )
        self.unit_combobox.set("Default")
        self.unit_combobox.grid(column=1, row=0, sticky=tk.W)
        self.unit_combobox.bind("<<ComboboxSelected>>", lambda e: self._update_result_display())

        # Result display
        result_label = ttk.Label(results_frame, text="Total Playtime:", style='Header.TLabel')
        result_label.grid(column=0, row=1, sticky=tk.W, pady=(10, 0))

        self.result_entry = ttk.Entry(results_frame, font=('Helvetica', 11, 'bold'))
        self.result_entry.grid(column=1, row=1, sticky=(tk.W, tk.E), pady=(10, 0), padx=(5, 5))

        self.copy_btn = ttk.Button(results_frame, text="Copy", command=self._copy_to_clipboard)
        self.copy_btn.grid(column=2, row=1, sticky=tk.W, pady=(10, 0))

        # Row 6: Status bar
        self.status_var = tk.StringVar(value="Ready - Select an environment or browse to a log folder")
        status_bar = ttk.Label(
            self.mainframe,
            textvariable=self.status_var,
            style='Status.TLabel',
            relief=tk.SUNKEN,
            padding=(5, 2)
        )
        status_bar.grid(column=0, row=6, columnspan=4, sticky=(tk.W, tk.E))

        # Load clipboard icon
        self.root.after(100, self._load_icon)

    def _detect_installations(self):
        """Detect installed Star Citizen environments."""
        self.detected_paths = sc_playtime.get_default_paths()

        if self.detected_paths:
            environments = list(self.detected_paths.keys())
            self.env_combobox['values'] = environments
            self.env_combobox.set(environments[0])
            self._on_environment_change(None)
            self.status_var.set(f"Found {len(environments)} environment(s): {', '.join(environments)}")
        else:
            self.env_combobox['values'] = ["No installations found"]
            self.env_combobox.set("No installations found")
            # Set a default path for manual browsing
            default = r'C:\Program Files\Roberts Space Industries\StarCitizen\LIVE\logbackups'
            self.path_entry.delete(0, tk.END)
            self.path_entry.insert(0, default)
            self.status_var.set("No Star Citizen installation detected - please browse manually")

    def _on_environment_change(self, event):
        """Handle environment selection change."""
        selected = self.env_combobox.get()
        if selected in self.detected_paths:
            self.path_entry.delete(0, tk.END)
            self.path_entry.insert(0, self.detected_paths[selected])
            self.status_var.set(f"Selected {selected} environment")

    def _select_directory(self):
        """Open directory browser."""
        initial_dir = self.path_entry.get()
        if not os.path.exists(initial_dir):
            initial_dir = os.path.expanduser("~")

        directory = filedialog.askdirectory(initialdir=initial_dir, mustexist=True)
        if directory:
            self.path_entry.delete(0, tk.END)
            self.path_entry.insert(0, directory)
            self.status_var.set(f"Selected: {directory}")

    def _calculate_playtime(self):
        """Calculate total playtime from log files."""
        path = self.path_entry.get()

        if not path:
            messagebox.showwarning("Warning", "Please select a log folder path.")
            return

        if not os.path.exists(path):
            messagebox.showerror("Error", f"Path does not exist:\n{path}")
            self.status_var.set("Error: Path does not exist")
            return

        self.status_var.set("Calculating...")
        self.root.update()

        # Clear previous log
        self.log_text.delete(1.0, tk.END)

        # Calculate playtime
        log_output, self.time_delta, file_count = sc_playtime.just_do_it(path)

        # Display log
        self.log_text.insert(tk.END, log_output)
        self.log_text.see(tk.END)

        # Update result display
        self._update_result_display()

        if file_count > 0:
            self.status_var.set(f"Processed {file_count} log file(s) successfully")
        else:
            self.status_var.set("No valid log files found in the selected path")

    def _update_result_display(self):
        """Update the result entry based on selected format."""
        if self.time_delta is None:
            return

        unit = self.unit_combobox.get()
        self.result_entry.delete(0, tk.END)

        if unit == 'Default':
            result = sc_playtime.format_timedelta(self.time_delta)
        elif unit == 'Hours':
            hours = self.time_delta.total_seconds() / 3600
            result = f"{hours:.2f} hours"
        elif unit == 'Minutes':
            minutes = self.time_delta.total_seconds() / 60
            result = f"{minutes:.2f} minutes"
        elif unit == 'Seconds':
            seconds = self.time_delta.total_seconds()
            result = f"{seconds:.0f} seconds"
        elif unit == 'Days':
            days = self.time_delta.total_seconds() / 86400
            result = f"{days:.2f} days"
        else:
            result = str(self.time_delta)

        self.result_entry.insert(0, result)

    def _copy_to_clipboard(self):
        """Copy result to clipboard."""
        result = self.result_entry.get()
        if result:
            self.root.clipboard_clear()
            self.root.clipboard_append(result)
            self.status_var.set("Copied to clipboard!")
            self.root.after(2000, lambda: self.status_var.set("Ready"))

    def _load_icon(self):
        """Load the clipboard icon for the copy button."""
        try:
            # Try to find the icon in various locations
            script_dir = os.path.dirname(os.path.abspath(__file__))
            icon_paths = [
                os.path.join(script_dir, "resources", "clipboard.png"),
                os.path.join(script_dir, "resources/clipboard.png"),
                "resources/clipboard.png",
            ]

            for icon_path in icon_paths:
                if os.path.exists(icon_path):
                    icon = Image.open(icon_path)
                    icon = icon.resize((16, 16), Image.LANCZOS)
                    self.copy_icon = ImageTk.PhotoImage(icon)
                    self.copy_btn.config(image=self.copy_icon, compound=tk.LEFT)
                    break
        except Exception:
            pass  # Icon loading is optional

    def run(self):
        """Start the application."""
        self.root.mainloop()


def main():
    app = SCPlaytimeCalculator()
    app.run()


if __name__ == "__main__":
    main()
