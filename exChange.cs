using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nsStockManage
{
    public partial class ExChange : Form
    {
        DataTable dtExchange = new DataTable();
        DBConnection connection = new DBConnection();
        String sql = "";
        int count = 0;

        public ExChange()
        {
            InitializeComponent();
            dtExchange.Columns.Add("序号", typeof(String));
            dtExchange.Columns.Add("归还工装", typeof(String));
            dtExchange.Columns.Add("物料号", typeof(String));
            dtExchange.Columns.Add("换新工装", typeof(String));
            dataGridView_Exchange.DataSource = dtExchange;
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
            {
                Name = "deleteButton",
                HeaderText = " 删除 ",
                Text = "删除",
                UseColumnTextForButtonValue = true,
            };
            dataGridView_Exchange.Columns.Add(buttonColumn);
            dataGridView_Exchange.Columns[0].Width = 50;
            dataGridView_Exchange.Columns[1].Width = 150;
            dataGridView_Exchange.Columns[2].Width = 80;
            dataGridView_Exchange.Columns[3].Width = 150;
            dataGridView_Exchange.Columns[4].Width = 50;
            dataGridView_Exchange.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_Exchange.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        //初始化数据表
        public bool InitData(DataTable data)
        {
            if (data.Rows.Count <= 0)
            {
                MessageBox.Show("请先扫入要归还的工装！");
                return false;
            }
            if (Program.mw.textBox_toolsReturn_returnLine.Text.Length <= 0)
            {
                MessageBox.Show("请先输入所属线体！");
                return false;
            }
            if (Program.mw.textBox_toolsReturn_returner.Text.Length != 8)
            {
                MessageBox.Show("请先输入归还人工号！");
                return false;
            }
            if (Program.mw.textBox_toolsReturn_operator.Text.Length != 8)
            {
                MessageBox.Show("请先输入操作人工号！");
                return false;
            }
            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow newRow = dtExchange.NewRow();
                newRow[0] = (i + 1).ToString();
                newRow[1] = data.Rows[i][1].ToString();
                newRow[2] = data.Rows[i][2].ToString();
                dtExchange.Rows.Add(newRow);
            }
            dataGridView_Exchange.DataSource = dtExchange;
            dataGridView_Exchange.Height = (dataGridView_Exchange.Rows.Count + 1) * dataGridView_Exchange.RowTemplate.Height;
            button_exChange_Enter.Location = new Point(122, dataGridView_Exchange.Height + 65);
            button_exChange_Cancel.Location = new Point(309, dataGridView_Exchange.Height + 65);
            return true;
        }

        //工装编码文本框回车函数
        public void TextBox_Exchange_code_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                String codeNew = textBox_exChange_code.Text;
                if (codeNew.Length <= 0)
                {
                    return;
                }
                if (!CommonFunction.CheckCodeLegality(codeNew))
                {
                    MessageBox.Show("编码有误，请核对！");
                    return;
                }
                if (dtExchange.Rows.Count >= 100)
                {
                    MessageBox.Show("请勿超过100件/次！");
                    return;
                }
                if (count >= dtExchange.Rows.Count)
                {
                    MessageBox.Show("换新数量已足够，请勿再次扫码！");
                    return;
                }
                String sql = "select * from tools where code='" + codeNew + "' order by idTools DESC limit 1";  //看数据库里是否已有该工装信息
                DataSet ds = connection.Select(sql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    String materialNumberNew = ds.Tables[0].Rows[0][4].ToString();
                    //检查是否已添加过
                    foreach (DataRow row in dtExchange.Rows)
                    {
                        if (row[3].ToString().Equals(codeNew))
                        {
                            MessageBox.Show("该工装已添加，请勿重复扫码！");
                            return;
                        }
                    }
                    //核对工装存储状态
                    if (!ds.Tables[0].Rows[0][9].ToString().Contains("上架"))
                    {
                        MessageBox.Show("该工装" + ds.Tables[0].Rows[0][9].ToString() + "，无法出借！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //核对工装功能状态
                    if (!ds.Tables[0].Rows[0][15].ToString().Contains("正常"))
                    {
                        MessageBox.Show("该工装已" + ds.Tables[0].Rows[0][15].ToString() + "，无法出借！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //校验工装可用寿命
                    int lifeLeft = -1;
                    if (int.TryParse(ds.Tables[0].Rows[0][21].ToString(), out lifeLeft) && lifeLeft <= 0)
                    {
                        MessageBox.Show("该工装已达寿命上限，请勿出借！");
                        return;
                    }
                    //如果存在且未添加过，在下方datagridview中查找匹配的物料号
                    bool matched = false;
                    for (int i = 0; i < dtExchange.Rows.Count; i++)
                    {
                        if (dtExchange.Rows[i][2].ToString().Equals(materialNumberNew) && String.IsNullOrEmpty(dtExchange.Rows[i][3].ToString()))
                        {
                            dtExchange.Rows[i][3] = codeNew;
                            dataGridView_Exchange.Rows[i].DefaultCellStyle.BackColor = Color.GreenYellow;
                            textBox_exChange_code.Text = "";
                            matched = true;     //匹配到标识
                            count += 1;         //匹配计数加1
                            break;
                        }
                    }
                    //如果未匹配到物料号，或datagridview已填满，提示
                    if (!matched)
                    {
                        MessageBox.Show("归还工装无此物料号，请核对！");
                        return;
                    }
                    textBox_exChange_code.Focus();
                }
                else
                {
                    MessageBox.Show("无此工装信息，请核对！");
                }

            }
        }

        //以旧换新 确定按钮
        public void Button_Exchange_Enter_Click(object sender, EventArgs e)
        {
            ToolsIn toolsIn = new ToolsIn();
            //获取编码、领用人、操作人信息
            //检查领用人信息、检查操作人信息
            //更新工装数据库，添加记录数据库
            String borrower = Program.mw.textBox_toolsReturn_returner.Text;
            String borrowerName = Program.mw.textBox_toolsReturn_returnerName.Text;
            String borrowerContact = Program.mw.textBox_toolsReturn_returnerContact.Text;
            String borrowLine = Program.mw.textBox_toolsReturn_returnLine.Text;
            String operator1 = Program.mw.textBox_toolsReturn_operator.Text;
            String operatorName = Program.mw.textBox_toolsReturn_operatorName.Text;
            String operatorContact = Program.mw.textBox_toolsReturn_operatorContact.Text;
            //先执行归还入库按钮操作
            if (toolsIn.ToolsReturn_Enter() == true)
            {
                //校验各项数据
                if (borrower.Length != 8 || CommonFunction.HasChinese(borrower) || borrowerName.Length < 1)
                {
                    MessageBox.Show("请正确填写领用人信息！");
                    return;
                }
                if (operator1.Length != 8 || CommonFunction.HasChinese(operator1) || operatorName.Length < 1)
                {
                    MessageBox.Show("请正确填写操作人信息！");
                    return;
                }
                if (count < dtExchange.Rows.Count)
                {
                    DialogResult result = MessageBox.Show("换新工装数量少于归还工装，确认继续？", "数量确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                        return;
                }

                try
                {
                    foreach (DataRow row in dtExchange.Rows)
                    {
                        String code = row[3].ToString();
                        String[] temp = code.Split('-');
                        String category = temp[0];
                        String materialNumber = temp[1];
                        String number = temp[2];
                        String functionState = "";
                        //获取该工装功能状态
                        sql = "select * from tools where code='" + code + "' order by idTools DESC limit 1";
                        DataSet ds = connection.Select(sql);
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            functionState = ds.Tables[0].Rows[0][15].ToString();
                        }

                        //更新工装数据库
                        sql = @"update tools set storageState='已出借',line='" + borrowLine + "',borrower='" + borrower + "',operator='" + operator1
                        + "',lendDuration='0' where code='" + code + "';";
                        if (connection.Update(sql))
                        {
                            //添加记录数据库
                            sql = @"insert into records 
                                   (code,category,materialNumber,number,functionState,operationType,operationDate,operationTime,operator,operatorName,terminal,line,borrower,borrowerName) 
                            values (
                                     '" + code + "'," +
                                    "'" + category + "'," +
                                    "'" + materialNumber + "'," +
                                    "'" + number + "'," +
                                    "'" + functionState + "'," +
                                    "'" + "以旧换新出借" + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                    "'" + DateTime.Now.ToString("HH:mm:ss") + "'," +
                                    "'" + operator1 + "'," +
                                    "'" + operatorName + "'," +
                                    "'" + MainWindow.TerminalNumber + "'," +
                                    "'" + borrowLine + "'," +
                                    "'" + borrower + "'," +
                                    "'" + borrowerName + "');";
                            connection.Insert(sql);
                        }
                        else
                        {
                            MessageBox.Show("更新工装信息失败！");
                            return;
                        }
                    }

                    //更新人员信息库
                    CommonFunction.UpdatePersonalInfo(borrower, borrowerName, borrowerContact, borrowLine);
                    CommonFunction.UpdatePersonalInfo(operator1, operatorName, operatorContact);

                    MessageBox.Show("换新成功！");
                    this.Close();
                    toolsIn.ToolsReturn_CleanALL();
                    Program.mw.textBox_toolsReturn_code.Focus();
                    return;
                }
                catch
                {
                    MessageBox.Show("数据保存失败！");
                    return;
                }
            }
            else
            {
                MessageBox.Show("工装归还入库失败！");
                return;
            }

        }

        //以旧换新 取消按钮
        public void Button_Exchange_Cancel_Click(object sender, EventArgs e)
        {
            dtExchange.Rows.Clear();
            this.Close();
        }

        //dataGridView中删除行按钮的单击事件
        public void DataGridView_Exchange_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                dataGridView_Exchange.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                dtExchange.Rows[e.RowIndex][3] = "";
                dataGridView_Exchange.DataSource = dtExchange;
                count -= 1;
            }
        }
    }
}
