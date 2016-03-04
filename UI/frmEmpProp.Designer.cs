namespace UI
{
    partial class frmEmpProp
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
            this.lblDept = new System.Windows.Forms.Label();
            this.grbMain = new System.Windows.Forms.GroupBox();
            this.lblInitials = new System.Windows.Forms.Label();
            this.btnDeptList = new System.Windows.Forms.Button();
            this.chkInActive = new System.Windows.Forms.CheckBox();
            this.cboPlant = new System.Windows.Forms.ComboBox();
            this.txtMobileNo = new System.Windows.Forms.TextBox();
            this.txtEMail = new System.Windows.Forms.TextBox();
            this.txtDept = new System.Windows.Forms.TextBox();
            this.txtInitials = new System.Windows.Forms.TextBox();
            this.txtLName = new System.Windows.Forms.TextBox();
            this.txtMidName = new System.Windows.Forms.TextBox();
            this.txtFName = new System.Windows.Forms.TextBox();
            this.txtEmpCode = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grbButtons = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.grbMain.SuspendLayout();
            this.grbButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDept
            // 
            this.lblDept.BackColor = System.Drawing.SystemColors.Control;
            this.lblDept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDept.Location = new System.Drawing.Point(104, 111);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(269, 18);
            this.lblDept.TabIndex = 27;
            // 
            // grbMain
            // 
            this.grbMain.Controls.Add(this.lblDept);
            this.grbMain.Controls.Add(this.lblInitials);
            this.grbMain.Controls.Add(this.btnDeptList);
            this.grbMain.Controls.Add(this.chkInActive);
            this.grbMain.Controls.Add(this.cboPlant);
            this.grbMain.Controls.Add(this.txtMobileNo);
            this.grbMain.Controls.Add(this.txtEMail);
            this.grbMain.Controls.Add(this.txtDept);
            this.grbMain.Controls.Add(this.txtInitials);
            this.grbMain.Controls.Add(this.txtLName);
            this.grbMain.Controls.Add(this.txtMidName);
            this.grbMain.Controls.Add(this.txtFName);
            this.grbMain.Controls.Add(this.txtEmpCode);
            this.grbMain.Controls.Add(this.label15);
            this.grbMain.Controls.Add(this.label14);
            this.grbMain.Controls.Add(this.label13);
            this.grbMain.Controls.Add(this.label12);
            this.grbMain.Controls.Add(this.label11);
            this.grbMain.Controls.Add(this.label10);
            this.grbMain.Controls.Add(this.label9);
            this.grbMain.Controls.Add(this.label8);
            this.grbMain.Controls.Add(this.label6);
            this.grbMain.Controls.Add(this.label5);
            this.grbMain.Controls.Add(this.label4);
            this.grbMain.Controls.Add(this.label3);
            this.grbMain.Controls.Add(this.label2);
            this.grbMain.Controls.Add(this.label1);
            this.grbMain.Location = new System.Drawing.Point(2, 1);
            this.grbMain.Name = "grbMain";
            this.grbMain.Size = new System.Drawing.Size(480, 256);
            this.grbMain.TabIndex = 2;
            this.grbMain.TabStop = false;
            // 
            // lblInitials
            // 
            this.lblInitials.BackColor = System.Drawing.SystemColors.Control;
            this.lblInitials.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInitials.Location = new System.Drawing.Point(104, 86);
            this.lblInitials.Name = "lblInitials";
            this.lblInitials.Size = new System.Drawing.Size(269, 18);
            this.lblInitials.TabIndex = 26;
            // 
            // btnDeptList
            // 
            this.btnDeptList.Image = global::UI.Properties.Resources.Search16x16;
            this.btnDeptList.Location = new System.Drawing.Point(394, 111);
            this.btnDeptList.Name = "btnDeptList";
            this.btnDeptList.Size = new System.Drawing.Size(23, 23);
            this.btnDeptList.TabIndex = 21;
            this.btnDeptList.UseVisualStyleBackColor = true;
            this.btnDeptList.Click += new System.EventHandler(this.btnDeptList_Click);
            // 
            // chkInActive
            // 
            this.chkInActive.AutoSize = true;
            this.chkInActive.Location = new System.Drawing.Point(104, 218);
            this.chkInActive.Name = "chkInActive";
            this.chkInActive.Size = new System.Drawing.Size(68, 17);
            this.chkInActive.TabIndex = 25;
            this.chkInActive.Text = "In Active";
            this.chkInActive.UseVisualStyleBackColor = true;
            this.chkInActive.CheckedChanged += new System.EventHandler(this.chkInActive_CheckedChanged);
            // 
            // cboPlant
            // 
            this.cboPlant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPlant.FormattingEnabled = true;
            this.cboPlant.Location = new System.Drawing.Point(104, 187);
            this.cboPlant.Name = "cboPlant";
            this.cboPlant.Size = new System.Drawing.Size(128, 21);
            this.cboPlant.TabIndex = 24;
            this.cboPlant.SelectedIndexChanged += new System.EventHandler(this.cboPlant_SelectedIndexChanged);
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.Location = new System.Drawing.Point(104, 162);
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Size = new System.Drawing.Size(128, 20);
            this.txtMobileNo.TabIndex = 23;
            this.txtMobileNo.TextChanged += new System.EventHandler(this.txtMobileNo_TextChanged);
            this.txtMobileNo.Enter += new System.EventHandler(this.txtMobileNo_Enter);
            this.txtMobileNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMobileNo_KeyPress);
            this.txtMobileNo.Leave += new System.EventHandler(this.txtMobileNo_Leave);
            // 
            // txtEMail
            // 
            this.txtEMail.Location = new System.Drawing.Point(104, 137);
            this.txtEMail.Name = "txtEMail";
            this.txtEMail.Size = new System.Drawing.Size(269, 20);
            this.txtEMail.TabIndex = 22;
            this.txtEMail.TextChanged += new System.EventHandler(this.txtEMail_TextChanged);
            this.txtEMail.Enter += new System.EventHandler(this.txtEMail_Enter);
            this.txtEMail.Leave += new System.EventHandler(this.txtEMail_Leave);
            // 
            // txtDept
            // 
            this.txtDept.Location = new System.Drawing.Point(104, 111);
            this.txtDept.Name = "txtDept";
            this.txtDept.Size = new System.Drawing.Size(269, 20);
            this.txtDept.TabIndex = 20;
            this.txtDept.Visible = false;
            // 
            // txtInitials
            // 
            this.txtInitials.Location = new System.Drawing.Point(104, 86);
            this.txtInitials.Name = "txtInitials";
            this.txtInitials.Size = new System.Drawing.Size(269, 20);
            this.txtInitials.TabIndex = 19;
            this.txtInitials.Visible = false;
            // 
            // txtLName
            // 
            this.txtLName.Location = new System.Drawing.Point(307, 60);
            this.txtLName.Name = "txtLName";
            this.txtLName.Size = new System.Drawing.Size(128, 20);
            this.txtLName.TabIndex = 18;
            this.txtLName.TextChanged += new System.EventHandler(this.txtLName_TextChanged);
            this.txtLName.Enter += new System.EventHandler(this.txtLName_Enter);
            this.txtLName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLName_KeyPress);
            this.txtLName.Leave += new System.EventHandler(this.txtLName_Leave);
            // 
            // txtMidName
            // 
            this.txtMidName.Location = new System.Drawing.Point(173, 60);
            this.txtMidName.Name = "txtMidName";
            this.txtMidName.Size = new System.Drawing.Size(128, 20);
            this.txtMidName.TabIndex = 17;
            this.txtMidName.TextChanged += new System.EventHandler(this.txtMidName_TextChanged);
            this.txtMidName.Enter += new System.EventHandler(this.txtMidName_Enter);
            this.txtMidName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMidName_KeyDown);
            this.txtMidName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMidName_KeyPress);
            this.txtMidName.Leave += new System.EventHandler(this.txtMidName_Leave);
            // 
            // txtFName
            // 
            this.txtFName.Location = new System.Drawing.Point(20, 60);
            this.txtFName.Name = "txtFName";
            this.txtFName.Size = new System.Drawing.Size(147, 20);
            this.txtFName.TabIndex = 16;
            this.txtFName.TextChanged += new System.EventHandler(this.txtFName_TextChanged);
            this.txtFName.Enter += new System.EventHandler(this.txtFName_Enter);
            this.txtFName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFName_KeyDown);
            this.txtFName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFName_KeyPress);
            this.txtFName.Leave += new System.EventHandler(this.txtFName_Leave);
            // 
            // txtEmpCode
            // 
            this.txtEmpCode.Location = new System.Drawing.Point(104, 18);
            this.txtEmpCode.Name = "txtEmpCode";
            this.txtEmpCode.Size = new System.Drawing.Size(98, 20);
            this.txtEmpCode.TabIndex = 15;
            this.txtEmpCode.TextChanged += new System.EventHandler(this.txtEmpCode_TextChanged);
            this.txtEmpCode.Enter += new System.EventHandler(this.txtEmpCode_Enter);
            this.txtEmpCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpCode_KeyPress);
            this.txtEmpCode.Leave += new System.EventHandler(this.txtEmpCode_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(233, 191);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(11, 13);
            this.label15.TabIndex = 14;
            this.label15.Text = "*";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(17, 187);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 13);
            this.label14.TabIndex = 13;
            this.label14.Text = "Plant";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(233, 162);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(11, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "*";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 162);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 11;
            this.label12.Text = "Mobile No.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 137);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "E-Mail";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 111);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Department";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(379, 111);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Initials";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(304, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Last Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(170, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Middle Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(71, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "First Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(203, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Employee Code";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(394, 19);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grbButtons
            // 
            this.grbButtons.Controls.Add(this.btnCancel);
            this.grbButtons.Controls.Add(this.btnSave);
            this.grbButtons.Location = new System.Drawing.Point(2, 257);
            this.grbButtons.Name = "grbButtons";
            this.grbButtons.Size = new System.Drawing.Size(480, 58);
            this.grbButtons.TabIndex = 3;
            this.grbButtons.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(307, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmEmpProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 317);
            this.Controls.Add(this.grbMain);
            this.Controls.Add(this.grbButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEmpProp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Employee";
            this.Load += new System.EventHandler(this.frmEmpProp_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmEmpProp_KeyDown);
            this.grbMain.ResumeLayout(false);
            this.grbMain.PerformLayout();
            this.grbButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDept;
        private System.Windows.Forms.GroupBox grbMain;
        private System.Windows.Forms.Label lblInitials;
        private System.Windows.Forms.Button btnDeptList;
        private System.Windows.Forms.CheckBox chkInActive;
        private System.Windows.Forms.ComboBox cboPlant;
        private System.Windows.Forms.TextBox txtMobileNo;
        private System.Windows.Forms.TextBox txtEMail;
        private System.Windows.Forms.TextBox txtDept;
        private System.Windows.Forms.TextBox txtInitials;
        private System.Windows.Forms.TextBox txtLName;
        private System.Windows.Forms.TextBox txtMidName;
        private System.Windows.Forms.TextBox txtFName;
        private System.Windows.Forms.TextBox txtEmpCode;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grbButtons;
        private System.Windows.Forms.Button btnSave;
    }
}