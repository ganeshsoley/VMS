namespace UI
{
    partial class frmUserAuthority
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
            this.grbUserList = new System.Windows.Forms.GroupBox();
            this.cboUser = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grbRights = new System.Windows.Forms.GroupBox();
            this.tvwRights = new System.Windows.Forms.TreeView();
            this.grbSave = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.grbUserList.SuspendLayout();
            this.grbRights.SuspendLayout();
            this.grbSave.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbUserList
            // 
            this.grbUserList.Controls.Add(this.cboUser);
            this.grbUserList.Controls.Add(this.label1);
            this.grbUserList.Location = new System.Drawing.Point(0, 0);
            this.grbUserList.Name = "grbUserList";
            this.grbUserList.Size = new System.Drawing.Size(469, 50);
            this.grbUserList.TabIndex = 0;
            this.grbUserList.TabStop = false;
            // 
            // cboUser
            // 
            this.cboUser.FormattingEnabled = true;
            this.cboUser.Location = new System.Drawing.Point(80, 16);
            this.cboUser.Name = "cboUser";
            this.cboUser.Size = new System.Drawing.Size(131, 21);
            this.cboUser.TabIndex = 1;
            this.cboUser.SelectedIndexChanged += new System.EventHandler(this.cboUser_SelectedIndexChanged);
            this.cboUser.Enter += new System.EventHandler(this.cboUser_Enter);
            this.cboUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboUser_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select User";
            // 
            // grbRights
            // 
            this.grbRights.Controls.Add(this.tvwRights);
            this.grbRights.Location = new System.Drawing.Point(0, 50);
            this.grbRights.Name = "grbRights";
            this.grbRights.Size = new System.Drawing.Size(469, 347);
            this.grbRights.TabIndex = 1;
            this.grbRights.TabStop = false;
            // 
            // tvwRights
            // 
            this.tvwRights.CheckBoxes = true;
            this.tvwRights.Location = new System.Drawing.Point(6, 11);
            this.tvwRights.Name = "tvwRights";
            this.tvwRights.Size = new System.Drawing.Size(453, 330);
            this.tvwRights.TabIndex = 0;
            this.tvwRights.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvwRights_AfterCheck);
            this.tvwRights.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwRights_AfterSelect);
            this.tvwRights.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvwRights_NodeMouseClick);
            this.tvwRights.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tvwRights_KeyPress);
            // 
            // grbSave
            // 
            this.grbSave.Controls.Add(this.btnCancel);
            this.grbSave.Controls.Add(this.btnOk);
            this.grbSave.Location = new System.Drawing.Point(0, 398);
            this.grbSave.Name = "grbSave";
            this.grbSave.Size = new System.Drawing.Size(469, 49);
            this.grbSave.TabIndex = 2;
            this.grbSave.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(387, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(63, 22);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(314, 16);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(63, 22);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmUserAuthority
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 447);
            this.Controls.Add(this.grbSave);
            this.Controls.Add(this.grbRights);
            this.Controls.Add(this.grbUserList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUserAuthority";
            this.Text = "User Rights";
            this.Load += new System.EventHandler(this.frmUserAuthority_Load);
            this.grbUserList.ResumeLayout(false);
            this.grbUserList.PerformLayout();
            this.grbRights.ResumeLayout(false);
            this.grbSave.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbUserList;
        private System.Windows.Forms.ComboBox cboUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grbRights;
        private System.Windows.Forms.GroupBox grbSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TreeView tvwRights;
    }
}