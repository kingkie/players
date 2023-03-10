using BroadcastPlayer.Models;
using CinemaSetter.ToolKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastPlayer
{
    public class PlayList
    {
        public List<VideoInfo> VideoList
        {
            get;
            set;
        }
        /// <summary>
        /// 加载视频列表
        /// </summary>
        /// <param name="XMLFilename"></param>
        /// <returns></returns>
        public static PlayList LoadFromXML(string XMLFilename)
        {
            PlayList er = Tools.DESerializer<PlayList>(Tools.file_get_content(XMLFilename));
            if (er == null)
            {
                er = new PlayList();
                er.VideoList = new List<VideoInfo>();
            }
            return er;
        }
        /// <summary>
        /// 保存列表
        /// </summary>
        /// <param name="XMLFilename"></param>
        /// <returns></returns>
        public bool SaveToXMLFile(string XMLFilename)
        {
            string XMLString = Tools.XmlSerialize<PlayList>(this);
            return Tools.file_put_content(XMLFilename, XMLString);
        }
    }
}
