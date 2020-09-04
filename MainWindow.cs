using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataGridViewAutoFilter;

namespace nsStockManage
{
    public partial class MainWindow : Form
    {
        // 构造函数
        public MainWindow()
        {
            InitializeComponent();
            this.Load += new EventHandler(Init_Load);               //初始化程序
        }

        // 全局变量
        public static String mainPath = null;                       //系统目录
        public static String configPath = null;                     //配置文件目录
        public static String TerminalNumber = null;                 //终端编号标识符
        public static String serverIP = null;                       //数据库服务器地址
        public static String mysqlDatabase = null;                  //数据库名称
        public static String mysqlUserName = null;                  //数据库用户名
        public static String mysqlPassword = null;                  //数据库密码
        public static DBConnection connection = null;               //公用数据库连接对象
        public MyBindingNavigator myBindingNavigator_ToolsData = new MyBindingNavigator();
        public MyBindingNavigator myBindingNavigator_RecordsData = new MyBindingNavigator();
        public MyBindingNavigator myBindingNavigator_PersonsData = new MyBindingNavigator();
        public MyBindingNavigator myBindingNavigator_NewToolsIn = new MyBindingNavigator();
        public MyBindingNavigator myBindingNavigator_RepairToolsIn = new MyBindingNavigator();
        public MyBindingNavigator myBindingNavigator_ScrapTools = new MyBindingNavigator();
        public MyBindingNavigator myBindingNavigator_WarningSetUp = new MyBindingNavigator();
        public MyBindingNavigator myBindingNavigator_WarningOverview = new MyBindingNavigator();

        // 公共数据表，各panel界面加载用
        public static DataSet public_dsToolsData = new DataSet();
        public static DataSet public_dsRecordsData = new DataSet();
        public static DataSet public_dsPersonData = new DataSet();

        public static bool mySqlConnectionState = false;            //数据库连接状态标识符


        // 局部变量
        MenuStrip menuStrip = new MenuStrip();
        ToolStrip toolStrip = new ToolStrip();
        ToolsIn toolsIn = new ToolsIn();
        ToolsOut toolsOut = new ToolsOut();
        ShelfManage shelfManage = new ShelfManage();
        DataManage dataManage = new DataManage();
        DataAnalysis dataAnalysis = new DataAnalysis();
        WarningManage warningManage = new WarningManage();

        //启动加载函数
        private void Init_Load(object sender, EventArgs e)
        {
            //实例化主窗口
            Program.mw = this;

            //获取程序的基目录：System.AppDomain.CurrentDomain.BaseDirectory
            mainPath = System.IO.Directory.GetCurrentDirectory();
            configPath = mainPath + "/config.txt";                  //配置文件目录
            SetConfig.ReadConfig(configPath);                       //读取配置文件中的内容

            //连接数据库
            connection = new DBConnection();
            if (connection.Open())
            {
                connection.Close();
            }
            else
            {
                System.Environment.Exit(0);
            }

            this.menuStrip.终端管理ToolStripMenuItem_Click();
            //检测客户端名称，若已存在检测是否重复,若已存在则导入，若无则弹出登陆界面
            ClientManageFunction cmf = new ClientManageFunction();
            cmf.CheckTerminal();

            //检索预警项，显示报警信息
            CommonFunction.WarningUpdate();

            //建立时钟
            Timer timer = new Timer();                                   //实例化一个timer
            timer.Enabled = true;                                        //使timer可用
            timer.Interval = 10800000;                                   //设置时间间隔，每3小时刷新一次(以毫秒为单位)
            timer.Tick += new EventHandler(CommonFunction.RefreshAllTime); //给timer关联函数

            DateTime buildDate = System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location);////////////////////////////////////

            //绘制各panel中的DataGridView
            FillDataGridViews();
            DrawDataGridViews();
        }

        

        /*****************************************  系统启动加载相关函数  *******************************************/
        private void FillDataGridViews()
        {
            //新购入库
            CommonFunction.FillDataGridView_Records(dataGridView_NewToolsIn, myBindingNavigator_NewToolsIn, bindingNavigator_NewToolsIn, "新购");
            CommonFunction.DrawDataGridViewHeader(dataGridView_NewToolsIn, 21);
            //领用归还
            CommonFunction.FillDataGridView_Records(dataGridView_toolsReturn_records, "领用归还", "30");
            CommonFunction.DrawDataGridViewHeader(dataGridView_toolsReturn_records, 22);
            //维修入库
            CommonFunction.FillDataGridView_Records(dataGridView_RepairToolsIn, myBindingNavigator_RepairToolsIn, bindingNavigator_RepairToolsIn, "维修入库");
            CommonFunction.DrawDataGridViewHeader(dataGridView_RepairToolsIn, 23);
            //按工装出借
            CommonFunction.FillDataGridView_Records(dataGridView_outByTools_records, "按工装出借", "30");
            CommonFunction.DrawDataGridViewHeader(dataGridView_outByTools_records, 31);
            //维修报废
            CommonFunction.FillDataGridView_Records(dataGridView_ScrapTools, myBindingNavigator_ScrapTools, bindingNavigator_ScrapTools, "维修%' OR operationType LIKE '%报废");
            CommonFunction.DrawDataGridViewHeader(dataGridView_ScrapTools, 33);
            //工装上架
            shelfManage.FillListView_putOnShelf(listView_putOnShelf);
            //工装移位
            shelfManage.FillListView_changeShelf(listView_changeShelf);
            //工装数据管理
            CommonFunction.FillDataGridView_Tools(dataGridView_toolsData, myBindingNavigator_ToolsData, bindingNavigator_ToolsData, "");
            CommonFunction.DrawDataGridViewHeader(dataGridView_toolsData, 51);
            //操作记录管理
            public_dsRecordsData = CommonFunction.FillDataGridView_Records(dataGridView_recordsData, myBindingNavigator_RecordsData, bindingNavigator_RecordsData, "");
            CommonFunction.DrawDataGridViewHeader(dataGridView_recordsData, 52);
            //人员数据管理
            CommonFunction.FillDataGridView_Persons(dataGridView_personsData, myBindingNavigator_PersonsData, bindingNavigator_PersonsData, "");
            CommonFunction.DrawDataGridViewHeader(dataGridView_personsData, 53);
            //预警设置
            CommonFunction.FillDataGridView_WarningManage(dataGridView_warningSetUp, myBindingNavigator_WarningSetUp, bindingNavigator_WarningSetUp);
            CommonFunction.DrawDataGridViewHeader(dataGridView_warningSetUp, 71);
            //预警概览
            CommonFunction.FillDataGridView_WarningOverview(dataGridView_warningOverview, myBindingNavigator_WarningOverview, bindingNavigator_WarningOverview);
            CommonFunction.DrawDataGridViewHeader(dataGridView_warningOverview, 72);
        }

