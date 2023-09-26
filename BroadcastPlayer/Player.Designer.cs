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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TOPmenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Connect_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoConnect_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseSerialPort_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChooseDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.Connect_ADP_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisConnect_ADP_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Reset_config_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Server_config_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScriptTimer = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.g_Min_Btn = new System.Windows.Forms.Button();
            this.g_Max_Btn = new System.Windows.Forms.Button();
            this.g_Close_Btn = new System.Windows.Forms.Button();
            this.ICON_PB = new System.Windows.Forms.PictureBox();
            this.Title_Label = new System.Windows.Forms.Label();
            this.CurrentDeviceLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xPanderPanelList1 = new BSE.Windows.Forms.XPanderPanelList();
            this.xPanderPanel1 = new BSE.Windows.Forms.XPanderPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.xPanderPanel2 = new BSE.Windows.Forms.XPanderPanel();
            this.pnlVideoList = new System.Windows.Forms.Panel();
            this.tblVideos = new System.Windows.Forms.TableLayoutPanel();
            this.dgvVideo = new System.Windows.Forms.DataGridView();
            this.VideoName = new System.Windows.Forms.DataGridViewButtonColumn();
            this.VideoAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
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
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            this.TOPmenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ICON_PB)).BeginInit();
            this.panel1.SuspendLayout();
            this.xPanderPanelList1.SuspendLayout();
            this.xPanderPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.xPanderPanel2.SuspendLayout();
            this.pnlVideoList.SuspendLayout();
            this.tblVideos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVideo)).BeginInit();
            this.panel5.SuspendLayout();
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
            this.TOPmenuStrip1.Size = new System.Drawing.Size(1024, 39);
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
            this.AutoConnect_ToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.AutoConnect_ToolStripMenuItem.Text = "Connect Dongle";
            this.AutoConnect_ToolStripMenuItem.Click += new System.EventHandler(this.AutoConnect_ToolStripMenuItem_Click);
            // 
            // CloseSerialPort_ToolStripMenuItem
            // 
            this.CloseSerialPort_ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.CloseSerialPort_ToolStripMenuItem.Name = "CloseSerialPort_ToolStripMenuItem";
            this.CloseSerialPort_ToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.CloseSerialPort_ToolStripMenuItem.Text = "Disconnect";
            this.CloseSerialPort_ToolStripMenuItem.Click += new System.EventHandler(this.cloasePort_Click);
            // 
            // ChooseDevice
            // 
            this.ChooseDevice.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ChooseDevice.Name = "ChooseDevice";
            this.ChooseDevice.Size = new System.Drawing.Size(209, 26);
            this.ChooseDevice.Text = "Choose Device";
            this.ChooseDevice.Click += new System.EventHandler(this.ChooseDeviceToolStripMenuItem_Click);
            // 
            // Connect_ADP_ToolStripMenuItem
            // 
            this.Connect_ADP_ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Connect_ADP_ToolStripMenuItem.Name = "Connect_ADP_ToolStripMenuItem";
            this.Connect_ADP_ToolStripMenuItem.Size = new System.Drawing.Size(229, 26);
            this.Connect_ADP_ToolStripMenuItem.Text = "Connect ADP";
            this.Connect_ADP_ToolStripMenuItem.Click += new System.EventHandler(this.Connect_ADP_ToolStripMenuItem_Click);
            // 
            // DisConnect_ADP_ToolStripMenuItem
            // 
            this.DisConnect_ADP_ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.DisConnect_ADP_ToolStripMenuItem.Name = "DisConnect_ADP_ToolStripMenuItem";
            this.DisConnect_ADP_ToolStripMenuItem.Size = new System.Drawing.Size(229, 26);
            this.DisConnect_ADP_ToolStripMenuItem.Text = "DisConnect ADP";
            this.DisConnect_ADP_ToolStripMenuItem.Click += new System.EventHandler(this.DisConnect_ADP_ToolStripMenuItem_Click);
            // 
            // Reset_config_ToolStripMenuItem
            // 
            this.Reset_config_ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Reset_config_ToolStripMenuItem.Name = "Reset_config_ToolStripMenuItem";
            this.Reset_config_ToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.Reset_config_ToolStripMenuItem.Text = "Clear Config";
            this.Reset_config_ToolStripMenuItem.Click += new System.EventHandler(this.Reset_config_ToolStripMenuItem_Click);
            // 
            // Server_config_ToolStripMenuItem
            // 
            this.Server_config_ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Server_config_ToolStripMenuItem.Name = "Server_config_ToolStripMenuItem";
            this.Server_config_ToolStripMenuItem.Size = new System.Drawing.Size(229, 26);
            this.Server_config_ToolStripMenuItem.Text = "Server Config";
            this.Server_config_ToolStripMenuItem.Click += new System.EventHandler(this.Server_config_ToolStripMenuItem_Click);
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
            this.button1.Location = new System.Drawing.Point(660, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 43);
            this.button1.TabIndex = 2;
            this.button1.Text = "Choose Video";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OpenFile_ToolStripMenuItem1_Click);
            // 
            // g_Min_Btn
            // 
            this.g_Min_Btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.g_Min_Btn.FlatAppearance.BorderSize = 0;
            this.g_Min_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.g_Min_Btn.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.g_Min_Btn.Location = new System.Drawing.Point(903, 2);
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
            this.g_Max_Btn.Location = new System.Drawing.Point(943, 2);
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
            this.g_Close_Btn.Location = new System.Drawing.Point(983, 2);
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
            this.Title_Label.Size = new System.Drawing.Size(169, 15);
            this.Title_Label.TabIndex = 8;
            this.Title_Label.Text = "Scent-PlayerV2.2.1";
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
            this.CurrentDeviceLabel.Size = new System.Drawing.Size(505, 25);
            this.CurrentDeviceLabel.TabIndex = 17;
            this.CurrentDeviceLabel.Text = "Current Status: Control All Devices";
            this.CurrentDeviceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.xPanderPanelList1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(824, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 729);
            this.panel1.TabIndex = 18;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // xPanderPanelList1
            // 
            this.xPanderPanelList1.CaptionStyle = BSE.Windows.Forms.CaptionStyle.Normal;
            this.xPanderPanelList1.Controls.Add(this.xPanderPanel1);
            this.xPanderPanelList1.Controls.Add(this.xPanderPanel2);
            this.xPanderPanelList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xPanderPanelList1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xPanderPanelList1.GradientBackground = System.Drawing.Color.Empty;
            this.xPanderPanelList1.Location = new System.Drawing.Point(0, 0);
            this.xPanderPanelList1.Name = "xPanderPanelList1";
            this.xPanderPanelList1.PanelColors = null;
            this.xPanderPanelList1.PanelStyle = BSE.Windows.Forms.PanelStyle.Office2007;
            this.xPanderPanelList1.ShowBorder = false;
            this.xPanderPanelList1.Size = new System.Drawing.Size(200, 729);
            this.xPanderPanelList1.TabIndex = 15;
            // 
            // xPanderPanel1
            // 
            this.xPanderPanel1.CaptionFont = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xPanderPanel1.CaptionHeight = 50;
            this.xPanderPanel1.Controls.Add(this.tableLayoutPanel1);
            this.xPanderPanel1.CustomColors.BackColor = System.Drawing.SystemColors.Control;
            this.xPanderPanel1.CustomColors.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.xPanderPanel1.CustomColors.CaptionCheckedGradientBegin = System.Drawing.Color.Empty;
            this.xPanderPanel1.CustomColors.CaptionCheckedGradientEnd = System.Drawing.Color.Empty;
            this.xPanderPanel1.CustomColors.CaptionCheckedGradientMiddle = System.Drawing.Color.Empty;
            this.xPanderPanel1.CustomColors.CaptionCloseIcon = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel1.CustomColors.CaptionExpandIcon = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel1.CustomColors.CaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.xPanderPanel1.CustomColors.CaptionGradientEnd = System.Drawing.SystemColors.ButtonFace;
            this.xPanderPanel1.CustomColors.CaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.xPanderPanel1.CustomColors.CaptionPressedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(188)))), ((int)(((byte)(235)))));
            this.xPanderPanel1.CustomColors.CaptionPressedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(188)))), ((int)(((byte)(235)))));
            this.xPanderPanel1.CustomColors.CaptionPressedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(188)))), ((int)(((byte)(235)))));
            this.xPanderPanel1.CustomColors.CaptionSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.xPanderPanel1.CustomColors.CaptionSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.xPanderPanel1.CustomColors.CaptionSelectedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.xPanderPanel1.CustomColors.CaptionSelectedText = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel1.CustomColors.CaptionText = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel1.CustomColors.FlatCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.xPanderPanel1.CustomColors.FlatCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.xPanderPanel1.CustomColors.InnerBorderColor = System.Drawing.SystemColors.Window;
            this.xPanderPanel1.Expand = true;
            this.xPanderPanel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xPanderPanel1.ForeColor = System.Drawing.Color.Black;
            this.xPanderPanel1.Image = null;
            this.xPanderPanel1.Name = "xPanderPanel1";
            this.xPanderPanel1.PanelStyle = BSE.Windows.Forms.PanelStyle.Office2007;
            this.xPanderPanel1.Size = new System.Drawing.Size(200, 679);
            this.xPanderPanel1.TabIndex = 0;
            this.xPanderPanel1.Text = "Scent Release Control";
            this.xPanderPanel1.ToolTipTextCloseIcon = null;
            this.xPanderPanel1.ToolTipTextExpandIconPanelCollapsed = null;
            this.xPanderPanel1.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 50);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(198, 629);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel2.Location = new System.Drawing.Point(3, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(192, 543);
            this.panel2.TabIndex = 14;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.numericUpDown1);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(192, 44);
            this.panel6.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(13, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 32);
            this.label1.TabIndex = 10;
            this.label1.Text = "Release\r\nDuration";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numericUpDown1.Location = new System.Drawing.Point(89, 10);
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
            this.numericUpDown1.Size = new System.Drawing.Size(65, 26);
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
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(160, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "S";
            // 
            // xPanderPanel2
            // 
            this.xPanderPanel2.CaptionFont = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xPanderPanel2.CaptionHeight = 50;
            this.xPanderPanel2.Controls.Add(this.pnlVideoList);
            this.xPanderPanel2.CustomColors.BackColor = System.Drawing.SystemColors.Control;
            this.xPanderPanel2.CustomColors.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.xPanderPanel2.CustomColors.CaptionCheckedGradientBegin = System.Drawing.Color.Empty;
            this.xPanderPanel2.CustomColors.CaptionCheckedGradientEnd = System.Drawing.Color.Empty;
            this.xPanderPanel2.CustomColors.CaptionCheckedGradientMiddle = System.Drawing.Color.Empty;
            this.xPanderPanel2.CustomColors.CaptionCloseIcon = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel2.CustomColors.CaptionExpandIcon = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel2.CustomColors.CaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.xPanderPanel2.CustomColors.CaptionGradientEnd = System.Drawing.SystemColors.ButtonFace;
            this.xPanderPanel2.CustomColors.CaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.xPanderPanel2.CustomColors.CaptionPressedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(188)))), ((int)(((byte)(235)))));
            this.xPanderPanel2.CustomColors.CaptionPressedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(188)))), ((int)(((byte)(235)))));
            this.xPanderPanel2.CustomColors.CaptionPressedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(188)))), ((int)(((byte)(235)))));
            this.xPanderPanel2.CustomColors.CaptionSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.xPanderPanel2.CustomColors.CaptionSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.xPanderPanel2.CustomColors.CaptionSelectedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.xPanderPanel2.CustomColors.CaptionSelectedText = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel2.CustomColors.CaptionText = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel2.CustomColors.FlatCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.xPanderPanel2.CustomColors.FlatCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.xPanderPanel2.CustomColors.InnerBorderColor = System.Drawing.SystemColors.Window;
            this.xPanderPanel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xPanderPanel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel2.Image = null;
            this.xPanderPanel2.Name = "xPanderPanel2";
            this.xPanderPanel2.PanelStyle = BSE.Windows.Forms.PanelStyle.Office2007;
            this.xPanderPanel2.Size = new System.Drawing.Size(200, 50);
            this.xPanderPanel2.TabIndex = 1;
            this.xPanderPanel2.Text = "Video List";
            this.xPanderPanel2.ToolTipTextCloseIcon = null;
            this.xPanderPanel2.ToolTipTextExpandIconPanelCollapsed = null;
            this.xPanderPanel2.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // pnlVideoList
            // 
            this.pnlVideoList.Controls.Add(this.tblVideos);
            this.pnlVideoList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVideoList.Location = new System.Drawing.Point(1, 50);
            this.pnlVideoList.Name = "pnlVideoList";
            this.pnlVideoList.Size = new System.Drawing.Size(198, 0);
            this.pnlVideoList.TabIndex = 0;
            // 
            // tblVideos
            // 
            this.tblVideos.ColumnCount = 1;
            this.tblVideos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblVideos.Controls.Add(this.dgvVideo, 0, 1);
            this.tblVideos.Controls.Add(this.panel5, 0, 0);
            this.tblVideos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblVideos.Location = new System.Drawing.Point(0, 0);
            this.tblVideos.Name = "tblVideos";
            this.tblVideos.RowCount = 2;
            this.tblVideos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblVideos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblVideos.Size = new System.Drawing.Size(198, 0);
            this.tblVideos.TabIndex = 3;
            // 
            // dgvVideo
            // 
            this.dgvVideo.AllowUserToAddRows = false;
            this.dgvVideo.AllowUserToDeleteRows = false;
            this.dgvVideo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVideo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VideoName,
            this.VideoAddr});
            this.dgvVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVideo.Location = new System.Drawing.Point(8, 48);
            this.dgvVideo.Margin = new System.Windows.Forms.Padding(8);
            this.dgvVideo.MultiSelect = false;
            this.dgvVideo.Name = "dgvVideo";
            this.dgvVideo.ReadOnly = true;
            this.dgvVideo.RowTemplate.Height = 23;
            this.dgvVideo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvVideo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVideo.Size = new System.Drawing.Size(182, 1);
            this.dgvVideo.TabIndex = 2;
            this.dgvVideo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVideo_CellContentClick);
            this.dgvVideo.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvVideo_RowPostPaint);
            // 
            // VideoName
            // 
            this.VideoName.DataPropertyName = "VideoName";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.VideoName.DefaultCellStyle = dataGridViewCellStyle9;
            this.VideoName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.VideoName.HeaderText = "Video Name";
            this.VideoName.MinimumWidth = 100;
            this.VideoName.Name = "VideoName";
            this.VideoName.ReadOnly = true;
            this.VideoName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.VideoName.Width = 140;
            // 
            // VideoAddr
            // 
            this.VideoAddr.DataPropertyName = "VideoAddr";
            this.VideoAddr.HeaderText = "Video Address";
            this.VideoAddr.MinimumWidth = 240;
            this.VideoAddr.Name = "VideoAddr";
            this.VideoAddr.ReadOnly = true;
            this.VideoAddr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.VideoAddr.Visible = false;
            this.VideoAddr.Width = 240;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnSelect);
            this.panel5.Controls.Add(this.btnClear);
            this.panel5.Controls.Add(this.btnDel);
            this.panel5.Controls.Add(this.btnAdd);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(192, 34);
            this.panel5.TabIndex = 3;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(95, 4);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(24, 24);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = "S";
            this.ttMain.SetToolTip(this.btnSelect, "Select Folder");
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(66, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(24, 24);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "C";
            this.ttMain.SetToolTip(this.btnClear, "List Clear");
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(36, 4);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(24, 24);
            this.btnDel.TabIndex = 1;
            this.btnDel.Text = "-";
            this.ttMain.SetToolTip(this.btnDel, "Del Video");
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(6, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(24, 24);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "+";
            this.ttMain.SetToolTip(this.btnAdd, "Add Video");
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            this.WakeUpBTN.Location = new System.Drawing.Point(740, 12);
            this.WakeUpBTN.Name = "WakeUpBTN";
            this.WakeUpBTN.Size = new System.Drawing.Size(75, 43);
            this.WakeUpBTN.TabIndex = 20;
            this.WakeUpBTN.Text = "Wake Up Device";
            this.WakeUpBTN.UseVisualStyleBackColor = true;
            this.WakeUpBTN.Click += new System.EventHandler(this.WakeUpBTN_Click);
            // 
            // CycleCB
            // 
            this.CycleCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CycleCB.AutoSize = true;
            this.CycleCB.Location = new System.Drawing.Point(555, 25);
            this.CycleCB.Name = "CycleCB";
            this.CycleCB.Size = new System.Drawing.Size(102, 16);
            this.CycleCB.TabIndex = 21;
            this.CycleCB.Text = "Loop Playback";
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
            this.ShowMC_CB.Text = "Show Video Bar";
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
            this.FastBackward_BTN.Text = "Backward";
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
            this.FastForward_BTN.Text = "Forward";
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
            this.panel3.Location = new System.Drawing.Point(0, 702);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(824, 66);
            this.panel3.TabIndex = 25;
            // 
            // txtCycTimes
            // 
            this.txtCycTimes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCycTimes.Location = new System.Drawing.Point(511, 23);
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
            this.lblCycTimes.Location = new System.Drawing.Point(437, 27);
            this.lblCycTimes.Name = "lblCycTimes";
            this.lblCycTimes.Size = new System.Drawing.Size(71, 12);
            this.lblCycTimes.TabIndex = 26;
            this.lblCycTimes.Text = "Loop Count:";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.Controls.Add(this.WMP);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 39);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(824, 663);
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
            this.WMP.Size = new System.Drawing.Size(824, 663);
            this.WMP.TabIndex = 0;
            this.WMP.StatusChange += new System.EventHandler(this.WMP_StatusChange);
            this.WMP.PositionChange += new AxWMPLib._WMPOCXEvents_PositionChangeEventHandler(this.WMP_PositionChange);
            this.WMP.ClickEvent += new AxWMPLib._WMPOCXEvents_ClickEventHandler(this.WMP_ClickEvent);
            this.WMP.ClientSizeChanged += new System.EventHandler(this.WMP_ClientSizeChanged);
            // 
            // Player
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
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
            this.xPanderPanelList1.ResumeLayout(false);
            this.xPanderPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.xPanderPanel2.ResumeLayout(false);
            this.pnlVideoList.ResumeLayout(false);
            this.tblVideos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVideo)).EndInit();
            this.panel5.ResumeLayout(false);
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
        private BSE.Windows.Forms.XPanderPanelList xPanderPanelList1;
        private BSE.Windows.Forms.XPanderPanel xPanderPanel1;
        private BSE.Windows.Forms.XPanderPanel xPanderPanel2;
        private System.Windows.Forms.Panel pnlVideoList;
        private System.Windows.Forms.DataGridView dgvVideo;
        private System.Windows.Forms.TableLayoutPanel tblVideos;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ToolTip ttMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridViewButtonColumn VideoName;
        private System.Windows.Forms.DataGridViewTextBoxColumn VideoAddr;
    }
}

