
namespace RDP.MODEL.ProductionPLanning
{
    partial class frmRSPCalculation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvRSP = new System.Windows.Forms.DataGridView();
            this.sl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SAMPLE_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SAMPLE_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MKT_UOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STKQTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.M1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRSP)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvRSP
            // 
            this.dgvRSP.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvRSP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRSP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sl,
            this.SAMPLE_ID,
            this.SAMPLE_Name,
            this.UOM,
            this.MKT_UOM,
            this.STKQTY,
            this.M1,
            this.Column8,
            this.Column9});
            this.dgvRSP.Location = new System.Drawing.Point(4, 6);
            this.dgvRSP.Name = "dgvRSP";
            this.dgvRSP.RowHeadersVisible = false;
            this.dgvRSP.RowHeadersWidth = 51;
            this.dgvRSP.Size = new System.Drawing.Size(686, 208);
            this.dgvRSP.TabIndex = 0;
            this.dgvRSP.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvRSP_CellPainting);
            this.dgvRSP.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvRSP_ColumnWidthChanged);
            this.dgvRSP.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvRSP_Scroll);
            this.dgvRSP.Paint += new System.Windows.Forms.PaintEventHandler(this.dgvRSP_Paint);
            // 
            // sl
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.sl.DefaultCellStyle = dataGridViewCellStyle1;
            this.sl.HeaderText = "SL No";
            this.sl.MinimumWidth = 6;
            this.sl.Name = "sl";
            this.sl.Width = 60;
            // 
            // SAMPLE_ID
            // 
            this.SAMPLE_ID.DataPropertyName = "SAMPLE_ID";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SAMPLE_ID.DefaultCellStyle = dataGridViewCellStyle2;
            this.SAMPLE_ID.HeaderText = "Sample ID";
            this.SAMPLE_ID.MinimumWidth = 6;
            this.SAMPLE_ID.Name = "SAMPLE_ID";
            this.SAMPLE_ID.Width = 90;
            // 
            // SAMPLE_Name
            // 
            this.SAMPLE_Name.DataPropertyName = "SAMPLE_Name";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SAMPLE_Name.DefaultCellStyle = dataGridViewCellStyle3;
            this.SAMPLE_Name.HeaderText = "Sample Name";
            this.SAMPLE_Name.MinimumWidth = 6;
            this.SAMPLE_Name.Name = "SAMPLE_Name";
            this.SAMPLE_Name.Width = 160;
            // 
            // UOM
            // 
            this.UOM.DataPropertyName = "UOM";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.UOM.DefaultCellStyle = dataGridViewCellStyle4;
            this.UOM.HeaderText = "UOM";
            this.UOM.MinimumWidth = 6;
            this.UOM.Name = "UOM";
            this.UOM.Width = 60;
            // 
            // MKT_UOM
            // 
            this.MKT_UOM.DataPropertyName = "MKT_UOM";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MKT_UOM.DefaultCellStyle = dataGridViewCellStyle5;
            this.MKT_UOM.HeaderText = "C/F";
            this.MKT_UOM.MinimumWidth = 6;
            this.MKT_UOM.Name = "MKT_UOM";
            this.MKT_UOM.Width = 60;
            // 
            // STKQTY
            // 
            this.STKQTY.DataPropertyName = "STKQTY";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.STKQTY.DefaultCellStyle = dataGridViewCellStyle6;
            this.STKQTY.HeaderText = "Stock";
            this.STKQTY.MinimumWidth = 6;
            this.STKQTY.Name = "STKQTY";
            this.STKQTY.Width = 60;
            // 
            // M1
            // 
            this.M1.DataPropertyName = "Month1";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.M1.DefaultCellStyle = dataGridViewCellStyle7;
            this.M1.HeaderText = "April-22";
            this.M1.MinimumWidth = 6;
            this.M1.Name = "M1";
            this.M1.Width = 65;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "Month2";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column8.DefaultCellStyle = dataGridViewCellStyle8;
            this.Column8.HeaderText = "May-22";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            this.Column8.Width = 65;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "Month3";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column9.DefaultCellStyle = dataGridViewCellStyle9;
            this.Column9.HeaderText = "June-22";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            this.Column9.Width = 65;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnCalculate);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Location = new System.Drawing.Point(4, 215);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(686, 46);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(601, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(6, 12);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 28);
            this.btnCalculate.TabIndex = 0;
            this.btnCalculate.Text = "Add";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 28);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // frmRSPCalculation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 266);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvRSP);
            this.Name = "frmRSPCalculation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RSP Calculation";
            this.Load += new System.EventHandler(this.frmRSPCalculation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRSP)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRSP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn sl;
        private System.Windows.Forms.DataGridViewTextBoxColumn SAMPLE_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SAMPLE_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn MKT_UOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn STKQTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn M1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
    }
}