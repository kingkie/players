using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using CinemaSetter.ToolKit;

namespace BroadcastPlayer.Runtime
{
    class ScentRealmSDKCMD
    {

        public static string GetErrorMSG(Int32 StatusCode)
        {
            string[] MSGArray = new String[] {
                    "OK",
                    "设备",
                    "控制器"
                };
            return MSGArray[StatusCode];
        }

        public static Boolean IsOK(Int32 StatusCode)
        {
            return StatusCode == 0;
        }

        void Log(string msg)
        {
            Tools.TLog("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + msg);
        }

        public int init()
        {
            int ret = -1;
            ret = ScentrealmInit();
            this.Log("ScentrealmInit = " + ret);
            return ret;
        }

        public int PlaySmell(int smell, int duration)
        {
            int ret = -1;
            ret = ScentrealmPlaySmell(smell, duration);
            this.Log("ScentrealmPlaySmell smell = " + smell + " duration = " + duration + " ret = " + ret);
            return ret;
        }


        public int StopPlay()
        {
            int ret = -1;
            ret = ScentrealmStopPlay();
            this.Log("ScentrealmStopAll = " + ret);
            return ret;
        }

        /// <summary>
        /// 0设备连接成功，1设备未连接，2控制器未连接
        /// </summary>
        /// <returns></returns>
        public int GetConnectStatus()
        {
            int ret = -1;
            ret = ScentrealmGetConnectStatus();
            this.Log("ScentrealmGetStatus = " + ret);
            return ret;
        }

        public int Close()
        {
            int ret = -1;
            ret = ScentrealmClose();
            this.Log("ScentrealmClose = " + ret);
            return ret;
        }

        [DllImport("scentrealm-SDK")]
        static extern private int ScentrealmInit();

        [DllImport("scentrealm-SDK")]
        static extern private int ScentrealmPlaySmell(int smell, int duration);

        [DllImport("scentrealm-SDK")]
        static extern private int ScentrealmClose();

        [DllImport("scentrealm-SDK")]
        static extern private int ScentrealmStopPlay();

        [DllImport("scentrealm-SDK")]
        static extern private int ScentrealmGetConnectStatus();
    }
}
