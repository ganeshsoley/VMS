namespace UI
{
    partial class frmVisitorProp
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
            this.grbProp = new System.Windows.Forms.GroupBox();
            this.cboIDProofType = new System.Windows.Forms.ComboBox();
            this.txtIDProofNo = new System.Windows.Forms.TextBox();
            this.txtMobileNo = new System.Windows.Forms.TextBox();
            this.cboCompany = new System.Windows.Forms.ComboBox();
            this.lblRegNo = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtVName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grbButtons = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.grbProp.SuspendLayout();
            this.grbButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbProp
            // 
            this.grbProp.Controls.Add(this.cboIDProofType);
            this.grbProp.Controls.Add(this.txtIDProofNo);
            this.grbProp.Controls.Add(this.txtMobileNo);
            this.grbProp.Controls.Add(this.cboCompany);
            this.grbProp.Controls.Add(this.lblRegNo);
            this.grbProp.Controls.Add(this.txtAddress);
            this.grbProp.Controls.Add(this.txtVName);
            this.grbProp.Controls.Add(this.label10);
            this.grbProp.Controls.Add(this.label9);
            this.grbProp.Controls.Add(this.label8);
            this.grbProp.Controls.Add(this.label7);
            this.grbProp.Controls.Add(this.label6);
            this.grbProp.Controls.Add(this.label5);
            this.grbProp.Controls.Add(this.label4);
            this.grbProp.Controls.Add(this.label3);
            this.grbProp.Controls.Add(this.label2);
            this.grbProp.Controls.Add(this.label1);
            this.grbProp.Location = new System.Drawing.Point(0, 0);
            this.grbProp.Name = "grbProp";
            this.grbProp.Size = new System.Drawing.Size(449, 220);
            this.grbProp.TabIndex = 0;
            this.grbProp.TabStop = false;
            // 
            // cboIDProofType
            // 
            this.cboIDProofType.FormattingEnabled = true;
            this.cboIDProofType.Location = new System.Drawing.Point(100, 156);
            this.cboIDProofType.Name = "cboIDProofType";
            this.cboIDProofType.Size = new System.Drawing.Size(170, 21);
            this.cboIDProofType.TabIndex = 15;
            this.cboIDProofType.SelectedIndexChanged += new System.EventHandler(this.cboIDProofType_SelectedIndexChanged);
            this.cboIDProofType.Enter += new System.EventHandler(this.cboIDProofType_Enter);
            this.cboIDProofType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboIDProofType_KeyPress);
            this.cboIDProofType.Leave += new System.EventHandler(this.cboIDProofType_Leave);
            // 
            // txtIDProofNo
            // 
            this.txtIDProofNo.Location = new System.Drawing.Point(100, 182);
            this.txtIDProofNo.Name = "txtIDProofNo";
            this.txtIDProofNo.Size = new System.Drawing.Size(170, 20);
            this.txtIDProofNo.TabIndex = 16;
            this.txtIDProofNo.TextChanged += new System.EventHandler(this.txtIDProofNo_TextChanged);
            this.txtIDProofNo.Enter += new System.EventHandler(this.txtIDProofNo_Enter);
            this.txtIDProofNo.Leave += new System.EventHandler(this.txtIDProofNo_Leave);
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.Location = new System.Drawing.Point(100, 130);
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Size = new System.Drawing.Size(115, 20);
            this.txtMobileNo.TabIndex = 14;
            this.txtMobileNo.TextChanged += new System.EventHandler(this.txtMobileNo_TextChanged);
            this.txtMobileNo.Enter += new System.EventHandler(this.txtMobileNo_Enter);
            this.txtMobileNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMobileNo_KeyPress);
            this.txtMobileNo.Leave += new System.EventHandler(this.txtMobileNo_Leave);
            // 
            // cboCompany
            // 
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(100, 77);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(315, 21);
            this.cboCompany.TabIndex = 10;
            this.cboCompany.SelectedIndexChanged += new System.EventHandler(this.cboCompany_SelectedIndexChanged);
            this.cboCompany.TextChanged += new System.EventHandler(this.cboCompany_TextChanged);
            this.cboCompany.Enter += new System.EventHandler(this.cboCompany_Enter);
            this.cboCompany.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboCompany_KeyPress);
            this.cboCompany.Leave += new System.EventHandler(this.cboCompany_Leave);
            // 
            // lblRegNo
            // 
            this.lblRegNo.BackColor = System.Drawing.SystemColors.Window;
            this.lblRegNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRegNo.Location = new System.Drawing.Point(100, 25);
            this.lblRegNo.Name = "lblRegNo";
            this.lblRegNo.Size = new System.Drawing.Size(115, 20);
            this.lblRegNo.TabIndex = 7;
            this.lblRegNo.Visible = false;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(100, 104);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(315, 20);
            this.txtAddress.TabIndex = 11;
            this.txtAddress.TextChanged += new System.EventHandler(this.txtAddress_TextChanged);
            this.txtAddress.Enter += new System.EventHandler(this.txtAddress_Enter);
            this.txtAddress.Leave += new System.EventHandler(this.txtAddress_Leave);
            // 
            // txtVName
            // 
            this.txtVName.Location = new System.Drawing.Point(100, 51);
            this.txtVName.Name = "txtVName";
            this.txtVName.Size = new System.Drawing.Size(315, 20);
            this.txtVName.TabIndex = 8;
            this.txtVName.TextChanged += new System.EventHandler(this.txtVName_TextChanged);
            this.txtVName.Enter += new System.EventHandler(this.txtVName_Enter);
            this.txtVName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVName_KeyPress);
            this.txtVName.Leave += new System.EventHandler(this.txtVName_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 182);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "ID Proof No.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 156);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "ID Proof Type";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(221, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Mobile No.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Address";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(421, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Company";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(421, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Reg. No.";
            this.label1.Visible = false;
            // 
            // grbButtons
            // 
            this.grbButtons.Controls.Add(this.btnCancel);
            this.grbButtons.Controls.Add(this.btnSave);
            this.grbButtons.Location = new System.Drawing.Point(0, 220);
            this.grbButtons.Name = "grbButtons";
            this.grbButtons.Size = new System.Drawing.Size(449, 55);
            this.grbButtons.TabIndex = 1;
            this.grbButtons.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(369, 19);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 22);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(298, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(65, 22);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmVisitorProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 277);
            this.Controls.Add(this.grbButtons);
            this.Controls.Add(this.grbProp);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVisitorProp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Visitor";
            this.Load += new System.EventHandler(this.frmVisitorProp_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmVisitorProp_KeyDown);
            this.grbProp.ResumeLayout(false);
            this.grbProp.PerformLayout();
            this.grbButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbProp;
        private System.Windows.Forms.ComboBox cboCompany;
        private System.Windows.Forms.Label lblRegNo;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtVName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboIDProofType;
        private System.Windows.Forms.TextBox txtIDProofNo;
        private System.Windows.Forms.TextBox txtMobileNo;
        private System.Windows.Forms.GroupBox grbButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}