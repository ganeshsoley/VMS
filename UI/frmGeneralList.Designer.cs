namespace UI
{
    partial class frmGeneralList
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvwGeneral = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnSetQty = new System.Windows.Forms.Button();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalQty = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvwGeneral);
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(686, 298);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lvwGeneral
            // 
            this.lvwGeneral.FullRowSelect = true;
            this.lvwGeneral.GridLines = true;
            this.lvwGeneral.Location = new System.Drawing.Point(6, 7);
            this.lvwGeneral.MultiSelect = false;
            this.lvwGeneral.Name = "lvwGeneral";
            this.lvwGeneral.Size = new System.Drawing.Size(675, 285);
            this.lvwGeneral.TabIndex = 0;
            this.lvwGeneral.UseCompatibleStateImageBehavior = false;
            this.lvwGeneral.View = System.Windows.Forms.View.Details;
            this.lvwGeneral.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwGeneral_ColumnClick);
            this.lvwGeneral.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvwGeneral_ItemCheck);
            this.lvwGeneral.Click += new System.EventHandler(this.lvwGeneral_Click);
            this.lvwGeneral.DoubleClick += new System.EventHandler(this.lvwGeneral_DoubleClick);
            this.lvwGeneral.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvwGeneral_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.btnOk);
            this.groupBox2.Controls.Add(this.btnSetQty);
            this.groupBox2.Controls.Add(this.txtQty);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblTotalQty);
            this.groupBox2.Controls.Add(this.lblQty);
            this.groupBox2.Location = new System.Drawing.Point(2, 300);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(686, 59);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(609, 19);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(538, 19);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnSetQty
            // 
            this.btnSetQty.Location = new System.Drawing.Point(149, 17);
            this.btnSetQty.Name = "btnSetQty";
            this.btnSetQty.Size = new System.Drawing.Size(62, 22);
            this.btnSetQty.TabIndex = 4;
            this.btnSetQty.Text = "&Set";
            this.btnSetQty.UseVisualStyleBackColor = true;
            this.btnSetQty.Click += new System.EventHandler(this.btnSetQty_Click);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(43, 17);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(100, 20);
            this.txtQty.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(377, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "label3";
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.AutoSize = true;
            this.lblTotalQty.Location = new System.Drawing.Point(285, 17);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Size = new System.Drawing.Size(35, 13);
            this.lblTotalQty.TabIndex = 1;
            this.lblTotalQty.Text = "label2";
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Location = new System.Drawing.Point(11, 17);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(26, 13);
            this.lblQty.TabIndex = 0;
            this.lblQty.Text = "Qty.";
            // 
            // frmGeneralList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 363);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGeneralList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "General List";
            this.Load += new System.EventHandler(this.frmGeneralList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.ListView lvwGeneral;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalQty;
        private System.Windows.Forms.Label lblQty;
        public System.Windows.Forms.Button btnSetQty;
        public System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}