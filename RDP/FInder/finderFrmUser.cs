using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace RDP.Finder
{
    public partial class finderFrmUser : Form
    {
        public string sourceForm;
        public string txtMainUserName { get; set; }
        private string strSQL;
        public finderFrmUser()
        {
            InitializeComponent();
            lstCommonUserFinder.View = View.Details;
            lstCommonUserFinder.LabelEdit = true;
            lstCommonUserFinder.AllowColumnReorder = true;
            lstCommonUserFinder.FullRowSelect = true;
            lstCommonUserFinder.GridLines = true;
            lstCommonUserFinder.Sorting = SortOrder.Ascending;  
        }
        public void UserType()
        {
            if (sourceForm == "UserTable")
            {
                lstCommonUserFinder.Columns.Add("User Id", 80, HorizontalAlignment.Left);
                lstCommonUserFinder.Columns.Add("Full Name", 230, HorizontalAlignment.Left);
            }

            if (sourceForm == "UserTableForAssignProfile")
            {
                lstCommonUserFinder.Columns.Add("User Id", 80, HorizontalAlignment.Left);
                lstCommonUserFinder.Columns.Add("Full Name", 230, HorizontalAlignment.Left);
            }
            if (sourceForm == "UIAssign")
            {
                lstCommonUserFinder.Columns.Add("User Id", 80, HorizontalAlignment.Left);
                lstCommonUserFinder.Columns.Add("Full Name", 230, HorizontalAlignment.Left);
            }     
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void finderFrmUser_Load(object sender, EventArgs e)
        {
            cmbFindBy.Items.Add("Show All Records");
            cmbFindBy.Items.Add("User");
            cmbFindBy.SelectedIndex = 1;

            CustomizeInnitialise();

            cmbStartWith.Items.Add("Starts with");
            cmbStartWith.Items.Add("Contains");
            cmbStartWith.SelectedIndex = 0; 
        }
        public void CustomizeInnitialise()
        {
            if (cmbFindBy.SelectedIndex == 0)
            {
                this.cmbStartWith.Hide();
                this.txtFilter.Hide();
                this.lblFilter.Hide();
            }
            else
            {
                this.cmbStartWith.Show();
                this.txtFilter.Show();
                this.lblFilter.Show();
            }
        }

        private void cmbFindBy_TextChanged(object sender, EventArgs e)
        {
            CustomizeInnitialise();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (cmbFindBy.Text == "Show All Records")
            {
                if (cmbStartWith.Text == "Starts with")
                {
                    strSQL = "select * from  UMUSER where USERID LIKE '" + txtFilter.Text.Trim() + "%'";
                    dataLoadList(strSQL, "UMUSER");
                }
                if (cmbStartWith.Text == "Contains")
                {
                    strSQL = "select * from  UMUSER where USERID LIKE '%" + txtFilter.Text.Trim() + "%'";
                    dataLoadList(strSQL, "UMUSER");
                }
            }
            if (cmbFindBy.Text == "User")
            {
                if (cmbStartWith.Text == "Starts with")
                {
                    strSQL = "select * from  UMUSER where USERID LIKE '" + txtFilter.Text.Trim() + "%'";
                    dataLoadList(strSQL, "UMUSER");
                }
                if (cmbStartWith.Text == "Contains")
                {
                    strSQL = "select * from  UMUSER where USERID LIKE '%" + txtFilter.Text.Trim() + "%' ";
                    dataLoadList(strSQL, "UMUSER");
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            sendText();
            this.Close();
        }
        private void sendText()
        {
            try
            {
                if (sourceForm == "UserTable")
                {
                    this.Owner.Controls.Find("txtUserID", true).First().Text = lstCommonUserFinder.SelectedItems[0].Text;
                    this.Owner.Controls.Find("txtEmployeeFullName", true).First().Text = lstCommonUserFinder.SelectedItems[0].SubItems[1].Text;
                }
                if (sourceForm == "UserAuthorization")
                {
                    this.Owner.Controls.Find("txtUserID", true).First().Text = lstCommonUserFinder.SelectedItems[0].Text;
                }
                if (sourceForm == "UserTableForAssignProfile")
                {
                    this.Owner.Controls.Find("txtUserID", true).First().Text = lstCommonUserFinder.SelectedItems[0].Text;
                    this.Owner.Controls.Find("txtEmployeeFullName", true).First().Text = lstCommonUserFinder.SelectedItems[0].SubItems[1].Text;
                }
                if (sourceForm == "UIAssign")
                {
                    this.Owner.Controls.Find("txtUID", true).First().Text = lstCommonUserFinder.SelectedItems[0].Text;
                   // this.Owner.Controls.Find("txtUserName", true).First().Text = lstCommonUserFinder.SelectedItems[0].SubItems[1].Text;
                }  
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Select Any User Id!", ex.Message);
            }
        }
        public void dataLoadList(string strSQL, string myString)
        {
            DataTable dtable = new DataTable();
            dataProvider lstData = new dataProvider();

            dtable = lstData.getDataTable(strSQL, myString);
            lstCommonUserFinder.Items.Clear();
            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];
                if (drow.RowState != DataRowState.Deleted)
                {
                    ListViewItem lvi = new ListViewItem(drow["USERID"].ToString().Trim());
                    lvi.SubItems.Add(drow["USERFULLNAME"].ToString());
                    lstCommonUserFinder.Items.Add(lvi);
                }
            }
        }

        private void lstCommonUserFinder_DoubleClick(object sender, EventArgs e)
        {
            sendText();
            this.Close();
        }

        private void cmbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
