using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace nsStockManage
{
    class MenuStrip
    {
        /***************    各个按钮单击函数    ***************/
        
        //客户端管理模块
        public void 终端管理ToolStripMenuItem_Click()
        {
            SetMenuStripItemsBackColor(0);          //设置菜单栏选中后高亮
            SetToolStripButtonBackColor(Color.LightSteelBlue, Color.LightSteelBlue, Color.LightSteelBlue);//侧边栏按钮设置默认背景色
            SetToolStripButtonDisplayStyle(ToolStripItemDisplayStyle.ImageAndText, ToolStripItemDisplayStyle.ImageAndText, ToolStripItemDisplayStyle.Text);
            SetToolStripButtonText("更改终端", "退出系统","");

            Program.mw.toolStripButton1.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\client.ico");
            Program.mw.toolStripButton2.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\exit.ico");

            CommonFunction.HideAllPanelsExcept(Program.mw.panel11_welcome);

            int x = Program.mw.Width / 10 * 7;
            int y = Program.mw.Height / 10 * 7;
            Program.mw.groupBox_welcome_totalView.Location = new Point(x, y);
        }

        //工装入库
        public void 工装入库ToolStripMenuItem_Click()
        {
            SetMenuStripItemsBackColor(1);          //设置菜单栏选中后高亮
            SetToolStripButtonBackColor(Color.LightSteelBlue, Color.Aqua, Color.LightSteelBlue);//侧边栏按钮设置默认背景色
            SetToolStripButtonDisplayStyle(ToolStripItemDisplayStyle.ImageAndText, ToolStripItemDisplayStyle.ImageAndText, ToolStripItemDisplayStyle.ImageAndText);
            SetToolStripButtonText("新购入库", "领用归还", "维修入库");

            Program.mw.toolStripButton1.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\return.ico");
            Program.mw.toolStripButton2.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\newToolsIn.ico");
            Program.mw.toolStripButton3.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\repair1.ico");

            CommonFunction.HideAllPanelsExcept(Program.mw.panel22_toolsReturn);     //改为默认显示“领用归还”界面
            Program.mw.textBox_toolsReturn_code.Focus();

        }
        //工装出库
        public void 工装出库ToolStripMenuItem_Click()
        {
            SetMenuStripItemsBackColor(2);          //设置菜单栏选中后高亮
            SetToolStripButtonBackColor(Color.Aqua, Color.LightSteelBlue, Color.LightSteelBlue);//侧边栏按钮设置默认背景色
            SetToolStripButtonDisplayStyle(ToolStripItemDisplayStyle.ImageAndText, ToolStripItemDisplayStyle.ImageAndText, ToolStripItemDisplayStyle.ImageAndText);
            SetToolStripButtonText("工装方式", "机型方式", "维修报废");

            Program.mw.toolStripButton1.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\tools.ico");
            Program.mw.toolStripButton2.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\TV1.ico");
            Program.mw.toolStripButton3.Image = new Bitmap(System.Windows.Forms.Application.StartupPath +"\\image"+ "\\discard1.ico");

            CommonFunction.HideAllPanelsExcept(Program.mw.panel31_outByTools);    //显示工装方式出库界面
            Program.mw.textBox_outByTools_code.Focus();
        }

        //库位管理
        public void 库位管理ToolStripMenuItem_Click()
        {
            SetMenuStripItemsBackColor(3);          //设置菜单栏选中后高亮
            SetToolStripButtonBackColor(Color.Aqua, Color.LightSteelBlue, Color.LightSteelBlue);//侧边栏按钮设置默认背景色
            SetToolStripButtonDisplayStyle(ToolStripItemDisplayStyle.ImageAndText, ToolStripItemDisplayStyle.ImageAndText, ToolStripItemDisplayStyle.ImageAndText);
            SetToolStripButtonText("工装上架", "工装移位", "查看库位");

            Program.mw.toolStripButton1.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\shelf.ico");
            Program.mw.toolStripButton2.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\change.ico");
            Program.mw.toolStripButton3.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\lookup.ico");

            CommonFunction.HideAllPanelsExcept(Program.mw.panel41_putOnShelf);
            Program.mw.textBox_putOnShelf_code.Focus();
        }
  
        //数据管理
        public void 数据管理ToolStripMenuItem_Click()
        {
            SetMenuStripItemsBackColor(4);          //设置菜单栏选中后高亮
            SetToolStripButtonBackColor(Color.Aqua, Color.LightSteelBlue, Color.LightSteelBlue);//侧边栏按钮设置默认背景色
            SetToolStripButtonDisplayStyle(ToolStripItemDisplayStyle.ImageAndText, ToolStripItemDisplayStyle.ImageAndText, ToolStripItemDisplayStyle.ImageAndText);
            SetToolStripButtonText("工装数据", "操作记录", "人员数据");

            Program.mw.toolStripButton1.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\toolsData1.ico");
            Program.mw.toolStripButton2.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\record.ico");
            Program.mw.toolStripButton3.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\person.ico");

            CommonFunction.HideAllPanelsExcept(Program.mw.panel51_toolsData);
            Program.mw.textBox_toolsData_code.Focus();
        }

        //数据分析
        public void 数据分析ToolStripMenuItem_Click()
        {
            SetMenuStripItemsBackColor(5);          //设置菜单栏选中后高亮
            SetToolStripButtonBackColor(Color.LightSteelBlue, Color.LightSteelBlue, Color.LightSteelBlue);//侧边栏按钮设置默认背景色
            SetToolStripButtonDisplayStyle(ToolStripItemDisplayStyle.Text, ToolStripItemDisplayStyle.Text, ToolStripItemDisplayStyle.Text);
            SetToolStripButtonText("", "", "");

            CommonFunction.HideAllPanelsExcept(Program.mw.panel61_dataAnalysis);
            Program.mw.textBox_dataAnalysis_materialNumber.Focus();
        }

        //预警管理
        public void 预警管理ToolStripMenuItem_Click()
        {
            SetMenuStripItemsBackColor(6);          //设置菜单栏选中后高亮
            SetToolStripButtonBackColor(Color.Aqua, Color.LightSteelBlue, Color.LightSteelBlue);//侧边栏按钮设置默认背景色
            SetToolStripButtonDisplayStyle(ToolStripItemDisplayStyle.ImageAndText, ToolStripItemDisplayStyle.ImageAndText, ToolStripItemDisplayStyle.Text);
            SetToolStripButtonText("预警设置", "预警概览", "");

            Program.mw.toolStripButton1.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\setup.ico");
            Program.mw.toolStripButton2.Image = new Bitmap(System.Windows.Forms.Application.StartupPath + "\\image" + "\\warning.ico");

            CommonFunction.HideAllPanelsExcept(Program.mw.panel71_warningSetUp);
            Program.mw.textBox_warningSetUp_materialNumber.Focus();
        }


        /***************    通用函数    ***************/

        //菜单栏选中后高亮
        private void SetMenuStripItemsBackColor(int i)
        {
            for (int j = 0; j <= 6; j++)
            {
                if (j == i)
                {
                    Program.mw.menuStrip1.Items[j].BackColor = System.Drawing.Color.Aqua;
                }
                else
                {
                    Program.mw.menuStrip1.Items[j].BackColor = System.Drawing.Color.LightSteelBlue;
                }
            }
        }
        //侧边栏按钮设置默认背景色
        private void SetToolStripButtonBackColor(Color color1, Color color2, Color color3)
        {
            Program.mw.toolStripButton1.BackColor = color1;
            Program.mw.toolStripButton2.BackColor = color2;
            Program.mw.toolStripButton3.BackColor = color3;
        }

        private void SetToolStripButtonDisplayStyle(ToolStripItemDisplayStyle style1, ToolStripItemDisplayStyle style2, ToolStripItemDisplayStyle style3)
        {
            Program.mw.toolStripButton1.DisplayStyle = style1;
            Program.mw.toolStripButton2.DisplayStyle = style2;
            Program.mw.toolStripButton3.DisplayStyle = style3;
        }

        private void SetToolStripButtonText(String string1, String string2, String string3)
        {
            Program.mw.toolStripButton1.Text = string1;
            Program.mw.toolStripButton2.Text = string2;
            Program.mw.toolStripButton3.Text = string3;
        }


    }
}
