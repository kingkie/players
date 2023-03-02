using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BroadcastPlayer
{
    class Define
    {



    }

    /// <summary>
    /// 气味脚本的获取和处理类型
    /// </summary>
    class ScriptSchedule
    {
       

        public String second2hmsStr(int second)
        {
            int H = second / 3600;
            int m = (second / 60) % 60;
            int s = second % 60;
            return String.Format("{0:D2}:{1:D2}:{2:D2}", H, m, s);
        }

        public String second2hmsStr(Int64 millisecond)
        {
            int second = Convert.ToInt32(millisecond / 1000);
            int H = second / 3600;
            int m = (second / 60) % 60;
            int s = second % 60;
            return String.Format("{0:D2}:{1:D2}:{2:D2},{3:D3}", H, m, s, millisecond % 1000);
        }

        public static Int64 DecimalString2MilliSecond(String str)
        {

            String[] s = str.Split('.');
            if (s.Length == 2)
            {

                if (s[1].Length == 1)
                {
                    s[1] = s[1] + "00";
                }
                if (s[1].Length == 2)
                {
                    s[1] = s[1] + "0";
                }

                return Convert.ToInt64(s[0]) * 1000 + Convert.ToInt64(s[1]);
            }
            return Convert.ToInt64(s[0]) * 1000;
        }


        

        public string convertSn2eightbit(string sn)
        {
            return String.Format("{0:D8}", convertSn2int(sn));
        }


        public int convertSn2int(String sn)
        {
            int s = 0, s1 = 0, ic = 0;
            char[] snArray = sn.ToCharArray();

            for (int i = sn.Length - 1; i >= 0; i--)
            {
                if (snArray[i] >= '0' && snArray[i] <= '9')
                {
                    ic++;
                    s1 = s;
                    s = s + (snArray[i] - '0') * (int)(Math.Pow(10, ic - 1));
                    if (s > 100)
                    {
                        return s1;
                    }
                }
            }
            return s;
        }


        public int str2second(String timeStr)
        {
            // 00:00:00,000
            String[] a = timeStr.Split(new char[] { ':', ',' });
            int s = 0;
            for (int i = 0; i < 3; i++)
            {
                s = s * 60 + int.Parse(a[i]);
            }
            return s;
        }

        public int str2milliecond(String timeStr)
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

        public ScheduleSimple convertSrt2ScheduleSimple(String filename)
        {
            if (false == File.Exists(filename))
            {
                return null;
            }
            ScheduleSimple simple = new ScheduleSimple();

            StreamReader sr = new StreamReader(filename, Encoding.UTF8);
            String line;

            List<String> lineList = new List<String>();

            while ((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                if ("" != line)
                    lineList.Add(line);
                //Console.WriteLine(line);
            }

            String[] lineArray = lineList.ToArray();

            Regex regex0 = new Regex(@"^\d+$");
            Regex regex1 = new Regex(@"\d{2}:\d{2}:\d{2},\d{1,3}");
            //Regex regex2 = new Regex(@"^[\w\;\&.\s]*(\#\!\#)?[\w]+$");

            int index = 0, sSum = 0;

            List<ScheduleDataSimple> dataList = new List<ScheduleDataSimple>();
            // sort 
            for (int i = 2; i < lineArray.Length; i += 3)
            {
                String match0 = regex0.Match(lineArray[i - 2]).ToString();
                MatchCollection mc = regex1.Matches(lineArray[i - 1]);
                //      String match2 = regex2.Match(lineArray[i]).ToString();
                String[] splitArray = Regex.Split(lineArray[i], "#!#");
                //Console.WriteLine("match0 = " + match0 + " match1 = " + mc[0].Value + " match2 = " + match2);
                if (match0 != "" && mc.Count == 2 && splitArray.Length == 2)
                {
                    int start = str2milliecond(mc[0].Value), end = str2milliecond(mc[1].Value);
                    ScheduleDataSimple sDs = new ScheduleDataSimple();
                    sDs.index = index + 1;
                    sDs.start = start;
                    sDs.duration = end - start;
                    sDs.smellID = convertSn2int(splitArray[1]);

                    Console.WriteLine(String.Format("index = {0},start = {1},duration = {2},smellID = {3}"
                        , sDs.index
                        , sDs.start
                        , sDs.duration
                        , sDs.smellID));

                    dataList.Add(sDs);
                    if (end > sSum)
                    {
                        sSum = end;
                    }
                    index++;
                }
            }
            simple.data = dataList.ToArray();
            simple.instructionNum = index;
            simple.scriptDuration = sSum;
            return simple;
        }
        
    }
    
    /// <summary>
    /// 内部使用的 脚本指令 结构
    /// </summary>
    class ScheduleDataSimple
    {
        public int index;
        public int start;
        public int duration;
        public int smellID;
    }

    /// <summary>
    /// 内部使用的 脚本结构
    /// </summary>
    class ScheduleSimple
    {
        public int scriptIndex;
        public int scriptDuration;
        public int instructionNum;
        public ScheduleDataSimple[] data;
    }

}
