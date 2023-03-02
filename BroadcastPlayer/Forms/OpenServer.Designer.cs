
namespace BroadcastPlayer.Forms
{
    partial class OpenServer
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
            this.pnlCmd = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboServerIP = new System.Windows.Forms.ComboBox();
            this.btnService = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvVideo = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.VideoName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VideoAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlCmd.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCmd
            // 
            this.pnlCmd.Controls.Add(this.cboServerIP);
            this.pnlCmd.Controls.Add(this.btnService);
            this.pnlCmd.Controls.Add(this.txtPort);
            this.pnlCmd.Controls.Add(this.label2);
            this.pnlCmd.Controls.Add(this.label1);
            this.pnlCmd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCmd.Location = new System.Drawing.Point(0, 281);
            this.pnlCmd.Name = "pnlCmd";
            this.pnlCmd.Size = new System.Drawing.Size(548, 85);
            this.pnlCmd.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvVideo);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(548, 281);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "视频列表";
            // 
            // cboServerIP
            // 
            this.cboServerIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboServerIP.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboServerIP.FormattingEnabled = true;
            this.cboServerIP.Items.AddRange(new object[] {
            "127.0.0.1"});
            this.cboServerIP.Location = new System.Drawing.Point(71, 31);
            this.cboServerIP.Name = "cboServerIP";
            this.cboServerIP.Size = new System.Drawing.Size(151, 24);
            this.cboServerIP.TabIndex = 7;
            // 
            // btnService
            // 
            this.btnService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnService.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnService.Location = new System.Drawing.Point(399, 22);
            this.btnService.Name = "btnService";
            this.btnService.Size = new System.Drawing.Size(137, 43);
            this.btnService.TabIndex = 9;
            this.btnService.Text = "启动服务";
            this.btnService.UseVisualStyleBackColor = true;
            this.btnService.Click += new System.EventHandler(this.btnService_Click);
            // 
            // txtPort
            // 
            this.txtPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPort.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPort.Location = new System.Drawing.Point(303, 30);
            this.txtPort.Name = "txtPort";
            this.txtPort.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPort.Size = new System.Drawing.Size(56, 26);
            this.txtPort.TabIndex = 8;
            this.txtPort.Text = "65535";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(225, 35);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "服务端口:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(10, 35);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "服务IP:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnDel);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(444, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(101, 261);
            this.panel1.TabIndex = 0;
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
            this.dgvVideo.Location = new System.Drawing.Point(3, 17);
            this.dgvVideo.Name = "dgvVideo";
            this.dgvVideo.RowTemplate.Height = 23;
            this.dgvVideo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVideo.Size = new System.Drawing.Size(441, 261);
            this.dgvVideo.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 21);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 35);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "添 加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(12, 67);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 35);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "修 改";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Visible = false;
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(12, 114);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 35);
            this.btnDel.TabIndex = 2;
            this.btnDel.Text = "删 除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(7, 207);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 35);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存设置";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // VideoName
            // 
            this.VideoName.DataPropertyName = "VideoName";
            this.VideoName.HeaderText = "视频名称";
            this.VideoName.MinimumWidth = 100;
            this.VideoName.Name = "VideoName";
            this.VideoName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.VideoName.Width = 120;
            // 
            // VideoAddr
            // 
            this.VideoAddr.DataPropertyName = "VideoAddr";
            this.VideoAddr.HeaderText = "视频地址";
            this.VideoAddr.MinimumWidth = 240;
            this.VideoAddr.Name = "VideoAddr";
            this.VideoAddr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.VideoAddr.Width = 240;
            // 
            // OpenServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 366);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlCmd);
            this.Name = "OpenServer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "服务配置";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OpenServer_Load);
            this.pnlCmd.ResumeLayout(false);
            this.pnlCmd.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVideo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCmd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboServerIP;
        private System.Windows.Forms.Button btnService;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvVideo;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn VideoName;
        private System.Windows.Forms.DataGridViewTextBoxColumn VideoAddr;
    }
}