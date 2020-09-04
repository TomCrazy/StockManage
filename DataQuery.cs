using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using DataGridViewAutoFilter;

namespace nsStockManage
{
    class DataManage
    {
        /*****************************************工装数据界面**************************************************/

        public void textBox_toolsData_code_KeyPress(char e)            //编码文本框回车函数
        {
            if (e == (char)Keys.Enter)
            {
                String code = Program.mw.textBox_toolsData_code.Text;

                if (code.Length > 0)
                {
                    if (CommonFunction.CheckCodeLegality(code))        //判断编码合法性
                    {
                        String[] temp = code.Split('-');
                        String materialNumber = temp[1];
                        String sql = "SELECT * FROM tools WHERE code LIKE '%" + code + "%';";
                        DataSet ds = MainWindow.connection.Select(sql);
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            MainWindow.public_dsToolsData = ds;
                            //刷新数据窗口
                            CommonFunction.FillDataGridView_Tools(Program.mw.dataGridView_toolsData, Program.mw.myBindingNavigator_ToolsData, Program.mw.bindingNavigator_ToolsData, ds);
                            
                            //展示库存
                            CommonFunction.Fill_GroupBox_Info(Program.mw.panel51_toolsData, materialNumber, Program.mw.button_toolsData_export);
                        }
                        else
                        {
                            MessageBox.Show("未找到相关记录！");
                        }
                    }
                }
            }
        }

        public void ToolsData_LookUp()
        {
            String code = Program.mw.textBox_toolsData_code.Text;
            String toolName = Program.mw.textBox_toolsData_toolName.Text;
            String materialNumber = Program.mw.textBox_toolsData_materialNumber.Text;
            String model = Program.mw.textBox_toolsData_model.Text;
            String line = Program.mw.textBox_toolsData_line.Text;
            String storageState = Program.mw.comboBox_toolsData_storageState.Text;
            String functionState = Program.mw.comboBox_toolsData_functionState.Text;

            StringBuilder sql = new StringBuilder("SELECT * FROM tools");
            List<string> wheres = new List<string>();

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

            if (line.Trim().Length > 0)
            {
                wheres.Add(" line LIKE '%" + line.Trim() + "%'");
            }

            if (storageState.Trim().Length > 0)
            {
                wheres.Add(" storageState LIKE '%" + storageState.Trim() + "%'");
            }

            if (functionState.Trim().Length > 0)
            {
                wheres.Add(" functionState LIKE '%" + functionState.Trim() + "%'");
            }

            //判断条件是否为空，为空则不查询
            if (wheres.Count > 0)
            {
                String wh = String.Join(" AND ", wheres.ToArray());
                sql.Append(" WHERE " + wh);

                try
                {
                    DataSet ds = MainWindow.connection.Select(sql.ToString());
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        MainWindow.public_dsToolsData = ds;
                        //刷新数据窗口
                        CommonFunction.FillDataGridView_Tools(Program.mw.dataGridView_toolsData, Program.mw.myBindingNavigator_ToolsData, Program.mw.bindingNavigator_ToolsData, ds);
                        
                        //展示库存
                        CommonFunction.Fill_GroupBox_Info(Program.mw.panel51_toolsData, materialNumber, Program.mw.button_toolsData_export);
                    }
                    else
                    {
                        MessageBox.Show("未找到相关记录！");
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show("数据查询出错！" + e.Message);
                }
            }
            else
            {
                try
                {
                    String sql1 = "SELECT * FROM tools;";
                    DataSet ds = MainWindow.connection.Select(sql1);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        MainWindow.public_dsToolsData = ds;
                        //刷新数据窗口
                        CommonFunction.FillDataGridView_Tools(Program.mw.dataGridView_toolsData, Program.mw.myBindingNavigator_ToolsData, Program.mw.bindingNavigator_ToolsData, ds);
                        
                        //展示库存
                        CommonFunction.Fill_GroupBox_Info(Program.mw.panel51_toolsData, materialNumber, Program.mw.button_toolsData_export);
                    }
                    else
                    {
                        MessageBox.Show("未找到相关记录！");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("数据查询出错！" + e.Message);
                }
            }

        }
        
