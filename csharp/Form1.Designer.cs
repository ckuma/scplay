using System.Drawing;

namespace StarCitizenPlaytimeCalculator
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // Initialize all controls
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();

            this.panelConfig = new System.Windows.Forms.Panel();
            this.lblConfigTitle = new System.Windows.Forms.Label();
            this.lblEnvironment = new System.Windows.Forms.Label();
            this.comboEnvironment = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblLogFolder = new System.Windows.Forms.Label();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnProcessLogs = new System.Windows.Forms.Button();

            this.panelLog = new System.Windows.Forms.Panel();
            this.lblLogTitle = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.txtOutput = new System.Windows.Forms.RichTextBox();

            this.panelResults = new System.Windows.Forms.Panel();
            this.lblResultsTitle = new System.Windows.Forms.Label();
            this.lblDisplayAs = new System.Windows.Forms.Label();
            this.comboBoxFormat = new System.Windows.Forms.ComboBox();
            this.lblTotalPlaytime = new System.Windows.Forms.Label();
            this.txtTotalPlayTime = new System.Windows.Forms.TextBox();
            this.btnCopyToClipboard = new System.Windows.Forms.Button();

            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);

            this.SuspendLayout();

            //
            // Form colors
            //
            Color bgDark = Color.FromArgb(30, 30, 35);
            Color bgPanel = Color.FromArgb(42, 42, 50);
            Color accentCyan = Color.FromArgb(130, 170, 210);
            Color accentGreen = Color.FromArgb(0, 255, 136);
            Color textLight = Color.FromArgb(224, 224, 224);
            Color textSecondary = Color.FromArgb(160, 160, 160);

            //
            // panelHeader
            //
            this.panelHeader.BackColor = bgPanel;
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Height = 60;
            this.panelHeader.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.panelHeader.Controls.Add(this.lblVersion);
            this.panelHeader.Controls.Add(this.lblTitle);

            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = accentCyan;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Text = "Star Citizen Playtime Calculator";

            //
            // lblVersion
            //
            this.lblVersion.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblVersion.ForeColor = textSecondary;
            this.lblVersion.Location = new System.Drawing.Point(650, 22);
            this.lblVersion.Text = "v2.0";

            //
            // panelConfig
            //
            this.panelConfig.BackColor = bgPanel;
            this.panelConfig.Location = new System.Drawing.Point(12, 72);
            this.panelConfig.Size = new System.Drawing.Size(676, 110);
            this.panelConfig.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelConfig.Padding = new System.Windows.Forms.Padding(15);
            this.panelConfig.Controls.Add(this.lblConfigTitle);
            this.panelConfig.Controls.Add(this.lblEnvironment);
            this.panelConfig.Controls.Add(this.comboEnvironment);
            this.panelConfig.Controls.Add(this.btnRefresh);
            this.panelConfig.Controls.Add(this.lblLogFolder);
            this.panelConfig.Controls.Add(this.txtFolderPath);
            this.panelConfig.Controls.Add(this.btnBrowse);
            this.panelConfig.Controls.Add(this.btnProcessLogs);

            //
            // lblConfigTitle
            //
            this.lblConfigTitle.AutoSize = true;
            this.lblConfigTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblConfigTitle.ForeColor = accentCyan;
            this.lblConfigTitle.Location = new System.Drawing.Point(15, 8);
            this.lblConfigTitle.Text = "Configuration";

            //
            // lblEnvironment
            //
            this.lblEnvironment.AutoSize = true;
            this.lblEnvironment.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEnvironment.ForeColor = textLight;
            this.lblEnvironment.Location = new System.Drawing.Point(15, 35);
            this.lblEnvironment.Text = "Environment:";

            //
            // comboEnvironment
            //
            this.comboEnvironment.BackColor = bgDark;
            this.comboEnvironment.ForeColor = textLight;
            this.comboEnvironment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboEnvironment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEnvironment.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comboEnvironment.Location = new System.Drawing.Point(110, 32);
            this.comboEnvironment.Size = new System.Drawing.Size(200, 23);
            this.comboEnvironment.SelectedIndexChanged += new System.EventHandler(this.comboEnvironment_SelectedIndexChanged);

            //
            // btnRefresh
            //
            this.btnRefresh.BackColor = Color.FromArgb(55, 60, 70);
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = accentCyan;
            this.btnRefresh.ForeColor = accentCyan;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefresh.Location = new System.Drawing.Point(320, 30);
            this.btnRefresh.Size = new System.Drawing.Size(80, 27);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            //
            // lblLogFolder
            //
            this.lblLogFolder.AutoSize = true;
            this.lblLogFolder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLogFolder.ForeColor = textLight;
            this.lblLogFolder.Location = new System.Drawing.Point(15, 70);
            this.lblLogFolder.Text = "Log Folder:";

            //
            // txtFolderPath
            //
            this.txtFolderPath.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.txtFolderPath.BackColor = bgDark;
            this.txtFolderPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFolderPath.ForeColor = textLight;
            this.txtFolderPath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFolderPath.Location = new System.Drawing.Point(110, 67);
            this.txtFolderPath.Size = new System.Drawing.Size(360, 23);

            //
            // btnBrowse
            //
            this.btnBrowse.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnBrowse.BackColor = Color.FromArgb(60, 60, 80);
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.FlatAppearance.BorderColor = textSecondary;
            this.btnBrowse.ForeColor = textLight;
            this.btnBrowse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBrowse.Location = new System.Drawing.Point(480, 65);
            this.btnBrowse.Size = new System.Drawing.Size(80, 27);
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);

            //
            // btnProcessLogs
            //
            this.btnProcessLogs.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnProcessLogs.BackColor = accentGreen;
            this.btnProcessLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessLogs.FlatAppearance.BorderSize = 0;
            this.btnProcessLogs.ForeColor = bgDark;
            this.btnProcessLogs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnProcessLogs.Location = new System.Drawing.Point(570, 65);
            this.btnProcessLogs.Size = new System.Drawing.Size(90, 27);
            this.btnProcessLogs.Text = "Calculate";
            this.btnProcessLogs.UseVisualStyleBackColor = false;
            this.btnProcessLogs.Click += new System.EventHandler(this.btnProcessLogs_Click);

            //
            // panelLog
            //
            this.panelLog.BackColor = bgPanel;
            this.panelLog.Location = new System.Drawing.Point(12, 194);
            this.panelLog.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelLog.Size = new System.Drawing.Size(676, 220);
            this.panelLog.Padding = new System.Windows.Forms.Padding(15);
            this.panelLog.Controls.Add(this.lblLogTitle);
            this.panelLog.Controls.Add(this.progressBar);
            this.panelLog.Controls.Add(this.txtOutput);

            //
            // lblLogTitle
            //
            this.lblLogTitle.AutoSize = true;
            this.lblLogTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLogTitle.ForeColor = textSecondary;
            this.lblLogTitle.Location = new System.Drawing.Point(15, 8);
            this.lblLogTitle.Text = "Processing Log";

            //
            // progressBar
            //
            this.progressBar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.progressBar.Location = new System.Drawing.Point(15, 30);
            this.progressBar.Size = new System.Drawing.Size(646, 5);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.MarqueeAnimationSpeed = 30;
            this.progressBar.Visible = false;

            //
            // txtOutput
            //
            this.txtOutput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.txtOutput.BackColor = bgDark;
            this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutput.ForeColor = textLight;
            this.txtOutput.Font = new System.Drawing.Font("Cascadia Code", 9F);
            this.txtOutput.Location = new System.Drawing.Point(15, 40);
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(646, 165);

            //
            // panelResults
            //
            this.panelResults.BackColor = bgPanel;
            this.panelResults.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelResults.Location = new System.Drawing.Point(12, 426);
            this.panelResults.Size = new System.Drawing.Size(676, 70);
            this.panelResults.Padding = new System.Windows.Forms.Padding(15);
            this.panelResults.Controls.Add(this.lblResultsTitle);
            this.panelResults.Controls.Add(this.lblDisplayAs);
            this.panelResults.Controls.Add(this.comboBoxFormat);
            this.panelResults.Controls.Add(this.lblTotalPlaytime);
            this.panelResults.Controls.Add(this.txtTotalPlayTime);
            this.panelResults.Controls.Add(this.btnCopyToClipboard);

            //
            // lblResultsTitle
            //
            this.lblResultsTitle.AutoSize = true;
            this.lblResultsTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblResultsTitle.ForeColor = accentGreen;
            this.lblResultsTitle.Location = new System.Drawing.Point(15, 8);
            this.lblResultsTitle.Text = "Results";

            //
            // lblDisplayAs
            //
            this.lblDisplayAs.AutoSize = true;
            this.lblDisplayAs.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDisplayAs.ForeColor = textLight;
            this.lblDisplayAs.Location = new System.Drawing.Point(15, 38);
            this.lblDisplayAs.Text = "Display as:";

            //
            // comboBoxFormat
            //
            this.comboBoxFormat.BackColor = bgDark;
            this.comboBoxFormat.ForeColor = textLight;
            this.comboBoxFormat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comboBoxFormat.FormattingEnabled = true;
            this.comboBoxFormat.Items.AddRange(new object[] {
                "Default",
                "Hours",
                "Minutes",
                "Seconds",
                "Days"
            });
            this.comboBoxFormat.Location = new System.Drawing.Point(85, 35);
            this.comboBoxFormat.Size = new System.Drawing.Size(90, 23);
            this.comboBoxFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxFormat_SelectedIndexChanged);

            //
            // lblTotalPlaytime
            //
            this.lblTotalPlaytime.AutoSize = true;
            this.lblTotalPlaytime.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalPlaytime.ForeColor = textLight;
            this.lblTotalPlaytime.Location = new System.Drawing.Point(195, 37);
            this.lblTotalPlaytime.Text = "Total Playtime:";

            //
            // txtTotalPlayTime
            //
            this.txtTotalPlayTime.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.txtTotalPlayTime.BackColor = bgDark;
            this.txtTotalPlayTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalPlayTime.ForeColor = accentGreen;
            this.txtTotalPlayTime.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.txtTotalPlayTime.Location = new System.Drawing.Point(310, 33);
            this.txtTotalPlayTime.ReadOnly = true;
            this.txtTotalPlayTime.Size = new System.Drawing.Size(310, 27);

            //
            // btnCopyToClipboard
            //
            this.btnCopyToClipboard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnCopyToClipboard.BackColor = Color.FromArgb(55, 60, 70);
            this.btnCopyToClipboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyToClipboard.FlatAppearance.BorderColor = accentGreen;
            this.btnCopyToClipboard.ForeColor = accentGreen;
            this.btnCopyToClipboard.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCopyToClipboard.Location = new System.Drawing.Point(630, 32);
            this.btnCopyToClipboard.Size = new System.Drawing.Size(30, 28);
            this.toolTip.SetToolTip(this.btnCopyToClipboard, "Copy to clipboard");
            this.btnCopyToClipboard.UseVisualStyleBackColor = false;
            this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);

            //
            // statusStrip
            //
            this.statusStrip.BackColor = bgPanel;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.statusLabel
            });
            this.statusStrip.Location = new System.Drawing.Point(0, 508);
            this.statusStrip.Size = new System.Drawing.Size(700, 22);

            //
            // statusLabel
            //
            this.statusLabel.ForeColor = textSecondary;
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusLabel.Text = "Ready - Select an environment or browse to a log folder";

            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = bgDark;
            this.ClientSize = new System.Drawing.Size(700, 530);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelConfig);
            this.Controls.Add(this.panelLog);
            this.Controls.Add(this.panelResults);
            this.Controls.Add(this.statusStrip);
            this.MinimumSize = new System.Drawing.Size(716, 569);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Star Citizen Playtime Calculator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Header
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblVersion;

        // Configuration panel
        private System.Windows.Forms.Panel panelConfig;
        private System.Windows.Forms.Label lblConfigTitle;
        private System.Windows.Forms.Label lblEnvironment;
        private System.Windows.Forms.ComboBox comboEnvironment;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblLogFolder;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnProcessLogs;

        // Log panel
        private System.Windows.Forms.Panel panelLog;
        private System.Windows.Forms.Label lblLogTitle;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.RichTextBox txtOutput;

        // Results panel
        private System.Windows.Forms.Panel panelResults;
        private System.Windows.Forms.Label lblResultsTitle;
        private System.Windows.Forms.Label lblDisplayAs;
        private System.Windows.Forms.ComboBox comboBoxFormat;
        private System.Windows.Forms.Label lblTotalPlaytime;
        private System.Windows.Forms.TextBox txtTotalPlayTime;
        private System.Windows.Forms.Button btnCopyToClipboard;

        // Status bar
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
