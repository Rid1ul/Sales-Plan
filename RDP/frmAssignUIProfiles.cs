using System;
using System.Data;
using System.Windows.Forms;
namespace RDP
{
    public partial class frmAssignUIProfiles : Form
    {
        string strSQL;
        DataTable dtableGrade = new DataTable();
        dataProvider lstIncomeTaxRules = new dataProvider();
        dataProvider DBexe = new dataProvider();
        public string txtMainUserName { get; set; }
        public frmAssignUIProfiles()
        {
            InitializeComponent();
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUserIdForUIProfile_Click(object sender, EventArgs e)
        {
            //finderFrmUser userFinder = new finderFrmUser();
            //userFinder.Owner = this;
            //strSQL = "Select USERID,USERFULLNAME from USERTBL where SWACTV='0'";
            //userFinder.sourceForm = "UIAssign";
            //userFinder.dataLoadList(strSQL, "USERTBL");
            //userFinder.UserType();
            //userFinder.ShowDialog();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try 
            {
                if(txtUID.Text=="" || txtProfileId.Text=="")
                {
                    MessageBox.Show("Insert User Name and Profile Id");
                    return;
                }
                else
                {
                    insertUIProfile();
                    loadData();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Do the Transaction properly" + ex);
            }
        }

        private void insertUIProfile()
        {
            string AUDTDATE = DateTime.Now.ToString("yyyyMMdd");
            string AUDTTIME = DateTime.Now.ToString("hhMMss");

            strSQL = "INSERT INTO UIASSIGNPROFILE (AUDTDATE,AUDTTIME,AUDTUSER,USERID,PROFILEID,TRANSDATE)"
                           + " VALUES ('" + AUDTDATE + "','" + AUDTTIME + "', '" + txtMainUserName + "', '" + txtUID.Text.Trim() + "',"
                           + "'" + txtProfileId.Text.Trim().Replace("'", "''") + "','" + dtpTransDate.Value + "')";

            DBexe.ExecuteCommand(strSQL);
        }

        private void frmAssignUIProfiles_Load(object sender, EventArgs e)
        {
            ListViewInitialize();
            loadData();
        }

        private void loadData()
        {
            try
            {
                  strSQL = "Select * from UIASSIGNPROFILE  ";
                    DataTable dtable = new DataTable();
                    dataProvider lstData = new dataProvider();
                    dtable = lstData.getDataTable(strSQL, "UIASSIGNPROFILE");
                    lstUIPfMaintenance.Items.Clear();

                    for (int i = 0; i < dtable.Rows.Count; i++)
                    {
                        DataRow drow1 = dtable.Rows[i];
                        if (drow1.RowState != DataRowState.Deleted)
                        {
                            ListViewItem lvi = new ListViewItem(drow1["SLNO"].ToString().Trim());
                            lvi.SubItems.Add(drow1["USERID"].ToString().Trim());//TAXYEAR
                            lvi.SubItems.Add(drow1["PROFILEID"].ToString().Trim());//TAXYEAR                          

                            lstUIPfMaintenance.Items.Add(lvi);
                        }
                    }
               
                
            }

            catch (Exception ex)
            {
                MessageBox.Show("Data Load error" + ex);
            }
        }

        private void ListViewInitialize()
        {
            lstUIPfMaintenance.View = View.Details;
            lstUIPfMaintenance.GridLines = true;
            lstUIPfMaintenance.FullRowSelect = true;

            ColumnHeader PrId, UName, SLNO;
            PrId = new ColumnHeader();
            UName = new ColumnHeader();
            
            SLNO = new ColumnHeader();

            PrId.Text = "Profile Id.";
            PrId.TextAlign = HorizontalAlignment.Left;
            PrId.Width = 200;

            UName.Text = "User Name";
            UName.TextAlign = HorizontalAlignment.Left;
            UName.Width = 200;
           
            SLNO.Text = "SL No.";
            SLNO.TextAlign = HorizontalAlignment.Left;
            SLNO.Width = 50;
            lstUIPfMaintenance.Columns.Add(SLNO);
            lstUIPfMaintenance.Columns.Add(PrId);
            //lstAssignTaxHeadsToEachEmployee.Columns.Add(SLNO);
            lstUIPfMaintenance.Columns.Add(UName);
           
        }
        private void btnProfIdOfUIProfile_Click(object sender, EventArgs e)
        {
            //finderUIProfile UIProfile = new finderUIProfile();
            //UIProfile.Owner = this;
            //strSQL = "select distinct PROFILEID from UIPROFILEMAINTAINANCE";
            //UIProfile.sourceForm = "srcfileUIPROFILEMAINTAINANCE";
            //UIProfile.dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
            //UIProfile.UserType();
            //UIProfile.ShowDialog();
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

                strSQL = "DELETE from UIASSIGNPROFILE where SLNO = '" + item.Text + "'";

                dataProvider DBexe = new dataProvider();

                DBexe.ExecuteCommand(strSQL);

                //MessageBox.Show("Delete Successful....");

                loadData();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        
    }
}
