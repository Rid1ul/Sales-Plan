using RDP.DAL;
using RDP.UI.Target_Management;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RDP 
{ 
    public partial class BatchList : Form
    {
        string strSQL,sqlStrUIPROFILEMAINTAINANCE;
        DataTable dt = new DataTable();
        dataProvider lstData = new dataProvider();
        private frmMain master;
        string NoOfEntries;
        int row, col;
        string dateValue;
        string total;
        string transNo;
        public BatchList(frmMain frm)
        {
            master = frm;
            InitializeComponent();
          //  lblUserName.Visible = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            lblUserName.Text = master.txtMainUserName;     
        
        }

        public BatchList()
        {
        }

        

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            LoadBatch();
        }

        private void dgvBatchList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnOpen.PerformClick();


          //  int col = e.ColumnIndex;
          //  int row = e.RowIndex;
          //  string TransNo = dgvBatchList.Rows[row].Cells[10].Value.ToString();
          //  int EntryNo = Convert.ToInt32(dgvBatchList.Rows[row].Cells[0].Value);
          // // frmTargetManagement aTarget = new frmTargetManagement(TransNo, EntryNo);


          //  frmTargetManagement userForm = new frmTargetManagement(lblUserName.Text, TransNo, EntryNo);
          ////  frmTargetManagement userForm = new frmTargetManagement(lblUserName.Text, "10000002", EntryNo);
          //  userForm.batchTotal = dgvBatchList.Rows[row].Cells[5].Value.ToString();
            
          //  userForm.ShowDialog();
          //  LoadBatch();
        }

        private void BatchList_Load(object sender, EventArgs e)
        {          
            dgvBatchList.RowHeadersVisible = false;            
            LoadBatch();

            this.dgvBatchList.RowsDefaultCellStyle.BackColor = Color.White;
            this.dgvBatchList.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            //  transNo = lstData.getResultString("select transno from item_production_header where BATCHNO = '"+dgvBatchList.Rows[]+"' and AUDTUSER = '3062'");

            buttonPrivilege();
        }

        private void buttonPrivilege()
        {
            strSQL = "select PROFILEID from UIASSIGNPROFILE where USERID='" + master.txtMainUserName + "'";
            DataTable dtableBtn = new DataTable();
            dataProvider dtproviderBtn = new dataProvider();
            dtableBtn = dtproviderBtn.getDataTable(strSQL, "UIASSIGNPROFILE");
            for (int i = 0; i < dtableBtn.Rows.Count; i++)
            {
                DataRow drow = dtableBtn.Rows[i];
                if (drow.RowState != DataRowState.Deleted)
                {
                    string proId = drow["PROFILEID"].ToString().Trim();
                    sqlStrUIPROFILEMAINTAINANCE = "select BUTTON from UIPROFILEMAINTAINANCE where PROFILEID='" + proId + "' and FORMNAME='" + this.Name + "'";
                    DataTable dtableUIPROFILEMAINTAINANCE = new DataTable();
                    dtableUIPROFILEMAINTAINANCE = dtproviderBtn.getDataTable(sqlStrUIPROFILEMAINTAINANCE, "UIPROFILEMAINTAINANCE");
                    for (int p = 0; p < dtableUIPROFILEMAINTAINANCE.Rows.Count; p++)
                    {
                        DataRow drowUIPROFILEMAINTAINANCE = dtableUIPROFILEMAINTAINANCE.Rows[p];

                        if (drowUIPROFILEMAINTAINANCE.RowState != DataRowState.Deleted)
                        {
                            string buttnAll = drowUIPROFILEMAINTAINANCE["Button"].ToString().Trim();
                            if (buttnAll == "btnNew")
                            {
                                btnNew.Visible = false;
                            }
                            if (buttnAll == "btnOpen")
                            {
                                btnOpen.Visible = false;
                            }
                            if (buttnAll == "btnClose")
                            {
                                btnClose.Visible = false;
                            }
                            if (buttnAll == "btnPost")
                            {
                                btnPost.Visible = false;
                            }
                            if (buttnAll == "btnPostall")
                            {
                                btnPostAll.Visible = false;
                            }
                            if (buttnAll == "btnDelete")
                            {
                                btnDelete.Visible = false;
                            }
                        }
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvBatchList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            col = e.ColumnIndex;
            row = e.RowIndex;
        }

        private void btnNew_Click(object sender, EventArgs e)                           // New Button
        {
            try
            {
                DialogResult dialogresult = MessageBox.Show("Do you want to open a new batch for the period "+DateTime.Now.ToString("MMMM-yyyy")+" ?", "New Batch", MessageBoxButtons.YesNo);
                if (dialogresult == DialogResult.Yes)
                {
                    frmTargetManagement userForm = new frmTargetManagement(lblUserName.Text, "0", 0);
                    userForm.ShowDialog();

                    LoadBatch();

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

                    
        }

        private void btnOpen_Click(object sender, EventArgs e)                      // Open Button
        {
            strSQL = "select transno from item_production_header where BATCHNO = '" + dgvBatchList.Rows[row].Cells[0].Value + "' and AUDTUSER = '" + lblUserName.Text + "'";

            DataTable trans = new DataTable();
            trans = lstData.getDataTable(strSQL, "item_production_header");


            if (trans.Rows.Count<1)
            {
                //  int EntryNo = Convert.ToInt32(dgvBatchList.Rows[row].Cells[0].Value);
                int EntryNo = Convert.ToInt32(dgvBatchList.Rows[row].Cells[0].Value);
                frmTargetManagement userForm;

                if (lblUserName.Text=="admin")
                {
                     userForm = new frmTargetManagement(lblUserName.Text, dgvBatchList.Rows[row].Cells[10].Value.ToString() , EntryNo);
                }
                else
                {
                    userForm = new frmTargetManagement(lblUserName.Text, "0", EntryNo);
                }                
                userForm.batchTotal = dgvBatchList.Rows[row].Cells[5].Value.ToString();
                userForm.batchDesc = dgvBatchList.Rows[row].Cells[2].Value.ToString();
                userForm.batchNo = dgvBatchList.Rows[row].Cells[0].Value.ToString();
                userForm.batchDate = dgvBatchList.Rows[row].Cells[1].Value.ToString();
                userForm.ShowDialog();

                LoadBatch();
            }
            else
            {
                transNo = trans.Rows[0][0].ToString();
                int EntryNo = Convert.ToInt32(dgvBatchList.Rows[row].Cells[0].Value);
                frmTargetManagement userForm = new frmTargetManagement(lblUserName.Text, transNo, EntryNo);
                userForm.batchTotal = dgvBatchList.Rows[row].Cells[5].Value.ToString();
                userForm.batchNo = dgvBatchList.Rows[row].Cells[0].Value.ToString();

                userForm.ShowDialog();
                LoadBatch();
            }

            
        }

        private void btnDelete_Click(object sender, EventArgs e)             // Delete Button
        {
            try
            {
                DialogResult dialogresult = MessageBox.Show("Do you want to delete ?", "Delete", MessageBoxButtons.YesNo);

                if (dialogresult == DialogResult.Yes)
                {
                    strSQL = "update Item_Production_Header set STATUS = 2 where batchno = '" + dgvBatchList.Rows[row].Cells[0].Value.ToString() + "'";
                    lstData.ExecuteCommand(strSQL);

                    strSQL = "update Item_Production_Planning set STATUS = 2 where batchno = '" + dgvBatchList.Rows[row].Cells[0].Value.ToString() + "'";
                    lstData.ExecuteCommand(strSQL);

                    strSQL = "update RSP_Monthly set STATUS = 2 where batchno = '" + dgvBatchList.Rows[row].Cells[0].Value.ToString() + "'";
                    lstData.ExecuteCommand(strSQL);

                    MessageBox.Show("Deleted Succesfully");
                    LoadBatch();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPost_Click(object sender, EventArgs e)        // Post Button
        {
            try
            {
                DialogResult dialogresult = MessageBox.Show("Do you want to post ?", "Post", MessageBoxButtons.YesNo);

                if (dialogresult == DialogResult.Yes)
                {
                    strSQL = "update Item_Production_Header set STATUS = 1 where batchno = '"+dgvBatchList.Rows[row].Cells[0].Value.ToString()+"'";
                    lstData.ExecuteCommand(strSQL);

                    strSQL = "update Item_Production_Planning set STATUS = 1 where batchno = '" + dgvBatchList.Rows[row].Cells[0].Value.ToString() + "'";
                    lstData.ExecuteCommand(strSQL);

                    strSQL = "update RSP_Monthly set STATUS = 1 where batchno = '" + dgvBatchList.Rows[row].Cells[0].Value.ToString() + "'";
                    lstData.ExecuteCommand(strSQL);

                    MessageBox.Show("Posted Succesfully");
                    LoadBatch();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnPostAll_Click(object sender, EventArgs e)           // Post All Button
        {
            try
            {
                DialogResult dialogresult = MessageBox.Show("Do you want to post all batches ?", "Post", MessageBoxButtons.YesNo);

                if (dialogresult == DialogResult.Yes)
                {
                    for (int i = 0; i < dgvBatchList.Rows.Count; i++)
                    {
                        strSQL = "update Item_Production_Header set STATUS = 1 where batchno = '" + dgvBatchList.Rows[i].Cells[0].Value.ToString() + "'";
                        lstData.ExecuteCommand(strSQL);
                    }

                    for (int i = 0; i < dgvBatchList.Rows.Count; i++)
                    {
                        strSQL = "update Item_Production_Planning set STATUS = 1 where batchno = '" + dgvBatchList.Rows[i].Cells[0].Value.ToString() + "'";
                        lstData.ExecuteCommand(strSQL);
                    }
                    MessageBox.Show("All Batches Posted Succesfully");
                    LoadBatch();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void chkPostDel_CheckedChanged(object sender, EventArgs e)
        {
            if(chkPostDel.Checked==true)
            {
                strSQL = "declare @Today as Date = '" + dtpDate.Value.ToString("yyyy-MM-dd") + "' declare @StartOfMonth as Date = DateAdd(day, 1 - Day(@Today), @Today)" +
                " declare @EndOfMonth as Date = DateAdd(day, -1, DateAdd(month, 1, @StartOfMonth)) " +
                "select BATCHDESC, BatchNo, BATCHDATE, RDPPERIOD,[Status], " +
                "Sum(cast(BATCHTOTAL as int)) as BatchTotal,count(TransNo) as NoofEntry,min(TRANSNO) as TransNo, max(RDPDATE) as LastEdited from  ITEM_Production_Header " +
                "where RDPDate <= @EndOfMonth and RDPDate >= @StartOfMonth and STATUS in (0,1,2) " +
                "group by BATCHDESC,BatchNo,BATCHDATE,RDPPERIOD,[Status]";
                dt = lstData.getDataTable(strSQL, "ITEM_Production_Header");
                dgvBatchList.AutoGenerateColumns = false;
                dgvBatchList.DataSource = dt;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dateValue = new DateTime(Convert.ToInt16(dt.Rows[i][2].ToString().Substring(0, 4)), Convert.ToInt16(dt.Rows[i][2].ToString().Substring(4, 2)), Convert.ToInt16(dt.Rows[i][2].ToString().Substring(6, 2))).ToString("dd-MMMM-yyyy");
                    dgvBatchList.Rows[i].Cells[1].Value = dateValue;
                }


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][4].ToString() == "0")
                    {
                        dgvBatchList.Rows[i].Cells[9].Value = "Open";
                    }
                    else if (dt.Rows[i][4].ToString() == "1")
                    {
                        dgvBatchList.Rows[i].Cells[9].Value = "Posted";
                    }
                    else if (dt.Rows[i][4].ToString() == "2")
                    {
                        dgvBatchList.Rows[i].Cells[9].Value = "Deleted";
                    }
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    total = dt.Rows[i][5].ToString();
                    dgvBatchList.Rows[i].Cells[5].Value = string.Format("{0:n0}", Int32.Parse(total));
                }

                btnDelete.Enabled = false;
                btnPost.Enabled = false;
                btnPostAll.Enabled = false;
            }
            else
            {
                LoadBatch();

                btnDelete.Enabled = true;
                btnPost.Enabled = true;
                btnPostAll.Enabled = true;
            }
        }
       

        private void LoadBatch()
        {            
            strSQL = "declare @Today as Date = '" + dtpDate.Value.ToString("yyyy-MM-dd") + "' declare @StartOfMonth as Date = DateAdd(day, 1 - Day(@Today), @Today)" +
                " declare @EndOfMonth as Date = DateAdd(day, -1, DateAdd(month, 1, @StartOfMonth)) " +
                "select BATCHDESC, BatchNo, BATCHDATE, RDPPERIOD,[Status], " +
                "Sum(cast(BATCHTOTAL as int)) as BatchTotal,count(TransNo) as NoofEntry,min(TRANSNO) as TransNo, max(RDPDATE) as LastEdited from ITEM_Production_Header " +
                "where RDPDate <= @EndOfMonth and RDPDate >= @StartOfMonth and STATUS=0 " +
                "group by BATCHDESC,BatchNo,BATCHDATE,RDPPERIOD,[Status]";
            dt = lstData.getDataTable(strSQL, "ITEM_Production_Header");
            dgvBatchList.AutoGenerateColumns = false;
            dgvBatchList.DataSource = dt;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dateValue = new DateTime(Convert.ToInt16(dt.Rows[i][2].ToString().Substring(0, 4)), Convert.ToInt16(dt.Rows[i][2].ToString().Substring(4, 2)), Convert.ToInt16(dt.Rows[i][2].ToString().Substring(6, 2))).ToString("dd-MMMM-yyyy");
                dgvBatchList.Rows[i].Cells[1].Value = dateValue;
            }


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][4].ToString() == "0")
                {
                    dgvBatchList.Rows[i].Cells[9].Value = "Open";
                }
                else if(dt.Rows[i][4].ToString() == "1")
                {
                    dgvBatchList.Rows[i].Cells[9].Value = "Posted";
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                total = dt.Rows[i][5].ToString();
                dgvBatchList.Rows[i].Cells[5].Value = string.Format("{0:n0}", Int32.Parse(total));
            }
            
        }

    }
   
}
