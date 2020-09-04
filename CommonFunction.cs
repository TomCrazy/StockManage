using System;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Data;
using System.Globalization;
using System.Runtime.InteropServices;
using DataGridViewAutoFilter;

namespace nsStockManage
{
    class CommonFunction
    {

        /// <summary>
        /// 公共数组
        /// </summary>

        public static readonly String[] arrayTerminals = new String[]        //允许的客户端总数
        { "none", "客户端1", "客户端2", "客户端3", "客户端4", "客户端5", "客户端6", "客户端7", "客户端8", "客户端9" };
        public static String[] header_tools = new String[]          //工装数据库的表头
        {
            "序号", "工装名称", "工装编码", "工装缩写", "物料号", "编号", "架号", "层号", "位号", "存储状态",
            "所在线体", "所在工位", "领用人", "操作人", "借出时长", "功能状态", "关联机型", "厂家", "单价", "寿命类型",
            "额定寿命", "可用寿命", "维修次数", "出借次数", "购入日期", "报废日期", "修改日期", "备注", "工装描述"
        };
        public static readonly String[] header_records = new String[]        //记录数据库的表头
        {
            "序号", "工装名称", "工装编码", "工装缩写", "物料号", "编号", "架号", "层号", "位号", "功能状态",
            "寿命类型", "额定寿命", "可用寿命", "操作类别", "操作日期", "操作时间", "操作人工号", "操作人", "终端名称", "领用线体",
            "领用工位", "领用人工号", "领用人", "用途", "备注", "存储状态", "原架号", "原层号", "原位号", "工装描述"
        };
        public static readonly String[] header_personal = new String[]        //人员数据库的表头
        {
            "序号", "员工编号", "姓名", "车间", "线体", "联系方式"
        };
        public static readonly String[] header_warningSetup = new String[]    //预警设置数据库的表头
        {
            "序号", "预警项", "物料号", "预警类别", "预警方式", "报警值", "备注", "修改时间"
        };
        public static readonly String[] header_warnings = new String[]        //预警概览数据库的表头
        {
            "序号", "预警项", "物料号", "预警类别", "预警方式", "报警值", "总量", "当前量", "备注", "更新时间", "是否预警"
        };
        public static readonly String[] header_newToolsIn = new String[]      //新购工装入库的表头
        {
            "序号", "工装名称", "工装编码", "物料号", "工装描述", "架号", "层号", "位号", "操作类别",
            "额定寿命", "购入日期", "操作人", "备注"
        };


        /// <summary>
        /// 隐藏所有的panel
        /// </summary>
        public static  void  HideAllPanels()
        {
            Program.mw.panel11_welcome.Visible = false;
            Program.mw.panel21_newToolsIn.Visible = false;
            Program.mw.panel22_toolsReturn.Visible = false;
            Program.mw.panel23_repairtoolsIn.Visible = false;
            Program.mw.panel31_outByTools.Visible = false;
            Program.mw.panel33_scrapTools.Visible = false;
            Program.mw.panel41_putOnShelf.Visible = false;
            Program.mw.panel42_changeShelf.Visible = false;
            Program.mw.panel43_lookUpShelf.Visible = false;
            Program.mw.panel51_toolsData.Visible = false;
            Program.mw.panel52_recordsData.Visible = false;
            Program.mw.panel53_personsData.Visible = false;
            Program.mw.panel61_dataAnalysis.Visible = false;
            Program.mw.panel71_warningSetUp.Visible = false;
            Program.mw.panel72_warningOverview.Visible = false;
        }
        public static void HideAllPanelsExcept(Panel panel)
        {
            HideAllPanels();
            panel.Visible = true;
        }

        /// <summary>
        /// 刷新指定panel里的显示时间
        /// </summary>
        public static void RefreshAllTime(object sender, System.EventArgs e)
        { 
            Program.mw.dateTimePicker_recordsData_operationDate.Value = DateTime.Now;

            //检索预警项，显示报警信息
            CommonFunction.WarningUpdate();
        }


