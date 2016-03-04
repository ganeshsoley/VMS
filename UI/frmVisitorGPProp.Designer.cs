namespace UI
{
    partial class frmVisitorGPProp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVisitorGPProp));
            this.grbMain = new System.Windows.Forms.GroupBox();
            this.dtpInTime = new System.Windows.Forms.DateTimePicker();
            this.txtGateDate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grbPhoto = new System.Windows.Forms.GroupBox();
            this.picVideo = new System.Windows.Forms.PictureBox();
            this.btnCaptureImg = new System.Windows.Forms.Button();
            this.btnStartCam = new System.Windows.Forms.Button();
            this.picVisitorImg = new System.Windows.Forms.PictureBox();
            this.grbMaterial = new System.Windows.Forms.GroupBox();
            this.txtCarryingMaterial = new System.Windows.Forms.TextBox();
            this.txtDepositedMaterial = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.grbEmployee = new System.Windows.Forms.GroupBox();
            this.btnEmpList = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.lblEmpContact = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblEmpDept = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblToMeet = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.grbVisitor = new System.Windows.Forms.GroupBox();
            this.lblNoOfPersons = new System.Windows.Forms.Label();
            this.lblVisitorName = new System.Windows.Forms.Label();
            this.lblAppointmentNo = new System.Windows.Forms.Label();
            this.btnVisitorList = new System.Windows.Forms.Button();
            this.btnAppointmentList = new System.Windows.Forms.Button();
            this.txtVehicleNo = new System.Windows.Forms.TextBox();
            this.lblContactNo = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.cboPurpose = new System.Windows.Forms.ComboBox();
            this.txtExPersons = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grbButtons = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.saveImg = new System.Windows.Forms.SaveFileDialog();
            this.grbMain.SuspendLayout();
            this.grbPhoto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picVisitorImg)).BeginInit();
            this.grbMaterial.SuspendLayout();
            this.grbEmployee.SuspendLayout();
            this.grbVisitor.SuspendLayout();
            this.grbButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbMain
            // 
            this.grbMain.BackColor = System.Drawing.SystemColors.Control;
            this.grbMain.Controls.Add(this.dtpInTime);
            this.grbMain.Controls.Add(this.txtGateDate);
            this.grbMain.Controls.Add(this.label4);
            this.grbMain.Controls.Add(this.label3);
            this.grbMain.Controls.Add(this.label2);
            this.grbMain.Controls.Add(this.label1);
            this.grbMain.Controls.Add(this.grbPhoto);
            this.grbMain.Controls.Add(this.grbMaterial);
            this.grbMain.Controls.Add(this.grbEmployee);
            this.grbMain.Controls.Add(this.grbVisitor);
            this.grbMain.Location = new System.Drawing.Point(2, -2);
            this.grbMain.Name = "grbMain";
            this.grbMain.Size = new System.Drawing.Size(742, 390);
            this.grbMain.TabIndex = 0;
            this.grbMain.TabStop = false;
            // 
            // dtpInTime
            // 
            this.dtpInTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpInTime.Location = new System.Drawing.Point(215, 17);
            this.dtpInTime.Name = "dtpInTime";
            this.dtpInTime.Size = new System.Drawing.Size(96, 20);
            this.dtpInTime.TabIndex = 3;
            this.dtpInTime.ValueChanged += new System.EventHandler(this.dtpInTime_ValueChanged);
            this.dtpInTime.Leave += new System.EventHandler(this.dtpInTime_Leave);
            this.dtpInTime.Validating += new System.ComponentModel.CancelEventHandler(this.dtpInTime_Validating);
            // 
            // txtGateDate
            // 
            this.txtGateDate.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtGateDate.Enabled = false;
            this.txtGateDate.Location = new System.Drawing.Point(59, 17);
            this.txtGateDate.Name = "txtGateDate";
            this.txtGateDate.Size = new System.Drawing.Size(87, 20);
            this.txtGateDate.TabIndex = 1;
            this.txtGateDate.TextChanged += new System.EventHandler(this.txtGateDate_TextChanged);
            this.txtGateDate.Enter += new System.EventHandler(this.txtGateDate_Enter);
            this.txtGateDate.Leave += new System.EventHandler(this.txtGateDate_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(312, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(179, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Time";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(146, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date";
            // 
            // grbPhoto
            // 
            this.grbPhoto.Controls.Add(this.picVideo);
            this.grbPhoto.Controls.Add(this.btnCaptureImg);
            this.grbPhoto.Controls.Add(this.btnStartCam);
            this.grbPhoto.Controls.Add(this.picVisitorImg);
            this.grbPhoto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbPhoto.Location = new System.Drawing.Point(453, 40);
            this.grbPhoto.Name = "grbPhoto";
            this.grbPhoto.Size = new System.Drawing.Size(285, 340);
            this.grbPhoto.TabIndex = 8;
            this.grbPhoto.TabStop = false;
            this.grbPhoto.Text = "Visitor Photo";
            // 
            // picVideo
            // 
            this.picVideo.Location = new System.Drawing.Point(15, 21);
            this.picVideo.Name = "picVideo";
            this.picVideo.Size = new System.Drawing.Size(254, 252);
            this.picVideo.TabIndex = 3;
            this.picVideo.TabStop = false;
            // 
            // btnCaptureImg
            // 
            this.btnCaptureImg.BackColor = System.Drawing.SystemColors.Control;
            this.btnCaptureImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaptureImg.Location = new System.Drawing.Point(158, 295);
            this.btnCaptureImg.Name = "btnCaptureImg";
            this.btnCaptureImg.Size = new System.Drawing.Size(76, 23);
            this.btnCaptureImg.TabIndex = 2;
            this.btnCaptureImg.Text = "Capture";
            this.btnCaptureImg.UseVisualStyleBackColor = false;
            this.btnCaptureImg.Click += new System.EventHandler(this.btnCaptureImg_Click);
            // 
            // btnStartCam
            // 
            this.btnStartCam.BackColor = System.Drawing.SystemColors.Control;
            this.btnStartCam.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartCam.Location = new System.Drawing.Point(47, 295);
            this.btnStartCam.Name = "btnStartCam";
            this.btnStartCam.Size = new System.Drawing.Size(76, 23);
            this.btnStartCam.TabIndex = 1;
            this.btnStartCam.Text = "Start Camera";
            this.btnStartCam.UseVisualStyleBackColor = false;
            this.btnStartCam.Click += new System.EventHandler(this.btnStartCam_Click);
            // 
            // picVisitorImg
            // 
            this.picVisitorImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picVisitorImg.Location = new System.Drawing.Point(15, 21);
            this.picVisitorImg.Name = "picVisitorImg";
            this.picVisitorImg.Size = new System.Drawing.Size(254, 252);
            this.picVisitorImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picVisitorImg.TabIndex = 0;
            this.picVisitorImg.TabStop = false;
            // 
            // grbMaterial
            // 
            this.grbMaterial.Controls.Add(this.txtCarryingMaterial);
            this.grbMaterial.Controls.Add(this.txtDepositedMaterial);
            this.grbMaterial.Controls.Add(this.label24);
            this.grbMaterial.Controls.Add(this.label23);
            this.grbMaterial.Location = new System.Drawing.Point(6, 303);
            this.grbMaterial.Name = "grbMaterial";
            this.grbMaterial.Size = new System.Drawing.Size(441, 77);
            this.grbMaterial.TabIndex = 7;
            this.grbMaterial.TabStop = false;
            // 
            // txtCarryingMaterial
            // 
            this.txtCarryingMaterial.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtCarryingMaterial.Location = new System.Drawing.Point(106, 45);
            this.txtCarryingMaterial.Name = "txtCarryingMaterial";
            this.txtCarryingMaterial.Size = new System.Drawing.Size(273, 20);
            this.txtCarryingMaterial.TabIndex = 3;
            this.txtCarryingMaterial.TextChanged += new System.EventHandler(this.txtCarryingMaterial_TextChanged);
            this.txtCarryingMaterial.Enter += new System.EventHandler(this.txtCarryingMaterial_Enter);
            this.txtCarryingMaterial.Leave += new System.EventHandler(this.txtCarryingMaterial_Leave);
            // 
            // txtDepositedMaterial
            // 
            this.txtDepositedMaterial.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtDepositedMaterial.Location = new System.Drawing.Point(106, 16);
            this.txtDepositedMaterial.Name = "txtDepositedMaterial";
            this.txtDepositedMaterial.Size = new System.Drawing.Size(273, 20);
            this.txtDepositedMaterial.TabIndex = 2;
            this.txtDepositedMaterial.TextChanged += new System.EventHandler(this.txtDepositedMaterial_TextChanged);
            this.txtDepositedMaterial.Enter += new System.EventHandler(this.txtDepositedMaterial_Enter);
            this.txtDepositedMaterial.Leave += new System.EventHandler(this.txtDepositedMaterial_Leave);
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(17, 45);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(51, 30);
            this.label24.TabIndex = 1;
            this.label24.Text = "Carrying Material";
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(17, 16);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(64, 33);
            this.label23.TabIndex = 0;
            this.label23.Text = "Deposited Material";
            // 
            // grbEmployee
            // 
            this.grbEmployee.Controls.Add(this.btnEmpList);
            this.grbEmployee.Controls.Add(this.label22);
            this.grbEmployee.Controls.Add(this.lblEmpContact);
            this.grbEmployee.Controls.Add(this.label20);
            this.grbEmployee.Controls.Add(this.lblEmpDept);
            this.grbEmployee.Controls.Add(this.label18);
            this.grbEmployee.Controls.Add(this.lblToMeet);
            this.grbEmployee.Controls.Add(this.label16);
            this.grbEmployee.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbEmployee.Location = new System.Drawing.Point(6, 227);
            this.grbEmployee.Name = "grbEmployee";
            this.grbEmployee.Size = new System.Drawing.Size(441, 76);
            this.grbEmployee.TabIndex = 6;
            this.grbEmployee.TabStop = false;
            this.grbEmployee.Text = "Employee Detail";
            // 
            // btnEmpList
            // 
            this.btnEmpList.BackColor = System.Drawing.SystemColors.Control;
            this.btnEmpList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmpList.Image = ((System.Drawing.Image)(resources.GetObject("btnEmpList.Image")));
            this.btnEmpList.Location = new System.Drawing.Point(401, 22);
            this.btnEmpList.Name = "btnEmpList";
            this.btnEmpList.Size = new System.Drawing.Size(23, 23);
            this.btnEmpList.TabIndex = 2;
            this.btnEmpList.UseVisualStyleBackColor = false;
            this.btnEmpList.Click += new System.EventHandler(this.btnEmpList_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(384, 22);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(11, 13);
            this.label22.TabIndex = 6;
            this.label22.Text = "*";
            // 
            // lblEmpContact
            // 
            this.lblEmpContact.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblEmpContact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEmpContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpContact.Location = new System.Drawing.Point(292, 45);
            this.lblEmpContact.Name = "lblEmpContact";
            this.lblEmpContact.Size = new System.Drawing.Size(90, 20);
            this.lblEmpContact.TabIndex = 6;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(246, 46);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(38, 13);
            this.label20.TabIndex = 5;
            this.label20.Text = "Mobile";
            // 
            // lblEmpDept
            // 
            this.lblEmpDept.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblEmpDept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEmpDept.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpDept.Location = new System.Drawing.Point(106, 46);
            this.lblEmpDept.Name = "lblEmpDept";
            this.lblEmpDept.Size = new System.Drawing.Size(134, 20);
            this.lblEmpDept.TabIndex = 4;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(17, 46);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(62, 13);
            this.label18.TabIndex = 3;
            this.label18.Text = "Department";
            // 
            // lblToMeet
            // 
            this.lblToMeet.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblToMeet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblToMeet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToMeet.Location = new System.Drawing.Point(106, 22);
            this.lblToMeet.Name = "lblToMeet";
            this.lblToMeet.Size = new System.Drawing.Size(276, 20);
            this.lblToMeet.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(17, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(47, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "To Meet";
            // 
            // grbVisitor
            // 
            this.grbVisitor.Controls.Add(this.lblNoOfPersons);
            this.grbVisitor.Controls.Add(this.lblVisitorName);
            this.grbVisitor.Controls.Add(this.lblAppointmentNo);
            this.grbVisitor.Controls.Add(this.btnVisitorList);
            this.grbVisitor.Controls.Add(this.btnAppointmentList);
            this.grbVisitor.Controls.Add(this.txtVehicleNo);
            this.grbVisitor.Controls.Add(this.lblContactNo);
            this.grbVisitor.Controls.Add(this.lblAddress);
            this.grbVisitor.Controls.Add(this.lblCompany);
            this.grbVisitor.Controls.Add(this.cboPurpose);
            this.grbVisitor.Controls.Add(this.txtExPersons);
            this.grbVisitor.Controls.Add(this.label15);
            this.grbVisitor.Controls.Add(this.label14);
            this.grbVisitor.Controls.Add(this.label13);
            this.grbVisitor.Controls.Add(this.label12);
            this.grbVisitor.Controls.Add(this.label11);
            this.grbVisitor.Controls.Add(this.label10);
            this.grbVisitor.Controls.Add(this.label9);
            this.grbVisitor.Controls.Add(this.label8);
            this.grbVisitor.Controls.Add(this.label7);
            this.grbVisitor.Controls.Add(this.label6);
            this.grbVisitor.Controls.Add(this.label5);
            this.grbVisitor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbVisitor.Location = new System.Drawing.Point(6, 40);
            this.grbVisitor.Name = "grbVisitor";
            this.grbVisitor.Size = new System.Drawing.Size(441, 186);
            this.grbVisitor.TabIndex = 5;
            this.grbVisitor.TabStop = false;
            this.grbVisitor.Text = "Visitor Detail";
            // 
            // lblNoOfPersons
            // 
            this.lblNoOfPersons.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblNoOfPersons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNoOfPersons.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOfPersons.Location = new System.Drawing.Point(109, 71);
            this.lblNoOfPersons.Name = "lblNoOfPersons";
            this.lblNoOfPersons.Size = new System.Drawing.Size(42, 20);
            this.lblNoOfPersons.TabIndex = 13;
            this.lblNoOfPersons.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblVisitorName
            // 
            this.lblVisitorName.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblVisitorName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVisitorName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVisitorName.Location = new System.Drawing.Point(109, 45);
            this.lblVisitorName.Name = "lblVisitorName";
            this.lblVisitorName.Size = new System.Drawing.Size(273, 20);
            this.lblVisitorName.TabIndex = 11;
            // 
            // lblAppointmentNo
            // 
            this.lblAppointmentNo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblAppointmentNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAppointmentNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppointmentNo.Location = new System.Drawing.Point(109, 20);
            this.lblAppointmentNo.Name = "lblAppointmentNo";
            this.lblAppointmentNo.Size = new System.Drawing.Size(107, 20);
            this.lblAppointmentNo.TabIndex = 9;
            // 
            // btnVisitorList
            // 
            this.btnVisitorList.BackColor = System.Drawing.SystemColors.Control;
            this.btnVisitorList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVisitorList.Image = ((System.Drawing.Image)(resources.GetObject("btnVisitorList.Image")));
            this.btnVisitorList.Location = new System.Drawing.Point(401, 47);
            this.btnVisitorList.Name = "btnVisitorList";
            this.btnVisitorList.Size = new System.Drawing.Size(23, 23);
            this.btnVisitorList.TabIndex = 12;
            this.btnVisitorList.UseVisualStyleBackColor = false;
            this.btnVisitorList.Click += new System.EventHandler(this.btnVisitorList_Click);
            // 
            // btnAppointmentList
            // 
            this.btnAppointmentList.BackColor = System.Drawing.SystemColors.Control;
            this.btnAppointmentList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAppointmentList.Image = ((System.Drawing.Image)(resources.GetObject("btnAppointmentList.Image")));
            this.btnAppointmentList.Location = new System.Drawing.Point(222, 20);
            this.btnAppointmentList.Name = "btnAppointmentList";
            this.btnAppointmentList.Size = new System.Drawing.Size(23, 23);
            this.btnAppointmentList.TabIndex = 10;
            this.btnAppointmentList.UseVisualStyleBackColor = false;
            this.btnAppointmentList.Click += new System.EventHandler(this.btnAppointmentList_Click);
            // 
            // txtVehicleNo
            // 
            this.txtVehicleNo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtVehicleNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVehicleNo.Location = new System.Drawing.Point(285, 149);
            this.txtVehicleNo.Name = "txtVehicleNo";
            this.txtVehicleNo.Size = new System.Drawing.Size(97, 20);
            this.txtVehicleNo.TabIndex = 19;
            this.txtVehicleNo.TextChanged += new System.EventHandler(this.txtVehicleNo_TextChanged);
            this.txtVehicleNo.Enter += new System.EventHandler(this.txtVehicleNo_Enter);
            this.txtVehicleNo.Leave += new System.EventHandler(this.txtVehicleNo_Leave);
            // 
            // lblContactNo
            // 
            this.lblContactNo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblContactNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblContactNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactNo.Location = new System.Drawing.Point(109, 149);
            this.lblContactNo.Name = "lblContactNo";
            this.lblContactNo.Size = new System.Drawing.Size(107, 20);
            this.lblContactNo.TabIndex = 18;
            // 
            // lblAddress
            // 
            this.lblAddress.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.Location = new System.Drawing.Point(109, 122);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(273, 20);
            this.lblAddress.TabIndex = 17;
            this.lblAddress.Visible = false;
            // 
            // lblCompany
            // 
            this.lblCompany.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.Location = new System.Drawing.Point(109, 95);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(273, 20);
            this.lblCompany.TabIndex = 16;
            // 
            // cboPurpose
            // 
            this.cboPurpose.BackColor = System.Drawing.SystemColors.HighlightText;
            this.cboPurpose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPurpose.FormattingEnabled = true;
            this.cboPurpose.Location = new System.Drawing.Point(285, 70);
            this.cboPurpose.Name = "cboPurpose";
            this.cboPurpose.Size = new System.Drawing.Size(97, 21);
            this.cboPurpose.TabIndex = 15;
            this.cboPurpose.SelectedIndexChanged += new System.EventHandler(this.cboPurpose_SelectedIndexChanged);
            this.cboPurpose.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboPurpose_KeyPress);
            this.cboPurpose.Leave += new System.EventHandler(this.cboPurpose_Leave);
            // 
            // txtExPersons
            // 
            this.txtExPersons.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtExPersons.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExPersons.Location = new System.Drawing.Point(176, 71);
            this.txtExPersons.Name = "txtExPersons";
            this.txtExPersons.Size = new System.Drawing.Size(43, 20);
            this.txtExPersons.TabIndex = 14;
            this.txtExPersons.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtExPersons.TextChanged += new System.EventHandler(this.txtExPersons_TextChanged);
            this.txtExPersons.Enter += new System.EventHandler(this.txtExPersons_Enter);
            this.txtExPersons.Leave += new System.EventHandler(this.txtExPersons_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(384, 70);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(11, 13);
            this.label15.TabIndex = 10;
            this.label15.Text = "*";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(384, 45);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(11, 13);
            this.label14.TabIndex = 9;
            this.label14.Text = "*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(222, 149);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "Vehicle No.";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(231, 71);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "Purpose";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(157, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(13, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "+";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(17, 149);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Contact No.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(17, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Address";
            this.label9.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(17, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Company";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(17, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "No. of Persons";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(17, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(17, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Appointment No.";
            // 
            // grbButtons
            // 
            this.grbButtons.BackColor = System.Drawing.SystemColors.Control;
            this.grbButtons.Controls.Add(this.btnCancel);
            this.grbButtons.Controls.Add(this.btnSave);
            this.grbButtons.Location = new System.Drawing.Point(2, 388);
            this.grbButtons.Name = "grbButtons";
            this.grbButtons.Size = new System.Drawing.Size(742, 61);
            this.grbButtons.TabIndex = 9;
            this.grbButtons.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Location = new System.Drawing.Point(658, 19);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.Location = new System.Drawing.Point(576, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(64, 24);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmVisitorGPProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(746, 451);
            this.Controls.Add(this.grbButtons);
            this.Controls.Add(this.grbMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVisitorGPProp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Visitor Gate Pass";
            this.Load += new System.EventHandler(this.frmVisitorGPProp_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmVisitorGPProp_KeyDown);
            this.grbMain.ResumeLayout(false);
            this.grbMain.PerformLayout();
            this.grbPhoto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picVisitorImg)).EndInit();
            this.grbMaterial.ResumeLayout(false);
            this.grbMaterial.PerformLayout();
            this.grbEmployee.ResumeLayout(false);
            this.grbEmployee.PerformLayout();
            this.grbVisitor.ResumeLayout(false);
            this.grbVisitor.PerformLayout();
            this.grbButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbMain;
        private System.Windows.Forms.GroupBox grbPhoto;
        private System.Windows.Forms.GroupBox grbMaterial;
        private System.Windows.Forms.GroupBox grbEmployee;
        private System.Windows.Forms.GroupBox grbVisitor;
        private System.Windows.Forms.GroupBox grbButtons;
        private System.Windows.Forms.TextBox txtGateDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpInTime;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtExPersons;
        private System.Windows.Forms.Label lblContactNo;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.ComboBox cboPurpose;
        private System.Windows.Forms.TextBox txtVehicleNo;
        private System.Windows.Forms.Button btnVisitorList;
        private System.Windows.Forms.Button btnAppointmentList;
        private System.Windows.Forms.Button btnEmpList;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lblEmpContact;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblEmpDept;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblToMeet;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtCarryingMaterial;
        private System.Windows.Forms.TextBox txtDepositedMaterial;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btnStartCam;
        private System.Windows.Forms.PictureBox picVisitorImg;
        private System.Windows.Forms.Button btnCaptureImg;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblAppointmentNo;
        private System.Windows.Forms.Label lblVisitorName;
        private System.Windows.Forms.Label lblNoOfPersons;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox picVideo;
        private System.Windows.Forms.SaveFileDialog saveImg;
    }
}