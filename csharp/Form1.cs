using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarCitizenPlaytimeCalculator
{
    public partial class Form1 : Form
    {
        private const string DefaultPath = @"C:\Program Files\Roberts Space Industries\StarCitizen\LIVE\logbackups";
        private TimeSpan totalPlayTime = TimeSpan.Zero;
        private Dictionary<string, string> detectedPaths = new Dictionary<string, string>();

        // Colors for formatting
        private readonly Color accentCyan = Color.FromArgb(0, 212, 255);
        private readonly Color accentGreen = Color.FromArgb(0, 255, 136);
        private readonly Color accentGold = Color.FromArgb(255, 215, 0);
        private readonly Color textLight = Color.FromArgb(224, 224, 224);

        public Form1()
        {
            InitializeComponent();
            comboBoxFormat.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load and resize the clipboard icon
            try
            {
                var originalImage = StarCitizenPlaytimeCalculator.Properties.Resources.clipboard_icon;
                var resizedImage = new Bitmap(originalImage, new Size(this.btnCopyToClipboard.Height - 4, this.btnCopyToClipboard.Height - 4));
                this.btnCopyToClipboard.Image = resizedImage;
            }
            catch
            {
                // Icon loading is optional
            }

            // Detect installations on load
            DetectInstallations();
        }

        private void DetectInstallations()
        {
            detectedPaths.Clear();
            comboEnvironment.Items.Clear();

            // Common installation paths to check
            string[] drivesToCheck = { "C", "D", "E", "F", "G" };
            string[] environments = { "LIVE", "PTU", "EPTU", "TECH-PREVIEW" };

            foreach (var drive in drivesToCheck)
            {
                string basePath = $@"{drive}:\Program Files\Roberts Space Industries\StarCitizen";

                if (Directory.Exists(basePath))
                {
                    foreach (var env in environments)
                    {
                        string logPath = Path.Combine(basePath, env, "logbackups");
                        if (Directory.Exists(logPath))
                        {
                            string key = drive == "C" ? env : $"{env} ({drive}:)";
                            detectedPaths[key] = logPath;
                        }
                    }
                }
            }

            if (detectedPaths.Count > 0)
            {
                foreach (var env in detectedPaths.Keys)
                {
                    comboEnvironment.Items.Add(env);
                }
                comboEnvironment.SelectedIndex = 0;
                UpdateStatus($"Found {detectedPaths.Count} environment(s)", StatusType.Info);
            }
            else
            {
                comboEnvironment.Items.Add("No installations found");
                comboEnvironment.SelectedIndex = 0;

                // Set default path for manual browsing
                if (Directory.Exists(DefaultPath))
                {
                    txtFolderPath.Text = DefaultPath;
                }
                UpdateStatus("No Star Citizen installation detected - please browse manually", StatusType.Warning);
            }
        }

        private void comboEnvironment_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = comboEnvironment.SelectedItem?.ToString();
            if (selected != null && detectedPaths.ContainsKey(selected))
            {
                txtFolderPath.Text = detectedPaths[selected];
                UpdateStatus($"Selected {selected} environment", StatusType.Info);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DetectInstallations();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Browse for logbackups folder";

                if (!string.IsNullOrEmpty(txtFolderPath.Text) && Directory.Exists(txtFolderPath.Text))
                {
                    folderDialog.SelectedPath = txtFolderPath.Text;
                }
                else
                {
                    folderDialog.SelectedPath = DefaultPath;
                }

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFolderPath.Text = folderDialog.SelectedPath;
                    UpdateStatus($"Selected: {folderDialog.SelectedPath}", StatusType.Info);
                }
            }
        }

        private async void btnProcessLogs_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(txtFolderPath.Text))
            {
                MessageBox.Show("The selected folder does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus("Error: Path does not exist", StatusType.Error);
                return;
            }

            // Disable button and show progress
            btnProcessLogs.Enabled = false;
            progressBar.Visible = true;
            txtOutput.Clear();
            UpdateStatus("Calculating...", StatusType.Info);

            try
            {
                // Run calculation asynchronously
                await Task.Run(() =>
                {
                    totalPlayTime = CalculateTotalPlayTime(txtFolderPath.Text);
                });

                // Update display
                DisplayTotalPlayTime();

                var logFiles = Directory.GetFiles(txtFolderPath.Text, "*.log", SearchOption.AllDirectories);
                if (logFiles.Length > 0)
                {
                    UpdateStatus($"Processed {logFiles.Length} log file(s) successfully", StatusType.Success);
                }
                else
                {
                    UpdateStatus("No valid log files found in the selected path", StatusType.Warning);
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error: {ex.Message}", StatusType.Error);
            }
            finally
            {
                // Re-enable button and hide progress
                btnProcessLogs.Enabled = true;
                progressBar.Visible = false;
            }
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTotalPlayTime.Text))
            {
                Clipboard.SetText(txtTotalPlayTime.Text);
                UpdateStatus("Copied to clipboard!", StatusType.Success);
                toolTip.SetToolTip(btnCopyToClipboard, "Copied!");

                // Reset tooltip after 2 seconds
                Task.Delay(2000).ContinueWith(_ =>
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            toolTip.SetToolTip(btnCopyToClipboard, "Copy to clipboard");
                            UpdateStatus("Ready", StatusType.Info);
                        }));
                    }
                });
            }
        }

        private TimeSpan CalculateTotalPlayTime(string folderPath)
        {
            var logFiles = Directory.GetFiles(folderPath, "*.log", SearchOption.AllDirectories);
            TimeSpan totalPlayTime = TimeSpan.Zero;

            foreach (var logFile in logFiles)
            {
                // Use Invoke for thread-safe UI updates
                this.Invoke(new Action(() =>
                {
                    AppendColoredText("File: ", accentCyan, true);
                    AppendColoredText($"{logFile}\n", textLight, false);
                }));

                var lines = File.ReadAllLines(logFile);
                DateTime? firstTimestamp = null;
                DateTime? lastTimestamp = null;

                foreach (var line in lines)
                {
                    if (line.StartsWith("<") && line.IndexOf(">") > 0)
                    {
                        string timestampString = line.Substring(1, line.IndexOf(">") - 1);
                        if (DateTime.TryParse(timestampString, out DateTime timestamp))
                        {
                            if (!firstTimestamp.HasValue)
                            {
                                firstTimestamp = timestamp;
                            }
                            lastTimestamp = timestamp;
                        }
                    }
                }

                if (firstTimestamp.HasValue && lastTimestamp.HasValue)
                {
                    var sessionTime = lastTimestamp.Value - firstTimestamp.Value;
                    totalPlayTime += sessionTime;

                    this.Invoke(new Action(() =>
                    {
                        AppendColoredText("Session Time: ", accentCyan, true);
                        AppendColoredText($"{sessionTime}\n", accentGold, false);
                    }));
                }
            }

            this.Invoke(new Action(() =>
            {
                AppendColoredText($"\nTotal logs processed: {logFiles.Length}\n", accentGreen, true);
                AppendColoredText($"Total Play Time: {FormatPlayTime(totalPlayTime)}\n", accentGreen, true);
            }));

            return totalPlayTime;
        }

        private void AppendColoredText(string text, Color color, bool bold)
        {
            txtOutput.SelectionStart = txtOutput.TextLength;
            txtOutput.SelectionLength = 0;
            txtOutput.SelectionColor = color;
            txtOutput.SelectionFont = new Font(txtOutput.Font, bold ? FontStyle.Bold : FontStyle.Regular);
            txtOutput.AppendText(text);
            txtOutput.SelectionColor = txtOutput.ForeColor;
            txtOutput.ScrollToCaret();
        }

        private void DisplayTotalPlayTime()
        {
            string format = comboBoxFormat.SelectedItem?.ToString() ?? "Default";
            string formattedPlayTime;

            switch (format)
            {
                case "Hours":
                    formattedPlayTime = FormatPlayTimeInHours(totalPlayTime);
                    break;
                case "Minutes":
                    formattedPlayTime = $"{totalPlayTime.TotalMinutes:F2} minutes";
                    break;
                case "Seconds":
                    formattedPlayTime = $"{totalPlayTime.TotalSeconds:F0} seconds";
                    break;
                case "Days":
                    formattedPlayTime = $"{totalPlayTime.TotalDays:F2} days";
                    break;
                default:
                    formattedPlayTime = FormatPlayTime(totalPlayTime);
                    break;
            }

            txtTotalPlayTime.Text = formattedPlayTime;
        }

        private string FormatPlayTime(TimeSpan totalPlayTime)
        {
            int months = (int)(totalPlayTime.TotalDays / 30);
            int days = (int)(totalPlayTime.TotalDays % 30);
            int hours = totalPlayTime.Hours;
            int minutes = totalPlayTime.Minutes;
            int seconds = totalPlayTime.Seconds;

            return $"{months} months, {days} days, {hours} hours, {minutes} minutes, {seconds} seconds";
        }

        private string FormatPlayTimeInHours(TimeSpan totalPlayTime)
        {
            int totalHours = (int)totalPlayTime.TotalHours;
            int minutes = totalPlayTime.Minutes;
            int seconds = totalPlayTime.Seconds;

            return $"{totalHours} hours, {minutes} minutes, {seconds} seconds";
        }

        private void comboBoxFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (totalPlayTime != TimeSpan.Zero)
            {
                DisplayTotalPlayTime();
            }
        }

        private enum StatusType
        {
            Info,
            Success,
            Warning,
            Error
        }

        private void UpdateStatus(string message, StatusType type)
        {
            statusLabel.Text = message;

            switch (type)
            {
                case StatusType.Success:
                    statusLabel.ForeColor = accentGreen;
                    break;
                case StatusType.Warning:
                    statusLabel.ForeColor = Color.FromArgb(255, 193, 7);
                    break;
                case StatusType.Error:
                    statusLabel.ForeColor = Color.FromArgb(220, 53, 69);
                    break;
                default:
                    statusLabel.ForeColor = Color.FromArgb(160, 160, 160);
                    break;
            }
        }
    }
}