        /// <summary>
        /// 绘制各DataGridView控件的表头
        /// </summary>
        /// 
        public static void DrawDataGridViewHeader(DataGridView dataGridView, int panelCode)
        {
            //采用switch结构效率要高于if
            switch (panelCode)
            {
                case 21:    //新购入库
                    for (int i = 0; i < CommonFunction.header_records.Length; i++)
                    {
                        dataGridView.Columns[i].HeaderCell = new DataGridViewAutoFilterColumnHeaderCell();
                        dataGridView.Columns[i].HeaderText = CommonFunction.header_records[i];
                        dataGridView.Columns[i].Visible = false;
                    }
                    dataGridView.Columns[0].Visible = true;            //序号
                    dataGridView.Columns[1].Visible = true;            //工装名称
                    dataGridView.Columns[2].Visible = true;            //工装编码
                    dataGridView.Columns[4].Visible = true;            //物料号
                    dataGridView.Columns[29].Visible = true;           //工装描述
                    dataGridView.Columns[6].Visible = true;            //架号
                    dataGridView.Columns[7].Visible = true;            //位号
                    dataGridView.Columns[8].Visible = true;            //层号
                    dataGridView.Columns[13].Visible = true;           //操作类别
                    dataGridView.Columns[11].Visible = true;           //额定寿命
                    dataGridView.Columns[14].Visible = true;           //购入日期
                    dataGridView.Columns[17].Visible = true;           //操作人姓名
                    dataGridView.Columns[24].Visible = true;           //备注
                    dataGridView.Columns[2].Frozen = true;  //冻结指定列
                    break;

                case 22:    //归还入库
                    for (int i = 0; i < CommonFunction.header_records.Length; i++)
                    {
                        dataGridView.Columns[i].HeaderText = CommonFunction.header_records[i];
                        dataGridView.Columns[i].Visible = false;
                    }
                    dataGridView.Columns[0].Visible = true;            //序号
                    dataGridView.Columns[2].Visible = true;            //工装编码
                    dataGridView.Columns[13].Visible = true;           //操作类别
                    dataGridView.Columns[14].Visible = true;           //操作日期
                    dataGridView.Columns[15].Visible = true;           //操作时间
                    dataGridView.Columns[19].Visible = true;           //领用线体
                    dataGridView.Columns[22].Visible = true;           //领用人姓名
                    dataGridView.Columns[24].Visible = true;           //备注
                    break;

                case 23:    //维修入库
                    for (int i = 0; i < CommonFunction.header_records.Length; i++)
                    {
                        dataGridView.Columns[i].HeaderCell = new DataGridViewAutoFilterColumnHeaderCell();
                        dataGridView.Columns[i].HeaderText = CommonFunction.header_records[i];
                        dataGridView.Columns[i].Visible = false;
                    }
                    dataGridView.Columns[0].Visible = true;            //序号
                    dataGridView.Columns[1].Visible = true;            //工装名称
                    dataGridView.Columns[2].Visible = true;            //工装编码
                    dataGridView.Columns[4].Visible = true;            //物料号
                    dataGridView.Columns[9].Visible = true;            //功能状态
                    dataGridView.Columns[10].Visible = true;           //寿命类型
                    dataGridView.Columns[11].Visible = true;           //额定寿命
                    dataGridView.Columns[13].Visible = true;           //操作类别
                    dataGridView.Columns[14].Visible = true;           //操作日期
                    dataGridView.Columns[15].Visible = true;           //操作时间
                    dataGridView.Columns[17].Visible = true;           //操作人姓名
                    dataGridView.Columns[24].Visible = true;           //备注
                    break;

                case 31:    //按工装出借
                    for (int i = 0; i < CommonFunction.header_records.Length; i++)
                    {
                        dataGridView.Columns[i].HeaderText = CommonFunction.header_records[i];
                        dataGridView.Columns[i].Visible = false;
                    }
                    dataGridView.Columns[0].Visible = true;            //序号
                    dataGridView.Columns[2].Visible = true;            //工装编码
                    dataGridView.Columns[13].Visible = true;           //操作类别
                    dataGridView.Columns[14].Visible = true;           //操作日期
                    dataGridView.Columns[15].Visible = true;           //操作时间
                    dataGridView.Columns[19].Visible = true;           //领用线体
                    dataGridView.Columns[22].Visible = true;           //领用人姓名
                    dataGridView.Columns[24].Visible = true;           //备注
                    break;

                case 32:    //按机型出库
                case 33:    //维修报废
                    for (int i = 0; i < CommonFunction.header_records.Length; i++)
                    {
                        dataGridView.Columns[i].HeaderCell = new DataGridViewAutoFilterColumnHeaderCell();
                        dataGridView.Columns[i].HeaderText = CommonFunction.header_records[i];
                        dataGridView.Columns[i].Visible = false;
                    }
                    dataGridView.Columns[0].Visible = true;            //序号
                    dataGridView.Columns[1].Visible = true;            //工装名称
                    dataGridView.Columns[2].Visible = true;            //工装编码
                    dataGridView.Columns[4].Visible = true;            //物料号
                    dataGridView.Columns[9].Visible = true;            //功能状态
                    dataGridView.Columns[6].Visible = true;            //架号
                    dataGridView.Columns[7].Visible = true;            //位号
                    dataGridView.Columns[8].Visible = true;            //层号
                    dataGridView.Columns[13].Visible = true;           //操作类别
                    dataGridView.Columns[22].Visible = true;           //接收人姓名
                    dataGridView.Columns[17].Visible = true;           //操作人姓名
                    dataGridView.Columns[24].Visible = true;           //备注
                    dataGridView.Columns[2].Frozen = true;  //冻结指定列
                    break;

                case 51:    //工装数据
                    for (int i = 0; i < CommonFunction.header_tools.Length; i++)
                    {
                        dataGridView.Columns[i].HeaderCell = new DataGridViewAutoFilterColumnHeaderCell();
                        dataGridView.Columns[i].HeaderText = CommonFunction.header_tools[i];
                    }
                    break;

                case 52:    //操作记录
                    for (int i = 0; i < CommonFunction.header_records.Length; i++)
                    {
                        dataGridView.Columns[i].HeaderCell = new DataGridViewAutoFilterColumnHeaderCell();
                        dataGridView.Columns[i].HeaderText = CommonFunction.header_records[i];
                    }
                    break;

                case 53:    //人员数据
                    for (int i = 0; i < CommonFunction.header_personal.Length; i++)
                    {
                        dataGridView.Columns[i].HeaderCell = new DataGridViewAutoFilterColumnHeaderCell();
                        dataGridView.Columns[i].HeaderText = CommonFunction.header_personal[i];
                    }
                    break;

                case 71:    //预警设置
                    for (int i = 0; i < CommonFunction.header_warningSetup.Length; i++)
                    {
                        dataGridView.Columns[i].HeaderCell = new DataGridViewAutoFilterColumnHeaderCell();
                        dataGridView.Columns[i].HeaderText = CommonFunction.header_warningSetup[i];
                    }
                    break;

                case 72:    //预警概览
                    for (int i = 0; i < CommonFunction.header_warnings.Length; i++)
                    {
                        dataGridView.Columns[i].HeaderCell = new DataGridViewAutoFilterColumnHeaderCell();
                        dataGridView.Columns[i].HeaderText = CommonFunction.header_warnings[i];
                    }
                    break;

                default:
                    break;

            }
        }


