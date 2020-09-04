using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using DataGridViewAutoFilter;

//工装入库类
namespace nsStockManage
{
    class ToolsIn
    {
        String sql = null;
        public static DataTable dtToolsReturn = new DataTable();
        

        /*****************************************    新购工装入库界面     ********************************************/

        // 新购工装入库界面 按键函数 //

        public void TextBox_newToolsIn_code_KeyPress(char e)            //编码文本框回车函数
        {
            if (e == (char)Keys.Enter)
            {
                if (Program.mw.checkBox_newToolsIn_batch.Checked == true)
                {
                    Program.mw.textBox_newToolsIn_endCode.Focus();
                }
                else
                {
                    Program.mw.textBox_newToolsIn_toolName.Focus();
                }
            }
        }
        
        public void TextBox_newToolsIn_endCode_KeyPress(char e)         //结尾编码文本框回车函数
        {
            if (e == (char)Keys.Enter)
            {
                Program.mw.textBox_newToolsIn_toolName.Focus();
            }
        }

        public void TextBox_newToolsIn_price_KeyPress(KeyPressEventArgs e)       //限制价格编辑框只能输入数字和小数点
        {
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46 && (int)e.KeyChar != 9)    //非数字和小数点不做处理
                e.Handled = true;

            if ((int)e.KeyChar == 46)                                     //小数点的处理
            {
                if (Program.mw.textBox_newToolsIn_price.Text.Length <= 0)   //小数点不能在第一位
                    e.Handled = true;
                else
                {
                    float f;        //小数点不能重复输入
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(Program.mw.textBox_newToolsIn_price.Text, out oldf);
                    b2 = float.TryParse(Program.mw.textBox_newToolsIn_price.Text + e.KeyChar.ToString(), out f);
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

        public void TextBox_newToolsIn_operator_KeyPress(char e)         //操作人文本框回车函数
        {
            if (e == (char)Keys.Enter)
            {
                String operator1 = Program.mw.textBox_newToolsIn_operator.Text;
                sql = "select * from personal where employeeID='" + operator1 + "' limit 1";  //自动填充员工信息
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
                {
                    Program.mw.textBox_newToolsIn_shelf.Focus();
                }
                else
                {
                    Program.mw.textBox_newToolsIn_operatorName.Focus();
                }
            }
        }

        public void TextBox_newToolsIn_shelf_KeyPress(char e)            //架号文本框回车函数
        {
            if (e == (char)Keys.Enter)
            {
                String area = Program.mw.textBox_newToolsIn_shelf.Text;
                String[] temp = area.Split('-');
                if (temp.Length >= 3)
                {
                    Program.mw.button_newToolsIn_enter.Focus();
                }
                else
                {
                    Program.mw.textBox_newToolsIn_layer.Focus();
                }
            }
        }

        //是否批量入库 状态变化函数
        public void CheckBox_batch_CheckedChanged()
        {
            if (Program.mw.checkBox_newToolsIn_batch.Checked)     //批量入库
            {
                Program.mw.textBox_newToolsIn_endCode.BackColor = Color.White;                      //结尾编码变白 
                Program.mw.textBox_newToolsIn_endCode.Enabled = true;                               //结尾编码可编辑
            }
            else     //非批量入库
            {
                Program.mw.textBox_newToolsIn_endCode.BackColor = Color.LightGray;                  //结尾编码变灰
                Program.mw.textBox_newToolsIn_endCode.Enabled = false;                              //结尾编码只读
            }
        }
        
        public void ComboBox_newToolsIn_lifetype_SelectedIndexChanged()         //额定寿命类型选取
        {
            if (Program.mw.comboBox_newToolsIn_lifetype.Text == "时间")
            {
                Program.mw.textBox_newToolsIn_lifespan.Text = "天 ";
                CommonFunction.TextboxLeave(Program.mw.textBox_newToolsIn_lifespan);
            }
            else // "次数"
            {
                Program.mw.textBox_newToolsIn_lifespan.Text = "次 ";
                CommonFunction.TextboxLeave(Program.mw.textBox_newToolsIn_lifespan);
            }
        }

        // 新购工装入库界面 焦点函数 //
        public void TextBox_newToolsIn_lifespan_Enter()                 //额定寿命 文本框获得焦点
        {
            if ((Program.mw.textBox_newToolsIn_lifespan.Text == "天 ") || (Program.mw.textBox_newToolsIn_lifespan.Text == "次 "))
            {
                Program.mw.textBox_newToolsIn_lifespan.Text = "";
                CommonFunction.TextboxEnter(Program.mw.textBox_newToolsIn_lifespan);
            }
        }
        public void TextBox_newToosIn_lifespan_Leave()                  //额定寿命 文本框失去焦点
        {
            if (String.IsNullOrEmpty(Program.mw.textBox_newToolsIn_lifespan.Text))
            {
                if(Program.mw.comboBox_newToolsIn_lifetype.Text == "时间")
                {
                    Program.mw.textBox_newToolsIn_lifespan.Text = "天 ";
                }
                else   //"次数"
                {
                    Program.mw.textBox_newToolsIn_lifespan.Text = "次 ";
                }
                CommonFunction.TextboxLeave(Program.mw.textBox_newToolsIn_lifespan);
            }
        }

        public void TextBox_newToosIn_price_Enter()                 //单价 文本框获得焦点
        {
            if (Program.mw.textBox_newToolsIn_price.Text == "元 ")
            {
                Program.mw.textBox_newToolsIn_price.Text = "";
                CommonFunction.TextboxEnter(Program.mw.textBox_newToolsIn_price);
            }
        }
        public void TextBox_newToosIn_price_Leave()                 //单价 文本框失去焦点
        {
            if (String.IsNullOrEmpty(Program.mw.textBox_newToolsIn_price.Text))
            {
                Program.mw.textBox_newToolsIn_price.Text = "元 ";
                CommonFunction.TextboxLeave(Program.mw.textBox_newToolsIn_price);
            }
        }

        public void TextBox_newToosIn_operator_Enter()              //操作人 文本框获得焦点
        {
            if (Program.mw.textBox_newToolsIn_operator.Text == "员工编号 ")
            {
                Program.mw.textBox_newToolsIn_operator.Text = "";
                CommonFunction.TextboxEnter(Program.mw.textBox_newToolsIn_operator);
            }
        }
        public void TextBox_newToosIn_operator_Leave()              //操作人 文本框失去焦点
        {
            if (String.IsNullOrEmpty(Program.mw.textBox_newToolsIn_operator.Text))
            {
                Program.mw.textBox_newToolsIn_operator.Text = "员工编号 ";
                CommonFunction.TextboxLeave(Program.mw.textBox_newToolsIn_operator);
            }
            else
            {
                String operator1 = Program.mw.textBox_newToolsIn_operator.Text;
                sql = "select * from personal where employeeID='" + operator1 + "' limit 1;";
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count >0 && ds.Tables[0].Rows.Count > 0)
                {
                    Program.mw.textBox_newToolsIn_operatorName.Text = ds.Tables[0].Rows[0][2].ToString();
                    Program.mw.textBox_newToolsIn_operatorContact.Text = ds.Tables[0].Rows[0][5].ToString();
                }
            }
        }

        public void TextBox_newToosIn_shelf_Leave()              //架号 文本框失去焦点
        {
            String area = Program.mw.textBox_newToolsIn_shelf.Text;
            if (area.Length > 5)
            {
                String[] temp = area.Split('-');
                if (temp.Length >= 3)
                {
                    Program.mw.textBox_newToolsIn_shelf.Text = temp[1];
                    Program.mw.textBox_newToolsIn_layer.Text = temp[2];
                    Program.mw.textBox_newToolsIn_position.Text = temp[3];
                }
            }
        }

        public void TextBox_newToolsIn_code_Leave()                 //编码文本框失去焦点函数
        {
            String code = Program.mw.textBox_newToolsIn_code.Text;
            String[] temp = null;
            String category;
            String materialNumber;
            String number;

            if (code.Length > 0)        //有编码
            {
                if (CommonFunction.CheckCodeLegality(code))            //判断编码合法性
                {
                    temp = code.Split('-');
                    category = temp[0];
                    materialNumber = temp[1];
                    number = temp[2];

                    Program.mw.textBox_newToolsIn_materialNumber.Text = materialNumber;

                    sql = "select * from tools where materialNumber='" + materialNumber + "' order by idTools DESC limit 1";  //自动填充已知信息
                    DataSet ds = MainWindow.connection.Select(sql);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        Program.mw.textBox_newToolsIn_toolName.Text = ds.Tables[0].Rows[0][1].ToString();
                        Program.mw.textBox_newToolsIn_description.Text = ds.Tables[0].Rows[0][28].ToString();
                        Program.mw.textBox_newToolsIn_manufacturer.Text = ds.Tables[0].Rows[0][17].ToString();
                        Program.mw.textBox_newToolsIn_price.Text = ds.Tables[0].Rows[0][18].ToString();
                        Program.mw.comboBox_newToolsIn_lifetype.Text = ds.Tables[0].Rows[0][19].ToString();
                        Program.mw.textBox_newToolsIn_lifespan.Text = ds.Tables[0].Rows[0][20].ToString();
                        Program.mw.textBox_newToolsIn_shelf.Text = ds.Tables[0].Rows[0][6].ToString();
                        Program.mw.textBox_newToolsIn_layer.Text = ds.Tables[0].Rows[0][7].ToString();
                        Program.mw.textBox_newToolsIn_position.Text = ds.Tables[0].Rows[0][8].ToString();

                        CommonFunction.TextboxEnter(Program.mw.textBox_newToolsIn_lifespan);
                        CommonFunction.TextboxEnter(Program.mw.textBox_newToolsIn_price);
                        //展示库存
                        CommonFunction.Fill_GroupBox_Info(Program.mw.panel21_newToolsIn, materialNumber, Program.mw.button_newToolsIn_Clear);

                    }
                }
                else
                {
                    MessageBox.Show("工装编码有误！");
                }
            }
            return;
        }
        
