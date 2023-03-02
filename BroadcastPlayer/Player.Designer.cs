namespace BroadcastPlayer
{
    partial class Player
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Player));
            this.TOPmenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Connect_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoConnect_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseSerialPort_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChooseDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.Connect_ADP_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisConnect_ADP_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Reset_config_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScriptTimer = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.g_Min_Btn = new System.Windows.Forms.Button();
            this.g_Max_Btn = new System.Windows.Forms.Button();
            this.g_Close_Btn = new System.Windows.Forms.Button();
            this.ICON_PB = new System.Windows.Forms.PictureBox();
            this.Title_Label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CurrentDeviceLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ErrorMSG = new System.Windows.Forms.Label();
            this.WakeUpBTN = new System.Windows.Forms.Button();
            this.CycleCB = new System.Windows.Forms.CheckBox();
            this.ShowMC_CB = new System.Windows.Forms.CheckBox();
            this.FastBackward_BTN = new System.Windows.Forms.Button();
            this.FastForward_BTN = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtCycTimes = new System.Windows.Forms.TextBox();
            this.lblCycTimes = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.WMP = new AxWMPLib.AxWindowsMediaPlayer();
            this.Server_config_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TOPmenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ICON_PB)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WMP)).BeginInit();
            this.SuspendLayout();
            // 
            // TOPmenuStrip1
            // 
            this.TOPmenuStrip1.BackColor = System.Drawing.Color.White;
            this.TOPmenuStrip1.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.TOPmenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Connect_ToolStripMenuItem});
            this.TOPmenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.TOPmenuStrip1.Name = "TOPmenuStrip1";
            this.TOPmenuStrip1.Padding = new System.Windows.Forms.Padding(6, 2, 121, 2);
            this.TOPmenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TOPmenuStrip1.Size = new System.Drawing.Size(900, 39);
            this.TOPmenuStrip1.TabIndex = 1;
            this.TOPmenuStrip1.Text = "menuStrip1";
            this.TOPmenuStrip1.Paint += new System.Windows.Forms.PaintEventHandler(this.TOPmenuStrip1_Paint);
            // 
            // Connect_ToolStripMenuItem
            // 
            this.Connect_ToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.Connect_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AutoConnect_ToolStripMenuItem,
            this.CloseSerialPort_ToolStripMenuItem,
            this.ChooseDevice,
            this.Connect_ADP_ToolStripMenuItem,
            this.DisConnect_ADP_ToolStripMenuItem,
            this.Reset_config_ToolStripMenuItem,
            this.Server_config_ToolStripMenuItem});
            this.Connect_ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.Connect_ToolStripMenuItem.Name = "Connect_ToolStripMenuItem";
            this.Connect_ToolStripMenuItem.Size = new System.Drawing.Size(33, 35);
            this.Connect_ToolStripMenuItem.Text = " ";
            this.Connect_ToolStripMenuItem.Click += new System.EventHandler(this.Connect_ToolStripMenuItem_Click);
            this.Connect_ToolStripMenuItem.Paint += new System.Windows.Forms.PaintEventHandler(this.Connect_ToolStripMenuItem_Paint);
            // 
            // AutoConnect_ToolStripMenuItem
            // 
            this.AutoConnect_ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.AutoConnect_ToolStripMenuItem.Name = "AutoConnect_ToolStripMenuItem";
            this.AutoConnect_ToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.AutoConnect_ToolStripMenuItem.Text = "连遥控器";
            this.AutoConnect_ToolStripMenuItem.Click += new System.EventHandler(this.AutoConnect_ToolStripMenuItem_Click);
            // 
            // CloseSerialPort_ToolStripMenuItem
            // 
            this.CloseSerialPort_ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.CloseSerialPort_ToolStripMenuItem.Name = "CloseSerialPort_ToolStripMenuItem";
            this.CloseSerialPort_ToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.CloseSerialPort_ToolStripMenuItem.Text = "断开连接";
            this.CloseSerialPort_ToolStripMenuItem.Click += new System.EventHandler(this.cloasePort_Click);
            // 
            // ChooseDevice
            // 
            this.ChooseDevice.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ChooseDevice.Name = "ChooseDevice";
            this.ChooseDevice.Size = new System.Drawing.Size(180, 26);
            this.ChooseDevice.Text = "选择设备";
            this.ChooseDevice.Click += new System.EventHandler(this.ChooseDeviceToolStripMenuItem_Click);
            // 
            // Connect_ADP_ToolStripMenuItem
            // 
            this.Connect_ADP_ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Connect_ADP_ToolStripMenuItem.Name = "Connect_ADP_ToolStripMenuItem";
            this.Connect_ADP_ToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.Connect_ADP_ToolStripMenuItem.Text = "连接设备";
            this.Connect_ADP_ToolStripMenuItem.Click += new System.EventHandler(this.Connect_ADP_ToolStripMenuItem_Click);
            // 
            // DisConnect_ADP_ToolStripMenuItem
            // 
            this.DisConnect_ADP_ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.DisConnect_ADP_ToolStripMenuItem.Name = "DisConnect_ADP_ToolStripMenuItem";
            this.DisConnect_ADP_ToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.DisConnect_ADP_ToolStripMenuItem.Text = "断开连接";
            this.DisConnect_ADP_ToolStripMenuItem.Click += new System.EventHandler(this.DisConnect_ADP_ToolStripMenuItem_Click);
            // 
            // Reset_config_ToolStripMenuItem
            // 
            this.Reset_config_ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Reset_config_ToolStripMenuItem.Name = "Reset_config_ToolStripMenuItem";
            this.Reset_config_ToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.Reset_config_ToolStripMenuItem.Text = "清除配置";
            this.Reset_config_ToolStripMenuItem.Click += new System.EventHandler(this.Reset_config_ToolStripMenuItem_Click);
            // 
            // ScriptTimer
            // 
            this.ScriptTimer.Interval = 40;
            this.ScriptTimer.Tick += new System.EventHandler(this.ScriptTimer_Tick);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(543, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 43);
            this.button1.TabIndex = 2;
            this.button1.Text = "打开";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OpenFile_ToolStripMenuItem1_Click);
            // 
            // g_Min_Btn
            // 
            this.g_Min_Btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.g_Min_Btn.FlatAppearance.BorderSize = 0;
            this.g_Min_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.g_Min_Btn.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.g_Min_Btn.Location = new System.Drawing.Point(779, 2);
            this.g_Min_Btn.Name = "g_Min_Btn";
            this.g_Min_Btn.Size = new System.Drawing.Size(40, 38);
            this.g_Min_Btn.TabIndex = 3;
            this.g_Min_Btn.Text = "一";
            this.g_Min_Btn.UseVisualStyleBackColor = true;
            this.g_Min_Btn.Click += new System.EventHandler(this.g_Min_Btn_Click);
            // 
            // g_Max_Btn
            // 
            this.g_Max_Btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.g_Max_Btn.FlatAppearance.BorderSize = 0;
            this.g_Max_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.g_Max_Btn.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.g_Max_Btn.Location = new System.Drawing.Point(819, 2);
            this.g_Max_Btn.Name = "g_Max_Btn";
            this.g_Max_Btn.Size = new System.Drawing.Size(40, 38);
            this.g_Max_Btn.TabIndex = 4;
            this.g_Max_Btn.Text = "□";
            this.g_Max_Btn.UseVisualStyleBackColor = true;
            this.g_Max_Btn.Click += new System.EventHandler(this.g_Max_Btn_Click);
            // 
            // g_Close_Btn
            // 
            this.g_Close_Btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.g_Close_Btn.FlatAppearance.BorderSize = 0;
            this.g_Close_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.g_Close_Btn.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.g_Close_Btn.Location = new System.Drawing.Point(859, 2);
            this.g_Close_Btn.Name = "g_Close_Btn";
            this.g_Close_Btn.Size = new System.Drawing.Size(40, 38);
            this.g_Close_Btn.TabIndex = 5;
            this.g_Close_Btn.Text = "×";
            this.g_Close_Btn.UseVisualStyleBackColor = true;
            this.g_Close_Btn.Click += new System.EventHandler(this.g_Close_Btn_Click);
            // 
            // ICON_PB
            // 
            this.ICON_PB.BackColor = System.Drawing.Color.White;
            this.ICON_PB.Image = ((System.Drawing.Image)(resources.GetObject("ICON_PB.Image")));
            this.ICON_PB.Location = new System.Drawing.Point(9, 4);
            this.ICON_PB.Name = "ICON_PB";
            this.ICON_PB.Size = new System.Drawing.Size(30, 30);
            this.ICON_PB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ICON_PB.TabIndex = 7;
            this.ICON_PB.TabStop = false;
            // 
            // Title_Label
            // 
            this.Title_Label.AutoSize = true;
            this.Title_Label.BackColor = System.Drawing.Color.White;
            this.Title_Label.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Title_Label.Location = new System.Drawing.Point(46, 13);
            this.Title_Label.Name = "Title_Label";
            this.Title_Label.Size = new System.Drawing.Size(150, 15);
            this.Title_Label.TabIndex = 8;
            this.Title_Label.Text = "气味播放器-v2.1.1";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DarkGray;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 45);
            this.label2.TabIndex = 9;
            this.label2.Text = "单路控制";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CurrentDeviceLabel
            // 
            this.CurrentDeviceLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CurrentDeviceLabel.BackColor = System.Drawing.Color.LightGray;
            this.CurrentDeviceLabel.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CurrentDeviceLabel.ForeColor = System.Drawing.Color.DimGray;
            this.CurrentDeviceLabel.Location = new System.Drawing.Point(248, 7);
            this.CurrentDeviceLabel.Name = "CurrentDeviceLabel";
            this.CurrentDeviceLabel.Size = new System.Drawing.Size(381, 25);
            this.CurrentDeviceLabel.TabIndex = 17;
            this.CurrentDeviceLabel.Text = "当前设备:所有设备";
            this.CurrentDeviceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(707, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(193, 561);
            this.panel1.TabIndex = 18;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Location = new System.Drawing.Point(0, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(193, 481);
            this.panel2.TabIndex = 14;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(67, 53);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(77, 21);
            this.numericUpDown1.TabIndex = 13;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(152, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "S";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "播放时长";
            // 
            // ErrorMSG
            // 
            this.ErrorMSG.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ErrorMSG.AutoSize = true;
            this.ErrorMSG.ForeColor = System.Drawing.Color.Red;
            this.ErrorMSG.Location = new System.Drawing.Point(329, 27);
            this.ErrorMSG.Name = "ErrorMSG";
            this.ErrorMSG.Size = new System.Drawing.Size(53, 12);
            this.ErrorMSG.TabIndex = 19;
            this.ErrorMSG.Text = "ErrorMSG";
            // 
            // WakeUpBTN
            // 
            this.WakeUpBTN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WakeUpBTN.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.WakeUpBTN.Location = new System.Drawing.Point(623, 12);
            this.WakeUpBTN.Name = "WakeUpBTN";
            this.WakeUpBTN.Size = new System.Drawing.Size(75, 43);
            this.WakeUpBTN.TabIndex = 20;
            this.WakeUpBTN.Text = "唤醒";
            this.WakeUpBTN.UseVisualStyleBackColor = true;
            this.WakeUpBTN.Click += new System.EventHandler(this.WakeUpBTN_Click);
            // 
            // CycleCB
            // 
            this.CycleCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CycleCB.AutoSize = true;
            this.CycleCB.Location = new System.Drawing.Point(492, 25);
            this.CycleCB.Name = "CycleCB";
            this.CycleCB.Size = new System.Drawing.Size(48, 16);
            this.CycleCB.TabIndex = 21;
            this.CycleCB.Text = "循环";
            this.CycleCB.UseVisualStyleBackColor = true;
            this.CycleCB.CheckedChanged += new System.EventHandler(this.CycleCB_CheckedChanged);
            // 
            // ShowMC_CB
            // 
            this.ShowMC_CB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ShowMC_CB.AutoSize = true;
            this.ShowMC_CB.Location = new System.Drawing.Point(10, 25);
            this.ShowMC_CB.Name = "ShowMC_CB";
            this.ShowMC_CB.Size = new System.Drawing.Size(108, 16);
            this.ShowMC_CB.TabIndex = 22;
            this.ShowMC_CB.Text = "显示视频菜单栏";
            this.ShowMC_CB.UseVisualStyleBackColor = true;
            this.ShowMC_CB.CheckedChanged += new System.EventHandler(this.ShowMC_CB_CheckedChanged);
            // 
            // FastBackward_BTN
            // 
            this.FastBackward_BTN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FastBackward_BTN.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FastBackward_BTN.Location = new System.Drawing.Point(120, 12);
            this.FastBackward_BTN.Name = "FastBackward_BTN";
            this.FastBackward_BTN.Size = new System.Drawing.Size(79, 43);
            this.FastBackward_BTN.TabIndex = 23;
            this.FastBackward_BTN.Text = "快退";
            this.FastBackward_BTN.UseVisualStyleBackColor = true;
            this.FastBackward_BTN.Click += new System.EventHandler(this.FastBackward_BTN_Click);
            // 
            // FastForward_BTN
            // 
            this.FastForward_BTN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FastForward_BTN.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FastForward_BTN.Location = new System.Drawing.Point(214, 12);
            this.FastForward_BTN.Name = "FastForward_BTN";
            this.FastForward_BTN.Size = new System.Drawing.Size(80, 43);
            this.FastForward_BTN.TabIndex = 24;
            this.FastForward_BTN.Text = "快进";
            this.FastForward_BTN.UseVisualStyleBackColor = true;
            this.FastForward_BTN.Click += new System.EventHandler(this.FastForward_BTN_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtCycTimes);
            this.panel3.Controls.Add(this.lblCycTimes);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.FastForward_BTN);
            this.panel3.Controls.Add(this.ErrorMSG);
            this.panel3.Controls.Add(this.FastBackward_BTN);
            this.panel3.Controls.Add(this.WakeUpBTN);
            this.panel3.Controls.Add(this.ShowMC_CB);
            this.panel3.Controls.Add(this.CycleCB);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 534);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(707, 66);
            this.panel3.TabIndex = 25;
            // 
            // txtCycTimes
            // 
            this.txtCycTimes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCycTimes.Location = new System.Drawing.Point(448, 23);
            this.txtCycTimes.Name = "txtCycTimes";
            this.txtCycTimes.Size = new System.Drawing.Size(39, 21);
            this.txtCycTimes.TabIndex = 25;
            this.txtCycTimes.Text = "1";
            // 
            // lblCycTimes
            // 
            this.lblCycTimes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCycTimes.AutoSize = true;
            this.lblCycTimes.Location = new System.Drawing.Point(389, 27);
            this.lblCycTimes.Name = "lblCycTimes";
            this.lblCycTimes.Size = new System.Drawing.Size(59, 12);
            this.lblCycTimes.TabIndex = 26;
            this.lblCycTimes.Text = "循环次数:";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.Controls.Add(this.WMP);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 39);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(707, 495);
            this.panel4.TabIndex = 26;
            // 
            // WMP
            // 
            this.WMP.AllowDrop = true;
            this.WMP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WMP.Enabled = true;
            this.WMP.Location = new System.Drawing.Point(0, 0);
            this.WMP.Name = "WMP";
            this.WMP.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("WMP.OcxState")));
            this.WMP.Padding = new System.Windows.Forms.Padding(0, 0, 179, 0);
            this.WMP.Size = new System.Drawing.Size(707, 495);
            this.WMP.TabIndex = 0;
            this.WMP.StatusChange += new System.EventHandler(this.WMP_StatusChange);
            this.WMP.PositionChange += new AxWMPLib._WMPOCXEvents_PositionChangeEventHandler(this.WMP_PositionChange);
            this.WMP.ClickEvent += new AxWMPLib._WMPOCXEvents_ClickEventHandler(this.WMP_ClickEvent);
            this.WMP.ClientSizeChanged += new System.EventHandler(this.WMP_ClientSizeChanged);
            // 
            // Server_config_ToolStripMenuItem
            // 
            this.Server_config_ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Server_config_ToolStripMenuItem.Name = "Server_config_ToolStripMenuItem";
            this.Server_config_ToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.Server_config_ToolStripMenuItem.Text = "服务配置";
            this.Server_config_ToolStripMenuItem.Click += new System.EventHandler(this.Server_config_ToolStripMenuItem_Click);
            // 
            // Player
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.CurrentDeviceLabel);
            this.Controls.Add(this.Title_Label);
            this.Controls.Add(this.ICON_PB);
            this.Controls.Add(this.g_Close_Btn);
            this.Controls.Add(this.g_Max_Btn);
            this.Controls.Add(this.g_Min_Btn);
            this.Controls.Add(this.TOPmenuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.TOPmenuStrip1;
            this.Name = "Player";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "气味播放器";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Player_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Player_DragDrop);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Player_MouseDown);
            this.MouseLeave += new System.EventHandler(this.Player_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Player_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Player_MouseUp);
            this.TOPmenuStrip1.ResumeLayout(false);
            this.TOPmenuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ICON_PB)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WMP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer WMP;
        private System.Windows.Forms.MenuStrip TOPmenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Connect_ToolStripMenuItem;
        private System.Windows.Forms.Timer ScriptTimer;
        private System.Windows.Forms.ToolStripMenuItem AutoConnect_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseSerialPort_ToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button g_Min_Btn;
        private System.Windows.Forms.Button g_Max_Btn;
        private System.Windows.Forms.Button g_Close_Btn;
        private System.Windows.Forms.PictureBox ICON_PB;
        private System.Windows.Forms.Label Title_Label;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label CurrentDeviceLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ToolStripMenuItem ChooseDevice;
        private System.Windows.Forms.Label ErrorMSG;
        private System.Windows.Forms.ToolStripMenuItem Reset_config_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Connect_ADP_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DisConnect_ADP_ToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button WakeUpBTN;
        private System.Windows.Forms.CheckBox CycleCB;
        private System.Windows.Forms.CheckBox ShowMC_CB;
        private System.Windows.Forms.Button FastBackward_BTN;
        private System.Windows.Forms.Button FastForward_BTN;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtCycTimes;
        private System.Windows.Forms.Label lblCycTimes;
        private System.Windows.Forms.ToolStripMenuItem Server_config_ToolStripMenuItem;
    }
}

