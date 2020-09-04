using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using DataGridViewAutoFilter;

namespace nsStockManage
{
    class WarningManage
    {
        //DBConnection connection = new DBConnection();
        DataSet ds = null;
        String sql = null;


        /*****************************************    预警设置 界面     *************************************/

        // 预警设置界面 按键函数 //
        public void TextBox_warningSetUp_value_KeyPress(KeyPressEventArgs e)       //限制数量文本框只能输入数字和小数点
        {
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)    //非数字和小数点不做处理
                e.Handled = true;       //不处理

            if ((int)e.KeyChar == 46)                                     //小数点的处理
            {
                if (Program.mw.textBox_warningSetUp_value.Text.Length <= 0)
                    e.Handled = true;                                     //小数点不能在第一位
            }
        }

        // 预警设置界面 绘制函数 //

        

        // 预警设置界面 按钮函数 //
        public void WarningManage_Cancel()      //取消按钮
        {
            Program.mw.comboBox_warningSetUp_name.Text = "物料号";
            Program.mw.textBox_warningSetUp_materialNumber.Text = "";
            Program.mw.comboBox_warningSetUp_type.Text = "待修数量";
            Program.mw.textBox_warningSetUp_remarks.Text = "";
            Program.mw.comboBox_warningSetUp_method.Text = "数量";
            Program.mw.textBox_warningSetUp_value.Text = "";
        }

        public bool WarningManage_SetUp()       //设定按钮
        {
            String name = Program.mw.comboBox_warningSetUp_name.Text;
            String materialNumber = Program.mw.textBox_warningSetUp_materialNumber.Text;
            String type = Program.mw.comboBox_warningSetUp_type.Text;
            String remarks = Program.mw.textBox_warningSetUp_remarks.Text;
            String method = Program.mw.comboBox_warningSetUp_method.Text;
            String value = Program.mw.textBox_warningSetUp_value.Text;

            if (name.Contains("物料号"))
            {
                sql = "select * from tools where materialNumber = '" + materialNumber + "'";
            }
            else
            {
                sql = "select * from tools where category = '" + materialNumber + "'";
            }
            ds = MainWindow.connection.Select(sql);
            if (ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
            {
                MessageBox.Show("无此工装信息，请核对信息！");
                return false;
            }

            try
            {
                //若预警设置已存在，是否更新？
                sql = "select * from warningSetup where name = '" + name + "' AND materialNumber = '" + materialNumber + "' AND type = '" + type + "'";
                ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {//有记录，更新一下
                    DialogResult result = MessageBox.Show("该预警项已存在，是否要更改？", "更改预警项", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                    {
                        return false;
                    }
                    else
                    {
                        sql = @"UPDATE warningSetup 
                                SET method='" + method + "', value='" + value + "', remarks='" + remarks + "', setupDate='" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss") + "' " +
                                "WHERE name = '" + name + "' AND materialNumber = '" + materialNumber + "' AND type = '" + type + "'";
                    }
                }//无记录，新插入
                else
                {
                    //插入预警设置数据库
                    sql = @" INSERT INTO warningSetup
                             (name,materialNumber,type,method,value,remarks,setupDate)
                       VALUES('" + name + "','" + materialNumber + "','" + type + "','" + method + "','" + value + "','" + remarks + "','" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss") + "')";
                }

                if (MainWindow.connection.Insert(sql))
                {
                    //添加记录数据库
                    sql = @"insert into records 
                                    (toolName,code,category,materialNumber,number,shelf,operationType,operationDate,operationTime,terminal,remarks) 
                            values (
                                     '" + "预警项" + "'," +
                                    "'" + name + "'," +
                                    "'" + materialNumber + "'," +
                                    "'" + type + "'," +
                                    "'" + value + "'," +
                                    "'" + method + "'," +
                                    "'" + "预警设置" + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                    "'" + DateTime.Now.ToString("HH:mm:ss") + "'," +
                                    "'" + MainWindow.TerminalNumber + "'," +
                                    "'" + remarks + "');";
                    MainWindow.connection.Insert(sql);

                    //刷新数据窗口
                    CommonFunction.FillDataGridView_WarningManage(Program.mw.dataGridView_warningSetUp, Program.mw.myBindingNavigator_WarningSetUp, Program.mw.bindingNavigator_WarningSetUp);
                    return true;
                }
                else
                {
                    MessageBox.Show("数据保存失败！");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据保存失败！");
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                CommonFunction.WarningUpdate();
            }
        }


        /*****************************************    预警概览 界面     *************************************/


        // 预警设置界面 绘制函数 //

        
    }
}