        private void DrawDataGridViews()
        {
            //21新购入库
            CommonFunction.DrawDataGridView(Program.mw.dataGridView_NewToolsIn);

            //22领用归还
            CommonFunction.DrawDataGridView_Two(Program.mw.dataGridView_toolsReturn, Program.mw.dataGridView_toolsReturn_records);
            
            //dataGridView_toolsReturn表的绘制
            ToolsIn.dtToolsReturn.Columns.Add("序号", typeof(int));         //2
            ToolsIn.dtToolsReturn.Columns.Add("工装编码", typeof(String));  //3
            ToolsIn.dtToolsReturn.Columns.Add("物料号", typeof(String));    //4
            ToolsIn.dtToolsReturn.Columns.Add("存储状态", typeof(String));  //5
            ToolsIn.dtToolsReturn.Columns.Add("所在线体", typeof(String));  //6
            ToolsIn.dtToolsReturn.Columns.Add("领用人", typeof(String));    //7
            ToolsIn.dtToolsReturn.Columns.Add("备注", typeof(String));      //8
            ToolsIn.dtToolsReturn.Columns.Add("工装状态", typeof(String));  //

            dataGridView_toolsReturn.DataSource = ToolsIn.dtToolsReturn;
            dataGridView_toolsReturn.Columns["工装状态"].Visible = false;   //

            DataTable dtTemp = new DataTable();
            DataColumn col = new DataColumn("func", typeof(String)) { Unique = true };
            dtTemp.Columns.Add(col);
            dtTemp.Rows.Add("正常");
            dtTemp.Rows.Add("待修");
            dtTemp.Rows.Add("报废");

            DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn
            {
                Name = "functionState",
                DataSource = dtTemp,
                DisplayMember = "func",  //DataGridViewComboBoxColumn数据源中的列
                ValueMember = "func",    //DataGridViewComboBoxColumn数据源中的列
                DataPropertyName = "工装状态",//注意，DataGridView数据源中的列
                HeaderText = "工装状态"
                //DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;//这里设置为DropDownButton是为了看起来更像ComboBox
            };
            dataGridView_toolsReturn.Columns.Add(comboBoxColumn);       //9

            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
            {
                Name = "deleteButton",
                HeaderText = " 操作 ",
                Text = "删除",
                UseColumnTextForButtonValue = true,
            };
            dataGridView_toolsReturn.Columns.Add(buttonColumn);         //不知道这列为什么序号为0
            
            //23维修入库
            CommonFunction.DrawDataGridView(Program.mw.dataGridView_RepairToolsIn);


            //31按工装出借
            CommonFunction.DrawDataGridView_Two(Program.mw.dataGridView_outByTools, Program.mw.dataGridView_outByTools_records);
            
            //dataGridView_outByTools表的绘制
            ToolsOut.dtOutByTools.Columns.Add("序号", typeof(int));         //0
            ToolsOut.dtOutByTools.Columns.Add("工装编码", typeof(String));  //1
            ToolsOut.dtOutByTools.Columns.Add("物料号", typeof(String));    //2
            ToolsOut.dtOutByTools.Columns.Add("工装描述", typeof(String));  //3
            ToolsOut.dtOutByTools.Columns.Add("存储状态", typeof(String));  //4
            ToolsOut.dtOutByTools.Columns.Add("功能状态", typeof(String));  //5
            ToolsOut.dtOutByTools.Columns.Add("可用寿命", typeof(String));  //6
            ToolsOut.dtOutByTools.Columns.Add("备注", typeof(String));      //7

            dataGridView_outByTools.DataSource = ToolsOut.dtOutByTools;

            DataGridViewButtonColumn buttonColumn2 = new DataGridViewButtonColumn
            {
                Name = "deleteButton",
                HeaderText = " 操作 ",
                Text = "删除",
                UseColumnTextForButtonValue = true,
            };
            dataGridView_outByTools.Columns.Add(buttonColumn2);

            //32按机型出库

            //33报废出库
            CommonFunction.DrawDataGridView(Program.mw.dataGridView_ScrapTools);

            
            //41工装上架
            shelfManage.DrawListView_putOnShelf(Program.mw.listView_putOnShelf);

            //42工装移位
            shelfManage.DrawListView_changeShelf(Program.mw.listView_changeShelf);

            //43查看库位
            shelfManage.DrawListView_lookUpShelf(Program.mw.listView_lookUpShelf);

            //51工装数据
            CommonFunction.DrawDataGridView(Program.mw.dataGridView_toolsData);
            
            
            //52记录数据
            CommonFunction.DrawDataGridView(Program.mw.dataGridView_recordsData);
            
            
            //53人员数据
            CommonFunction.DrawDataGridView(Program.mw.dataGridView_personsData);

            
            //61数据分析
            dataAnalysis.DataAnalysis_DrawChart(Program.mw.chart_dataAnalysis_trend);
            Program.mw.chart_dataAnalysis_trend.MouseMove += new MouseEventHandler(dataAnalysis.DataAnalysis_chart_MouseMove);

            //71预警设置
            CommonFunction.DrawDataGridView(Program.mw.dataGridView_warningSetUp);

            
            //72预警概览
            CommonFunction.DrawDataGridView(Program.mw.dataGridView_warningOverview);

            
        }


