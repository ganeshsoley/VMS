namespace UI
{
    partial class frmVehicleProp
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
            this.grbMain = new System.Windows.Forms.GroupBox();
            this.txtPUCExpiry = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.txtLicenseNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVehicleNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grbButtons = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.grbMain.SuspendLayout();
            this.grbButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbMain
            // 
            this.grbMain.Controls.Add(this.txtPUCExpiry);
            this.grbMain.Controls.Add(this.label4);
            this.grbMain.Controls.Add(this.chkIsActive);
            this.grbMain.Controls.Add(this.txtLicenseNo);
            this.grbMain.Controls.Add(this.label3);
            this.grbMain.Controls.Add(this.label2);
            this.grbMain.Controls.Add(this.txtVehicleNo);
            this.grbMain.Controls.Add(this.label1);
            this.grbMain.Location = new System.Drawing.Point(2, -2);
            this.grbMain.Name = "grbMain";
            this.grbMain.Size = new System.Drawing.Size(287, 121);
            this.grbMain.TabIndex = 0;
            this.grbMain.TabStop = false;
            // 
            // txtPUCExpiry
            // 
            this.txtPUCExpiry.Location = new System.Drawing.Point(81, 66);
            this.txtPUCExpiry.Name = "txtPUCExpiry";
            this.txtPUCExpiry.Size = new System.Drawing.Size(152, 20);
            this.txtPUCExpiry.TabIndex = 6;
            this.txtPUCExpiry.TextChanged += new System.EventHandler(this.txtPUCExpiry_TextChanged);
            this.txtPUCExpiry.Leave += new System.EventHandler(this.txtPUCExpiry_Leave);
            this.txtPUCExpiry.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPUCExpiry_KeyPress);
            this.txtPUCExpiry.Enter += new System.EventHandler(this.txtPUCExpiry_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "PUC Expiry";
            // 
            // chkIsActive
            // 
            this.chkIsActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIsActive.Location = new System.Drawing.Point(14, 92);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(80, 17);
            this.chkIsActive.TabIndex = 7;
            this.chkIsActive.Text = "Is Active";
            this.chkIsActive.UseVisualStyleBackColor = true;
            this.chkIsActive.Leave += new System.EventHandler(this.chkIsActive_Leave);
            // 
            // txtLicenseNo
            // 
            this.txtLicenseNo.Location = new System.Drawing.Point(80, 40);
            this.txtLicenseNo.Name = "txtLicenseNo";
            this.txtLicenseNo.Size = new System.Drawing.Size(152, 20);
            this.txtLicenseNo.TabIndex = 4;
            this.txtLicenseNo.TextChanged += new System.EventHandler(this.txtLicenseNo_TextChanged);
            this.txtLicenseNo.Leave += new System.EventHandler(this.txtLicenseNo_Leave);
            this.txtLicenseNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLicenseNo_KeyPress);
            this.txtLicenseNo.Enter += new System.EventHandler(this.txtLicenseNo_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "License No.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(238, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "*";
            // 
            // txtVehicleNo
            // 
            this.txtVehicleNo.Location = new System.Drawing.Point(80, 14);
            this.txtVehicleNo.Name = "txtVehicleNo";
            this.txtVehicleNo.Size = new System.Drawing.Size(152, 20);
            this.txtVehicleNo.TabIndex = 1;
            this.txtVehicleNo.TextChanged += new System.EventHandler(this.txtVehicleNo_TextChanged);
            this.txtVehicleNo.Leave += new System.EventHandler(this.txtVehicleNo_Leave);
            this.txtVehicleNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVehicleNo_KeyPress);
            this.txtVehicleNo.Enter += new System.EventHandler(this.txtVehicleNo_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vehicle No.";
            // 
            // grbButtons
            // 
            this.grbButtons.Controls.Add(this.btnCancel);
            this.grbButtons.Controls.Add(this.btnSave);
            this.grbButtons.Location = new System.Drawing.Point(2, 118);
            this.grbButtons.Name = "grbButtons";
            this.grbButtons.Size = new System.Drawing.Size(287, 51);
            this.grbButtons.TabIndex = 1;
            this.grbButtons.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(203, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(133, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(64, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmVehicleProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 172);
            this.Controls.Add(this.grbMain);
            this.Controls.Add(this.grbButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVehicleProp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vehicle";
            this.Load += new System.EventHandler(this.frmVehicleProp_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmVehicleProp_KeyDown);
            this.grbMain.ResumeLayout(false);
            this.grbMain.PerformLayout();
            this.grbButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbMain;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.TextBox txtLicenseNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtVehicleNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grbButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPUCExpiry;
    }
}