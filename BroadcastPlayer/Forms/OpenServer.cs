using BroadcastPlayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BroadcastPlayer.Forms
{
    public partial class OpenServer : Form
    {
        public OpenServer()
        {
            InitializeComponent();
        }

        private void OpenServer_Load(object sender, EventArgs e)
        {
            List<string> loaclIps = GetLoacalIp();
            cboServerIP.Items.AddRange(loaclIps.ToArray());
            if(!string.IsNullOrEmpty( ServerManager.CreateInstance().ServerIP))
            {
                cboServerIP.SelectedItem = ServerManager.CreateInstance().ServerIP;
            }

            txtPort.Text = ServerManager.CreateInstance().ServerPort.ToString();
            if (ServerManager.CreateInstance().IsServerRunning)
            {
                btnService.Text = "停止服务";
            }
            else
            {
                btnService.Text = "开启服务";
            }
            dgvVideo.DataSource = new BindingList<VideoInfo>(ServerManager.CreateInstance().VideoList);
        }
        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        private List<string> GetLoacalIp()
        {
            List<string> Ips = new List<string>();
            string name = Dns.GetHostName();
            IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
            foreach (IPAddress ipa in ipadrlist)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                {
                    Ips.Add(ipa.ToString());
                }
            }
            return Ips;
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            IPAddress ipServer = null;
            if(!IPAddress.TryParse(cboServerIP.Text,out ipServer))
            {
                MessageBox.Show("请选择正确的服务IP！");
                return;
            }

            int serverPort = int.Parse(txtPort.Text);
            if(!(serverPort > 0 && serverPort < 65535))
            {
                MessageBox.Show("请设置正确的服务端口号！");
                return;
            }

            ServerManager.CreateInstance().ServerIP = cboServerIP.Text;
            ServerManager.CreateInstance().ServerPort = serverPort;

            ServerManager.CreateInstance().RunAndStopServer();
            if(ServerManager.CreateInstance().IsServerRunning)
            {
                btnService.Text = "停止服务";
            }
            else
            {
                btnService.Text = "开启服务";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "视频文件(*.mp4;*.avi;*.wmv;*.rmvb;*.3gp;*.rm)|*.mp4;*.avi;*.wmv;*.rmvb;*.3gp;*.rm|(All file(*.*)|*.*";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = fileDialog.SafeFileName;

                    VideoInfo vInfo = new VideoInfo();
                    vInfo.VideoName = fileName.Split(new char[] {'.'})[0];
                    vInfo.VideoAddr = fileDialog.FileName;
                    ServerManager.CreateInstance().VideoList.Add(vInfo);

                    dgvVideo.DataSource = null;
                    dgvVideo.DataSource = ServerManager.CreateInstance().VideoList;
                    dgvVideo.Refresh();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            IPAddress ipServer = null;
            if (!IPAddress.TryParse(cboServerIP.Text.Trim(), out ipServer))
            {
                MessageBox.Show("请选择正确的服务IP！");
                return;
            }
            else
            {
                ServerManager.CreateInstance().ServerIP = cboServerIP.Text.Trim();
            }

            int serverPort = int.Parse(txtPort.Text);
            if (!(serverPort > 0 && serverPort < 65535))
            {
                MessageBox.Show("请设置正确的服务端口号！");
                return;
            }
            else
            {
                ServerManager.CreateInstance().ServerPort = serverPort;
            }
            List<VideoInfo> lVideos = new List<VideoInfo>();
            foreach(DataGridViewRow rowItem in this.dgvVideo.Rows)
            {
                var vitem = rowItem.DataBoundItem as VideoInfo;
                if(vitem != null && !string.IsNullOrEmpty(vitem.VideoAddr))
                {
                    lVideos.Add(vitem);
                }
            }
            ServerManager.CreateInstance().VideoList.Clear();
            ServerManager.CreateInstance().VideoList.AddRange(lVideos);

            if( ServerManager.CreateInstance().SaveConfig())
            {
                MessageBox.Show("保存配置成功！");
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if(dgvVideo.SelectedRows.Count < 1)
            {
                MessageBox.Show("请选择要删除的行！");
            }
            var vInfo =  this.dgvVideo.CurrentRow.DataBoundItem as VideoInfo;

            dgvVideo.DataSource = null;
            ServerManager.CreateInstance().VideoList.Remove(vInfo);
            dgvVideo.DataSource = ServerManager.CreateInstance().VideoList;
            dgvVideo.Refresh();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }
    }
}
