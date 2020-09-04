using System;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace nsStockManage
{
    public class MyBindingNavigator
    {
        private BindingNavigator bindingNavigator = null;
        private DataGridView dataGridView = null;
        private BindingSource bindingSource = new BindingSource();
        private DataTable dt = null;

        private int pageSize = 30;          //每页显示行数
        private int maxCount = 0;           //总记录数
        private int currentCount = 0;       //当前页第一行号
        private int maxPage = 0;            //总共有多少页＝总记录数/每页显示行数
        private int currentPage = 0;        //当前页号
        private int nStart = 0;             //当前页面开始行号
        private int nEnd = 0;               //当前页面结束行号

        public void InitDataTable(BindingNavigator bindingNavigator, DataGridView bindingDataGridView, String strData)
        {
            this.bindingNavigator = bindingNavigator;
            this.dataGridView = bindingDataGridView;

            String sql = "SELECT * FROM "+ strData + ";";
            DataSet ds = MainWindow.connection.Select(sql);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];

                maxCount = dt.Rows.Count;           //总数据条数
                maxPage = (maxCount / pageSize);    //计算出总页数
                if ((maxCount % pageSize) > 0) maxPage++;
                currentPage = 1;        //当前页数从1开始
                currentCount = 0;       //当前记录数从0开始

                LoadData();
            }
        }
        public void InitDataTable(BindingNavigator bindingNavigator, DataGridView bindingDataGridView, DataSet dataSet)
        {
            this.bindingNavigator = bindingNavigator;
            this.dataGridView = bindingDataGridView;

            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                dt = dataSet.Tables[0];

                maxCount = dt.Rows.Count;           //总数据条数
                maxPage = (maxCount / pageSize);    //计算出总页数
                if ((maxCount % pageSize) > 0) maxPage++;
                currentPage = 1;        //当前页数从1开始
                currentCount = 0;       //当前记录数从0开始

                LoadData();
            }
        }

        private void LoadData()
        {
            nStart = pageSize * (currentPage - 1) + 1;
            if (currentPage == maxPage)
                nEnd = maxCount;
            else
                nEnd = nStart + pageSize - 1;

            DataTable dtTemp = dt.Clone();   //克隆DataTable结构框架

            //从元数据源复制记录行
            for (int i = nStart; i <= nEnd; i++)
            {
                dtTemp.ImportRow(dt.Rows[i-1]);
            }
            bindingSource.DataSource = dtTemp;
            bindingNavigator.BindingSource = bindingSource;
            dataGridView.DataSource = bindingSource;
            /*
            for (int i = 0; i < headerString.Length; i++)   //重制表头
            {
                dataGridView.Columns[i].HeaderCell = new DataGridViewAutoFilterColumnHeaderCell();
                dataGridView.Columns[i].HeaderText = headerString[i];
            }*/

            bindingNavigator.Items[3].Text = currentPage.ToString();
            bindingNavigator.Items[4].Text = "/ " + maxPage.ToString();
            bindingNavigator.Items[11].Text = "总数：" + maxCount.ToString() + "条";
        }
        
        //各按钮单击函数
        public void BindingNavigator_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if(e.ClickedItem.Text == "第一页")
            {
                currentPage = 1;
                currentCount = 1;
                LoadData();
            }
            if (e.ClickedItem.Text == "上一页")
            {
                if (currentPage > 1)
                {
                    currentPage--;
                    currentCount = pageSize * (currentPage - 1);
                }
                LoadData();
            }
            if (e.ClickedItem.Text == "下一页")
            {
                if (currentPage < maxPage)
                {
                    currentPage++;
                    currentCount = pageSize * (currentPage - 1);
                }
                LoadData();
            }
            if (e.ClickedItem.Text == "最后页")
            {
                currentPage = maxPage;
                currentCount = pageSize * (currentPage - 1);
                LoadData();
            }
        }

        //选择每页显示多少条数据
        public void BindingNavigator_ComboBox_TextChanged(object sender, EventArgs e)
        {
            ToolStripComboBox comboBox = sender as ToolStripComboBox;
            pageSize = int.Parse(comboBox.Text);
            currentPage = 1;
            maxPage = (maxCount / pageSize);    //重新计算总页数
            if ((maxCount % pageSize) > 0) maxPage++;
            LoadData();
        }
        
        //回车键跳转至指定页码
        public void BindingNavigator_JumpToPage(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            ToolStripTextBox textBox = sender as ToolStripTextBox;
            string pattern = @"^\d+$";
            string input = textBox.Text;
            Regex regex = new Regex(pattern);
            //判断是否匹配成功
            bool res = regex.IsMatch(input);
            if (!res) return;
            currentPage = int.Parse(input);

            if (currentPage > maxPage || currentPage == 0) return;
            currentCount = pageSize * (currentPage - 1);
            LoadData();
        }
        
    }
}
