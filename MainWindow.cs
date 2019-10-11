using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using nsStockManage;
using nsDBConnection;
using nsClientManage;

namespace nsMainWindow
{
    public partial class MainWindow : Form
    {
        //全局变量
        public static String mainPath = null;                       //系统目录
        public static String filePath = null;                       //配置文件目录
        public static String TerminalNumber = null;                 //终端编号标识符
        public static String serverIP = null;                       //服务器地址
        public static bool mySqlConnectionState = false;            //数据库连接状态标识符
        public static String[] arrayTerminals = new String[] { "123", "客户端1", "客户端2", "客户端3", "客户端4", "客户端5", "客户端6", "客户端7", "客户端8", "客户端9" };
        int workWidth = Screen.PrimaryScreen.Bounds.Width;          // 屏幕工作区域宽度
        int workHeight = Screen.PrimaryScreen.Bounds.Height;        // 屏幕工作区域高度

        //构造函数
        public MainWindow()
        {
            InitializeComponent();
            this.Load += new EventHandler(init_Load);               //初始化程序
            this.panel_newtoolsIn.Visible = false;
            this.panel_toolsReturn.Visible = false;
            this.panel_repairtoolsIn.Visible = false;
            this.panel_outByTools.Visible = false;
        }

        //主窗口加载事件函数
        private void init_Load(object sender, EventArgs e)
        {
            //实例化主窗口
            Program.mw = this;
            //连接数据库
            DBConnection connect = new DBConnection();
            //显示数据库状态
            connect.Open();
            //获取程序的基目录：System.AppDomain.CurrentDomain.BaseDirectory
            mainPath = System.IO.Directory.GetCurrentDirectory();
            filePath = mainPath + "/config.txt";                    //配置文件目录
            //检测客户端名称，若已存在检测是否重复
            ClientManageFunction cmf = new ClientManageFunction();
            cmf.checkTerminal();
            //若已存在则导入，若无则弹出登陆界面
            //显示客户端名称
            this.statusStripStatusLabel1.Text = TerminalNumber;

            //检索预警项，显示报警信息
        }

        //数据库连接函数
        private void ConnectMysql()
        {
            DBConnection connect = new DBConnection();
            connect.Open();
            ///需在程序关闭函数里添加对象回收语句
        }

        /*****************************************主界面顶部 菜单栏*******************************************/

        nsStockManage.MenuStrip menuStrip = new nsStockManage.MenuStrip();
        
        private void 终端管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStrip.终端管理ToolStripMenuItem_Click();
        }

