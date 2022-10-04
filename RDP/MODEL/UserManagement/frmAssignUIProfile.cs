using System;
using System.Data;
using System.Windows.Forms;
using RDP;
using RDP.Class;
using RDP.Finder;

namespace RDP.MODEL
{
    public partial class frmAssignUIProfile : Form
    {
        public string txtMainUserName { get; set; }     

        string strSQL, sqlStrUIPROFILEMAINTAINANCE;
        string activeStatus;

        string AUDTDATE = DateTime.Now.ToString("yyyyMMdd");
        string AUDTTIME = DateTime.Now.ToString("HHmmss");

        dataProvider DBexe = new dataProvider();

        DataTable dtable = new DataTable();
        dataProvider dtprovider = new dataProvider();
        SQL sq = new SQL();
        public frmAssignUIProfile()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRole_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.btnFirst, "First");
            toolTip1.SetToolTip(this.btnLast, "Last");
            toolTip1.SetToolTip(this.btnNext, "Next");
            toolTip1.SetToolTip(this.btnPrevious, "Previous");
            toolTip1.SetToolTip(this.btnFinder, "Finder");


            toolTip1.SetToolTip(this.btnProfileFirst, "First");
            toolTip1.SetToolTip(this.btnProfileLast, "Last");
            toolTip1.SetToolTip(this.btnProfileNext, "Next");
            toolTip1.SetToolTip(this.btnProfilePrevious, "Previous");
            toolTip1.SetToolTip(this.btnProfileFinder, "Finder");

            buttonPrivilege();

            //btnAdd.Enabled = true;            
            //btnRemove.Enabled = false;

