using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataGridViewAutoFilter;

//工装出库类
namespace nsStockManage
{
    class ToolsOut
    {
        //DBConnection connection = new DBConnection();
        String sql = null;
        public static DataTable dtOutByTools = new DataTable();

        /*****************************************    按工装方式出库界面     *************************************/

        // 按工装出库界面 按键函数 //
        public void TextBox_outByTools_code_KeyPress(char e)           //工装编码文本框回车函数
        {
            //检验工装是否存在、检验工装是否已添加过一遍、检验工装存储状态、检验工装功能状态、检验工装寿命
            if (e == (char)Keys.Enter)
            {
                if (dtOutByTools.Rows.Count >= 100)
                {
                    MessageBox.Show("请勿超过100条/次！");
                    return;
                }
                String code = Program.mw.textBox_outByTools_code.Text;
                sql = "select * from tools where code='" + code + "' order by idTools DESC limit 1";  //看数据库里是否已有该工装信息
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //检查是否已添加过
                    foreach (DataRow row in dtOutByTools.Rows)
                    {
                        if (row[1].ToString().Equals(code))
                        {
                            MessageBox.Show("该工装已添加，请勿重复扫码！");
                            return;
                        }
                    }
                    if (!ds.Tables[0].Rows[0][9].ToString().Contains("上架"))    //核对工装存储状态
                    {
                        MessageBox.Show("该工装" + ds.Tables[0].Rows[0][9].ToString() + "，无法出借！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (!ds.Tables[0].Rows[0][15].ToString().Contains("正常"))    //核对工装功能状态
                    {
                        MessageBox.Show("该工装已" + ds.Tables[0].Rows[0][15].ToString() + "，无法出借！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    int lifeLeft = -1;
                    if (int.TryParse(ds.Tables[0].Rows[0][21].ToString(), out lifeLeft) && lifeLeft <= 0)  //校验工装可用寿命
                    {
                        MessageBox.Show("该工装已达寿命上限，请勿出借！");
                        return;
                    }
                    //如果存在且未添加过，添加进下方的DataGridView中，并清空该文本框
                    int newIndex = dtOutByTools.Rows.Count + 1;
                    DataRow dr = dtOutByTools.NewRow();
                    dr[0] = newIndex;                                 //序号
                    dr[1] = ds.Tables[0].Rows[0][2].ToString();       //工装编码
                    dr[2] = ds.Tables[0].Rows[0][4].ToString();       //物料号
                    dr[3] = ds.Tables[0].Rows[0][28].ToString();      //工装描述
                    dr[4] = ds.Tables[0].Rows[0][9].ToString();       //存储状态
                    dr[5] = ds.Tables[0].Rows[0][15].ToString();      //功能状态
                    dr[6] = ds.Tables[0].Rows[0][21].ToString();      //可用寿命
                    dr[7] = ds.Tables[0].Rows[0][27].ToString();     //备注
                    dtOutByTools.Rows.Add(dr);
                    Program.mw.dataGridView_outByTools.DataSource = dtOutByTools;
                    Program.mw.textBox_outByTools_code.Text = "";
                    //展示库存
                    CommonFunction.Fill_GroupBox_Info(Program.mw.panel31_outByTools, dr[2].ToString(), Program.mw.button_outByTools_Clear);
                }
                else
                {
                    MessageBox.Show("无此工装信息，请核对！");
                }
            }
        }

        public void TextBox_outByTools_borrower_KeyPress(char e)       //领用人文本框 回车函数
        {
            if (e == (char)Keys.Enter)
            {
                String borrower = Program.mw.textBox_outByTools_borrower.Text;
                sql = "select * from personal where employeeID='" + borrower + "' limit 1";
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Program.mw.textBox_outByTools_operator.Focus();
                }
                else
                {
                    Program.mw.textBox_outByTools_borrowerName.Focus();
                }
            }
        }
        public void TextBox_outByTools_operator_KeyPress(char e)        //操作人文本框 回车函数
        {
            if (e == (char)Keys.Enter)
            {
                String operator1 = Program.mw.textBox_outByTools_operator.Text;
                sql = "select * from personal where employeeID='" + operator1 + "' limit 1";  //自动填充员工信息
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Program.mw.button_outByTools_Enter.Focus();
                }
                else
                {
                    Program.mw.textBox_outByTools_operatorName.Focus();
                }
            }
        }

        // 按工装出库界面 焦点函数 //
        /*
        public void TextBox_outByTools_code_Leave()                    //编码文本框失去焦点函数
        {
            String code = Program.mw.textBox_outByTools_code.Text;
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
                    DataSet ds = connection.Select(sql);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        Program.mw.textBox_outByTools_materialNumber.Text = materialNumber;

                        Program.mw.comboBox_outByTools_functionState.Text = ds.Tables[0].Rows[0][15].ToString();
                        Program.mw.comboBox_outByTools_storageState.Text = ds.Tables[0].Rows[0][9].ToString();
                        Program.mw.textBox_outByTools_layer.Text = ds.Tables[0].Rows[0][6].ToString();
                        Program.mw.textBox_outByTools_position.Text = ds.Tables[0].Rows[0][7].ToString();
                        Program.mw.textBox_outByTools_position.Text = ds.Tables[0].Rows[0][8].ToString();
                        Program.mw.textBox_outByTools_toolName.Text = ds.Tables[0].Rows[0][1].ToString();
                        Program.mw.textBox_outByTools_remarks.Text = ds.Tables[0].Rows[0][27].ToString();
                        Program.mw.textBox_outByTools_workStation.Text = ds.Tables[0].Rows[0][11].ToString();

                        CommonFunction.Fill_GroupBox_Info(Program.mw.panel31_outByTools, materialNumber, Program.mw.button_outByTools_Clear);
                    }
                    else
                    {
                        MessageBox.Show("无此工装信息！");
                    }
                }
                else
                {
                    MessageBox.Show("工装编码有误！");
                }
            }
        }*/

        public void TextBox_outByTools_borrower_Enter()                //领用人文本框获得焦点函数
        {
            if (Program.mw.textBox_outByTools_borrower.Text == "员工编号 ")
            {
                Program.mw.textBox_outByTools_borrower.Text = "";
                CommonFunction.TextboxEnter(Program.mw.textBox_outByTools_borrower);
            }
        }
        public void TextBox_outByTools_borrower_Leave()                //领用人文本框失去焦点函数
        {
            if (String.IsNullOrEmpty(Program.mw.textBox_outByTools_borrower.Text))
            {
                Program.mw.textBox_outByTools_borrower.Text = "员工编号 ";
                CommonFunction.TextboxLeave(Program.mw.textBox_outByTools_borrower);
            }
            else
            {
                String borrower = Program.mw.textBox_outByTools_borrower.Text;
                sql = "select * from personal where employeeID='" + borrower + "' limit 1;";
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Program.mw.textBox_outByTools_borrowerName.Text = ds.Tables[0].Rows[0][2].ToString();
                    Program.mw.textBox_outByTools_borrowerContact.Text = ds.Tables[0].Rows[0][5].ToString();
                    Program.mw.textBox_outByTools_borrowLine.Text = ds.Tables[0].Rows[0][4].ToString();
                }
            }
        }
        public void TextBox_outByTools_operator_Enter()                //操作人文本框获得焦点函数
        {
            if (Program.mw.textBox_outByTools_operator.Text == "员工编号 ")
            {
                Program.mw.textBox_outByTools_operator.Text = "";
                CommonFunction.TextboxEnter(Program.mw.textBox_outByTools_operator);
            }
        }
        public void TextBox_outByTools_operator_Leave()                //操作人文本框失去焦点函数
        {
            if (String.IsNullOrEmpty(Program.mw.textBox_outByTools_operator.Text))
            {
                Program.mw.textBox_outByTools_operator.Text = "员工编号 ";
                CommonFunction.TextboxLeave(Program.mw.textBox_outByTools_operator);
            }
            else
            {
                String operator1 = Program.mw.textBox_outByTools_operator.Text;
                sql = "select * from personal where employeeID='" + operator1 + "' limit 1;";
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Program.mw.textBox_outByTools_operatorName.Text = ds.Tables[0].Rows[0][2].ToString();
                    Program.mw.textBox_outByTools_operatorContact.Text = ds.Tables[0].Rows[0][5].ToString();
                }
            }
        }

