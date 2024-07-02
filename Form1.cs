using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace StarCitizenPlaytimeCalculator
{
    public partial class Form1 : Form
    {
        private const string DefaultPath = @"C:\Program Files\Roberts Space Industries\StarCitizen\LIVE\logbackups";
        private TimeSpan totalPlayTime = TimeSpan.Zero;

        public Form1()
        {
            InitializeComponent();
            // Pre-fill the folder path with the default value if it exists
            if (Directory.Exists(DefaultPath))
            {
                txtFolderPath.Text = DefaultPath;
            }
            comboBoxFormat.SelectedIndex = 0; // Default to "Default" format
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Browse for logbackups folder";
                folderDialog.SelectedPath = DefaultPath;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFolderPath.Text = folderDialog.SelectedPath;
                }
                else
                {
                    if (Directory.Exists(folderDialog.SelectedPath))
                    {
                        FolderBrowserDialogHelper.ScrollToPath(folderDialog);
                    }
                }
            }
        }

        private void btnProcessLogs_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtFolderPath.Text))
            {
                txtOutput.Clear();
                totalPlayTime = CalculateTotalPlayTime(txtFolderPath.Text);
                DisplayTotalPlayTime();
            }
            else
            {
                MessageBox.Show("The selected folder does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTotalPlayTime.Text))
            {
                Clipboard.SetText(txtTotalPlayTime.Text);
                toolTip.SetToolTip(btnCopyToClipboard, "Copied!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load and resize the clipboard icon
            var originalImage = StarCitizenPlaytimeCalculator.Properties.Resources.clipboard_icon;
            var resizedImage = new Bitmap(originalImage, new Size(this.btnCopyToClipboard.Height - 4, this.btnCopyToClipboard.Height - 4));
            this.btnCopyToClipboard.Image = resizedImage;
        }

        private TimeSpan CalculateTotalPlayTime(string folderPath)
        {
            var logFiles = Directory.GetFiles(folderPath, "*.log", SearchOption.AllDirectories);
            TimeSpan totalPlayTime = TimeSpan.Zero;

            foreach (var logFile in logFiles)
            {
                AppendBoldText("File: ");
                AppendText($"{logFile}, ");
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
                    AppendBoldText("Session Time: ");
                    AppendText($"{sessionTime}{Environment.NewLine}");
                }
            }

            AppendBoldText("Total logs processed: ");
            AppendText($"{logFiles.Length}{Environment.NewLine}");
            AppendBoldText("Total Play Time: ");
            AppendText($"{FormatPlayTime(totalPlayTime)}{Environment.NewLine}");
            return totalPlayTime;
        }

        private void AppendBoldText(string text)
        {
            txtOutput.SelectionFont = new Font(txtOutput.Font, FontStyle.Bold);
            txtOutput.AppendText(text);
            txtOutput.SelectionFont = new Font(txtOutput.Font, FontStyle.Regular);
            txtOutput.ScrollToCaret();
        }

        private void AppendText(string text)
        {
            txtOutput.AppendText(text);
            txtOutput.ScrollToCaret();
        }

        private void DisplayTotalPlayTime()
        {
            string formattedPlayTime = comboBoxFormat.SelectedItem.ToString() == "Hours"
                ? FormatPlayTimeInHours(totalPlayTime)
                : FormatPlayTime(totalPlayTime);

            txtTotalPlayTime.Text = $"Total Playtime: {formattedPlayTime}";
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
            DisplayTotalPlayTime();
        }
    }

    public static class FolderBrowserDialogHelper
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private const int BFFM_INITIALIZED = 1;
        private const int BFFM_SETSELECTIONW = 1126;

        public static void ScrollToPath(FolderBrowserDialog fbd)
        {
            IntPtr hwnd = IntPtr.Zero;
            IntPtr pathPtr = Marshal.StringToHGlobalUni(fbd.SelectedPath);
            try
            {
                SendMessage(hwnd, BFFM_INITIALIZED, IntPtr.Zero, IntPtr.Zero);
                SendMessage(hwnd, BFFM_SETSELECTIONW, IntPtr.Zero, pathPtr);
            }
            finally
            {
                Marshal.FreeHGlobal(pathPtr);
            }
        }
    }
}