            lstViewRole.View = View.Details;
            lstViewRole.LabelEdit = true;
            lstViewRole.AllowColumnReorder = true;
            lstViewRole.FullRowSelect = true;
            lstViewRole.GridLines = true;
            lstViewRole.Sorting = SortOrder.Ascending;
            lstViewRole.Columns.Add("Profile ID", 250, HorizontalAlignment.Left);            
        }
        public void OldDataFill()
        {
            try
            {
                lstViewRole.Items.Clear();

                DataTable dtable = new DataTable();
                dataProvider lstData = new dataProvider();

                strSQL = "Select * from UIASSIGNPROFILE WHERE USERID = '" + txtUserID.Text + "' ";
                dtable = lstData.getDataTable(strSQL, "UIASSIGNPROFILE");

                for (int i = 0; i < dtable.Rows.Count; i++)
                {
                    DataRow drow = dtable.Rows[i];

                    if (drow.RowState != DataRowState.Deleted)
                    {
                        if (drow["SWACTV"].ToString() == "0")
                        {
                            chkStatus.Checked = false;                            
                        }
                        else
                        {
                            chkStatus.Checked = true;                            
                        }
                        dtpTransDate.Value = new DateTime(Convert.ToInt16(dtable.Rows[0]["TRANSDATE"].ToString().Substring(0, 4)), Convert.ToInt16(dtable.Rows[0]["TRANSDATE"].ToString().Substring(4, 2)), Convert.ToInt16(dtable.Rows[0]["TRANSDATE"].ToString().Substring(6, 2)));

                        ListViewItem lvi = new ListViewItem(drow["PROFILEID"].ToString());                                           
                        lstViewRole.Items.Add(lvi);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void buttonPrivilege()
        {
            strSQL = "Select PROFILEID from UIASSIGNPROFILE where USERID='" + txtMainUserName + "'";
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
                            if (buttnAll == "btnAdd")
                            {
                                btnAdd.Visible = false;
                            }
                            if (buttnAll == "btnSave")
                            {
                                btnSave.Visible = false;
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

        private void btnFinder_Click(object sender, EventArgs e)
        {
            finderFrmUser userFinder = new finderFrmUser();
            userFinder.Owner = this;
            strSQL = "Select USERID,USERFULLNAME from UMUSER where SWACTV='0'";
            userFinder.sourceForm = "UserTableForAssignProfile";
            userFinder.dataLoadList(strSQL, "UMUSER");
            userFinder.UserType();
            userFinder.ShowDialog();
        }
        private void btnProfileFinder_Click(object sender, EventArgs e)
        {
            finderUIProfileMaintenance UIProfileMaintenance = new finderUIProfileMaintenance();
            UIProfileMaintenance.Owner = this;
            strSQL = "Select distinct PROFILEID,BUTTON from UIPROFILEMAINTAINANCE where active=1";
            UIProfileMaintenance.sourceForm = "AssignProfileID";
            UIProfileMaintenance.dataLoadListProfileID(strSQL, "UIPROFILEMAINTAINANCE");
            UIProfileMaintenance.Show();
        
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            strSQL = "select top 1(USERID),USERFULLNAME from (select distinct USERID,USERFULLNAME from UMUSER where USERID >'" + txtUserID.Text + "')as UMUSER ";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UMUSER");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    txtUserID.Text = drow["USERID"].ToString().Trim();
                    txtEmployeeFullName.Text = drow["USERFULLNAME"].ToString().Trim();
                }
            }
               
            EditButton();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            strSQL = "Select top 1(USERID)as USERID,USERFULLNAME from (select USERID,USERFULLNAME from  UMUSER where USERID<'" + txtUserID.Text + "') as UMUSER group by USERID,USERFULLNAME order by USERID desc";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UMUSER");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    txtUserID.Text = drow["USERID"].ToString().Trim();
                    txtEmployeeFullName.Text = drow["USERFULLNAME"].ToString().Trim();
                }
            }
            EditButton();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            strSQL = "select   top 1 USERID as USERID,USERFULLNAME from UMUSER order by USERID desc";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UMUSER");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    txtUserID.Text = drow["USERID"].ToString().Trim();
                    txtEmployeeFullName.Text = drow["USERFULLNAME"].ToString().Trim();
                }
            }
            EditButton();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            strSQL = "select top 1 USERID,USERFULLNAME from UMUSER";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UMUSER");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    txtUserID.Text = drow["USERID"].ToString().Trim();
                    txtEmployeeFullName.Text = drow["USERFULLNAME"].ToString().Trim();
                }
            }
            EditButton();
        }

        private void btnProfileNext_Click(object sender, EventArgs e)
        {
            strSQL = "select TOP 1(PROFILEID) from (select distinct PROFILEID from UIPROFILEMAINTAINANCE where PROFILEID>'" + txtProfileId.Text + "'  )as UIPROFILEMAINTAINANCE order by PROFILEID asc ";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UIPROFILEMAINTAINANCE");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    txtProfileId.Text = drow["PROFILEID"].ToString().Trim();
                }
            }
            //FillData();
        }

        private void btnProfilePrevious_Click(object sender, EventArgs e)
        {
            strSQL = "select TOP 1(PROFILEID) from (select distinct PROFILEID from UIPROFILEMAINTAINANCE where PROFILEID<'" + txtProfileId.Text + "'  )as UIPROFILEMAINTAINANCE order by PROFILEID desc ";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UIPROFILEMAINTAINANCE");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    txtProfileId.Text = drow["PROFILEID"].ToString().Trim();
                }
            }            
            //FillData();
        }

        private void btnProfileLast_Click(object sender, EventArgs e)
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
                    txtProfileId.Text = drow["PROFILEID"].ToString().Trim();
                }
            }            
            //FillData();
        }

        private void btnProfileFirst_Click(object sender, EventArgs e)
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
                    txtProfileId.Text = drow["PROFILEID"].ToString().Trim();
                }
            }            
            //FillData();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (txtUserID.Text == "")
            {
                MessageBox.Show("Please Provide User ID");
                return;
            }
            if (txtProfileId.Text == "")
            {
                MessageBox.Show("Please Provide Profile ID");
                return;
            }
            for (int i = 0; i < lstViewRole.Items.Count; i++)
            {                
                {
                    if ((lstViewRole.Items[i].SubItems[0].Text == txtProfileId.Text) )
                    {
                        MessageBox.Show("Already Exists !");
                        return;
                    }
                }                   
            }            
            ListViewItem lvi = new ListViewItem(txtProfileId.Text);
            lstViewRole.Items.Add(lvi);

            btnSave.Enabled = true;
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem eachItem in lstViewRole.SelectedItems)
            {
                lstViewRole.Items.Remove(eachItem);   // Remove from only listview item, not db
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string transDate = dtpTransDate.Value.ToString("yyyyMMdd");

                if (chkStatus.Checked == true)
                {
                    activeStatus = "1";
                }
                else
                {
                    activeStatus = "0";
                }
                DialogResult dialogResult = MessageBox.Show("Do you want to save?", "Save", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    for (int i = 0; i < lstViewRole.Items.Count; i++)
                    {
                        string profileId = lstViewRole.Items[i].SubItems[0].Text;

                        strSQL = "Insert into UIASSIGNPROFILE(AUDTDATE,AUDTTIME,AUDTUSER,USERID,USERFULLNAME,PROFILEID,TRANSDATE,SWACTV,DATELASTMN,LSTUSER) " +
                                    "Values('" + AUDTDATE + "','" + AUDTTIME + "', '" + txtMainUserName + "','" + txtUserID.Text + "','" + txtEmployeeFullName.Text + "'," +
                                    "'" + profileId + "','" + transDate + "','" + activeStatus + "','" + AUDTDATE + "','" + txtMainUserName + "')";
                        DBexe.ExecuteCommand(strSQL);
                    }

                    MessageBox.Show("Saved Succesfully");
                    TextClear();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string transDate = dtpTransDate.Value.ToString("yyyyMMdd");




                if (chkStatus.Checked == true)
                {
                    activeStatus = "1";
                }
                else
                {
                    activeStatus = "0";
                }
                DialogResult dialogResult = MessageBox.Show("Do you want to update?", "Update", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    //Start Log details
                    string logUser = "Updated Assign UI Profile -User ID- " + txtUserID.Text;

                    strSQL = "INSERT INTO LogDetails (CreateBy,Description,Action,Form) VALUES ('" + txtMainUserName + "', '" + logUser + "' , 'Update','Assign UI Profile')";
                    DBexe.ExecuteCommand(strSQL);

                    strSQL = "INSERT INTO UIASSIGNPROFILE_Log SELECT *,'" + txtMainUserName + "' as CreateBy, GETDATE() as CreateDateTime ,'Update' as Action FROM UIASSIGNPROFILE WHERE USERID='" + txtUserID.Text + "' and USERFULLNAME='" + txtEmployeeFullName.Text + "' ";
                    DBexe.ExecuteCommand(strSQL);

                    //........Log details end



                    strSQL = "Delete from UIASSIGNPROFILE where USERID='" + txtUserID.Text + "' and USERFULLNAME='" + txtEmployeeFullName.Text + "'";
                    DBexe.ExecuteCommand(strSQL);

                    for (int i = 0; i < lstViewRole.Items.Count; i++)
                    {
                        string profileId = lstViewRole.Items[i].SubItems[0].Text;

                        strSQL = "Insert into UIASSIGNPROFILE(AUDTDATE,AUDTTIME,AUDTUSER,USERID,USERFULLNAME,PROFILEID,TRANSDATE,SWACTV,DATELASTMN,LSTUSER) " +
                                    "Values('" + AUDTDATE + "','" + AUDTTIME + "', '" + txtMainUserName + "','" + txtUserID.Text + "','" + txtEmployeeFullName.Text + "'," +
                                    "'" + profileId + "','" + transDate + "','" + activeStatus + "','" + AUDTDATE + "','" + txtMainUserName + "')";
                        DBexe.ExecuteCommand(strSQL);
                    }

                    MessageBox.Show("Updated Succesfully");
                    TextClear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void TextClear()
        {
            txtUserID.Text = "";
            txtEmployeeFullName.Text = "";
            txtProfileId.Text = "";
            lstViewRole.Items.Clear();

            chkStatus.Checked = false;            
            btnAdd.Visible = true;
            btnAdd.Enabled = true;

            btnSave.Enabled = false;
            btnDelete.Enabled = false;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserID.Text == "")
                {
                    MessageBox.Show("Please Provide User ID");
                    return;
                }                
                DialogResult dialogResult = MessageBox.Show("Do you want to delete?", "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    ListViewItem item = lstViewRole.SelectedItems[0];


                    //Start Log details
                    string logUser = "Deleted Assign UI Profile -User ID- " + txtUserID.Text;

                    strSQL = "INSERT INTO LogDetails (CreateBy,Description,Action,Form) VALUES ('" + txtMainUserName + "', '" + logUser + "' , 'Delete','Assign UI Profile')";
                    DBexe.ExecuteCommand(strSQL);

                    strSQL = "INSERT INTO UIASSIGNPROFILE_Log SELECT *,'" + txtMainUserName + "' as CreateBy, GETDATE() as CreateDateTime ,'Delete' as Action FROM UIASSIGNPROFILE where USERID='" + txtUserID.Text + "' and PROFILEID='" + item.Text + "' ";
                    DBexe.ExecuteCommand(strSQL);

                    //........Log details end


                    strSQL = "Delete from UIASSIGNPROFILE where USERID='" + txtUserID.Text + "' and PROFILEID='" + item.Text + "'";
                    DBexe.ExecuteCommand(strSQL);
                }
                MessageBox.Show("Deleted Successfully");
                TextClear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtUserID_TextChanged(object sender, EventArgs e)
        {
            strSQL = "Select * from UIASSIGNPROFILE where USERID='" + txtUserID.Text + "'";
            dtable = sq.get_rs(strSQL);
            if (dtable.Rows.Count != 0)
            {
                OldDataFill();
            }
            else
            {
                txtProfileId.Text = "";
                lstViewRole.Items.Clear();
            }
        }

        public void EditButton()
        {
            strSQL = "Select * from UIASSIGNPROFILE where USERID='" + txtUserID.Text + "'";
            dtable = sq.get_rs(strSQL);
            if (dtable.Rows.Count != 0)
            {
                btnAdd.Enabled = false;
                btnAdd.Visible = false;

                //btnSave.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                btnAdd.Enabled = true;
                btnAdd.Visible = true;

                btnSave.Enabled = false;
                btnDelete.Enabled = false;  
            }
        }


    }
}
