using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastPlayer
{
    public class AppManager
    {
        private static AppManager instance = null;
        private static object singleLock = new object(); //锁同步

        /// <summary>
        /// 创建单例
        /// </summary>
        /// <returns>返回单例对象</returns>
        public static AppManager CreateInstance()
        {
            lock (singleLock)
            {
                if (instance == null)
                {
                    instance = new AppManager();
                }
            }
            return instance;
        }


        /// <summary>
        /// 播放列表
        /// </summary>
        public PlayList PlayList
        {
            get;
            set;
        }

        public void Init()
        {
            try
            {
                if (File.Exists("PlayList.pls"))
                {
                    PlayList = PlayList.LoadFromXML("PlayList.pls");
                }
                else
                {
                    PlayList = new PlayList();
                    PlayList.VideoList = new List<Models.VideoInfo>();

                    PlayList.SaveToXMLFile("PlayList.pls");
                    //File.Create("PlayList.pls");
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// 保存当前播放列表
        /// </summary>
        public void SavePlayList()
        {
            try
            {
                PlayList.SaveToXMLFile("PlayList.pls");
            }
            catch
            { }
        }
    }
}
