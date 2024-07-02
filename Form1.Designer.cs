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
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.btnProcessLogs = new System.Windows.Forms.Button();
            this.txtTotalPlayTime = new System.Windows.Forms.TextBox();
            this.btnCopyToClipboard = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
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
            this.txtTotalPlayTime.Size = new System.Drawing.Size(355, 20);
            this.txtTotalPlayTime.TabIndex = 3;
            // 
            // btnCopyToClipboard
            // 
            this.btnCopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyToClipboard.Location = new System.Drawing.Point(454, 41);
            this.btnCopyToClipboard.Name = "btnCopyToClipboard";
            this.btnCopyToClipboard.Size = new System.Drawing.Size(34, 23);
            this.btnCopyToClipboard.TabIndex = 4;
            this.toolTip.SetToolTip(this.btnCopyToClipboard, "Copy to clipboard");
            this.btnCopyToClipboard.UseVisualStyleBackColor = true;
            this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
            this.btnCopyToClipboard.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCopyToClipboard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.Location = new System.Drawing.Point(12, 70);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(476, 150);
            this.txtOutput.TabIndex = 5;
            this.txtOutput.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 232);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnCopyToClipboard);
            this.Controls.Add(this.txtTotalPlayTime);
            this.Controls.Add(this.btnProcessLogs);
            this.Controls.Add(this.txtFolderPath);
            this.Controls.Add(this.btnBrowse);
            this.MinimumSize = new System.Drawing.Size(520, 270);
            this.Name = "Form1";
            this.Text = "Star Citizen Logs Analyzer - Calculate Total Time Played";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Button btnProcessLogs;
        private System.Windows.Forms.TextBox txtTotalPlayTime;
        private System.Windows.Forms.Button btnCopyToClipboard;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
