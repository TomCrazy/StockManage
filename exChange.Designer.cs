namespace nsStockManage
{
    partial class ExChange
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_exChange_code = new System.Windows.Forms.TextBox();
            this.label_exChange_code = new System.Windows.Forms.Label();
            this.button_exChange_Enter = new System.Windows.Forms.Button();
            this.button_exChange_Cancel = new System.Windows.Forms.Button();
            this.dataGridView_Exchange = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Exchange)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_exChange_code
            // 
            this.textBox_exChange_code.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.textBox_exChange_code.Location = new System.Drawing.Point(210, 17);
            this.textBox_exChange_code.Name = "textBox_exChange_code";
            this.textBox_exChange_code.Size = new System.Drawing.Size(176, 23);
            this.textBox_exChange_code.TabIndex = 0;
            this.textBox_exChange_code.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Exchange_code_KeyPress);
            // 
            // label_exChange_code
            // 
            this.label_exChange_code.AutoSize = true;
            this.label_exChange_code.BackColor = System.Drawing.Color.Transparent;
            this.label_exChange_code.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label_exChange_code.Location = new System.Drawing.Point(110, 19);
            this.label_exChange_code.Name = "label_exChange_code";
            this.label_exChange_code.Size = new System.Drawing.Size(104, 17);
            this.label_exChange_code.TabIndex = 1;
            this.label_exChange_code.Text = "请扫入换新工装：";
            // 
            // button_exChange_Enter
            // 
            this.button_exChange_Enter.BackColor = System.Drawing.Color.Lime;
            this.button_exChange_Enter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_exChange_Enter.Location = new System.Drawing.Point(122, 216);
            this.button_exChange_Enter.Name = "button_exChange_Enter";
            this.button_exChange_Enter.Size = new System.Drawing.Size(75, 23);
            this.button_exChange_Enter.TabIndex = 2;
            this.button_exChange_Enter.Text = "确定";
            this.button_exChange_Enter.UseVisualStyleBackColor = false;
            this.button_exChange_Enter.Click += new System.EventHandler(this.Button_Exchange_Enter_Click);
            // 
            // button_exChange_Cancel
            // 
            this.button_exChange_Cancel.BackColor = System.Drawing.Color.LightCoral;
            this.button_exChange_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_exChange_Cancel.Location = new System.Drawing.Point(309, 216);
            this.button_exChange_Cancel.Name = "button_exChange_Cancel";
            this.button_exChange_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_exChange_Cancel.TabIndex = 2;
            this.button_exChange_Cancel.Text = "取消";
            this.button_exChange_Cancel.UseVisualStyleBackColor = false;
            this.button_exChange_Cancel.Click += new System.EventHandler(this.Button_Exchange_Cancel_Click);
            // 
            // dataGridView_Exchange
            // 
            this.dataGridView_Exchange.AllowUserToAddRows = false;
            this.dataGridView_Exchange.AllowUserToDeleteRows = false;
            this.dataGridView_Exchange.AllowUserToResizeColumns = false;
            this.dataGridView_Exchange.AllowUserToResizeRows = false;
            this.dataGridView_Exchange.ColumnHeadersHeight = 25;
            this.dataGridView_Exchange.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView_Exchange.Location = new System.Drawing.Point(20, 52);
            this.dataGridView_Exchange.Name = "dataGridView_Exchange";
            this.dataGridView_Exchange.ReadOnly = true;
            this.dataGridView_Exchange.RowHeadersVisible = false;
            this.dataGridView_Exchange.RowHeadersWidth = 10;
            this.dataGridView_Exchange.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView_Exchange.RowTemplate.Height = 23;
            this.dataGridView_Exchange.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView_Exchange.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView_Exchange.Size = new System.Drawing.Size(480, 150);
            this.dataGridView_Exchange.TabIndex = 3;
            this.dataGridView_Exchange.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_Exchange_CellContentClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "归还工装";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 160;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "物料号";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "换新工装";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 160;
            // 
            // ExChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(534, 461);
            this.Controls.Add(this.dataGridView_Exchange);
            this.Controls.Add(this.button_exChange_Cancel);
            this.Controls.Add(this.button_exChange_Enter);
            this.Controls.Add(this.textBox_exChange_code);
            this.Controls.Add(this.label_exChange_code);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExChange";
            this.ShowInTaskbar = false;
            this.Text = "以旧换新";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Exchange)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox_exChange_code;
        private System.Windows.Forms.Label label_exChange_code;
        private System.Windows.Forms.Button button_exChange_Cancel;
        private System.Windows.Forms.DataGridView dataGridView_Exchange;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Button button_exChange_Enter;
    }
}