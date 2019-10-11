using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nsMainWindow;

namespace nsStockManage
{
    class MenuStrip
    {
        /***************    各个按钮单击函数    ***************/

        //客户端管理模块
        public void 终端管理ToolStripMenuItem_Click()
        {
            Program.mw.toolStripButton1.BackColor = System.Drawing.SystemColors.ActiveCaption;   //侧边栏按钮设置默认背景色
            Program.mw.toolStripButton2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            Program.mw.toolStripButton3.BackColor = System.Drawing.SystemColors.ActiveCaption;

            Program.mw.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            Program.mw.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            Program.mw.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            Program.mw.toolStripButton1.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\client.ico");
            Program.mw.toolStripButton2.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\exit.ico");
            Program.mw.toolStripButton1.Text = "更改终端";
            Program.mw.toolStripButton2.Text = "退出系统";
            Program.mw.toolStripButton3.Text = "";
        }

        //工装入库
        public void 工装入库ToolStripMenuItem_Click()
        {
            Program.mw.toolStripButton1.BackColor = System.Drawing.SystemColors.ActiveCaption;   //侧边栏按钮设置默认背景色
            Program.mw.toolStripButton2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            Program.mw.toolStripButton3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            Program.mw.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            Program.mw.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            Program.mw.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            Program.mw.toolStripButton1.Text = "新购入库";
            Program.mw.toolStripButton2.Text = "领用归还";
            Program.mw.toolStripButton3.Text = "维修入库";
            Program.mw.toolStripButton1.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\return.ico");
            Program.mw.toolStripButton2.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\newToolsIn.ico");
            Program.mw.toolStripButton3.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\repair1.ico");
        }
        //工装出库
        public void 工装出库ToolStripMenuItem_Click()
        {
            Program.mw.toolStripButton1.BackColor = System.Drawing.SystemColors.ActiveCaption;   //侧边栏按钮设置默认背景色
            Program.mw.toolStripButton2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            Program.mw.toolStripButton3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            Program.mw.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            Program.mw.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            Program.mw.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            Program.mw.toolStripButton1.Text = "工装方式";
            Program.mw.toolStripButton2.Text = "机型方式";
            Program.mw.toolStripButton3.Text = "维修报废";
            Program.mw.toolStripButton1.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\tools.ico");
            Program.mw.toolStripButton2.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\TV1.ico");
            Program.mw.toolStripButton3.Image = new Bitmap(System.Windows.Forms.Application.StartupPath +"\\image"+ "\\discard1.ico");
        }

        //库位管理
        public void 库位管理ToolStripMenuItem_Click()
        {
            Program.mw.toolStripButton1.BackColor = System.Drawing.SystemColors.ActiveCaption;   //侧边栏按钮设置默认背景色
            Program.mw.toolStripButton2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            Program.mw.toolStripButton3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            Program.mw.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            Program.mw.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            Program.mw.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            Program.mw.toolStripButton1.Text = "工装上架";
            Program.mw.toolStripButton2.Text = "工装移位";
            Program.mw.toolStripButton3.Text = "查看库位";
            Program.mw.toolStripButton1.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\shelf.ico");
            Program.mw.toolStripButton2.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\change.ico");
            Program.mw.toolStripButton3.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\lookup.ico");
        }

        //数据管理
        public void 数据管理ToolStripMenuItem_Click()
        {
            Program.mw.toolStripButton1.BackColor = System.Drawing.SystemColors.ActiveCaption;   //侧边栏按钮设置默认背景色
            Program.mw.toolStripButton2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            Program.mw.toolStripButton3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            Program.mw.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            Program.mw.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            Program.mw.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            Program.mw.toolStripButton1.Text = "工装数据";
            Program.mw.toolStripButton2.Text = "机型数据";
            Program.mw.toolStripButton3.Text = "";
            Program.mw.toolStripButton1.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\toolsData1.ico");
            Program.mw.toolStripButton2.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\machine.ico");

        }

        //数据分析
        public void 数据分析ToolStripMenuItem_Click()
        {
            Program.mw.toolStripButton1.BackColor = System.Drawing.SystemColors.ActiveCaption;   //侧边栏按钮设置默认背景色
            Program.mw.toolStripButton2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            Program.mw.toolStripButton3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            Program.mw.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            Program.mw.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            Program.mw.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            Program.mw.toolStripButton1.Text = "";
            Program.mw.toolStripButton2.Text = "";
            Program.mw.toolStripButton3.Text = "";
        }

        //预警管理
        public void 预警管理ToolStripMenuItem_Click()
        {
            Program.mw.toolStripButton1.BackColor = System.Drawing.SystemColors.ActiveCaption;   //侧边栏按钮设置默认背景色
            Program.mw.toolStripButton2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            Program.mw.toolStripButton3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            Program.mw.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            Program.mw.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            Program.mw.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            Program.mw.toolStripButton1.Text = "";
            Program.mw.toolStripButton2.Text = "";
            Program.mw.toolStripButton3.Text = "";
        }

    }
}