        private void 工装入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStrip.工装入库ToolStripMenuItem_Click();
        }

        private void 工装出库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStrip.工装出库ToolStripMenuItem_Click();
        }

        private void 库位管理ToolStripMenuItem_Click(object sender, EventArgs e)
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

        /*****************************************主界面左侧  工具栏*******************************************/

        nsStockManage.ToolStrip toolStrip = new nsStockManage.ToolStrip();
        
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

        /********************************************************新购工装入库界面******************************************************/

        ToolsIn toolsIn = new ToolsIn();
        
        private void checkBox_batch_CheckedChanged(object sender, EventArgs e)     //check_box状态变化
        {
            toolsIn.checkBox_batch_CheckedChanged();
        }
        private void textBox_newToolsIn_code_KeyPress(object sender, KeyPressEventArgs e)
        {
            toolsIn.textBox_newToolsIn_code_KeyPress(e.KeyChar);
        }
        private void comboBox_newToolsIn_lifetype_SelectedIndexChanged(object sender, EventArgs e)  //额定寿命类型选取
        {
            toolsIn.comboBox_newToolsIn_lifetype_SelectedIndexChanged();
        }
        private void textBox_newToolsIn_lifespan_Enter(object sender, EventArgs e)
        {
            toolsIn.textBox_newToolsIn_lifespan_Enter();
        }
        private void textBox_newToosIn_lifespan_Leave(object sender, EventArgs e)
        {
            toolsIn.textBox_newToosIn_lifespan_Leave();
        }
        private void textBox_newToosIn_price_Enter(object sender, EventArgs e)
        {
            toolsIn.textBox_newToosIn_price_Enter();
        }
        private void textBox_newToosIn_price_Leave(object sender, EventArgs e)
        {
            toolsIn.textBox_newToosIn_price_Leave();
        }
        private void textBox_newToosIn_operator_Enter(object sender, EventArgs e)
        {
            toolsIn.textBox_newToosIn_operator_Enter();
        }
        private void textBox_newToosIn_operator_Leave(object sender, EventArgs e)
        {
            toolsIn.textBox_newToosIn_operator_Leave();
        }

        //新购工装入库确定按钮
        private void button_newToosIn_enter_Click(object sender, EventArgs e)
        {
            if (toolsIn.newToolsIn_enter() == true)
            {
                toolsIn.newToolsInCleanAll();
                textBox_newToolsIn_code.Focus();
            }
        }
        //新购工装入库清空按钮
        private void button_newToosIn_cancel_Click(object sender, EventArgs e)
        {
            toolsIn.newToolsInCleanAll();
            textBox_newToolsIn_code.Focus();
        }
        
        //新购工装入库界面 限制编辑框输入内容
        private void textBox_newToolsIn_lifespan_KeyPress(object sender, KeyPressEventArgs e)    //限制额定寿命编辑框只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
             e.Handled = true;
            }
        }
        private void textBox_newToolsIn_price_KeyPress(object sender, KeyPressEventArgs e)       //限制价格编辑框只能输入数字和小数点
        {
            toolsIn.textBox_newToolsIn_price_KeyPress(e);
        }
        private void textBox_newToolsIn_operator_KeyPress(object sender, KeyPressEventArgs e)    //限制操作人编辑只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        private void textBox_newToolsIn_contact_KeyPress(object sender, KeyPressEventArgs e)     //限制联系方式只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }

        /********************************************************领用归还界面******************************************************/

        //领用归还入库清空按钮
        private void button_toolsReturn_cancel_Click(object sender, EventArgs e)
        {
            toolsIn.toolsReturnCleanALL();
            textBox_toolsReturn_code.Focus();
        }
        
        //领用归还界面 限制编辑框输入内容
        private void textBox_toolsReturn_return_KeyPress(object sender, KeyPressEventArgs e)               //限制归还人编辑框只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        private void textBox_toolsReturn_contact_KeyPress(object sender, KeyPressEventArgs e)              //限制归还人联系方式编辑框只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        private void textBox_toolsReturn_operator_KeyPress(object sender, KeyPressEventArgs e)             //限制操作人编辑框只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        private void textBox_toolsReturnOperator_contact_KeyPress(object sender, KeyPressEventArgs e)      //限制操作人联系方式编辑框只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        //领用归还界面 归还人 文本框默认值函数
        private void textBox_toolsReturn_return_Enter(object sender, EventArgs e)
        {
            toolsIn.textBox_toolsReturn_return_Enter();
        }
        private void textBox_toolsReturn_return_Leave(object sender, EventArgs e)
        {
            toolsIn.textBox_toolsReturn_return_Leave();
        }
        //领用归还界面 操作人 文本框默认值函数
        private void textBox_toolsReturn_operator_Enter(object sender, EventArgs e)
        {
            toolsIn.textBox_toolsReturn_operator_Enter();
        }
        private void textBox_toolsReturn_operator_Leave(object sender, EventArgs e)
        {
            toolsIn.textBox_toolsReturn_operator_Leave();
        }

        /********************************************************维修入库界面******************************************************/

        //维修入库确定按钮
        private void button_Enter_Click(object sender, EventArgs e)    
        {

        }
        //维修入库清空按钮
        private void button_Clear_Click(object sender, EventArgs e)
        {
            toolsIn.toolsRepairCleanALL();
            textBox_repairtoolsIn_code.Focus();
        }
        private void textBox_repairtoolsIn_contact_KeyPress(object sender, KeyPressEventArgs e)           //限制操作人联系方式编辑框只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        private void textBox_repairtoolsIn_operator_KeyPress(object sender, KeyPressEventArgs e)           //限制操作人编辑框只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (Char)8)
            {
                e.Handled = true;
            }
        }
        //维修入库界面 操作人 文本框默认值函数
        private void textBox_repairtoolsIn_operator_Leave(object sender, EventArgs e)
        {
            toolsIn.textBox_repairtoolsIn_operator_Leave();
        }
        private void textBox_repairtoolsIn_operator_Enter(object sender, EventArgs e)
        {
            toolsIn.textBox_repairtoolsIn_operator_Enter();
        }
        //维修入库界面 额定寿命 文本框默认值函数
        private void textBox_repairtoolsIn_lifespan_Leave(object sender, EventArgs e)
        {
            toolsIn.textBox_repairtoolsIn_lifespan_Leave();
        }
        private void textBox_repairtoolsIn_lifespan_Enter(object sender, EventArgs e)
        {
            toolsIn.textBox_repairtoolsIn_lifespan_Enter();
        }
        private void comboBox_repairtoolsIn_lifetype_TextChanged(object sender, EventArgs e)
        {
            if(comboBox_repairtoolsIn_lifetype.Text == "时间")
            {
                textBox_repairtoolsIn_lifespan.Text = "天 ";
            }
            else
            {
                textBox_repairtoolsIn_lifespan.Text = "次 ";
            }
        }
    }
}
