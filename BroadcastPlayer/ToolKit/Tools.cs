using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Security.Cryptography;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using NPOI.HSSF.UserModel;
using System.Data;
using NPOI.HSSF.Util;
using System.Net;
using System.Net.Sockets;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Globalization;

namespace CinemaSetter.ToolKit
{
    class UISync
    {
        private static ISynchronizeInvoke Sync;

        public static void Init(ISynchronizeInvoke sync)
        {
            Sync = sync;
        }

        public static void Execute(Action action)
        {
            if (Sync.InvokeRequired)
            {
                Sync.BeginInvoke(action, null);
            }
            else
            {
                action.Invoke();
            }
        }
    }


    public class UISyncInstance
    {
        private ISynchronizeInvoke Sync;

        public void Init(ISynchronizeInvoke sync)
        {
            Sync = sync;
        }

        public void Execute(Action action)
        {
            if (Sync.InvokeRequired)
            {
                Sync.BeginInvoke(action, null);
            }
            else
            {
                action.Invoke();
            }
        }
    }

    public delegate void VoidDictionaryStringObjectDelegate(Dictionary<String, Object> data);

    public delegate Boolean BooleanNoParamDelegate();

    public delegate void VoidNoParamDelegate();

    public delegate void VoidByteDelegate(Byte bt);

    public delegate void VoidStringDelegate(String filename);

    public delegate void VoidStringSerialPortDelegate(String str, System.IO.Ports.SerialPort SP);

    public delegate Boolean BooleanByteArrayDelegate(Byte[] Data);

    public delegate void VoidSerialPortDelegate(System.IO.Ports.SerialPort SP);

    public delegate void VoidDataTableDelegate(DataTable DT);

    public delegate void VoidIPAddressDelegate(IPAddress Addr);

    public delegate void VoidInt32Delegate(Int32 Timeout);

    public delegate void VoidStringInt32Delegate(String str, Int32 i32);

    public delegate void VoidBitmapDelegate(Bitmap image);

    public delegate Int32 Int32StringDelegate(String Input);

    public delegate void VoidObjectDelegate(Object obj);

    public delegate void VoidByteArrayDelegate(Byte[] ByteArray);

    public delegate void VoidByteArrayInt32Int32Delegate(Byte[] ByteArray, Int32 PackageCount, Int32 CurrentPackageIndex);

    public delegate Boolean BooleanByteArrayInt32Int32Delegate(Byte[] ByteArray, Int32 PackageCount, Int32 CurrentPackageIndex);

    public delegate void VoidInt32DoubleDoubleBooleanDelegate(Int32 key, Double flow, Double variance, Boolean IsValid);

    public delegate void VoidDoubleListDouble(List<Double> valueList, Double Variance);

    public delegate List<String> StringListDelegate();

    public delegate void VoidStringByteArrayDelegate(String pa, Byte[] pb);

    public delegate Boolean BooleanDictionaryStringStringDelegate(Dictionary<String, String> Map);

    public delegate Boolean BooleanStringDelegate(String Map);

    public delegate void VoidInt64Int64Delegate(Int64 Start, Int64 Keep);

    public delegate void VoidSerialPortObjectDelegate(System.IO.Ports.SerialPort SP, Object obj);

    public delegate Boolean BooleanByteArrayOutObjDelegate(Byte[] Data, out object obj);



    public class FileInUseDetection
    {

        [DllImport("kernel32.dll")]
        private static extern IntPtr _lopen(string lpPathName, int iReadWrite);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hObject);

        private static int OF_READWRITE = 2;
        private static int OF_SHARE_DENY_NONE = 0x40;
        private static readonly IntPtr HFILE_ERROR = new IntPtr(-1);
        public static Boolean IsFileInUse(string vFileName)
        {
            IntPtr vHandle = _lopen(vFileName, OF_READWRITE | OF_SHARE_DENY_NONE);
            if (vHandle == HFILE_ERROR)
            {
                Console.WriteLine("文件被其他程序占用");
                return true;
            }
            CloseHandle(vHandle);
            return false;
        }

