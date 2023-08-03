using BroadcastPlayer.Models;
using CinemaSetter.ToolKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastPlayer
{
    public class ServerConfig
    {
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
        }

        public List<VideoInfo> VideoList
        {
            get;
            set;
        }

        public static ServerConfig LoadFromXML(string XMLFilename)
        {
            ServerConfig er = Tools.DESerializer<ServerConfig>(Tools.file_get_content(XMLFilename));
            if(er == null)
            {
                er = new ServerConfig();
            }
            return er;
        }

        public bool SaveToXMLFile(string XMLFilename)
        {
            string XMLString = Tools.XmlSerialize<ServerConfig>(this);
            return Tools.file_put_content(XMLFilename, XMLString);
        }
    }
}