        // 按工装出库界面 按钮函数 //
        
        public bool OutByTools_Enter()
        {
            //获取编码、领用人、操作人信息
            //检查领用人信息、检查操作人信息
            //更新工装数据库，添加记录数据库
            String borrower = Program.mw.textBox_outByTools_borrower.Text;
            String borrowerName = Program.mw.textBox_outByTools_borrowerName.Text;
            String borrowerContact = Program.mw.textBox_outByTools_borrowerContact.Text;
            String borrowLine = Program.mw.textBox_outByTools_borrowLine.Text;
            String operator1 = Program.mw.textBox_outByTools_operator.Text;
            String operatorName = Program.mw.textBox_outByTools_operatorName.Text;
            String operatorContact = Program.mw.textBox_outByTools_operatorContact.Text;
            String purpose = Program.mw.textBox_outByTools_purpose.Text;

            //校验各项数据
            if (borrower.Length != 8 || CommonFunction.HasChinese(borrower) || borrowerName.Length < 1)
            {
                MessageBox.Show("请正确填写领用人信息！");
                return false;
            }
            if (operator1.Length != 8 || CommonFunction.HasChinese(operator1) || operatorName.Length < 1)
            {
                MessageBox.Show("请正确填写操作人信息！");
                return false;
            }
            
            try
            {
                //单件出借，没有在编码文本框按回车
                if(dtOutByTools.Rows.Count <= 0)
                {
                    String code = Program.mw.textBox_outByTools_code.Text;
                    if (CommonFunction.CheckCodeLegality(code))
                    {
                        String[] temp = code.Split('-');
                        String category = temp[0];
                        String materialNumber = temp[1];
                        String number = temp[2];
                        String functionState = "正常";

                        sql = "select * from tools where code='" + code + "' order by idTools DESC limit 1";
                        DataSet ds = MainWindow.connection.Select(sql);
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            if (!ds.Tables[0].Rows[0][9].ToString().Contains("上架"))    //核对工装存储状态
                            {
                                MessageBox.Show("该工装" + ds.Tables[0].Rows[0][9].ToString() + "，无法出借！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            if (!ds.Tables[0].Rows[0][15].ToString().Contains("正常"))    //核对工装功能状态
                            {
                                MessageBox.Show("该工装已" + ds.Tables[0].Rows[0][15].ToString() + "，无法出借！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            int lifeLeft = -1;
                            if (int.TryParse(ds.Tables[0].Rows[0][21].ToString(), out lifeLeft) && lifeLeft <= 0)  //校验工装可用寿命
                            {
                                MessageBox.Show("该工装已达寿命上限，请勿出借！");
                                return false;
                            }
                            //更新工装数据库
                            sql = @"update tools set storageState='已出借',line='" + borrowLine + "',borrower='" + borrower + "',operator='" + operator1
                            + "',lendDuration='0' where code='" + code + "';";
                            if (MainWindow.connection.Update(sql))
                            {
                                //添加记录数据库
                                sql = @"insert into records 
                                   (code,category,materialNumber,number,functionState,operationType,operationDate,operationTime,operator,operatorName,terminal,line,borrower,borrowerName,purpose) 
                                    values (
                                             '" + code + "'," +
                                        "'" + category + "'," +
                                        "'" + materialNumber + "'," +
                                        "'" + number + "'," +
                                        "'" + functionState + "'," +
                                        "'" + "按工装出借" + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                        "'" + DateTime.Now.ToString("HH:mm:ss") + "'," +
                                        "'" + operator1 + "'," +
                                        "'" + operatorName + "'," +
                                        "'" + MainWindow.TerminalNumber + "'," +
                                        "'" + borrowLine + "'," +
                                        "'" + borrower + "'," +
                                        "'" + borrowerName + "'," +
                                        "'" + purpose + "');";
                                MainWindow.connection.Insert(sql);

                                //更新人员信息库
                                CommonFunction.UpdatePersonalInfo(borrower, borrowerName, borrowerContact, borrowLine);
                                CommonFunction.UpdatePersonalInfo(operator1, operatorName, operatorContact);
                                //刷新DataGridView数据
                                CommonFunction.FillDataGridView_Records(Program.mw.dataGridView_outByTools_records, "按工装出借");
                                OutByTools_CleanAll();
                                return true;
                            }
                            else
                            {
                                MessageBox.Show("更新工装信息失败！");
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("无此工装信息，请核对！");
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("编码格式错误，请核对！");
                        return false;
                    }
                }
                //批量出借
                else
                {
                    foreach (DataRow row in dtOutByTools.Rows)
                    {
                        String code = row[1].ToString();
                        String remarks = row[7].ToString();
                        String[] temp = code.Split('-');
                        String category = temp[0];
                        String materialNumber = temp[1];
                        String number = temp[2];
                        String functionState = "正常";

                        //更新工装数据库   由于扫码时已核对过，无需再次核对信息
                        sql = @"update tools set storageState='已出借',line='" + borrowLine + "',borrower='" + borrower + "',operator='" + operator1
                        + "',lendDuration='0',remarks='" + remarks + "' where code='" + code + "';";
                        if (MainWindow.connection.Update(sql))
                        {
                            //添加记录数据库
                            sql = @"insert into records 
                                   (code,category,materialNumber,number,functionState,operationType,operationDate,operationTime,operator,operatorName,terminal,line,borrower,borrowerName,purpose,remarks) 
                            values (
                                     '" + code + "'," +
                                "'" + category + "'," +
                                "'" + materialNumber + "'," +
                                "'" + number + "'," +
                                "'" + functionState + "'," +
                                "'" + "按工装出借" + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                "'" + DateTime.Now.ToString("HH:mm:ss") + "'," +
                                "'" + operator1 + "'," +
                                "'" + operatorName + "'," +
                                "'" + MainWindow.TerminalNumber + "'," +
                                "'" + borrowLine + "'," +
                                "'" + borrower + "'," +
                                "'" + borrowerName + "'," +
                                "'" + purpose + "'," +
                                "'" + remarks + "');";
                            MainWindow.connection.Insert(sql);

                        }
                        else
                        {
                            MessageBox.Show("更新工装信息失败！");
                            return false;
                        }
                    }
                    //更新人员信息库
                    CommonFunction.UpdatePersonalInfo(borrower, borrowerName, borrowerContact, borrowLine);
                    CommonFunction.UpdatePersonalInfo(operator1, operatorName, operatorContact);
                    //刷新DataGridView数据
                    CommonFunction.FillDataGridView_Records(Program.mw.dataGridView_outByTools_records, "按工装出借");
                    OutByTools_CleanAll();
                    return true;
                }
                
            }
            catch
            {
                MessageBox.Show("数据保存失败！");
                return false;
            }
        }

        // 按工装出借界面 清除函数 //
        public void OutByTools_CleanAll()
        {
            Program.mw.textBox_outByTools_code.Text = "";
            Program.mw.textBox_outByTools_borrower.Text = "员工编号 ";
            CommonFunction.TextboxLeave(Program.mw.textBox_outByTools_borrower);
            Program.mw.textBox_outByTools_borrowerName.Text = "";
            Program.mw.textBox_outByTools_borrowerContact.Text = "";
            Program.mw.textBox_outByTools_borrowLine.Text = "";
            Program.mw.textBox_outByTools_operator.Text = "员工编号 ";
            CommonFunction.TextboxLeave(Program.mw.textBox_outByTools_operator);
            Program.mw.textBox_outByTools_operatorName.Text = "";
            Program.mw.textBox_outByTools_operatorContact.Text = "";
            Program.mw.textBox_outByTools_purpose.Text = "";

            dtOutByTools.Rows.Clear();
            Program.mw.dataGridView_outByTools.DataSource = dtOutByTools;
        }

        //dataGridView中删除行按钮的单击事件
        public void DataGridView_outByTools_CellContentClick(object sender, DataGridViewCellEventArgs e, DataGridView dataGridView)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                DialogResult result = MessageBox.Show("确定删除此行？", "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    dtOutByTools.Rows[e.RowIndex].Delete();
                    for (int i = 0; i < dtOutByTools.Rows.Count; i++) //重新编号
                    {
                        dtOutByTools.Rows[i][0] = i + 1;
                    }
                    dataGridView.DataSource = dtOutByTools;
                }
                else
                {
                    return;
                }
            }
        }


        /****************************************     按机型方式出库界面     *************************************/


        /****************************************     维修报废出库界面       *************************************/
        
        public void TextBox_scrapTools_receiver_KeyPress(char e)       //接收人文本框 回车函数
        {
            if (e == (char)Keys.Enter)
            {
                String receiver = Program.mw.textBox_scrapTools_receiver.Text;
                sql = "select * from personal where employeeID='" + receiver + "' limit 1";
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Program.mw.textBox_scrapTools_operator.Focus();
                }
                else
                {
                    Program.mw.textBox_scrapTools_receiverName.Focus();
                }
            }
        }
        public void TextBox_scrapTools_operator_KeyPress(char e)        //操作人文本框 回车函数
        {
            if (e == (char)Keys.Enter)
            {
                String operator1 = Program.mw.textBox_scrapTools_operator.Text;
                sql = "select * from personal where employeeID='" + operator1 + "' limit 1";  //自动填充员工信息
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Program.mw.button_scrapTools_repair.Focus();
                }
                else
                {
                    Program.mw.textBox_scrapTools_operatorName.Focus();
                }
            }
        }

        // 维修报废出库界面 焦点函数 //
        public void TextBox_scrapTools_code_Leave()                    //工装编码文本框 失去焦点函数
        {
            String code = Program.mw.textBox_scrapTools_code.Text;
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
                        Program.mw.textBox_scrapTools_materialNumber.Text = materialNumber;

                        Program.mw.textBox_scrapTools_toolName.Text = ds.Tables[0].Rows[0][1].ToString();
                        Program.mw.textBox_scrapTools_remarks.Text = ds.Tables[0].Rows[0][27].ToString();
                        Program.mw.textBox_scrapTools_materialNumber.Text = ds.Tables[0].Rows[0][4].ToString();
                        Program.mw.comboBox_scrapTools_functionState.Text = ds.Tables[0].Rows[0][15].ToString();
                        Program.mw.comboBox_scrapTools_storageState.Text = ds.Tables[0].Rows[0][9].ToString();
                        Program.mw.textBox_scrapTools_repairTimes.Text = ds.Tables[0].Rows[0][22].ToString();
                        //库存展示
                        CommonFunction.Fill_GroupBox_Info(Program.mw.panel33_scrapTools, materialNumber, Program.mw.button_scrapTools_clear);
                    }
                    else
                    {
                        MessageBox.Show("无此工装信息！");
                    }
                }
            }
        }

