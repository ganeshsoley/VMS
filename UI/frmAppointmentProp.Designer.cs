namespace UI
{
    partial class frmAppointmentProp
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
            this.grbButtons = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.grbApmt = new System.Windows.Forms.GroupBox();
            this.dtpAppointDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAppointmentNo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grbVisitor = new System.Windows.Forms.GroupBox();
            this.dtpSchTime = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.lblContactNo = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grbEmployee = new System.Windows.Forms.GroupBox();
            this.btnEmployeeList = new System.Windows.Forms.Button();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblToMeet = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnVisitorList = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.grbButtons.SuspendLayout();
            this.grbApmt.SuspendLayout();
            this.grbVisitor.SuspendLayout();
            this.grbEmployee.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbButtons
            // 
            this.grbButtons.Controls.Add(this.btnCancel);
            this.grbButtons.Controls.Add(this.btnSave);
            this.grbButtons.Location = new System.Drawing.Point(2, 199);
            this.grbButtons.Name = "grbButtons";
            this.grbButtons.Size = new System.Drawing.Size(434, 54);
            this.grbButtons.TabIndex = 21;
            this.grbButtons.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(348, 19);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(264, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 22;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grbApmt
            // 
            this.grbApmt.Controls.Add(this.label2);
            this.grbApmt.Controls.Add(this.dtpAppointDate);
            this.grbApmt.Controls.Add(this.label3);
            this.grbApmt.Controls.Add(this.lblAppointmentNo);
            this.grbApmt.Controls.Add(this.label1);
            this.grbApmt.Location = new System.Drawing.Point(2, 3);
            this.grbApmt.Name = "grbApmt";
            this.grbApmt.Size = new System.Drawing.Size(433, 43);
            this.grbApmt.TabIndex = 0;
            this.grbApmt.TabStop = false;
            // 
            // dtpAppointDate
            // 
            this.dtpAppointDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAppointDate.Location = new System.Drawing.Point(324, 12);
            this.dtpAppointDate.Name = "dtpAppointDate";
            this.dtpAppointDate.Size = new System.Drawing.Size(86, 20);
            this.dtpAppointDate.TabIndex = 4;
            this.dtpAppointDate.Value = new System.DateTime(2015, 12, 18, 0, 0, 0, 0);
            this.dtpAppointDate.ValueChanged += new System.EventHandler(this.dtpAppointDate_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(226, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Appointment Date";
            // 
            // lblAppointmentNo
            // 
            this.lblAppointmentNo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblAppointmentNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAppointmentNo.Location = new System.Drawing.Point(101, 16);
            this.lblAppointmentNo.Name = "lblAppointmentNo";
            this.lblAppointmentNo.Size = new System.Drawing.Size(111, 18);
            this.lblAppointmentNo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "AppointmentNo ";
            // 
            // grbVisitor
            // 
            this.grbVisitor.Controls.Add(this.label10);
            this.grbVisitor.Controls.Add(this.label4);
            this.grbVisitor.Controls.Add(this.btnVisitorList);
            this.grbVisitor.Controls.Add(this.dtpSchTime);
            this.grbVisitor.Controls.Add(this.label9);
            this.grbVisitor.Controls.Add(this.lblContactNo);
            this.grbVisitor.Controls.Add(this.label8);
            this.grbVisitor.Controls.Add(this.lblCompany);
            this.grbVisitor.Controls.Add(this.label6);
            this.grbVisitor.Controls.Add(this.lblName);
            this.grbVisitor.Controls.Add(this.label5);
            this.grbVisitor.Location = new System.Drawing.Point(2, 46);
            this.grbVisitor.Name = "grbVisitor";
            this.grbVisitor.Size = new System.Drawing.Size(434, 88);
            this.grbVisitor.TabIndex = 5;
            this.grbVisitor.TabStop = false;
            // 
            // dtpSchTime
            // 
            this.dtpSchTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpSchTime.Location = new System.Drawing.Point(324, 60);
            this.dtpSchTime.Name = "dtpSchTime";
            this.dtpSchTime.Size = new System.Drawing.Size(86, 20);
            this.dtpSchTime.TabIndex = 14;
            this.dtpSchTime.ValueChanged += new System.EventHandler(this.dtpSchTime_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(226, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Schedule Time";
            // 
            // lblContactNo
            // 
            this.lblContactNo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblContactNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblContactNo.Location = new System.Drawing.Point(101, 60);
            this.lblContactNo.Name = "lblContactNo";
            this.lblContactNo.Size = new System.Drawing.Size(102, 18);
            this.lblContactNo.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Contact No";
            // 
            // lblCompany
            // 
            this.lblCompany.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCompany.Location = new System.Drawing.Point(101, 38);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(252, 18);
            this.lblCompany.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Company";
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblName.Location = new System.Drawing.Point(101, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(252, 18);
            this.lblName.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Name";
            // 
            // grbEmployee
            // 
            this.grbEmployee.Controls.Add(this.label7);
            this.grbEmployee.Controls.Add(this.btnEmployeeList);
            this.grbEmployee.Controls.Add(this.lblDepartment);
            this.grbEmployee.Controls.Add(this.label14);
            this.grbEmployee.Controls.Add(this.lblToMeet);
            this.grbEmployee.Controls.Add(this.label12);
            this.grbEmployee.Location = new System.Drawing.Point(2, 134);
            this.grbEmployee.Name = "grbEmployee";
            this.grbEmployee.Size = new System.Drawing.Size(434, 64);
            this.grbEmployee.TabIndex = 15;
            this.grbEmployee.TabStop = false;
            // 
            // btnEmployeeList
            // 
            this.btnEmployeeList.Image = global::UI.Properties.Resources.Search16x16;
            this.btnEmployeeList.Location = new System.Drawing.Point(362, 13);
            this.btnEmployeeList.Name = "btnEmployeeList";
            this.btnEmployeeList.Size = new System.Drawing.Size(23, 23);
            this.btnEmployeeList.TabIndex = 18;
            this.btnEmployeeList.UseVisualStyleBackColor = true;
            this.btnEmployeeList.Click += new System.EventHandler(this.btnEmployeeList_Click);
            // 
            // lblDepartment
            // 
            this.lblDepartment.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDepartment.Location = new System.Drawing.Point(101, 38);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(164, 18);
            this.lblDepartment.TabIndex = 20;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 38);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(62, 13);
            this.label14.TabIndex = 19;
            this.label14.Text = "Department";
            // 
            // lblToMeet
            // 
            this.lblToMeet.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblToMeet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblToMeet.Location = new System.Drawing.Point(101, 16);
            this.lblToMeet.Name = "lblToMeet";
            this.lblToMeet.Size = new System.Drawing.Size(233, 18);
            this.lblToMeet.TabIndex = 17;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "To Meet";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(416, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(359, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(340, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(12, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "*";
            // 
            // btnVisitorList
            // 
            this.btnVisitorList.Image = global::UI.Properties.Resources.Search16x16;
            this.btnVisitorList.Location = new System.Drawing.Point(377, 16);
            this.btnVisitorList.Name = "btnVisitorList";
            this.btnVisitorList.Size = new System.Drawing.Size(23, 23);
            this.btnVisitorList.TabIndex = 8;
            this.btnVisitorList.UseVisualStyleBackColor = true;
            this.btnVisitorList.Click += new System.EventHandler(this.btnVisitorList_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(416, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(12, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "*";
            // 
            // frmAppointmentProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 255);
            this.Controls.Add(this.grbEmployee);
            this.Controls.Add(this.grbVisitor);
            this.Controls.Add(this.grbButtons);
            this.Controls.Add(this.grbApmt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAppointmentProp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Appointment";
            this.Load += new System.EventHandler(this.frmAppointmentProp_Load);
            this.grbButtons.ResumeLayout(false);
            this.grbApmt.ResumeLayout(false);
            this.grbApmt.PerformLayout();
            this.grbVisitor.ResumeLayout(false);
            this.grbVisitor.PerformLayout();
            this.grbEmployee.ResumeLayout(false);
            this.grbEmployee.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox grbApmt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAppointmentNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpAppointDate;
        private System.Windows.Forms.GroupBox grbVisitor;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpSchTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblContactNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox grbEmployee;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblToMeet;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnVisitorList;
        private System.Windows.Forms.Button btnEmployeeList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
    }
}