        /// <summary>
        /// 检查编码的合法性函数
        /// </summary>
        public static bool CheckCodeLegality(String code)
        {
            if (code.Length > 10 && code.Contains("-") && code.Length < 30)
            {
                String[] temp = code.Split('-');
                if (temp.Length == 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// 检查结尾编码的合法性函数
        /// </summary>
        public static bool CheckEndCodeLegality(String startCode, String endCode)
        {
            if (CheckCodeLegality(startCode) && CheckCodeLegality(endCode))
            {
                String startNumber = startCode.Substring(startCode.Length - 4, 4);
                String endNumber = endCode.Substring(endCode.Length - 4, 4);
                if (int.Parse(startNumber) < int.Parse(endNumber))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }

        /// <summary>
        /// 检查字符串中是否包含中文
        /// </summary>
        public static bool HasChinese(string str)
        {
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        }

        /// <summary>
        /// 文本框获得焦点函数
        /// </summary>
        public static void TextboxEnter(TextBox textbox)
        {
            textbox.ForeColor = Color.Black;
            textbox.TextAlign = HorizontalAlignment.Left;
        }
        /// <summary>
        /// 文本框失去焦点函数
        /// </summary>
        public static void TextboxLeave(TextBox textbox)
        {
            textbox.ForeColor = Color.Gray;
            textbox.TextAlign = HorizontalAlignment.Right;
        }
        /// <summary>
        /// 操作人文本框焦点函数
        /// </summary>
        public static void TextBox_operator_Enter(TextBox textBox_operator)                //操作人文本框获得焦点函数
        {
            if (textBox_operator.Text == "员工编号 ")
            {
                textBox_operator.Text = "";
            }
            CommonFunction.TextboxEnter(textBox_operator);
        }
        public static void TextBox_operator_Leave(TextBox textBox_operator, TextBox textBox_operatorName, TextBox textBox_operatorContact)//操作人文本框失去焦点函数
        {
            if (String.IsNullOrEmpty(textBox_operator.Text))
            {
                textBox_operator.Text = "员工编号 ";
                CommonFunction.TextboxLeave(textBox_operator);
            }
            else
            {
                String operator1 = textBox_operator.Text;
                String sql = "select * from personal where employeeID='" + operator1 + "' limit 1;";
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    textBox_operatorName.Text = ds.Tables[0].Rows[0][2].ToString();
                    textBox_operatorContact.Text = ds.Tables[0].Rows[0][5].ToString();
                }
            }
        }


        /// <summary>
        /// 更新人员信息库
        /// </summary>
        public static void UpdatePersonalInfo(String employeeID,String name, String contact, String line)
        {
            DataSet ds = null;
            String sql = null;
            sql = "select * from personal where employeeID='" + employeeID + "' limit 1;";
            ds = MainWindow.connection.Select(sql);
            if (ds.Tables.Count > 0 &&ds.Tables[0].Rows.Count > 0)
            {
                sql = "update personal set name='" + name + "',line='" + line + "',contact='" + contact + "' where employeeID='" + employeeID + "';";
                MainWindow.connection.Update(sql);
            }
            else
            {
                sql = "insert into personal (employeeID,name,line,contact) VALUES ('" + employeeID + "','" + name + "','" + line + "','" + contact + "');";
                MainWindow.connection.Insert(sql);
            }
            return;
        }
        public static void UpdatePersonalInfo(String employeeID, String name, String contact)   //重载
        {
            DataSet ds = null;
            String sql = null;
            sql = "select * from personal where employeeID='" + employeeID + "' limit 1;";
            ds = MainWindow.connection.Select(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                sql = "update personal set name='" + name + "',contact='" + contact + "' where employeeID='" + employeeID + "';";
                MainWindow.connection.Update(sql);
            }
            else
            {
                sql = "insert into personal (employeeID,name,contact) VALUES ('" + employeeID + "','" + name + "','" + contact + "');";
                MainWindow.connection.Insert(sql);
            }
            return;
        }

        /// <summary>
        /// 工装编码文本框回车函数
        /// </summary>
        public static void TextBox_code_KeyPress(TextBox textBox_code, TextBox textBox_focus, char e)
        {
            if (e == (char)Keys.Enter)
            {
                String code = textBox_code.Text;
                String sql = "select * from tools where code='" + code + "' order by idTools DESC limit 1";
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    textBox_focus.Focus();
                }
                else
                {
                    MessageBox.Show("无此工装！");
                    return;
                }
            }
        }
        /// <summary>
        /// 操作人文本框回车函数
        /// </summary>
        public static void TextBox_operator_KeyPress(TextBox textBox_operator, Button button_focus, TextBox textBox_focus, char e)
        {
            if (e == (char)Keys.Enter)
            {
                String operator1 = textBox_operator.Text;
                String sql = "select * from personal where employeeID='" + operator1 + "' limit 1";
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    button_focus.Focus();
                }
                else
                {
                    textBox_focus.Focus();
                }
                MainWindow.connection.Close();
                ds.Dispose();
            }
        }
        public static void TextBox_operator_KeyPress(TextBox textBox_operator, TextBox textBox_focus1, TextBox textBox_focus2, char e)
        {
            if (e == (char)Keys.Enter)
            {
                String operator1 = textBox_operator.Text;
                String sql = "select * from personal where employeeID='" + operator1 + "' limit 1";
                DataSet ds = MainWindow.connection.Select(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    textBox_focus1.Focus();
                }
                else
                {
                    textBox_focus2.Focus();
                }
                MainWindow.connection.Close();
                ds.Dispose();
            }
        }

        /// <summary>
        /// 补全数据表中的空缺日期并按日期排序
        /// </summary>
        public static DataTable DateCompletion(DataTable dt, String startDate, String endDate, String period)
        {
            int rowsCount = dt.Rows.Count;
            DateTime sDate = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.CurrentCulture);
            DateTime eDate = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.CurrentCulture);