        public void ToolsData_Export()
        {
            if (MainWindow.public_dsToolsData.Tables.Count > 0 && MainWindow.public_dsToolsData.Tables[0].Rows.Count > 0)
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
                    CommonFunction.DataSetToExcel(MainWindow.public_dsToolsData, localFilePath);
                }
            }
            else
            {
                MessageBox.Show("无数据！");
                return;
            }
            
        }

        public void ToolsData_CleanAll()
        {
            Program.mw.textBox_toolsData_code.Text = "";
            Program.mw.textBox_toolsData_toolName.Text = "";
            Program.mw.textBox_toolsData_materialNumber.Text = "";
            Program.mw.textBox_toolsData_model.Text = "";
            Program.mw.textBox_toolsData_line.Text = "";
            Program.mw.comboBox_toolsData_storageState.Text = "";
            Program.mw.comboBox_toolsData_functionState.Text = "";

            CommonFunction.FillDataGridView_Tools(Program.mw.dataGridView_toolsData, Program.mw.myBindingNavigator_ToolsData, Program.mw.bindingNavigator_ToolsData, "");
        }




        /**********************************************记录数据界面**************************************************/
        
        public void TextBox_recordsData_code_KeyPress(char e)            //编码文本框回车函数
        {
            if (e == (char)Keys.Enter)
            {
                String code = Program.mw.textBox_recordsData_code.Text;

                if (code.Length > 0)
                {
                    if (CommonFunction.CheckCodeLegality(code))        //判断编码合法性
                    {
                        String[] temp = code.Split('-');
                        String materialNumber = temp[1];
                        String sql = "SELECT * FROM records WHERE code LIKE '%" + code + "%';";
                        DataSet ds = MainWindow.connection.Select(sql);
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            MainWindow.public_dsRecordsData = ds;
                            //刷新数据窗口
                            CommonFunction.FillDataGridView_Records(Program.mw.dataGridView_recordsData, Program.mw.myBindingNavigator_RecordsData, Program.mw.bindingNavigator_RecordsData, ds);
                            
                            //展示库存
                            CommonFunction.Fill_GroupBox_Info(Program.mw.panel52_recordsData, materialNumber, Program.mw.button_recordsData_export);
                        }
                        else
                        {
                            MessageBox.Show("未找到相关记录！");
                        }
                    }
                }
            }
        }

        public void RecordsData_LookUp()
        {
            String code = Program.mw.textBox_recordsData_code.Text;
            String toolName = Program.mw.textBox_recordsData_toolName.Text;
            String materialNumber = Program.mw.textBox_recordsData_materialNumber.Text;
            String remarks = Program.mw.textBox_recordsData_remarks.Text;
            String borrower = Program.mw.textBox_recordsData_borrower.Text;
            String line = Program.mw.textBox_recordsData_line.Text;
            String operationType = Program.mw.comboBox_recordsData_operationType.Text;
            String operator1 = Program.mw.textBox_recordsData_operator.Text;
            String operationDate = Program.mw.dateTimePicker_recordsData_operationDate.Text;

            StringBuilder sql = new StringBuilder("SELECT * FROM records");
            List<string> wheres = new List<string>();

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

            if (remarks.Trim().Length > 0)
            {
                wheres.Add(" remarks LIKE '%" + remarks.Trim() + "%'");
            }

            if (borrower.Trim().Length > 0)
            {
                wheres.Add(" borrower LIKE '%" + borrower.Trim() + "%'");
            }

            if (line.Trim().Length > 0)
            {
                wheres.Add(" line LIKE '%" + line.Trim() + "%'");
            }

            if (operationType.Trim().Length > 0)
            {
                wheres.Add(" operationType LIKE '%" + operationType.Trim() + "%'");
            }

            if (operator1.Trim().Length > 0)
            {
                wheres.Add(" operator LIKE '%" + operator1.Trim() + "%'");
            }

            if (operationDate.Trim().Length > 0 && (Program.mw.dateTimePicker_recordsData_operationDate.Checked))
            {
                wheres.Add(" operationDate LIKE '%" + operationDate.Trim() + "%'");
            }

            //判断条件是否为空，为空则不查询
            if (wheres.Count > 0)
            {
                String wh = String.Join(" AND ", wheres.ToArray());
                sql.Append(" WHERE " + wh);

                try
                {
                    DataSet ds = MainWindow.connection.Select(sql.ToString());
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        MainWindow.public_dsRecordsData = ds;
                        //刷新数据窗口
                        CommonFunction.FillDataGridView_Records(Program.mw.dataGridView_recordsData, Program.mw.myBindingNavigator_RecordsData, Program.mw.bindingNavigator_RecordsData, ds);
                        
                        //展示库存
                        CommonFunction.Fill_GroupBox_Info(Program.mw.panel52_recordsData, materialNumber, Program.mw.button_recordsData_export);
                    }
                    else
                    {
                        MessageBox.Show("未找到相关记录！");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("数据查询出错！" + e.Message);
                }
            }
            else
            {
                try
                {
                    String sql1 = "SELECT * FROM records;";
                    DataSet ds = MainWindow.connection.Select(sql1);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        MainWindow.public_dsRecordsData = ds;
                        //刷新数据窗口
                        CommonFunction.FillDataGridView_Records(Program.mw.dataGridView_recordsData, Program.mw.myBindingNavigator_RecordsData, Program.mw.bindingNavigator_RecordsData, ds);
                        
                        //展示库存
                        CommonFunction.Fill_GroupBox_Info(Program.mw.panel52_recordsData, materialNumber, Program.mw.button_recordsData_export);
                    }
                    else
                    {
                        MessageBox.Show("未找到相关记录！");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("数据查询出错！" + e.Message);
                }
            }

        }

        public void RecordsData_Export()
        {
            if (MainWindow.public_dsRecordsData.Tables.Count > 0 && MainWindow.public_dsRecordsData.Tables[0].Rows.Count > 0)
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
                    CommonFunction.DataSetToExcel(MainWindow.public_dsRecordsData, localFilePath);
                }
            }
            else
            {
                MessageBox.Show("无数据！");
                return;
            }
        }

        public void RecordsData_CleanAll()
        {
            Program.mw.textBox_recordsData_borrower.Text = "";
            Program.mw.textBox_recordsData_code.Text = "";
            Program.mw.textBox_recordsData_toolName.Text = "";
            Program.mw.textBox_recordsData_line.Text = "";
            Program.mw.textBox_recordsData_materialNumber.Text = "";
            Program.mw.comboBox_recordsData_operationType.Text = "";
            Program.mw.textBox_recordsData_operator.Text = "";
            Program.mw.textBox_recordsData_remarks.Text = "";
            Program.mw.dateTimePicker_recordsData_operationDate.ResetText();

            MainWindow.public_dsRecordsData = CommonFunction.FillDataGridView_Records(Program.mw.dataGridView_recordsData, Program.mw.myBindingNavigator_RecordsData, Program.mw.bindingNavigator_RecordsData, "");
            
        }



        /**********************************************人员数据界面**************************************************/

        public void textBox_personsData_employeeID_KeyPress(char e)            //编码文本框回车函数
        {
            if (e == (char)Keys.Enter)
            {
                String employeeID = Program.mw.textBox_personsData_employeeID.Text;

                if (employeeID.Length > 0)
                {
                    String sql = "SELECT * FROM personal WHERE employeeID LIKE '%" + employeeID + "%';";
                    DataSet ds = MainWindow.connection.Select(sql);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        MainWindow.public_dsPersonData = ds;
                        //刷新数据窗口
                        CommonFunction.FillDataGridView_Persons(Program.mw.dataGridView_personsData, Program.mw.myBindingNavigator_PersonsData, Program.mw.bindingNavigator_PersonsData, ds);
                        
                    }
                    else
                    {
                        MessageBox.Show("未找到相关记录！");
                    }
                }
            }
        }

        public void PersonsData_LookUp()
        {
            String employeeID = Program.mw.textBox_personsData_employeeID.Text;
            String name = Program.mw.textBox_personsData_name.Text;
            String contact = Program.mw.textBox_personsData_contact.Text;
            String workshop = Program.mw.textBox_personsData_workshop.Text;
            String line = Program.mw.textBox_personsData_line.Text;

            StringBuilder sql = new StringBuilder("SELECT * FROM personal");
            List<string> wheres = new List<string>();

            //拼接查询条件
            if (employeeID.Trim().Length > 0)
            {
                wheres.Add(" employeeID LIKE '%" + employeeID.Trim() + "%'");
            }

            if (name.Trim().Length > 0)
            {
                wheres.Add(" name LIKE '%" + name.Trim() + "%'");
            }

            if (contact.Trim().Length > 0)
            {
                wheres.Add(" contact LIKE '%" + contact.Trim() + "%'");
            }

            if (workshop.Trim().Length > 0)
            {
                wheres.Add(" workshop LIKE '%" + workshop.Trim() + "%'");
            }

            if (line.Trim().Length > 0)
            {
                wheres.Add(" line LIKE '%" + line.Trim() + "%'");
            }

            //判断条件是否为空，为空则不查询
            if (wheres.Count > 0)
            {
                String wh = String.Join(" AND ", wheres.ToArray());
                sql.Append(" WHERE " + wh);

                try
                {
                    DataSet ds = MainWindow.connection.Select(sql.ToString());
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        MainWindow.public_dsPersonData = ds;
                        //刷新数据窗口
                        CommonFunction.FillDataGridView_Persons(Program.mw.dataGridView_personsData, Program.mw.myBindingNavigator_PersonsData, Program.mw.bindingNavigator_PersonsData, ds);
                        
                    }
                    else
                    {
                        MessageBox.Show("未找到相关记录！");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("数据查询出错！" + e.Message);
                }
            }
            else
            {
                try
                {
                    String sql1 = "SELECT * FROM personal;";
                    DataSet ds = MainWindow.connection.Select(sql1);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        MainWindow.public_dsPersonData = ds;
                        //刷新数据窗口
                        CommonFunction.FillDataGridView_Persons(Program.mw.dataGridView_personsData, Program.mw.myBindingNavigator_PersonsData, Program.mw.bindingNavigator_PersonsData, ds);
                        
                    }
                    else
                    {
                        MessageBox.Show("未找到相关记录！");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("数据查询出错！" + e.Message);
                }
            }

        }

        public void PersonsData_Export()
        {
            if (MainWindow.public_dsPersonData.Tables.Count > 0 && MainWindow.public_dsPersonData.Tables[0].Rows.Count > 0)
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
                    CommonFunction.DataSetToExcel(MainWindow.public_dsPersonData, localFilePath);
                }
            }
            else
            {
                MessageBox.Show("无数据！");
                return;
            }
        }

        public void  PersonData_CleanAll()
        {
            Program.mw.textBox_personsData_contact.Text = "";
            Program.mw.textBox_personsData_employeeID.Text = "";
            Program.mw.textBox_personsData_line.Text = "";
            Program.mw.textBox_personsData_name.Text = "";
            Program.mw.textBox_personsData_workshop.Text = "";

            CommonFunction.FillDataGridView_Persons(Program.mw.dataGridView_personsData, Program.mw.myBindingNavigator_PersonsData, Program.mw.bindingNavigator_PersonsData, "");

        }






    }
}