        //结尾编码失去焦点函数
        public void TextBox_newToolsIn_endCode_Leave()
        {
            String startCode = Program.mw.textBox_newToolsIn_code.Text;
            String endCode = Program.mw.textBox_newToolsIn_endCode.Text;
            if(endCode.Length > 0)
            {
                if (!CommonFunction.CheckEndCodeLegality(startCode, endCode))
                {
                    MessageBox.Show("结尾编码有误！");
                }
            }
            return;
        }

        // 新购工装入库界面 按钮函数 //
        
        public bool NewToolsIn_enter()      //    新购工装入库确认按钮函数
        {
            String code = Program.mw.textBox_newToolsIn_code.Text;
            String endCode = Program.mw.textBox_newToolsIn_endCode.Text;
            String functionState = Program.mw.comboBox_newToolsIn_functionState.Text;
            String toolName = Program.mw.textBox_newToolsIn_toolName.Text;
            String description = Program.mw.textBox_newToolsIn_description.Text;
            String remarks = Program.mw.textBox_newToolsIn_remarks.Text;
            String materialNumber = Program.mw.textBox_newToolsIn_materialNumber.Text;
            String lifetype = Program.mw.comboBox_newToolsIn_lifetype.Text;
            String lifespan = Program.mw.textBox_newToolsIn_lifespan.Text;
            String safetyStock = Program.mw.textBox_newToolsIn_safetyStock.Text;
            String manufacturer = Program.mw.textBox_newToolsIn_manufacturer.Text;
            String price = Program.mw.textBox_newToolsIn_price.Text;
            String operator1 = Program.mw.textBox_newToolsIn_operator.Text;
            String operatorName = Program.mw.textBox_newToolsIn_operatorName.Text;
            String operatorContact = Program.mw.textBox_newToolsIn_operatorContact.Text;
            String shelf = Program.mw.textBox_newToolsIn_shelf.Text;
            String layer = Program.mw.textBox_newToolsIn_layer.Text;
            String position = Program.mw.textBox_newToolsIn_position.Text;
            String storageState = "未上架";

            //校验各项数据
            if (code.Length <= 0)
            {
                MessageBox.Show("编码不能为空！");
                return false;
            }
            if (!CommonFunction.CheckCodeLegality(code))
            {
                MessageBox.Show("编码有误！");
                return false;
            }
            if (Program.mw.checkBox_newToolsIn_batch.Checked)
            {
                if(endCode.Length <= 0)
                {
                    MessageBox.Show("结尾编码不能为空！");
                    return false;
                }
                if (!CommonFunction.CheckEndCodeLegality(code, endCode))
                {
                    MessageBox.Show("结尾编码有误！");
                    return false;
                }
                String startNumber = code.Substring(code.Length - 4, 4);
                String endNumber = endCode.Substring(endCode.Length - 4, 4);
                if (int.Parse(endNumber) - int.Parse(startNumber) > 1000)
                {
                    MessageBox.Show("批量入库不能超过1000件/次！");
                    return false;
                }
            }
            if (operator1.Length != 8 || CommonFunction.HasChinese(operator1))
            {
                MessageBox.Show("请填写正确的操作人工号！");
                return false;
            }
            sql = "select * from tools where code='" + code + "';";
            DataSet ds = MainWindow.connection.Select(sql);
            if(ds.Tables.Count >0 && ds.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show("此工装已存在！");
                return false;
            }

            String[] temp = code.Split('-');
            String category = temp[0];
            String materialNumber1 = temp[1];
            String number = temp[2];

            if (shelf.Length > 0 && layer.Length > 0)
            {
                storageState = "已上架";
            }
            if (price.Contains("元"))
            {
                price = "0";
            }
            if(lifespan.Contains("天") || lifespan.Contains("次"))
            {
                lifespan = "";
            }

            if (!Program.mw.checkBox_newToolsIn_batch.Checked)              //非批量入库
            {
                try
                {
                    sql = @"INSERT INTO tools 
                       (toolName,code,category,materialNumber,number,shelf,layer,position,storageState,operator,functionState,manufacturer,price,lifeType,lifeSpan,lifeLeft,purchaseDate,remarks,description) 
                        values (
                                 '" + toolName + "'," +
                                "'" + code + "'," +
                                "'" + category + "'," +
                                "'" + materialNumber + "'," +
                                "'" + number + "'," +
                                "'" + shelf + "'," +
                                "'" + layer + "'," +
                                "'" + position + "'," +
                                "'" + storageState + "'," +
                                "'" + operator1 + "'," +
                                "'" + functionState + "'," +
                                "'" + manufacturer + "'," +
                                "'" + price + "'," +
                                "'" + lifetype + "'," +
                                "'" + lifespan + "'," +
                                "'" + lifespan + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                "'" + remarks + "'," +
                                "'" + description + "')";

                    if (MainWindow.connection.Insert(sql))
                    {
                        sql = @"INSERT INTO records 
                                   (toolName,code,category,materialNumber,number,shelf,layer,position,functionState,lifetype,lifespan,lifeleft,operationType,operationDate,operationTime,operator,operatorName,terminal,remarks,storageState,description) 
                            values (
                                     '" + toolName + "'," +
                                    "'" + code + "'," +
                                    "'" + category + "'," +
                                    "'" + materialNumber + "'," +
                                    "'" + number + "'," +
                                    "'" + shelf + "'," +
                                    "'" + layer + "'," +
                                    "'" + position + "'," +
                                    "'" + functionState + "'," +
                                    "'" + lifetype + "'," +
                                    "'" + lifespan + "'," +
                                    "'" + lifespan + "'," +
                                    "'" + "新购入库" + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                    "'" + DateTime.Now.ToString("HH:mm:ss") + "'," +
                                    "'" + operator1 + "'," +
                                    "'" + operatorName + "'," +
                                    "'" + MainWindow.TerminalNumber + "'," +
                                    "'" + remarks + "'," +
                                    "'" + storageState + "'," +
                                    "'" + description + "')";

                        if (!MainWindow.connection.Insert(sql))
                        {
                            MessageBox.Show("添加记录信息失败！");
                        }

                        //设置安全库存
                        if(int.TryParse(safetyStock,out int s) && s >0)
                        {
                            sql = "select * from warningSetup where name = '物料号' AND materialNumber = '" + materialNumber + "' AND type = '安全库存'";
                            ds = MainWindow.connection.Select(sql);
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                sql = "UPDATE warningSetup SET value='" + safetyStock + "' WHERE name = '物料号' AND materialNumber = '" + materialNumber + "' AND type = '安全库存'";
                                if (!MainWindow.connection.Update(sql))
                                {
                                    MessageBox.Show("更新安全库存预警项失败！");
                                }
                            }
                            else
                            {
                                sql = @" INSERT INTO warningSetup
                                         (name,materialNumber,type,method,value,remarks,setupDate)
                                   VALUES('物料号','" + materialNumber + "','安全库存','数量','" + safetyStock + "','新购入库','" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss") + "')";
                                if (!MainWindow.connection.Insert(sql))
                                {
                                    MessageBox.Show("添加安全库存预警项失败！");
                                }
                            }
                        }

                        //更新人员信息库
                        CommonFunction.UpdatePersonalInfo(operator1, operatorName, operatorContact);

                        //刷新数据窗口
                        CommonFunction.FillDataGridView_Records(Program.mw.dataGridView_NewToolsIn, "新购");
                        CommonFunction.Fill_GroupBox_Info(materialNumber);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("添加工装信息失败！");
                        return false;
                    }
                    
                }
                catch(Exception e)
                {
                    MessageBox.Show("入库失败！错误信息："+e.Message);
                    return false;
                }
            }
            else                    //批量入库
            {
                String startNumber = code.Substring(code.Length - 5);         //把序号的年份字母去除
                String endNumber = endCode.Substring(code.Length - 5);
                try
                {
                    sql = @"insert into tools 
                       (toolName,code,category,materialNumber,number,shelf,layer,position,storageState,operator,functionState,manufacturer,price,lifeType,lifeSpan,lifeLeft,purchaseDate,remarks,description) 
                        values (
                                 '" + toolName + "'," +
                                "'" + code + "'," +
                                "'" + category + "'," +
                                "'" + materialNumber + "'," +
                                "'" + number + "'," +
                                "'" + shelf + "'," +
                                "'" + layer + "'," +
                                "'" + position + "'," +
                                "'" + storageState + "'," +
                                "'" + operator1 + "'," +
                                "'" + functionState + "'," +
                                "'" + manufacturer + "'," +
                                "'" + price + "'," +
                                "'" + lifetype + "'," +
                                "'" + lifespan + "'," +
                                "'" + lifespan + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                "'" + remarks + "'," +
                                "'" + description + "')";

                    for (int i = 1; i <= (int.Parse(endNumber) - int.Parse(startNumber) + 1); i++)              //插入其余条
                    {
                        if (MainWindow.connection.Insert(sql))
                        {
                            code = code.Remove(code.Length - 5) + (int.Parse(startNumber) + i).ToString().PadLeft(5, '0'); //SQL字符串需手动更新一下
                            number = code.Substring(code.Length - 6);
                            sql = @"insert into tools 
                                       (toolName,code,category,materialNumber,number,shelf,layer,position,storageState,operator,functionState,manufacturer,price,lifeType,lifeSpan,lifeLeft,purchaseDate,remarks,description) 
                                values (
                                         '" + toolName + "'," +
                                        "'" + code + "'," +
                                        "'" + category + "'," +
                                        "'" + materialNumber + "'," +
                                        "'" + number + "'," +
                                        "'" + shelf + "'," +
                                        "'" + layer + "'," +
                                        "'" + position + "'," +
                                        "'" + storageState + "'," +
                                        "'" + operator1 + "'," +
                                        "'" + functionState + "'," +
                                        "'" + manufacturer + "'," +
                                        "'" + price + "'," +
                                        "'" + lifetype + "'," +
                                        "'" + lifespan + "'," +
                                        "'" + lifespan + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                        "'" + remarks + "'," +
                                        "'" + description + "')";
                        }
                        else
                        {
                            MessageBox.Show("添加第 "+ i +" 条工装信息时失败！");
                            return false;
                        }
                    }

                    sql = @"insert into records 
                                   (toolName,code,category,materialNumber,number,shelf,layer,position,functionState,lifetype,lifespan,lifeleft,operationType,operationDate,operationTime,operator,operatorName,terminal,remarks,storageState,description) 
                            values (
                                 '" + toolName + "'," +
                                "'" + Program.mw.textBox_newToolsIn_code.Text + "'," +
                                "'" + category + "'," +
                                "'" + materialNumber + "'," +
                                "'" + number + "'," +
                                "'" + shelf + "'," +
                                "'" + layer + "'," +
                                "'" + position + "'," +
                                "'" + functionState + "'," +
                                "'" + lifetype + "'," +
                                "'" + lifespan + "'," +
                                "'" + lifespan + "'," +
                                "'" + "新购入库" + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                "'" + DateTime.Now.ToString("HH:mm:ss") + "'," +
                                "'" + operator1 + "'," +
                                "'" + operatorName + "'," +
                                "'" + MainWindow.TerminalNumber + "'," +
                                "'" + remarks + "（批量入库，从" + Program.mw.textBox_newToolsIn_code.Text + "到" + endCode + "）" + "'," +
                                "'" + storageState + "'," +
                                "'" + description + "')";

                    if (!MainWindow.connection.Insert(sql))
                    {
                        MessageBox.Show("添加记录信息失败！");
                    }

                    //设置安全库存
                    if (int.TryParse(safetyStock, out int s) && s > 0)
                    {
                        sql = "select * from warningSetup where name = '物料号' AND materialNumber = '" + materialNumber + "' AND type = '安全库存'";
                        ds = MainWindow.connection.Select(sql);
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            sql = "UPDATE warningSetup SET value='" + safetyStock + "' WHERE name = '物料号' AND materialNumber = '" + materialNumber + "' AND type = '安全库存'";
                            if (!MainWindow.connection.Update(sql))
                            {
                                MessageBox.Show("更新安全库存预警项失败！");
                            }
                        }
                        else
                        {
                            sql = @" INSERT INTO warningSetup
                                     (name,materialNumber,type,method,value,remarks,setupDate)
                               VALUES('物料号','" + materialNumber + "','安全库存','数量','" + safetyStock + "','新购入库','" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss") + "')";
                            if (!MainWindow.connection.Insert(sql))
                            {
                                MessageBox.Show("添加安全库存预警项失败！");
                            }
                        }
                    }

                    //更新人员信息库
                    CommonFunction.UpdatePersonalInfo(operator1, operatorName, operatorContact);

                    //刷新数据窗口
                    CommonFunction.FillDataGridView_Records(Program.mw.dataGridView_NewToolsIn, "新购");
                    CommonFunction.Fill_GroupBox_Info(materialNumber);
                    NewToolsIn_CleanAll();
                    return true;
                }
                catch(Exception e)
                {
                    MessageBox.Show("数据保存失败！错误信息："+ e.Message);
                    return false;
                }
            }

        }

        //清除功能
        public void NewToolsIn_CleanAll()
        {
            Program.mw.textBox_newToolsIn_code.Text = "";
            Program.mw.textBox_newToolsIn_endCode.Text = "";
            Program.mw.comboBox_newToolsIn_functionState.Text = "正常";
            Program.mw.textBox_newToolsIn_toolName.Text = "";
            Program.mw.textBox_newToolsIn_description.Text = "";
            Program.mw.textBox_newToolsIn_remarks.Text = "";
            Program.mw.textBox_newToolsIn_materialNumber.Text = "";
            Program.mw.textBox_newToolsIn_manufacturer.Text = "";
            Program.mw.comboBox_newToolsIn_lifetype.Text = "时间";
            Program.mw.textBox_newToolsIn_lifespan.Text = "天 ";
            CommonFunction.TextboxLeave(Program.mw.textBox_newToolsIn_lifespan);
            Program.mw.textBox_newToolsIn_price.Text = "元 ";
            CommonFunction.TextboxLeave(Program.mw.textBox_newToolsIn_price);
            Program.mw.textBox_newToolsIn_operator.Text = "员工编号 ";
            CommonFunction.TextboxLeave(Program.mw.textBox_newToolsIn_operator);
            Program.mw.textBox_newToolsIn_operatorName.Text = "";
            Program.mw.textBox_newToolsIn_operatorContact.Text = "";
            Program.mw.textBox_newToolsIn_shelf.Text = "";
            Program.mw.textBox_newToolsIn_layer.Text = "";
            Program.mw.textBox_newToolsIn_position.Text = "";
        }
        
        ///////////////////////    绘制数据表格
        


        /***********************************************      领用归还界面      ***********************************************/

        //领用归还界面  按键函数//
        public void TextBox_toolsReturn_code_KeyPress(char e)       //编码文本框 回车函数
        {
            if (e == (char)Keys.Enter)
            {
                if(dtToolsReturn.Rows.Count >= 100)
                {
                    MessageBox.Show("请勿超过100件/次！");
                    return;
                }
                String code = Program.mw.textBox_toolsReturn_code.Text;
                sql = "select * from tools where code='" + code + "' order by idTools DESC limit 1";  //看数据库里是否已有该工装信息
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count >0 && ds.Tables[0].Rows.Count > 0)
                {
                    //检查是否已添加过
                    foreach (DataRow row in dtToolsReturn.Rows)
                    {
                        if (row[1].ToString().Equals(code))
                        {
                            MessageBox.Show("该工装已添加，请勿重复扫码！");
                            return;
                        }
                    }
                    if (!ds.Tables[0].Rows[0][9].ToString().Contains("出借"))    //核对工装存储状态
                    {
                        MessageBox.Show("该工装未出借，无法归还！","注意",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        return;
                    }
                    //如果存在且未添加过，添加进下方的DataGridView中，并清空该文本框
                    int newIndex = dtToolsReturn.Rows.Count + 1;
                    DataRow dr = dtToolsReturn.NewRow();
                    dr[0] = newIndex;                                 //序号
                    dr[1] = ds.Tables[0].Rows[0][2].ToString();       //工装编码
                    dr[2] = ds.Tables[0].Rows[0][4].ToString();       //物料号
                    dr[3] = ds.Tables[0].Rows[0][9].ToString();       //存储状态
                    dr[4] = ds.Tables[0].Rows[0][10].ToString();      //所在线体
                    dr[5] = ds.Tables[0].Rows[0][12].ToString();      //领用人
                    dr[6] = ds.Tables[0].Rows[0][27].ToString();      //备注
                    dr[7] = "正常";                                   //工装状态
                    dtToolsReturn.Rows.Add(dr);
                    Program.mw.dataGridView_toolsReturn.DataSource = dtToolsReturn;
                    Program.mw.textBox_toolsReturn_code.Text = "";
                    //自动填充归还人
                    if(!String.IsNullOrEmpty(ds.Tables[0].Rows[0][12].ToString()))
                    {
                        Program.mw.textBox_toolsReturn_returner.Text = ds.Tables[0].Rows[0][12].ToString();
                        TextBox_toolsReturn_returner_Leave();
                        Program.mw.textBox_toolsReturn_returner.ForeColor = Color.Black;
                        Program.mw.textBox_toolsReturn_returner.TextAlign = HorizontalAlignment.Left;
                    }
                    
                    //展示库存
                    CommonFunction.Fill_GroupBox_Info(Program.mw.panel22_toolsReturn, dr[2].ToString(), Program.mw.button_toolsReturn_Clear);
                }
                else
                {
                    MessageBox.Show("无此工装信息，请核对！");
                }
            }
        }

        public void TextBox_toolsReturn_returner_KeyPress(char e)       //归还人文本框 回车函数
        {
            if (e == (char)Keys.Enter)
            {
                String returner = Program.mw.textBox_toolsReturn_returner.Text;
                sql = "select * from personal where employeeID='" + returner + "' limit 1";  //自动填充员工信息
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Program.mw.textBox_toolsReturn_operator.Focus();
                }
                else
                {
                    Program.mw.textBox_toolsReturn_returnerName.Focus();
                }
            }
        }

        public void TextBox_toolsReturn_operator_KeyPress(char e)               //操作人文本框 回车函数
        {
            if (e == (char)Keys.Enter)
            {
                String operator1 = Program.mw.textBox_toolsReturn_operator.Text;
                sql = "select * from personal where employeeID='" + operator1 + "' limit 1";  //自动填充员工信息
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Program.mw.button_toolsReturn_enter.Focus();
                }
                else
                {
                    Program.mw.textBox_toolsReturn_operatorName.Focus();
                }
            }
        }

        //领用归还界面  焦点函数//
        
        public void TextBox_toolsReturn_returner_Enter()        //归还人文本框 获得焦点
        {
            if (Program.mw.textBox_toolsReturn_returner.Text == "员工编号 ")
            {
                Program.mw.textBox_toolsReturn_returner.Text = "";
                Program.mw.textBox_toolsReturn_returner.ForeColor = Color.Black;
                Program.mw.textBox_toolsReturn_returner.TextAlign = HorizontalAlignment.Left;
            }
        }
        public void TextBox_toolsReturn_returner_Leave()        //归还人文本框 失去焦点
        {
            if (String.IsNullOrEmpty(Program.mw.textBox_toolsReturn_returner.Text))
            {
                Program.mw.textBox_toolsReturn_returner.Text = "员工编号 ";
                Program.mw.textBox_toolsReturn_returner.ForeColor = Color.Gray;
                Program.mw.textBox_toolsReturn_returner.TextAlign = HorizontalAlignment.Right;
            }
            else
            {
                String returner = Program.mw.textBox_toolsReturn_returner.Text;
                sql = "select * from personal where employeeID='" + returner + "' limit 1;";
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Program.mw.textBox_toolsReturn_returnerName.Text = ds.Tables[0].Rows[0][2].ToString();
                    Program.mw.textBox_toolsReturn_returnerContact.Text = ds.Tables[0].Rows[0][5].ToString();
                    Program.mw.textBox_toolsReturn_returnLine.Text = ds.Tables[0].Rows[0][4].ToString();
                }
            }
        }
        
