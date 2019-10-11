﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using nsDBConnection;
using nsMainWindow;

//工装入库类
namespace nsStockManage
{
    class ToolsIn
    {
        DBConnection connection = new DBConnection();
        DataSet ds = null;
        String sql = null;

        /*****************************************    新购工装入库界面     *************************************/


        //新购工装入库界面文本框默认值函数
        public void comboBox_newToolsIn_lifetype_SelectedIndexChanged()  //额定寿命类型选取
        {
            if (Program.mw.comboBox_newToolsIn_lifetype.Text == "时间")
            {
                Program.mw.textBox_newToolsIn_lifespan.Text = "天 ";
                Program.mw.textBox_newToolsIn_lifespan.ForeColor = Color.Gray;
                Program.mw.textBox_newToolsIn_lifespan.TextAlign = HorizontalAlignment.Right;
            }
            if (Program.mw.comboBox_newToolsIn_lifetype.Text == "次数")
            {
                Program.mw.textBox_newToolsIn_lifespan.Text = "次 ";
                Program.mw.textBox_newToolsIn_lifespan.ForeColor = Color.Gray;
                Program.mw.textBox_newToolsIn_lifespan.TextAlign = HorizontalAlignment.Right;
            }
        }

        public void textBox_newToolsIn_lifespan_Enter()                 //新购工装入库界面 额定寿命 文本框默认值
        {
            if ((Program.mw.textBox_newToolsIn_lifespan.Text == "天 ") || (Program.mw.textBox_newToolsIn_lifespan.Text == "次 "))
            {
                Program.mw.textBox_newToolsIn_lifespan.Text = "";
                Program.mw.textBox_newToolsIn_lifespan.ForeColor = Color.Black;
                Program.mw.textBox_newToolsIn_lifespan.TextAlign = HorizontalAlignment.Left;
            }
        }
        public void textBox_newToosIn_lifespan_Leave()
        {
            if ((String.IsNullOrEmpty(Program.mw.textBox_newToolsIn_lifespan.Text)) && (Program.mw.comboBox_newToolsIn_lifetype.Text == "时间"))
            {
                Program.mw.textBox_newToolsIn_lifespan.Text = "天 ";
                Program.mw.textBox_newToolsIn_lifespan.ForeColor = Color.Gray;
                Program.mw.textBox_newToolsIn_lifespan.TextAlign = HorizontalAlignment.Right;
            }
            if ((String.IsNullOrEmpty(Program.mw.textBox_newToolsIn_lifespan.Text)) && (Program.mw.comboBox_newToolsIn_lifetype.Text == "次数"))
            {
                Program.mw.textBox_newToolsIn_lifespan.Text = "次 ";
                Program.mw.textBox_newToolsIn_lifespan.ForeColor = Color.Gray;
                Program.mw.textBox_newToolsIn_lifespan.TextAlign = HorizontalAlignment.Right;
            }
        }
        public void textBox_newToosIn_price_Enter()                 //新购工装入库界面 单价 文本框默认值
        {
            if (Program.mw.textBox_newToolsIn_price.Text == "元 ")
            {
                Program.mw.textBox_newToolsIn_price.Text = "";
                Program.mw.textBox_newToolsIn_price.ForeColor = Color.Black;
                Program.mw.textBox_newToolsIn_price.TextAlign = HorizontalAlignment.Left;
            }
        }
        public void textBox_newToosIn_price_Leave()
        {
            if (String.IsNullOrEmpty(Program.mw.textBox_newToolsIn_price.Text))
            {
                Program.mw.textBox_newToolsIn_price.Text = "元 ";
                Program.mw.textBox_newToolsIn_price.ForeColor = Color.Gray;
                Program.mw.textBox_newToolsIn_price.TextAlign = HorizontalAlignment.Right;
            }
        }
        public void textBox_newToolsIn_price_KeyPress(KeyPressEventArgs e)       //限制价格编辑框只能输入数字和小数点
        {
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)    //非数字和小数点不做处理
                e.Handled = true;

