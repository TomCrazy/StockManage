using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace nsStockManage
{
    class ShelfManage
    {
        //DBConnection connection = new DBConnection();
        String sql = null;

        /*****************************************  工装上架界面  **************************************************/

        // 工装上架界面 按键函数 //

        /*public void TextBox_putOnShelf_code_KeyPress(char e)           //工装编码文本框回车函数
        {
            if (e == (char)Keys.Enter)
            {
                String code = Program.mw.textBox_putOnShelf_code.Text;
                sql = "select * from tools where code='" + code + "' order by idTools DESC limit 1";
                DataSet ds = connection.Select(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Program.mw.textBox_putOnShelf_operator.Focus();
                }
                else
                {
                    MessageBox.Show("无此工装！");
                    return;
                }
            }
        }*/

        public void TextBox_putOnShelf_operator_KeyPress(char e)        //操作人文本框 回车函数
        {
            if (e == (char)Keys.Enter)
            {
                String operator1 = Program.mw.textBox_putOnShelf_operator.Text;
                sql = "select * from personal where employeeID='" + operator1 + "' limit 1";
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Program.mw.button_putOnShelf_put.Focus();
                }
                else
                {
                    Program.mw.textBox_putOnShelf_operatorName.Focus();
                }
            }
        }

        // 工装上架界面 焦点函数 //
        public void TextBox_putOnShelf_code_Leave()                    //编码文本框失去焦点函数
        {
            String code = Program.mw.textBox_putOnShelf_code.Text;
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
                        Program.mw.textBox_putOnShelf_materialNumber.Text = materialNumber;

                        Program.mw.textBox_putOnShelf_toolName.Text = ds.Tables[0].Rows[0][1].ToString();
                        Program.mw.comboBox_putOnShelf_functionState.Text = ds.Tables[0].Rows[0][15].ToString();
                        Program.mw.comboBox_putOnShelf_storageState.Text = ds.Tables[0].Rows[0][9].ToString();
                        Program.mw.textBox_putOnShelf_shelf.Text = ds.Tables[0].Rows[0][6].ToString();
                        Program.mw.textBox_putOnShelf_layer.Text = ds.Tables[0].Rows[0][7].ToString();
                        Program.mw.textBox_putOnShelf_position.Text = ds.Tables[0].Rows[0][8].ToString();
                        //展示库存
                        CommonFunction.Fill_GroupBox_Info(Program.mw.panel41_putOnShelf, materialNumber, Program.mw.button_PutOnShelf_Clear);
                    }
                    else
                    {
                        MessageBox.Show("无此工装信息！");
                        return;
                    }
                }
            }
        }

        public void TextBox_putOnShelf_operator_Enter()                //操作人文本框获得焦点函数
        {
            if (Program.mw.textBox_putOnShelf_operator.Text == "员工编号 ")
            {
                Program.mw.textBox_putOnShelf_operator.Text = "";
                CommonFunction.TextboxEnter(Program.mw.textBox_putOnShelf_operator);
            }
        }
        public void TextBox_putOnShelf_operator_Leave()                //操作人文本框失去焦点函数
        {
            if (String.IsNullOrEmpty(Program.mw.textBox_putOnShelf_operator.Text))
            {
                Program.mw.textBox_putOnShelf_operator.Text = "员工编号 ";
                CommonFunction.TextboxLeave(Program.mw.textBox_putOnShelf_operator);
            }
            else
            {
                String operator1 = Program.mw.textBox_putOnShelf_operator.Text;
                sql = "select * from personal where employeeID='" + operator1 + "' limit 1;";
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Program.mw.textBox_putOnShelf_operatorName.Text = ds.Tables[0].Rows[0][2].ToString();
                    Program.mw.textBox_putOnShelf_operatorContact.Text = ds.Tables[0].Rows[0][5].ToString();
                }
            }
        }

        // 工装上架界面 按钮函数 //
        public bool PutOnShelf_put()
        {
            String code = Program.mw.textBox_putOnShelf_code.Text;
            String toolName = Program.mw.textBox_putOnShelf_toolName.Text;
            String functionState = Program.mw.comboBox_putOnShelf_functionState.Text;
            String storageState = Program.mw.comboBox_putOnShelf_storageState.Text;
            String operator1 = Program.mw.textBox_putOnShelf_operator.Text;
            String operatorName = Program.mw.textBox_putOnShelf_operatorName.Text;
            String operatorContact = Program.mw.textBox_putOnShelf_operatorContact.Text;
            String shelf = Program.mw.textBox_putOnShelf_shelf.Text;
            String layer = Program.mw.textBox_putOnShelf_layer.Text;
            String position = Program.mw.textBox_putOnShelf_position.Text;

            int lifeLeft1 = 0;

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
                MessageBox.Show("工装已报废，无法上架！");
                return false;
            }
            if (!storageState.Contains("未上架"))      //校验工装存储状态
            {
                MessageBox.Show("工装" + storageState + "，无法上架！");
                return false;
            }
            if (operator1.Length != 8)
            {
                MessageBox.Show("请正确填写操作人！");
                return false;
            }

            String[] temp = code.Split('-');
            String category = temp[0];
            String materialNumber = temp[1];
            String number = temp[2];

            try
            {
                //更新工装数据库
                sql = @"update tools set storageState='已上架',shelf='"+ shelf +"',layer='"+ layer +"',position='"+ position +"',line='',workStation='',borrower='',operator='" + operator1
                    + "',lendDuration='0' where code='" + code + "';";
                if (MainWindow.connection.Update(sql))
                {
                    //添加记录数据库
                    sql = @"insert into records 
                                   (toolName,code,category,materialNumber,number,shelf,layer,position,functionState,operationType,operationDate,operationTime,operator,operatorName,terminal) 
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
                                    "'" + "工装上架" + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                    "'" + DateTime.Now.ToString("HH:mm:ss") + "'," +
                                    "'" + operator1 + "'," +
                                    "'" + operatorName + "'," +
                                    "'" + MainWindow.TerminalNumber + "');";
                    MainWindow.connection.Insert(sql);
                }
                
                //更新人员信息库
                CommonFunction.UpdatePersonalInfo(operator1, operatorName, operatorContact);

                //刷新数据窗口
                FillListView_putOnShelf(Program.mw.listView_putOnShelf);
                CommonFunction.Fill_GroupBox_Info(materialNumber);
                PutOnShelf_CleanAll();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("数据保存失败！" + e.Message);
                return false;
            }
        }

        // 工装上架界面 清除函数 //
        public void PutOnShelf_CleanAll()
        {
            Program.mw.textBox_putOnShelf_code.Text = "";
            Program.mw.textBox_putOnShelf_shelf.Text = "";
            Program.mw.textBox_putOnShelf_layer.Text = "";
            Program.mw.textBox_putOnShelf_position.Text = "";
            Program.mw.textBox_putOnShelf_toolName.Text = "";
            Program.mw.textBox_putOnShelf_materialNumber.Text = "";
            Program.mw.textBox_putOnShelf_operatorName.Text = "";
            Program.mw.textBox_putOnShelf_operatorContact.Text = "";
            Program.mw.comboBox_putOnShelf_functionState.Text = "";
            Program.mw.comboBox_putOnShelf_storageState.Text = "";
            Program.mw.textBox_putOnShelf_operator.Text = "员工编号 ";
            CommonFunction.TextboxLeave(Program.mw.textBox_putOnShelf_operator);
        }
 
        ///////////////////////    绘制数据表格
        public void DrawListView_putOnShelf(ListView listview)
        {
            listview.Clear();
            int listViewWidth = Screen.PrimaryScreen.Bounds.Width - listview.Location.X * 2 - Program.mw.toolStrip1.Width;
            int listViewHeight = Screen.PrimaryScreen.Bounds.Height - listview.Location.Y - Program.mw.statusStrip1.Height - Program.mw.menuStrip1.Height - 85;
            int listViewColumnWidth = listViewWidth / 14;
            listview.Size = new System.Drawing.Size(listViewWidth, listViewHeight);
            listview.Font = new System.Drawing.Font("微软雅黑", 8F);
            listview.GridLines = true;
            listview.View = View.Details;
            listview.HeaderStyle = ColumnHeaderStyle.Clickable;//表头样式
            listview.FullRowSelect = true;//表示在控件上，是否可以选择一整行
            listview.Columns.Add("", 0, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("序号", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("工装名称", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("工装编码", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("物料号", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("功能状态", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("库位", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("架位", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("层位", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("操作类别", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("操作日期", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("寿命类型", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("可用寿命", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("操作人", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("姓名", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("备注", listViewWidth - 14 * listViewColumnWidth, HorizontalAlignment.Center);
            /*  displaySheet.Location = new System.Drawing.Point(90, 40);
            displaySheet.Size= new System.Drawing.Size(100,100);
            this.Controls.Add(displaySheet);*/
            //this.listView1.BeginUpdate();  //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度

            listview.Columns[0].Width = 0;
        }

        ///////////////////////    填充数据
        public void FillListView_putOnShelf(ListView listview)
        {
            listview.Items.Clear();
            sql = "SELECT * FROM records WHERE operationType LIKE '%上架%' ORDER BY idRecords DESC LIMIT 100";
            DataSet ds = MainWindow.connection.Select(sql);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = "";
                    lvi.SubItems.Add(row[0].ToString());            //序号
                    lvi.SubItems.Add(row[1].ToString());            //工装名称
                    lvi.SubItems.Add(row[2].ToString());            //工装编码
                    lvi.SubItems.Add(row[4].ToString());            //物料号
                    lvi.SubItems.Add(row[9].ToString());            //功能状态
                    lvi.SubItems.Add(row[6].ToString());            //库位
                    lvi.SubItems.Add(row[7].ToString());            //架位
                    lvi.SubItems.Add(row[8].ToString());            //层位
                    lvi.SubItems.Add(row[13].ToString());           //操作类别
                    lvi.SubItems.Add(row[14].ToString());           //操作日期
                    lvi.SubItems.Add(row[10].ToString());           //寿命类型
                    lvi.SubItems.Add(row[12].ToString());           //可用寿命
                    lvi.SubItems.Add(row[16].ToString());           //操作人
                    lvi.SubItems.Add(row[17].ToString());           //操作人姓名
                    lvi.SubItems.Add(row[24].ToString());           //备注
                    listview.Items.Add(lvi);
                }
                listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);   // 填充完数据之后，列宽设置为自适应
            }

        }


        /*****************************************工装移位界面**************************************************/

        // 工装移位界面 按键函数 //
        //由CommonFunction代替

        // 工装移位界面 焦点函数 //
        public void TextBox_changeShelf_code_Leave()                    //编码文本框失去焦点函数
        {
            String code = Program.mw.textBox_changeShelf_code.Text;
            String[] temp = null;
            String category;
            String materialNumber;
            String number;

            if (code.Length > 0)
            {
                if (CommonFunction.CheckCodeLegality(code))             //判断编码合法性
                {
                    temp = code.Split('-');
                    category = temp[0];
                    materialNumber = temp[1];
                    number = temp[2];

                    sql = "select * from tools where code='" + code + "' order by idTools DESC limit 1";  //自动填充已知信息
                    DataSet ds = MainWindow.connection.Select(sql);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        Program.mw.textBox_changeShelf_toolName.Text = ds.Tables[0].Rows[0][1].ToString();
                        Program.mw.comboBox_changeShelf_functionState.Text = ds.Tables[0].Rows[0][15].ToString();
                        Program.mw.textBox_changeShelf_remarks.Text = ds.Tables[0].Rows[0][27].ToString();
                        Program.mw.comboBox_changeShelf_storageState.Text = ds.Tables[0].Rows[0][9].ToString();
                        Program.mw.textBox_changeShelf_oldShelf.Text = ds.Tables[0].Rows[0][6].ToString();
                        Program.mw.textBox_changeShelf_oldLayer.Text = ds.Tables[0].Rows[0][7].ToString();
                        Program.mw.textBox_changeShelf_oldPosition.Text = ds.Tables[0].Rows[0][8].ToString();
                        //展示库存
                        CommonFunction.Fill_GroupBox_Info(Program.mw.panel42_changeShelf, materialNumber, Program.mw.button_ChangeShelf_Clear);
                    }
                    else
                    {
                        MessageBox.Show("无此工装信息！");
                        return;
                    }
                }
            }
        }

        public void TextBox_changeShelf_materialNumber_Leave()                    //物料号文本框失去焦点函数
        {
            String materialNumber = Program.mw.textBox_changeShelf_materialNumber.Text;

            if (materialNumber.Length > 0)
            {
                if (materialNumber.Length >= 3)             //判断物料号合法性
                {
                    sql = "select * from tools where materialNumber='" + materialNumber + "' order by idTools DESC limit 1";  //自动填充已知信息
                    DataSet ds = MainWindow.connection.Select(sql);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        Program.mw.textBox_changeShelf_toolName.Text = ds.Tables[0].Rows[0][1].ToString();
                        Program.mw.textBox_changeShelf_oldShelf.Text = ds.Tables[0].Rows[0][6].ToString();
                        Program.mw.textBox_changeShelf_oldLayer.Text = ds.Tables[0].Rows[0][7].ToString();
                        Program.mw.textBox_changeShelf_oldPosition.Text = ds.Tables[0].Rows[0][8].ToString();
                        //展示库存
                        CommonFunction.Fill_GroupBox_Info(Program.mw.panel42_changeShelf, materialNumber, Program.mw.button_ChangeShelf_Clear);
                    }
                    else
                    {
                        MessageBox.Show("无此物料号信息！");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("物料号不能少于3位！");
                    return;
                }
            }
        }

        // 工装移位界面 按钮函数 //
        public bool ChangeShelf_change()
        {
            String code = Program.mw.textBox_changeShelf_code.Text;
            String materialNumber = Program.mw.textBox_changeShelf_materialNumber.Text;
            String toolName = Program.mw.textBox_changeShelf_toolName.Text;
            String functionState = Program.mw.comboBox_changeShelf_functionState.Text;
            String remarks = Program.mw.textBox_changeShelf_remarks.Text;
            String storageState = Program.mw.comboBox_changeShelf_storageState.Text;
            String oldShelf = Program.mw.textBox_changeShelf_oldShelf.Text;
            String oldLayer = Program.mw.textBox_changeShelf_oldLayer.Text;
            String oldPosition = Program.mw.textBox_changeShelf_oldPosition.Text;
            String operator1 = Program.mw.textBox_changeShelf_operator.Text;
            String operatorName = Program.mw.textBox_changeShelf_operatorName.Text;
            String operatorContact = Program.mw.textBox_changeShelf_operatorContact.Text;
            String newShelf = Program.mw.textBox_changeShelf_newShelf.Text;
            String newLayer = Program.mw.textBox_changeShelf_newLayer.Text;
            String newPosition = Program.mw.textBox_changeShelf_newPosition.Text;

            //根据工装编码移位，只移这一个工装
            if (code.Length > 0)
            {
                if (!CommonFunction.CheckCodeLegality(code))    //校验编码合法性
                {
                    MessageBox.Show("编码不合法！");
                    return false;
                }
                sql = "select * from tools where code='" + code + "' order by idTools DESC limit 1";
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)           //再次校验是否有工装信息
                {
                    MessageBox.Show("无此工装信息！");
                    return false;
                }
                if (functionState.Contains("报废"))      //校验工装功能状态
                {
                    MessageBox.Show("该工装已报废，无法移位！");
                    return false;
                }
                if (!storageState.Contains("上架"))      //校验工装存储状态
                {
                    MessageBox.Show("该工装" + storageState + "，无法移位！");
                    return false;
                }
                if (newShelf.Length <= 0 || newLayer.Length <= 0 || newPosition.Length <= 0)
                {
                    MessageBox.Show("请填写新架号、层号、位号！");
                    return false;
                }
                if (operator1.Length != 8)
                {
                    MessageBox.Show("请正确填写操作人信息！");
                    return false;
                }

                String[] temp = code.Split('-');
                String category = temp[0];
                materialNumber = temp[1];
                String number = temp[2];

                try
                {
                    //更新工装数据库
                    sql = @"update tools set shelf='" + newShelf + "',layer='" + newLayer + "',position='" + newPosition + "',operator='" + operator1
                        + "',remarks='" + remarks + "' where code='" + code + "';";
                    if (MainWindow.connection.Update(sql))
                    {
                        //添加记录数据库
                        sql = @"insert into records 
                                   (toolName,code,category,materialNumber,number,shelf,layer,position,functionState,operationType,operationDate,operationTime,operator,operatorName,terminal,remarks,storageState,oldShelf,oldLayer,oldPosition) 
                            values (
                                     '" + toolName + "'," +
                                    "'" + code + "'," +
                                    "'" + category + "'," +
                                    "'" + materialNumber + "'," +
                                    "'" + number + "'," +
                                    "'" + newShelf + "'," +
                                    "'" + newLayer + "'," +
                                    "'" + newPosition + "'," +
                                    "'" + functionState + "'," +
                                    "'" + "工装移位" + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                    "'" + DateTime.Now.ToString("HH:mm:ss") + "'," +
                                    "'" + operator1 + "'," +
                                    "'" + operatorName + "'," +
                                    "'" + MainWindow.TerminalNumber + "'," +
                                    "'" + remarks + "'," +
                                    "'" + storageState + "'," +
                                    "'" + oldShelf + "'," +
                                    "'" + oldLayer + "'," +
                                    "'" + oldPosition + "');";
                        MainWindow.connection.Insert(sql);

                        //更新人员信息库
                        CommonFunction.UpdatePersonalInfo(operator1, operatorName, operatorContact);

                        //刷新数据窗口
                        FillListView_changeShelf(Program.mw.listView_changeShelf);
                        CommonFunction.Fill_GroupBox_Info(materialNumber);
                        ChangeShelf_CleanAll();
                        return true;

                    }
                    else
                    {
                        MessageBox.Show("更新工装信息失败！");
                        return false;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("数据保存失败！" + e.Message);
                    return false;
                }
            }
            //根据物料号移位，移这一类工装
            else if (materialNumber.Length > 0)
            {
                sql = "select * from tools where materialNumber='" + materialNumber + "' order by idTools DESC limit 1";
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)           //再次校验是否有工装信息
                {
                    MessageBox.Show("无此物料号信息！");
                    return false;
                }
                if (newShelf.Length <= 0 || newLayer.Length <= 0 || newPosition.Length <= 0)
                {
                    MessageBox.Show("请填写新架号、层号、位号！");
                    return false;
                }
                if (operator1.Length != 8)
                {
                    MessageBox.Show("请正确填写操作人信息！");
                    return false;
                }

                try
                {
                    //更新工装数据库
                    sql = @"update tools set shelf='" + newShelf + "',layer='" + newLayer + "',position='" + newPosition + "',operator='" + operator1
                        + "' where materialNumber='" + materialNumber + "';";
                    if (MainWindow.connection.Update(sql))
                    {
                        //添加记录数据库
                        sql = @"insert into records 
                                   (toolName,code,category,materialNumber,shelf,layer,position,functionState,operationType,operationDate,operationTime,operator,operatorName,terminal,remarks,storageState,oldShelf,oldLayer,oldPosition) 
                            values (
                                     '" + toolName + "'," +
                                    "'按物料号移位'," +
                                    "'按物料号移位'," +
                                    "'" + materialNumber + "'," +
                                    "'" + newShelf + "'," +
                                    "'" + newLayer + "'," +
                                    "'" + newPosition + "'," +
                                    "'正常'," +
                                    "'" + "工装移位" + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                    "'" + DateTime.Now.ToString("HH:mm:ss") + "'," +
                                    "'" + operator1 + "'," +
                                    "'" + operatorName + "'," +
                                    "'" + MainWindow.TerminalNumber + "'," +
                                    "'" + remarks + "'," +
                                    "'已上架'," +
                                    "'" + oldShelf + "'," +
                                    "'" + oldLayer + "'," +
                                    "'" + oldPosition + "');";
                        MainWindow.connection.Insert(sql);

                        //更新人员信息库
                        CommonFunction.UpdatePersonalInfo(operator1, operatorName, operatorContact);

                        //刷新数据窗口
                        FillListView_changeShelf(Program.mw.listView_changeShelf);
                        CommonFunction.Fill_GroupBox_Info(materialNumber);
                        ChangeShelf_CleanAll();
                        return true;

                    }
                    else
                    {
                        MessageBox.Show("更新工装信息失败！");
                        return false;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("数据保存失败！" + e.Message);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("工装编码或物料号至少要填写一项！");
                return false;
            }
            

            
        }

        // 工装移位界面 清除函数 //
        public void ChangeShelf_CleanAll()
        {
            Program.mw.textBox_changeShelf_code.Text = "";
            Program.mw.textBox_changeShelf_toolName.Text = "";
            Program.mw.comboBox_changeShelf_functionState.Text = "";
            Program.mw.textBox_changeShelf_remarks.Text = "";
            Program.mw.textBox_changeShelf_materialNumber.Text = "";
            Program.mw.comboBox_changeShelf_storageState.Text = "";
            Program.mw.textBox_changeShelf_oldShelf.Text = "";
            Program.mw.textBox_changeShelf_oldLayer.Text = "";
            Program.mw.textBox_changeShelf_oldPosition.Text = "";
            Program.mw.textBox_changeShelf_newShelf.Text = "";
            Program.mw.textBox_changeShelf_newLayer.Text = "";
            Program.mw.textBox_changeShelf_newPosition.Text = "";
            Program.mw.textBox_changeShelf_operator.Text = "员工编号 ";
            CommonFunction.TextboxLeave(Program.mw.textBox_changeShelf_operator);
            Program.mw.textBox_changeShelf_operatorName.Text = "";
            Program.mw.textBox_changeShelf_operatorContact.Text = "";
        }

        ///////////////////////    绘制数据表
        public void DrawListView_changeShelf(ListView listview)
        {
            listview.Clear();
            int listViewWidth = Screen.PrimaryScreen.Bounds.Width - listview.Location.X * 2 - Program.mw.toolStrip1.Width;
            int listViewHeight = Screen.PrimaryScreen.Bounds.Height - listview.Location.Y - Program.mw.statusStrip1.Height - Program.mw.menuStrip1.Height - 85;
            int listViewColumnWidth = listViewWidth / 16;
            listview.Size = new System.Drawing.Size(listViewWidth, listViewHeight);
            listview.Font = new System.Drawing.Font("微软雅黑", 8F);
            listview.GridLines = true;
            listview.View = View.Details;
            listview.HeaderStyle = ColumnHeaderStyle.Clickable;//表头样式
            listview.FullRowSelect = true;//表示在控件上，是否可以选择一整行
            listview.Columns.Add("", 0, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("序号", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("工装名称", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("工装编码", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("物料号", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("功能状态", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("存储状态", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("操作类别", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("操作日期", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("原库位", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("原架位", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("原层位", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("新库位", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("新架位", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("新层位", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("操作人", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("姓名", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("备注", listViewWidth - 14 * listViewColumnWidth, HorizontalAlignment.Center);

            listview.Columns[0].Width = 0;
        }

        ///////////////////////    填充数据
        public void FillListView_changeShelf(ListView listview)
        {
            listview.Items.Clear();
            sql = "SELECT * FROM records WHERE operationType LIKE '%移位%' ORDER BY idRecords DESC LIMIT 100";
            DataSet ds = MainWindow.connection.Select(sql);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = "";
                    lvi.SubItems.Add(row[0].ToString());            //序号
                    lvi.SubItems.Add(row[1].ToString());            //工装名称
                    lvi.SubItems.Add(row[2].ToString());            //工装编码
                    lvi.SubItems.Add(row[4].ToString());            //物料号
                    lvi.SubItems.Add(row[9].ToString());            //功能状态
                    lvi.SubItems.Add(row[25].ToString());           //存储状态
                    lvi.SubItems.Add(row[13].ToString());           //操作类别
                    lvi.SubItems.Add(row[14].ToString());           //操作日期
                    lvi.SubItems.Add(row[26].ToString());           //原库位
                    lvi.SubItems.Add(row[27].ToString());           //原架位
                    lvi.SubItems.Add(row[28].ToString());           //原层位
                    lvi.SubItems.Add(row[6].ToString());            //新库位
                    lvi.SubItems.Add(row[7].ToString());            //新架位
                    lvi.SubItems.Add(row[8].ToString());            //新层位
                    lvi.SubItems.Add(row[16].ToString());           //操作人
                    lvi.SubItems.Add(row[17].ToString());           //操作人姓名
                    lvi.SubItems.Add(row[24].ToString());           //备注
                    listview.Items.Add(lvi);
                }
                listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);   // 填充完数据之后，列宽设置为自适应
            }
                
        }


        /*****************************************查看库位界面**************************************************/

        // 查看库位界面 按钮函数 //
        public bool LookUpShelf_search()
        {
            String code = Program.mw.textBox_lookUpShelf_code.Text;
            String toolName = Program.mw.textBox_lookUpShelf_toolName.Text;
            String materialNumber = Program.mw.textBox_lookUpShelf_materialNumber.Text;
            String model = Program.mw.textBox_lookUpShelf_model.Text;
            String category = Program.mw.textBox_lookUpShelf_category.Text;
            String shelf = Program.mw.textBox_lookUpShelf_shelf.Text;
            String layer = Program.mw.textBox_lookUpShelf_layer.Text;
            String position = Program.mw.textBox_lookUpShelf_position.Text;
            String remarks = Program.mw.textBox_lookUpShelf_remarks.Text;

            StringBuilder sql = new StringBuilder("SELECT * FROM tools");
            List<string> wheres = new List<string>();

            try
            {
                //拼接查询条件
                if (code.Trim().Length > 0)
                {
                    wheres.Add(" code LIKE '%" + code.Trim() + "%'");
                }
                if (toolName.Trim().Length > 0)
                {
                    wheres.Add(" toolName LIKE '%" + toolName.Trim() + "%'");
                }

                if (materialNumber.Trim().Length > 0)
                {
                    wheres.Add(" materialNumber LIKE '%" + materialNumber.Trim() + "%'");
                }

                if (model.Trim().Length > 0)
                {
                    wheres.Add(" model LIKE '%" + model.Trim() + "%'");
                }

                if (category.Trim().Length > 0)
                {
                    wheres.Add(" category LIKE '%" + category.Trim() + "%'");
                }

                if (shelf.Trim().Length > 0)
                {
                    wheres.Add(" shelf LIKE '%" + shelf.Trim() + "%'");
                }

                if (layer.Trim().Length > 0)
                {
                    wheres.Add(" layer LIKE '%" + layer.Trim() + "%'");
                }

                if (position.Trim().Length > 0)
                {
                    wheres.Add(" position LIKE '%" + position.Trim() + "%'");
                }

                if (remarks.Trim().Length > 0)
                {
                    wheres.Add(" remarks LIKE '%" + remarks.Trim() + "%'");
                }

                //判断条件是否为空，为空则不查询
                if (wheres.Count > 0)
                {
                    String wh = String.Join(" AND ", wheres.ToArray());
                    sql.Append(" WHERE " + wh + " limit 1000;");

                    DataSet ds = MainWindow.connection.Select(sql.ToString());

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        //刷新数据窗口
                        FillListView_lookUpShelf(Program.mw.listView_lookUpShelf, ds);
                        CommonFunction.Fill_GroupBox_Info(materialNumber);
                        LookUpShelf_CleanAll();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("未找到相关记录！");
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("数据查询失败！"+ e.Message);
                return false;
            }
        }

        public void LookUpShelf_CleanAll()
        {
            Program.mw.textBox_lookUpShelf_code.Text = "";
            Program.mw.textBox_lookUpShelf_toolName.Text = "";
            Program.mw.textBox_lookUpShelf_materialNumber.Text = "";
            Program.mw.textBox_lookUpShelf_model.Text = "";
            Program.mw.textBox_lookUpShelf_category.Text = "";
            Program.mw.textBox_lookUpShelf_shelf.Text = "";
            Program.mw.textBox_lookUpShelf_layer.Text = "";
            Program.mw.textBox_lookUpShelf_position.Text = "";
            Program.mw.textBox_lookUpShelf_remarks.Text = "";
        }

        public void DrawListView_lookUpShelf(ListView listview)
        {
            listview.Clear();
            int listViewWidth = Screen.PrimaryScreen.Bounds.Width - listview.Location.X * 2 - Program.mw.toolStrip1.Width;
            int listViewHeight = Screen.PrimaryScreen.Bounds.Height - listview.Location.Y - Program.mw.statusStrip1.Height - Program.mw.menuStrip1.Height - 85;
            int listViewColumnWidth = listViewWidth / 18;
            listview.Size = new System.Drawing.Size(listViewWidth, listViewHeight);
            listview.Font = new System.Drawing.Font("微软雅黑", 8F);
            listview.GridLines = true;
            listview.View = View.Details;
            listview.HeaderStyle = ColumnHeaderStyle.Clickable;//表头样式
            listview.FullRowSelect = true;//表示在控件上，是否可以选择一整行
            listview.Columns.Add("", 0, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("序号", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("工装名称", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("工装编码", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("物料号", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("物料描述", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("架号", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("层号", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("位号", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("存储状态", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
            listview.Columns.Add("功能状态", listViewColumnWidth, HorizontalAlignment.Center); //添加
            listview.Columns.Add("关联机型", listViewColumnWidth, HorizontalAlignment.Center); //添加    
            listview.Columns.Add("备注", listViewColumnWidth, HorizontalAlignment.Center);
        }

        ///////////////////////    填充数据
        public void FillListView_lookUpShelf(ListView listview, DataSet ds)
        {
            listview.Items.Clear();
            int i = 1;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = "";
                lvi.SubItems.Add(i.ToString());                 //序号
                lvi.SubItems.Add(row[1].ToString());            //工装名称
                lvi.SubItems.Add(row[2].ToString());            //工装编码
                lvi.SubItems.Add(row[4].ToString());            //物料号
                lvi.SubItems.Add(row[4].ToString());            //物料描述
                lvi.SubItems.Add(row[6].ToString());            //架号
                lvi.SubItems.Add(row[7].ToString());            //层号
                lvi.SubItems.Add(row[8].ToString());            //位号
                lvi.SubItems.Add(row[9].ToString());            //存储状态
                lvi.SubItems.Add(row[15].ToString());           //功能状态
                lvi.SubItems.Add(row[16].ToString());           //关联机型
                lvi.SubItems.Add(row[27].ToString());           //备注
                listview.Items.Add(lvi);
                i++;
            }
            listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);   // 填充完数据之后，列宽设置为自适应
            listview.Columns[0].Width = 0;
        }

    }
}
