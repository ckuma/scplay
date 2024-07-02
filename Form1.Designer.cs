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
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.btnProcessLogs = new System.Windows.Forms.Button();
            this.txtTotalPlayTime = new System.Windows.Forms.TextBox();
            this.btnCopyToClipboard = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.toolTip = new System.Windows.Forms.ToolTip();
            this.comboBoxFormat = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBrowse.Location = new System.Drawing.Point(12, 12);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolderPath.Location = new System.Drawing.Point(93, 14);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(395, 20);
            this.txtFolderPath.TabIndex = 1;
            // 
            // btnProcessLogs
            // 
            this.btnProcessLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnProcessLogs.Location = new System.Drawing.Point(12, 41);
            this.btnProcessLogs.Name = "btnProcessLogs";
            this.btnProcessLogs.Size = new System.Drawing.Size(75, 23);
            this.btnProcessLogs.TabIndex = 2;
            this.btnProcessLogs.Text = "Process Logs";
            this.btnProcessLogs.UseVisualStyleBackColor = true;
            this.btnProcessLogs.Click += new System.EventHandler(this.btnProcessLogs_Click);
            // 
            // txtTotalPlayTime
            // 
            this.txtTotalPlayTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalPlayTime.Location = new System.Drawing.Point(93, 43);
            this.txtTotalPlayTime.Name = "txtTotalPlayTime";
            this.txtTotalPlayTime.ReadOnly = true;
            this.txtTotalPlayTime.Size = new System.Drawing.Size(280, 20);
            this.txtTotalPlayTime.TabIndex = 3;
            // 
            // comboBoxFormat
            // 
            this.comboBoxFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormat.FormattingEnabled = true;
            this.comboBoxFormat.Items.AddRange(new object[] {
            "Default",
            "Hours"});
            this.comboBoxFormat.Location = new System.Drawing.Point(379, 43);
            this.comboBoxFormat.Name = "comboBoxFormat";
            this.comboBoxFormat.Size = new System.Drawing.Size(70, 21);
            this.comboBoxFormat.TabIndex = 4;
            this.comboBoxFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxFormat_SelectedIndexChanged);
            // 
            // btnCopyToClipboard
            // 
            this.btnCopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyToClipboard.Location = new System.Drawing.Point(454, 41);
            this.btnCopyToClipboard.Name = "btnCopyToClipboard";
            this.btnCopyToClipboard.Size = new System.Drawing.Size(34, 23);
            this.btnCopyToClipboard.TabIndex = 5;
            this.toolTip.SetToolTip(this.btnCopyToClipboard, "Copy to clipboard");
            this.btnCopyToClipboard.UseVisualStyleBackColor = true;
            this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right
            | System.Windows.Forms.AnchorStyles.Bottom));
            this.txtOutput.Location = new System.Drawing.Point(12, 70);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(476, 150);
            this.txtOutput.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 232);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnCopyToClipboard);
            this.Controls.Add(this.comboBoxFormat);
            this.Controls.Add(this.txtTotalPlayTime);
            this.Controls.Add(this.btnProcessLogs);
            this.Controls.Add(this.txtFolderPath);
            this.Controls.Add(this.btnBrowse);
            this.MinimumSize = new System.Drawing.Size(520, 270);
            this.Name = "Form1";
            this.Text = "Star Citizen Logs Analyzer - Calculate Total Time Played";
            this.Load += new System.EventHandler(this.Form1_Load); // Subscribe to Load event
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Button btnProcessLogs;
        private System.Windows.Forms.TextBox txtTotalPlayTime;
        private System.Windows.Forms.Button btnCopyToClipboard;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.ComboBox comboBoxFormat;
        private System.Windows.Forms.ToolTip toolTip;
    }
} 
