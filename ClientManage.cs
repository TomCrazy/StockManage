using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace nsStockManage
{
    public partial class ClientManage : Form
    {

        /////*--------------------------------------   J   --------------------------------------*/////

        private DBConnection connnection = new DBConnection();

        public ClientManage()
        {
            InitializeComponent();
        }

        private void ClientManage_Load(object sender, EventArgs e)
        {
            //去重已登录的客户端
            DataSet ds = connnection.Select("select * from terminal;");
            if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    for (int i = 0; i < comboBox_clientSelect.Items.Count; i++)
                    {
                        if (comboBox_clientSelect.Items[i].ToString().Contains(row[1].ToString()))
                        {
                            comboBox_clientSelect.Items.RemoveAt(i);
                        }
                    }
                }
            }
        }

        //确定按钮
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox_clientSelect.Text == "")
            {
                MessageBox.Show("请选择客户端名称！");
            }
            else
            {
                if(String.IsNullOrEmpty(MainWindow.TerminalNumber))    //新客户端
                {
                    MainWindow.TerminalNumber = comboBox_clientSelect.Text;
                    Program.mw.statusStripStatusLabel_client.Text = MainWindow.TerminalNumber;
                    //插入配置文件
                    using (StreamWriter sw = new StreamWriter(MainWindow.configPath, true, Encoding.UTF8))
                    {
                        String str1 = "[TerminalNumber]";
                        String str2 = MainWindow.TerminalNumber;
                        sw.WriteLine();
                        sw.WriteLine(str1);
                        sw.WriteLine(str2);
                        sw.Close();
                    }
                    //插入数据库
                    connnection.Insert("insert into terminal (terminal) values ('"+ MainWindow.TerminalNumber +"')");
                }
                else                                              //变更客户端
                {
                    ClientManageFunction cmf = new ClientManageFunction();
                    cmf.ChangeTerminal(comboBox_clientSelect.Text);
                }
                this.Close();
            }
        }

        //取消按钮
        private void button2_Click(object sender, EventArgs e)    //取消按钮
        {
            this.Close();
            if (String.IsNullOrEmpty(MainWindow.TerminalNumber))
            {
                Application.Exit();
            }
        }

        /////*--------------------------------------   L   --------------------------------------*/////

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)    //去掉combo_box选中时候的蓝色背景
        {
            comboBox_clientSelect.Enabled = false;
            comboBox_clientSelect.Enabled = true;
        }
    }

    /////*--------------------------------------   J   --------------------------------------*/////

    public class ClientManageFunction
    {
        //核对客户端名称
        public void CheckTerminal()
        {
            try
            {
                if (System.IO.File.Exists(MainWindow.configPath))
                {
                    using (StreamReader sr = new StreamReader(MainWindow.configPath, Encoding.UTF8))     //读取配置文件内容
                    {
                        String nextLine = null;
                        String terminalNumber = null;
                        while ((nextLine = sr.ReadLine()) != null)
                        {
                            if (nextLine.Contains("[TerminalNumber]"))
                            {
                                terminalNumber = sr.ReadLine();
                            }
                        }
                        sr.Close();

                        if (String.IsNullOrEmpty(terminalNumber))                  //配置文件中无客户端记录
                        {
                            ClientManage cm = new ClientManage();
                            cm.ShowDialog();
                        }
                        else                                                       //有客户端记录
                        {
                            MainWindow.TerminalNumber = terminalNumber;
                            DBConnection connection = new DBConnection();
                            String sql = "select * from terminal where terminal = '" + terminalNumber + "';";
                            DataSet ds = connection.Select(sql);
                            if(ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                            {
                                sql = "insert into terminal (terminal) values ('" + terminalNumber + "');";
                                connection.Insert(sql);
                            }
                        }
                        Program.mw.statusStripStatusLabel_client.Text = MainWindow.TerminalNumber;
                    }
                }
                else
                {
                    MessageBox.Show("配置文件不存在！");
                    Program.mw.Close();
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        
        //变更客户端
        public void ChangeTerminal(String newTerminal)
        {
            //修改变量
            String oldTerminal = MainWindow.TerminalNumber;
            MainWindow.TerminalNumber = newTerminal;
            Program.mw.statusStripStatusLabel_client.Text = newTerminal;
            
            //修改数据库
            DBConnection connection = new DBConnection();
            connection.Update("update terminal set terminal='"+ newTerminal +"' where terminal='"+ oldTerminal +"'");

            //修改配置文件
            String[] lines = File.ReadAllLines(MainWindow.configPath, Encoding.UTF8);
            for(int i=0; i<lines.Length; i++)
            {
                if (lines[i].Contains(oldTerminal))
                {
                    lines[i] = newTerminal;
                }
            }
            File.WriteAllLines(MainWindow.configPath, lines);

            MessageBox.Show("客户端变更成功！");
        }

    }
}
