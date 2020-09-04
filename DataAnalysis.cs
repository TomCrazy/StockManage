using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data;

namespace nsStockManage
{
    class DataAnalysis
    {
        //DBConnection connection = new DBConnection();
        DataSet ds = new DataSet();
        String sql = null;
        bool addSeries = false;         //是否勾选“添加曲线”标志位
        int num = 1;                    //添加曲线时的计数
        readonly Color[] colors = { Color.Blue , Color.Aqua , Color.BlueViolet , Color.Brown , Color.BurlyWood , Color.CadetBlue , Color.Chartreuse ,
        Color.Chocolate , Color.Coral , Color.Crimson , Color.CornflowerBlue , Color.Cyan , Color.DarkCyan , Color.DarkGoldenrod , Color.DarkGreen ,
        Color.DarkBlue , Color.DarkKhaki , Color.DarkOrange , Color.DarkRed , Color.DeepPink , Color.Gold , Color.Gray };
        HitTestResult Result = new HitTestResult();     //鼠标指向事件的辅助对象

        public bool Button_DataAnalysis_Analyze()
        {
            String inquiryMode = Program.mw.comboBox_dataAnalysis_inquiryMode.Text;
            String materialNumber = Program.mw.textBox_dataAnalysis_materialNumber.Text;
            String analysisType = Program.mw.comboBox_dataAnalysis_analysisType.Text;
            String period = Program.mw.comboBox_dataAnalysis_period.Text;
            String startDate = Program.mw.dateTimePicker_dataAnalysis_startDate.Text;
            String endDate = Program.mw.dateTimePicker_dataAnalysis_endDate.Text;
            String operationType = "";
            String strPeriod = "";
            String strInquiryMode = inquiryMode;
            String legend = inquiryMode + materialNumber + analysisType + "数";

            if(Convert.ToDateTime(endDate) < Convert.ToDateTime(startDate))
            {
                MessageBox.Show("结束日期不得早于起始日期！");
                return false;
            }

            if (inquiryMode.Contains("物料号"))
            {
                strInquiryMode = "materialNumber";
            }
            else
            {
                strInquiryMode = "category";
            }
            switch (period)
            {
                case "天":
                    strPeriod = "%Y%m%d";
                    break;
                case "周":
                    strPeriod = "%Y%u";
                    break;
                case "月":
                    strPeriod = "%Y%m";
                    break;
                case "年":
                    strPeriod = "%Y";
                    break;
            }
            switch (analysisType)
            {
                case "送修":
                    operationType = "维修出库";
                    break;
                case "报废":
                    operationType = "报废出库";
                    break;
                case "领用":
                    operationType = "出借";
                    break;
                case "归还":
                    operationType = "领用归还";
                    break;
                case "采购":
                    operationType = "新购入库";
                    break;
            }

            try
            {
                sql = "SELECT DATE_FORMAT(operationDate,'" + strPeriod + "') 日期, COUNT(*) "+ legend +" " +
                      "FROM records " +
                      "WHERE 1=1 AND " + strInquiryMode + " LIKE '%" + materialNumber + "%' AND operationType LIKE '%" + operationType + "%' AND operationDate BETWEEN '" + startDate + "' AND '" + endDate + "' " +
                      "GROUP BY 日期 ORDER BY operationDate;";
                DataSet dsTemp = MainWindow.connection.Select(sql);
                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    DataTable dtTemp = CommonFunction.DateCompletion(dsTemp.Tables[0], startDate, endDate, period); //数据补全
                    if (addSeries)
                    {
                        if(ds.Tables.Count > 0)     //已有数据
                        {
                            num++;
                            ds.Tables[0].Columns.Add(legend, typeof(long));    //直接添加新列
                            int columnCount = ds.Tables[0].Columns.Count;
                            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ds.Tables[0].Rows[i][columnCount -1] = dtTemp.Rows[i][1];
                            }
                        }
                        else
                        {
                            ds.Tables.Add(dtTemp.Copy());      //新添表
                        }
                    }
                    else
                    {
                        ds.Tables.Clear();
                        ds.Tables.Add(dtTemp.Copy());
                        num = 1;
                    }
                    DataAnalysis_AddSeries(Program.mw.chart_dataAnalysis_trend , num , legend);
                }
                else
                {
                    MessageBox.Show("无数据！");
                }
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public void Button_DataAnalysis_Export()
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                //设置文件类型 
                Filter = "xlsx文件|*.xlsx|xls文件|*.xls",

                //设置默认文件类型显示顺序 
                FilterIndex = 1,

                //保存对话框是否记忆上次打开的目录 
                RestoreDirectory = true
            };

            //点了保存按钮进入 
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                String localFilePath = sfd.FileName.ToString();     //获得文件路径 
                //String fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径
                //给文件名前加上时间 
                //String newFileName = DateTime.Now.ToString("yyyyMMddHHmm") +"-"+ fileNameExt; 

