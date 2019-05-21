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
        const string SecretKey = "hyc";

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
            string server = tbServer.Text.Trim();
            string account = tbAccount.Text.Trim();
            string password = tbPassword.Text.Trim();
            string database = tbDatabase.Text.Trim();
            string table = tbTable.Text.Trim();
            if(string.IsNullOrEmpty(server)||string.IsNullOrEmpty(account)||string.IsNullOrEmpty(password)||string.IsNullOrEmpty(database)||string.IsNullOrEmpty(table))
            {
                MessageBox.Show("不能为空");
                return;
            }
            //连接数据库
            string connStr = @"server=" + server + ";database=" + database + ";user id=" + account + ";pwd=" + password;
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                DataTable dt = getSqlTable(conn, table);
                if (dt.Rows.Count > 0)
                {
                    executeSQLFromTable(dt, conn);
                    MessageBox.Show("修改成功");
                    //保存数据
                    saveInf(server, account, password, database, table);
                }
                else
                    MessageBox.Show("没有需要修改的列");
            }
            catch (SqlException)
            {
                MessageBox.Show("连接失败");
            }
            catch (Exception)
            {
                MessageBox.Show("发生一个错误");
            }
        }

        /// <summary>
        /// 获取一个存储sql语句的表
        /// </summary>
        /// <param name="conn">The connection.</param>
        /// <param name="tableStr">The table string.</param>
        /// <returns></returns>
        private DataTable getSqlTable(SqlConnection conn, string tableStr)
        {
            string sql = "SELECT 'alter table ['+d.name+ '] alter column [' + a.name + '] n'" +
                          "+ b.name + '(' + cast(a.length * 2 as varchar) + ')' as my_sql " +
                          "FROM syscolumns a " +
                          "left join systypes b on a.xtype = b.xusertype " +
                          "inner join sysobjects d on a.id = d.id and d.xtype = 'U' and d.name like '" + tableStr + "' and d.name <> 'dtproperties'" +
                          "where " +
                          "b.name in('char', 'varchar') " +
                          "and " +
                          "not exists(SELECT 1 FROM sysobjects where xtype = 'PK' and name in ( " +
                          "SELECT name FROM sysindexes WHERE indid in (" +
                          "SELECT indid FROM sysindexkeys WHERE id = a.id AND colid = a.colid" +
                          "))) " +
                          "order by d.name,a.name";
            //string sql = "select * from " + tableStr;
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
        private void executeSQLFromTable(DataTable dt, SqlConnection conn)
        {
            conn.Open();
            foreach(DataRow dr in dt.Rows)
            {
                SqlCommand cmd = new SqlCommand(dr[0] as string, conn);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        /// <summary>
        /// 保存输入信息
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="account">The account.</param>
        /// <param name="password">The password.</param>
        /// <param name="database">The database.</param>
        /// <param name="table">The table.</param>
        private void saveInf(string server, string account, string password, string database, string table)
        {
            if (!File.Exists(INFPATH))
                File.Create(INFPATH).Close();
            using (StreamWriter writer = new StreamWriter(INFPATH))
            {
                writer.WriteLine(Encrypt(server));
                writer.WriteLine(Encrypt(account));
                writer.WriteLine(Encrypt(password));
                writer.WriteLine(Encrypt(database));
                writer.WriteLine(Encrypt(table));
            }
        }

        /// <summary>
        /// 加载窗口时，从文件读取上次输入的信息
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                using(StreamReader reader = new StreamReader(INFPATH))
                {
                    tbServer.Text = Encrypt(reader.ReadLine());
                    tbAccount.Text = Encrypt(reader.ReadLine());
                    tbPassword.Text = Encrypt(reader.ReadLine());
                    tbDatabase.Text = Encrypt(reader.ReadLine());
                    tbTable.Text = Encrypt(reader.ReadLine());
                }
            }
            catch
            {
            }
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
            for (int i = 0; i<data.Length; i++)
            {
                data[i] ^= key[i % key.Length];
            }
            return new string(data);
        }

    }

}
