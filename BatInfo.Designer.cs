namespace PowerTray
{
    partial class BatInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatInfo));
            this.close = new System.Windows.Forms.Button();
            this.bottomBar = new System.Windows.Forms.Panel();
            this.refresh = new System.Windows.Forms.Button();
            this.autoRefresh = new System.Windows.Forms.CheckBox();
            this.info = new System.Windows.Forms.SplitContainer();
            this.Items = new System.Windows.Forms.ListBox();
            this.Values = new System.Windows.Forms.ListBox();
            this.bottomBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.info)).BeginInit();
            this.info.Panel1.SuspendLayout();
            this.info.Panel2.SuspendLayout();
            this.info.SuspendLayout();
            this.SuspendLayout();
            // 
            // close
            // 
            this.close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.close.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close.Location = new System.Drawing.Point(481, 102);
            this.close.Margin = new System.Windows.Forms.Padding(4);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(154, 54);
            this.close.TabIndex = 0;
            this.close.Text = "Close\r\n";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.Close_Click);
            // 
            // bottomBar
            // 
            this.bottomBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bottomBar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bottomBar.Controls.Add(this.refresh);
            this.bottomBar.Controls.Add(this.autoRefresh);
            this.bottomBar.Controls.Add(this.close);
            this.bottomBar.Location = new System.Drawing.Point(0, 902);
            this.bottomBar.Margin = new System.Windows.Forms.Padding(0);
            this.bottomBar.Name = "bottomBar";
            this.bottomBar.Size = new System.Drawing.Size(648, 185);
            this.bottomBar.TabIndex = 2;
            this.bottomBar.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel1_Paint);
            // 
            // refresh
            // 
            this.refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refresh.Location = new System.Drawing.Point(319, 102);
            this.refresh.Margin = new System.Windows.Forms.Padding(6);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(150, 54);
            this.refresh.TabIndex = 2;
            this.refresh.Text = "Refresh";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // autoRefresh
            // 
            this.autoRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.autoRefresh.AutoSize = true;
            this.autoRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoRefresh.Location = new System.Drawing.Point(24, 117);
            this.autoRefresh.Margin = new System.Windows.Forms.Padding(6);
            this.autoRefresh.Name = "autoRefresh";
            this.autoRefresh.Size = new System.Drawing.Size(205, 35);
            this.autoRefresh.TabIndex = 1;
            this.autoRefresh.Text = "Auto Refresh";
            this.autoRefresh.UseVisualStyleBackColor = true;
            this.autoRefresh.Click += new System.EventHandler(this.AutoRefreshToggle);
            // 
            // info
            // 
            this.info.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.info.Location = new System.Drawing.Point(0, 0);
            this.info.Margin = new System.Windows.Forms.Padding(6);
            this.info.Name = "info";
            // 
            // info.Panel1
            // 
            this.info.Panel1.Controls.Add(this.Items);
            this.info.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            this.info.Panel1MinSize = 0;
            // 
            // info.Panel2
            // 
            this.info.Panel2.Controls.Add(this.Values);
            this.info.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.info.Size = new System.Drawing.Size(648, 896);
            this.info.SplitterDistance = 311;
            this.info.SplitterWidth = 8;
            this.info.TabIndex = 3;
            // 
            // Items
            // 
            this.Items.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Items.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Items.FormattingEnabled = true;
            this.Items.ItemHeight = 31;
            this.Items.Items.AddRange(new object[] {
            "Percent",
            "Charge time",
            "Status (System)",
            "----------------",
            "Design Capacity",
            "Current Capacity",
            "Current Charge",
            "Charge Rate",
            "Battery Health",
            "----------------",
            "Description",
            "Caption",
            "Model ID",
            "Expected Run Time",
            "Voltage",
            "Status"});
            this.Items.Location = new System.Drawing.Point(0, 0);
            this.Items.Name = "Items";
            this.Items.Size = new System.Drawing.Size(311, 896);
            this.Items.TabIndex = 1;
            this.Items.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // Values
            // 
            this.Values.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Values.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Values.FormattingEnabled = true;
            this.Values.ItemHeight = 31;
            this.Values.Items.AddRange(new object[] {
            "100%",
            "2 hours",
            "Online",
            "900"});
            this.Values.Location = new System.Drawing.Point(0, 0);
            this.Values.Name = "Values";
            this.Values.Size = new System.Drawing.Size(329, 896);
            this.Values.TabIndex = 2;
            this.Values.SelectedIndexChanged += new System.EventHandler(this.Values_SelectedIndexChanged);
            // 
            // BatInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 1079);
            this.Controls.Add(this.info);
            this.Controls.Add(this.bottomBar);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(654, 1088);
            this.Name = "BatInfo";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Battery Info and Statistics";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.BatInfo_Load);
            this.bottomBar.ResumeLayout(false);
            this.bottomBar.PerformLayout();
            this.info.Panel1.ResumeLayout(false);
            this.info.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.info)).EndInit();
            this.info.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Panel bottomBar;
        private System.Windows.Forms.CheckBox autoRefresh;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.SplitContainer info;
        private System.Windows.Forms.ListBox Items;
        private System.Windows.Forms.ListBox Values;
    }
}