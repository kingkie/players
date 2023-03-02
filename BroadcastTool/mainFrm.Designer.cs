
namespace BroadcastTool
{
    partial class mainFrm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainFrm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpgHome = new System.Windows.Forms.TabPage();
            this.tblSetting = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nuLogicChl = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.chkAllDevs = new System.Windows.Forms.CheckBox();
            this.nuDevChl = new System.Windows.Forms.NumericUpDown();
            this.cboBraudRate = new System.Windows.Forms.ComboBox();
            this.txtIdFrom = new System.Windows.Forms.TextBox();
            this.nuGasTotalCount = new System.Windows.Forms.NumericUpDown();
            this.cboDevType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCircuitCnt = new System.Windows.Forms.Label();
            this.lblDevType = new System.Windows.Forms.Label();
            this.pnlCmd = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.tpgOther = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtVideo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSet = new System.Windows.Forms.Button();
            this.txtDevSn = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.nuAdjustStep = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tpgHome.SuspendLayout();
            this.tblSetting.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuLogicChl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuDevChl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuGasTotalCount)).BeginInit();
            this.pnlCmd.SuspendLayout();
            this.tpgOther.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuAdjustStep)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpgHome);
            this.tabControl1.Controls.Add(this.tpgOther);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(72, 24);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(551, 361);
            this.tabControl1.TabIndex = 0;
            // 
            // tpgHome
            // 
            this.tpgHome.Controls.Add(this.tblSetting);
            this.tpgHome.Location = new System.Drawing.Point(4, 28);
            this.tpgHome.Name = "tpgHome";
            this.tpgHome.Padding = new System.Windows.Forms.Padding(3);
            this.tpgHome.Size = new System.Drawing.Size(543, 329);
            this.tpgHome.TabIndex = 0;
            this.tpgHome.Text = "常用设置";
            this.tpgHome.UseVisualStyleBackColor = true;
            // 
            // tblSetting
            // 
            this.tblSetting.ColumnCount = 1;
            this.tblSetting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSetting.Controls.Add(this.groupBox1, 0, 0);
            this.tblSetting.Controls.Add(this.pnlCmd, 0, 1);
            this.tblSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblSetting.Location = new System.Drawing.Point(3, 3);
            this.tblSetting.Name = "tblSetting";
            this.tblSetting.RowCount = 2;
            this.tblSetting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSetting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tblSetting.Size = new System.Drawing.Size(537, 323);
            this.tblSetting.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nuAdjustStep);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.nuLogicChl);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.chkAllDevs);
            this.groupBox1.Controls.Add(this.nuDevChl);
            this.groupBox1.Controls.Add(this.cboBraudRate);
            this.groupBox1.Controls.Add(this.txtIdFrom);
            this.groupBox1.Controls.Add(this.nuGasTotalCount);
            this.groupBox1.Controls.Add(this.cboDevType);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblCircuitCnt);
            this.groupBox1.Controls.Add(this.lblDevType);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(531, 245);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // nuLogicChl
            // 
            this.nuLogicChl.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nuLogicChl.Location = new System.Drawing.Point(378, 138);
            this.nuLogicChl.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nuLogicChl.Name = "nuLogicChl";
            this.nuLogicChl.Size = new System.Drawing.Size(120, 23);
            this.nuLogicChl.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(303, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 14);
            this.label4.TabIndex = 11;
            this.label4.Text = "逻辑信道：";
            // 
            // chkAllDevs
            // 
            this.chkAllDevs.AutoSize = true;
            this.chkAllDevs.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkAllDevs.Location = new System.Drawing.Point(377, 36);
            this.chkAllDevs.Name = "chkAllDevs";
            this.chkAllDevs.Size = new System.Drawing.Size(82, 18);
            this.chkAllDevs.TabIndex = 10;
            this.chkAllDevs.Text = "全部设备";
            this.chkAllDevs.UseVisualStyleBackColor = true;
            // 
            // nuDevChl
            // 
            this.nuDevChl.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nuDevChl.Location = new System.Drawing.Point(378, 83);
            this.nuDevChl.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nuDevChl.Name = "nuDevChl";
            this.nuDevChl.Size = new System.Drawing.Size(120, 23);
            this.nuDevChl.TabIndex = 9;
            // 
            // cboBraudRate
            // 
            this.cboBraudRate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboBraudRate.FormattingEnabled = true;
            this.cboBraudRate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "115200"});
            this.cboBraudRate.Location = new System.Drawing.Point(121, 196);
            this.cboBraudRate.Name = "cboBraudRate";
            this.cboBraudRate.Size = new System.Drawing.Size(121, 22);
            this.cboBraudRate.TabIndex = 8;
            // 
            // txtIdFrom
            // 
            this.txtIdFrom.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIdFrom.Location = new System.Drawing.Point(121, 138);
            this.txtIdFrom.Name = "txtIdFrom";
            this.txtIdFrom.Size = new System.Drawing.Size(120, 23);
            this.txtIdFrom.TabIndex = 7;
            // 
            // nuGasTotalCount
            // 
            this.nuGasTotalCount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nuGasTotalCount.Location = new System.Drawing.Point(121, 83);
            this.nuGasTotalCount.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nuGasTotalCount.Name = "nuGasTotalCount";
            this.nuGasTotalCount.Size = new System.Drawing.Size(120, 23);
            this.nuGasTotalCount.TabIndex = 3;
            // 
            // cboDevType
            // 
            this.cboDevType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboDevType.FormattingEnabled = true;
            this.cboDevType.Items.AddRange(new object[] {
            "遥控器433模块",
            "车载模块"});
            this.cboDevType.Location = new System.Drawing.Point(121, 34);
            this.cboDevType.Name = "cboDevType";
            this.cboDevType.Size = new System.Drawing.Size(120, 22);
            this.cboDevType.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(303, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "物理信道：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(33, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "设备波特率：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(19, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "气路开始编号：";
            // 
            // lblCircuitCnt
            // 
            this.lblCircuitCnt.AutoSize = true;
            this.lblCircuitCnt.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCircuitCnt.Location = new System.Drawing.Point(33, 87);
            this.lblCircuitCnt.Name = "lblCircuitCnt";
            this.lblCircuitCnt.Size = new System.Drawing.Size(91, 14);
            this.lblCircuitCnt.TabIndex = 1;
            this.lblCircuitCnt.Text = "气路总通道：";
            // 
            // lblDevType
            // 
            this.lblDevType.AutoSize = true;
            this.lblDevType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDevType.Location = new System.Drawing.Point(47, 37);
            this.lblDevType.Name = "lblDevType";
            this.lblDevType.Size = new System.Drawing.Size(77, 14);
            this.lblDevType.TabIndex = 0;
            this.lblDevType.Text = "设备类型：";
            // 
            // pnlCmd
            // 
            this.pnlCmd.Controls.Add(this.btnOK);
            this.pnlCmd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCmd.Location = new System.Drawing.Point(3, 254);
            this.pnlCmd.Name = "pnlCmd";
            this.pnlCmd.Size = new System.Drawing.Size(531, 66);
            this.pnlCmd.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(403, 19);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(95, 32);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "设  置";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tpgOther
            // 
            this.tpgOther.Controls.Add(this.groupBox2);
            this.tpgOther.Controls.Add(this.panel1);
            this.tpgOther.Location = new System.Drawing.Point(4, 28);
            this.tpgOther.Name = "tpgOther";
            this.tpgOther.Padding = new System.Windows.Forms.Padding(3);
            this.tpgOther.Size = new System.Drawing.Size(543, 329);
            this.tpgOther.TabIndex = 1;
            this.tpgOther.Text = "其它设置";
            this.tpgOther.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDevSn);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtVideo);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(537, 251);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // txtVideo
            // 
            this.txtVideo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtVideo.Location = new System.Drawing.Point(125, 32);
            this.txtVideo.Name = "txtVideo";
            this.txtVideo.Size = new System.Drawing.Size(339, 23);
            this.txtVideo.TabIndex = 9;
            this.txtVideo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtVideo_MouseUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(23, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 14);
            this.label5.TabIndex = 8;
            this.label5.Text = "最后播放视频：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSet);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 254);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(537, 72);
            this.panel1.TabIndex = 0;
            // 
            // btnSet
            // 
            this.btnSet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSet.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSet.Location = new System.Drawing.Point(406, 22);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(95, 32);
            this.btnSet.TabIndex = 1;
            this.btnSet.Text = "设  置";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // txtDevSn
            // 
            this.txtDevSn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDevSn.Location = new System.Drawing.Point(125, 79);
            this.txtDevSn.Name = "txtDevSn";
            this.txtDevSn.Size = new System.Drawing.Size(339, 23);
            this.txtDevSn.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(23, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 14);
            this.label6.TabIndex = 10;
            this.label6.Text = "当前设备编号：";
            // 
            // nuAdjustStep
            // 
            this.nuAdjustStep.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nuAdjustStep.Location = new System.Drawing.Point(377, 198);
            this.nuAdjustStep.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nuAdjustStep.Name = "nuAdjustStep";
            this.nuAdjustStep.Size = new System.Drawing.Size(120, 23);
            this.nuAdjustStep.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(302, 202);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 14);
            this.label7.TabIndex = 13;
            this.label7.Text = "快进秒数：";
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 361);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配置工具";
            this.Load += new System.EventHandler(this.mainFrm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpgHome.ResumeLayout(false);
            this.tblSetting.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuLogicChl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuDevChl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuGasTotalCount)).EndInit();
            this.pnlCmd.ResumeLayout(false);
            this.tpgOther.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nuAdjustStep)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpgHome;
        private System.Windows.Forms.TabPage tpgOther;
        private System.Windows.Forms.TableLayoutPanel tblSetting;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblDevType;
        private System.Windows.Forms.Label lblCircuitCnt;
        private System.Windows.Forms.ComboBox cboDevType;
        private System.Windows.Forms.NumericUpDown nuGasTotalCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboBraudRate;
        private System.Windows.Forms.TextBox txtIdFrom;
        private System.Windows.Forms.NumericUpDown nuDevChl;
        private System.Windows.Forms.Panel pnlCmd;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkAllDevs;
        private System.Windows.Forms.NumericUpDown nuLogicChl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.TextBox txtVideo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDevSn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nuAdjustStep;
        private System.Windows.Forms.Label label7;
    }
}

