namespace Sniffer
{
    partial class MainFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnAddFilter = new System.Windows.Forms.Button();
            this.btnRemoveFilter = new System.Windows.Forms.Button();
            this.btnAddAllow = new System.Windows.Forms.Button();
            this.btnRemoveAllow = new System.Windows.Forms.Button();
            this.btnFilterAll = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.cklFilter = new System.Windows.Forms.CheckedListBox();
            this.cklAllow = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.70707F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.29293F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(745, 508);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnStart);
            this.flowLayoutPanel1.Controls.Add(this.btnStop);
            this.flowLayoutPanel1.Controls.Add(this.btnAddFilter);
            this.flowLayoutPanel1.Controls.Add(this.btnRemoveFilter);
            this.flowLayoutPanel1.Controls.Add(this.btnAddAllow);
            this.flowLayoutPanel1.Controls.Add(this.btnRemoveAllow);
            this.flowLayoutPanel1.Controls.Add(this.btnFilterAll);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(739, 48);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(3, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 43);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(84, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 43);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnAddFilter
            // 
            this.btnAddFilter.Location = new System.Drawing.Point(165, 3);
            this.btnAddFilter.Name = "btnAddFilter";
            this.btnAddFilter.Size = new System.Drawing.Size(75, 43);
            this.btnAddFilter.TabIndex = 2;
            this.btnAddFilter.Text = "添加过滤";
            this.btnAddFilter.UseVisualStyleBackColor = true;
            this.btnAddFilter.Click += new System.EventHandler(this.btnAddFilter_Click);
            // 
            // btnRemoveFilter
            // 
            this.btnRemoveFilter.Location = new System.Drawing.Point(246, 3);
            this.btnRemoveFilter.Name = "btnRemoveFilter";
            this.btnRemoveFilter.Size = new System.Drawing.Size(75, 43);
            this.btnRemoveFilter.TabIndex = 3;
            this.btnRemoveFilter.Text = "移除过滤";
            this.btnRemoveFilter.UseVisualStyleBackColor = true;
            this.btnRemoveFilter.Click += new System.EventHandler(this.btnRemoveFilter_Click);
            // 
            // btnAddAllow
            // 
            this.btnAddAllow.Location = new System.Drawing.Point(327, 3);
            this.btnAddAllow.Name = "btnAddAllow";
            this.btnAddAllow.Size = new System.Drawing.Size(75, 43);
            this.btnAddAllow.TabIndex = 4;
            this.btnAddAllow.Text = "添加例外";
            this.btnAddAllow.UseVisualStyleBackColor = true;
            this.btnAddAllow.Click += new System.EventHandler(this.btnAddAllow_Click);
            // 
            // btnRemoveAllow
            // 
            this.btnRemoveAllow.Location = new System.Drawing.Point(408, 3);
            this.btnRemoveAllow.Name = "btnRemoveAllow";
            this.btnRemoveAllow.Size = new System.Drawing.Size(75, 43);
            this.btnRemoveAllow.TabIndex = 5;
            this.btnRemoveAllow.Text = "移除例外";
            this.btnRemoveAllow.UseVisualStyleBackColor = true;
            this.btnRemoveAllow.Click += new System.EventHandler(this.btnRemoveAllow_Click);
            // 
            // btnFilterAll
            // 
            this.btnFilterAll.Location = new System.Drawing.Point(489, 3);
            this.btnFilterAll.Name = "btnFilterAll";
            this.btnFilterAll.Size = new System.Drawing.Size(75, 43);
            this.btnFilterAll.TabIndex = 6;
            this.btnFilterAll.Text = "过滤所有";
            this.btnFilterAll.UseVisualStyleBackColor = true;
            this.btnFilterAll.Click += new System.EventHandler(this.btnFilterAll_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.txtLog, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 57);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(739, 448);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(243, 3);
            this.txtLog.MaxLength = 0;
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(493, 442);
            this.txtLog.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.cklFilter, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.cklAllow, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(234, 442);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // cklFilter
            // 
            this.cklFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cklFilter.FormattingEnabled = true;
            this.cklFilter.Location = new System.Drawing.Point(3, 135);
            this.cklFilter.Name = "cklFilter";
            this.cklFilter.Size = new System.Drawing.Size(228, 292);
            this.cklFilter.TabIndex = 6;
            // 
            // cklAllow
            // 
            this.cklAllow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cklAllow.FormattingEnabled = true;
            this.cklAllow.Location = new System.Drawing.Point(3, 3);
            this.cklAllow.Name = "cklAllow";
            this.cklAllow.Size = new System.Drawing.Size(228, 116);
            this.cklAllow.TabIndex = 7;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 508);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "网络包嗅探";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnAddFilter;
        private System.Windows.Forms.Button btnRemoveFilter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.CheckedListBox cklFilter;
        private System.Windows.Forms.Button btnAddAllow;
        private System.Windows.Forms.Button btnRemoveAllow;
        private System.Windows.Forms.CheckedListBox cklAllow;
        private System.Windows.Forms.Button btnFilterAll;
    }
}

