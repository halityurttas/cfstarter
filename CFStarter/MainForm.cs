using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFStarter
{
    public partial class MainForm : Form
    {
        Process p = null;
        bool icon = false;

        public MainForm()
        {
            InitializeComponent();
        }

        public bool ControlInvokeRequired(Control c, Action a)
        {
            if (c.InvokeRequired) c.Invoke(new MethodInvoker(delegate { a(); }));
            else return false;

            return true;
        }

        public void UpdateLog(String text)
        {
            //Check if invoke requied if so return - as i will be recalled in correct thread
            if (ControlInvokeRequired(txtLog, () => UpdateLog(text))) return;
            txtLog.AppendText(text);
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Any(m => m == "cfpath"))
            {
                txtCfPath.Text = ConfigurationManager.AppSettings["cfpath"];
                
                var setting = XmlSerializationHelper.Deserialize<ServerConf.Server>(ConfigurationManager.AppSettings["cfpath"] + "\\runtime\\conf\\server.xml");
                txtAppPath.Text = setting.Service.Engine.Host.Context.DocBase;
                
            }
        }

        private void btnSavePath_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings["cfpath"].Value = txtCfPath.Text;
            config.Save(ConfigurationSaveMode.Modified);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            fbCF.ShowDialog();
            if (!string.IsNullOrWhiteSpace(fbCF.SelectedPath))
            {
                txtCfPath.Text = fbCF.SelectedPath;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "Start")
            {
                p = new Process();
                p.StartInfo.FileName = txtCfPath.Text + "\\bin\\coldfusion.exe";
                p.StartInfo.Arguments = @"-start -console";
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardInput = false;
                p.StartInfo.UseShellExecute = false;
                p.OutputDataReceived += (a, b) => UpdateLog(b.Data + Environment.NewLine);
                p.ErrorDataReceived += (a, b) => UpdateLog(b.Data + Environment.NewLine);
                p.Start();
                p.BeginErrorReadLine();
                p.BeginOutputReadLine();
                icon = true;
                notify.Icon = new Icon("coldfusion_101.ico");
                btnStart.Text = "Stop";
                startToolStripMenuItem.Text = "Stop";
            }
            else
            {
                p.Kill();
                icon = false;
                notify.Icon = new Icon("coldfusion_102.ico");
                btnStart.Text = "Start";
                startToolStripMenuItem.Text = "Start";
            }
        }

        private void timerProc_Tick(object sender, EventArgs e)
        {
            if (p == null || p.HasExited)
            {
                if (icon)
                {
                    notify.Icon = new Icon("coldfusion_102.ico");
                    icon = false;
                }
            }
            else
            {
                if (!icon)
                {
                    notify.Icon = new Icon("coldfusion_101.ico");
                    icon = true;
                }
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnStart_Click(sender, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notify.Visible = false;
            if (p != null && !p.HasExited)
            {
                p.Kill();
            }
            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            notify.Visible = false;
            if (p != null && !p.HasExited)
            {
                p.Kill();
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            
        }

        private void notify_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void btnSaveAppPath_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtAppPath.Text))
            {
                var setting = XmlSerializationHelper.Deserialize<ServerConf.Server>(txtCfPath.Text + "\\runtime\\conf\\server.xml");
                setting.Service.Engine.Host.Context.DocBase = txtAppPath.Text;
                XmlSerializationHelper.Serialize<ServerConf.Server>(txtCfPath.Text + "\\runtime\\conf\\server.xml", setting);
                MessageBox.Show("Save successfull!");
            }
        }

        private void btnBrowseAppPath_Click(object sender, EventArgs e)
        {
            fbCF.ShowDialog();
            if (!string.IsNullOrWhiteSpace(fbCF.SelectedPath))
            {
                txtAppPath.Text = fbCF.SelectedPath;
            }
        }
    }
}
