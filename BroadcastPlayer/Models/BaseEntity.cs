using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastPlayer.Models
{
    public class BaseEntity
    {
        public static string DatabaseFilename { get; set; } = "MyDatabase.sqlite";
        public static string DataSource { get; set; } = "Data Source=MyDatabase.sqlite;Version=3;";

        public static List<String> constraint = new List<string>() {
            "UUID","AddressID"
        };

        //public static string _tablename = "";
        //public static string _primaryKey = "";


        //public static string tablename { get { return _tablename; } }
        //public static string primaryKey { get { return _primaryKey; } }

        //public static List<String> _except = new List<string>() {

        //};

        //public static List<String> except { get { return _except; } }


        public static void Mark(String mk)
        {
            Console.WriteLine(String.Format("[{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"), mk));
        }


        public static Dictionary<Type, ColType> map = new Dictionary<Type, ColType>()
        {
            { typeof(String),ColType.Text},
            { typeof(Byte),ColType.Integer},


            { typeof(Int16),ColType.Integer},
            { typeof(UInt16),ColType.Integer},
            { typeof(Int32),ColType.Integer},
            { typeof(Int64),ColType.Integer},
            { typeof(DateTime),ColType.DateTime},
            { typeof(Decimal),ColType.Decimal},
        };

        public static Dictionary<String, ColType> strColTypeMap = new Dictionary<String, ColType>()
        {
            { "integer",ColType.Integer},
            { "INT",ColType.Integer},
            { "real",ColType.Decimal},
            { "text",ColType.Text},
            { "blob",ColType.BLOB},
            { "datetime",ColType.DateTime},
        };


        /// <summary>
        /// 新增字段，字段的默认值
        /// </summary>
        public static Dictionary<ColType, String> ColumnDefaultValueMap = new Dictionary<ColType, String>()
        {
            { ColType.Integer,"0"},
            { ColType.Decimal,"0"},
            { ColType.Text,"null"},
            { ColType.BLOB,"null"},
            { ColType.DateTime,"\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\""},
        };


        public static void CreateTable(Type t, String tablename = "")
        {
            SQLiteTable tb = new SQLiteTable(tablename != "" ? tablename : t.GetProperty("tablename").GetValue(null, null).ToString());
            PropertyInfo[] PropertyList = t.GetProperties();
            List<String> except = (List<String>)t.GetProperty("except").GetValue(null, null);
            ColType ct;
            tb.Columns.Add(new SQLiteColumn(t.GetProperty("primaryKey").GetValue(null, null).ToString(), ColType.Integer, true, true, true, ""));
            foreach (PropertyInfo item in PropertyList)
            {
                string name = item.Name;
                map.TryGetValue(item.PropertyType, out ct);
                if (!except.Contains(name))
                    tb.Columns.Add(new SQLiteColumn(name, ct, false, false, false, ""));
            }
            UsingSQLite((SQLiteConnection conn, SQLiteCommand cmd) =>
            {
                cmd.Connection = conn;
                SQLiteHelper hp = new SQLiteHelper(cmd);
                hp.CreateTable(tb);
            });
        }


        public void SaveAll(BaseEntity[] Entities)
        {

            UsingSQLite((SQLiteConnection conn, SQLiteCommand cmd) =>
            {

                foreach (BaseEntity be in Entities)
                {

                    Dictionary<string, object> dic = new Dictionary<string, object>();

                    Type t = be.GetType();
                    PropertyInfo[] PropertyList = t.GetProperties();
                    String pk = t.GetProperty("primaryKey").GetValue(null, null).ToString();
                    List<String> except = (List<String>)t.GetProperty("except").GetValue(null, null);
                    foreach (PropertyInfo item in PropertyList)
                    {
                        string name = item.Name;
                        if (!except.Contains(name))
                        {
                            object value = item.GetValue(be, null);
                            dic[name] = value;
                        }
                    }
                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    sh.Insert(t.GetProperty("tablename").GetValue(null, null).ToString(), dic);
                    t.GetProperty(pk).SetValue(be, sh.LastInsertRowId());
                }

            });

        }

        public void Save()
        {
            UsingSQLite((SQLiteConnection conn, SQLiteCommand cmd) =>
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();

                Type t = this.GetType();
                PropertyInfo[] PropertyList = t.GetProperties();
                String pk = t.GetProperty("primaryKey").GetValue(null, null).ToString();
                List<String> except = (List<String>)t.GetProperty("except").GetValue(null, null);
                foreach (PropertyInfo item in PropertyList)
                {
                    string name = item.Name;
                    if (!except.Contains(name))
                    {
                        object value = item.GetValue(this, null);
                        dic[name] = value;
                    }
                }
                SQLiteHelper sh = new SQLiteHelper(cmd);


                sh.Insert(t.GetProperty("tablename").GetValue(null, null).ToString(), dic);

                t.GetProperty(pk).SetValue(this, sh.LastInsertRowId());

            });
        }

        public void Update(Dictionary<string, object> dicData, Dictionary<string, object> dicCond)
        {

            UsingSQLite((SQLiteConnection conn, SQLiteCommand cmd) =>
            {
                SQLiteHelper sh = new SQLiteHelper(cmd);
                sh.Update(this.GetType().GetProperty("tablename").GetValue(null, null).ToString(), dicData, dicCond);
            });
        }


        public void Update(Dictionary<string, object> dicData)
        {
            UsingSQLite((SQLiteConnection conn, SQLiteCommand cmd) =>
            {
                SQLiteHelper sh = new SQLiteHelper(cmd);
                Type t = this.GetType();
                String pk = t.GetProperty("primaryKey").GetValue(null, null).ToString();
                dicData["UpdatedAt"] = DateTime.Now;
                sh.Update(t.GetProperty("tablename").GetValue(null, null).ToString(), dicData, new Dictionary<string, object>() {
                    { pk,t.GetProperty(pk).GetValue(this, null)}
                });
            });
        }

        public int Delete(Dictionary<string, object> dicCond)
        {
            int ret = -1;

            UsingSQLite((SQLiteConnection conn, SQLiteCommand cmd) =>
            {
                SQLiteHelper sh = new SQLiteHelper(cmd);
                ret = sh.Delete(this.GetType().GetProperty("tablename").GetValue(null, null).ToString(), dicCond);
            });

            return ret;
        }


        public int Delete()
        {
            int ret = -1;
            UsingSQLite((SQLiteConnection conn, SQLiteCommand cmd) =>
            {
                SQLiteHelper sh = new SQLiteHelper(cmd);

                Type t = this.GetType();
                String pk = t.GetProperty("primaryKey").GetValue(null, null).ToString();
                ret = sh.Delete(t.GetProperty("tablename").GetValue(null, null).ToString(), new Dictionary<string, object>() {
                    { pk,t.GetProperty(pk).GetValue(this, null)}
                });
            });
            return ret;
        }


        public DataTable All()
        {
            DataTable dt = null;

            UsingSQLite((SQLiteConnection conn, SQLiteCommand cmd) =>
            {
                SQLiteHelper sh = new SQLiteHelper(cmd);
                String tablename = this.GetType().GetProperty("tablename").GetValue(null, null).ToString();
                dt = sh.Select("Select * from " + tablename);
            });

            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageInfo">
        /// 
        ///  CurrentPage,
        ///  TotalPage,
        ///  TotalNum,
        ///  PageSize
        /// </param>
        /// <returns></returns>
        public DataTable Page(Int32 CurrentPage, Int32 PageSize,out Int32[] PageInfo,String Condition = "", String OrderBy = "")
        {
            DataTable dt = null;
           
            Int32 TotalPage = 0;
            Int32 TotalNum = 0;
            UsingSQLite((SQLiteConnection conn, SQLiteCommand cmd) =>
            {
                SQLiteHelper sh = new SQLiteHelper(cmd);
                String tablename = this.GetType().GetProperty("tablename").GetValue(null, null).ToString();

                String WhereString = "";

                if (!String.IsNullOrEmpty(Condition)) {
                    WhereString = " WHERE " + Condition;
                }

                dt = sh.Select("Select count(*) AS Count from " + tablename + WhereString);
                TotalNum = Convert.ToInt32(dt.Rows[0]["Count"]);

                TotalPage = (TotalNum % PageSize == 0) ? TotalNum / PageSize : TotalNum / PageSize + 1;
                CurrentPage = CurrentPage < 1 ? 1 : CurrentPage;
                CurrentPage = CurrentPage > TotalPage ? TotalPage : CurrentPage;
                String SQLString = "Select * from " + tablename + WhereString + " " + OrderBy + " limit " + (CurrentPage - 1) * PageSize + "," + PageSize;

                Console.WriteLine("Q SQL = " + SQLString);
                dt = sh.Select(SQLString);
            });
            PageInfo = new Int32[4] {
                CurrentPage,
                TotalPage,
                TotalNum,
                PageSize
            };
            return dt;
        }



        public DataTable Select(String Fields, String Where = "",String OrderBy = "",String GroupBy = "")
        {
            DataTable dt = null;

            UsingSQLite((SQLiteConnection conn, SQLiteCommand cmd) =>
            {
                SQLiteHelper sh = new SQLiteHelper(cmd);
                String tablename = this.GetType().GetProperty("tablename").GetValue(null, null).ToString();
                String WhereStr = String.IsNullOrEmpty(Where) ? "" : " WHERE " + Where;
                String OrderByStr = String.IsNullOrEmpty(OrderBy) ? "" : " ORDER BY " + OrderBy;
                String GroupByStr = String.IsNullOrEmpty(GroupBy) ? "" : " Group BY " + GroupBy;
                String SQL = "SELECT " + Fields + " FROM " + tablename + WhereStr  + GroupByStr + OrderByStr;
                
                dt = sh.Select(SQL);

                Console.WriteLine(conn.ConnectionString);

                Console.WriteLine(conn.State);
                Console.WriteLine("SQL=" + SQL);
            });

            return dt;
        }

        public DataTable Select(Dictionary<string, object> dicCond)
        {
            DataTable dt = null;

            UsingSQLite((SQLiteConnection conn, SQLiteCommand cmd) =>
            {
                SQLiteHelper sh = new SQLiteHelper(cmd);
                String tablename = this.GetType().GetProperty("tablename").GetValue(null, null).ToString();
                dt = sh.Query(tablename, dicCond);
            });

            return dt;
        }


        public DataTable DeleteAll()
        {
            DataTable dt = null;

            UsingSQLite((SQLiteConnection conn, SQLiteCommand cmd) =>
            {
                SQLiteHelper sh = new SQLiteHelper(cmd);
                String tablename = this.GetType().GetProperty("tablename").GetValue(null, null).ToString();

                String SQL = "DELETE FROM " + tablename;

                dt = sh.Select(SQL);
            });
            return dt;
        }

        public static void PrintDataSet(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Console.Write(dt.Columns[j].ColumnName + " = " + dt.Rows[i][j] + " \t");
                }
                Console.WriteLine();
            }
        }



        public delegate void UsingSQLiteDelegate(SQLiteConnection conn, SQLiteCommand cmd);


        public static void UsingSQLite(UsingSQLiteDelegate v_UsingSQLiteDelegate,String DataSourceName = "")
        {
            if (String.IsNullOrEmpty(DataSourceName))
                DataSourceName = DataSource;

            using (SQLiteConnection conn = new SQLiteConnection(DataSourceName))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    //String t1 = DateTime.Now.ToString("HH:mm:ss.ffffff");
                    conn.Open();
                    //String t2 = DateTime.Now.ToString("HH:mm:ss.ffffff");
                    
                    if (null != v_UsingSQLiteDelegate)
                        v_UsingSQLiteDelegate.Invoke(conn, cmd);
                    //String t3 = DateTime.Now.ToString("HH:mm:ss.ffffff");
                    conn.Close();
                    //String t4 = DateTime.Now.ToString("HH:mm:ss.ffffff");
                    //Console.WriteLine(String.Format("{0} {1} {2} {3}",t1,t2,t3,t4));
                }
            }
        }


        /// <summary>
        /// 更新表结构
        /// </summary>
        /// <param name="Entity">表的实体类类型</param>
        /// <param name="renameColumns">重命名字段</param>
        public static void UpgradeTableStructure(Type Entity, Dictionary<String, String> renameColumns = null)
        {
            // 结构一样就无需改变
            // 如果有变化（列名，字段类型），就直接重建表
            //   取出对应字段的值 insert into new_table select new_columns from old_table

            UsingSQLite((SQLiteConnection conn, SQLiteCommand cmd) =>
            {
                String stateStr = Enum.GetName(typeof(ConnectionState), conn.State);

                SQLiteHelper sh = new SQLiteHelper(cmd);
                String tablename = Entity.GetProperty("tablename").GetValue(null, null).ToString();
                String primaryKey = Entity.GetProperty("primaryKey").GetValue(null, null).ToString();

                #region 读取当前表字段
                DataTable dt = sh.GetColumnStatus(tablename);
                Dictionary<String, ColType> oldFields = new Dictionary<string, ColType>();

                Dictionary<String, String> insertColName = new Dictionary<string, String>() {
                        { primaryKey,primaryKey}
                    };

                ColType ct;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    String name = dt.Rows[i]["name"].ToString(), type = dt.Rows[i]["type"].ToString();
                    if (!strColTypeMap.TryGetValue(type, out ct))
                    {
                        throw new Exception("type `" + type + "` is not configured!");
                    }
                    oldFields.Add(name, ct);
                }

                //PrintDataSet(dt);
                #endregion

                Boolean needRecreate = false;

                PropertyInfo[] PropertyList = Entity.GetProperties();
                List<String> except = (List<String>)Entity.GetProperty("except").GetValue(null, null);
                foreach (PropertyInfo item in PropertyList)
                {
                    string name = item.Name;
                    map.TryGetValue(item.PropertyType, out ct);

                    //if (false == map.TryGetValue(item.PropertyType, out ct)) {
                    //    throw new Exception("type `" + item.PropertyType + " is not configured");
                    //}
                    if (!except.Contains(name))
                    {
                        if (oldFields.ContainsKey(name))
                        {
                            // 包含该字段
                            ColType ct1;
                            oldFields.TryGetValue(name, out ct1);
                            if (ct != ct1)
                            {
                                // 类型不同
                                needRecreate = true;
                            }
                            // 加入数据回填
                            insertColName.Add(name, name);
                        }
                        else
                        {
                            // 不包含该字段
                            needRecreate = true;

                            // 从重命名字典中搜寻
                            String oldColumnName = "";
                            if (null != renameColumns && renameColumns.TryGetValue(name, out oldColumnName))
                            {
                                // 如果该字段属于重命名字段
                                insertColName.Add(name, oldColumnName);
                            }
                            else
                            {
                                // 取默认值
                                String dfV;
                                ColumnDefaultValueMap.TryGetValue(ct, out dfV);

                                insertColName.Add(name, dfV);
                            }
                        }
                    }
                }

                if (oldFields.Count() != insertColName.Count())
                {
                    needRecreate = true;
                }

                if (needRecreate)
                {
                    String tempTableName = tablename + "_old_" + (new Random()).Next(100, 999);
                    // create new table
                    CreateTable(Entity, tempTableName);
                    // insert data
                    StringBuilder insertSql = new StringBuilder();
                    insertSql.Append("INSERT INTO `" + tempTableName + "` SELECT ");

                    Boolean isFirst = true;
                    foreach (KeyValuePair<String, String> kv in insertColName)
                    {
                        if (isFirst)
                            insertSql.Append(kv.Value);
                        else
                            insertSql.Append("," + kv.Value);
                        if (isFirst) isFirst = !isFirst;
                    }
                    insertSql.Append(" FROM `" + tablename + "`");
                    cmd.CommandText = insertSql.ToString();
                    cmd.ExecuteNonQuery();
                    // drop old table
                    sh.DropTable(tablename);
                    // rename new table
                    sh.RenameTable(tempTableName, tablename);
                }
            });
        }

        public void ExecuteSQL(String SQLStr)
        {
            UsingSQLite((SQLiteConnection conn, SQLiteCommand cmd) =>
            {
                SQLiteHelper sh = new SQLiteHelper(cmd);
                sh.Execute(SQLStr);
            });
        }


        #region Batch Insert
        /// <summary>
        /// 当前批量导入的值的数量
        /// </summary>
        int CurrentValuePairCount = 0;

        /// <summary>
        /// 一次批量导入数量的临界值，超出则进行一次 INSERT 操作
        /// </summary>
        int FlushValuePairCount = 50;

        /// <summary>
        /// 批量导入 SQL 语句头
        /// </summary>
        StringBuilder BatchInsertSQLHeader = new StringBuilder();

        /// <summary>
        /// 批量导入 SQL 语句 VALUE 体
        /// </summary>
        StringBuilder BatchInsertSQLValues = new StringBuilder();

        /// <summary>
        /// 设置批量导入的字段
        /// </summary>
        /// <param name="fields"></param>
        public void SetBatchInsertFields(String[] fields)
        {
            BatchInsertSQLHeader.Append("INSERT INTO `" + this.GetType().GetProperty("tablename").GetValue(null, null).ToString() + "` (");
            Boolean isFirst = true;
            foreach (String str in fields)
            {
                if (!isFirst)
                {
                    BatchInsertSQLHeader.Append(",");
                }
                BatchInsertSQLHeader.Append("\"" + str + "\"");
                if (isFirst)
                    isFirst = false;
            }
            BatchInsertSQLHeader.Append(")VALUES");
        }

        /// <summary>
        /// 新增批量导入的值
        /// </summary>
        /// <param name="pair"></param>
        public void AddValuePair(params Object[] pair)
        {
            Boolean isFirst = true;
            BatchInsertSQLValues.Append("(");
            foreach (Object obj in pair)
            {
                if (!isFirst)
                {
                    BatchInsertSQLValues.Append(",");
                }
                if (obj is int)
                {
                    BatchInsertSQLValues.Append(obj.ToString());
                }
                else if (obj is String)
                {
                    BatchInsertSQLValues.Append("'" + obj.ToString() + "'");
                }
                else
                {
                    BatchInsertSQLValues.Append("'" + obj.ToString() + "'");
                }
                if (isFirst)
                    isFirst = false;
            }
            BatchInsertSQLValues.Append("),");
            CurrentValuePairCount++;
            if (CurrentValuePairCount > FlushValuePairCount)
            {
                FlushBatchInsert();
            }
        }

        /// <summary>
        /// 执行一次批量 INSERT 操作
        /// </summary>
        public void FlushBatchInsert()
        {
            if (CurrentValuePairCount == 0)
            {
                return;
            }
            CurrentValuePairCount = 0;

            ExecuteSQL(BatchInsertSQLHeader.ToString() + BatchInsertSQLValues.ToString(0, BatchInsertSQLValues.Length - 1));

            BatchInsertSQLValues.Clear();
        }
        #endregion Batch Import



        #region Init Database

        public static void InitSQLiteTables()
        {
 

        }

        public static void UpdateTableStructure()
        {

        }

        public static void GenerateRequiredData()
        {
            // 初始化 0 信道


        }
        #endregion Init Database



        #region Init Physical Database

        public static void InitPSQLiteTables()
        {
         
        }

        public static void UpdatePTableStructure()
        {
           

        }

        public static void GeneratePRequiredData()
        {
            // 初始化 0 信道


        }
        #endregion Init Database


    }
}
