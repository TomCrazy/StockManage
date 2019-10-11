using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nsClientManage;
using nsMainWindow;
using System.Windows.Forms;

namespace nsStockManage
{
    /*****************************************主界面左侧  工具栏*******************************************/

    class ToolStrip
    {

        public void toolStripButton1_Click()                    //按下工具栏处第一个按钮（更改终端、新购入库、工装方式、工装上架、工装数据）
        {
            Program.mw.toolStripButton1.BackColor = System.Drawing.Color.LightSkyBlue;
            Program.mw.toolStripButton2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            Program.mw.toolStripButton3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            if (Program.mw.toolStripButton1.Text == "更改终端")
            {
                ClientManage clientWindow = new ClientManage();
                clientWindow.ShowDialog();
            }
            if (Program.mw.toolStripButton1.Text == "新购入库")
            {
                Program.mw.panel_newtoolsIn.Visible = true;
                Program.mw.panel_toolsReturn.Visible = false;
                Program.mw.panel_repairtoolsIn.Visible = false;
                Program.mw.panel_outByTools.Visible = false;

                if (Program.mw.checkBox_newToolsIn_batch.Checked == false)                                  //非批量入库
                {
                    Program.mw.textBox_newToolsIn_numberEnd.BackColor = System.Drawing.Color.LightGray;     //结尾编码变灰
                    Program.mw.textBox_newToolsIn_numberEnd.ReadOnly = true;                                //结尾编码只读
                }
                Program.mw.textBox_newToolsIn_code.Focus();                                                 //默认焦点置于二维码输入框

                ToolsIn ti = new ToolsIn();
                ti.drawListView_newToolsIn(Program.mw.listView_newToolsIn);
                ti.fillListView_newToolsIn(Program.mw.listView_newToolsIn);
            }
            if(Program.mw.toolStripButton1.Text == "工装方式")
            {
                Program.mw.panel_newtoolsIn.Visible = false;
                Program.mw.panel_toolsReturn.Visible = false;
                Program.mw.panel_repairtoolsIn.Visible = false;
                Program.mw.panel_outByTools.Visible = true;

                int listViewWidth = Screen.PrimaryScreen.Bounds.Width - Program.mw.listView_repairtoolsIn.Location.X * 2 - Program.mw.toolStrip1.Width;
                int listViewHeight = Screen.PrimaryScreen.Bounds.Height - Program.mw.listView_repairtoolsIn.Location.Y - Program.mw.statusStrip1.Height - Program.mw.menuStrip1.Height - 85;
                int listViewColumnWidth = listViewWidth / 15;
                Program.mw.listView_outByTools.Size = new System.Drawing.Size(listViewWidth, listViewHeight);
                Program.mw.listView_outByTools.Font = new System.Drawing.Font("微软雅黑", 8F);
                Program.mw.listView_outByTools.GridLines = true;
                Program.mw.listView_outByTools.View = View.Details;
                Program.mw.listView_outByTools.HeaderStyle = ColumnHeaderStyle.Clickable;//表头样式
                Program.mw.listView_outByTools.FullRowSelect = true;//表示在控件上，是否可以选择一整行
                Program.mw.listView_outByTools.Columns.Add("", 0, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
                Program.mw.listView_outByTools.Columns.Add("工装编码", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
                Program.mw.listView_outByTools.Columns.Add("工装名称", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_outByTools.Columns.Add("物料号", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_outByTools.Columns.Add("功能状态", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
                Program.mw.listView_outByTools.Columns.Add("领用线体", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
                Program.mw.listView_outByTools.Columns.Add("领用工位", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_outByTools.Columns.Add("用途", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_outByTools.Columns.Add("库位", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_outByTools.Columns.Add("架位", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
                Program.mw.listView_outByTools.Columns.Add("层位", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
                Program.mw.listView_outByTools.Columns.Add("领用人", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_outByTools.Columns.Add("领用人姓名", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_outByTools.Columns.Add("领用人联系方式", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_outByTools.Columns.Add("厂家", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
                Program.mw.listView_outByTools.Columns.Add("备注", listViewWidth - listViewColumnWidth * 14, HorizontalAlignment.Center);
            }

        }

        public void toolStripButton2_Click()  //toolStrip第二个按钮
        {
            Program.mw.toolStripButton2.BackColor = System.Drawing.Color.LightSkyBlue;
            Program.mw.toolStripButton1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            Program.mw.toolStripButton3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            if (Program.mw.toolStripButton2.Text == "退出系统")     //退出系统按钮
            {
                DialogResult MsgBoxResult;//设置对话框的返回值

                MsgBoxResult = MessageBox.Show("是否退出系统？",//对话框的显示内容

                "退出提示",//对话框的标题

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
                Program.mw.panel_toolsReturn.Visible = true;
                Program.mw.panel_newtoolsIn.Visible = false;
                Program.mw.panel_repairtoolsIn.Visible = false;
                Program.mw.panel_outByTools.Visible = false;

                int listViewWidth = Screen.PrimaryScreen.Bounds.Width - Program.mw.listView_toolsReturn.Location.X * 2 - Program.mw.toolStrip1.Width;
                int listViewHeight = Screen.PrimaryScreen.Bounds.Height - Program.mw.listView_toolsReturn.Location.Y - Program.mw.statusStrip1.Height - Program.mw.menuStrip1.Height - 85;
                int listViewColumnWidth = listViewWidth / 12;
                Program.mw.listView_toolsReturn.Size = new System.Drawing.Size(listViewWidth, listViewHeight);
                Program.mw.listView_toolsReturn.Font = new System.Drawing.Font("微软雅黑", 8F);
                Program.mw.listView_toolsReturn.GridLines = true;
                Program.mw.listView_toolsReturn.View = View.Details;
                Program.mw.listView_toolsReturn.HeaderStyle = ColumnHeaderStyle.Clickable;//表头样式
                Program.mw.listView_toolsReturn.FullRowSelect = true;//表示在控件上，是否可以选择一整行
                Program.mw.listView_toolsReturn.Columns.Add("", 0, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
                Program.mw.listView_toolsReturn.Columns.Add("工装二维码", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
                Program.mw.listView_toolsReturn.Columns.Add("工装编码", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_toolsReturn.Columns.Add("物料号", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_toolsReturn.Columns.Add("归还线体", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
                Program.mw.listView_toolsReturn.Columns.Add("归还日期/时间", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_toolsReturn.Columns.Add("归还人", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_toolsReturn.Columns.Add("归还人姓名", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
                Program.mw.listView_toolsReturn.Columns.Add("归还人联系方式", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
                Program.mw.listView_toolsReturn.Columns.Add("操作人", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_toolsReturn.Columns.Add("操作人姓名", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_toolsReturn.Columns.Add("操作人联系方式", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_toolsReturn.Columns.Add("备注", listViewWidth - listViewColumnWidth * 11, HorizontalAlignment.Center);
            }
        }

        public void toolStripButton3_Click()                //toolStrip第三个按钮
        {
            Program.mw.toolStripButton3.BackColor = System.Drawing.Color.LightSkyBlue;
            Program.mw.toolStripButton1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            Program.mw.toolStripButton2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            if (Program.mw.toolStripButton3.Text == "维修入库")
            {
                Program.mw.panel_toolsReturn.Visible = false;
                Program.mw.panel_newtoolsIn.Visible = false;
                Program.mw.panel_outByTools.Visible = false;
                Program.mw.panel_repairtoolsIn.Visible = true;

                int listViewWidth = Screen.PrimaryScreen.Bounds.Width - Program.mw.listView_repairtoolsIn.Location.X * 2 - Program.mw.toolStrip1.Width;
                int listViewHeight = Screen.PrimaryScreen.Bounds.Height - Program.mw.listView_repairtoolsIn.Location.Y - Program.mw.statusStrip1.Height - Program.mw.menuStrip1.Height - 85;
                int listViewColumnWidth = listViewWidth / 15;
                Program.mw.listView_repairtoolsIn.Size = new System.Drawing.Size(listViewWidth, listViewHeight);
                Program.mw.listView_repairtoolsIn.Font = new System.Drawing.Font("微软雅黑", 8F);
                Program.mw.listView_repairtoolsIn.GridLines = true;
                Program.mw.listView_repairtoolsIn.View = View.Details;
                Program.mw.listView_repairtoolsIn.HeaderStyle = ColumnHeaderStyle.Clickable;//表头样式
                Program.mw.listView_repairtoolsIn.FullRowSelect = true;//表示在控件上，是否可以选择一整行
                Program.mw.listView_repairtoolsIn.Columns.Add("", 0, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
                Program.mw.listView_repairtoolsIn.Columns.Add("工装编码", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
                Program.mw.listView_repairtoolsIn.Columns.Add("工装名称", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_repairtoolsIn.Columns.Add("物料号", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_repairtoolsIn.Columns.Add("功能状态", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
                Program.mw.listView_repairtoolsIn.Columns.Add("额定寿命", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
                Program.mw.listView_repairtoolsIn.Columns.Add("维修日期", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_repairtoolsIn.Columns.Add("维修次数", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_repairtoolsIn.Columns.Add("库位", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_repairtoolsIn.Columns.Add("架位", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
                Program.mw.listView_repairtoolsIn.Columns.Add("层位", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
                Program.mw.listView_repairtoolsIn.Columns.Add("操作人", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_repairtoolsIn.Columns.Add("操作人姓名", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_repairtoolsIn.Columns.Add("操作人联系方式", listViewColumnWidth, HorizontalAlignment.Center); //添加
                Program.mw.listView_repairtoolsIn.Columns.Add("厂家", listViewColumnWidth, HorizontalAlignment.Center); //添加（列宽度、列的对齐方式）
                Program.mw.listView_repairtoolsIn.Columns.Add("备注", listViewWidth - listViewColumnWidth * 14, HorizontalAlignment.Center);
            }
        }
    }
}