        public static Boolean IsFileInUse_Ex(string vFileName)
        {
            try
            {
                FileStream fs = new FileStream(vFileName, FileMode.Open);
            }
            catch
            {
                return true;
            }
            return false;
        }
    }


    public class StatisticsCounter
    {
        private Dictionary<String, Int32> TESTStatisticsMap = new Dictionary<string, int>();

        private List<String> TESTStatisticsitems = new List<string>();

        private String Success_Suffix = "_Success";
        private String Failure_Suffix = "_Failure";
        private String Total_Suffix = "_Total";

        public void AddItem(String Key)
        {
            if (!TESTStatisticsitems.Contains(Key))
                TESTStatisticsitems.Add(Key);
        }

        public void Clear()
        {
            TESTStatisticsMap.Clear();
            TESTStatisticsitems.Clear();
        }

        public void AddSuccess(String Key)
        {
            Add(Key + Success_Suffix);
        }

        public void AddFailure(String Key)
        {
            Add(Key + Failure_Suffix);
        }

        public void AddTotal(String Key)
        {
            Add(Key + Total_Suffix);
        }

        public void Add(String Key)
        {
            if (!TESTStatisticsMap.ContainsKey(Key))
                TESTStatisticsMap.Add(Key, 0);
            TESTStatisticsMap[Key]++;
        }

        public Int32 GetCount(String Key)
        {
            if (TESTStatisticsMap.ContainsKey(Key))
                return TESTStatisticsMap[Key];
            return 0;
        }

        public String FormatStatistics()
        {
            StringBuilder Result = new StringBuilder();
            foreach (String Name in TESTStatisticsitems)
            {
                Result.Append(String.Format("[{0}] {2}/{1} = {3:N2}%\n"
                    , Name
                    , GetCount(Name + Total_Suffix)
                    , GetCount(Name + Success_Suffix)
                    //      , GetCount(Name + Total_Suffix) - GetCount(Name + Success_Suffix)
                    , 100.0 * GetCount(Name + Success_Suffix) / GetCount(Name + Total_Suffix)
                    ));
            }
            return Result.ToString();
        }
    }

    /// <summary>
    /// 工具方法
    /// </summary>
    class Tools
    {
        public static int ArrayCompare(byte[] buf1, byte[] buf2, int length)
        {
            for (int i = 0; i < length; i++)
            {
                if (buf1[i] > buf2[i])
                {
                    return 1;
                }
                else if (buf1[i] < buf2[i])
                {
                    return -1;
                }
                else
                {
                    continue;
                }
            }
            return 0;
        }
        public static Byte[] Convert2ByteArray(Int32 Target, Int32 Count)
        {
            Byte[] Ret = new Byte[Count];

            for (int i = 0; i < Count; i++)
            {
                Ret[Count - i - 1] = (Byte)((Target >> (i * 8)) & 0xff);
            }
            return Ret;
        }




        [DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
        public static extern int mciSendString(
         string lpstrCommand,
         string lpstrReturnString,
         int uReturnLength,
         int hwndCallback
        );

        public static void StartRecordSound()
        {
            mciSendString("set wave bitpersample 8", "", 0, 0);

            mciSendString("set wave samplespersec 20000", "", 0, 0);
            mciSendString("set wave channels 2", "", 0, 0);
            mciSendString("set wave format tag pcm", "", 0, 0);
            mciSendString("open new type WAVEAudio alias movie", "", 0, 0);

            mciSendString("record movie", "", 0, 0);
        }

        public static void StopRecordSound()
        {
            mciSendString("stop movie", "", 0, 0);
            mciSendString("save movie 1.wav", "", 0, 0);
            mciSendString("close movie", "", 0, 0);
        }


        public static Boolean WaitForFileUnlocked(String Filename, Int32 MaxWaitMilliSeconds, Int32 CheckInterval = 1000)
        {
            Int32 WaitTime = 0;

            FileInUseDetection.IsFileInUse(Filename);

            while (WaitTime < MaxWaitMilliSeconds
                && !(File.Exists(Filename) && !IsFileInUse(Filename)))
            {

                Console.WriteLine(String.Format("File.Exists = {0},IsFileInUse = {1}", File.Exists(Filename), IsFileInUse(Filename)));

                Thread.Sleep(CheckInterval);
                WaitTime += CheckInterval;
            }

            if (WaitTime >= MaxWaitMilliSeconds
                && !(File.Exists(Filename) && !IsFileInUse(Filename)))
                return false;
            return true;
        }

        public static Boolean IsFileInUse(String Filename)
        {
            return FileInUseDetection.IsFileInUse_Ex(Filename);
        }




        public static void Swap(ref int o1, ref int o2)
        {
            int Temp = o1;
            o2 = o1;
            o1 = Temp;
        }

        public static Boolean TryParseRange(String RangeString, out HashSet<Int32> Output, Int32 Scale = 16, String NumericalSeparator = ",", String RangeSeparator = "-~")
        {
            Output = new HashSet<Int32>();

            String[] SplitArray = RangeString.Split(NumericalSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            Dictionary<Int32, Int32StringDelegate> ParseHandler = new Dictionary<int, Int32StringDelegate>()
            {
                { 16, (String Input) => {
                    return Tools.Hex2ToByte(Input);
                } }
            };
            foreach (String Segment in SplitArray)
            {
                String[] RangeArray = Segment.Split(RangeSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (RangeArray.Length == 1)
                {
                    if (ParseHandler.ContainsKey(Scale))
                        Output.Add(ParseHandler[Scale].Invoke(RangeArray[0]));
                }
                else if (RangeArray.Length == 2)
                {
                    if (ParseHandler.ContainsKey(Scale))
                    {
                        Int32 Start = ParseHandler[Scale].Invoke(RangeArray[0]);
                        Int32 End = ParseHandler[Scale].Invoke(RangeArray[1]);

                        if (Start > End)
                            Swap(ref Start, ref End);

                        for (Int32 i = Start; i <= End; i++)
                            Output.Add(i);
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }



        public static bool TryParseInt(String str, out Int32 Res)
        {
            if (str.Length > 4)
            {
                return int.TryParse(str.Substring(0, 4), out Res);
            }

            return int.TryParse(str, out Res);
        }

        /// <summary>
        /// 数字转 0xXX 形式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String ToHex2Str(Object input)
        {
            return String.Format("0x{0:X2}", Convert.ToInt16(input));
        }

        /// <summary>
        /// 0xXX 转 Byte
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Byte Hex2ToByte(String input)
        {
            if (input.Length >= 4)
            {
                return HexStr2Hex(input.Substring(2, 2))[0];
            }
            return HexStr2Hex(input)[0];
        }

        public static String RandomCnWordWithLengthLimit(Int32 Min, Int32 Max)
        {
            Random rd = new Random(Seed++);
            Int32 WordCount = rd.Next(Min, Max);
            StringBuilder sb = new StringBuilder();
            // CNWordCollection
            for (int i = 0; i < WordCount; i++)
            {
                Int32 Index = rd.Next(0, CNWordCollection.Length - 1);
                sb.Append(CNWordCollection[Index]);
            }
            return sb.ToString();
        }


        public static String RandomEnWordWithLengthLimit(Int32 Min, Int32 Max)
        {
            StringBuilder sb = new StringBuilder();
            while (sb.Length < Min)
            {
                String NewWord = RandomEnWord();
                if (sb.Length + NewWord.Length < Max)
                {
                    sb.Append(NewWord + " ");
                }
            }
            Int32 TryCount = 5;
            while (TryCount-- > 0 && sb.Length < Max)
            {
                String NewWord = RandomEnWord();
                if (sb.Length + NewWord.Length < Max)
                {
                    sb.Append(NewWord + " ");
                }
            }
            return sb.ToString().Substring(0, sb.Length - 2);
        }

        public static String CNWordCollection = "说话间就见老道慢慢地捏了一个手诀分明是有意让李孜省看个清楚然后步罡踏斗念一声太上老君急急如律令疾就见四面冷风猝起遥远的天际之上隐隐约约响起了美妙的仙乐之声伴随着这奇异的乐音就见流云冉冉数十个美丽到了极致的仙子罗带轻拂红袖飘飘隐现于云端高处的天界宫阙漫然飞舞而下随着那曼妙绝伦的轻舞仙界异花漫天飘坠仙子们飘拂而至已经到了触手可及的地步李孜省激动得险些大叫起来正要小心翼翼地伸出手触碰一下仙子的衣袂却不想仙子们突然一个曼妙地旋舞竟然舞上高空向着四面八方分散开来逐渐远去渐而消失李孜省失惊之下大叫起来师傅你怎么不叫她们下来啪的一声怪老道一巴掌拍在李孜省的脑袋上你想什么呢这可是天界的仙子居然想让仙子陪你也不想想自己有没有这个福分哼这些仙子除非是见到人间的龙种像皇帝什么的才会愿意下来和他们交合你算什么东西也敢想入非非原来是这样李孜省如梦方醒";
        public static String ENWordCollection = "monochrome garbage conscious forethought sealed chew sole cultivate vestige preface collate capture obviate violate isolation sufficient phenomenon variation polymorphic safety net problematic simplistic thereby confusion coincidence coincidence realm practical seal peculiar exhibit aardvark trot fluke flipper hooves suckle mammal classification subtly stem prove derive ponder arbitrary overlap solely shrink sparse occasion shuffle shuffling implication fundamental caution intersection comprise multidimensional shallow quirk syntactic pseudorandom comma bizarre plural signify identification guarantee syntactically in summary contrast equality advisable exposed disproportionate overhead base underlie underlying bitwise intuitive respectively bearing compromise confidential truncate elegant suspicious impair injudicious subtle sensible flaw synonyms consistent specialized significance exhaust neatly strewn pile likewise distinct chunk shortcoming apparent fragment negate ascertain inevitably argue garbage reclaim potentially allot collectively restriction notation conundrum nevertheless immaterial parlance encapsulation precede activate wrap time consuming cumbersome approach arrange involve adopt robust evolve mechanisms detect awkward amok manner screwdriver puncture tire compound mimic spot mandatory intervene cascade omit brace multiplicative summarizing definitive ambiguities occasionally refactor sufficiently trapped semicolon variation explicitly stick creep gritty purist emphasize implicitly postfix unary caution propagate associate deduce occurrence esoteric whistle subsequent evaluate associativity square curly brace parenthese precedence demonstrate propagate demonstrate circumstance operands tempt definite applicable monetary arithmetic primitive arithmetic intentionally cue preserve anchor govern obscure extensible interactive equivalence manipulate relate partition comprise assembly longhand intellisense dual ultimate premium minor bullet convention dictate skim oriented migrate scalable parallel constitute arguably variance interoperability incarnation facility famous advent debut anachronism inconsistence refine dotage accustomed manner entail surgery anatomical quizzically mental block mental inspiration author gainfully prose manuscript editor thankless on track tireless single out motivate collaborate fruitful hence invaluable innovation novel inventiveness scope text press announce scheme council sensitivity ferocity myth Edinburgh span cantilever Victorian forth paint fable repeated tensor coroutine finite drain intensity rim velocity hazard bundle awesome hierarchy criteria mesh geometry wireframe hybrid graphically alternatively isometric perspective gizmo gismo uniformly manipulate underneath organism financial bubble radar scatter gallery combo glorious conjunction successor mortgage thine confess supreme currency commodity competence inevitable summon attentively empathetic sweat slide off stride counselor complicated heartache sorrow demand cherish respect decency bucket metabolism choke loose savor breakneck feast vision feverish religious serene primitives splendid probe figure figured vase wood carving heroic plaster generation marble linger bearded gnarled The Winged Victory of Venuses Apollos Homer Rodin Gothic Greek Raphael Leonardo da Vinci Titian Rembrandt Veronese Athenian El Greco warriors Corot Nile charging rhythmic sense frieze Parthenon goddess sculpture unfolded chambers vast procreation shelter artistic urge facet myriad reveal metropolitan fruitful fashion presentation realistic conquer stature roam mastodon carcass gigantic inhabitant condense exhibit museum kaleidoscope pageant glimpse hasty hastier hastiest devote panorama magnificent awe behold thrilling jealous serenity underdog favor frank motives ulterior accuse illogical thorn conduit resonate peace radiate congruence embrace exterior essence warmth emanate thread nominate collaboration gratitude complements coordinators retention impact facilities tailored clinical recognition mutex immutable chroma mastermind coherency load itinerary simultaneous scenario acquisition deliberate fragmentation immersion infidelity throttle thermal confronted dual summarize grand dummy prone sizable endeavor endeavour frontier remainder contaminate weed reap sculptor carve hazy reminder verse minor notably triplet chain insight routine appreciate appropriate expressive mix tear down mockery laptop fake tricky delimited flavor interpret interpreter form spinning orchestrate density distributed vast implication entanglement intensive infrastructure discrete hypervisor isolate prerequisites distract scaling orientation warranty instruction synopsis viability infect factor pragmatic extreme oriented gang assume corrupt jaw borne summons tiles marble moon come off extent alienate disparage idol innocent Scorpio eloquent supervisor slack scale parity declarative leverage homestead vagrant pivot compilation refactor timing attack compromised bypass mandatory toast greenhorn swipe concatenate harness suspended presence accomplish abstraction at a glance glance thermostat monitor peripheral tend supercharge innovation terminology thumb approximate conversion virtually clause aggregate for for conveniently eliminate occupy desirable drift resolution gigantic nasty accommodate interpret monstrous tricky justify peek dispense accurately vow despite leading ascent accent metrics dimension extent scaleable block raster variable pitch fixed pitch pitch govern linefeed carriage surrounding demand comfort examine accumulate deliberate reaction merely clipping redundancy restore preparation ugly passport virtually encompass prompt presence rectangle roundabout on demand demand accumulate explicitly portion discard facility ostensibly dimension stick stick to GDI Graphics Device extensive sophistication eventuality intelligently barely stipulation annoy inert politely lengthy multitasking preemptive reentrant proceed synchronize dispose kinematic interpolate repaint keystroke poll relay detect extract handy trap dedicate bulk acquaint conceptual hurdle fairly inform regarded as stuck identical coordinate maintain erase pardon successive initial thick overlapped shot old habits die distract halt regardless general purpose assume fragment solid stock term composite bitwise clutter character denote legendary notation Hungarian peculiar misnomer respectively abbreviation smoothly transition transparently dimensions direct conversation remainder repositions companion precisely fundamental undoubtedly undoubled telegraph blink central convey mechanism convention messy accustomed analogous anthropomorphic evident engaged routines parenthesized eventually blossom exclusive exclusively manner unified unify sufficient respond outmode dramatically evolved evolve unadorned characterized emphasize urban signage inspired paradigm tablet radical enthusiast hopping frequency hopping frequency bound breadcrumb boost snippet supervision peripheral central cancelled prime cope with cope restrict prior embedded conduct profile facilitates comprise concurrency sufficient arbitrary guidance supervision latency sniff repetitively dedicated dedicate infrared surpass ultimate perform inquiry conjunction facilitate upon registration pave promote cooperative consumption hint indent indentation attach bracket scatter penalty vendors compliant legacy obviate connectivity expertise onset initially dominant restrictions audience paradigm multicast collision simultaneously nutshell retransmission overhead crucial hence chip internals purely scratch overlapped regulations verbatim robust dive strafing stuff dimensional premise illustrate pose idle centimeter demonstrate visually on the fly by wrap wrap yourself a bunch bells and gismo textured textured appeal to technical daily moderate for wizard obsolete tilt ";

        public static Int32 Seed = Convert.ToInt32(DateTime.Now.ToString("fffffff"));

        public static String RandomEnWord()
        {
            String[] Array = ENWordCollection.Split(' ');
            return Array[(new Random(Seed++)).Next(0, Array.Length - 1)];
        }

        public static byte[] CalculateChecksum(Byte[] Data, Int32 StartIndex, Int32 Length)
        {
            Byte[] checksum = new Byte[2];
            int checkData = 0;
            if (Data != null)
                for (int j = StartIndex, i = 0; i < Length; j++, i++)
                {
                    checkData += Data[j];
                }
            checksum[0] = (byte)(0xff & (checkData >> 8));
            checksum[1] = (byte)(0xff & checkData);
            return checksum;
        }

        public static void ChestAddressing()
        {
            Int32 ListenPort = 34567;
            UdpClient client = new UdpClient(new IPEndPoint(IPAddress.Any, ListenPort));
            //  client.JoinMulticastGroup(IPAddress.Parse(BroadCastIP));
            IPEndPoint multicast = new IPEndPoint(IPAddress.Parse("255.255.255.255"), ListenPort);

            Byte[] ChestAddressingReq = { 0xf5, 0x04, 0x03, 0x01, 0x00, 0x08, 0x55 };
            client.Send(ChestAddressingReq, ChestAddressingReq.Length, multicast);

            Thread RespThread = NewThread(() => {
                byte[] buf = null;
                while (null != (buf = client.Receive(ref multicast)))
                {


                    string msg = Encoding.Default.GetString(buf);
                    Console.WriteLine("[R] {0}", msg);
                    break;
                }
                client.Close();
            });

            RespThread.Start();
            NewThread(() => {
                Thread.Sleep(3000);
                if (null != RespThread && RespThread.IsAlive)
                {
                    RespThread.Abort();
                }
            });
        }


        public static void UDPSender()
        {
            UdpClient client = new UdpClient(new IPEndPoint(IPAddress.Any, 0));
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 9999);
            byte[] buf = Encoding.Default.GetBytes("Hello from UDP broadcast");
            //Thread t = new Thread(new ThreadStart(RecvThread));
            //t.IsBackground = true;
            //t.Start();
            client.Send(buf, buf.Length, endpoint);
            //while (true)
            //{
            //    client.Send(buf, buf.Length, endpoint);
            //    Thread.Sleep(1000);
            //}
        }

        public static void RecvThread()
        {
            //      UdpClient client = new UdpClient(new IPEndPoint(IPAddress.Any, 0));
            ////      client.JoinMulticastGroup(IPAddress.Parse("224.0.0.1"));
            //      IPEndPoint multicast = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 9999);
            //      while (true)
            //      {
            //          byte[] buf = client.Receive(ref multicast);
            //          string msg = Encoding.Default.GetString(buf);
            //          Console.WriteLine(msg);
            //      }

            UdpClient client = new UdpClient(new IPEndPoint(IPAddress.Any, 9999));
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                byte[] buf = client.Receive(ref endpoint);
                string msg = Encoding.Default.GetString(buf);
                Console.WriteLine(msg);
            }
        }



        public static Int32 ConvertToInt32(Byte[] Data, Int32 Offset, Int32 Length)
        {
            Int32 Result = 0;
            for (int i = 0; i < Length; i++)
            {
                Result = (Result << 8) + Data[i + Offset];
            }
            return Result;
        }

        public static int HMSStr2MilliSecond(String timeStr)
        {
            // 00:00:00,000
            String[] a = timeStr.Split(new char[] { ':', ',' });
            int s = 0;
            for (int i = 0; i < 3; i++)
            {
                s = s * 60 + int.Parse(a[i]);
            }
            return s * 1000 + int.Parse(a[3]);
        }

        private static UInt16 POLY = 0x1021;

        public static UInt16 PolynomicCRC16(Byte[] Data, Int32 Start, Int32 Length, UInt16 CRC)
        {
            for (int j = 0; j < Length; j++)              /* Step through bytes in memory */
            {
                CRC = (UInt16)(CRC ^ (Data[j] << 8));     /* Fetch byte from memory, XOR into CRC top byte*/
                for (int i = 0; i < 8; i++)             /* Prepare to rotate 8 bits */
                {
                    if ((UInt16)(CRC & 0x8000) > 0)            /* b15 is set... */
                        CRC = (UInt16)((CRC << 1) ^ POLY);    /* rotate and XOR with polynomic */
                    else                          /* b15 is clear... */
                        CRC <<= 1;                  /* just rotate */
                }                             /* Loop for 8 bits */
                CRC &= 0xFFFF;                  /* Ensure CRC remains 16-bit value */
            }                               /* Loop until num=0 */
            return (CRC);                    /* Return updated CRC */
        }



        #region 异步 MessageBox

        public static Int32 MessageBoxAutoClose = 4000;

        /// <summary>
        /// 异步显示 MessageBox
        /// </summary>
        /// <param name="p"></param>
        public static void NonBlockingMsgBox(string p)
        {
            Thread thread1 = new Thread((object obj) => {
                Thread thread = new Thread(new ParameterizedThreadStart(DeleteShowMessage));
                thread.IsBackground = true;
                String tag = "MBX" + Tools.randomStr(3);
                thread.Start(tag);
                MessageBox.Show(obj as string, tag);
            });
            thread1.Start(p);
        }


        // <summary>
        /// 异步显示 MessageBox
        /// </summary>
        /// <param name="p"></param>
        public static void BlockingMsgBox(string p, String caption = "")
        {
            MessageBox.Show(p, caption);
        }


        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public const int WM_CLOSE = 0x10;

        private static void DeleteShowMessage(Object obj)
        {
            Console.WriteLine("Auto Close MessageBox");
            Thread.Sleep(MessageBoxAutoClose);
            //查找MessageBox的弹出窗口,注意MessageBox对应的标题
            IntPtr ptr = FindWindow(null, obj as string);
            if (ptr != IntPtr.Zero)
            {
                Console.WriteLine("Got The MessageBox,Close");
                //查找到窗口则关闭
                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
            else
            {
                Console.WriteLine("Can't find The MessageBox");
            }
        }
        #endregion

        public static Boolean HasThisMessageBox(string obj)
        {
            //查找MessageBox的弹出窗口,注意MessageBox对应的标题
            IntPtr ptr = FindWindow(null, obj);
            if (ptr != IntPtr.Zero)
            {
                Tools.TLog("Got The MessageBox,Close");
                return true;
            }
            Tools.TLog("Can't find The MessageBox");
            return false;
        }




        /// <summary>
        /// 查询首个大于给定值的下标
        /// </summary>
        /// <param name="DataArray"></param>
        /// <param name="Position"></param>
        /// <returns></returns>
        public static Int32 SearchMaxLT(Int64[] DataArray, Int64 Position)
        {
            for (int i = 0; i < DataArray.Length; i++)
            {
                if (DataArray[i] >= Position)
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// 二分法，查询首个大于给定值的下标
        /// </summary>
        /// <param name="DataArray">查询数组</param>
        /// <param name="Position">给定值</param>
        /// <param name="S">起始下标</param>
        /// <param name="E">结束下标</param>
        /// <returns></returns>
        public static Int32 DichotomySearchMaxLT(Int64[] DataArray, Int64 Position, Int32 S, Int32 E)
        {
            if (DataArray.Length == 0)
                return -2;
            if (E == DataArray.Length - 1 && 0 == S)
            {
                if (Position > DataArray[E])
                {
                    // 给定值 大于最大值，返回-1
                    return -1;
                }
                if (Position == DataArray[E])
                {
                    return E;
                }

                if (Position == DataArray[S])
                {
                    return S;
                }
                if (Position < DataArray[S])
                {
                    // 给定值 小于最小值，返回-2
                    return -2;
                }
            }

            Int32 H = (S + E) / 2;

            if (S + 1 == E)
            {
                // 只剩两个
                return E;
            }

            if (DataArray[H] > Position)
            {
                return DichotomySearchMaxLT(DataArray, Position, S, H);
            }
            else if (DataArray[H] < Position)
            {
                return DichotomySearchMaxLT(DataArray, Position, H, E);
            }
            else
            {
                return H;
            }
        }

        public static Int64[] GenerateRandomTimstampArray(Int32 Number, Int32 Seed = 12312)
        {
            Int64 NowTimestamp = 86400;// Tools.GetTimeStamp();
            List<Int64> TimestampList = new List<long>();
            Random rd = new Random(Seed);
            Int64 MaxNumber = 86400;

            for (int i = 0; i < Number; i++)
            {
                TimestampList.Add(NowTimestamp - (rd.Next() % MaxNumber));
            }
            TimestampList.Sort();
            return TimestampList.ToArray();
        }

        public static void PrintArray<T>(T[] Target)
        {
            foreach (T t in Target)
            {
                ConsoleWriteLine(t + " ", 0, false);
            }
            ConsoleWriteLine();
        }

        private static void TextDichotomySearch()
        {
            Int32 ArrayLength = 100;
            Int64[] TArray = GenerateRandomTimstampArray(ArrayLength);
            //    Thread.Sleep(10);
            Int64[] PArray = GenerateRandomTimstampArray(1, 3434);

            Int32 Index1 = SearchMaxLT(TArray, PArray[0]);


            Int32 Index21 = DichotomySearchMaxLT(TArray, PArray[0], 0, ArrayLength - 1);

            if (Index1 != Index21)
            {
                PrintArray<Int64>(TArray);
                ConsoleWriteLine(String.Format("P = {0} I0 = {1} I1 = {2}", PArray[0], Index1, Index21));
            }
        }

        internal static void TestFunction()
        {

            return;
            Byte[] Ins = Tools.HexStr2Hex("f5e11122334455667788990011220102030400041b55");

            Runtime.DataStructrues.ControllerSerialPort csp = new Runtime.DataStructrues.ControllerSerialPort();
            Object obj;
            csp.Validate(Ins, out obj);

            return;
            for (Int32 i = 0; i < 10; i++)
            {
                Console.WriteLine(RandomEnWordWithLengthLimit(5, 25));
            }

            Environment.Exit(0);

            String distFilename = "Text.bmp";
            Bitmap Dist = new Bitmap(44, 44);
            Graphics g = Graphics.FromImage(Dist);
            // 22 135

            Font NFont = new System.Drawing.Font("黑体", 24f);
            SizeF sizeF = g.MeasureString("蔡", NFont);
            // 16.35351 |15.1875
            Console.WriteLine(sizeF.Width + " |" + sizeF.Height);
            //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.DrawRectangle(new Pen(Brushes.White), 0, 0, Dist.Width, Dist.Height);
            g.DrawString("蔡", NFont, Brushes.Black, 0, 0);

            MemoryStream ms = new MemoryStream();
            Dist.Save(ms, ImageFormat.Gif);

            Image gif = Image.FromStream(ms);
            if (File.Exists(distFilename))
                File.Delete(distFilename);
            gif.Save(distFilename, ImageFormat.Bmp);//这里保存的就是8位色深的位图了。
                                                    //   gif.Dispose();
            Dist.Dispose();
            g.Dispose();



            Environment.Exit(0);
        }

        public static Thread NewThread(VoidNoParamDelegate handler)
        {
            Thread thread = new Thread(() =>
            {
                handler.Invoke();
            });
            thread.Start();
            return thread;
        }


        public static void ScanFolderWithCallback(String ScanDirName, VoidStringDelegate Handler = null)
        {
            Queue<String> queue = new Queue<String>();
            queue.Enqueue(ScanDirName);
            while (queue.Count > 0)
            {
                String FolderName = queue.Dequeue();
                foreach (String file in Directory.GetFiles(FolderName))
                {
                    //ConsoleWriteLine(file);
                    if (null != Handler)
                        Handler.Invoke(file);
                }
                foreach (String file in Directory.GetDirectories(FolderName))
                {
                    queue.Enqueue(file);
                }
            }
        }

        public static Int32 LogLevel = 0;

        public static void ConsoleWriteLine(String Message = "", Int32 Level = 0, Boolean WriteLine = true)
        {
            if (Level >= LogLevel)
            {
                if (WriteLine)
                {
                    Console.WriteLine(Message);
                }
                else
                {
                    Console.Write(Message);
                }
            }

        }

        /// <summary>
        /// Aes加解密钥必须32位
        /// </summary>
        public static string AesKey = "asekey32w";
        /// <summary>
        /// 获取Aes32位密钥
        /// </summary>
        /// <param name="key">Aes密钥字符串</param>
        /// <returns>Aes32位密钥</returns>
        static byte[] GetAesKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "Aes密钥不能为空");
            }
            if (key.Length < 32)
            {
                // 不足32补全
                key = key.PadRight(32, '0');
            }
            if (key.Length > 32)
            {
                key = key.Substring(0, 32);
            }
            return Encoding.UTF8.GetBytes(key);
        }
        /// <summary>
        /// Aes加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="key">aes密钥，长度必须32位</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptAes(string source, string key)
        {
            using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
            {
                aesProvider.Key = GetAesKey(key);
                aesProvider.Mode = CipherMode.ECB;
                aesProvider.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor())
                {
                    byte[] inputBuffers = Encoding.UTF8.GetBytes(source);
                    byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    aesProvider.Clear();
                    aesProvider.Dispose();
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }
        /// <summary>
        /// Aes解密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="key">aes密钥，长度必须32位</param>
        /// <returns>解密后的字符串</returns>
        public static string DecryptAes(string source, string key)
        {
            using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
            {
                aesProvider.Key = GetAesKey(key);
                aesProvider.Mode = CipherMode.ECB;
                aesProvider.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor())
                {
                    byte[] inputBuffers = Convert.FromBase64String(source);
                    byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    aesProvider.Clear();
                    return Encoding.UTF8.GetString(results);
                }
            }
        }



        //body是要传递的参数,格式"roleId=1&uid=2"
        //post的cotentType填写:
        //"application/x-www-form-urlencoded"
        //soap填写:"text/xml; charset=utf-8"
        public static string HttpPost(string url, Dictionary<String, String> Params, string contentType = "application/x-www-form-urlencoded")
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.ContentType = contentType;
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 20000;
            //    httpWebRequest.t
            String body = "";
            foreach (KeyValuePair<String, String> kv in Params)
            {
                body += String.Format("&{0}={1}", kv.Key, kv.Value);
            }
            body = body.Substring(1);
            byte[] btBodys = Encoding.UTF8.GetBytes(body);
            httpWebRequest.ContentLength = btBodys.Length;
            httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();

            httpWebResponse.Close();
            streamReader.Close();
            httpWebRequest.Abort();
            httpWebResponse.Close();
            return responseContent;
        }




        public static string GetRemoteIP()
        {
            string tempip = "";
            System.Net.WebRequest request = System.Net.WebRequest.Create("http://ip.qq.com/");
            request.Timeout = 10000;
            System.Net.WebResponse response = request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(resStream, System.Text.Encoding.Default);
            string htmlinfo = sr.ReadToEnd();
            //匹配IP的正则表达式
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("((25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]\\d|\\d)\\.){3}(25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]\\d|[1-9])", System.Text.RegularExpressions.RegexOptions.None);
            System.Text.RegularExpressions.Match mc = r.Match(htmlinfo);
            //获取匹配到的IP
            tempip = mc.Groups[0].Value;

            resStream.Close();
            sr.Close();
            return tempip;
        }
        public static string GetMacString()
        {
            string strMac = "";
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in interfaces)
            {

                //Console.WriteLine(String.Format(
                //    "{0} | {1} | {2} | {3} | {4} | {5}", 
                //    ni.Name,
                //    ni.Description,
                //    ni.Id,
                //    ni.GetPhysicalAddress().ToString(),
                //    ni.NetworkInterfaceType.ToString(),
                //    ni.OperationalStatus.ToString()
                //    ));

                if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    ni.OperationalStatus == OperationalStatus.Up)
                {

                    if (ni.Description.StartsWith("VMware")
                        || ni.Description.StartsWith("Bluetooth"))
                    {
                        continue;
                    }
                    IPInterfaceProperties iii = ni.GetIPProperties();
                    //Bluetooth 设备(个人区域网) #6 
                    //VMware Virtual Ethernet Adapter for VMnet8
                    //Realtek PCIe GBE Family Controller
                    strMac += ni.GetPhysicalAddress().ToString() + "|";
                }
            }
            if (String.IsNullOrEmpty(strMac.Trim('|')))
            {
                strMac = "";
                foreach (NetworkInterface ni in interfaces)
                {
                    if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                        ni.OperationalStatus == OperationalStatus.Up)
                    {
                        strMac += ni.GetPhysicalAddress().ToString() + "|";
                    }
                }
            }
            return strMac.Trim('|');
        }

        /// <summary>  
        /// 获取当前使用的IP  
        /// </summary>  
        /// <returns></returns>  
        public static string GetLocalIPAddress()
        {
            try
            {
                System.Net.Sockets.TcpClient c = new System.Net.Sockets.TcpClient();
                c.Connect("www.baidu.com", 80);
                string ip = ((System.Net.IPEndPoint)c.Client.LocalEndPoint).Address.ToString();
                c.Close();
                return ip;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>  
        /// 获取本机主DNS  
        /// </summary>  
        /// <returns></returns>  
        public static string GetPrimaryDNS()
        {
            string result = RunApp("nslookup", "", true);
            Match m = Regex.Match(result, @"\d+\.\d+\.\d+\.\d+");
            if (m.Success)
            {
                return m.Value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>  
        /// 运行一个控制台程序并返回其输出参数。  
        /// </summary>  
        /// <param name="filename">程序名</param>  
        /// <param name="arguments">输入参数</param>  
        /// <returns></returns>  
        public static string RunApp(string filename, string arguments, bool recordLog)
        {
            try
            {
                if (recordLog)
                {
                    Trace.WriteLine(filename + " " + arguments);
                }
                Process proc = new Process();
                proc.StartInfo.FileName = filename;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.Arguments = arguments;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();

                using (System.IO.StreamReader sr = new System.IO.StreamReader(proc.StandardOutput.BaseStream, Encoding.Default))
                {
                    //string txt = sr.ReadToEnd();  
                    //sr.Close();  
                    //if (recordLog)  
                    //{  
                    //    Trace.WriteLine(txt);  
                    //}  
                    //if (!proc.HasExited)  
                    //{  
                    //    proc.Kill();  
                    //}  
                    //上面标记的是原文，下面是我自己调试错误后自行修改的  
                    Thread.Sleep(100);           //貌似调用系统的nslookup还未返回数据或者数据未编码完成，程序就已经跳过直接执行  
                                                 //txt = sr.ReadToEnd()了，导致返回的数据为空，故睡眠令硬件反应  
                    if (!proc.HasExited)         //在无参数调用nslookup后，可以继续输入命令继续操作，如果进程未停止就直接执行  
                    {                            //txt = sr.ReadToEnd()程序就在等待输入，而且又无法输入，直接掐住无法继续运行  
                        proc.Kill();
                    }
                    string txt = sr.ReadToEnd();
                    sr.Close();
                    if (recordLog)
                        Trace.WriteLine(txt);
                    return txt;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return ex.Message;
            }
        }


        public static void KillRunningInstance(String ProcessName)
        {
            Process[] Processes = Process.GetProcessesByName(ProcessName);
            foreach (Process process in Processes)
            {
                process.Kill();
            }
        }
        public static void MkdirForFile(String FileNameWithPath, String BasePath)
        {
            String[] PathArray = FileNameWithPath.Split(new Char[] { '\\', '/' });
            String PathName = BasePath;

            for (int i = 0; i < PathArray.Length - 1; i++)
            {
                if (!Directory.Exists((PathName = CompoundPath(PathName, PathArray[i]))))
                {
                    Console.WriteLine("mk " + PathName);
                    Directory.CreateDirectory(PathName);
                }
            }
        }

        public static String CompoundURL(params String[] Array)
        {
            String Result = Array[0];
            for (int i = 1; i < Array.Length; i++)
            {
                Result = Result.TrimEnd('/', '\\') + "/" + Array[i].TrimStart('/');
            }
            return Result;
        }

        public static String CompoundPath(params String[] Array)
        {
            return CompoundURL(Array).Replace("/", "\\");
        }

        /// <summary> 
        /// 转换字节大小 
        /// </summary> 
        /// <param name="byteSize">输入字节数</param> 
        /// <returns>返回值</returns> 
        public static string ConvertSize(long byteSize)
        {
            string str = "";
            float tempf = (float)byteSize;
            if (tempf / 1024 > 1)
            {
                if ((tempf / 1024) / 1024 > 1)
                {
                    str = ((tempf / 1024) / 1024).ToString("##0.00", CultureInfo.InvariantCulture) + "MB";
                }
                else
                {
                    str = (tempf / 1024).ToString("##0.00", CultureInfo.InvariantCulture) + "KB";
                }
            }
            else
            {
                str = tempf.ToString(CultureInfo.InvariantCulture) + "B";
            }
            return str;
        }
        public static void PrintNow()
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff"));
        }

        public static String FormatDatetime(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
        }


        public static void PrintStringArray(String[] Array)
        {
            for (int i = 0; i < Array.Length; i++)
            {

                Console.WriteLine(Array[i]);
            }
        }

        public static T DESerializer<T>(string strXML) where T : class
        {
            try
            {
                using (StringReader sr = new StringReader(strXML))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(sr) as T;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public static string XmlSerialize<T>(T obj)
        {
            using (StringWriter sw = new StringWriter())
            {
                Type t = obj.GetType();
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(sw, obj);
                sw.Close();
                return sw.ToString();
            }
        }

        public static String FormatDataSource(String filename)
        {
            return String.Format("Data Source={0};Version=3;", filename);
        }

        public static void PrintDataTable(DataTable dt)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Console.WriteLine(string.Format("{0} = {1}", dt.Columns[j].ColumnName, dt.Rows[i][j].ToString()));
                }
                Console.WriteLine();
            }
        }

        public static Double Pix2MM(Int32 Pix, Int32 DPI = 200)
        {
            Double Scale = 2.54; // 1 英寸＝2.54 厘米
            return Scale * (1.0 * Pix / DPI);
        }

        [DllImport("gdi32.dll")]
        public static extern int SetTextCharacterExtra(IntPtr hdc, int nCharExtra);


        
        
        /// <summary>
        /// 转换8位色深的位图
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="distFilename"></param>
        public static void FormatTo8bpp(String filename, string distFilename)
        {
            Bitmap bmp = (Bitmap)Bitmap.FromFile(filename);
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Gif);
            Image gif = Image.FromStream(ms);
            gif.Save(distFilename, ImageFormat.Bmp);//这里保存的就是8位色深的位图了。
        }


        public static bool file_put_content(String filename, String content)
        {

            FileStream fs = new FileStream(filename, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                sw.Write(content);
                sw.Flush();
                return true;
            }
            catch (Exception ex)
            {
                ConsoleWriteLine(ex.Message.ToString());
                return false;
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }

        public static String GetHostName()
        {
            return Dns.GetHostName();
        }

        public static string GetIpAddress()
        {
            string hostName = Dns.GetHostName();   //获取本机名
            IPHostEntry localhost = Dns.GetHostEntry(hostName);    //方法已过期，可以获取IPv4的地址
                                                                   //IPHostEntry localhost = Dns.GetHostEntry(hostName);   //获取IPv6地址
            IPAddress localaddr = localhost.AddressList[0];
            foreach (IPAddress addr in localhost.AddressList)
            {
                Console.WriteLine(addr.ToString());
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                    Console.WriteLine(addr.ToString());
            }

            return localaddr.ToString();
        }


        public static Double Round(Double Target, Int32 Decimals)
        {

            return Math.Round(Target, Decimals);
        }

        public static Boolean ValidMinMax(Double target, String Constraint)
        {
            Double Min, Max;
            AnalysisMinMax(Constraint, out Min, out Max);
            if (Min > 0 && target < Min)
                return false;
            if (Max > 0 && target > Max)
                return false;
            return true;
        }

        public static Boolean ValidMinMaxWithSpecifyIndex(Double target, String Constraint, Int32 Index)
        {
            String[] ConfigArray = Constraint.Split(',');
            if (Index >= ConfigArray.Length)
            {
                return ValidMinMax(target, ConfigArray[ConfigArray.Length - 1]);
            }
            return ValidMinMax(target, ConfigArray[Index]);
        }

        public static Boolean AnalysisMinMax(String configString, out Double Min, out Double Max)
        {
            Min = -1;
            Max = -1;
            // 800
            // 0.05-0.06
            // >0.06
            if (!String.IsNullOrEmpty(configString) && !String.IsNullOrWhiteSpace(configString))
            {
                configString = configString.Trim().Replace(" ", "");
                String[] SplitArray = configString.Split(new Char[] { '-', '~', '>' });
                try
                {
                    if (SplitArray.Length == 1)
                    {
                        Min = Convert.ToDouble(SplitArray[0]);
                    }
                    else if (SplitArray.Length == 2)
                    {
                        if (SplitArray[0] == "")
                        {
                            Max = Convert.ToDouble(SplitArray[1]);
                        }
                        else
                        {
                            Min = Convert.ToDouble(SplitArray[0]);
                            Max = Convert.ToDouble(SplitArray[1]);
                        }
                    }
                    return true;
                }
                catch
                {

                }
                return false;
            }
            return false;

        }



        public static String TLog(String msg)
        {
            String FMsg = string.Format("[{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff"), msg);
            Console.WriteLine(FMsg);
            return FMsg;
        }
        public static void TLog(String format, params object[] param)
        {
            Console.WriteLine("[{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff"), String.Format(format, param));
        }
        

        /// <summary>
        /// 计算方差
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Double CalculateVariance(Double[] list)
        {
            Double Avg = 0;
            Int32 Count = list.Length;
            foreach (Double d in list)
            {
                Avg += d;
            }
            Avg = Avg / Count;
            Double Variance = 0;
            foreach (Double d in list)
            {
                Variance += (d - Avg) * (d - Avg);
            }
            Variance /= (Count - 1);
            Variance = Math.Sqrt(Variance);
            return Variance;
        }


        /// <summary>
        /// 计算方差
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Double CalculateVarianceInt64(Int64[] list)
        {
            Double Avg = 0;
            Int32 Count = list.Length;
            foreach (Int64 d in list)
            {
                Avg += d;
            }
            Avg = Avg / Count;
            Double Variance = 0;
            foreach (Int64 d in list)
            {
                Variance += (d - Avg) * (d - Avg);
            }
            Variance /= 1.0 * (Count - 1);
            Variance = Math.Sqrt(Variance);
            return Variance;
        }


        public delegate Double GetDoubleDelegate(Object input);



        public static String ObjectList2String(List<Double> list)
        {
            String Result = "";
            if (list.Count == 0)
                return "[]";
            foreach (Object obj in list)
            {
                Result = Result + ", " + obj.ToString();
            }
            return "[" + Result.Substring(2) + "]";
        }

        /// <summary>
        /// 计算方差
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Double CalculateVariance(List<Object> list, GetDoubleDelegate handler)
        {
            Double Avg = 0;
            Int32 Count = list.Count;
            foreach (Object d in list)
            {
                Avg += handler.Invoke(d);
            }
            Avg = Avg / Count;
            Double Variance = 0;
            foreach (Object obj in list)
            {
                Double d = handler.Invoke(obj);
                Variance += (d - Avg) * (d - Avg);
            }
            Variance /= (Count - 1);
            Variance = Math.Sqrt(Variance);
            return Variance;
        }




        public static List<Double> ConvertToDoubleList(List<Object> list, GetDoubleDelegate handler)
        {
            List<Double> result = new List<Double>();
            foreach (Object d in list)
            {
                result.Add(handler.Invoke(d));
            }
            return result;
        }

        public static Double CalMinVariance(List<Double> list, Double MinVariance, Int32 MinSimpleNum)
        {
            Double Avg = 0;
            Double Sum = 0;
            Int32 Count = list.Count;
            foreach (Double d in list)
            {
                Sum += d;
            }
            Avg = Sum / Count;
            Double Variance = 0;
            foreach (Double d in list)
            {
                Variance += (d - Avg) * (d - Avg);
            }
            Variance /= (Count - 1);
            Variance = Math.Sqrt(Variance);

            Double CurrentVariance = Variance;

            if (CurrentVariance > MinVariance)
            {
                Int32 RemovIndex = 0;
                //       Int32 i = 0;
                while (list.Count > 0 && CurrentVariance > MinVariance)
                {
                    if (list[0] > list[Count - 1])
                    {
                        RemovIndex = Count - 1;
                    }
                    else
                    {
                        RemovIndex = 0;
                    }

                    Sum -= list[RemovIndex];
                    Avg = Sum / --Count;
                    list.RemoveAt(RemovIndex);
                    Variance = 0;
                    foreach (Double d in list)
                    {
                        Variance += (d - Avg) * (d - Avg);
                    }
                    Variance /= (Count - 1);
                    Variance = Math.Sqrt(Variance);
                    CurrentVariance = Variance;
                    //     RemovIndex = (i++ % 2) * (Count - 1);
                }
            }

            if (MinSimpleNum > list.Count)
            {
                // 样本数量不足
                return -1;
            }
            return CurrentVariance;
        }

        


        public static String Byte2Str(Byte[] array, int start = 0, int length = 0)
        {
            String result = "";
            if (length <= 0)
                length = array.Length;
            for (int i = start; i < length; i++)
            {
                String ch = Char.ConvertFromUtf32((int)array[i]);
                String a = Convert.ToString(array[i], 10);
                result += ch;
            }
            return result;
        }

        /// <summary>
        /// 带定时检测停止的定时器
        /// </summary>
        /// <param name="MilliSeconds"></param>
        /// <param name="CheckStopIntervalInMilliSecond"></param>
        /// <param name="CheckStopSignal"></param>
        /// <returns></returns>
        public static Boolean SleepWithStopSignal(Int32 MilliSeconds, Int32 CheckStopIntervalInMilliSecond, BooleanNoParamDelegate CheckStopSignal)
        {
            Int32 Seconds = MilliSeconds / CheckStopIntervalInMilliSecond;
            Int64 MissionStartTimeStamp = Tools.GetTimeStamp();
            Int64 ExpectedTimeLine = MissionStartTimeStamp;
            Int32 OffSet = 0;
            for (int i = 0; i < Seconds; i++)
            {
                if (CheckStopIntervalInMilliSecond > OffSet)
                    Thread.Sleep(CheckStopIntervalInMilliSecond - OffSet);
                ExpectedTimeLine += CheckStopIntervalInMilliSecond;
                Int64 NewTimeStamp = Tools.GetTimeStamp();
                OffSet = Convert.ToInt32(NewTimeStamp - ExpectedTimeLine);
                if (CheckStopSignal.Invoke())
                {
                    return true;
                }
            }
            Thread.Sleep(MilliSeconds % CheckStopIntervalInMilliSecond);
            return false;
        }

        public static DateTime UnixZero = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        public static Int64 GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - UnixZero;
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
        public static Int64 TimeDistance(DateTime dt1, DateTime dt2)
        {
            TimeSpan ts = dt1 - dt2;
            return Convert.ToInt64(ts.TotalMilliseconds);
        }


        public static DateTime FromUnixTime(Int64 unixTime)
        {
            if (unixTime > 0)
            {
                DateTime nDt = UnixZero.AddMilliseconds(unixTime + 28800000);
                return new DateTime(nDt.Year, nDt.Month, nDt.Day, nDt.Hour, nDt.Minute, nDt.Second, nDt.Millisecond);
            }
            return UnixZero;
        }

        public static long ToUnixTime(DateTime date)
        {
            return Convert.ToInt64((date.ToUniversalTime() - UnixZero).TotalMilliseconds);
        }

        public static String Int2HMS(int i)
        {
            int ss = i % 60;
            i = i / 60;
            int mm = i % 60;
            i = i / 60;
            int hh = i % 60;
            return String.Format("{0:D2}:{1:D2}:{2:D2}", hh, mm, ss);
        }


        public static String Int2HMSF(int i)
        {
            int ff = i % 1000;
            i = i / 1000;
            int ss = i % 60;
            i = i / 60;
            int mm = i % 60;
            i = i / 60;
            int hh = i % 60;
            return String.Format("{0:D2}:{1:D2}:{2:D2},{3:D3}", hh, mm, ss, ff);
        }



        /// <summary>  
        /// 根据属性获取对应的属性名称  
        /// </summary>  
        /// <typeparam name="T">对象类型</typeparam>  
        /// <typeparam name="K">对象数据的类型</typeparam>  
        /// <param name="t">对象</param>  
        /// <param name="expr">需要获取的属性</param>  
        /// <returns>属性名</returns>  
        public static string GetPropertyName<T, K>(T t, System.Linq.Expressions.Expression<Func<T, K>> expr)
        {
            string propertyName = string.Empty;    //返回的属性名  
                                                   //对象是不是一元运算符  
            if (expr.Body is System.Linq.Expressions.UnaryExpression)
            {
                propertyName = ((System.Linq.Expressions.MemberExpression)((System.Linq.Expressions.UnaryExpression)expr.Body).Operand).Member.Name;
            }
            //对象是不是访问的字段或属性  
            else if (expr.Body is System.Linq.Expressions.MemberExpression)
            {
                propertyName = ((System.Linq.Expressions.MemberExpression)expr.Body).Member.Name;
            }
            //对象是不是参数表达式  
            else if (expr.Body is System.Linq.Expressions.ParameterExpression)
            {
                propertyName = ((System.Linq.Expressions.ParameterExpression)expr.Body).Type.Name;
            }
            return propertyName;
        }

        // 关闭64位（文件系统）的操作转向
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);
        // 开启64位（文件系统）的操作转向
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        public static Process kbpr;

        public static void CloseKbd()
        {
            if ((kbpr != null) && (!kbpr.HasExited))
            {
                kbpr.Kill();
            }
        }

        public static void InvokeKbd()
        {
            if ((kbpr == null) || (kbpr.HasExited))
            {
                /* 
                 * 直接调用osk.exe会提示无法调用屏幕键盘
                 * 原因是在64位的Windows操作系统中，为了兼容32位程序的运行,64位的Windows操作系统采用重定向机制。
                 * 所以需要禁止系统的重定向
                 */
                IntPtr oldWOW64State = new IntPtr();
                bool bRet = Wow64DisableWow64FsRedirection(ref oldWOW64State);

                kbpr = System.Diagnostics.Process.Start("osk.exe");

                if (bRet)
                {
                    Wow64RevertWow64FsRedirection(oldWOW64State);
                }
            }
        }



        public static int[] CellID2Point(String cellID)
        {
            Char[] target = cellID.ToUpper().ToCharArray();
            int i = 0, startX = 0, startY = 0;
            for (; i < target.Length; i++)
            {
                if (target[i] >= '0' && target[i] <= '9')
                {
                    startX = startX * 10 + (target[i] - '0');
                }
                else
                    break;
            }
            for (; i < target.Length; i++)
            {
                if (target[i] >= 'A' && target[i] <= 'Z')
                {
                    startY = startY * 26 + (target[i] - 'A' + 1);
                }
                else
                    break;
            }
            return new int[] { startX, startY };
        }


        public static void RenderToExcel(DataGridView dgv, String distFilename)
        {
            FileStream fs = new FileStream(distFilename, FileMode.CreateNew);
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();
            IRow headerRow = sheet.CreateRow(0);

            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                // dgv.Columns[i].HeaderText
                headerRow.CreateCell(i).SetCellValue(dgv.Columns[i].HeaderText);//If Caption not set, returns the ColumnName value
                Console.Write(" i = " + i + " " + dgv.Columns[i].HeaderText);
            }
            Console.WriteLine();




            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                IRow dataRow = sheet.CreateRow(i + 1);
                for (int j = 0; j < dgv.Rows[i].Cells.Count; j++)
                {
                    if (null != dgv.Rows[i].Cells[j].Value)
                    {

                        dataRow.CreateCell(j).SetCellValue(dgv.Rows[i].Cells[j].Value.ToString());
                        Console.Write(" j = " + j + " " + dgv.Rows[i].Cells[j].Value.ToString());
                    }
                }
                Console.WriteLine();
            }

            workbook.Write(fs);
        }

        public static MemoryStream RenderToExcel(DataTable table, String distFilename)
        {
            FileStream fs = new FileStream(distFilename, FileMode.CreateNew);
            MemoryStream ms = new MemoryStream();
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();
            IRow headerRow = sheet.CreateRow(0);

            // handling header.
            foreach (DataColumn column in table.Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);//If Caption not set, returns the ColumnName value

            // handling value.
            int rowIndex = 1;

            foreach (DataRow row in table.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);

                foreach (DataColumn column in table.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }

                rowIndex++;
            }

            workbook.Write(fs);
            ms.Flush();
            ms.Position = 0;


            return ms;
        }


        public delegate Boolean ProcessExcelLineDelegate(String[] line);


        /// <summary>
        /// 从 excel 中导入设备数据
        /// </summary>
        /// <param name="excelName">文件全路径</param>
        /// <param name="startCell">起始单元格（1A，2B，如此表示）</param>
        /// <param name="columnCount">取的列长</param>
        public static List<String[]> LoadExcel(string excelName, String startCell, int columnCount, ProcessExcelLineDelegate processor = null)
        {
            if (!File.Exists(excelName))
            {
                return null;
            }
            List<String[]> result = new List<string[]>();

            IWorkbook sd = new XSSFWorkbook(excelName);
            ISheet sheet = sd.GetSheetAt(0);
            IEnumerator a = sheet.GetRowEnumerator();
            int[] startPoint = CellID2Point(startCell);
            while (a.MoveNext())
            {
                IRow rowi = (IRow)a.Current;
                String[] rowLine = new string[2];
                for (int i = 0; i < rowi.Cells.Count; i++)
                {
                    if (rowi.RowNum < startPoint[0] - 1)
                        continue;
                    if (rowi.Cells[i].ColumnIndex < startPoint[1] - 1)
                        continue;
                    if (rowi.Cells[i].ColumnIndex >= startPoint[1] + columnCount - 1)
                        continue;

                    rowLine[rowi.Cells[i].ColumnIndex + 1 - startPoint[1]] = rowi.Cells[i].ToString();
                    if (null != processor)
                    {
                        if (!processor.Invoke(rowLine))
                        {
                            continue;
                        }
                    }
                }
                result.Add(rowLine);
            }
            return result;
        }
        /// <summary>
        /// Get File Last Modify Time (Local Time)
        /// </summary>
        /// <param name="name"></param>
        public static DateTime GetFileLastMTime(String name)
        {
            FileInfo di = new FileInfo(name);
            if (!di.Exists)
            {

            }
            return di.LastWriteTime;
        }
        /// <summary>
        /// Get Directory Last Modify Time (Local Time)
        /// </summary>
        /// <param name="name"></param>
        public static DateTime GetDirLastMTime(String name)
        {
            DirectoryInfo di = new DirectoryInfo(name);
            if (!di.Exists)
            {

            }
            // access time 对于 目录来说是访问里面文件的时间
            // 更改目录下的文件，at 和 wt 都会更新
            //Console.WriteLine("di.LastWriteTime = " + di.LastWriteTime);
            //Console.WriteLine("di.LastAccessTime = " + di.LastAccessTime);
            return di.LastWriteTime;
        }

        public static String GetSHA1(string s)
        {
            try
            {

                FileStream file = new FileStream(s, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] retval = sha1.ComputeHash(file);
                file.Close();

                StringBuilder sc = new StringBuilder();
                for (int i = 0; i < retval.Length; i++)
                {
                    sc.Append(retval[i].ToString("x2"));
                }
                Console.WriteLine("文件SHA1：{0}", sc);
                return sc.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        /// <summary>
        /// 获取文件内容
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static String file_get_content(String filename)
        {
            if (!File.Exists(filename)) return "";
            StreamReader sr = new StreamReader(filename, Encoding.UTF8);
            String line;
            String all = "";
            while ((line = sr.ReadLine()) != null)
            {
                all += line + "\n";
            }
            sr.Close();
            sr.Dispose();
            return all;
        }


        public static String FileErrorLog(String msg)
        {
            String logDir = "Logs";
            if (false == Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }
            String now = DateTime.Now.ToString();
            String logfilename = logDir + "/" + now.Split(' ')[0].Replace('/', '-') + ".err.log";

            FileStream fs = new FileStream(logfilename, FileMode.Create | FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            sw.Write(msg);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
            return logfilename;
        }


        public static void FileLog(String msg)
        {
            String logDir = "Logs";
            if (false == Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }
            String now = DateTime.Now.ToString();
            String logfilename = logDir + "/" + now.Split(' ')[0].Replace('/', '-') + ".log";

            FileStream fs = new FileStream(logfilename, FileMode.Create | FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine("[" + now + "] " + msg);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }


        public static String ByteArray2HexStr(Byte[] array, int start = 0, int length = 0)
        {
            String result = "";
            if (length <= 0)
                length = array.Length;
            for (int i = start; i < start + length; i++)
            {
                String a = Convert.ToString(array[i], 16);
                if (a.Length < 2)
                {
                    a = "0" + a;
                }
                result += a;
            }
            return result.ToUpper();
        }


        /// <summary>
        /// 打印 Byte 数组
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static String PrintByteArray(Byte[] array, Boolean isConsole = true)
        {
            String result = "";
            if (null == array)
            {
                return "";
            }
            for (int i = 0; i < array.Length; i++)
            {
                String a = Convert.ToString(array[i], 16);
                if (a.Length < 2)
                {
                    a = "0" + a;
                }
                result += a;
                if (1 == i % 2)
                {
                    result += " ";
                }
            }
            if (isConsole)
                Console.WriteLine(result);
            return result;
        }


        public static Boolean ByteArrayEqual(Byte[] arr1, Byte[] arr2)
        {
            if (arr1 != null && arr2 != null && arr1.Length == arr2.Length)
            {
                for (int i = 0; i < arr1.Length; i++)
                {
                    if (arr1[i] != arr2[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public static String randomStr(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            String collections = "1234567890QWERTYUIOPASDFGHJKLZXCVBNMqwertyuioplkmjnhbgvfcdxsza";
            Char[] collectionsArray = collections.ToCharArray();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = collectionsArray[Convert.ToInt32(Math.Floor(collectionsArray.Length * random.NextDouble()))];

                builder.Append(ch);
            }
            return builder.ToString();
        }

        public static Byte[] HexStr2Hex(string hexString)
        {
            hexString = hexString.ToLower().Trim();
            if (hexString.Length < 2)
                hexString = "0" + hexString;
            if (hexString.Length % 2 == 1)
                hexString = hexString.Substring(0, hexString.Length - 1);
            Char[] hexChar = hexString.ToCharArray();
            Byte[] byteArray = new Byte[hexString.Length / 2];
            for (int i = 0; i < hexChar.Length; i += 2)
            {
                int left = 0, right = 0;
                if (hexChar[i] > '9')
                    left = hexChar[i] - 'a' + 10;
                else
                    left = hexChar[i] - '0';
                if (hexChar[i + 1] > '9')
                    right = hexChar[i + 1] - 'a' + 10;
                else
                    right = hexChar[i + 1] - '0';
                byteArray[i / 2] = Convert.ToByte(left * 16 + right);
            }
            return byteArray;
        }
    }
}