            switch (period)
            {
                case "天":
                    {
                        DateTime tempDate = sDate;
                        int periodCount = eDate.Subtract(sDate).Days + 1;
                    
                        for (int i = 0; i < periodCount; i++)
                        {
                            bool ifhas = false;
                            if (rowsCount > 0)
                            {
                                for (int j = 0; j < rowsCount; j++)
                                {
                                    if (dt.Rows[j][0].ToString().Contains(tempDate.ToString("yyyyMMdd")))
                                    {
                                        ifhas = true;
                                        break;
                                    }
                                }
                                if (!ifhas)
                                {
                                    dt.Rows.Add(tempDate.ToString("yyyyMMdd"), "0");
                                }
                            }
                            else
                            {
                                dt.Rows.Add(tempDate.ToString("yyyyMMdd"), "0");
                            }
                            tempDate = tempDate.AddDays(1);
                        }
                        DataTable tempTable = SortTable(dt, 0);
                        return tempTable;
                    }
                case "周":
                    {
                        GregorianCalendar gc = new GregorianCalendar();
                        int currentWeek = gc.GetWeekOfYear(sDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                        int currentYear = sDate.Year;

                        int periodCount = (eDate.Subtract(sDate).Days + 1) / 7;
                        
                        for (int i = 0; i < periodCount; i++)
                        {
                            bool ifhas = false;
                            if (rowsCount > 0)
                            {
                                for (int j = 0; j < rowsCount; j++)
                                {
                                    if (dt.Rows[j][0].ToString().Contains(currentYear.ToString() + currentWeek.ToString().PadLeft(2, '0')))
                                    {
                                        ifhas = true;
                                        break;
                                    }
                                }
                                if (!ifhas)
                                {
                                    dt.Rows.Add(currentYear.ToString() + currentWeek.ToString().PadLeft(2, '0'), "0");
                                }
                            }
                            else
                            {
                                dt.Rows.Add(currentYear.ToString() + currentWeek.ToString().PadLeft(2, '0'), "0");
                            }
                            currentWeek += 1;
                            if(currentWeek >= 52)
                            {
                                currentWeek = 1;
                                currentYear += 1;
                            }
                        }
                        DataTable tempTable = SortTable(dt, 0);
                        return tempTable;
                    }
                case "月":
                    {
                        int currentYear = sDate.Year;
                        int currentMonth = sDate.Month;
                        int lastYear = eDate.Year;
                        int lastMonth = eDate.Month;
                        int periodCount = (lastYear - currentYear) * 12 + (lastMonth - currentMonth) + 1;
                        
                        for (int i = 0; i < periodCount; i++)
                        {
                            bool ifhas = false;
                            if (rowsCount > 0)
                            {
                                for (int j = 0; j < rowsCount; j++)
                                {
                                    if (dt.Rows[j][0].ToString().Contains(currentYear.ToString() + currentMonth.ToString().PadLeft(2, '0')))
                                    {
                                        ifhas = true;
                                        break;
                                    }
                                }
                                if (!ifhas)
                                {
                                    dt.Rows.Add(currentYear.ToString() + currentMonth.ToString().PadLeft(2, '0'), "0");
                                }
                            }
                            else
                            {
                                dt.Rows.Add(currentYear.ToString() + currentMonth.ToString().PadLeft(2, '0'), "0");
                            }
                            currentMonth += 1;
                            if (currentMonth > 12)
                            {
                                currentMonth = 1;
                                currentYear += 1;
                            }
                        }
                        DataTable tempTable = SortTable(dt, 0);
                        return tempTable;
                    }
                case "年":
                    {
                        int currentYear = sDate.Year;
                        int lastYear = eDate.Year;

                        int periodCount = lastYear - currentYear + 1;

                        for (int i = 0; i < periodCount; i++)
                        {
                            bool ifhas = false;
                            if (rowsCount > 0)
                            {
                                for (int j = 0; j < rowsCount; j++)
                                {
                                    if (dt.Rows[j][0].ToString().Contains(currentYear.ToString()))
                                    {
                                        ifhas = true;
                                        break;
                                    }
                                }
                                if (!ifhas)
                                {
                                    dt.Rows.Add(currentYear.ToString(), "0");
                                }
                            }
                            else
                            {
                                dt.Rows.Add(currentYear.ToString(), "0");
                            }
                            currentYear += 1;
                        }
                        DataTable tempTable = SortTable(dt, 0);
                        return tempTable;
                    }
                default:
                    {
                        MessageBox.Show("周期错误");
                        return null;
                    }
            }//switch
            
        }