        /*****************************************  主界面顶部 菜单栏  *******************************************/

        private void 终端管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStrip.终端管理ToolStripMenuItem_Click();
        }

        private void 工装入库ToolStripMenuItem_Click(object sender, EventArgs e)                         //点击工装入库时自动切换至新购入库界面
        {
            menuStrip.工装入库ToolStripMenuItem_Click();
        }

        private void 工装出库ToolStripMenuItem_Click(object sender, EventArgs e)                          //点击工装出库时自动切换至工装方式界面
        {
            menuStrip.工装出库ToolStripMenuItem_Click();
        }

        private void 库位管理ToolStripMenuItem_Click(object sender, EventArgs e)                         //点击库位管理时自动切换至工装上架界面
        {
            menuStrip.库位管理ToolStripMenuItem_Click();
        }

        private void 数据管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStrip.数据管理ToolStripMenuItem_Click();
        }

        private void 数据分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStrip.数据分析ToolStripMenuItem_Click();
        }

        private void 预警管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStrip.预警管理ToolStripMenuItem_Click();
        }

        /*****************************************  主界面左侧  工具栏  *******************************************/

        private void toolStripButton1_Click(object sender, EventArgs e)//按下工具栏处第一个按钮（更改终端、新购入库、工装方式、工装上架、工装数据）
        {
            toolStrip.toolStripButton1_Click();
        }
        private void toolStripButton2_Click(object sender, EventArgs e)//toolStrip第二个按钮
        {
            toolStrip.toolStripButton2_Click();
        }
        private void toolStripButton3_Click(object sender, EventArgs e)//toolStrip第三个按钮
        {
            toolStrip.toolStripButton3_Click();
        }

        /************************************  BindingNavigator  *************************************/

        public void BindingNavigator_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            BindingNavigator bindingNavigator = sender as BindingNavigator;
            if (bindingNavigator.Name.Contains("NewToolsIn"))
            {
                myBindingNavigator_NewToolsIn.BindingNavigator_ItemClicked(sender, e);
            }
            if (bindingNavigator.Name.Contains("RepairToolsIn"))
            {
                myBindingNavigator_RepairToolsIn.BindingNavigator_ItemClicked(sender, e);
            }
            if (bindingNavigator.Name.Contains("ScrapTools"))
            {
                myBindingNavigator_ScrapTools.BindingNavigator_ItemClicked(sender, e);
            }
            if (bindingNavigator.Name.Contains("ToolsData"))
            {
                myBindingNavigator_ToolsData.BindingNavigator_ItemClicked(sender, e);
            }
            if (bindingNavigator.Name.Contains("RecordsData"))
            {
                myBindingNavigator_RecordsData.BindingNavigator_ItemClicked(sender, e);
            }
            if (bindingNavigator.Name.Contains("PersonsData"))
            {
                myBindingNavigator_PersonsData.BindingNavigator_ItemClicked(sender, e);
            }
            if (bindingNavigator.Name.Contains("WarningSetUp"))
            {
                myBindingNavigator_WarningSetUp.BindingNavigator_ItemClicked(sender, e);
            }
            if (bindingNavigator.Name.Contains("WarningOverview"))
            {
                myBindingNavigator_WarningOverview.BindingNavigator_ItemClicked(sender, e);
            }
        }
        public void BindingNavigator_ComboBox_TextChanged(object sender, EventArgs e)
        {
            ToolStripComboBox bindingNavigator = sender as ToolStripComboBox;
            if (bindingNavigator.Name.Contains("NewToolsIn"))
            {
                myBindingNavigator_NewToolsIn.BindingNavigator_ComboBox_TextChanged(sender, e);
            }
            if (bindingNavigator.Name.Contains("RepairToolsIn"))
            {
                myBindingNavigator_RepairToolsIn.BindingNavigator_ComboBox_TextChanged(sender, e);
            }
            if (bindingNavigator.Name.Contains("ScrapTools"))
            {
                myBindingNavigator_ScrapTools.BindingNavigator_ComboBox_TextChanged(sender, e);
            }
            if (bindingNavigator.Name.Contains("ToolsData"))
            {
                myBindingNavigator_ToolsData.BindingNavigator_ComboBox_TextChanged(sender, e);
            }
            if (bindingNavigator.Name.Contains("RecordsData"))
            {
                myBindingNavigator_RecordsData.BindingNavigator_ComboBox_TextChanged(sender, e);
            }
            if (bindingNavigator.Name.Contains("PersonsData"))
            {
                myBindingNavigator_PersonsData.BindingNavigator_ComboBox_TextChanged(sender, e);
            }
            if (bindingNavigator.Name.Contains("WarningSetUp"))
            {
                myBindingNavigator_WarningSetUp.BindingNavigator_ComboBox_TextChanged(sender, e);
            }
            if (bindingNavigator.Name.Contains("WarningOverview"))
            {
                myBindingNavigator_WarningOverview.BindingNavigator_ComboBox_TextChanged(sender, e);
            }
        }
        private void BindingNavigator_JumpPage(object sender, KeyPressEventArgs e)
        {
            ToolStripTextBox bindingNavigator = sender as ToolStripTextBox;
            if (bindingNavigator.Name.Contains("NewToolsIn"))
            {
                myBindingNavigator_NewToolsIn.BindingNavigator_JumpToPage(sender, e);
            }
            if (bindingNavigator.Name.Contains("RepairToolsIn"))
            {
                myBindingNavigator_RepairToolsIn.BindingNavigator_JumpToPage(sender, e);
            }
            if (bindingNavigator.Name.Contains("ScrapTools"))
            {
                myBindingNavigator_ScrapTools.BindingNavigator_JumpToPage(sender, e);
            }
            if (bindingNavigator.Name.Contains("ToolsData"))
            {
                myBindingNavigator_ToolsData.BindingNavigator_JumpToPage(sender, e);
            }
            if (bindingNavigator.Name.Contains("RecordsData"))
            {
                myBindingNavigator_RecordsData.BindingNavigator_JumpToPage(sender, e);
            }
            if (bindingNavigator.Name.Contains("PersonsData"))
            {
                myBindingNavigator_PersonsData.BindingNavigator_JumpToPage(sender, e);
            }
            if (bindingNavigator.Name.Contains("WarningSetUp"))
            {
                myBindingNavigator_WarningSetUp.BindingNavigator_JumpToPage(sender, e);
            }
            if (bindingNavigator.Name.Contains("WarningOverview"))
            {
                myBindingNavigator_WarningOverview.BindingNavigator_JumpToPage(sender, e);
            }
        }


        /***************************************************** 21 新购工装入库界面 ****************************************************/

        // 新购工装入库界面 按钮函数 //
        private void Button_newToosIn_enter_Click(object sender, EventArgs e)               //新购工装入库 确定按钮
        {
            if (toolsIn.NewToolsIn_enter() == true)
            {
                textBox_newToolsIn_code.Focus();
            }
        }
        private void Button_newToosIn_cancel_Click(object sender, EventArgs e)              //新购工装入库 清空按钮
        {
            toolsIn.NewToolsIn_CleanAll();
            textBox_newToolsIn_code.Focus();
        }

        // 新购工装入库界面 按键函数 //
        private void TextBox_newToolsIn_code_KeyPress(object sender, KeyPressEventArgs e)   //编码框回车处理
        {
            toolsIn.TextBox_newToolsIn_code_KeyPress(e.KeyChar);
        }
        private void TextBox_newToolsIn_endCode_KeyPress(object sender, KeyPressEventArgs e)   //结尾编码框回车处理
        {
            toolsIn.TextBox_newToolsIn_endCode_KeyPress(e.KeyChar);
        }
        private void TextBox_newToolsIn_lifespan_KeyPress(object sender, KeyPressEventArgs e)    //限制额定寿命文本框只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        private void TextBox_newToolsIn_safetyStock_KeyPress(object sender, KeyPressEventArgs e) //限制安全库存文本框只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        private void TextBox_newToolsIn_price_KeyPress(object sender, KeyPressEventArgs e)       //价格文本框 按键函数
        {
            toolsIn.TextBox_newToolsIn_price_KeyPress(e);
        }
        private void TextBox_newToolsIn_operator_KeyPress(object sender, KeyPressEventArgs e)    //限制操作人编辑只能输入数字
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                toolsIn.TextBox_newToolsIn_operator_KeyPress(e.KeyChar);
            }
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        private void TextBox_newToolsIn_contact_KeyPress(object sender, KeyPressEventArgs e)     //限制联系方式只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        private void TextBox_newToolsIn_shelf_KeyPress(object sender, KeyPressEventArgs e)     //架号文本框回车函数
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                toolsIn.TextBox_newToolsIn_shelf_KeyPress(e.KeyChar);
            }
        }

        // 新购工装入库界面 焦点函数 //
        private void CheckBox_batch_CheckedChanged(object sender, EventArgs e)              //批量入库复选框状态
        {
            toolsIn.CheckBox_batch_CheckedChanged();
        }
        private void TextBox_newToolsIn_code_Leave(object sender, EventArgs e)                 //编码框失去焦点
        {
            toolsIn.TextBox_newToolsIn_code_Leave();
        }
        private void TextBox_newToolsIn_endCode_Leave(object sender, EventArgs e)           //结尾编码框失去焦点
        {
            toolsIn.TextBox_newToolsIn_endCode_Leave();
        }
        private void ComboBox_newToolsIn_lifetype_SelectedIndexChanged(object sender, EventArgs e)  //额定寿命类型选取
        {
            toolsIn.ComboBox_newToolsIn_lifetype_SelectedIndexChanged();
        }
        private void TextBox_newToolsIn_lifespan_Enter(object sender, EventArgs e)      //额定寿命获得焦点
        {
            toolsIn.TextBox_newToolsIn_lifespan_Enter();
        }
        private void TextBox_newToosIn_lifespan_Leave(object sender, EventArgs e)      //额定寿命失去焦点
        {
            toolsIn.TextBox_newToosIn_lifespan_Leave();
        }
        private void TextBox_newToosIn_price_Enter(object sender, EventArgs e)          //单价获得焦点
        {
            toolsIn.TextBox_newToosIn_price_Enter();
        }
        private void TextBox_newToosIn_price_Leave(object sender, EventArgs e)          //单价失去焦点
        {
            toolsIn.TextBox_newToosIn_price_Leave();
        }
        private void TextBox_newToosIn_operator_Enter(object sender, EventArgs e)       //操作人获得焦点
        {
            toolsIn.TextBox_newToosIn_operator_Enter();
        }
        private void TextBox_newToosIn_operator_Leave(object sender, EventArgs e)       //操作人失去焦点
        {
            toolsIn.TextBox_newToosIn_operator_Leave();
        }
        private void TextBox_newToosIn_shelf_Leave(object sender, EventArgs e)          //架号失去焦点
        {
            toolsIn.TextBox_newToosIn_shelf_Leave();
        }


        /***************************************************** 22 领用归还界面  ****************************************************/

        // 领用归还界面 按钮函数 //
        private void button_toolsReturn_enter_Click(object sender, EventArgs e)                //领用归还入库 确定按钮
        {
            if (toolsIn.ToolsReturn_Enter() == true)
            {
                textBox_toolsReturn_code.Focus();
            }
        }
        private void Button_toolsReturn_exChange_Click(object sender, EventArgs e)            //领用归还入库 以旧换新按钮
        {
            ExChange exChange = new ExChange();
            if (exChange.InitData(ToolsIn.dtToolsReturn))
            {
                exChange.ShowDialog();
            }
        }
        private void button_toolsReturn_cancel_Click(object sender, EventArgs e)               //领用归还入库 清空按钮
        {
            toolsIn.ToolsReturn_CleanALL();
            textBox_toolsReturn_code.Focus();
        }

        private void DataGridView_toolsReturn_CellContentClick(object sender, DataGridViewCellEventArgs e)  //DataGridView删除行按钮
        {
            toolsIn.DataGridView_toolsReturn_CellContentClick(sender, e, dataGridView_toolsReturn);
        }


        // 领用归还界面 按键函数 //
        private void textBox_toolsReturn_code_KeyPress(object sender, KeyPressEventArgs e)      //编码文本框 回车函数
        {
            toolsIn.TextBox_toolsReturn_code_KeyPress(e.KeyChar);
        }

        private void textBox_toolsReturn_returner_KeyPress(object sender, KeyPressEventArgs e)  //限制归还人编辑框只能输入数字
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                toolsIn.TextBox_toolsReturn_returner_KeyPress(e.KeyChar);
            }
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        private void textBox_toolsReturn_returnerContact_KeyPress(object sender, KeyPressEventArgs e)      //限制归还人联系方式编辑框只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        private void textBox_toolsReturn_operator_KeyPress(object sender, KeyPressEventArgs e)     //限制操作人编辑框只能输入数字
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                toolsIn.TextBox_toolsReturn_operator_KeyPress(e.KeyChar);
            }
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        private void textBox_toolsReturn_operatorContact_KeyPress(object sender, KeyPressEventArgs e)  //限制操作人联系方式编辑框只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }

        // 领用归还界面 焦点函数 //
        private void textBox_toolsReturn_returner_Enter(object sender, EventArgs e)       //归还人获得焦点
        {
            toolsIn.TextBox_toolsReturn_returner_Enter();
        }
        private void textBox_toolsReturn_returner_Leave(object sender, EventArgs e)       //归还人失去焦点
        {
            toolsIn.TextBox_toolsReturn_returner_Leave();
        }
        private void textBox_toolsReturn_operator_Enter(object sender, EventArgs e)     //操作人获得焦点
        {
            toolsIn.TextBox_toolsReturn_operator_Enter();
        }
        private void textBox_toolsReturn_operator_Leave(object sender, EventArgs e)     //操作人失去焦点
        {
            toolsIn.TextBox_toolsReturn_operator_Leave();
        }


        /**************************************************  23 维修入库界面 ****************************************************/


        //维修入库界面 按钮函数
        private void Button_repairtoolsIn_Enter_Click(object sender, EventArgs e)                //维修入库 确定按钮
        {
            if (toolsIn.RepairtoolsIn_Enter() == true)
            {
                textBox_repairtoolsIn_code.Focus();
            }
        }

        private void Button_repairtoolsIn_Clear_Click(object sender, EventArgs e)                //维修入库 清空按钮
        {
            toolsIn.RepairToolsIn_CleanALL();
            textBox_repairtoolsIn_code.Focus();
        }

        //维修入库界面 按键函数
        private void TextBox_repairtoolsIn_code_KeyPress(object sender, KeyPressEventArgs e)      //工装编码文本框 回车函数
        {
            toolsIn.TextBox_repairtoolsIn_code_KeyPress(e.KeyChar);
        }

        private void TextBox_repairtoolsIn_operator_KeyPress(object sender, KeyPressEventArgs e)   //限制操作人编辑框只能输入数字
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                toolsIn.TextBox_repairtoolsIn_operator_KeyPress(e.KeyChar);
            }
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        private void TextBox_repairtoolsIn_contact_KeyPress(object sender, KeyPressEventArgs e)    //限制操作人联系方式编辑框只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }

        //维修入库界面 焦点函数
        private void TextBox_repairtoolsIn_code_Leave(object sender, EventArgs e)           //编码文本框失去焦点
        {
            toolsIn.TextBox_repairtoolsIn_code_Leave();
        }
        private void TextBox_repairtoolsIn_operator_Enter(object sender, EventArgs e)       //操作人获得焦点
        {
            CommonFunction.TextBox_operator_Enter(Program.mw.textBox_repairtoolsIn_operator);
        }
        private void TextBox_repairtoolsIn_operator_Leave(object sender, EventArgs e)       //操作人失去焦点
        {
            CommonFunction.TextBox_operator_Leave(Program.mw.textBox_repairtoolsIn_operator, Program.mw.textBox_repairtoolsIn_operatorName, Program.mw.textBox_repairtoolsIn_operatorContact);
        }
        private void TextBox_repairtoolsIn_lifespan_Enter(object sender, EventArgs e)       //额定寿命获得焦点
        {
            toolsIn.TextBox_repairtoolsIn_lifespan_Enter();
        }
        private void TextBox_repairtoolsIn_lifespan_Leave(object sender, EventArgs e)       //额定寿命失去焦点
        {
            toolsIn.TextBox_repairtoolsIn_lifespan_Leave();
        }
        private void ComboBox_repairtoolsIn_lifetype_TextChanged(object sender, EventArgs e)//寿命类型改变
        {
            if (comboBox_repairtoolsIn_lifeType.Text == "时间")
            {
                textBox_repairtoolsIn_lifeSpan.Text = "天 ";
            }
            else
            {
                textBox_repairtoolsIn_lifeSpan.Text = "次 ";
            }
        }



        /*************************************************** 31 按工装方式出库界面 ****************************************************/

        // 按工装出借界面 按钮函数 //
        private void Button_outByTools_Enter_Click(object sender, EventArgs e)                //按工装方式出库 确定按钮
        {
            if (toolsOut.OutByTools_Enter() == true)
            {
                textBox_outByTools_code.Focus();
            }
        }

        private void Button_outByTools_Clear_Click(object sender, EventArgs e)                //按工装方式出库 清空按钮
        {
            toolsOut.OutByTools_CleanAll();
            textBox_outByTools_code.Focus();
        }

        private void DataGridView_outByTools_CellContentClick(object sender, DataGridViewCellEventArgs e)  //DataGridView删除行按钮
        {
            toolsOut.DataGridView_outByTools_CellContentClick(sender, e, dataGridView_outByTools);
        }

        // 按工装出借界面 按键函数 //
        private void TextBox_outByTools_code_KeyPress(object sender, KeyPressEventArgs e)       //工装编码文本框 回车函数
        {
            toolsOut.TextBox_outByTools_code_KeyPress(e.KeyChar);
            //if (e.KeyChar == (char)Keys.Enter)
            //    Program.mw.textBox_outByTools_borrower.Focus();
        }

        private void TextBox_outByTools_borrower_KeyPress(object sender, KeyPressEventArgs e)   //限制领用人编辑框只能输入数字
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                toolsOut.TextBox_outByTools_borrower_KeyPress(e.KeyChar);
            }
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        private void TextBox_outByTools_borrowerContact_KeyPress(object sender, KeyPressEventArgs e)    //限制领用人联系方式编辑框只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        private void TextBox_outByTools_operator_KeyPress(object sender, KeyPressEventArgs e)   //限制操作人编辑框只能输入数字
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                toolsOut.TextBox_outByTools_operator_KeyPress(e.KeyChar);
            }
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        private void TextBox_outByTools_operatorContact_KeyPress(object sender, KeyPressEventArgs e)    //限制操作人联系方式编辑框只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }

        //按工装出借界面 焦点函数
        private void TextBox_outByTools_code_Leave(object sender, EventArgs e)           //编码文本框失去焦点
        {
            //toolsOut.TextBox_outByTools_code_Leave();
        }
        private void TextBox_outByTools_borrower_Enter(object sender, EventArgs e)       //领用人获得焦点
        {
            toolsOut.TextBox_outByTools_borrower_Enter();
        }
        private void TextBox_outByTools_borrower_Leave(object sender, EventArgs e)       //领用人失去焦点
        {
            toolsOut.TextBox_outByTools_borrower_Leave();
        }
        private void TextBox_outByTools_operator_Enter(object sender, EventArgs e)       //操作人获得焦点
        {
            toolsOut.TextBox_outByTools_operator_Enter();
        }
        private void TextBox_outByTools_operator_Leave(object sender, EventArgs e)       //操作人失去焦点
        {
            toolsOut.TextBox_outByTools_operator_Leave();
        }

        /**************************************************** 32 按机型方式出库界面 ****************************************************/



        /**************************************************** 33 维修报废出库界面  ****************************************************/

        // 维修报废出库界面 按钮函数 //
        private void Button_scrapTools_repair_Click(object sender, EventArgs e)               //维修报废出库 维修按钮
        {
            if (toolsOut.ScrapTools_Repair() == true)
            {
                textBox_scrapTools_code.Focus();
            }
        }

        private void Button_scrapTools_scrap_Click(object sender, EventArgs e)                //维修报废出库 报废按钮
        {
            if (toolsOut.ScrapTools_Scrap() == true)
            {
                toolsOut.ScrapTools_CleanAll();
                textBox_scrapTools_code.Focus();
            }
        }

        private void Button_scrapTools_clear_Click(object sender, EventArgs e)                //维修报废出库 清空按钮
        {
            toolsOut.ScrapTools_CleanAll();
            textBox_scrapTools_code.Focus();
        }

        // 维修报废出库界面 按键函数 //
        private void TextBox_scrapTools_code_KeyPress(object sender, KeyPressEventArgs e)       //工装编码文本框 回车函数
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Program.mw.textBox_scrapTools_receiver.Focus();
            }
        }

        private void TextBox_scrapTools_receiver_KeyPress(object sender, KeyPressEventArgs e)   //限制接收人编辑框只能输入数字
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                toolsOut.TextBox_scrapTools_receiver_KeyPress(e.KeyChar);
            }
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        private void TextBox_scrapTools_receiverContact_KeyPress(object sender, KeyPressEventArgs e)    //限制接收人联系方式编辑框只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        private void TextBox_scrapTools_operator_KeyPress(object sender, KeyPressEventArgs e)   //限制操作人编辑框只能输入数字
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                toolsOut.TextBox_scrapTools_operator_KeyPress(e.KeyChar);
            }
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        private void TextBox_scrapTools_operatorContact_KeyPress(object sender, KeyPressEventArgs e)    //限制操作人联系方式编辑框只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }

        // 维修报废出库界面 焦点函数 //
        private void TextBox_scrapTools_code_Leave(object sender, EventArgs e)
        {
            toolsOut.TextBox_scrapTools_code_Leave();
        }
        private void TextBox_scrapTools_operator_Enter(object sender, EventArgs e)
        {
            toolsOut.TextBox_scrapTools_operator_Enter();
        }
        private void TextBox_scrapTools_operator_Leave(object sender, EventArgs e)
        {
            toolsOut.TextBox_scrapTools_operator_Leave();
        }
        private void TextBox_scrapTools_receiver_Enter(object sender, EventArgs e)
        {
            toolsOut.TextBox_scrapTools_receiver_Enter();
        }
        private void TextBox_scrapTools_receiver_Leave(object sender, EventArgs e)
        {
            toolsOut.TextBox_scrapTools_receiver_Leave();
        }



        /*************************************************** 41 库位管理 工装上架界面 ***************************************************/

        // 工装上架界面 按钮函数 //
        private void Button_putOnShelf_put_Click(object sender, EventArgs e)               //工装上架 上架按钮
        {
            if (shelfManage.PutOnShelf_put() == true)
            {
                textBox_putOnShelf_code.Focus();
            }
        }

        private void Button_putOnShelf_clear_Click(object sender, EventArgs e)               //工装上架 清空按钮
        {
            shelfManage.PutOnShelf_CleanAll();
            textBox_putOnShelf_code.Focus();
        }

        // 工装上架界面 按键函数 //
        private void TextBox_putOnShelf_code_KeyPress(object sender, KeyPressEventArgs e)       //工装编码文本框 回车函数
        {
            if (e.KeyChar == (char)Keys.Enter)
                Program.mw.textBox_putOnShelf_operator.Focus();
        }

        private void TextBox_putOnShelf_operator_KeyPress(object sender, KeyPressEventArgs e)   //限制操作人编辑框只能输入数字
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                shelfManage.TextBox_putOnShelf_operator_KeyPress(e.KeyChar);
            }
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        private void TextBox_putOnShelf_operatorContact_KeyPress(object sender, KeyPressEventArgs e)    //限制操作人联系方式编辑框只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }

        // 工装上架界面 焦点函数 //
        private void TextBox_putOnShelf_code_Leave(object sender, EventArgs e)
        {
            shelfManage.TextBox_putOnShelf_code_Leave();
        }
        private void TextBox_putOnShelf_operator_Enter(object sender, EventArgs e)
        {
            shelfManage.TextBox_putOnShelf_operator_Enter();
        }
        private void TextBox_putOnShelf_operator_Leave(object sender, EventArgs e)
        {
            shelfManage.TextBox_putOnShelf_operator_Leave();
        }


        /*************************************************** 42 库位管理 工装移位界面 ***************************************************/

        // 工装移位界面 按钮函数 //
        private void Button_shelfManage_changeShelf_Click(object sender, EventArgs e)               //工装移位 移位按钮
        {
            if (shelfManage.ChangeShelf_change() == true)
            {
                textBox_changeShelf_code.Focus();
            }
        }

        private void Button_changeShelf_cancel_Click(object sender, EventArgs e)               //工装移位 清空按钮
        {
            shelfManage.ChangeShelf_CleanAll();
            textBox_changeShelf_code.Focus();
        }

        // 工装移位界面 按键函数 //
        private void TextBox_changeShelf_code_KeyPress(object sender, KeyPressEventArgs e)       //工装编码文本框 回车函数
        {
            if (e.KeyChar == (char)Keys.Enter)
                Program.mw.textBox_changeShelf_operator.Focus();
        }

        private void TextBox_changeShelf_materialNumber_KeyPress(object sender, KeyPressEventArgs e)//物料号文本框 回车函数
        {
            if (e.KeyChar == (char)Keys.Enter)
                Program.mw.textBox_changeShelf_operator.Focus();
        }

        private void TextBox_changeShelf_operator_KeyPress(object sender, KeyPressEventArgs e)   //操作人文本框 按键函数
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                CommonFunction.TextBox_operator_KeyPress(Program.mw.textBox_changeShelf_operator, Program.mw.textBox_changeShelf_newShelf, Program.mw.textBox_changeShelf_operatorName, e.KeyChar);
            }
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        private void TextBox_changeShelf_operatorContact_KeyPress(object sender, KeyPressEventArgs e)    //限制操作人联系方式编辑框只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }

        // 工装移位界面 焦点函数 //
        private void TextBox_changeShelf_code_Leave(object sender, EventArgs e)
        {
            shelfManage.TextBox_changeShelf_code_Leave();
        }
        private void TextBox_changeShelf_materialNumber_Leave(object sender, EventArgs e)
        {
            shelfManage.TextBox_changeShelf_materialNumber_Leave();
        }
        private void TextBox_changeShelf_operator_Enter(object sender, EventArgs e)
        {
            CommonFunction.TextBox_operator_Enter(Program.mw.textBox_changeShelf_operator);
        }
        private void TextBox_changeShelf_operator_Leave(object sender, EventArgs e)
        {
            CommonFunction.TextBox_operator_Leave(Program.mw.textBox_changeShelf_operator, Program.mw.textBox_changeShelf_operatorName, Program.mw.textBox_changeShelf_operatorContact);
        }


        /*************************************************** 43 库位管理 查看库位界面 ***************************************************/

        // 查看库位界面 按键函数 //

        // 查看库位界面 焦点函数 //

        // 查看库位界面 按钮函数 //
        private void Button_lookUpShelf_search_Click(object sender, EventArgs e)
        {
            shelfManage.LookUpShelf_search();
            textBox_lookUpShelf_code.Focus();
        }

        private void Button_lookUpShelf_cancel_Click(object sender, EventArgs e)
        {
            shelfManage.LookUpShelf_CleanAll();
            textBox_lookUpShelf_code.Focus();
        }


        /*************************************************** 51 数据管理 工装数据界面 ***************************************************/

        private void TextBox_toolsData_code_KeyPress(object sender, KeyPressEventArgs e) //工装编码文本框 回车函数
        {
            dataManage.textBox_toolsData_code_KeyPress(e.KeyChar);
        }

        private void Button_toolsData_lookUp_Click(object sender, EventArgs e)       //工装数据界面 查询按钮
        {
            dataManage.ToolsData_LookUp();
        }

        private void Button_toolsData_clear_Click(object sender, EventArgs e)     //工装数据界面 清空按钮
        {
            dataManage.ToolsData_CleanAll();
        }

        private void Button_toolsData_export_Click(object sender, EventArgs e)      //工装数据界面 导出按钮
        {
            dataManage.ToolsData_Export();
        }

        /*************************************************** 52 数据管理 记录数据界面 ***************************************************/

        private void TextBox_recordsData_code_KeyPress(object sender, KeyPressEventArgs e) //工装编码文本框 回车函数
        {
            dataManage.TextBox_recordsData_code_KeyPress(e.KeyChar);
        }

        private void Button_recordsData_lookUp_Click(object sender, EventArgs e)    //记录数据界面 查询按钮
        {
            dataManage.RecordsData_LookUp();
        }

        private void Button_recordsData_clear_Click(object sender, EventArgs e)   //记录数据界面 清空按钮
        {
            dataManage.RecordsData_CleanAll();
        }

        private void Button_recordsData_export_Click(object sender, EventArgs e)      //记录数据界面 导出按钮
        {
            dataManage.RecordsData_Export();
        }

        /*************************************************** 53 数据管理 人员数据界面 ***************************************************/

        private void TextBox_personsData_employeeID_KeyPress(object sender, KeyPressEventArgs e) //员工编号文本框 回车函数
        {
            dataManage.textBox_personsData_employeeID_KeyPress(e.KeyChar);
        }

        private void Button_personsData_lookUp_Click(object sender, EventArgs e)    //人员数据界面 查询按钮
        {
            dataManage.PersonsData_LookUp();
        }

        private void Button_personData_clear_Click(object sender, EventArgs e)   //人员数据界面 清空按钮
        {
            dataManage.PersonData_CleanAll();
        }

        private void Button_personsData_export_Click(object sender, EventArgs e)      //人员数据界面 导出按钮
        {
            dataManage.PersonsData_Export();
        }




        /*************************************************** 61 数据分析界面 ****************************************************/

        private void Button_dataAnalysis_analyze_Click(object sender, EventArgs e)          //查询按钮
        {
            dataAnalysis.Button_DataAnalysis_Analyze();
        }

        private void CheckBox_dataAnalysis_add_CheckedChanged(object sender, EventArgs e)   //添加/查询 复选框
        {
            dataAnalysis.CheckBox_DataAnalysis_Add_CheckedChanged();
        }

        private void TextBox_dataAnalysis_materialNumber_KeyPress(object sender, KeyPressEventArgs e) //物料号文本框 回车函数
        {
            dataAnalysis.TextBox_dataAnalysis_materialNumber_KeyPress(e.KeyChar);
        }

        private void Button_dataAnalysis_export_Click(object sender, EventArgs e)           //导出按钮
        {
            dataAnalysis.Button_DataAnalysis_Export();
        }

        private void Button_dataAnalysis_clear_Click(object sender, EventArgs e)            //清空按钮
        {
            dataAnalysis.DataAnalysis_CleanAll();
        }

        private void ComboBox_dataAnalysis_inquiryMode_SelectedIndexChanged(object sender, EventArgs e)     //查询方式改变
        {
            if (comboBox_dataAnalysis_inquiryMode.Text == "物料号")
                Program.mw.label_dataAnalysis_materialNumber.Text = "物料号:";
            if (comboBox_dataAnalysis_inquiryMode.Text == "工装类别")
                Program.mw.label_dataAnalysis_materialNumber.Text = "工装类别:";
        }



        /*************************************************** 71 预警设置界面 ****************************************************/

        private void Button_warningSetUp_Setup_Click(object sender, EventArgs e)
        {
            warningManage.WarningManage_SetUp();
        }

        private void ComboBox_warningSetUp_name_SelectedIndexChanged(object sender, EventArgs e)   //预警项选择框改变
        {
            if (comboBox_warningSetUp_name.Text == "物料号")
            {
                Program.mw.label_warningSetUp_materialNumber.Text = "物料号:";
            }
            if (comboBox_warningSetUp_name.Text == "工装类别")
            {
                Program.mw.label_warningSetUp_materialNumber.Text = "工装类别:";
            }
        }

        private void ComboBox_warningSetUp_type_SelectedIndexChanged(object sender, EventArgs e)   //预警类别选择框改变
        {
            if (comboBox_warningSetUp_type.Text == "安全库存")
            {
                Program.mw.comboBox_warningSetUp_method.Text = "数量";
                Program.mw.comboBox_warningSetUp_method.Enabled = false;
            }
            if (comboBox_warningSetUp_type.Text == "待修数量")
            {
                Program.mw.comboBox_warningSetUp_method.Enabled = true;
            }
        }

        private void ComboBox_warningSetUp_methods_SelectedIndexChanged(object sender, EventArgs e) //预警方式选择框改变
        {
            if (comboBox_warningSetUp_method.Text.Contains("数量"))
            {
                Program.mw.label_warningSetUp_value.Text = "数   量:";
                Program.mw.textBox_warningSetUp_value.Text = "件 ";
                CommonFunction.TextboxLeave(Program.mw.textBox_warningSetUp_value);
            }
            if (comboBox_warningSetUp_method.Text.Contains("比例"))
            {
                Program.mw.label_warningSetUp_value.Text = "百分比:";
                Program.mw.textBox_warningSetUp_value.Text = "% ";
                CommonFunction.TextboxLeave(Program.mw.textBox_warningSetUp_value);
            }
        }

        private void TextBox_warningSetUp_value_Enter(object sender, EventArgs e)       //数值文本框 获得焦点
        {
            if (Program.mw.textBox_warningSetUp_value.Text == "件 " || Program.mw.textBox_warningSetUp_value.Text == "% ")
            {
                Program.mw.textBox_warningSetUp_value.Text = "";
            }
            CommonFunction.TextboxEnter(Program.mw.textBox_warningSetUp_value);
        }

        private void TextBox_warningSetUp_value_Leave(object sender, EventArgs e)       //数值文本框 失去焦点
        {
            if (String.IsNullOrEmpty(Program.mw.textBox_warningSetUp_value.Text) && Program.mw.comboBox_warningSetUp_method.Text.Contains("数量"))
            {
                Program.mw.textBox_warningSetUp_value.Text = "件 ";
                CommonFunction.TextboxLeave(Program.mw.textBox_warningSetUp_value);
            }
            if (String.IsNullOrEmpty(Program.mw.textBox_warningSetUp_value.Text) && Program.mw.comboBox_warningSetUp_method.Text.Contains("比例"))
            {
                Program.mw.textBox_warningSetUp_value.Text = "% ";
                CommonFunction.TextboxLeave(Program.mw.textBox_warningSetUp_value);
            }
        }

        private void TextBox_warningSetUp_value_KeyPress(object sender, KeyPressEventArgs e)    //数值文本框 按键函数
        {
            warningManage.TextBox_warningSetUp_value_KeyPress(e);
        }






        /*************************************************** 72 预警概览界面 ****************************************************/



    }
}
