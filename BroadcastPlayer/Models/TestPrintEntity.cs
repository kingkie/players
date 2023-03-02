
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace BroadcastPlayer.Models
{

    class TestPrintEntity : BaseEntity
    {

        public static string _tablename = "Test_Print";
        public static string _primaryKey = "ID";

        public static string tablename { get { return _tablename; } }
        public static string primaryKey { get { return _primaryKey; } }

        public static List<String> _except = new List<string>() {
            "tablename","primaryKey","ID","except"
        };


        public static List<String> except { get { return _except; } }

        /// <summary>
        /// 序号
        /// </summary>
        public Int64 ID { get; set; }

        public Int64 UniqueID { get; set; }

        /// <summary>
        /// 设备UID
        /// </summary>
        public String UID { get; set; }

        public String BatchNumber { get; set; }

        public DateTime CreatedAt { get; set; }


        public TestPrintEntity() {
            CreatedAt = DateTime.Now;
        }
        

        /// <summary>
        /// 更新指定 ID 的 UID
        /// </summary>
        public static String GetDeviceUUIDByUniqueID(Int32 UniqueID)
        {
            BaseEntity.DataSource = "Data Source=USM;Version=3;";
            TestPrintEntity tpe = new TestPrintEntity();
            DataTable dt = tpe.Select("UID", "UniqueID = '"+ UniqueID + "'");
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["UID"].ToString();
            }

            return null;
        }
       
    }

    
}
