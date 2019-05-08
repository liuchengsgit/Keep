using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Threading;

namespace VerifyDigitalSignature
{
    public partial class FrmVerifyDigitalSignature : Form
    {
        /// <summary>
        /// verify digital signature
        /// </summary>
        private VerifyDigitalSignature _verifyDigitalSignature;

        public FrmVerifyDigitalSignature()
        {
            InitializeComponent();
        }

        private void FrmVerifyDigitalSignature_Load(object sender, EventArgs e)
        {
            // Create a new instance
            _verifyDigitalSignature = new VerifyDigitalSignature(this, null, this.UpdateProcessBarEventHandle, this.OnThreadFinished);
        }

        private void FrmVerifyDigitalSignature_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_verifyDigitalSignature.IsRunning)
            {
                if (DialogResult.Yes == MessageBox.Show("正在统计代码，要终止程序吗?",
                        "对话框", MessageBoxButtons.YesNo))
                    e.Cancel = false;
                else
                    e.Cancel = true;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
            {
                txtPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = 0;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            this.SetCheckButtonState(false);
            txtResults.Text = string.Empty;

            _verifyDigitalSignature.Start(txtPath.Text);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.SetCheckButtonState(true);
            _verifyDigitalSignature.Stop();
        }

        private void SetCheckButtonState(bool enable)
        {
            btnVerify.Enabled = enable;
            btnStop.Enabled = !enable;
        }

        private void OnThreadFinished()
        {
            this.SetCheckButtonState(true);
            txtResults.Text = _verifyDigitalSignature.Log;
            MessageBox.Show(_verifyDigitalSignature.GetLastResult());
        }

        /// <summary>
        /// update the process bar
        /// </summary>
        private void UpdateProcessBarEventHandle(int count, int currentCount, int totalFiles, int curIndex, string currentFile)
        {
            //label2.Text = string.Format("总计:{0} 行", count);
            //lblLines.Text = string.Format("共{0}行...", currentCount);
            //lblFile.Text = currentFile;

            //update the process bar
            progressBar1.Maximum = totalFiles;
            progressBar1.Value = curIndex;
            progressBar1.Update();
            progressBar1.Refresh();
        }      
    }
}
