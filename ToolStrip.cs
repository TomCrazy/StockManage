using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace nsStockManage
{
    /*****************************************主界面左侧  工具栏*******************************************/

    class ToolStrip
    {

        /************************************ 第一个按钮 **************************************/

        public void toolStripButton1_Click()                    //按下工具栏处第一个按钮（更改终端、新购入库、工装方式、工装上架、工装数据）
        {
            Program.mw.toolStripButton1.BackColor = System.Drawing.Color.Aqua;
            Program.mw.toolStripButton2.BackColor = System.Drawing.Color.LightSteelBlue;
            Program.mw.toolStripButton3.BackColor = System.Drawing.Color.LightSteelBlue;

            if (Program.mw.toolStripButton1.Text == "更改终端")
            {
                ClientManage clientWindow = new ClientManage();
                clientWindow.ShowDialog();
            }

            if (Program.mw.toolStripButton1.Text == "新购入库")
            {
                CommonFunction.HideAllPanelsExcept(Program.mw.panel21_newToolsIn);

                if (Program.mw.checkBox_newToolsIn_batch.Checked == false)                                  //非批量入库
                {
                    Program.mw.textBox_newToolsIn_endCode.BackColor = System.Drawing.Color.LightGray;     //结尾编码变灰
                    Program.mw.textBox_newToolsIn_endCode.Enabled = false;                                //结尾编码只读
                }
                Program.mw.textBox_newToolsIn_code.Focus();                                                 //默认焦点置于二维码输入框

            }

            if(Program.mw.toolStripButton1.Text == "工装方式")
            {
                CommonFunction.HideAllPanelsExcept(Program.mw.panel31_outByTools);
                Program.mw.textBox_outByTools_code.Focus();
            }
            if(Program.mw.toolStripButton1.Text == "工装上架")
            {
                CommonFunction.HideAllPanelsExcept(Program.mw.panel41_putOnShelf);
                Program.mw.textBox_putOnShelf_code.Focus();
            }
            if(Program.mw.toolStripButton1.Text=="工装数据")
            {
                CommonFunction.HideAllPanelsExcept(Program.mw.panel51_toolsData);
                Program.mw.textBox_toolsData_code.Focus();
            }
            if(Program.mw.toolStripButton1.Text=="预警设置")
            {
                CommonFunction.HideAllPanelsExcept(Program.mw.panel71_warningSetUp);
                Program.mw.textBox_warningSetUp_materialNumber.Focus();
            }

        }

        /************************************ 第二个按钮 **************************************/

        public void toolStripButton2_Click()  //toolStrip第二个按钮（退出系统、领用归还、机型方式、工装移位、机型数据）
        {
            Program.mw.toolStripButton1.BackColor = System.Drawing.Color.LightSteelBlue;
            Program.mw.toolStripButton2.BackColor = System.Drawing.Color.Aqua;
            Program.mw.toolStripButton3.BackColor = System.Drawing.Color.LightSteelBlue;

            if (Program.mw.toolStripButton2.Text == "退出系统")     //退出系统按钮
            {
                DialogResult MsgBoxResult;//设置对话框的返回值

                MsgBoxResult = MessageBox.Show("是否退出系统？",//对话框的显示内容
                "退出系统",//对话框的标题
                MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮
                MessageBoxIcon.Exclamation,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号
                MessageBoxDefaultButton.Button2);//定义对话框的按钮式样

                if (MsgBoxResult == DialogResult.Yes)//如果对话框的返回值是YES（按"Y"按钮）
                {
                    Program.mw.Close();
                    Application.Exit();
                }

                if (MsgBoxResult == DialogResult.No)//如果对话框的返回值是NO（按"N"按钮）
                {
                }
            }
            if (Program.mw.toolStripButton2.Text == "领用归还")
            {
                CommonFunction.HideAllPanelsExcept(Program.mw.panel22_toolsReturn);
                Program.mw.textBox_toolsReturn_code.Focus();
            }
            if(Program.mw.toolStripButton2.Text == "机型方式")
            {
            }
            if (Program.mw.toolStripButton2.Text == "工装移位")
            {
                CommonFunction.HideAllPanelsExcept(Program.mw.panel42_changeShelf);
                Program.mw.textBox_changeShelf_code.Focus();
            }
            if(Program.mw.toolStripButton2.Text=="操作记录")
            {
                CommonFunction.HideAllPanelsExcept(Program.mw.panel52_recordsData);
                Program.mw.textBox_recordsData_code.Focus();
            }
            if(Program.mw.toolStripButton2.Text=="预警概览")
            {
                CommonFunction.HideAllPanelsExcept(Program.mw.panel72_warningOverview);
                Program.mw.panel72_warningOverview.Focus();
            }
        }
        
        /************************************ 第三个按钮 **************************************/

        public void toolStripButton3_Click()                //toolStrip第三个按钮
        {
            Program.mw.toolStripButton1.BackColor = System.Drawing.Color.LightSteelBlue;
            Program.mw.toolStripButton2.BackColor = System.Drawing.Color.LightSteelBlue;
            Program.mw.toolStripButton3.BackColor = System.Drawing.Color.Aqua;

            if (Program.mw.toolStripButton3.Text == "维修入库")
            {
                CommonFunction.HideAllPanelsExcept(Program.mw.panel23_repairtoolsIn);
                Program.mw.textBox_repairtoolsIn_code.Focus();
            }
            if(Program.mw.toolStripButton3.Text == "维修报废")
            {
                CommonFunction.HideAllPanelsExcept(Program.mw.panel33_scrapTools);
                Program.mw.textBox_scrapTools_code.Focus();
            }
            if(Program.mw.toolStripButton3.Text=="查看库位")
            {
                CommonFunction.HideAllPanelsExcept(Program.mw.panel43_lookUpShelf);
                Program.mw.textBox_lookUpShelf_code.Focus();
            }
            if(Program.mw.toolStripButton3.Text=="人员数据")
            {
                CommonFunction.HideAllPanelsExcept(Program.mw.panel53_personsData);
                Program.mw.textBox_personsData_employeeID.Focus();
            }
        }
    }
}
