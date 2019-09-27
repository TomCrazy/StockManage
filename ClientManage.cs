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

using nsStockManage;
using nsMainWindow;
using nsDBConnection;

namespace nsClientManage
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
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                for (int i = 0; i < comboBox_clientSelect.Items.Count; i++)
                {
                    if (comboBox_clientSelect.Items[i].ToString() == row[1].ToString())
                    {
                        comboBox_clientSelect.Items.RemoveAt(i);
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
                if(MainWindow.TerminalNumber == null)                       //新客户端
                {
                    MainWindow.TerminalNumber = comboBox_clientSelect.Text;
                    Program.mw.statusStripStatusLabel1.Text = MainWindow.TerminalNumber;
                    //插入配置文件
                    using (StreamWriter sw = new StreamWriter(MainWindow.filePath, true, Encoding.UTF8))
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
                    connnection.Close();
                }
                else                                                       //变更客户端
                {
                    ClientManageFunction cmf = new ClientManageFunction();
                    cmf.changeTerminal(comboBox_clientSelect.Text);
                }
                this.Close();
            }
        }

        //取消按钮
        private void button2_Click(object sender, EventArgs e)    //取消按钮
        {
            connnection.Close();
            this.Close();
            if (MainWindow.TerminalNumber == null)
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
        public void checkTerminal()
        {
            try
            {
                if (System.IO.File.Exists(MainWindow.filePath))
                {
                    using (StreamReader sr = new StreamReader(MainWindow.filePath, Encoding.UTF8))     //读取配置文件内容
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

                        if (terminalNumber == null)                                //无客户端记录
                        {
                            ClientManage cm = new ClientManage();
                            cm.ShowDialog();
                        }
                        else                                                       //有客户端记录
                        {
                            MainWindow.TerminalNumber = terminalNumber;
                            //MessageBox.Show(MainWindow.TerminalNumber);
                        }
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
        public void changeTerminal(String newTerminal)
        {
            //修改变量
            String oldTerminal = MainWindow.TerminalNumber;
            MainWindow.TerminalNumber = newTerminal;
            Program.mw.statusStripStatusLabel1.Text = newTerminal;
            
            //修改数据库
            DBConnection connection = new DBConnection();
            connection.Update("update terminal set terminal='"+ newTerminal +"' where terminal='"+ oldTerminal +"'");
            connection.Close();

            //修改配置文件
            String[] lines = File.ReadAllLines(MainWindow.filePath,Encoding.UTF8);
            for(int i=0; i<lines.Length; i++)
            {
                if (lines[i].Contains(oldTerminal))
                {
                    lines[i] = newTerminal;
                }
            }
            File.WriteAllLines(MainWindow.filePath, lines);

            MessageBox.Show("客户端变更成功！");
        }

    }
}