                CommonFunction.DataSetToExcel(ds, localFilePath);
            }
        }

        public void CheckBox_DataAnalysis_Add_CheckedChanged()      //添加复选框 状态变化函数
        {
            if(Program.mw.checkBox_dataAnalysis_add.Checked == true)
            {
                Program.mw.button_dataAnalysis_analyze.Text = "添  加";
                Program.mw.comboBox_dataAnalysis_period.Enabled = false;
                Program.mw.dateTimePicker_dataAnalysis_startDate.Enabled = false;
                Program.mw.dateTimePicker_dataAnalysis_endDate.Enabled = false;
                addSeries = true;
            }
            else
            {
                Program.mw.button_dataAnalysis_analyze.Text = "查  询";
                Program.mw.comboBox_dataAnalysis_period.Enabled = true;
                Program.mw.dateTimePicker_dataAnalysis_startDate.Enabled = true;
                Program.mw.dateTimePicker_dataAnalysis_endDate.Enabled = true;
                addSeries = false;
            }

        }

        public void DataAnalysis_CleanAll()
        {
            Program.mw.textBox_dataAnalysis_materialNumber.Text = "";
            Program.mw.comboBox_dataAnalysis_inquiryMode.Text = "物料号";
            Program.mw.comboBox_dataAnalysis_period.Text = "天";
            Program.mw.comboBox_dataAnalysis_analysisType.Text = "维修";
            Program.mw.dateTimePicker_dataAnalysis_startDate.ResetText();
            Program.mw.dateTimePicker_dataAnalysis_endDate.ResetText();
            Program.mw.chart_dataAnalysis_trend.Series.Clear();
        }

        public void DataAnalysis_DrawChart(Chart chart)
        {
            int chartDataTrendWidth = Screen.PrimaryScreen.Bounds.Width - chart.Location.X * 2 - Program.mw.toolStrip1.Width;
            int chartDataTrendHeight = Screen.PrimaryScreen.Bounds.Height - chart.Location.Y - Program.mw.statusStrip1.Height - Program.mw.menuStrip1.Height - 85;
            chart.Size = new System.Drawing.Size(chartDataTrendWidth, chartDataTrendHeight);
            Program.mw.button_dataAnalysis_export.Location = new Point(Program.mw.groupBox_dataAnalysis_screen.Location.X + 560, Program.mw.button_dataAnalysis_clear.Location.Y + 13);
        }

        public void DataAnalysis_AddSeries(Chart chart, int num, String legend)     //添加曲线
        {
            if(num <= 1)
            {
                chart.DataSource = ds.Tables[0];
                chart.Series.Clear();
            }

            Series series = new Series(legend)
            {
                ChartType = SeriesChartType.Spline,             //图类型(折线)
                BorderWidth = 2,                                //线条粗细
                Color = colors[num],                            //线条颜色
                MarkerColor = colors[num],                      //数据点颜色
                MarkerSize = 6,                                 //数据点大小
                MarkerStyle = MarkerStyle.Circle,               //数据点形状
                LegendText = legend,                            //图例内容
            };
            
            series.XValueMember = ds.Tables[0].Columns[0].ColumnName;       // "period";
            series.YValueMembers = ds.Tables[0].Columns[num].ColumnName;    // "count";
            series.XValueType = ChartValueType.String;
            series.YValueType = ChartValueType.Int64;
            chart.Series.Add(series);
            chart.DataBind();

            Program.mw.button_dataAnalysis_export.Visible = true;
        }

        public void DataAnalysis_chart_MouseMove(object sender, MouseEventArgs e)
        {/*
            if (e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                int i = e.HitTestResult.PointIndex;
                DataPoint dp = e.HitTestResult.Series.Points[i];
                //分别显示x轴和y轴的数值，其中{1:F3},表示显示的是float类型，精确到小数点后3位。  
                e.Text = string.Format("X:{0};Y:{1} ", dp.XValue, dp.YValues[0]);
            }*/
            
            Result = Program.mw.chart_dataAnalysis_trend.HitTest(e.X, e.Y);
            if(Result.ChartElementType == ChartElementType.DataPoint)
            {
                int i = Result.PointIndex;
                DataPoint dp = Result.Series.Points[i];
                dp.ToolTip = "#VALX,#VALY";
            }
        }

        //工装编码文本框回车函数
        public void TextBox_dataAnalysis_materialNumber_KeyPress(char e)
        {
            if (e == (char)Keys.Enter)
            {
                String code = Program.mw.textBox_dataAnalysis_materialNumber.Text;
                if (CommonFunction.CheckCodeLegality(code))
                {
                    String[] temp = code.Split('-');
                    String materialNumber = temp[1];
                    Program.mw.textBox_dataAnalysis_materialNumber.Text = materialNumber;
                }
            }
        }



    }
}
