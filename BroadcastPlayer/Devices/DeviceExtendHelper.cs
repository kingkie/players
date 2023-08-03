using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace BroadcastPlayer
{
    public class DeviceExtendHelper
	{
		private static DeviceExtendHelper instance = null;
		private static object singleLock = new object();
		public XiaoboServer XiaoboServer
		{
			get;
			set;
		}

		public UdpClient XiaoboClient
		{
			get;
			set;
		}

		public bool Connected
		{
			get;
			set;
		}

		public string ServerIp
		{
			get;
			set;
		}

		public int ServerPort
		{
			get;
			set;
		}

		public static DeviceExtendHelper CreateInstance()
		{
			lock (singleLock)
			{
				if (instance == null)
				{
				    instance = new DeviceExtendHelper();
				}
			}
			return instance;
		}

		public void Init()
		{
			try
			{
				string text = "XiaoboServer.xml";
				if (!File.Exists(text))
				{
					FileStream fileStream = File.Create(text);
					fileStream.Close();
					this.XiaoboServer = new XiaoboServer();
					this.XiaoboServer.SaveToXMLFile(text);
				}
				else
				{
					this.XiaoboServer = XiaoboServer.LoadFromXML(text);
				}
				this.ServerIp = this.XiaoboServer.ServerIP;
				this.ServerPort = this.XiaoboServer.ServerPort;
				if (XiaoboClient == null)
				{
					this.XiaoboClient = new UdpClient();
				}
				if (this.XiaoboServer != null && this.XiaoboServer.ServerPort > 0)
				{
					this.XiaoboConnect();
				}
			}
			catch
			{
			}
		}

		public void DisConnect()
		{
			try
			{
				bool flag = this.XiaoboClient != null && this.XiaoboClient.Client != null;
				if (flag)
				{
					this.XiaoboClient.Client.Disconnect(true);
					this.XiaoboClient.Close();
					this.XiaoboClient = null;
				}
				this.Connected = false;
			}
			catch (Exception ex)
			{
				this.Connected = false;
			}
		}

		public void XiaoboConnect()
		{
			try
			{
				if (XiaoboClient == null)
				{
					this.XiaoboClient = new UdpClient();
				}
				this.XiaoboClient.Connect(this.ServerIp, this.ServerPort);
				this.Connected = this.XiaoboClient.Client.Connected;
			}
			catch
			{
				this.Connected = false;
			}
		}

		public void PlaySmell(int SmellID, int Duration)
		{
			string cmdStr = "play 01FF" + SmellID.ToString("00") + Duration.ToString("0000");
			this.SendCmd(cmdStr);
		}

		public void StopPlay()
		{
			string cmdStr = "stop 01FF000000";
			this.SendCmd(cmdStr);
		}

		public void SendCmd(string cmdStr)
		{
			try
			{
				byte[] bytes = Encoding.ASCII.GetBytes(cmdStr);
				if (this.XiaoboClient != null && this.XiaoboClient.Client.Connected)
				{
					Console.WriteLine("rntNum:" + this.XiaoboClient.Send(bytes, bytes.Length).ToString());
				}
				else
				{
					if (this.XiaoboClient == null)
					{
						this.XiaoboClient = new UdpClient();
					}
					this.XiaoboClient.Send(bytes, bytes.Length, this.ServerIp, this.ServerPort);
				}
			}
			catch
			{
			}
		}
	}
}
