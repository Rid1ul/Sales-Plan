using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RDP;
using RDP.Finder;

namespace RDP.MODEL.UserManagement
{
    public partial class frmUserGroup : Form
    {
        public string txtMainUserName { get; set; }

        string strSQL, sqlStrUIPROFILEMAINTAINANCE;
        string activeStatus;
        string firstId;

        string AUDTDATE = DateTime.Now.ToString("yyyyMMdd");
        string AUDTTIME = DateTime.Now.ToString("HHmmss");

        dataProvider DBexe = new dataProvider();

        DataTable dtable = new DataTable();
        public frmUserGroup()
        {
            InitializeComponent();
        }

        private void frmUserGroup_Load(object sender, EventArgs e)
        {
            buttonPrivilege();
            cmbModuleName.SelectedIndex = 0;
            initializeScreenleft();
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
                            if (buttnAll == "btnSave")
                            {
                                btnSave.Visible = false;
                            }
                            if (buttnAll == "btnDelete")
                            {
                                btnDelete.Visible = false;
                            }
                            if (buttnAll == "btnSelectAll")
                            {
                                btnSelectAll.Visible = false;
                            }
                            if (buttnAll == "btnRemoveAll")
                            {
                                btnRemoveAll.Visible = false;
                            }
                        }
                    }
                }
            }
        }

        private void initializeScreenleft()
        {
            lstviewUserGroup.View = View.Details;
            lstviewUserGroup.GridLines = true;
            lstviewUserGroup.FullRowSelect = true;

            ColumnHeader screenName;
            screenName = new ColumnHeader();

            screenName.Text = "Screen Name";
            screenName.TextAlign = HorizontalAlignment.Left;
            screenName.Width = 320;

            lstviewUserGroup.Columns.Add(screenName);
            lstviewUserGroup.CheckBoxes = true;
        }

        private void cmbModuleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            strSQL = "select distinct GROUPID as GROUPID from UMUGRUP where GROUPID='"+txtGroupId.Text+"'";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UMUGRUP");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    firstId = drow["GROUPID"].ToString().Trim();
                }
            }

            if (txtGroupId.Text==firstId)
            {
                lstviewUserGroup.Items.Clear();
                GridLOad();
                uncheckall();
                FillData();
            }
            else
            {
                lstviewUserGroup.Items.Clear();
                GridLOad();
                uncheckall();
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstviewUserGroup.Items.Count; i++)
            {
                lstviewUserGroup.Items[i].Checked = true;
            }
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstviewUserGroup.Items.Count; i++)
            {
                lstviewUserGroup.Items[i].Checked = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbModuleName.Text == "Select")
            {
                MessageBox.Show("Select  Module");
                this.ActiveControl = cmbModuleName;
                return;
            }
            if (txtGroupId.Text == "")
            {
                MessageBox.Show("Enter group Id");
                this.ActiveControl = txtGroupId;
                return;
            }
            if (txtGroupDescription.Text == "")
            {
                MessageBox.Show("Enter group description");
                this.ActiveControl = txtGroupDescription;
                return;
            }

            int count = 0; //check selection
            for (int i = 0; i < lstviewUserGroup.Items.Count; i++)
            {
                if (lstviewUserGroup.Items[i].Checked == true)
                {
                    count++;
                }
                
            }

            if (count == 0)
            {
                MessageBox.Show("Select any to continue");
                return;
            }

            strSQL = "delete from UMUGRUP where GROUPID='" + txtGroupId.Text + "' and MODULE='" + cmbModuleName.Text + "'";
            DBexe.ExecuteCommand(strSQL);

            HashSet<string> screenname = new HashSet<string>();

            for (int k = 0; k < lstviewUserGroup.Items.Count; k++)
            {
                if (lstviewUserGroup.Items[k].Checked == true)
                {
                    screenname.Add(lstviewUserGroup.Items[k].SubItems[0].Text);

                    //strSQL = "Insert into UMUGRUP (GROUPID,MODULE,GRPDESCRIPTION,SCREENNAME" +
                    // ",SWACTV,AUDTDATE,AUDTTIME,AUDTUSER,DATELASTMN,LSTUSER) values(" +
                    // "'" + txtGroupId.Text + "','" + cmbModuleName.Text + "','" + txtGroupDescription.Text + "','" + lstviewUserGroup.Items[k].SubItems[0].Text + "'," +
                    // "'0','" + AUDTDATE + "','" + AUDTTIME + "','" + txtMainUserName + "','" + AUDTDATE + "','" + txtMainUserName + "')";
                    //DBexe.ExecuteCommand(strSQL);


                    string parent = "";
                    string code = "";
                    string acname1 = "";
                    string acname2 = "";
                    string acname3 = "";

                    strSQL = "select parent from ADMINTREEMAKER where ac_name='" + lstviewUserGroup.Items[k].SubItems[0].Text + "'";
                    

                    DataTable dtr = new DataTable();
                    dataProvider dpr = new dataProvider();

                    dtr = dpr.getDataTable(strSQL, "ADMINTREEMAKER");

                    for (int n = 0; n < dtr.Rows.Count; n++)
                    {
                        DataRow dro = dtr.Rows[n];

                        if (dro.RowState != DataRowState.Deleted)
                        {
                            parent = dro["parent"].ToString().Trim();
                           
                        }
                        else
                        {
                            return;
                        }
                    }


                    if (parent.Length!=3)
                    {
                        strSQL = "select code,ac_name,parent from ADMINTREEMAKER where code='" + parent + "'";
                        DataTable dtable2 = new DataTable();
                        dataProvider dtprovider2 = new dataProvider();

                        dtable2 = dtprovider2.getDataTable(strSQL, "UMUSER");

                        for (int m = 0; m < dtable2.Rows.Count; m++)
                        {
                            DataRow drow2 = dtable2.Rows[m];

                            if (drow2.RowState != DataRowState.Deleted)
                            {
                                code = drow2["code"].ToString().Trim();
                                parent = drow2["parent"].ToString().Trim();
                                acname1 = drow2["ac_name"].ToString().Trim();
                                screenname.Add(acname1);
                            }
                        }

                        strSQL = "select code,ac_name,parent from ADMINTREEMAKER where code='" + parent + "'";
                        DataTable dtable3 = new DataTable();
                        dataProvider dtprovider3 = new dataProvider();

                        dtable3 = dtprovider3.getDataTable(strSQL, "UMUSER");

                        for (int n = 0; n < dtable3.Rows.Count; n++)
                        {
                            DataRow drow3 = dtable3.Rows[n];

                            if (drow3.RowState != DataRowState.Deleted)
                            {
                                code = drow3["code"].ToString().Trim();
                                parent = drow3["parent"].ToString().Trim();
                                acname2 = drow3["ac_name"].ToString().Trim();
                                screenname.Add(acname2);
                            }
                        }
                        strSQL = "select code,ac_name,parent from ADMINTREEMAKER where code='" + parent + "'";
                        DataTable dtable4 = new DataTable();
                        dataProvider dtprovider4 = new dataProvider();

                        dtable4 = dtprovider4.getDataTable(strSQL, "UMUSER");

                        for (int o = 0; o < dtable4.Rows.Count; o++)
                        {
                            DataRow drow4 = dtable4.Rows[o];

                            if (drow4.RowState != DataRowState.Deleted)
                            {
                                code = drow4["code"].ToString().Trim();
                                parent = drow4["parent"].ToString().Trim();
                                acname3 = drow4["ac_name"].ToString().Trim();
                                screenname.Add(acname3);

                            }
                        }
                    }
                    else
                    {
                        strSQL = "select code,ac_name,parent from ADMINTREEMAKER where code='" + parent + "'";
                        DataTable dtable2 = new DataTable();
                        dataProvider dtprovider2 = new dataProvider();

                        dtable2 = dtprovider2.getDataTable(strSQL, "UMUSER");

                        for (int m = 0; m < dtable2.Rows.Count; m++)
                        {
                            DataRow drow2 = dtable2.Rows[m];

                            if (drow2.RowState != DataRowState.Deleted)
                            {
                                code = drow2["code"].ToString().Trim();
                                parent = drow2["parent"].ToString().Trim();
                                acname1 = drow2["ac_name"].ToString().Trim();
                                screenname.Add(acname1);
                            }
                        }

                        strSQL = "select code,ac_name,parent from ADMINTREEMAKER where code='" + parent + "'";
                        DataTable dtable3 = new DataTable();
                        dataProvider dtprovider3 = new dataProvider();

                        dtable3 = dtprovider3.getDataTable(strSQL, "UMUSER");

                        for (int n = 0; n < dtable3.Rows.Count; n++)
                        {
                            DataRow drow3 = dtable3.Rows[n];

                            if (drow3.RowState != DataRowState.Deleted)
                            {
                                code = drow3["code"].ToString().Trim();
                                parent = drow3["parent"].ToString().Trim();
                                acname2 = drow3["ac_name"].ToString().Trim();
                                screenname.Add(acname2);
                            }
                        }
                        
                    }

                    

                }
            }


            foreach (var item in screenname)
            {
                strSQL = "Insert into UMUGRUP (GROUPID,MODULE,GRPDESCRIPTION,SCREENNAME" +
                 ",SWACTV,AUDTDATE,AUDTTIME,AUDTUSER,DATELASTMN,LSTUSER) values(" +
                 "'" + txtGroupId.Text + "','" + cmbModuleName.Text + "','" + txtGroupDescription.Text + "','" + item + "'," +
                 "'0','" + AUDTDATE + "','" + AUDTTIME + "','" + txtMainUserName + "','" + AUDTDATE + "','" + txtMainUserName + "')";
                DBexe.ExecuteCommand(strSQL);
            }
            //Log details
            string logUser = "Updated User Group- Group ID- " + txtGroupId.Text.Trim() + " -Description- " + txtGroupDescription.Text.Trim();
            strSQL = "INSERT INTO LogDetails (CreateBy,Description,Action,Form) VALUES ('" + txtMainUserName + "', '" + logUser + "' , 'Add','User Group')";
            DBexe.ExecuteCommand(strSQL);
            //end Log Details

            MessageBox.Show("Saved Successfully");

            //string Module = "";
            //strSQL = "Select MODULE from UMUGRUP where GROUPID='" + txtGroupId.Text.Trim() + "'";
            //DataTable dtable = new DataTable();
            //dataProvider dtprovider = new dataProvider();

            //dtable = dtprovider.getDataTable(strSQL, "UMUGRUP");

            //for (int i = 0; i < dtable.Rows.Count; i++)
            //{
            //    DataRow drow = dtable.Rows[i];

            //    if (drow.RowState != DataRowState.Deleted)
            //    {
            //        Module = drow["MODULE"].ToString().Trim();
            //    }
            //}
            //if (cmbModuleName.Text != Module)
            //{
            //    for (int i = 0; i < lstviewUserGroup.Items.Count; i++)
            //    {
            //        if (lstviewUserGroup.Items[i].Checked == true)
            //        {
            //            strSQL = "Insert into UMUGRUP (GROUPID,MODULE,GRPDESCRIPTION,SCREENNAME" +
            //             ",SWACTV,AUDTDATE,AUDTTIME,AUDTUSER,DATELASTMN,LSTUSER) values(" +
            //             "'" + txtGroupId.Text + "','" + cmbModuleName.Text + "','" + txtGroupDescription.Text + "','" + lstviewUserGroup.Items[i].SubItems[0].Text + "'," +
            //             "'0','" + AUDTDATE + "','" + AUDTTIME + "','" + txtMainUserName + "','" + AUDTDATE + "','" + txtMainUserName + "')";
            //            DBexe.ExecuteCommand(strSQL);
            //        }
            //    }
                
            //    //Log details
            //    string logUser = "Created User Group- Group ID- " + txtGroupId.Text.Trim() + " -Description- " + txtGroupDescription.Text.Trim();
            //    strSQL = "INSERT INTO LogDetails (CreateBy,Description,Action,Form) VALUES ('" + txtMainUserName + "', '" + logUser + "' , 'Add','User Group')";
            //    DBexe.ExecuteCommand(strSQL);
            //    //end Log Details

            //    MessageBox.Show("Saved Successfully");
            //}
            //else if (cmbModuleName.Text == Module)
            //{
            //    for (int i = 0; i < lstviewUserGroup.Items.Count; i++)
            //    {
            //        if (lstviewUserGroup.Items[i].Checked == true)
            //        {
            //            //strSQL = "Update UMUGRUP set GROUPID='"+txtGroupId.Text+"',MODULE='"+cmbModuleName.Text+"',GRPDESCRIPTION='"+txtGroupDescription.Text+"',SCREENNAME='"+lstviewUserGroup.Items[i].SubItems[0].Text+"'" +
            //            // ",DATELASTMN='"+AUDTDATE+"',LSTUSER='"+txtMainUserName+"' where SLNO='"+txtSLNO.Text+"'";
            //            //DBexe.ExecuteCommand(strSQL);

            //            strSQL = "delete from UMGROUP where GROUPID='"+ txtGroupId.Text + "' and MODULE='" + cmbModuleName.Text + "'";
            //            DBexe.ExecuteCommand(strSQL);

            //            strSQL = "Insert into UMUGRUP (GROUPID,MODULE,GRPDESCRIPTION,SCREENNAME" +
            //             ",SWACTV,AUDTDATE,AUDTTIME,AUDTUSER,DATELASTMN,LSTUSER) values(" +
            //             "'" + txtGroupId.Text + "','" + cmbModuleName.Text + "','" + txtGroupDescription.Text + "','" + lstviewUserGroup.Items[i].SubItems[0].Text + "'," +
            //             "'0','" + AUDTDATE + "','" + AUDTTIME + "','" + txtMainUserName + "','" + AUDTDATE + "','" + txtMainUserName + "')";
            //            DBexe.ExecuteCommand(strSQL);
            //        }
            //    }

            //    //Log details
            //    string logUser = "Updated User Group- Group ID- " + txtGroupId.Text.Trim() + " -Description- " + txtGroupDescription.Text.Trim();
            //    strSQL = "INSERT INTO LogDetails (CreateBy,Description,Action,Form) VALUES ('" + txtMainUserName + "', '" + logUser + "' , 'Add','User Group')";
            //    DBexe.ExecuteCommand(strSQL);
            //    //end Log Details

            //    MessageBox.Show("Update Successfully");
            //}
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            strSQL = "select top 1 GROUPID from UMUGRUP";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UMUGRUP");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    firstId = drow["GROUPID"].ToString().Trim();
                }
            }
            txtGroupId.Text = firstId;

            FillData();
        }

        private void FillData()
        {
            uncheckall();

            strSQL = "select * from UMUGRUP where GROUPID='" + txtGroupId.Text.ToUpper() + "' ";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UMUGRUP");
            
            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];
                if (drow.RowState != DataRowState.Deleted)
                {
                    txtSLNO.Text = drow["SLNO"].ToString();
                    txtGroupId.Text = dtable.Rows[i]["GROUPID"].ToString().Trim();
                    //cmbModuleName.Text = drow["MODULE"].ToString().Trim();
                    txtGroupDescription.Text = drow["GRPDESCRIPTION"].ToString().Trim();

                    for (int j = 0; j < lstviewUserGroup.Items.Count; j++)
                    {
                        if (drow["SCREENNAME"].ToString() == lstviewUserGroup.Items[j].SubItems[0].Text)
                        {
                            lstviewUserGroup.Items[j].Checked = true;
                        }
                    } 
                }
            }
        }
        public void uncheckall()
        {
            for (int j = 0; j < lstviewUserGroup.Items.Count; j++)
            {
                lstviewUserGroup.Items[j].Checked = false;
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            strSQL = "Select top 1(GROUPID)as GROUPID from (select GROUPID from  UMUGRUP where GROUPID<'" + txtGroupId.Text + "') as UMUGRUP group by GROUPID order by GROUPID desc";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UMUGRUP");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    firstId = drow["GROUPID"].ToString().Trim();
                }
            }
            txtGroupId.Text = firstId;
            FillData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            strSQL = "select top 1(GROUPID) from (select distinct GROUPID from UMUGRUP where GROUPID >'" + txtGroupId.Text + "')as UMUGRUP ";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UMUGRUP");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    firstId = drow["GROUPID"].ToString().Trim();
                }
            }

            txtGroupId.Text = firstId;
            FillData();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            strSQL = "select max(GROUPID) as GROUPID from UMUGRUP";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UMUGRUP");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    firstId = drow["GROUPID"].ToString().Trim();
                }
            }
            txtGroupId.Text = firstId;
            FillData();
        }

        private void btnEntryNoFinder_Click(object sender, EventArgs e)
        {
            frmFinderUserGroup userGroup = new frmFinderUserGroup();
            userGroup.Owner = this;
            strSQL = "Select distinct GROUPID,GRPDESCRIPTION from UMUGRUP";
            userGroup.sourceForm = "UserGroup";
            userGroup.dataLoadList(strSQL, "UMUGRUP");
            userGroup.UserType();
            userGroup.ShowDialog();
        }

        private void btnNewEntryNo_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        private void ClearAll()
        {
            txtGroupId.Text = "";
            txtGroupDescription.Text = "";
            cmbModuleName.SelectedIndex = 0;
            lstviewUserGroup.Items.Clear();
        }

        private void txtSLNO_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void txtGroupId_TextChanged(object sender, EventArgs e)
        {
            FillData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            strSQL = "Delete from UMUGRUP where GROUPID='"+txtGroupId.Text+"'";
            DBexe.ExecuteCommand(strSQL);
            MessageBox.Show("Deleted Successfully");
        }

        public void GridLOad()
        {
            DataTable dtable = new DataTable();
            dataProvider lstData = new dataProvider();

            strSQL = "select distinct SCREENNAME from ADMINBUTTONSETUP where MODULE='" + cmbModuleName.Text + "'";
            dtable = lstData.getDataTable(strSQL, "ADMINBUTTONSETUP");
            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];
                if (drow.RowState != DataRowState.Deleted)
                {
                    ListViewItem lvi = new ListViewItem(drow["SCREENNAME"].ToString().Trim());

                    lstviewUserGroup.Items.Add(lvi);
                }
            }
            //txtGrd.Text = Convert.ToString(lstGrd.Items.Count);
            //chkAll.Checked = false;
        }

    }
}
