namespace UI
{
    partial class frmReturnableDCList
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
            this.components = new System.ComponentModel.Container();
            this.grbFilter = new System.Windows.Forms.GroupBox();
            this.chkShowAll = new System.Windows.Forms.CheckBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.cboEntryType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lvwDCList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grbButtons = new System.Windows.Forms.GroupBox();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.conMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.modifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grbReport = new System.Windows.Forms.GroupBox();
            this.btnRptCancel = new System.Windows.Forms.Button();
            this.btnRptView = new System.Windows.Forms.Button();
            this.cboParty = new System.Windows.Forms.ComboBox();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grbFilter.SuspendLayout();
            this.grbButtons.SuspendLayout();
            this.conMenu.SuspendLayout();
            this.grbReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbFilter
            // 
            this.grbFilter.Controls.Add(this.chkShowAll);
            this.grbFilter.Controls.Add(this.dtpDate);
            this.grbFilter.Controls.Add(this.cboEntryType);
            this.grbFilter.Controls.Add(this.label1);
            this.grbFilter.Location = new System.Drawing.Point(0, 0);
            this.grbFilter.Name = "grbFilter";
            this.grbFilter.Size = new System.Drawing.Size(552, 55);
            this.grbFilter.TabIndex = 0;
            this.grbFilter.TabStop = false;
            // 
            // chkShowAll
            // 
            this.chkShowAll.AutoSize = true;
            this.chkShowAll.Location = new System.Drawing.Point(265, 21);
            this.chkShowAll.Name = "chkShowAll";
            this.chkShowAll.Size = new System.Drawing.Size(67, 17);
            this.chkShowAll.TabIndex = 4;
            this.chkShowAll.Text = "Show &All";
            this.chkShowAll.UseVisualStyleBackColor = true;
            this.chkShowAll.CheckedChanged += new System.EventHandler(this.chkShowAll_CheckedChanged);
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(171, 21);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(88, 20);
            this.dtpDate.TabIndex = 2;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // cboEntryType
            // 
            this.cboEntryType.FormattingEnabled = true;
            this.cboEntryType.Location = new System.Drawing.Point(65, 21);
            this.cboEntryType.Name = "cboEntryType";
            this.cboEntryType.Size = new System.Drawing.Size(100, 21);
            this.cboEntryType.TabIndex = 1;
            this.cboEntryType.SelectedIndexChanged += new System.EventHandler(this.cboEntryType_SelectedIndexChanged);
            this.cboEntryType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboEntryType_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "List For";
            // 
            // lvwDCList
            // 
            this.lvwDCList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lvwDCList.Location = new System.Drawing.Point(0, 61);
            this.lvwDCList.Name = "lvwDCList";
            this.lvwDCList.Size = new System.Drawing.Size(552, 268);
            this.lvwDCList.TabIndex = 1;
            this.lvwDCList.UseCompatibleStateImageBehavior = false;
            this.lvwDCList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwDCList_ColumnClick);
            this.lvwDCList.DoubleClick += new System.EventHandler(this.lvwDCList_DoubleClick);
            this.lvwDCList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvwDCList_KeyPress);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Entry No.";
            this.columnHeader1.Width = 65;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Entry Date";
            this.columnHeader2.Width = 75;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Party Name";
            this.columnHeader3.Width = 200;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Entry Type";
            this.columnHeader4.Width = 70;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "DC No.";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "DC Date";
            this.columnHeader6.Width = 75;
            // 
            // grbButtons
            // 
            this.grbButtons.Controls.Add(this.btnReport);
            this.grbButtons.Controls.Add(this.btnNew);
            this.grbButtons.Controls.Add(this.btnCancel);
            this.grbButtons.Controls.Add(this.btnOk);
            this.grbButtons.Location = new System.Drawing.Point(0, 328);
            this.grbButtons.Name = "grbButtons";
            this.grbButtons.Size = new System.Drawing.Size(552, 57);
            this.grbButtons.TabIndex = 2;
            this.grbButtons.TabStop = false;
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(12, 19);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(65, 23);
            this.btnReport.TabIndex = 7;
            this.btnReport.Text = "&Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(467, 19);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(65, 23);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(467, 19);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(386, 19);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // conMenu
            // 
            this.conMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modifyToolStripMenuItem,
            this.newToolStripMenuItem,
            this.toolStripSeparator1,
            this.deleteToolStripMenuItem});
            this.conMenu.Name = "conMenu";
            this.conMenu.Size = new System.Drawing.Size(113, 76);
            this.conMenu.Opening += new System.ComponentModel.CancelEventHandler(this.conMenu_Opening);
            // 
            // modifyToolStripMenuItem
            // 
            this.modifyToolStripMenuItem.Name = "modifyToolStripMenuItem";
            this.modifyToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.modifyToolStripMenuItem.Text = "&Modify";
            this.modifyToolStripMenuItem.Click += new System.EventHandler(this.modifyToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(109, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.deleteToolStripMenuItem.Text = "&Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // grbReport
            // 
            this.grbReport.Controls.Add(this.btnRptCancel);
            this.grbReport.Controls.Add(this.btnRptView);
            this.grbReport.Controls.Add(this.cboParty);
            this.grbReport.Controls.Add(this.dtpToDate);
            this.grbReport.Controls.Add(this.dtpFromDate);
            this.grbReport.Controls.Add(this.label4);
            this.grbReport.Controls.Add(this.label3);
            this.grbReport.Controls.Add(this.label2);
            this.grbReport.Location = new System.Drawing.Point(235, 116);
            this.grbReport.Name = "grbReport";
            this.grbReport.Size = new System.Drawing.Size(307, 111);
            this.grbReport.TabIndex = 3;
            this.grbReport.TabStop = false;
            this.grbReport.Visible = false;
            // 
            // btnRptCancel
            // 
            this.btnRptCancel.Location = new System.Drawing.Point(232, 78);
            this.btnRptCancel.Name = "btnRptCancel";
            this.btnRptCancel.Size = new System.Drawing.Size(65, 23);
            this.btnRptCancel.TabIndex = 7;
            this.btnRptCancel.Text = "Cancel";
            this.btnRptCancel.UseVisualStyleBackColor = true;
            this.btnRptCancel.Click += new System.EventHandler(this.btnRptCancel_Click);
            // 
            // btnRptView
            // 
            this.btnRptView.Location = new System.Drawing.Point(161, 78);
            this.btnRptView.Name = "btnRptView";
            this.btnRptView.Size = new System.Drawing.Size(65, 23);
            this.btnRptView.TabIndex = 6;
            this.btnRptView.Text = "&View";
            this.btnRptView.UseVisualStyleBackColor = true;
            this.btnRptView.Click += new System.EventHandler(this.btnRptView_Click);
            // 
            // cboParty
            // 
            this.cboParty.FormattingEnabled = true;
            this.cboParty.Location = new System.Drawing.Point(66, 51);
            this.cboParty.Name = "cboParty";
            this.cboParty.Size = new System.Drawing.Size(227, 21);
            this.cboParty.TabIndex = 5;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(210, 25);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(83, 20);
            this.dtpToDate.TabIndex = 4;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(66, 25);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(86, 20);
            this.dtpFromDate.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Party";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(158, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "To Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "From Date";
            // 
            // frmReturnableDCList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 387);
            this.Controls.Add(this.grbReport);
            this.Controls.Add(this.lvwDCList);
            this.Controls.Add(this.grbButtons);
            this.Controls.Add(this.grbFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReturnableDCList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Returnable DC List";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmReturnableDCList_FormClosing);
            this.Load += new System.EventHandler(this.frmReturnableDCList_Load);
            this.grbFilter.ResumeLayout(false);
            this.grbFilter.PerformLayout();
            this.grbButtons.ResumeLayout(false);
            this.conMenu.ResumeLayout(false);
            this.grbReport.ResumeLayout(false);
            this.grbReport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbFilter;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.ComboBox cboEntryType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvwDCList;
        private System.Windows.Forms.GroupBox grbButtons;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ContextMenuStrip conMenu;
        private System.Windows.Forms.ToolStripMenuItem modifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkShowAll;
        private System.Windows.Forms.GroupBox grbReport;
        private System.Windows.Forms.Button btnRptCancel;
        private System.Windows.Forms.Button btnRptView;
        private System.Windows.Forms.ComboBox cboParty;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnReport;
    }
}