        /// <summary>
        /// 将Table按指定列排序
        /// </summary>
        public static DataTable SortTable(DataTable dt, int sortColumn)
        {
            String columnsName = dt.Columns[sortColumn].ColumnName;
            DataTable tempTable = dt.Clone();
            tempTable.Columns[sortColumn].DataType = typeof(int);   //指定排序列为Int类型

            foreach (DataRow row in dt.Rows)
            {
                tempTable.ImportRow(row);   //导入旧数据
            }

            tempTable.DefaultView.Sort = columnsName + " ASC";
            tempTable = tempTable.DefaultView.ToTable();

            dt.Clear() ;
            foreach (DataRow row in tempTable.Rows)
            {
                dt.ImportRow(row);   //导回新数据
            }

            return dt;
        }


        /// <summary>
        /// DataSet导出到Excel
        /// </summary>

        public static void DataSetToExcel(DataSet dataSet, String savePath)
        {
            DataTable dataTable = dataSet.Tables[0];    //默认只导出第一个table
            int rowNumber = dataTable.Rows.Count;       //不包括字段名
            int columnNumber = dataTable.Columns.Count;
            int colIndex = 0;
            
            //建立Excel对象
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Application.Workbooks.Add(true);

            //这样就只会打开一个sheet，不会出现下面说到的打开两个sheet：一个空白，一个有数据的情况
            Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.ActiveWorkbook;
            Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.ActiveSheet;
            Microsoft.Office.Interop.Excel.Range range;

            try
            {
                //生成字段名称 
                foreach (DataColumn col in dataTable.Columns)
                {
                    colIndex++;
                    excelApp.Cells[1, colIndex] = col.ColumnName;
                }

                object[,] objData = new object[rowNumber, columnNumber];

                for (int r = 0; r < rowNumber; r++)
                {
                    for (int c = 0; c < columnNumber; c++)
                    {
                        objData[r, c] = dataTable.Rows[r][c];
                    }
                    Application.DoEvents();
                }

                // 写入Excel
                range = excelApp.get_Range((object)worksheet.Cells[2, 1], (object)worksheet.Cells[rowNumber + 1, columnNumber]);
                range.NumberFormat = "@";       //设置单元格为文本格式
                range.Value2 = objData;
                //excelApp.get_Range((object)worksheet.Cells[2, 1], (object)worksheet.Cells[rowNumber + 1, 1]).NumberFormat = "yyyy-m-d h:mm";

                workbook.SaveAs(savePath);
                MessageBox.Show("导出成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                workbook.Close();
                workbook = null;
                excelApp.Quit();
                KillProcess(excelApp);
            }
        }


        /// <summary>
        /// 杀进程
        /// </summary>
        /// 
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);

