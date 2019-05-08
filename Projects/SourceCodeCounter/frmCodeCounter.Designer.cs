namespace SourceCodeCounter
{
    partial class frmCodeCounter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPath = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.chkIgnoreEmptyLine = new System.Windows.Forms.CheckBox();
            this.chkIgnoreNamespace = new System.Windows.Forms.CheckBox();
            this.chkIgnoreComments = new System.Windows.Forms.CheckBox();
            this.chkIgnoreDesignerFile = new System.Windows.Forms.CheckBox();
            this.chkSaveLog = new System.Windows.Forms.CheckBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblFile = new System.Windows.Forms.Label();
            this.lblLines = new System.Windows.Forms.Label();
            this.lblTotalLines = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(13, 66);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(49, 13);
            this.lblPath.TabIndex = 0;
            this.lblPath.Text = "Directory";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(12, 87);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(418, 20);
            this.txtPath.TabIndex = 1;
            this.txtPath.Text = "C:\\Test";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(439, 85);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(55, 24);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // chkIgnoreEmptyLine
            // 
            this.chkIgnoreEmptyLine.AutoSize = true;
            this.chkIgnoreEmptyLine.Checked = true;
            this.chkIgnoreEmptyLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIgnoreEmptyLine.Location = new System.Drawing.Point(15, 122);
            this.chkIgnoreEmptyLine.Name = "chkIgnoreEmptyLine";
            this.chkIgnoreEmptyLine.Size = new System.Drawing.Size(106, 17);
            this.chkIgnoreEmptyLine.TabIndex = 3;
            this.chkIgnoreEmptyLine.Text = "Ignore empty line";
            this.chkIgnoreEmptyLine.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreNamespace
            // 
            this.chkIgnoreNamespace.AutoSize = true;
            this.chkIgnoreNamespace.Checked = true;
            this.chkIgnoreNamespace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIgnoreNamespace.Location = new System.Drawing.Point(15, 144);
            this.chkIgnoreNamespace.Name = "chkIgnoreNamespace";
            this.chkIgnoreNamespace.Size = new System.Drawing.Size(116, 17);
            this.chkIgnoreNamespace.TabIndex = 4;
            this.chkIgnoreNamespace.Text = "Ignore Namespace";
            this.chkIgnoreNamespace.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreComments
            // 
            this.chkIgnoreComments.AutoSize = true;
            this.chkIgnoreComments.Checked = true;
            this.chkIgnoreComments.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIgnoreComments.Location = new System.Drawing.Point(15, 166);
            this.chkIgnoreComments.Name = "chkIgnoreComments";
            this.chkIgnoreComments.Size = new System.Drawing.Size(107, 17);
            this.chkIgnoreComments.TabIndex = 5;
            this.chkIgnoreComments.Text = "Ignore comments";
            this.chkIgnoreComments.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreDesignerFile
            // 
            this.chkIgnoreDesignerFile.AutoSize = true;
            this.chkIgnoreDesignerFile.Checked = true;
            this.chkIgnoreDesignerFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIgnoreDesignerFile.Location = new System.Drawing.Point(15, 188);
            this.chkIgnoreDesignerFile.Name = "chkIgnoreDesignerFile";
            this.chkIgnoreDesignerFile.Size = new System.Drawing.Size(305, 17);
            this.chkIgnoreDesignerFile.TabIndex = 6;
            this.chkIgnoreDesignerFile.Text = "Ignore automatically genetrated code (e.g. xxx.Designer.cs)";
            this.chkIgnoreDesignerFile.UseVisualStyleBackColor = true;
            // 
            // chkSaveLog
            // 
            this.chkSaveLog.AutoSize = true;
            this.chkSaveLog.Checked = true;
            this.chkSaveLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveLog.Enabled = false;
            this.chkSaveLog.Location = new System.Drawing.Point(14, 211);
            this.chkSaveLog.Name = "chkSaveLog";
            this.chkSaveLog.Size = new System.Drawing.Size(121, 17);
            this.chkSaveLog.TabIndex = 7;
            this.chkSaveLog.Text = "Save log file (log.txt)";
            this.chkSaveLog.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(14, 245);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(71, 23);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Start Count";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(93, 245);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(71, 23);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.lblFile);
            this.panel1.Controls.Add(this.lblLines);
            this.panel1.Location = new System.Drawing.Point(12, 287);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(479, 89);
            this.panel1.TabIndex = 10;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(8, 29);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(460, 13);
            this.progressBar1.TabIndex = 2;
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(7, 43);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(148, 13);
            this.lblFile.TabIndex = 1;
            this.lblFile.Text = "Show current source code file";
            // 
            // lblLines
            // 
            this.lblLines.AutoSize = true;
            this.lblLines.ForeColor = System.Drawing.Color.Blue;
            this.lblLines.Location = new System.Drawing.Point(8, 9);
            this.lblLines.Name = "lblLines";
            this.lblLines.Size = new System.Drawing.Size(88, 13);
            this.lblLines.TabIndex = 0;
            this.lblLines.Text = "Total 123 lines ...";
            // 
            // lblTotalLines
            // 
            this.lblTotalLines.AutoSize = true;
            this.lblTotalLines.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalLines.ForeColor = System.Drawing.Color.Red;
            this.lblTotalLines.Location = new System.Drawing.Point(181, 250);
            this.lblTotalLines.Name = "lblTotalLines";
            this.lblTotalLines.Size = new System.Drawing.Size(96, 12);
            this.lblTotalLines.TabIndex = 11;
            this.lblTotalLines.Text = "Total: 0 line";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(506, 52);
            this.panel2.TabIndex = 12;
            // 
            // frmCodeCounter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 388);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblTotalLines);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.chkSaveLog);
            this.Controls.Add(this.chkIgnoreDesignerFile);
            this.Controls.Add(this.chkIgnoreComments);
            this.Controls.Add(this.chkIgnoreNamespace);
            this.Controls.Add(this.chkIgnoreEmptyLine);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.lblPath);
            this.Name = "frmCodeCounter";
            this.Text = "C# Source Code Counter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCodeCounter_FormClosing);
            this.Load += new System.EventHandler(this.frmCodeCounter_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.CheckBox chkIgnoreEmptyLine;
        private System.Windows.Forms.CheckBox chkIgnoreNamespace;
        private System.Windows.Forms.CheckBox chkIgnoreComments;
        private System.Windows.Forms.CheckBox chkIgnoreDesignerFile;
        private System.Windows.Forms.CheckBox chkSaveLog;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Label lblLines;
        private System.Windows.Forms.Label lblTotalLines;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel panel2;
    }
}

