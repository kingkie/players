using BroadcastPlayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BroadcastPlayer
{
    public delegate void OnMessageReceived(List<string> lMsg);
    /// <summary>
    /// 程序管理类
    /// </summary>
    public class ServerManager
    {
        private static ServerManager instance = null;
        private static object singleLock = new object(); //锁同步

        /// <summary>
        /// 创建单例
        /// </summary>
        /// <returns>返回单例对象</returns>
        public static ServerManager CreateInstance()
        {
            lock (singleLock)
            {
                if (instance == null)
                {
                    instance = new ServerManager();
                }
            }
            return instance;
        }

        #region 私有变量
        //
        private TcpListener tcpListener;

        private Thread thTcp = null;
        #endregion End
        /// <summary>
        /// 接收到远程控制指令
        /// </summary>
        public OnMessageReceived OnMessageReceived;

        /// <summary>
        /// 服务是否运行中
        /// </summary>
        public bool IsServerRunning
        {
            get;
            set;
        } = false;
        /// <summary>
        /// 服务端口号
        /// </summary>
        public int ServerPort
        {
            get;
            set;
        }
        /// <summary>
        /// 服务IP
        /// </summary>
        public string ServerIP
        {
            get;
            set;
        } = "127.0.0.1";
        /// <summary>
        /// 视频列表
        /// </summary>
        public List<VideoInfo> VideoList
        {
            get;
            set;
        }

        public ServerConfig ServerConfig
        {
            get;
            set;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            string cfg = "ServerConfig.xml";
            if (!File.Exists(cfg))
            {
                var fs = File.Create(cfg);
                fs.Close();

                ServerConfig = new ServerConfig();
                ServerConfig.SaveToXMLFile(cfg);
            } 
            else
            {
                ServerConfig = ServerConfig.LoadFromXML(cfg);
                ServerIP = ServerConfig.ServerIP;
                ServerPort = ServerConfig.ServerPort;
                VideoList = ServerConfig.VideoList;
                if(VideoList == null)
                {
                    VideoList = new List<VideoInfo>();
                }
            }
        }

        public bool SaveConfig()
        {
            string cfg = "ServerConfig.xml";
            //if (!File.Exists(cfg))
            //{
            //    var fs = File.Create(cfg);
            //    fs.Close();
            //}
            if(ServerConfig == null)
            {
                ServerConfig = new ServerConfig();
            }
            else
            {
                ServerConfig.ServerIP = ServerIP;
                ServerConfig.ServerPort = ServerPort;
                ServerConfig.VideoList = VideoList;
            }
            return ServerConfig.SaveToXMLFile(cfg);
        }
        /// <summary>
        /// 开始或者停止服务
        /// </summary>
        public void RunAndStopServer()
        {
            if(!IsServerRunning)
            {
                if (thTcp != null)
                {
                    thTcp.Abort();
                    thTcp = null;
                }
                if (tcpListener != null)
                {
                    tcpListener.Stop();
                    tcpListener = null;
                }

                tcpListener = new TcpListener(IPAddress.Parse(ServerIP), ServerPort);
                tcpListener.Start();

                thTcp = new Thread(TcpOnMessage);
                thTcp.IsBackground = true;
                thTcp.Name = "TcpServer";
                thTcp.Start();

                IsServerRunning = true;
            }
            else
            {
                try
                {
                    if (thTcp != null)
                    {
                        thTcp.Abort();
                        thTcp = null;
                    }
                    if (tcpListener != null)
                    {
                        tcpListener.Stop();
                        tcpListener = null;
                    }
                    try
                    {
                        if (client != null)
                        {
                            client.Close();
                            client = null;
                        }
                        if (stream != null)
                        {
                            stream.Close();
                            stream.Dispose();
                            stream = null;
                        }
                    }
                    catch
                    { }
                    IsServerRunning = false;
                }
                catch
                {
                }
            }
        }

        private NetworkStream stream = null;
        private TcpClient client = null;
        private async void TcpOnMessage()
        {
            if(client != null)
            {
                client.Close();
            }
            client = await tcpListener.AcceptTcpClientAsync();
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[client.ReceiveBufferSize];

                    stream = client.GetStream();//获取网络流  

                    int readnum = stream.Read(buffer, 0, buffer.Length);//读取网络流中的数据
                    if(readnum == 0)
                    {
                        if (client != null)
                        {
                            client.Close();
                        }
                        client = await tcpListener.AcceptTcpClientAsync();
                        stream = null;
                        continue;
                    }

                    //stream.Close();//关闭流  
                    //client.Close();//关闭Client  

                    string receiveString = Encoding.Default.GetString(buffer).Trim('\0');//转换成字符串

                    Console.WriteLine(receiveString);

                    string[] cmdStr = receiveString.Split(new char[] { ' ' },StringSplitOptions.RemoveEmptyEntries);

                    if(cmdStr != null && cmdStr.Length > 0)
                    {
                        if (OnMessageReceived != null)
                        {
                            List<string> lCmdPara = new List<string>();
                            lCmdPara.AddRange(cmdStr);
                            OnMessageReceived.Invoke(lCmdPara);
                        }
                    }
                    Thread.Sleep(200);
                }
                catch(Exception ex)
                {
                    if(tcpListener != null)
                    {
                        if (client != null)
                        {
                            client.Close();
                        }
                        client = await tcpListener.AcceptTcpClientAsync();
                        stream = null;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