        public void TextBox_toolsReturn_operator_Enter()            //领用归还界面 操作人 文本框获得焦点
        {
            if (Program.mw.textBox_toolsReturn_operator.Text == "员工编号 ")
            {
                Program.mw.textBox_toolsReturn_operator.Text = "";
                Program.mw.textBox_toolsReturn_operator.ForeColor = Color.Black;
                Program.mw.textBox_toolsReturn_operator.TextAlign = HorizontalAlignment.Left;
            }
        }
        public void TextBox_toolsReturn_operator_Leave()            //领用归还界面 操作人 文本框失去焦点
        {
            if (String.IsNullOrEmpty(Program.mw.textBox_toolsReturn_operator.Text))
            {
                Program.mw.textBox_toolsReturn_operator.Text = "员工编号 ";
                Program.mw.textBox_toolsReturn_operator.ForeColor = Color.Gray;
                Program.mw.textBox_toolsReturn_operator.TextAlign = HorizontalAlignment.Right;
            }
            else
            {
                String operator1 = Program.mw.textBox_toolsReturn_operator.Text;
                sql = "select * from personal where employeeID='" + operator1 + "' limit 1;";
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Program.mw.textBox_toolsReturn_operatorName.Text = ds.Tables[0].Rows[0][2].ToString();
                    Program.mw.textBox_toolsReturn_operatorContact.Text = ds.Tables[0].Rows[0][5].ToString();
                }
            }
        }

