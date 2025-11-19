import os
import threading
import tkinter as tk
from tkinter import filedialog

import ttkbootstrap as ttk
from PIL import Image, ImageTk
from ttkbootstrap.constants import BOTH, LEFT, RIGHT, VERTICAL, YES, X, Y
from ttkbootstrap.dialogs import Messagebox

import sc_playtime

# Use high DPI awareness for better rendering on Windows
try:
    from ctypes import windll  # type: ignore[attr-defined]
    windll.shcore.SetProcessDpiAwareness(1)
except Exception:
    pass  # Fails on non-Windows systems


class SCPlaytimeCalculator:
    def __init__(self):
        # Use cyborg theme for sci-fi look matching Star Citizen
        self.root = ttk.Window(
            title="Star Citizen Playtime Calculator",
            themename="cyborg",
            size=(900, 700),
            minsize=(750, 550)
        )

        self.time_delta = None
        self.copy_icon = None
        self.detected_paths = {}
        self.is_calculating = False

        self._center_window()
        self._create_widgets()
        self._detect_installations()

    def _center_window(self):
        """Center the window on screen."""
        self.root.update_idletasks()
        screen_width = self.root.winfo_screenwidth()
        screen_height = self.root.winfo_screenheight()
        x = (screen_width // 2) - (900 // 2)
        y = (screen_height // 2) - (700 // 2)
        self.root.geometry(f"+{x}+{y}")

    def _create_widgets(self):
        """Create all UI widgets with modern styling."""
        # Main container with padding
        self.mainframe = ttk.Frame(self.root, padding=20)
        self.mainframe.pack(fill=BOTH, expand=YES)

        # Header section
        self._create_header()

        # Configuration section
        self._create_config_section()

        # Log output section
        self._create_log_section()

        # Results section
        self._create_results_section()

        # Status bar
        self._create_status_bar()

    def _create_header(self):
        """Create the header with title and branding."""
        header_frame = ttk.Frame(self.mainframe)
        header_frame.pack(fill=X, pady=(0, 15))

        # Title
        title_label = ttk.Label(
            header_frame,
            text="Star Citizen Playtime Calculator",
            font=("Segoe UI", 18, "bold"),
            bootstyle="inverse-primary"
        )
        title_label.pack(side=LEFT)

        # Version/info
        version_label = ttk.Label(
            header_frame,
            text="v2.0",
            font=("Segoe UI", 10),
            bootstyle="secondary"
        )
        version_label.pack(side=RIGHT, padx=5)

    def _create_config_section(self):
        """Create the configuration/input section."""
        config_frame = ttk.Labelframe(
            self.mainframe,
            text="Configuration",
            bootstyle="info",
            padding=15
        )
        config_frame.pack(fill=X, pady=(0, 15))

        # Environment row
        env_row = ttk.Frame(config_frame)
        env_row.pack(fill=X, pady=(0, 10))

        env_label = ttk.Label(
            env_row,
            text="Environment:",
            font=("Segoe UI", 10, "bold"),
            width=12
        )
        env_label.pack(side=LEFT)

        self.env_combobox = ttk.Combobox(
            env_row,
            state='readonly',
            width=25,
            bootstyle="info"
        )
        self.env_combobox.pack(side=LEFT, padx=(0, 10))
        self.env_combobox.bind("<<ComboboxSelected>>", self._on_environment_change)

        refresh_btn = ttk.Button(
            env_row,
            text="Refresh",
            command=self._detect_installations,
            bootstyle="info-outline",
            width=10
        )
        refresh_btn.pack(side=LEFT)

        # Path row
        path_row = ttk.Frame(config_frame)
        path_row.pack(fill=X)

        path_label = ttk.Label(
            path_row,
            text="Log Folder:",
            font=("Segoe UI", 10, "bold"),
            width=12
        )
        path_label.pack(side=LEFT)

        self.path_entry = ttk.Entry(path_row, font=("Segoe UI", 10))
        self.path_entry.pack(side=LEFT, fill=X, expand=YES, padx=(0, 10))

        browse_btn = ttk.Button(
            path_row,
            text="Browse",
            command=self._select_directory,
            bootstyle="secondary",
            width=10
        )
        browse_btn.pack(side=LEFT, padx=(0, 5))

        self.calculate_btn = ttk.Button(
            path_row,
            text="Calculate",
            command=self._calculate_playtime,
            bootstyle="success",
            width=12
        )
        self.calculate_btn.pack(side=LEFT)

    def _create_log_section(self):
        """Create the log output section."""
        log_frame = ttk.Labelframe(
            self.mainframe,
            text="Processing Log",
            bootstyle="secondary",
            padding=10
        )
        log_frame.pack(fill=BOTH, expand=YES, pady=(0, 15))

        # Progress bar (hidden by default)
        self.progress = ttk.Progressbar(
            log_frame,
            mode='indeterminate',
            bootstyle="success-striped"
        )
        self.progress.pack(fill=X, pady=(0, 10))
        self.progress.pack_forget()  # Hide initially

        # Log text with scrollbar
        log_container = ttk.Frame(log_frame)
        log_container.pack(fill=BOTH, expand=YES)

        self.log_text = tk.Text(
            log_container,
            height=12,
            wrap=tk.WORD,
            font=("Cascadia Code", 9),
            bg="#1a1a2e",
            fg="#e0e0e0",
            insertbackground="#00d4ff",
            selectbackground="#0f3460",
            relief="flat",
            padx=10,
            pady=10
        )
        self.log_text.pack(side=LEFT, fill=BOTH, expand=YES)

        scrollbar = ttk.Scrollbar(
            log_container,
            orient=VERTICAL,
            command=self.log_text.yview,
            bootstyle="round-info"
        )
        scrollbar.pack(side=RIGHT, fill=Y)
        self.log_text.config(yscrollcommand=scrollbar.set)

        # Configure text tags for colored output
        self.log_text.tag_configure("header", foreground="#00d4ff", font=("Cascadia Code", 9, "bold"))
        self.log_text.tag_configure("success", foreground="#00ff88")
        self.log_text.tag_configure("info", foreground="#e0e0e0")
        self.log_text.tag_configure("highlight", foreground="#ffd700")

    def _create_results_section(self):
        """Create the results display section."""
        results_frame = ttk.Labelframe(
            self.mainframe,
            text="Results",
            bootstyle="success",
            padding=15
        )
        results_frame.pack(fill=X, pady=(0, 15))

        # Results row
        results_row = ttk.Frame(results_frame)
        results_row.pack(fill=X)

        # Format selector
        format_label = ttk.Label(
            results_row,
            text="Display as:",
            font=("Segoe UI", 10)
        )
        format_label.pack(side=LEFT, padx=(0, 5))

        self.unit_combobox = ttk.Combobox(
            results_row,
            values=["Default", "Hours", "Minutes", "Seconds", "Days"],
            state='readonly',
            width=10,
            bootstyle="success"
        )
        self.unit_combobox.set("Default")
        self.unit_combobox.pack(side=LEFT, padx=(0, 20))
        self.unit_combobox.bind("<<ComboboxSelected>>", lambda e: self._update_result_display())

        # Total playtime label
        playtime_label = ttk.Label(
            results_row,
            text="Total Playtime:",
            font=("Segoe UI", 11, "bold")
        )
        playtime_label.pack(side=LEFT, padx=(0, 10))

        # Result entry (larger and more prominent)
        self.result_entry = ttk.Entry(
            results_row,
            font=("Segoe UI", 12, "bold"),
            bootstyle="success"
        )
        self.result_entry.pack(side=LEFT, fill=X, expand=YES, padx=(0, 10))

        # Copy button
        self.copy_btn = ttk.Button(
            results_row,
            text="Copy",
            command=self._copy_to_clipboard,
            bootstyle="success-outline",
            width=8
        )
        self.copy_btn.pack(side=LEFT)

        # Load clipboard icon after window is ready
        self.root.after(100, self._load_icon)

    def _create_status_bar(self):
        """Create the status bar at the bottom."""
        self.status_var = tk.StringVar(value="Ready - Select an environment or browse to a log folder")

        status_frame = ttk.Frame(self.mainframe)
        status_frame.pack(fill=X)

        self.status_label = ttk.Label(
            status_frame,
            textvariable=self.status_var,
            font=("Segoe UI", 9),
            bootstyle="secondary",
            padding=(10, 5)
        )
        self.status_label.pack(fill=X)

    def _detect_installations(self):
        """Detect installed Star Citizen environments."""
        self.detected_paths = sc_playtime.get_default_paths()

        if self.detected_paths:
            environments = list(self.detected_paths.keys())
            self.env_combobox['values'] = environments
            self.env_combobox.set(environments[0])
            self._on_environment_change(None)
            self._update_status(f"Found {len(environments)} environment(s): {', '.join(environments)}", "info")
        else:
            self.env_combobox['values'] = ["No installations found"]
            self.env_combobox.set("No installations found")
            # Set a default path for manual browsing
            default = r'C:\Program Files\Roberts Space Industries\StarCitizen\LIVE\logbackups'
            self.path_entry.delete(0, tk.END)
            self.path_entry.insert(0, default)
            self._update_status("No Star Citizen installation detected - please browse manually", "warning")

    def _update_status(self, message, status_type="info"):
        """Update status bar with colored message."""
        self.status_var.set(message)

        # Update status label style based on type
        style_map = {
            "info": "secondary",
            "success": "success",
            "warning": "warning",
            "error": "danger"
        }
        self.status_label.configure(bootstyle=style_map.get(status_type, "secondary"))

    def _on_environment_change(self, event):
        """Handle environment selection change."""
        selected = self.env_combobox.get()
        if selected in self.detected_paths:
            self.path_entry.delete(0, tk.END)
            self.path_entry.insert(0, self.detected_paths[selected])
            self._update_status(f"Selected {selected} environment", "info")

    def _select_directory(self):
        """Open directory browser."""
        initial_dir = self.path_entry.get()
        if not os.path.exists(initial_dir):
            initial_dir = os.path.expanduser("~")

        directory = filedialog.askdirectory(initialdir=initial_dir, mustexist=True)
        if directory:
            self.path_entry.delete(0, tk.END)
            self.path_entry.insert(0, directory)
            self._update_status(f"Selected: {directory}", "info")

    def _calculate_playtime(self):
        """Calculate total playtime from log files."""
        if self.is_calculating:
            return

        path = self.path_entry.get()

        if not path:
            Messagebox.show_warning("Please select a log folder path.", "Warning")
            return

        if not os.path.exists(path):
            Messagebox.show_error(f"Path does not exist:\n{path}", "Error")
            self._update_status("Error: Path does not exist", "error")
            return

        # Start calculation in background thread
        self.is_calculating = True
        self.calculate_btn.configure(state="disabled")
        self._update_status("Calculating...", "info")

        # Show and start progress bar
        self.progress.pack(fill=X, pady=(0, 10))
        self.progress.start(10)

        # Clear previous log
        self.log_text.delete(1.0, tk.END)

        # Run calculation in thread
        thread = threading.Thread(target=self._do_calculation, args=(path,))
        thread.daemon = True
        thread.start()

    def _do_calculation(self, path):
        """Perform the calculation in a background thread."""
        try:
            log_output, self.time_delta, file_count = sc_playtime.just_do_it(path)

            # Update UI in main thread
            self.root.after(0, lambda: self._calculation_complete(log_output, file_count))
        except Exception as e:
            error_msg = str(e)
            self.root.after(0, lambda msg=error_msg: self._calculation_error(msg))

    def _calculation_complete(self, log_output, file_count):
        """Handle calculation completion."""
        # Stop and hide progress bar
        self.progress.stop()
        self.progress.pack_forget()

        # Display log with formatting
        self._display_formatted_log(log_output)

        # Update result display
        self._update_result_display()

        # Update status
        if file_count > 0:
            self._update_status(f"Processed {file_count} log file(s) successfully", "success")
        else:
            self._update_status("No valid log files found in the selected path", "warning")

        # Re-enable button
        self.calculate_btn.configure(state="normal")
        self.is_calculating = False

    def _calculation_error(self, error_message):
        """Handle calculation error."""
        self.progress.stop()
        self.progress.pack_forget()
        self._update_status(f"Error: {error_message}", "error")
        self.calculate_btn.configure(state="normal")
        self.is_calculating = False

    def _display_formatted_log(self, log_output):
        """Display log output with color formatting."""
        self.log_text.delete(1.0, tk.END)

        for line in log_output.split('\n'):
            if line.startswith('File:'):
                self.log_text.insert(tk.END, "File: ", "header")
                self.log_text.insert(tk.END, line[5:] + "\n", "info")
            elif line.startswith('Session Time:'):
                self.log_text.insert(tk.END, "Session Time: ", "header")
                self.log_text.insert(tk.END, line[13:] + "\n", "highlight")
            elif line.startswith('Total'):
                self.log_text.insert(tk.END, line + "\n", "success")
            elif line.strip():
                self.log_text.insert(tk.END, line + "\n", "info")

        self.log_text.see(tk.END)

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
            self._update_status("Copied to clipboard!", "success")
            self.root.after(2000, lambda: self._update_status("Ready", "info"))

    def _load_icon(self):
        """Load the clipboard icon for the copy button."""
        try:
            script_dir = os.path.dirname(os.path.abspath(__file__))
            icon_paths = [
                os.path.join(script_dir, "resources", "clipboard.png"),
                os.path.join(script_dir, "resources/clipboard.png"),
                "resources/clipboard.png",
            ]

            for icon_path in icon_paths:
                if os.path.exists(icon_path):
                    icon = Image.open(icon_path)
                    icon = icon.resize((16, 16), Image.Resampling.LANCZOS)
                    self.copy_icon = ImageTk.PhotoImage(icon)
                    self.copy_btn.configure(image=self.copy_icon, compound=LEFT)
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