        public static bool KillProcess(Microsoft.Office.Interop.Excel.Application excel)
        {
            try
            {
                IntPtr t = new IntPtr(excel.Hwnd);   //得到Excel的句柄
                int tag = 0;
                GetWindowThreadProcessId(t, out tag);   //获取本进程id
                System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(tag);   //获取此进程的引用
                p.Kill();     //关闭进程，世界真美好
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        /// <summary>
        /// 预警更新
        /// </summary>
        /// 
        public static bool WarningUpdate()
        {
            DataSet ds1, ds2;
            String sql = "select * from warningSetup";
            ds1 = MainWindow.connection.Select(sql);
            sql = "truncate table warnings";
            MainWindow.connection.Truncate(sql);
            int idWarnings;
            String name, materialNumber, type, method, remarks;
            float value, totalValue, currentValue;
            char flag = '否';

            if (ds1.Tables.Count>0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow row1 in ds1.Tables[0].Rows)
                {
                    idWarnings = int.Parse(row1[0].ToString());
                    name = row1[1].ToString();
                    materialNumber = row1[2].ToString();
                    type = row1[3].ToString();
                    method = row1[4].ToString();
                    value = float.Parse(row1[5].ToString());
                    remarks = row1[6].ToString();

                    switch (name)
                    {
                        case "物料号":
                            {
                                switch (type)
                                {
                                    case "待修数量":
                                        {
                                            sql = "SELECT materialNumber, count(*) AS A, count(IF(functionState = '待修', 1, NULL)) AS B FROM tools WHERE materialNumber = '" + materialNumber + "' GROUP BY materialNumber;";
                                            break;
                                        }
                                    case "安全库存":
                                        {
                                            sql = "SELECT materialNumber, count(*) AS A, count(IF(storageState LIKE '%上架%' AND functionState = '正常', 1, NULL)) AS B FROM tools WHERE materialNumber = '" + materialNumber + "' GROUP BY materialNumber;";
                                            break;
                                        }
                                }
                                break;
                            }
                        case "工装类别":
                            {
                                switch (type)
                                {
                                    case "待修数量":
                                        {
                                            sql = "SELECT materialNumber, count(*) AS A, count(IF(functionState = '待修', 1, NULL)) AS B FROM tools WHERE category = '" + materialNumber + "' GROUP BY materialNumber;";
                                            break;
                                        }
                                    case "安全库存":
                                        {
                                            sql = "SELECT materialNumber, count(*) AS A, count(IF(storageState LIKE '%上架%' AND functionState = '正常', 1, NULL)) AS B FROM tools WHERE category = '" + materialNumber + "' GROUP BY materialNumber;";
                                            break;
                                        }
                                }
                                break;
                            }
                    }
                    //写入预警概览库
                    ds2 = MainWindow.connection.Select(sql);
                    if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row2 in ds2.Tables[0].Rows)
                        {
                            flag = '否';
                            materialNumber = row2[0].ToString();
                            totalValue = float.Parse(row2[1].ToString());
                            currentValue = float.Parse(row2[2].ToString());
                            if (type.Contains("安全库存"))
                            {
                                if (currentValue <= value)
                                    flag = '是';
                            }
                            else
                            {
                                if (method.Contains("数量"))
                                {
                                    if (currentValue >= value)
                                        flag = '是';
                                }
                                else
                                {
                                    if (currentValue / totalValue * 100 >= value)
                                        flag = '是';
                                }
                            }
                            sql = @"insert into warnings " +
                                "(name, materialNumber, type, method, value, totalValue, currentValue, remarks, updateTime, flag) " +
                                "values ('" + name + "','" + materialNumber + "','" + type + "','" + method + "','" + value + "','" +
                                totalValue + "','" + currentValue + "','" + remarks + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','"+ flag + "');";
                            MainWindow.connection.Insert(sql);
                        }
                    }
                }
            }
            //更新状态栏提示
            sql = "select count(*) from warnings where flag = '是';";
            ds2 = MainWindow.connection.Select(sql);
            if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                String count = ds2.Tables[0].Rows[0][0].ToString();
                Program.mw.statusStripStatusLabel_warnings.Text = "您有 " + count + " 条预警信息";
            }

