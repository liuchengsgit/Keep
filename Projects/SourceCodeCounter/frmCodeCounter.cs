using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SourceCodeCounter
{
    public partial class frmCodeCounter : Form
    {
        private CodeCounter _CodeCounter;

        public frmCodeCounter()
        {
            InitializeComponent();
        }

        private void frmCodeCounter_Load(object sender, EventArgs e)
        {
            _CodeCounter = new CodeCounter(this, null, this.UpdateProcessBarEventHandle, this.OnThreadFinished);
        }

        private void UpdateProcessBarEventHandle(int count, int currentCount, int totalFiles, int currIndex, string currentFile)
        {
            lblTotalLines.Text = string.Format("总计:{0} 行", count);
            lblLines.Text = string.Format("共{0}行...", currentCount);
            lblFile.Text = currentFile;

            //显示当前目录统计进度
            progressBar1.Maximum = totalFiles;
            progressBar1.Value = currIndex + 1;
            progressBar1.Update();
        }

        private void OnThreadFinished()
        {
            this.SetCountingButtonState(true);
            MessageBox.Show(_CodeCounter.GetLastResult());
        }

        private void SetCountingButtonState(bool enable)
        {
            btnStart.Enabled = enable;
            btnStop.Enabled = !enable;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
                txtPath.Text = folderBrowserDialog1.SelectedPath;

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = 0;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            this.SetCountingButtonState(false);

            //实例化一个统计器，使用策略模式
            ICountStrategy strategy = new CountStrategy(chkIgnoreEmptyLine.Checked,
                chkIgnoreNamespace.Checked, chkIgnoreComments.Checked, chkIgnoreDesignerFile.Checked);

            _CodeCounter.SetStrategy(strategy); //设置统计策略

            //开始统计
            _CodeCounter.Start(txtPath.Text);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.SetCountingButtonState(true);
            _CodeCounter.Stop();
        }

        private void frmCodeCounter_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_CodeCounter.IsRunning)
            {
                if (DialogResult.Yes == MessageBox.Show("正在统计代码，要终止程序吗?",
                    "对话框", MessageBoxButtons.YesNo))
                    e.Cancel = false;
                else
                    e.Cancel = true;
            }
        }
    }
}