        //领用归还界面  单击函数//
        ///////////////////////    领用归还入库确认按钮函数
        public bool ToolsReturn_Enter()
        {
            String returnLine = Program.mw.textBox_toolsReturn_returnLine.Text;
            String returner = Program.mw.textBox_toolsReturn_returner.Text;
            String returnerName = Program.mw.textBox_toolsReturn_returnerName.Text;
            String returnerContact = Program.mw.textBox_toolsReturn_returnerContact.Text;
            String operator1 = Program.mw.textBox_toolsReturn_operator.Text;
            String operatorName = Program.mw.textBox_toolsReturn_operatorName.Text;
            String operatorContact = Program.mw.textBox_toolsReturn_operatorContact.Text;

            //校验各项数据
            if(dtToolsReturn.Rows.Count <= 0)
            {
                MessageBox.Show("请先扫入要归还的工装信息！");
                return false;
            }
            if (returner.Length != 8 || CommonFunction.HasChinese(returner) || returnerName.Length < 1)
            {
                MessageBox.Show("请正确填写归还人信息！");
                return false;
            }
            if (operator1.Length != 8 || CommonFunction.HasChinese(operator1) || operatorName.Length < 1)
            {
                MessageBox.Show("请正确填写操作人信息！");
                return false;
            }

            try
            {
                //因涉及工装状态选择，领用归还不允许单件归还
                foreach (DataRow row in dtToolsReturn.Rows)
                {
                    String code = row[1].ToString();
                    String remarks = row[6].ToString();
                    String functionState = row[7].ToString();
                    String[] temp = code.Split('-');
                    String category = temp[0];
                    String materialNumber = temp[1];
                    String number = temp[2];
                    String lifeType = "";
                    int lifeLeft = -1;
                    int lendFrequency = 0;

                    sql = "select * from tools where code='" + code + "' order by idTools DESC limit 1";  //获取该工装寿命类型
                    DataSet ds = MainWindow.connection.Select(sql);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lifeType = ds.Tables[0].Rows[0][19].ToString();
                        int.TryParse(ds.Tables[0].Rows[0][21].ToString(), out lifeLeft);
                        int.TryParse(ds.Tables[0].Rows[0][23].ToString(), out lendFrequency);
                    }

                    //更新工装数据库
                    if (lifeType.Contains("次数") && lifeLeft > 0)
                    {
                        sql = @"update tools set storageState='已上架',line='',workStation='',borrower='',operator='" + operator1
                               + "',lendDuration='0',functionState='" + functionState + "',lifeLeft='" + (lifeLeft - 1)
                               + "',lendFrequency ='" + (lendFrequency + 1) + "',remarks='" + remarks + "' where code='" + code + "';";
                    }
                    else
                    {
                        sql = @"update tools set storageState='已上架',line='',workStation='',borrower='',operator='" + operator1
                            + "',lendDuration='0',functionState='" + functionState + "',lendFrequency='" + (lendFrequency + 1)
                            + "',remarks='" + remarks + "' where code='" + code + "';";
                    }
                    MainWindow.connection.Update(sql);

                    //添加记录数据库
                    sql = @"insert into records 
                                   (code,category,materialNumber,number,functionState,lifeLeft,operationType,operationDate,operationTime,operator,operatorName,terminal,line,borrower,borrowerName,remarks) 
                            values (
                                     '" + code + "'," +
                                    "'" + category + "'," +
                                    "'" + materialNumber + "'," +
                                    "'" + number + "'," +
                                    "'" + functionState + "'," +
                                    "'" + lifeLeft + "'," +
                                    "'" + "领用归还" + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                    "'" + DateTime.Now.ToString("HH:mm:ss") + "'," +
                                    "'" + operator1 + "'," +
                                    "'" + operatorName + "'," +
                                    "'" + MainWindow.TerminalNumber + "'," +
                                    "'" + returnLine + "'," +
                                    "'" + returner + "'," +
                                    "'" + returnerName + "'," +
                                    "'" + remarks + "');";
                    MainWindow.connection.Insert(sql);
                }

                //更新人员信息库
                CommonFunction.UpdatePersonalInfo(returner, returnerName, returnerContact, returnLine);
                CommonFunction.UpdatePersonalInfo(operator1, operatorName, operatorContact);

                //刷新DataGridView数据
                CommonFunction.FillDataGridView_Records(Program.mw.dataGridView_toolsReturn_records, "领用归还");
                ToolsReturn_CleanALL();
                return true;
            }
            catch
            {
                MessageBox.Show("数据保存失败！");
                return false;
            }
        }
        