        public void TextBox_scrapTools_receiver_Enter()                //接收人文本框获得焦点函数
        {
            if (Program.mw.textBox_scrapTools_receiver.Text == "员工编号 ")
            {
                Program.mw.textBox_scrapTools_receiver.Text = "";
                CommonFunction.TextboxEnter(Program.mw.textBox_scrapTools_receiver);
            }
        }
        public void TextBox_scrapTools_receiver_Leave()                //接收人文本框失去焦点函数
        {
            if (String.IsNullOrEmpty(Program.mw.textBox_scrapTools_receiver.Text))
            {
                Program.mw.textBox_scrapTools_receiver.Text = "员工编号 ";
                CommonFunction.TextboxLeave(Program.mw.textBox_scrapTools_receiver);
            }
            else
            {
                String receiver = Program.mw.textBox_scrapTools_receiver.Text;
                sql = "select * from personal where employeeID='" + receiver + "' limit 1;";
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Program.mw.textBox_scrapTools_receiverName.Text = ds.Tables[0].Rows[0][2].ToString();
                    Program.mw.textBox_scrapTools_receiverContact.Text = ds.Tables[0].Rows[0][5].ToString();
                }
            }
        }
        public void TextBox_scrapTools_operator_Enter()                //操作人文本框获得焦点函数
        {
            if (Program.mw.textBox_scrapTools_operator.Text == "员工编号 ")
            {
                Program.mw.textBox_scrapTools_operator.Text = "";
                CommonFunction.TextboxEnter(Program.mw.textBox_scrapTools_operator);
            }
        }
        public void TextBox_scrapTools_operator_Leave()                //操作人文本框失去焦点函数
        {
            if (String.IsNullOrEmpty(Program.mw.textBox_scrapTools_operator.Text))
            {
                Program.mw.textBox_scrapTools_operator.Text = "员工编号 ";
                CommonFunction.TextboxLeave(Program.mw.textBox_scrapTools_operator);
            }
            else
            {
                String operator1 = Program.mw.textBox_scrapTools_operator.Text;
                sql = "select * from personal where employeeID='" + operator1 + "' limit 1;";
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Program.mw.textBox_scrapTools_operatorName.Text = ds.Tables[0].Rows[0][2].ToString();
                    Program.mw.textBox_scrapTools_operatorContact.Text = ds.Tables[0].Rows[0][5].ToString();
                }
            }
        }

        // 维修报废出库界面 按钮函数 //
        public bool ScrapTools_Repair()             //维修按钮
        {
            String code = Program.mw.textBox_scrapTools_code.Text;
            String toolName = Program.mw.textBox_scrapTools_toolName.Text;
            String remarks = Program.mw.textBox_scrapTools_remarks.Text;
            String functionState = Program.mw.comboBox_scrapTools_functionState.Text;
            String storageState = Program.mw.comboBox_scrapTools_storageState.Text;
            String repairTimes = Program.mw.textBox_scrapTools_repairTimes.Text;
            String receiver = Program.mw.textBox_scrapTools_receiver.Text;
            String receiverName = Program.mw.textBox_scrapTools_receiverName.Text;
            String receiverContact = Program.mw.textBox_scrapTools_receiverContact.Text;
            String repairPlace = Program.mw.textBox_scrapTools_repairPlace.Text;
            String operator1 = Program.mw.textBox_scrapTools_operator.Text;
            String operatorName = Program.mw.textBox_scrapTools_operatorName.Text;
            String operatorContact = Program.mw.textBox_scrapTools_operatorContact.Text;

            //校验各项数据
            if (!CommonFunction.CheckCodeLegality(code))    //校验编码合法性
            {
                MessageBox.Show("编码不合法！");
                return false;
            }
            sql = "select * from tools where code='" + code + "' order by idTools DESC limit 1";
            DataSet ds = MainWindow.connection.Select(sql);
            if (ds.Tables.Count <= 0 ||ds.Tables[0].Rows.Count <= 0)           //再次校验是否有工装
            {
                MessageBox.Show("无此工装信息！");
                return false;
            }
            if (functionState.Contains("报废"))      //校验工装功能状态
            {
                MessageBox.Show("工装已报废，无法送修！");
                return false;
            }
            if (!storageState.Contains("上架"))      //校验工装存储状态
            {
                MessageBox.Show("工装" + storageState + "，无法送修！");
                return false;
            }
            if (receiver.Length != 8 || CommonFunction.HasChinese(receiver))
            {
                MessageBox.Show("请正确填写接收人工号！");
                return false;
            }
            if (operator1.Length != 8 || CommonFunction.HasChinese(operator1))
            {
                MessageBox.Show("请正确填写操作人工号！");
                return false;
            }
            
            String[] temp = code.Split('-');
            String category = temp[0];
            String materialNumber = temp[1];
            String number = temp[2];
            
            try
            {
                //更新工装数据库
                sql = @"update tools set shelf='', layer='', position='', line='', workStation='', storageState='已送修', functionState='待修', borrower='" + receiver + "',operator='" + operator1
                    + "',lendDuration='0',repairTimes='" + (repairTimes + 1) + "', remarks='"+ remarks +"' where code='" + code + "';";
                if (MainWindow.connection.Update(sql))
                {
                    //添加记录数据库
                    sql = @"insert into records 
                                   (toolName,code,category,materialNumber,number,functionState,operationType,operationDate,operationTime,operator,operatorName,terminal,borrower,borrowerName,remarks) 
                            values (
                                     '" + toolName + "'," +
                                    "'" + code + "'," +
                                    "'" + category + "'," +
                                    "'" + materialNumber + "'," +
                                    "'" + number + "'," +
                                    "'" + "待修" + "'," +
                                    "'" + "维修出库" + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                    "'" + DateTime.Now.ToString("HH:mm:ss") + "'," +
                                    "'" + operator1 + "'," +
                                    "'" + operatorName + "'," +
                                    "'" + MainWindow.TerminalNumber + "'," +
                                    "'" + receiver + "'," +
                                    "'" + receiverName + "'," +
                                    "'" + remarks + "');";
                    MainWindow.connection.Insert(sql);
                }
                else
                {
                    MessageBox.Show("更新工装信息失败！");
                    return false;
                }
                
                //更新人员信息库
                CommonFunction.UpdatePersonalInfo(receiver, receiverName, receiverContact);
                CommonFunction.UpdatePersonalInfo(operator1, operatorName, operatorContact);

                //刷新数据窗口
                CommonFunction.FillDataGridView_Records(Program.mw.dataGridView_ScrapTools, "维修");
                CommonFunction.Fill_GroupBox_Info(materialNumber);
                ScrapTools_CleanAll();
                return true;
            }
            catch
            {
                MessageBox.Show("数据保存失败！");
                return false;
            }
        }

        public bool ScrapTools_Scrap()             //报废按钮
        {
            String code = Program.mw.textBox_scrapTools_code.Text;
            String toolName = Program.mw.textBox_scrapTools_toolName.Text;
            String remarks = Program.mw.textBox_scrapTools_remarks.Text;
            String functionState = Program.mw.comboBox_scrapTools_functionState.Text;
            String storageState = Program.mw.comboBox_scrapTools_storageState.Text;
            String repairTimes = Program.mw.textBox_scrapTools_repairTimes.Text;
            String receiver = Program.mw.textBox_scrapTools_receiver.Text;
            String receiverName = Program.mw.textBox_scrapTools_receiverName.Text;
            String receiverContact = Program.mw.textBox_scrapTools_receiverContact.Text;
            String repairPlace = Program.mw.textBox_scrapTools_repairPlace.Text;
            String operator1 = Program.mw.textBox_scrapTools_operator.Text;
            String operatorName = Program.mw.textBox_scrapTools_operatorName.Text;
            String operatorContact = Program.mw.textBox_scrapTools_operatorContact.Text;

            //校验各项数据
            if (!CommonFunction.CheckCodeLegality(code))    //校验编码合法性
            {
                MessageBox.Show("编码不合法！");
                return false;
            }
            sql = "select * from tools where code='" + code + "' order by idTools DESC limit 1";
            DataSet ds = MainWindow.connection.Select(sql);
            if (ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)           //再次校验是否有工装信息
            {
                MessageBox.Show("无此工装！");
                return false;
            }
            if (functionState.Contains("报废"))      //校验工装功能状态
            {
                MessageBox.Show("工装已报废，请勿重复操作！");
                return false;
            }
            if (storageState.Contains("出借") || storageState.Contains("报废"))      //校验工装存储状态
            {
                MessageBox.Show("工装" + storageState + "，无法报废！");
                return false;
            }
            if (operator1.Length != 8)
            {
                MessageBox.Show("请正确填写操作人工号！");
                return false;
            }

            String[] temp = code.Split('-');
            String category = temp[0];
            String materialNumber = temp[1];
            String number = temp[2];

            try
            {
                //更新工装数据库
                sql = @"update tools set shelf='', layer='', position='', storageState='已报废', line='', workStation='', borrower='" + receiver + "',operator='" + operator1
                    + "',lendDuration='0', functionState='报废', lifeLeft='0', scrapDate='"+ DateTime.Now.ToString("yyyy-MM-dd") +"', remarks ='" + remarks + "' where code='" + code + "';";
                if (MainWindow.connection.Update(sql))
                {
                    //添加记录数据库
                    sql = @"insert into records 
                                   (toolName,code,category,materialNumber,number,functionState,operationType,operationDate,operationTime,operator,operatorName,terminal,borrower,borrowerName,remarks) 
                            values (
                                 '" + toolName + "'," +
                                    "'" + code + "'," +
                                    "'" + category + "'," +
                                    "'" + materialNumber + "'," +
                                    "'" + number + "'," +
                                    "'" + "报废" + "'," +
                                    "'" + "报废出库" + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                    "'" + DateTime.Now.ToString("HH:mm:ss") + "'," +
                                    "'" + operator1 + "'," +
                                    "'" + operatorName + "'," +
                                    "'" + MainWindow.TerminalNumber + "'," +
                                    "'" + receiver + "'," +
                                    "'" + receiverName + "'," +
                                    "'" + remarks + "');";
                    MainWindow.connection.Insert(sql);
                }
                else
                {
                    MessageBox.Show("更新工装信息失败！");
                    return false;
                }
                
                //更新人员信息库
                CommonFunction.UpdatePersonalInfo(receiver, receiverName, receiverContact);
                CommonFunction.UpdatePersonalInfo(operator1, operatorName, operatorContact);

                //刷新数据窗口
                CommonFunction.FillDataGridView_Records(Program.mw.dataGridView_ScrapTools, "报废");
                CommonFunction.Fill_GroupBox_Info(materialNumber);
                ScrapTools_CleanAll();
                return true;
            }
            catch
            {
                MessageBox.Show("数据保存失败！");
                return false;
            }
        }

        // 维修报废出库界面 清除函数 //
        public void ScrapTools_CleanAll()
        {
            Program.mw.textBox_scrapTools_code.Text = "";
            Program.mw.textBox_scrapTools_toolName.Text = "";
            Program.mw.textBox_scrapTools_remarks.Text = "";
            Program.mw.textBox_scrapTools_materialNumber.Text = "";
            Program.mw.comboBox_scrapTools_functionState.Text = "待修";
            Program.mw.textBox_scrapTools_operator.Text = "员工编号 ";
            CommonFunction.TextboxLeave(Program.mw.textBox_scrapTools_operator);
            Program.mw.textBox_scrapTools_receiver.Text = "员工编号 ";
            CommonFunction.TextboxLeave(Program.mw.textBox_scrapTools_receiver);
            Program.mw.textBox_scrapTools_operatorContact.Text = "";
            Program.mw.textBox_scrapTools_receiverContact.Text = "";
            Program.mw.textBox_scrapTools_operatorName.Text = "";
            Program.mw.textBox_scrapTools_receiverName.Text = "";
            Program.mw.textBox_scrapTools_repairTimes.Text = "";
            Program.mw.textBox_scrapTools_repairPlace.Text = "";
        }

        ///////////////////////    绘制数据表格

    }
}
