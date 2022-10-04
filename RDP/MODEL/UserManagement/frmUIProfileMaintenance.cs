using System;
using System.Data;
using System.Windows.Forms;
using RDP.Class;

namespace RDP
{
    public partial class frmUIProfileMaintenance : Form

    {
        string firstUId;
        public string txtMainUserName { get; set; }

        SQL sq= new SQL();


        private string strSQL, sqlStrUIPROFILEMAINTAINANCE;
        public string AUDTDATE;
        DataTable dtableGrade = new DataTable();
        dataProvider lstIncomeTaxRules = new dataProvider();
        dataProvider DBexe = new dataProvider();
        public frmUIProfileMaintenance()
        {
            InitializeComponent();
        }

        private void frmUIProfileMaintenance_Load(object sender, EventArgs e)
        {
            ListViewInitialize();
            loadData();

            buttonPrivilege();
        }
        private void buttonPrivilege()
        {
            strSQL = "select PROFILEID from UIASSIGNPROFILE where USERID='" + txtMainUserName + "'";
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
                    dataProvider dtproviderUIPROFILEMAINTAINANCE = new dataProvider();
                    dtableUIPROFILEMAINTAINANCE = dtproviderBtn.getDataTable(sqlStrUIPROFILEMAINTAINANCE, "UIPROFILEMAINTAINANCE");
                    for (int p = 0; p < dtableUIPROFILEMAINTAINANCE.Rows.Count; p++)
                    {
                        DataRow drowUIPROFILEMAINTAINANCE = dtableUIPROFILEMAINTAINANCE.Rows[p];

                        if (drowUIPROFILEMAINTAINANCE.RowState != DataRowState.Deleted)
                        {
                            string buttnAll = drowUIPROFILEMAINTAINANCE["Button"].ToString().Trim();
                            if (buttnAll == "btnInsert")
                            {
                                btnInsert.Visible = false;
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

        private void loadData()
        {
            try
            { 
            if(txtProfileId.Text=="")
            {
                strSQL = "Select * from UIPROFILEMAINTAINANCE";
                DataTable dtable = new DataTable();
                dataProvider lstData = new dataProvider();
                dtable = lstData.getDataTable(strSQL, "UIPROFILEMAINTAINANCE");
                lstUIPfMaintenance.Items.Clear();

                for (int i = 0; i < dtable.Rows.Count; i++)
                {
                    DataRow drow1 = dtable.Rows[i];
                    if (drow1.RowState != DataRowState.Deleted)
                    {
                        ListViewItem lvi = new ListViewItem(drow1["SLNO"].ToString().Trim());
                        lvi.SubItems.Add(drow1["SCREENNAME"].ToString().Trim());//TAXYEAR
                        lvi.SubItems.Add(drow1["FORMNAME"].ToString().Trim());
                        lvi.SubItems.Add(drow1["BUTTON"].ToString().Trim());
                        lvi.SubItems.Add(drow1["MODULE"].ToString().Trim());//TAXYEAR
                        lvi.SubItems.Add(drow1["ID"].ToString().Trim());
                        lstUIPfMaintenance.Items.Add(lvi);
                    }
                }
            }
            else
            {
                strSQL = "Select * from UIPROFILEMAINTAINANCE where PROFILEID='"+ txtProfileId.Text +"'  ";
                DataTable dtable = new DataTable();
                dataProvider lstData = new dataProvider();
                dtable = lstData.getDataTable(strSQL, "UIPROFILEMAINTAINANCE");
                lstUIPfMaintenance.Items.Clear();

                for (int i = 0; i < dtable.Rows.Count; i++)
                {
                    DataRow drow1 = dtable.Rows[i];
                    if (drow1.RowState != DataRowState.Deleted)
                    {
                        ListViewItem lvi = new ListViewItem(drow1["SLNO"].ToString().Trim());
                        lvi.SubItems.Add(drow1["SCREENNAME"].ToString().Trim());//TAXYEAR
                        lvi.SubItems.Add(drow1["FORMNAME"].ToString().Trim());
                        lvi.SubItems.Add(drow1["BUTTON"].ToString().Trim());
                        lvi.SubItems.Add(drow1["MODULE"].ToString().Trim());//TAXYEAR                        
                        lvi.SubItems.Add(drow1["ID"].ToString().Trim());
                        lstUIPfMaintenance.Items.Add(lvi);
                    }
                }
                
            }
            
            }

            catch(Exception ex)
            {
                MessageBox.Show("Data Load error" + ex);
            }            
        }

        private void ListViewInitialize()
        {
            lstUIPfMaintenance.View = View.Details;
            lstUIPfMaintenance.GridLines = true;
            lstUIPfMaintenance.FullRowSelect = true;

            ColumnHeader Scrn, MODNam, BtnHide, SLNO,formName;
            Scrn = new ColumnHeader();
            MODNam = new ColumnHeader();
            BtnHide = new ColumnHeader();
            SLNO = new ColumnHeader();
            formName = new ColumnHeader();


            Scrn.Text = "Screen Name";
            Scrn.TextAlign = HorizontalAlignment.Left;
            Scrn.Width = 280;

            formName.Text = "Form Name";
            formName.TextAlign = HorizontalAlignment.Left;
            formName.Width = 280;

            BtnHide.Text = "Control to Hide";
            BtnHide.TextAlign = HorizontalAlignment.Left;
            BtnHide.Width = 160;

            MODNam.Text = "Module Name";
            MODNam.TextAlign = HorizontalAlignment.Left;
            MODNam.Width = 200;

            
            SLNO.Text = "SL No.";
            SLNO.TextAlign = HorizontalAlignment.Left;
            SLNO.Width = 0;
            
            lstUIPfMaintenance.Columns.Add(SLNO);
            lstUIPfMaintenance.Columns.Add(Scrn);
            lstUIPfMaintenance.Columns.Add(formName);
            lstUIPfMaintenance.Columns.Add(BtnHide); 
            lstUIPfMaintenance.Columns.Add(MODNam);                                            
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try 
            {
                if(txtProfileId.Text=="")
                {
                    MessageBox.Show("Enter Profile Id");
                    return;
                }
                if (txtProfileDesc.Text == "")
                {
                    MessageBox.Show("Enter Profile Description");
                    return;
                }
                if (txtSCreen.Text == "")
                {
                    MessageBox.Show("Enter Screen Name");
                    return;
                }
                if (txtModuleName.Text == "")
                {
                    MessageBox.Show("Enter Module Name");
                    return;
                }
                if (txtBtn.Text == "")
                {
                    MessageBox.Show("Enter button");
                    return;
                }
                if (txtId.Text == "")
                {
                    MessageBox.Show("Enter  Id");
                    return;
                }


                strSQL = "Select BUTTON from  UIPROFILEMAINTAINANCE where FORMNAME='"+txtFormName.Text.Trim()+"' and BUTTON='"+txtBtn.Text.Trim()+"'";
                dtableGrade = sq.get_rs(strSQL);
                if (dtableGrade.Rows.Count > 0)
                {
                    MessageBox.Show("Data Already Exists !");
                    return;
                }


                string AUDTDATE = DateTime.Now.ToString("yyyyMMdd");
                string AUDTTIME = DateTime.Now.ToString("hhMMss");

                strSQL = "INSERT INTO UIPROFILEMAINTAINANCE (AUDTDATE,AUDTTIME,AUDTUSER,SCREENNAME,FORMNAME,MODULE,BUTTON,PROFILEID,PROFILEDESC,TRANSDATE,ID)"
                               + " VALUES ('" + AUDTDATE + "','" + AUDTTIME + "', '" + txtMainUserName + "', '" + txtSCreen.Text.Trim() + "', '" + txtFormName.Text.Trim() + "',"
                               + "'" + txtModuleName.Text.Trim().Replace("'", "''") + "','" + txtBtn.Text.Trim() + "','" + txtProfileId.Text.Trim().Replace("'", "''") + "','" + txtProfileDesc.Text.Trim().Replace("'", "''") + "','" + dtpTransDate.Value + "','" + txtId.Text.Trim() + "')";

                    DBexe.ExecuteCommand(strSQL);
                   //MessageBox.Show("Saved Successfully....");
              
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error from Save Event" + ex.Message);
            }
            loadData();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstUIPfMaintenance.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Item Selected");
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Do you want to delete?", "Delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ListViewItem item = lstUIPfMaintenance.SelectedItems[0];

                strSQL = "DELETE from UIPROFILEMAINTAINANCE where SLNO = '" + item.Text + "'";
                dataProvider DBexe = new dataProvider();
                DBexe.ExecuteCommand(strSQL);
                MessageBox.Show("Delete Successful....");

                loadData();
            }
        }

        private void btnInsertSCreen_Click(object sender, EventArgs e)
        {
            finderUIProfileMaintenance UIProfileMaintenance = new finderUIProfileMaintenance();
            UIProfileMaintenance.Owner = this;
            strSQL = "SELECT SLNO,MODULE,SCREENNAME,BUTTON,FORMNAME from ADMINBUTTONSETUP";
            UIProfileMaintenance.dataLoadList(strSQL, "ADMINBUTTONSETUP");
            UIProfileMaintenance.sourceForm = "moduleForUIProfileMaintenance";
            UIProfileMaintenance.Show();
        }
        private void btnEntryNoFinder_Click(object sender, EventArgs e)
        {
            finderUIProfileMaintenance UIProfileMaintenance = new finderUIProfileMaintenance();
            UIProfileMaintenance.Owner = this;
            strSQL = "Select SLNO,PROFILEID,PROFILEDESC,BUTTON,MODULE,SCREENNAME,FORMNAME from UIPROFILEMAINTAINANCE";
            UIProfileMaintenance.dataLoadListForProfile(strSQL, "UIPROFILEMAINTAINANCE");
            UIProfileMaintenance.sourceForm = "ProfileForUIProfileMaintenance";
            UIProfileMaintenance.Show();
        }

        private void txtProfileId_TextChanged(object sender, EventArgs e)
        {
            //txtProfileId.Text = firstUId;
            FillData();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            strSQL = "select top 1 PROFILEID from UIPROFILEMAINTAINANCE";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UIPROFILEMAINTAINANCE");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    firstUId = drow["PROFILEID"].ToString().Trim();
                }
            }
            txtProfileId.Text = firstUId;

            FillData();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            strSQL = "select TOP 1(PROFILEID) from (select distinct PROFILEID from UIPROFILEMAINTAINANCE where PROFILEID<'"+ txtProfileId.Text +"'  )as UIPROFILEMAINTAINANCE order by PROFILEID desc ";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UIPROFILEMAINTAINANCE");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    firstUId = drow["PROFILEID"].ToString().Trim();
                }
            }
            txtProfileId.Text = firstUId;
            FillData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            strSQL = "select TOP 1(PROFILEID) from (select distinct PROFILEID from UIPROFILEMAINTAINANCE where PROFILEID>'"+ txtProfileId.Text +"'  )as UIPROFILEMAINTAINANCE order by PROFILEID asc ";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UIPROFILEMAINTAINANCE");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    firstUId = drow["PROFILEID"].ToString().Trim();
                }
            }
            txtProfileId.Text = firstUId;
            FillData();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            strSQL = "select top 1 PROFILEID from UIPROFILEMAINTAINANCE ORDER BY PROFILEID DESC";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UIPROFILEMAINTAINANCE");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    firstUId = drow["PROFILEID"].ToString().Trim();
                }
            }


            txtProfileId.Text = firstUId;
            FillData();
        }

        private void FillData()
        {
            strSQL = "select * from UIPROFILEMAINTAINANCE where PROFILEID='" + txtProfileId.Text.ToUpper() + "' ";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UIPROFILEMAINTAINANCE");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    txtProfileDesc.Text = drow["PROFILEDESC"].ToString().Trim();
                    dtpTransDate.Value = DateTime.Parse(drow["TRANSDATE"].ToString().Trim());//Convert(drow["TRANSDATE"].ToString().Trim());
                   
                }
            }

            fillDetailsData();                                 
        }

        private void fillDetailsData()
        {
            try
            {
                if (txtProfileId.Text == "")
                {
                    strSQL = "Select * from UIPROFILEMAINTAINANCE";
                    DataTable dtable = new DataTable();
                    dataProvider lstData = new dataProvider();
                    dtable = lstData.getDataTable(strSQL, "UIPROFILEMAINTAINANCE");
                    lstUIPfMaintenance.Items.Clear();

                    for (int i = 0; i < dtable.Rows.Count; i++)
                    {
                        DataRow drow1 = dtable.Rows[i];
                        if (drow1.RowState != DataRowState.Deleted)
                        {
                            ListViewItem lvi = new ListViewItem(drow1["SLNO"].ToString().Trim());
                            lvi.SubItems.Add(drow1["SCREENNAME"].ToString().Trim());    //TAXYEAR
                            lvi.SubItems.Add(drow1["FORMNAME"].ToString().Trim());
                            lvi.SubItems.Add(drow1["BUTTON"].ToString().Trim());
                            lvi.SubItems.Add(drow1["MODULE"].ToString().Trim());        //TAXYEAR                            
                            lvi.SubItems.Add(drow1["ID"].ToString().Trim());

                            lstUIPfMaintenance.Items.Add(lvi);
                        }
                    }
                
                }
                else
                {
                    strSQL = "Select * from UIPROFILEMAINTAINANCE where PROFILEID='" + txtProfileId.Text + "' ";
                    DataTable dtable = new DataTable();
                    dataProvider lstData = new dataProvider();
                    dtable = lstData.getDataTable(strSQL, "UIPROFILEMAINTAINANCE");
                    lstUIPfMaintenance.Items.Clear();

                    for (int i = 0; i < dtable.Rows.Count; i++)
                    {
                        DataRow drow1 = dtable.Rows[i];
                        if (drow1.RowState != DataRowState.Deleted)
                        {
                            ListViewItem lvi = new ListViewItem(drow1["SLNO"].ToString().Trim());
                            lvi.SubItems.Add(drow1["SCREENNAME"].ToString().Trim());    //TAXYEAR
                            lvi.SubItems.Add(drow1["FORMNAME"].ToString().Trim());
                            lvi.SubItems.Add(drow1["BUTTON"].ToString().Trim());
                            lvi.SubItems.Add(drow1["MODULE"].ToString().Trim());        //TAXYEAR
                            
                            lvi.SubItems.Add(drow1["ID"].ToString().Trim());

                            lstUIPfMaintenance.Items.Add(lvi);
                        }
                    }
                }
                
            }
                
            catch(Exception ex)
            {
                MessageBox.Show("error" + ex);
            
            }
            
        }

        private void btnNewEntryNo_Click(object sender, EventArgs e)
        {
            ClearAll();
            FillData();
        }

        private void ClearAll()
        {
            dtpTransDate.Value=DateTime.Today;
            txtProfileId.Text = "";
            txtProfileDesc.Text = "";
            txtModuleName.Text = "";
            txtSCreen.Text = "";
            txtBtn.Text = "";
            txtId.Text = "";
        }
    }
}
