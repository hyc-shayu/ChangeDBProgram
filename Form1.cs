using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        const string INFPATH = @"changetable.inf";
        private InfClass inf = new InfClass();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 点击修改按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            inf.Server = tbServer.Text.Trim();
            inf.Account = tbAccount.Text.Trim();
            inf.Password = tbPassword.Text.Trim();
            inf.Database = tbDatabase.Text.Trim();
            inf.Table = tbTable.Text.Trim();
            inf.OldType = tbOldType.Text.Trim();
            inf.NewType = tbNewType.Text.Trim();

            if(inf.hasNullOrEmpty())
            {
                MessageBox.Show("不能为空");
                return;
            }
            //保存数据
            inf.saveInf(INFPATH);

            SQLServer sql_server = new SQLServer(inf);
            sql_server.run();
            
        }
        
        

        /// <summary>
        /// 加载窗口时，从文件读取上次输入的信息
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            inf.readInfFromFile(INFPATH);
            tbServer.Text = inf.Server;
            tbAccount.Text = inf.Account;
            tbPassword.Text = inf.Password;
            tbDatabase.Text = inf.Database;
            tbTable.Text = inf.Table;
            tbOldType.Text = inf.OldType;
            tbNewType.Text = inf.NewType;
        }

    }

    class SQLServer
    {
        private SqlConnection conn;
        private InfClass inf;

        public SQLServer(InfClass inf)
        {
            this.inf = inf;
            string connStr = @"server=" + inf.Server + ";database=" + inf.Database + ";user id=" + inf.Account + ";pwd=" + inf.Password;
            this.conn = new SqlConnection(connStr);
        }

        public void run()
        {
            if (!isExistsTable(conn, inf.Table)|| !execute())
                return;
        }

        /// <summary>
        /// 判断是否存在表
        /// </summary>
        /// <param name="conn">The connection.</param>
        /// <param name="table">The table.</param>
        /// <returns>
        ///   <c>true</c> if [is exists table] [the specified connection]; otherwise, <c>false</c>.
        /// </returns>
        private bool isExistsTable(SqlConnection conn, string table)
        {
            string sql = "select count(1) from sysobjects where name like '" + table + "'";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                if (result == 0)
                {
                    MessageBox.Show("表不存在");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                MessageBox.Show("连接失败，请检查输入");
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 执行修改流程
        /// </summary>
        /// <returns>成功或失败</returns>
        private bool execute()
        {
            try
            {            
                DataTable dt = getSqlTable(conn, inf.Table, inf.OldType, inf.NewType);
                if (dt.Rows.Count > 0)
                {
                    if (executeSQLFromTable(dt, conn))
                        MessageBox.Show("修改成功");
                }
                else
                    MessageBox.Show("没有需要修改的列");
                return true;
            }
            catch (SqlException)
            {
                MessageBox.Show("修改失败，请检查输入");
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("发生一个错误，原因如下\n",e.ToString()));
                return false;
            }
        }


        /// <summary>
        /// 获取一个存储sql语句的表
        /// </summary>
        /// <param name="conn">The connection.</param>
        /// <param name="tableStr">The table string.</param>
        /// <returns></returns>
        private DataTable getSqlTable(SqlConnection conn, string tableStr, string oldType, string newType)
        {
            //string sql = "SELECT 'alter table ['+d.name+ '] alter column [' + a.name + '] n'" +
            //              "+ b.name + '(' + cast(a.length * 2 as varchar) + ')' as my_sql " +
            //              "FROM syscolumns a " +
            //              "left join systypes b on a.xtype = b.xusertype " +
            //              "inner join sysobjects d on a.id = d.id and d.xtype = 'U' and d.name like '" + tableStr + "' and d.name <> 'dtproperties'" +
            //              "where " +
            //              "b.name in('char', 'varchar') " +
            //              "and " +
            //              "not exists(SELECT 1 FROM sysobjects where xtype = 'PK' and name in ( " +
            //              "SELECT name FROM sysindexes WHERE indid in (" +
            //              "SELECT indid FROM sysindexkeys WHERE id = a.id AND colid = a.colid" +
            //              "))) " +
            //              "order by d.name,a.name";

            string[] tmpStrs = oldType.Split(' ');
            if (tmpStrs.Length > 1)
            {
                for (int i = 0; i < tmpStrs.Length; i++)
                {
                    tmpStrs[i] = string.Format("'{0}'", tmpStrs[i]);
                }
                oldType = string.Join(",",tmpStrs);
            }

            string sql = "SELECT 'alter table ['+d.name+ '] alter column [' + a.name + '] " + newType +
                          "' as my_sql " +
                          "FROM syscolumns a " +
                          "left join systypes b on a.xtype = b.xusertype " +
                          "inner join sysobjects d on a.id = d.id and d.xtype = 'U' and d.name like '" + tableStr + "' and d.name <> 'dtproperties'" +
                          "where " +
                          "b.name in (" + oldType + ") " +
                          "and " +
                          "not exists(SELECT 1 FROM sysobjects where xtype = 'PK' and name in ( " +
                          "SELECT name FROM sysindexes WHERE indid in (" +
                          "SELECT indid FROM sysindexkeys WHERE id = a.id AND colid = a.colid" +
                          "))) " +
                          "order by d.name,a.name";

            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 执行表中的sql语句
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="conn">The connection.</param>
        private bool executeSQLFromTable(DataTable dt, SqlConnection conn)
        {
            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.Transaction = transaction;
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    cmd.CommandText = dr[0] as string;
                    cmd.ExecuteNonQuery();
                }
                transaction.Commit();
                return true;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                MessageBox.Show(string.Format("修改失败，错误原因如下\n{0}", e.ToString()));
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
    }

    class InfClass
    {
        //异或加密字段
        const string SecretKey = "hyc";

        private string _server;
        private string _account;
        private string _password;
        private string _database;
        private string _table;
        private string _oldType;
        private string _newType;
        public string Account { get => _account; set => _account = value; }
        public string Server { get => _server; set => _server = value; }
        public string Password { get => _password; set => _password = value; }
        public string Database { get => _database; set => _database = value; }
        public string Table { get => _table; set => _table = value; }
        public string OldType { get => _oldType; set => _oldType = value; }
        public string NewType { get => _newType; set => _newType = value; }

        /// <summary>
        /// 判断有空的数据
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has null or empty]; otherwise, <c>false</c>.
        /// </returns>
        public bool hasNullOrEmpty()
        {
            return (string.IsNullOrEmpty(_server) || string.IsNullOrEmpty(_account) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_database) || string.IsNullOrEmpty(_table) || string.IsNullOrEmpty(_oldType) || string.IsNullOrEmpty(_newType));
        }

        public void saveInf(string path)
        {
            using (BinaryWriter writer = new BinaryWriter(new FileStream(path, FileMode.OpenOrCreate), Encoding.Unicode))
            {
                writer.Write(Encrypt(string.Format("{0}—{1}—{2}—{3}—{4}—{5}—{6}", _server, _account, _password, _database, _table, _oldType, _newType)));
            }
        }

        /// <summary>
        /// 从文件中读取数据，设置属性
        /// </summary>
        /// <param name="path">The path.</param>
        public void readInfFromFile(string path)
        {
            try
            {
                using (BinaryReader reader = new BinaryReader(new FileStream(path, FileMode.Open), Encoding.Unicode))
                {
                    string rawStr = reader.ReadString();
                    rawStr = Encrypt(rawStr);
                    string[] strs = rawStr.Split('—');
                    _server = strs[0];
                    _account = strs[1];
                    _password = strs[2];
                    _database = strs[3];
                    _table = strs[4];
                    _oldType = strs[5];
                    _newType = strs[6];
                }
            }
            catch { }
        }

        /// <summary>
        /// 异或加密
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        private string Encrypt(string content)
        {
            char[] data = content.ToCharArray();
            char[] key = SecretKey.ToCharArray();
            for (int i = 0; i < data.Length; i++)
            {
                data[i] ^= key[i % key.Length];
            }
            return new string(data);
        }
    }

}