            if ((int)e.KeyChar == 46)                                     //小数点的处理
            {
                if (Program.mw.textBox_newToolsIn_lifespan.Text.Length <= 0)
                    e.Handled = true;                                     //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(Program.mw.textBox_newToolsIn_lifespan.Text, out oldf);
                    b2 = float.TryParse(Program.mw.textBox_newToolsIn_lifespan.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
                    }
                }
            }
        }

        public void textBox_newToosIn_operator_Enter()              //新购工装入库界面 操作人 文本框默认值
        {
            if (Program.mw.textBox_newToolsIn_operator.Text == "员工编号 ")
            {
                Program.mw.textBox_newToolsIn_operator.Text = "";
                Program.mw.textBox_newToolsIn_operator.ForeColor = Color.Black;
                Program.mw.textBox_newToolsIn_operator.TextAlign = HorizontalAlignment.Left;
            }
        }
        public void textBox_newToosIn_operator_Leave()
        {
            if (String.IsNullOrEmpty(Program.mw.textBox_newToolsIn_operator.Text))
            {
                Program.mw.textBox_newToolsIn_operator.Text = "员工编号 ";
                Program.mw.textBox_newToolsIn_operator.ForeColor = Color.Gray;
                Program.mw.textBox_newToolsIn_operator.TextAlign = HorizontalAlignment.Right;
            }
        }

        //是否批量入库状态变化函数
        public void checkBox_batch_CheckedChanged()
        {
            if (Program.mw.checkBox_newToolsIn_batch.Checked == false)     //非批量入库
            {
                Program.mw.textBox_newToolsIn_codeEnd.BackColor = System.Drawing.Color.LightGray;     //结尾编码变灰
                Program.mw.textBox_newToolsIn_codeEnd.ReadOnly = true;                                //结尾编码只读
            }
            if (Program.mw.checkBox_newToolsIn_batch.Checked == true)     //批量入库
            {
                Program.mw.textBox_newToolsIn_codeEnd.BackColor = Color.White;                        //结尾编码变白 
                Program.mw.textBox_newToolsIn_codeEnd.ReadOnly = false;                              //结尾编码可编辑
            }
        }

        //二维码文本框回车函数
        public void textBox_newToolsIn_code_KeyPress(char e)
        {
            if (e == (char)Keys.Enter)
            {
                String text = Program.mw.textBox_newToolsIn_code.Text;
                String[] temp = null;
                String code = text;
                String number;

                if (text.Contains("-序号："))                             //判断二维码是否包含编号
                {
                    text = text.Remove(0, 5);
                    text = text.Replace("-序号：", "@");
                    temp = text.Split('@');
                    code = temp[0];
                    number = temp[1];

                    if (CommonFunction.checkCodeLegality(code) && CommonFunction.checkNumberLegality(number))//二维码及编号的合法性
                    {
                        Program.mw.textBox_newToolsIn_code.Text = code;
                        Program.mw.textBox_newToolsIn_toolName.Text = number;

                        sql = "select * from tools where code='" + code + "' order by idTools DESC limit 1";  //自动填充已知信息
                        ds = connection.Select(sql);
                        if (ds.Tables[0].Rows[0] != null)
                        {
                            Program.mw.textBox_newToolsIn_materialNumber.Text = ds.Tables[0].Rows[0][3].ToString();
                            Program.mw.textBox_newToolsIn_manufacturer.Text = ds.Tables[0].Rows[0][15].ToString();
                            Program.mw.comboBox_newToolsIn_lifetype.Text = ds.Tables[0].Rows[0][17].ToString();
                            Program.mw.textBox_newToolsIn_lifespan.Text = ds.Tables[0].Rows[0][18].ToString();
                            Program.mw.textBox_newToolsIn_price.Text = ds.Tables[0].Rows[0][16].ToString();

                            Program.mw.textBox_newToolsIn_lifespan.ForeColor = Color.Black;
                            Program.mw.textBox_newToolsIn_lifespan.TextAlign = HorizontalAlignment.Left;
                            Program.mw.textBox_newToolsIn_price.ForeColor = Color.Black;
                            Program.mw.textBox_newToolsIn_price.TextAlign = HorizontalAlignment.Left;
                        }

                        if (Program.mw.checkBox_newToolsIn_batch.Checked == true)                //焦点跳转
                        {
                            Program.mw.textBox_newToolsIn_codeEnd.Focus();
                        }
                        else
                        {
                            Program.mw.textBox_newToolsIn_materialNumber.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("二维码不合法！");
                        Program.mw.textBox_newToolsIn_code.Text = "";
                        Program.mw.textBox_newToolsIn_toolName.Text = "";
                        return;
                    }
                }
                else                             //二维码不包含编号
                {
                    if (CommonFunction.checkCodeLegality(text) && !CommonFunction.HasChinese(text))
                    {
                        Program.mw.textBox_newToolsIn_toolName.Focus();

                        sql = "select * from tools where code='" + code + "' order by idTools DESC limit 1";  //自动填充已知信息
                        ds = connection.Select(sql);
                        if (ds.Tables[0].Rows[0] != null)
                        {
                            Program.mw.textBox_newToolsIn_materialNumber.Text = ds.Tables[0].Rows[0][3].ToString();
                            Program.mw.textBox_newToolsIn_manufacturer.Text = ds.Tables[0].Rows[0][15].ToString();
                            Program.mw.comboBox_newToolsIn_lifetype.Text = ds.Tables[0].Rows[0][17].ToString();
                            Program.mw.textBox_newToolsIn_lifespan.Text = ds.Tables[0].Rows[0][18].ToString();
                            Program.mw.textBox_newToolsIn_price.Text = ds.Tables[0].Rows[0][16].ToString();

                            Program.mw.textBox_newToolsIn_lifespan.ForeColor = Color.Black;
                            Program.mw.textBox_newToolsIn_lifespan.TextAlign = HorizontalAlignment.Left;
                            Program.mw.textBox_newToolsIn_price.ForeColor = Color.Black;
                            Program.mw.textBox_newToolsIn_price.TextAlign = HorizontalAlignment.Left;
                        }
                    }
                    else
                    {
                        MessageBox.Show("二维码不合法！");
                        Program.mw.textBox_newToolsIn_code.Text = "";
                        return;
                    }
                }

            }
        }

        //结尾编码回车函数
        public void textBox_newToolsIn_numberEnd_KeyPress(char e)
        {
            if (e == (char)Keys.Enter)
            {
                String startNumber = Program.mw.textBox_newToolsIn_toolName.Text;
                String endNumber = Program.mw.textBox_newToolsIn_codeEnd.Text;
                if (CommonFunction.checkNumberEndLegality(startNumber, endNumber))
                {
                    Program.mw.textBox_newToolsIn_materialNumber.Focus();
                }
                else
                {
                    MessageBox.Show("编号不合法！");
                    Program.mw.textBox_newToolsIn_codeEnd.Text = "";
                    return;
                }
            }
        }

        ///////////////////////    绘制数据表格
        public void drawListView_newToolsIn(ListView listview)
        {
            listview.Clear();
            int listViewWidth = Screen.PrimaryScreen.Bounds.Width - listview.Location.X * 2 - Program.mw.toolStrip1.Width;
            int listViewHeight = Screen.PrimaryScreen.Bounds.Height - listview.Location.Y - Program.mw.statusStrip1.Height - Program.mw.menuStrip1.Height - 85;
            int listViewColumnWidth = listViewWidth / 12;
            listview.Size = new System.Drawing.Size(listViewWidth, listViewHeight);
            listview.Font = new System.Drawing.Font("微软雅黑", 8F);
            listview.GridLines = true;
            listview.View = View.Details;
            listview.HeaderStyle = ColumnHeaderStyle.Clickable;//表头样式
            listview.FullRowSelect = true;//表示在控件上，是否可以选择一整行
            listview.Columns.Add("", 0, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("序号", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("工装二维码", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("工装编号", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("物料号", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("厂家", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("单价", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("额定寿命", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("购入日期", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("操作人", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("姓名", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("联系方式", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("备注", listViewWidth - 11 * listViewColumnWidth, HorizontalAlignment.Center);
            /*  displaySheet.Location = new System.Drawing.Point(90, 40);
            displaySheet.Size= new System.Drawing.Size(100,100);
            this.Controls.Add(displaySheet);*/
            //this.listView1.BeginUpdate();  //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
        }

        ///////////////////////    填充数据
        public void fillListView_newToolsIn(ListView listview)
        {
            listview.Items.Clear();
            sql = "select * from tools order by idTools DESC limit 20";
            ds = connection.Select(sql);
            int i = 1;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = "";
                lvi.SubItems.Add(i.ToString());
                lvi.SubItems.Add(row[1].ToString());
                lvi.SubItems.Add(row[2].ToString());
                lvi.SubItems.Add(row[3].ToString());
                lvi.SubItems.Add(row[15].ToString());
                lvi.SubItems.Add(row[16].ToString());
                lvi.SubItems.Add(row[18].ToString());
                lvi.SubItems.Add(row[22].ToString());
                lvi.SubItems.Add(row[11].ToString());
                lvi.SubItems.Add(row[11].ToString());           //*
                lvi.SubItems.Add(row[11].ToString());           //*
                lvi.SubItems.Add(row[33].ToString());
                listview.Items.Add(lvi);
                i++;
            }
            listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);   // 填充完数据之后，列宽设置为自适应
            listview.Columns[0].Width = 0;
            connection.Close();
        }

        ///////////////////////    新购工装入库确认按钮函数
        public bool newToolsIn_enter()
        {
            String[] newTools = new String[18];
            newTools[1] = Program.mw.textBox_newToolsIn_code.Text;
            newTools[2] = Program.mw.textBox_newToolsIn_remarks.Text;
            newTools[3] = Program.mw.comboBox_newToolsIn_functionState.Text;
            newTools[4] = Program.mw.textBox_newToolsIn_toolName.Text;
            newTools[5] = Program.mw.textBox_newToolsIn_codeEnd.Text;
            newTools[6] = Program.mw.textBox_newToolsIn_materialNumber.Text;
            newTools[7] = Program.mw.textBox_newToolsIn_manufacturer.Text;
            newTools[8] = Program.mw.dateTimePicker_newToolsIn_purchaseDate.Text;
            newTools[9] = Program.mw.comboBox_newToolsIn_lifetype.Text;
            newTools[10] = Program.mw.textBox_newToolsIn_lifespan.Text;
            newTools[11] = Program.mw.textBox_newToolsIn_price.Text;
            newTools[12] = Program.mw.textBox_newToolsIn_operator.Text;
            newTools[13] = Program.mw.textBox_newToolsIn_name.Text;                    //*未添加至数据库
            newTools[14] = Program.mw.textBox_newToolsIn_contact.Text;                 //*未添加至数据库
            newTools[15] = Program.mw.textBox_newToolsIn_area.Text;
            newTools[16] = Program.mw.textBox_newToolsIn_shelf.Text;
            newTools[17] = Program.mw.textBox_newToolsIn_layer.Text;

            //校验各项数据
            if (!CommonFunction.checkCodeLegality(newTools[1]))
            {
                MessageBox.Show("二维码不合法！");
                return false;
            }
            if (!CommonFunction.checkNumberLegality(newTools[4]))
            {
                MessageBox.Show("编号不合法！");
                return false;
            }
            if (Program.mw.checkBox_newToolsIn_batch.Checked)
            {
                if (!CommonFunction.checkNumberEndLegality(newTools[4], newTools[5]))
                {
                    MessageBox.Show("编号不合法！");
                    return false;
                }
            }

            String[] classes = new String[8];           //根据二维码获取7级类别
            classes = newTools[1].Split('-');

            if (!Program.mw.checkBox_newToolsIn_batch.Checked)              //非批量入库
            {
                try
                {
                    sql = @"insert into tools 
                       (code,remarks,functionState,number,materialNumber,manufacturer,purchaseDate,lifetype,lifespan,price,operator,area,shelf,layer,class1,class2,class3,class4,class5,class6,class7,version) 
                        values (

                                 '" + newTools[1] + "'," +
                                "'" + newTools[2] + "'," +
                                "'" + newTools[3] + "'," +
                                "'" + newTools[4] + "'," +
                                "'" + newTools[6] + "'," +
                                "'" + newTools[7] + "'," +
                                "'" + newTools[8] + "'," +
                                "'" + newTools[9] + "'," +
                                "'" + newTools[10] + "'," +
                                "'" + newTools[11] + "'," +
                                "'" + newTools[12] + "'," +
                                "'" + newTools[15] + "'," +
                                "'" + newTools[16] + "'," +
                                "'" + newTools[17] + "'," +
                                "'" + classes[0] + "'," +
                                "'" + classes[1] + "'," +
                                "'" + classes[2] + "'," +
                                "'" + classes[3] + "'," +
                                "'" + classes[4] + "'," +
                                "'" + classes[5] + "'," +
                                "'" + classes[6] + "'," +
                                "'" + classes[7] + "')";

                    connection.Insert(sql);

                    sql = @"insert into records 
                       (code,number,materialNumber,area,shelf,layer,functionState,lifetype,lifespan,lifeleft,class1,class2,class3,class4,class5,class6,class7,version,operationType,operationDate,operationTime,operator,username,remarks) 
                        values (
                                 '" + newTools[1] + "'," +
                                "'" + newTools[4] + "'," +
                                "'" + newTools[6] + "'," +
                                "'" + newTools[15] + "'," +
                                "'" + newTools[16] + "'," +
                                "'" + newTools[17] + "'," +
                                "'" + newTools[3] + "'," +
                                "'" + newTools[9] + "'," +
                                "'" + newTools[10] + "'," +
                                //lifeleft
                                "'" + classes[0] + "'," +
                                "'" + classes[1] + "'," +
                                "'" + classes[2] + "'," +
                                "'" + classes[3] + "'," +
                                "'" + classes[4] + "'," +
                                "'" + classes[5] + "'," +
                                "'" + classes[6] + "'," +
                                "'" + classes[7] + "'," +
                                "'" + "新购入库" + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                                "'" + DateTime.Now.ToString("hh:mm:ss") + "'" +
                                "'" + newTools[12] + "'," +
                                "'" + MainWindow.TerminalNumber + "'," +
                                "'" + newTools[2] + "')";

                    connection.Insert(sql);
                    fillListView_newToolsIn(Program.mw.listView_newToolsIn);
                    connection.Close();
                    return true;
                }
                catch
                {
                    MessageBox.Show("数据保存失败！");
                    return false;
                }
            }
            else                    //批量入库
            {
                String startNumber = newTools[4].Substring(newTools[4].LastIndexOf("-") + 1);
                String endNumber = newTools[5].Substring(newTools[5].LastIndexOf("-") + 1);
                try
                {
                    for (int i = 1; i <= (int.Parse(endNumber) - int.Parse(startNumber) + 1); i++)
                    {
                        connection.Insert(sql);
                        newTools[4] = newTools[4].Remove(newTools[4].LastIndexOf("-") + 1) + (int.Parse(startNumber) + i).ToString().PadLeft(startNumber.Length, '0'); //SQL字符串需手动更新一下
                        sql = @"insert into tools 
                               (code,remarks,functionState,number,materialNumber,manufacturer,purchaseDate,lifetype,lifespan,price,operator,area,shelf,layer,class1,class2,class3,class4,class5,class6,class7,version) 
                                values (
                                         '" + newTools[1] + "'," +
                                        "'" + newTools[2] + "'," +
                                        "'" + newTools[3] + "'," +
                                        "'" + newTools[4] + "'," +
                                        "'" + newTools[6] + "'," +
                                        "'" + newTools[7] + "'," +
                                        "'" + newTools[8] + "'," +
                                        "'" + newTools[9] + "'," +
                                        "'" + newTools[10] + "'," +
                                        "'" + newTools[11] + "'," +
                                        "'" + newTools[12] + "'," +
                                        "'" + newTools[15] + "'," +
                                        "'" + newTools[16] + "'," +
                                        "'" + newTools[17] + "'," +
                                        "'" + classes[0] + "'," +
                                        "'" + classes[1] + "'," +
                                        "'" + classes[2] + "'," +
                                        "'" + classes[3] + "'," +
                                        "'" + classes[4] + "'," +
                                        "'" + classes[5] + "'," +
                                        "'" + classes[6] + "'," +
                                        "'" + classes[7] + "')";

                    }
                    fillListView_newToolsIn(Program.mw.listView_newToolsIn);
                    connection.Close();
                    return true;
                }
                catch
                {
                    MessageBox.Show("数据保存失败！");
                    return false;
                }

            }

        }

        //清除功能
        public void newToolsInCleanAll()
        {
            Program.mw.textBox_newToolsIn_materialNumber.Text = "";
            Program.mw.textBox_newToolsIn_manufacturer.Text = "";
            Program.mw.dateTimePicker_newToolsIn_purchaseDate.ResetText();
            Program.mw.textBox_newToolsIn_lifespan.Text = "";
            Program.mw.textBox_newToolsIn_price.Text = "";
            Program.mw.textBox_newToolsIn_operator.Text = "";
            Program.mw.textBox_newToolsIn_name.Text = "";
            Program.mw.textBox_newToolsIn_contact.Text = "";
            Program.mw.textBox_newToolsIn_area.Text = "";
            Program.mw.textBox_newToolsIn_shelf.Text = "";
            Program.mw.textBox_newToolsIn_layer.Text = "";
            Program.mw.textBox_newToolsIn_toolName.Text = "";
            Program.mw.textBox_newToolsIn_code.Text = "";
            Program.mw.textBox_newToolsIn_remarks.Text = "";
            Program.mw.textBox_newToolsIn_codeEnd.Text = "";
            Program.mw.comboBox_newToolsIn_functionState.Text = "正常";
            Program.mw.comboBox_newToolsIn_lifetype.Text = "时间";
            Program.mw.textBox_newToolsIn_lifespan.Text = "天 ";
            Program.mw.textBox_newToolsIn_lifespan.ForeColor = Color.Gray;
            Program.mw.textBox_newToolsIn_lifespan.TextAlign = HorizontalAlignment.Right;
            Program.mw.textBox_newToolsIn_price.Text = "元 ";
            Program.mw.textBox_newToolsIn_price.ForeColor = Color.Gray;
            Program.mw.textBox_newToolsIn_price.TextAlign = HorizontalAlignment.Right;
            Program.mw.textBox_newToolsIn_operator.Text = "员工编号 ";
            Program.mw.textBox_newToolsIn_operator.ForeColor = Color.Gray;
            Program.mw.textBox_newToolsIn_operator.TextAlign = HorizontalAlignment.Right;
        }


        /********************************************************领用归还界面******************************************************/

        //清除功能
        public void toolsReturnCleanALL()
        {
            Program.mw.textBox_toolsReturn_materialNumber.Text = "";
            Program.mw.dateTimePicker_toolsReturn_returnTime.ResetText();
            Program.mw.textBox_toolsReturn_returnLine.Text = "";
            Program.mw.textBox_toolsReturn_operator.Text = "";
            Program.mw.textBox_toolsReturn_name.Text = "";
            Program.mw.textBox_toolsReturn_contact.Text = "";
            Program.mw.textBox_toolsReturn_return.Text = "";
            Program.mw.textBox_toolsReturn_area.Text = "";
            Program.mw.textBox_toolsReturn_shelf.Text = "";
            Program.mw.textBox_toolsReturn_layer.Text = "";
            Program.mw.textBox_toolsReturn_toolName.Text = "";
            Program.mw.textBox_toolsReturn_code.Text = "";
            Program.mw.textBox_toolsReturn_remarks.Text = "";
            Program.mw.textBox_toolsReturnOperator_name.Text = "";
            Program.mw.textBox_toolsReturnOperator_contact.Text = "";
            Program.mw.textBox_toolsReturn_operator.Text = "";
            Program.mw.comboBox_toolsReturn_functionState.Text = "正常";
            Program.mw.textBox_toolsReturn_operator.Text = "员工编号 ";
            Program.mw.textBox_toolsReturn_operator.ForeColor = Color.Gray;
            Program.mw.textBox_toolsReturn_operator.TextAlign = HorizontalAlignment.Right;
            Program.mw.textBox_toolsReturn_return.Text = "员工编号 ";
            Program.mw.textBox_toolsReturn_return.ForeColor = Color.Gray;
            Program.mw.textBox_toolsReturn_return.TextAlign = HorizontalAlignment.Right;
        }

        //领用归还界面 归还人 文本框默认值函数
        public void textBox_toolsReturn_return_Enter()
        {
            if (Program.mw.textBox_toolsReturn_return.Text == "员工编号 ")
            {
                Program.mw.textBox_toolsReturn_return.Text = "";
                Program.mw.textBox_toolsReturn_return.ForeColor = Color.Black;
                Program.mw.textBox_toolsReturn_return.TextAlign = HorizontalAlignment.Left;
            }
        }
        public void textBox_toolsReturn_return_Leave()
        {
            if (String.IsNullOrEmpty(Program.mw.textBox_toolsReturn_return.Text))
            {
                Program.mw.textBox_toolsReturn_return.Text = "员工编号 ";
                Program.mw.textBox_toolsReturn_return.ForeColor = Color.Gray;
                Program.mw.textBox_toolsReturn_return.TextAlign = HorizontalAlignment.Right;
            }
        }
        //领用归还界面 操作人 文本框默认值函数
        public void textBox_toolsReturn_operator_Enter()
        {
            if (Program.mw.textBox_toolsReturn_operator.Text == "员工编号 ")
            {
                Program.mw.textBox_toolsReturn_operator.Text = "";
                Program.mw.textBox_toolsReturn_operator.ForeColor = Color.Black;
                Program.mw.textBox_toolsReturn_operator.TextAlign = HorizontalAlignment.Left;
            }
        }
        public void textBox_toolsReturn_operator_Leave()
        {
            if (String.IsNullOrEmpty(Program.mw.textBox_toolsReturn_operator.Text))
            {
                Program.mw.textBox_toolsReturn_operator.Text = "员工编号 ";
                Program.mw.textBox_toolsReturn_operator.ForeColor = Color.Gray;
                Program.mw.textBox_toolsReturn_operator.TextAlign = HorizontalAlignment.Right;
            }
        }
        /********************************************************维修入库界面******************************************************/
        //清除功能
        public void toolsRepairCleanALL()
        {
            Program.mw.textBox_repairtoolsIn_code.Text = "";
            Program.mw.textBox_repairtoolsIn_toolName.Text = "";
            Program.mw.comboBox_repairtoolsIn_functionState.Text = "正常";
            Program.mw.textBox_repairtoolsIn_remarks.Text = "";
            
            Program.mw.textBox_repairtoolsIn_manufacturer.Text = "";
            Program.mw.dateTimePicker_repairtoolsIn_repairDate.ResetText();
            Program.mw.comboBox_repairtoolsIn_lifetype.Text = "时间";
            Program.mw.textBox_repairtoolsIn_area.Text = "";
            Program.mw.textBox_repairtoolsIn_layer.Text = "";
            Program.mw.textBox_repairtoolsIn_shelf.Text = "";
            Program.mw.textBox_repairtoolsIn_materialNumber.Text = "";
            Program.mw.textBox_repairtoolsIn_lifespan.Text = "天 ";
            Program.mw.textBox_repairtoolsIn_lifespan.ForeColor = Color.Gray;
            Program.mw.textBox_repairtoolsIn_lifespan.TextAlign = HorizontalAlignment.Right;
            Program.mw.textBox_repairtoolsIn_repairTimes.Text = "";
            Program.mw.textBox_repairtoolsIn_repairTimes.Text = "";
            Program.mw.textBox_repairtoolsIn_name.Text = "";
            Program.mw.textBox_repairtoolsIn_contact.Text = "";
            Program.mw.textBox_repairtoolsIn_operator.Text = "员工编号 ";
            Program.mw.textBox_repairtoolsIn_operator.ForeColor = Color.Gray;
            Program.mw.textBox_repairtoolsIn_operator.TextAlign = HorizontalAlignment.Right;
        }
        public void textBox_repairtoolsIn_operator_Leave()
        {
            if (String.IsNullOrEmpty(Program.mw.textBox_repairtoolsIn_operator.Text))
            {
                Program.mw.textBox_repairtoolsIn_operator.Text = "员工编号 ";
                Program.mw.textBox_repairtoolsIn_operator.ForeColor = Color.Gray;
                Program.mw.textBox_repairtoolsIn_operator.TextAlign = HorizontalAlignment.Right;
            }
        }
        public void textBox_repairtoolsIn_operator_Enter()
        {
            if (Program.mw.textBox_repairtoolsIn_operator.Text == "员工编号 ")
            {
                Program.mw.textBox_repairtoolsIn_operator.Text = "";
                Program.mw.textBox_repairtoolsIn_operator.ForeColor = Color.Black;
                Program.mw.textBox_repairtoolsIn_operator.TextAlign = HorizontalAlignment.Left;
            }
        }
        public void textBox_repairtoolsIn_lifespan_Enter()
        {
            if (Program.mw.comboBox_repairtoolsIn_lifetype.Text == "时间")
            {
                if (Program.mw.textBox_repairtoolsIn_lifespan.Text == "天 ")
                {
                    Program.mw.textBox_repairtoolsIn_lifespan.Text = "";
                    Program.mw.textBox_repairtoolsIn_lifespan.ForeColor = Color.Black;
                    Program.mw.textBox_repairtoolsIn_lifespan.TextAlign = HorizontalAlignment.Left;
                }
            }
            else
            {
                if (Program.mw.textBox_repairtoolsIn_lifespan.Text == "次 ")
                {
                    Program.mw.textBox_repairtoolsIn_lifespan.Text = "";
                    Program.mw.textBox_repairtoolsIn_lifespan.ForeColor = Color.Black;
                    Program.mw.textBox_repairtoolsIn_lifespan.TextAlign = HorizontalAlignment.Left;
                }
            } 
        }
        public void textBox_repairtoolsIn_lifespan_Leave()
        {
            if(Program.mw.comboBox_repairtoolsIn_lifetype.Text == "时间")
            {
                if (String.IsNullOrEmpty(Program.mw.textBox_repairtoolsIn_lifespan.Text))
                {
                    Program.mw.textBox_repairtoolsIn_lifespan.Text = "天 ";
                    Program.mw.textBox_repairtoolsIn_lifespan.ForeColor = Color.Gray;
                    Program.mw.textBox_repairtoolsIn_lifespan.TextAlign = HorizontalAlignment.Right;
                }
            }
            else
            {
                if (String.IsNullOrEmpty(Program.mw.textBox_repairtoolsIn_lifespan.Text))
                {
                    Program.mw.textBox_repairtoolsIn_lifespan.Text = "次 ";
                    Program.mw.textBox_repairtoolsIn_lifespan.ForeColor = Color.Gray;
                    Program.mw.textBox_repairtoolsIn_lifespan.TextAlign = HorizontalAlignment.Right;
                }
            }

  
        }
    }
}
