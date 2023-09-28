namespace DirectoryWalker
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.GroupBox directoryInfoGroupBox;
            System.Windows.Forms.Label amountOfFilesTipLabel;
            System.Windows.Forms.Label pathLabel;
            System.Windows.Forms.GroupBox progressGroupBox;
            System.Windows.Forms.Label progressLabelTip;
            System.Windows.Forms.Label processingFileTipLabel;
            System.Windows.Forms.Label statusLabelTip;
            this.amountOfFilesLabel = new System.Windows.Forms.Label();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.percentageLabel = new System.Windows.Forms.Label();
            this.progressLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.processingFileLabel = new System.Windows.Forms.Label();
            this.runButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.statusLabel = new System.Windows.Forms.Label();
            directoryInfoGroupBox = new System.Windows.Forms.GroupBox();
            amountOfFilesTipLabel = new System.Windows.Forms.Label();
            pathLabel = new System.Windows.Forms.Label();
            progressGroupBox = new System.Windows.Forms.GroupBox();
            progressLabelTip = new System.Windows.Forms.Label();
            processingFileTipLabel = new System.Windows.Forms.Label();
            statusLabelTip = new System.Windows.Forms.Label();
            directoryInfoGroupBox.SuspendLayout();
            progressGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // directoryInfoGroupBox
            // 
            directoryInfoGroupBox.Controls.Add(this.amountOfFilesLabel);
            directoryInfoGroupBox.Controls.Add(amountOfFilesTipLabel);
            directoryInfoGroupBox.Controls.Add(this.pathTextBox);
            directoryInfoGroupBox.Controls.Add(pathLabel);
            directoryInfoGroupBox.Location = new System.Drawing.Point(12, 12);
            directoryInfoGroupBox.Name = "directoryInfoGroupBox";
            directoryInfoGroupBox.Size = new System.Drawing.Size(495, 79);
            directoryInfoGroupBox.TabIndex = 4;
            directoryInfoGroupBox.TabStop = false;
            directoryInfoGroupBox.Text = "Directory Information";
            // 
            // amountOfFilesLabel
            // 
            this.amountOfFilesLabel.AutoSize = true;
            this.amountOfFilesLabel.Location = new System.Drawing.Point(94, 52);
            this.amountOfFilesLabel.Name = "amountOfFilesLabel";
            this.amountOfFilesLabel.Size = new System.Drawing.Size(82, 13);
            this.amountOfFilesLabel.TabIndex = 3;
            this.amountOfFilesLabel.Text = "{amountOfFiles}";
            // 
            // amountOfFilesTipLabel
            // 
            amountOfFilesTipLabel.AutoSize = true;
            amountOfFilesTipLabel.Location = new System.Drawing.Point(6, 52);
            amountOfFilesTipLabel.Name = "amountOfFilesTipLabel";
            amountOfFilesTipLabel.Size = new System.Drawing.Size(82, 13);
            amountOfFilesTipLabel.TabIndex = 2;
            amountOfFilesTipLabel.Text = "Amount of Files:";
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(89, 19);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.ReadOnly = true;
            this.pathTextBox.Size = new System.Drawing.Size(400, 20);
            this.pathTextBox.TabIndex = 1;
            // 
            // pathLabel
            // 
            pathLabel.AutoSize = true;
            pathLabel.Location = new System.Drawing.Point(6, 22);
            pathLabel.Name = "pathLabel";
            pathLabel.Size = new System.Drawing.Size(77, 13);
            pathLabel.TabIndex = 0;
            pathLabel.Text = "Directory Path:";
            // 
            // progressGroupBox
            // 
            progressGroupBox.Controls.Add(this.percentageLabel);
            progressGroupBox.Controls.Add(this.progressLabel);
            progressGroupBox.Controls.Add(this.cancelButton);
            progressGroupBox.Controls.Add(this.processingFileLabel);
            progressGroupBox.Controls.Add(this.runButton);
            progressGroupBox.Controls.Add(progressLabelTip);
            progressGroupBox.Controls.Add(this.progressBar);
            progressGroupBox.Controls.Add(processingFileTipLabel);
            progressGroupBox.Controls.Add(this.statusLabel);
            progressGroupBox.Controls.Add(statusLabelTip);
            progressGroupBox.Location = new System.Drawing.Point(12, 97);
            progressGroupBox.Name = "progressGroupBox";
            progressGroupBox.Size = new System.Drawing.Size(495, 197);
            progressGroupBox.TabIndex = 5;
            progressGroupBox.TabStop = false;
            progressGroupBox.Text = "Operation Progress";
            // 
            // percentageLabel
            // 
            this.percentageLabel.AutoSize = true;
            this.percentageLabel.Location = new System.Drawing.Point(6, 143);
            this.percentageLabel.Name = "percentageLabel";
            this.percentageLabel.Size = new System.Drawing.Size(47, 13);
            this.percentageLabel.TabIndex = 10;
            this.percentageLabel.Text = "{perc} %";
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(109, 42);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(113, 13);
            this.progressLabel.TabIndex = 9;
            this.progressLabel.Text = "{currentFile}/{totalFile}";
            // 
            // cancelButton
            // 
            this.cancelButton.Enabled = false;
            this.cancelButton.Location = new System.Drawing.Point(263, 167);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancel_Click);
            // 
            // processingFileLabel
            // 
            this.processingFileLabel.Location = new System.Drawing.Point(6, 81);
            this.processingFileLabel.Name = "processingFileLabel";
            this.processingFileLabel.Size = new System.Drawing.Size(483, 54);
            this.processingFileLabel.TabIndex = 7;
            this.processingFileLabel.Text = "{filePath}";
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(344, 167);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(145, 23);
            this.runButton.TabIndex = 0;
            this.runButton.Text = "Start Analysis";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.run_Click);
            // 
            // progressLabelTip
            // 
            progressLabelTip.AutoSize = true;
            progressLabelTip.Location = new System.Drawing.Point(6, 42);
            progressLabelTip.Name = "progressLabelTip";
            progressLabelTip.Size = new System.Drawing.Size(51, 13);
            progressLabelTip.TabIndex = 8;
            progressLabelTip.Text = "Progress:";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(59, 138);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(430, 23);
            this.progressBar.TabIndex = 2;
            // 
            // processingFileTipLabel
            // 
            processingFileTipLabel.AutoSize = true;
            processingFileTipLabel.Location = new System.Drawing.Point(6, 65);
            processingFileTipLabel.Name = "processingFileTipLabel";
            processingFileTipLabel.Size = new System.Drawing.Size(78, 13);
            processingFileTipLabel.TabIndex = 6;
            processingFileTipLabel.Text = "Processing file:";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(109, 20);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(43, 13);
            this.statusLabel.TabIndex = 5;
            this.statusLabel.Text = "Waiting";
            // 
            // statusLabelTip
            // 
            statusLabelTip.AutoSize = true;
            statusLabelTip.Location = new System.Drawing.Point(6, 20);
            statusLabelTip.Name = "statusLabelTip";
            statusLabelTip.Size = new System.Drawing.Size(40, 13);
            statusLabelTip.TabIndex = 4;
            statusLabelTip.Text = "Status:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 308);
            this.Controls.Add(progressGroupBox);
            this.Controls.Add(directoryInfoGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MIME types analyzer for working directory";
            directoryInfoGroupBox.ResumeLayout(false);
            directoryInfoGroupBox.PerformLayout();
            progressGroupBox.ResumeLayout(false);
            progressGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label amountOfFilesLabel;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.Label processingFileLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label percentageLabel;
    }
}

