namespace RDP
{
    partial class frmUIProfileMaintenance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUIProfileMaintenance));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNewEntryNo = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpTransDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBtn = new System.Windows.Forms.TextBox();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnInsertSCreen = new System.Windows.Forms.Button();
            this.txtModuleName = new System.Windows.Forms.TextBox();
            this.txtSCreen = new System.Windows.Forms.TextBox();
            this.lblEmp = new System.Windows.Forms.Label();
            this.lstUIPfMaintenance = new System.Windows.Forms.ListView();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnEntryNoFinder = new System.Windows.Forms.Button();
            this.txtProfileDesc = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProfileId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblDebitCredit = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.txtFormName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNewEntryNo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpTransDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtBtn);
            this.groupBox1.Controls.Add(this.btnInsert);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnInsertSCreen);
            this.groupBox1.Controls.Add(this.txtModuleName);
            this.groupBox1.Controls.Add(this.txtSCreen);
            this.groupBox1.Controls.Add(this.lblEmp);
            this.groupBox1.Controls.Add(this.lstUIPfMaintenance);
            this.groupBox1.Controls.Add(this.btnLast);
            this.groupBox1.Controls.Add(this.btnNext);
            this.groupBox1.Controls.Add(this.btnPrevious);
            this.groupBox1.Controls.Add(this.btnFirst);
            this.groupBox1.Controls.Add(this.btnEntryNoFinder);
            this.groupBox1.Controls.Add(this.txtProfileDesc);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtProfileId);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.lblDebitCredit);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(5, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(678, 473);
            this.groupBox1.TabIndex = 207;
            this.groupBox1.TabStop = false;
            // 
            // btnNewEntryNo
            // 
            this.btnNewEntryNo.Image = global::RDP.Properties.Resources.New;
            this.btnNewEntryNo.Location = new System.Drawing.Point(364, 13);
            this.btnNewEntryNo.Name = "btnNewEntryNo";
            this.btnNewEntryNo.Size = new System.Drawing.Size(25, 23);
            this.btnNewEntryNo.TabIndex = 3180;
            this.btnNewEntryNo.UseVisualStyleBackColor = true;
            this.btnNewEntryNo.Click += new System.EventHandler(this.btnNewEntryNo_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label7.Location = new System.Drawing.Point(804, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 16);
            this.label7.TabIndex = 3179;
            this.label7.Text = "Id";
            // 
            // txtId
            // 
            this.txtId.Enabled = false;
            this.txtId.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtId.Location = new System.Drawing.Point(854, 74);
            this.txtId.MaxLength = 9;
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(65, 23);
            this.txtId.TabIndex = 3178;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label3.Location = new System.Drawing.Point(450, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 16);
            this.label3.TabIndex = 3177;
            this.label3.Text = "Transaction Date";
            // 
            // dtpTransDate
            // 
            this.dtpTransDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpTransDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTransDate.Location = new System.Drawing.Point(560, 42);
            this.dtpTransDate.Name = "dtpTransDate";
            this.dtpTransDate.Size = new System.Drawing.Size(111, 20);
            this.dtpTransDate.TabIndex = 3176;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label2.Location = new System.Drawing.Point(4, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 16);
            this.label2.TabIndex = 3175;
            this.label2.Text = "Module Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label1.Location = new System.Drawing.Point(516, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 3174;
            this.label1.Text = "Button";
            // 
            // txtBtn
            // 
            this.txtBtn.Enabled = false;
            this.txtBtn.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtBtn.Location = new System.Drawing.Point(561, 71);
            this.txtBtn.MaxLength = 9;
            this.txtBtn.Name = "txtBtn";
            this.txtBtn.Size = new System.Drawing.Size(110, 23);
            this.txtBtn.TabIndex = 3173;
            // 
            // btnInsert
            // 
            this.btnInsert.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.btnInsert.Location = new System.Drawing.Point(505, 100);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(80, 23);
            this.btnInsert.TabIndex = 3172;
            this.btnInsert.Text = "&Insert";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.btnDelete.Location = new System.Drawing.Point(591, 100);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 23);
            this.btnDelete.TabIndex = 3171;
            this.btnDelete.Text = "&Remove";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnInsertSCreen
            // 
            this.btnInsertSCreen.Image = global::RDP.Properties.Resources.finder;
            this.btnInsertSCreen.Location = new System.Drawing.Point(256, 71);
            this.btnInsertSCreen.Name = "btnInsertSCreen";
            this.btnInsertSCreen.Size = new System.Drawing.Size(25, 23);
            this.btnInsertSCreen.TabIndex = 3170;
            this.btnInsertSCreen.UseVisualStyleBackColor = true;
            this.btnInsertSCreen.Click += new System.EventHandler(this.btnInsertSCreen_Click);
            // 
            // txtModuleName
            // 
            this.txtModuleName.Enabled = false;
            this.txtModuleName.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtModuleName.Location = new System.Drawing.Point(117, 71);
            this.txtModuleName.MaxLength = 9;
            this.txtModuleName.Name = "txtModuleName";
            this.txtModuleName.Size = new System.Drawing.Size(138, 23);
            this.txtModuleName.TabIndex = 3169;
            // 
            // txtSCreen
            // 
            this.txtSCreen.Enabled = false;
            this.txtSCreen.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtSCreen.Location = new System.Drawing.Point(332, 71);
            this.txtSCreen.MaxLength = 9;
            this.txtSCreen.Name = "txtSCreen";
            this.txtSCreen.Size = new System.Drawing.Size(182, 23);
            this.txtSCreen.TabIndex = 3167;
            // 
            // lblEmp
            // 
            this.lblEmp.AutoSize = true;
            this.lblEmp.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.lblEmp.Location = new System.Drawing.Point(284, 74);
            this.lblEmp.Name = "lblEmp";
            this.lblEmp.Size = new System.Drawing.Size(48, 16);
            this.lblEmp.TabIndex = 3168;
            this.lblEmp.Text = "Screen";
            // 
            // lstUIPfMaintenance
            // 
            this.lstUIPfMaintenance.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.lstUIPfMaintenance.Location = new System.Drawing.Point(1, 132);
            this.lstUIPfMaintenance.Name = "lstUIPfMaintenance";
            this.lstUIPfMaintenance.Size = new System.Drawing.Size(670, 335);
            this.lstUIPfMaintenance.TabIndex = 3154;
            this.lstUIPfMaintenance.UseCompatibleStateImageBehavior = false;
            // 
            // btnLast
            // 
            this.btnLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLast.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.Image")));
            this.btnLast.Location = new System.Drawing.Point(316, 13);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(25, 23);
            this.btnLast.TabIndex = 2231;
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.Location = new System.Drawing.Point(294, 13);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(25, 23);
            this.btnNext.TabIndex = 2230;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrevious.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevious.Image")));
            this.btnPrevious.Location = new System.Drawing.Point(139, 13);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(25, 23);
            this.btnPrevious.TabIndex = 2229;
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.Image")));
            this.btnFirst.Location = new System.Drawing.Point(117, 13);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(25, 23);
            this.btnFirst.TabIndex = 2228;
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnEntryNoFinder
            // 
            this.btnEntryNoFinder.Image = global::RDP.Properties.Resources.finder;
            this.btnEntryNoFinder.Location = new System.Drawing.Point(340, 13);
            this.btnEntryNoFinder.Name = "btnEntryNoFinder";
            this.btnEntryNoFinder.Size = new System.Drawing.Size(25, 23);
            this.btnEntryNoFinder.TabIndex = 2226;
            this.btnEntryNoFinder.UseVisualStyleBackColor = true;
            this.btnEntryNoFinder.Click += new System.EventHandler(this.btnEntryNoFinder_Click);
            // 
            // txtProfileDesc
            // 
            this.txtProfileDesc.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtProfileDesc.Location = new System.Drawing.Point(117, 42);
            this.txtProfileDesc.MaxLength = 50;
            this.txtProfileDesc.Name = "txtProfileDesc";
            this.txtProfileDesc.Size = new System.Drawing.Size(317, 23);
            this.txtProfileDesc.TabIndex = 2225;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label5.Location = new System.Drawing.Point(4, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 16);
            this.label5.TabIndex = 2224;
            this.label5.Text = "Profile Description";
            // 
            // txtProfileId
            // 
            this.txtProfileId.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtProfileId.Location = new System.Drawing.Point(164, 13);
            this.txtProfileId.MaxLength = 10;
            this.txtProfileId.Name = "txtProfileId";
            this.txtProfileId.Size = new System.Drawing.Size(130, 23);
            this.txtProfileId.TabIndex = 2223;
            this.txtProfileId.TextChanged += new System.EventHandler(this.txtProfileId_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label6.Location = new System.Drawing.Point(4, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 16);
            this.label6.TabIndex = 2222;
            this.label6.Text = "Profile ID";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1067, 232);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 208;
            this.textBox1.Text = "0.00";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDebitCredit
            // 
            this.lblDebitCredit.AutoSize = true;
            this.lblDebitCredit.Location = new System.Drawing.Point(246, 27);
            this.lblDebitCredit.Name = "lblDebitCredit";
            this.lblDebitCredit.Size = new System.Drawing.Size(0, 13);
            this.lblDebitCredit.TabIndex = 205;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(945, 235);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 203;
            this.label4.Text = "Opening Balance:";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(597, 481);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 209;
            this.cmdCancel.Text = "Close";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // txtFormName
            // 
            this.txtFormName.Enabled = false;
            this.txtFormName.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtFormName.Location = new System.Drawing.Point(812, 98);
            this.txtFormName.MaxLength = 9;
            this.txtFormName.Name = "txtFormName";
            this.txtFormName.Size = new System.Drawing.Size(110, 23);
            this.txtFormName.TabIndex = 3174;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label8.Location = new System.Drawing.Point(731, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 16);
            this.label8.TabIndex = 3175;
            this.label8.Text = "Form Name";
            // 
            // frmUIProfileMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 507);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtFormName);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmUIProfileMaintenance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UI Profile Maintenance";
            this.Load += new System.EventHandler(this.frmUIProfileMaintenance_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnEntryNoFinder;
        private System.Windows.Forms.TextBox txtProfileDesc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProfileId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblDebitCredit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ListView lstUIPfMaintenance;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnInsertSCreen;
        private System.Windows.Forms.TextBox txtModuleName;
        private System.Windows.Forms.TextBox txtSCreen;
        private System.Windows.Forms.Label lblEmp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpTransDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Button btnNewEntryNo;
        private System.Windows.Forms.TextBox txtFormName;
        private System.Windows.Forms.Label label8;
    }
}