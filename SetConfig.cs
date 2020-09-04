using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace nsStockManage
{
    class SetConfig
    {
        public static void ReadConfig(String configPath)
        {
            if (System.IO.File.Exists(configPath))
            {
                using (StreamReader sr = new StreamReader(configPath, Encoding.UTF8))     //读取配置文件内容
                {
                    String nextLine = null;
                    while ((nextLine = sr.ReadLine()) != null)
                    {
                        if (nextLine.Contains("[ServerIP]"))
                        {
                            MainWindow.serverIP = sr.ReadLine();
                        }
                        else if(nextLine.Contains("[MysqlDatabase]"))
                        {
                            MainWindow.mysqlDatabase = sr.ReadLine();
                        }
                        else if (nextLine.Contains("[MysqlUser]"))
                        {
                            MainWindow.mysqlUserName = sr.ReadLine();
                        }
                        else if (nextLine.Contains("[MysqlPassword]"))
                        {
                            MainWindow.mysqlPassword = sr.ReadLine();
                        }
                    }
                    sr.Close();
                }
            }
            else
            {
                MessageBox.Show("配置文件不存在！");
                Program.mw.Close();
                Application.Exit();
            }
        }
    }
}
