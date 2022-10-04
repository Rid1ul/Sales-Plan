using RDP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDP.Finder
{
    public partial class frmFinderUserGroup : Form
    {
        public string sourceForm;
        public string txtMainUserName { get; set; }
        private string strSQL;
        public frmFinderUserGroup()
        {
            InitializeComponent();
            lstUserGroupfinder.View = View.Details;
            lstUserGroupfinder.LabelEdit = true;
            lstUserGroupfinder.AllowColumnReorder = true;
            lstUserGroupfinder.FullRowSelect = true;
            lstUserGroupfinder.GridLines = true;
            lstUserGroupfinder.Sorting = SortOrder.Ascending;
        }

        private void frmFinderUserGroup_Load(object sender, EventArgs e)
        {
            cmbFindBy.Items.Add("Show All Records");
            cmbFindBy.Items.Add("Group Id");
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
        public void UserType()
        {
            if (sourceForm == "UserGroup")
            {
                lstUserGroupfinder.Columns.Add("Group Id", 100, HorizontalAlignment.Left);
                lstUserGroupfinder.Columns.Add("Group Description", 300, HorizontalAlignment.Left);
            }
            if (sourceForm == "UserAuthentication")
            {
                lstUserGroupfinder.Columns.Add("Group Id", 100, HorizontalAlignment.Left);
                lstUserGroupfinder.Columns.Add("Group Description", 300, HorizontalAlignment.Left);
            }

        }
        private void cmbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            CustomizeInnitialise();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (cmbFindBy.Text == "Show All Records")
            {
                if (cmbStartWith.Text == "Starts with")
                {
                    strSQL = "select * from  UMUGRUP where GROUPID LIKE '" + txtFilter.Text.Trim() + "%'";
                    dataLoadList(strSQL, "UMUGRUP");
                }
                if (cmbStartWith.Text == "Contains")
                {
                    strSQL = "select * from  UMUGRUP where GROUPID LIKE '%" + txtFilter.Text.Trim() + "%'";
                    dataLoadList(strSQL, "UMUGRUP");
                }
            }
            if (cmbFindBy.Text == "User")
            {
                if (cmbStartWith.Text == "Starts with")
                {
                    strSQL = "select * from  UMUGRUP where GROUPID LIKE '" + txtFilter.Text.Trim() + "%'";
                    dataLoadList(strSQL, "UMUGRUP");
                }
                if (cmbStartWith.Text == "Contains")
                {
                    strSQL = "select * from  UMUGRUP where GROUPID LIKE '%" + txtFilter.Text.Trim() + "%' ";
                    dataLoadList(strSQL, "UMUGRUP");
                }
            }
        }
        private void sendText()
        {
            try
            {
                if (sourceForm == "UserGroup")
                {
                    this.Owner.Controls.Find("txtGroupId", true).First().Text = lstUserGroupfinder.SelectedItems[0].SubItems[0].Text;
                    this.Owner.Controls.Find("txtGroupDescription", true).First().Text = lstUserGroupfinder.SelectedItems[0].SubItems[1].Text;
                }
                if (sourceForm == "UserAuthentication")
                {
                    this.Owner.Controls.Find("txtHideGroupDescription", true).First().Text = lstUserGroupfinder.SelectedItems[0].SubItems[1].Text;

                    this.Owner.Controls.Find("txtHideGroupID", true).First().Text = lstUserGroupfinder.SelectedItems[0].SubItems[0].Text;
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
            lstUserGroupfinder.Items.Clear();
            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];
                if (drow.RowState != DataRowState.Deleted)
                {
                    ListViewItem lvi = new ListViewItem(drow["GROUPID"].ToString().Trim());
                    lvi.SubItems.Add(drow["GRPDESCRIPTION"].ToString());
                    lstUserGroupfinder.Items.Add(lvi);
                }
            }
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            sendText();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstUserGroupfinder_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            sendText();
            this.Close();
        }
    }
}