        //清除功能  删除Datagridview中已录入的所有数据
        public void ToolsReturn_CleanALL()
        {
            Program.mw.textBox_toolsReturn_code.Text = "";
            Program.mw.textBox_toolsReturn_returnLine.Text = "";
            Program.mw.textBox_toolsReturn_returner.Text = "员工编号 ";
            CommonFunction.TextboxLeave(Program.mw.textBox_toolsReturn_returner);
            Program.mw.textBox_toolsReturn_returnerName.Text = "";
            Program.mw.textBox_toolsReturn_returnerContact.Text = "";
            Program.mw.textBox_toolsReturn_operator.Text = "员工编号 ";
            CommonFunction.TextboxLeave(Program.mw.textBox_toolsReturn_operator);
            Program.mw.textBox_toolsReturn_operatorName.Text = "";
            Program.mw.textBox_toolsReturn_operatorContact.Text = "";

            dtToolsReturn.Rows.Clear();
            Program.mw.dataGridView_toolsReturn.DataSource = dtToolsReturn;
        }

        //dataGridView中删除行按钮的单击事件
        public void DataGridView_toolsReturn_CellContentClick(object sender, DataGridViewCellEventArgs e, DataGridView dataGridView)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)      //我也不知道这列序号为啥是0
            {
                DialogResult result = MessageBox.Show("确定删除此行？", "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    dtToolsReturn.Rows[e.RowIndex].Delete();
                    for(int i = 0; i<dtToolsReturn.Rows.Count; i++) //重新编号
                    {
                        dtToolsReturn.Rows[i][0] = i + 1;
                    }
                    dataGridView.DataSource = dtToolsReturn;
                }
                else
                {
                    return;
                }
            }
        }
        
        //datagridview中的comboBox控件值改变事件 （未用到）
        /**第一步：**/
        public void DataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e, DataGridView dataGridView)
        {
            
            ComboBox combox = new ComboBox();
            if (dataGridView.CurrentCell.OwningColumn.Name == "functionState" && dataGridView.CurrentCell.RowIndex != -1)
            {
                //保存当前的事件源。为了触发事件后。再取消
                combox = e.Control as ComboBox;
                combox.SelectedIndexChanged += new EventHandler(DataGridView_ComboBox_SelectedIndexChanged);
            }
        }
        /**第二步：**/
        void DataGridView_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combox = sender as ComboBox;
            //添加离开combox的事件（重要!）
            combox.Leave += new EventHandler(DataGridView_Combox_Leave);
            try
            {
                if (combox.SelectedItem != null)
                {
                    //在这里就可以做值是否改变判断
                    //object preStatus = Program.mw.dataGridView_toolsReturn.CurrentRow.Cells[10].Value;
                    //string preStatusValue = (preStatus == null) ? string.Empty : preStatus.ToString();
                    //MessageBox.Show("11号格：" + preStatusValue);
                    string selectedStatusValue = combox.GetItemText(combox.Items[combox.SelectedIndex]);
                    //if (!preStatusValue.Equals(selectedStatusValue))
                    {
                        //MessageBox.Show("一样！");
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        /**第三步：离开combox时，把事件删除**/
        public void DataGridView_Combox_Leave(object sender, EventArgs e)
        {
            ComboBox combox = sender as ComboBox;
            //做完处理，须撤销动态事件
            combox.SelectedIndexChanged -= new EventHandler(DataGridView_ComboBox_SelectedIndexChanged);
        }


        ///////////////////////    制作dtToolsReturn并填充数据表格

        ///////////////////////    填充记录数据表格
        


        /********************************************************维修入库界面******************************************************/

        //维修入库界面  按键函数//

        public void TextBox_repairtoolsIn_code_KeyPress(char e)       //编码文本框 回车函数
        {
            if (e == (char)Keys.Enter)
            {
                String code = Program.mw.textBox_repairtoolsIn_code.Text;
                sql = "select * from tools where code='" + code + "' order by idTools DESC limit 1";
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Program.mw.textBox_repairtoolsIn_operator.Focus();
                }
                else
                {
                    MessageBox.Show("无此工装信息，请核查！");
                }
            }
        }

        public void TextBox_repairtoolsIn_operator_KeyPress(char e)               //操作人文本框 回车函数
        {
            if (e == (char)Keys.Enter)
            {
                String operator1 = Program.mw.textBox_repairtoolsIn_operator.Text;
                sql = "select * from personal where employeeID='" + operator1 + "' limit 1";  //自动填充员工信息
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Program.mw.button_repairtoolsIn_Enter.Focus();
                }
                else
                {
                    Program.mw.textBox_repairtoolsIn_operatorName.Focus();
                }
            }
        }

        //维修入库界面  焦点函数//

        public void TextBox_repairtoolsIn_code_Leave()                //编码文本框失去焦点函数
        {
            String code = Program.mw.textBox_repairtoolsIn_code.Text;
            String[] temp = null;
            String category;
            String materialNumber;
            String number;

            if (code.Length > 0)
            {
                if (CommonFunction.CheckCodeLegality(code))                //判断编码合法性
                {
                    temp = code.Split('-');
                    category = temp[0];
                    materialNumber = temp[1];
                    number = temp[2];

                    sql = "select * from tools where code='" + code + "' order by idTools DESC limit 1";  //自动填充已知信息
                    DataSet ds = MainWindow.connection.Select(sql);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        Program.mw.textBox_repairtoolsIn_remarks.Text = ds.Tables[0].Rows[0][27].ToString();
                        Program.mw.comboBox_repairtoolsIn_lifeType.Text = ds.Tables[0].Rows[0][19].ToString();
                        Program.mw.textBox_repairtoolsIn_lifeSpan.Text = ds.Tables[0].Rows[0][20].ToString();
                        CommonFunction.TextboxEnter(Program.mw.textBox_repairtoolsIn_lifeSpan);
                        Program.mw.textBox_repairtoolsIn_repairTimes.Text = ds.Tables[0].Rows[0][22].ToString();
                        Program.mw.textBox_repairtoolsIn_shelf.Text = ds.Tables[0].Rows[0][6].ToString();
                        Program.mw.textBox_repairtoolsIn_layer.Text = ds.Tables[0].Rows[0][7].ToString();
                        Program.mw.textBox_repairtoolsIn_position.Text = ds.Tables[0].Rows[0][8].ToString();
                        //展示库存
                        CommonFunction.Fill_GroupBox_Info(Program.mw.panel23_repairtoolsIn, materialNumber, Program.mw.button_repairtoolsIn_Clear);
                    }
                    else
                    {
                        MessageBox.Show("无此工装信息，请核查！");
                    }
                }
                else
                {
                    MessageBox.Show("工装编码有误！");
                }
            }
        }

        public void TextBox_repairtoolsIn_lifespan_Enter()              //额定寿命获得焦点
        {
            if (Program.mw.comboBox_repairtoolsIn_lifeType.Text == "时间")
            {
                if (Program.mw.textBox_repairtoolsIn_lifeSpan.Text == "天 ")
                {
                    Program.mw.textBox_repairtoolsIn_lifeSpan.Text = "";
                    CommonFunction.TextboxEnter(Program.mw.textBox_repairtoolsIn_lifeSpan);
                }
            }
            else
            {
                if (Program.mw.textBox_repairtoolsIn_lifeSpan.Text == "次 ")
                {
                    Program.mw.textBox_repairtoolsIn_lifeSpan.Text = "";
                    CommonFunction.TextboxEnter(Program.mw.textBox_repairtoolsIn_lifeSpan);
                }
            } 
        }
        public void TextBox_repairtoolsIn_lifespan_Leave()              //额定寿命失去焦点
        {
            if(Program.mw.comboBox_repairtoolsIn_lifeType.Text == "时间")
            {
                if (String.IsNullOrEmpty(Program.mw.textBox_repairtoolsIn_lifeSpan.Text))
                {
                    Program.mw.textBox_repairtoolsIn_lifeSpan.Text = "天 ";
                    CommonFunction.TextboxLeave(Program.mw.textBox_repairtoolsIn_lifeSpan);
                }
            }
            else
            {
                if (String.IsNullOrEmpty(Program.mw.textBox_repairtoolsIn_lifeSpan.Text))
                {
                    Program.mw.textBox_repairtoolsIn_lifeSpan.Text = "次 ";
                    CommonFunction.TextboxLeave(Program.mw.textBox_repairtoolsIn_lifeSpan);
                }
            }
        }

        //维修入库 确认按钮
        public bool RepairtoolsIn_Enter()
        {
            String code = Program.mw.textBox_repairtoolsIn_code.Text;
            String functionState = Program.mw.comboBox_repairtoolsIn_functionState.Text;
            String remarks = Program.mw.textBox_repairtoolsIn_remarks.Text;
            String repairTimes = Program.mw.textBox_repairtoolsIn_repairTimes.Text;
            String lifeType = Program.mw.comboBox_repairtoolsIn_lifeType.Text;
            String lifeSpan = Program.mw.textBox_repairtoolsIn_lifeSpan.Text;
            String operator1 = Program.mw.textBox_repairtoolsIn_operator.Text;
            String operatorName = Program.mw.textBox_repairtoolsIn_operatorName.Text;
            String operatorContact = Program.mw.textBox_repairtoolsIn_operatorContact.Text;
            String toolName = "";

            //校验各项数据
            if (!CommonFunction.CheckCodeLegality(code))
            {
                MessageBox.Show("工装编码有误！");
                return false;
            }
            if (operator1.Length != 8 || CommonFunction.HasChinese(operator1))
            {
                MessageBox.Show("请正确填写操作人！");
                return false;
            }
            sql = "select * from tools where code='" + code + "' order by idTools DESC limit 1";
            DataSet ds = MainWindow.connection.Select(sql);
            if (ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
            {
                MessageBox.Show("无此工装信息，请核查！");
                return false;
            }
            else
            {
                toolName = ds.Tables[0].Rows[0][1].ToString();
            }
            if (!ds.Tables[0].Rows[0][15].ToString().Contains("待修"))
            {
                MessageBox.Show("该工装状态已为：" + ds.Tables[0].Rows[0][15].ToString() + "，无法维修入库！");
                return false;
            }

            String[] temp = code.Split('-');
            String category = temp[0];
            String materialNumber1 = temp[1];
            String number = temp[2];
            int newRepairTimes = int.Parse(repairTimes) + 1;
            
            try
            {
                //更新工装数据库
                sql = @"update tools set storageState='已上架',line='',workStation='',borrower='',operator='" + operator1
                    + "',lendDuration='0',functionState='" + functionState + "',lifeType='"+lifeType+"',lifeSpan='"+lifeSpan
                    + "',repairTimes='" + newRepairTimes + "',remarks='" + remarks + "' where code='" + code + "';";
                if (MainWindow.connection.Update(sql))
                {
                    //添加记录数据库
                    sql = @"insert into records 
                                   (toolName,code,category,materialNumber,number,functionState,lifeType,lifeSpan,operationType,operationDate,operationTime,operator,operatorName,terminal,remarks) 
                            values (
                                 '" + toolName + "'," +
                                "'" + code + "'," +
                                "'" + category + "'," +
                                "'" + materialNumber1 + "'," +
                                "'" + number + "'," +
                                "'" + functionState + "'," +
                                "'" + lifeType + "'," +
                                "'" + lifeSpan + "'," +
                                "'" + "维修入库" + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                "'" + DateTime.Now.ToString("HH:mm:ss") + "'," +
                                "'" + operator1 + "'," +
                                "'" + operatorName + "'," +
                                "'" + MainWindow.TerminalNumber + "'," +
                                "'" + remarks + "');";
                    MainWindow.connection.Insert(sql);

                    //更新人员信息库
                    CommonFunction.UpdatePersonalInfo(operator1, operatorName, operatorContact);

                    CommonFunction.FillDataGridView_Records(Program.mw.dataGridView_RepairToolsIn, "维修入库");
                    CommonFunction.Fill_GroupBox_Info(materialNumber1);
                    RepairToolsIn_CleanALL();
                    return true;
                }
                else
                {
                    MessageBox.Show("维修入库失败！");
                    return false;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("维修入库失败！"+ e.Message);
                return false;
            }
        }//维修入库 确认按钮

        //清除功能函数
        public void RepairToolsIn_CleanALL()
        {
            Program.mw.textBox_repairtoolsIn_code.Text = "";
            Program.mw.comboBox_repairtoolsIn_functionState.Text = "正常";
            Program.mw.textBox_repairtoolsIn_remarks.Text = "";
            Program.mw.comboBox_repairtoolsIn_lifeType.Text = "时间";
            Program.mw.textBox_repairtoolsIn_shelf.Text = "";
            Program.mw.textBox_repairtoolsIn_layer.Text = "";
            Program.mw.textBox_repairtoolsIn_position.Text = "";
            Program.mw.textBox_repairtoolsIn_lifeSpan.Text = "天 ";
            CommonFunction.TextboxLeave(Program.mw.textBox_repairtoolsIn_lifeSpan);
            Program.mw.textBox_repairtoolsIn_repairTimes.Text = "";
            Program.mw.textBox_repairtoolsIn_operatorName.Text = "";
            Program.mw.textBox_repairtoolsIn_operatorContact.Text = "";
            Program.mw.textBox_repairtoolsIn_operator.Text = "员工编号 ";
            CommonFunction.TextboxLeave(Program.mw.textBox_repairtoolsIn_operator);
        }

        ///////////////////////    绘制数据表格


    }
}
