using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using DirectoryWalker.Core;

namespace DirectoryWalker
{
    public partial class MainForm : Form
    {
        private CancellationTokenSource cancellationTokenSource;
        private Core.DirectoryWalker walker;

        public MainForm()
        {
            InitializeComponent();

            walker = new Core.DirectoryWalker(Environment.CurrentDirectory);
            walker.ProgressChanged += Walker_ProgressChanged;
            walker.Completed += Walker_Completed;
            cancellationTokenSource = new CancellationTokenSource();

            pathTextBox.Text = walker.DirectoryPath;
            amountOfFilesLabel.Text = walker.TotalEntriesCount.ToString();
            progressLabel.Text = string.Format("0/{0}", walker.TotalEntriesCount);
            processingFileLabel.Text = string.Empty;
            percentageLabel.Text = "0 %";
            cancelButton.Enabled = false;
            runButton.Enabled = true;
        }

        private async void run_Click(object sender, EventArgs e)
        {
            try
            {
                cancellationTokenSource = new CancellationTokenSource();
                cancelButton.Enabled = true;
                runButton.Enabled = false;
                statusLabel.Text = "Analysis...";
                DirectoryEntry directoryEntry = await walker.WalkThroughAsync(cancellationTokenSource.Token);
                DirectoryMimeTypeInfo[] mimeTypeInfo = directoryEntry.GetMimeTypeInfos();

                if (!walker.IsCancelled)
                {
                    statusLabel.Text = "Generting report...";
                    HtmlReporter reporter = new HtmlReporter(walker.DirectoryPath, Path.GetFileName(walker.DirectoryPath) + "_report_" + DateTime.Now.ToString("HH-mm-ss_dd-MM-yyyy"));
                    reporter.GenerateReport(directoryEntry, mimeTypeInfo);

                    statusLabel.Text = "Completed";
                    switch (MessageBox.Show("Report was generated at: " + reporter.OutputFile + ". Open report now?", "Successfully generated!", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    {
                        case DialogResult.Yes:
                            Process.Start(new ProcessStartInfo(reporter.OutputFile) { UseShellExecute = true });
                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Unable to analyze directory! " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            cancellationTokenSource.Cancel();
        }

        private void Walker_Completed(object sender, DirectoryWalkerCompleted e)
        {
            cancelButton.Enabled = false;
            runButton.Enabled = true;
            progressBar.Value = 100;

            if (e.IsCancelled)
            {
                statusLabel.Text = "Cancelled";
                MessageBox.Show("Operation was cancelled by user!", "Cancelled", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void Walker_ProgressChanged(object sender, DirectoryWalkerProgressChanged e)
        {
            float progress = ((float)e.CurrentEntryIndex / e.TotalEntriesCount) * 100f;
            progressBar.Value = (int)progress;
            progressLabel.Text = string.Format("{0}/{1}", e.CurrentEntryIndex, e.TotalEntriesCount);
            processingFileLabel.Text = e.CurrentEntry.FullName;
            percentageLabel.Text = string.Format("{0:F2} %", progress);
        }
    }
}