            return true;
        }


        /// <summary>
        /// 绘制DataGridView控件
        /// </summary>
        /// 
        public static void DrawDataGridView(DataGridView dataGridView)   //绘制一个表格
        {
            int dataGridViewWidth = Screen.PrimaryScreen.Bounds.Width - dataGridView.Location.X * 2 - Program.mw.toolStrip1.Width;
            int dataGridViewHeight = Screen.PrimaryScreen.Bounds.Height - dataGridView.Location.Y - Program.mw.statusStrip1.Height - Program.mw.menuStrip1.Height - 95;
            dataGridView.Size = new System.Drawing.Size(dataGridViewWidth, dataGridViewHeight);
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;  //列标题居中
            dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Font = new Font("微软雅黑", 9);
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LemonChiffon;    //奇数行背景色
            dataGridView.AutoResizeColumns();
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView.RowHeadersWidth = 20;
        }

        public static void DrawDataGridView_Two(DataGridView dataGridView1, DataGridView dataGridView2)    //针对领用归还和工装出库界面，绘制两个表格
        {
            int dataGridViewWidth = Screen.PrimaryScreen.Bounds.Width - dataGridView1.Location.X * 2 - Program.mw.toolStrip1.Width;
            int dataGridViewHeight = Screen.PrimaryScreen.Bounds.Height - dataGridView1.Location.Y - Program.mw.statusStrip1.Height - Program.mw.menuStrip1.Height - 95;
            float dataGridViewWidth1 = dataGridViewWidth * (float)0.6;
            int dataGridViewWidth2 = dataGridViewWidth - (int)dataGridViewWidth1-10;
            dataGridView1.Size = new System.Drawing.Size((int)dataGridViewWidth1, dataGridViewHeight);
            dataGridView2.Size = new System.Drawing.Size((int)dataGridViewWidth2, dataGridViewHeight);
            dataGridView2.Location = new System.Drawing.Point(dataGridView1.Location.X + (int)dataGridViewWidth1+10, dataGridView1.Location.Y);

            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;  //列标题居中
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;   //内容居中
            dataGridView1.Font = new Font("微软雅黑", 9);
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LemonChiffon;    //奇数行背景色
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.RowHeadersWidth = 20;

            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;  //列标题居中
            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;   //内容居中
            dataGridView2.Font = new Font("微软雅黑", 9);
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.LemonChiffon;    //奇数行背景色
            dataGridView2.AutoResizeColumns();
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView2.RowHeadersWidth = 20;
        }

        /// <summary>
        /// 刷新DataGridView的数据
        /// </summary>
        /// 
        //记录数据库  根据操作刷新
        public static void FillDataGridView_Records(DataGridView dataGridView, String operation)
        {
            //重新获取数据
            String sql = "SELECT * FROM records WHERE operationType LIKE '%"+ operation +"%' ORDER BY idRecords DESC;";
            DataSet ds = MainWindow.connection.Select(sql);

            //重新绑定数据
            if (ds.Tables.Count > 0)
            {
                BindingSource dataSource = new BindingSource(ds.Tables[0], null);
                dataGridView.DataSource = dataSource;
            }
        }
        //记录数据库  根据操作刷新，带limit数量
        public static void FillDataGridView_Records(DataGridView dataGridView, String operation, String limit)
        {
            //重新获取数据
            String sql = "SELECT * FROM records WHERE operationType LIKE '%" + operation + "%' ORDER BY idRecords DESC limit "+limit+" ;";
            DataSet ds = MainWindow.connection.Select(sql);

            //重新绑定数据
            if (ds.Tables.Count > 0)
            {
                BindingSource dataSource = new BindingSource(ds.Tables[0], null);
                dataGridView.DataSource = dataSource;
            }
        }
        //记录数据库  根据操作刷新，带BindingNavigator
        public static DataSet FillDataGridView_Records(DataGridView dataGridView, MyBindingNavigator myBindingNavigator, BindingNavigator bindingNavigator, String operation)
        {
            //重新获取数据
            String sql = "SELECT * FROM records WHERE operationType LIKE '%" + operation + "%' ORDER BY idRecords DESC;";
            DataSet ds = MainWindow.connection.Select(sql);

            //重新绑定数据
            if (ds.Tables.Count > 0)
            {
                BindingSource dataSource = new BindingSource(ds.Tables[0], null);
                dataGridView.DataSource = dataSource;
                myBindingNavigator.InitDataTable(bindingNavigator, dataGridView, ds);
            }
            return ds;
        }
        //记录数据库  根据所给的数据刷新，带BindingNavigator
        public static void FillDataGridView_Records(DataGridView dataGridView, MyBindingNavigator myBindingNavigator, BindingNavigator bindingNavigator, DataSet dataSet)
        {
            //数据表ds通过传参获得
            DataSet ds = dataSet;
            //重新绑定数据
            if (ds.Tables.Count > 0)
            {
                BindingSource dataSource = new BindingSource(ds.Tables[0], null);
                dataGridView.DataSource = dataSource;
                myBindingNavigator.InitDataTable(bindingNavigator, dataGridView, ds);
            }
        }

        //工装数据库  根据工装编码刷新
        public static void FillDataGridView_Tools(DataGridView dataGridView, String code)
        {
            //重新获取数据
            String sql = "SELECT * FROM tools WHERE code LIKE '%" + code + "%' ORDER BY idTools;";
            DataSet ds = MainWindow.connection.Select(sql);

            //重新绑定数据
            if (ds.Tables.Count > 0)
            {
                BindingSource dataSource = new BindingSource(ds.Tables[0], null);
                dataGridView.DataSource = dataSource;
            }
        }

        //工装数据库  根据工装编码刷新，带BindingNavigator
        public static void FillDataGridView_Tools(DataGridView dataGridView, MyBindingNavigator myBindingNavigator, BindingNavigator bindingNavigator, String code)
        {
            //重新获取数据
            String sql = "SELECT * FROM tools WHERE code LIKE '%" + code + "%' ORDER BY idTools;";
            DataSet ds = MainWindow.connection.Select(sql);

            //重新绑定数据
            if (ds.Tables.Count > 0)
            {
                MainWindow.public_dsToolsData = ds;
                BindingSource dataSource = new BindingSource(ds.Tables[0], null);
                dataGridView.DataSource = dataSource;
                myBindingNavigator.InitDataTable(bindingNavigator, dataGridView, ds);
            }
        }
        //工装数据库  根据所给的数据刷新
        public static void FillDataGridView_Tools(DataGridView dataGridView, DataSet dataSet)
        {
            //数据表ds通过传参获得
            DataSet ds = dataSet;
            //重新绑定数据
            if (ds.Tables.Count > 0)
            {
                BindingSource dataSource = new BindingSource(ds.Tables[0], null);
                dataGridView.DataSource = dataSource;
            }
        }
        //工装数据库  根据所给的数据刷新，带BindingNavigator
        public static void FillDataGridView_Tools(DataGridView dataGridView, MyBindingNavigator myBindingNavigator, BindingNavigator bindingNavigator, DataSet dataSet)
        {
            //数据表ds通过传参获得
            DataSet ds = dataSet;
            //重新绑定数据
            if (ds.Tables.Count > 0)
            {
                BindingSource dataSource = new BindingSource(ds.Tables[0], null);
                dataGridView.DataSource = dataSource;
                myBindingNavigator.InitDataTable(bindingNavigator, dataGridView, ds);
            }
        }

        //人员数据库  根据员工编号刷新
        public static void FillDataGridView_Persons(DataGridView dataGridView, String employeeID)
        {
            //重新获取数据
            String sql = "SELECT * FROM personal WHERE employeeID LIKE '%" + employeeID + "%' ORDER BY idPersonal;";
            DataSet ds = MainWindow.connection.Select(sql);

            //重新绑定数据
            if (ds.Tables.Count > 0)
            {
                BindingSource dataSource = new BindingSource(ds.Tables[0], null);
                dataGridView.DataSource = dataSource;
            }
        }
        //人员数据库  根据员工编号刷新，带BindingNavigator
        public static void FillDataGridView_Persons(DataGridView dataGridView, MyBindingNavigator myBindingNavigator, BindingNavigator bindingNavigator, String employeeID)
        {
            //重新获取数据
            String sql = "SELECT * FROM personal WHERE employeeID LIKE '%" + employeeID + "%' ORDER BY idPersonal;";
            DataSet ds = MainWindow.connection.Select(sql);

            //重新绑定数据
            if (ds.Tables.Count > 0)
            {
                MainWindow.public_dsPersonData = ds;
                BindingSource dataSource = new BindingSource(ds.Tables[0], null);
                dataGridView.DataSource = dataSource;
                myBindingNavigator.InitDataTable(bindingNavigator, dataGridView, ds);
            }
        }
        //人员数据库  根据所给的数据刷新
        public static void FillDataGridView_Persons(DataGridView dataGridView, DataSet dataSet)
        {
            //数据表ds通过传参获得
            DataSet ds = dataSet;
            //重新绑定数据
            if (ds.Tables.Count > 0)
            {
                BindingSource dataSource = new BindingSource(ds.Tables[0], null);
                dataGridView.DataSource = dataSource;
            }
        }
        //人员数据库  根据所给的数据刷新，带BindingNavigator
        public static void FillDataGridView_Persons(DataGridView dataGridView, MyBindingNavigator myBindingNavigator, BindingNavigator bindingNavigator, DataSet dataSet)
        {
            //数据表ds通过传参获得
            DataSet ds = dataSet;
            //重新绑定数据
            if (ds.Tables.Count > 0)
            {
                BindingSource dataSource = new BindingSource(ds.Tables[0], null);
                dataGridView.DataSource = dataSource;
                myBindingNavigator.InitDataTable(bindingNavigator, dataGridView, ds);
            }
        }


        //预警设置数据库  带BindingNavigator
        public static void FillDataGridView_WarningManage(DataGridView dataGridView, MyBindingNavigator myBindingNavigator, BindingNavigator bindingNavigator)
        {
            String sql = "SELECT * FROM warningSetup;";
            DataSet ds = MainWindow.connection.Select(sql);

            if (ds.Tables.Count > 0)
            {
                BindingSource dataSource = new BindingSource(ds.Tables[0], null);
                dataGridView.DataSource = dataSource;
                myBindingNavigator.InitDataTable(bindingNavigator, dataGridView, ds);
            }
        }
        //预警概览数据库  带BindingNavigator
        public static void FillDataGridView_WarningOverview(DataGridView dataGridView, MyBindingNavigator myBindingNavigator, BindingNavigator bindingNavigator)
        {
            String sql = "SELECT * FROM warnings where flag = '是';";
            DataSet ds = MainWindow.connection.Select(sql);

            if (ds.Tables.Count > 0)
            {
                BindingSource dataSource = new BindingSource(ds.Tables[0], null);
                dataGridView.DataSource = dataSource;
                myBindingNavigator.InitDataTable(bindingNavigator, dataGridView, ds);
            }
        }

        /// <summary>
        /// 制作各panel中的库存展示框
        /// </summary>
        /// 
        public static void Fill_GroupBox_Info(Panel panel, String materialNumber, Button button)
        {
            Program.mw.groupBox_common_info.Parent = panel;
            Program.mw.groupBox_common_info.Location = new Point(button.Location.X + 120, button.Location.Y - 10);

            String sql = "SELECT storageState, count(*) FROM tools WHERE materialNumber='" + materialNumber + "' GROUP BY storageState;";
            DataSet ds = MainWindow.connection.Select(sql);
            int in1 = 0;
            int out1 = 0;
            int repair = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row[0].ToString().Contains("上架"))
                    in1 += int.Parse(row[1].ToString());
                if (row[0].ToString().Contains("出借"))
                    out1 += int.Parse(row[1].ToString());
                if (row[0].ToString().Contains("送修"))
                    repair += int.Parse(row[1].ToString());
            }
            Program.mw.label_common_info_in.Text = in1.ToString();
            Program.mw.label_common_info_out.Text = out1.ToString();
            Program.mw.label_common_info_repair.Text = repair.ToString();
        }
        //只更新数据的
        public static void Fill_GroupBox_Info(String materialNumber)
        {
            String sql = "SELECT storageState, count(*) FROM tools WHERE materialNumber='" + materialNumber + "' GROUP BY storageState;";
            DataSet ds = MainWindow.connection.Select(sql);
            int in1 = 0;
            int out1 = 0;
            int repair = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row[0].ToString().Contains("上架"))
                    in1 += int.Parse(row[1].ToString());
                if (row[0].ToString().Contains("出借"))
                    out1 += int.Parse(row[1].ToString());
                if (row[0].ToString().Contains("送修"))
                    repair += int.Parse(row[1].ToString());
            }
            Program.mw.label_common_info_in.Text = in1.ToString();
            Program.mw.label_common_info_out.Text = out1.ToString();
            Program.mw.label_common_info_repair.Text = repair.ToString();
        }



    }
}
