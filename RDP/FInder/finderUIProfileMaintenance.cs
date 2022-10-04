using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace RDP
{
    public partial class finderUIProfileMaintenance : Form
    {
        public string txtMainUserName { get; set; }
        private string strSQL;

        public string sourceForm;
        public string empGradefromEmpSearch;
        public string GradeId;
        public string GradeDesc;
        public finderUIProfileMaintenance()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
                ListViewItem item = lstUiProfile.SelectedItems[0];
                if ((item != null) && (sourceForm == "moduleForUIProfileMaintenance"))
                {
                    this.Owner.Controls.Find("txtId", true).First().Text = lstUiProfile.SelectedItems[0].Text;
                    this.Owner.Controls.Find("txtModuleName", true).First().Text = lstUiProfile.SelectedItems[0].SubItems[1].Text;
                    this.Owner.Controls.Find("txtSCreen", true).First().Text = lstUiProfile.SelectedItems[0].SubItems[2].Text;
                    this.Owner.Controls.Find("txtBtn", true).First().Text = lstUiProfile.SelectedItems[0].SubItems[3].Text;                    
                    this.Owner.Controls.Find("txtFormName", true).First().Text = lstUiProfile.SelectedItems[0].SubItems[4].Text;
                }

                if ((item != null) && (sourceForm == "ProfileForUIProfileMaintenance"))
                {
                    this.Owner.Controls.Find("txtProfileId", true).First().Text = lstUiProfile.SelectedItems[0].SubItems[3].Text;
                    this.Owner.Controls.Find("txtProfileDesc", true).First().Text = lstUiProfile.SelectedItems[0].SubItems[2].Text;
                }
                if ((item != null) && (sourceForm == "AssignProfileID"))
                {
                    this.Owner.Controls.Find("txtProfileId", true).First().Text = lstUiProfile.SelectedItems[0].Text;                    
                }                              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Select any Module!", ex.Message);
            }
        }

        private void finderUIProfileMaintenance_Load(object sender, EventArgs e)
        {
            listviewinitialize();

            if (sourceForm == "moduleForUIProfileMaintenance")
            {
                cmbFindBy.Items.Add("Show All Records");
                cmbFindBy.Items.Add("Module Name");
                cmbFindBy.Items.Add("Screen Name");
                cmbFindBy.Items.Add("Button");
                //cmbFindBy.Items.Add("Id No.");
                //cmbFindBy.Items.Add("FormName");
                cmbFindBy.SelectedIndex = 2;

                CustomizeInnitialise();

                cmbStartWith.Items.Add("Starts with");
                cmbStartWith.Items.Add("Contains");
                cmbStartWith.SelectedIndex = 1;                 
            }
            if (sourceForm == "ProfileForUIProfileMaintenance")
            {
                cmbFindBy.Items.Add("Show All Records");
                cmbFindBy.Items.Add("Module Name");
                cmbFindBy.Items.Add("Screen Name");
                cmbFindBy.Items.Add("Button");
                //cmbFindBy.Items.Add("Id No.");                
                cmbFindBy.SelectedIndex = 2;

                CustomizeInnitialise();

                cmbStartWith.Items.Add("Starts with");
                cmbStartWith.Items.Add("Contains");
                cmbStartWith.SelectedIndex = 1;
            }
            if (sourceForm == "AssignProfileID")
            {
                cmbFindBy.Items.Add("Show All Records");
                cmbFindBy.Items.Add("Profile ID");
                cmbFindBy.Items.Add("Button");                               
                cmbFindBy.SelectedIndex = 1;

                CustomizeInnitialise();

                cmbStartWith.Items.Add("Starts with");
                cmbStartWith.Items.Add("Contains");
                cmbStartWith.SelectedIndex = 1; 
            }

        }

        private void listviewinitialize()
        {
            if (sourceForm == "moduleForUIProfileMaintenance" || sourceForm == "ProfileForUIProfileMaintenance")
            {
                lstUiProfile.View = View.Details;
                lstUiProfile.LabelEdit = true;
                lstUiProfile.AllowColumnReorder = true;
                lstUiProfile.FullRowSelect = true;
                lstUiProfile.GridLines = true;
                lstUiProfile.Sorting = SortOrder.Ascending;
                lstUiProfile.Columns.Add("Sl No.", 0, HorizontalAlignment.Left);
                lstUiProfile.Columns.Add("Module Name", 100, HorizontalAlignment.Left);
                lstUiProfile.Columns.Add("Screen Name", 180, HorizontalAlignment.Left);
                lstUiProfile.Columns.Add("Button Name", 120, HorizontalAlignment.Left);
                lstUiProfile.Columns.Add("Form Name", 200, HorizontalAlignment.Left);
            }
            if (sourceForm == "AssignProfileID")
            {
                lstUiProfile.View = View.Details;
                lstUiProfile.LabelEdit = true;
                lstUiProfile.AllowColumnReorder = true;
                lstUiProfile.FullRowSelect = true;
                lstUiProfile.GridLines = true;
                lstUiProfile.Sorting = SortOrder.Ascending;
                lstUiProfile.Columns.Add("Profile ID", 140, HorizontalAlignment.Left);
                lstUiProfile.Columns.Add("Button Name", 140, HorizontalAlignment.Left);               
            }

        }

        private void CustomizeInnitialise()
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

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (sourceForm == "moduleForUIProfileMaintenance")
            {
                if (cmbStartWith.Text == "Starts with")
                {
                    if (cmbFindBy.Text == "Show All Records")
                    {
                        if (txtFilter.Text != "")
                        {
                            strSQL = "(select * from  ADMINBUTTONSETUP where MODULE LIKE '" + txtFilter.Text.Trim() + "%')";
                            dataLoadList(strSQL, "ADMINBUTTONSETUP");
                        }
                        else
                        {
                            strSQL = "select * from  ADMINBUTTONSETUP";
                            dataLoadList(strSQL, "ADMINBUTTONSETUP");
                        }

                    }
                    if (cmbFindBy.Text == "Module")
                    {
                        if (txtFilter.Text != "")
                        {
                            strSQL = "(select * from  ADMINBUTTONSETUP where MODULE LIKE '" + txtFilter.Text.Trim() + "%')";
                            dataLoadList(strSQL, "ADMINBUTTONSETUP");
                        }
                        else
                        {
                            strSQL = "select * from  ADMINBUTTONSETUP";
                            dataLoadList(strSQL, "ADMINBUTTONSETUP");
                        }
                    }
                    if (cmbFindBy.Text == "Screen Name")
                    {
                        if (txtFilter.Text != "")
                        {
                            strSQL = "(select * from  ADMINBUTTONSETUP where SCREENNAME LIKE '" + txtFilter.Text.Trim() + "%')";
                            dataLoadList(strSQL, "ADMINBUTTONSETUP");
                        }
                        else
                        {
                            strSQL = "select * from  ADMINBUTTONSETUP";
                            dataLoadList(strSQL, "ADMINBUTTONSETUP");
                        }
                    }
                    if (cmbFindBy.Text == "Button")
                    {
                        if (txtFilter.Text != "")
                        {
                            strSQL = "(select * from  ADMINBUTTONSETUP where BUTTON LIKE '" + txtFilter.Text.Trim() + "%')";
                            dataLoadList(strSQL, "ADMINBUTTONSETUP");
                        }
                        else
                        {
                            strSQL = "select * from  ADMINBUTTONSETUP";
                            dataLoadList(strSQL, "ADMINBUTTONSETUP");
                        }
                    }

                }
                if (cmbStartWith.Text == "Contains")
                {
                    if (cmbFindBy.Text == "Show All Records")
                    {
                        if (txtFilter.Text != "")
                        {
                            strSQL = "(select * from  ADMINBUTTONSETUP where MODULE LIKE '%" + txtFilter.Text.Trim() + "%')";
                            dataLoadList(strSQL, "ADMINBUTTONSETUP");
                        }
                        else
                        {
                            strSQL = "select * from  ADMINBUTTONSETUP";
                            dataLoadList(strSQL, "ADMINBUTTONSETUP");
                        }

                    }
                    if (cmbFindBy.Text == "Module")
                    {
                        if (txtFilter.Text != "")
                        {
                            strSQL = "(select * from  ADMINBUTTONSETUP where MODULE LIKE '%" + txtFilter.Text.Trim() + "%')";
                            dataLoadList(strSQL, "ADMINBUTTONSETUP");
                        }
                        else
                        {
                            strSQL = "select * from  ADMINBUTTONSETUP";
                            dataLoadList(strSQL, "ADMINBUTTONSETUP");
                        }
                    }
                    if (cmbFindBy.Text == "Screen Name")
                    {
                        if (txtFilter.Text != "")
                        {
                            strSQL = "(select * from  ADMINBUTTONSETUP where SCREENNAME LIKE '%" + txtFilter.Text.Trim() + "%')";
                            dataLoadList(strSQL, "ADMINBUTTONSETUP");
                        }
                        else
                        {
                            strSQL = "select * from  ADMINBUTTONSETUP";
                            dataLoadList(strSQL, "ADMINBUTTONSETUP");
                        }
                    }
                    if (cmbFindBy.Text == "Button")
                    {
                        if (txtFilter.Text != "")
                        {
                            strSQL = "(select * from  ADMINBUTTONSETUP where BUTTON LIKE '%" + txtFilter.Text.Trim() + "%')";
                            dataLoadList(strSQL, "ADMINBUTTONSETUP");
                        }
                        else
                        {
                            strSQL = "select * from  ADMINBUTTONSETUP";
                            dataLoadList(strSQL, "ADMINBUTTONSETUP");
                        }
                    }
                }     
            }



            if (sourceForm == "ProfileForUIProfileMaintenance")
            {
                if (cmbStartWith.Text == "Starts with")
                {
                    if (cmbFindBy.Text == "Show All Records")
                    {
                        if (txtFilter.Text != "")
                        {
                            strSQL = "(select * from  UIPROFILEMAINTAINANCE where MODULE LIKE '" + txtFilter.Text.Trim() + "%')";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }
                        else
                        {
                            strSQL = "select * from  UIPROFILEMAINTAINANCE";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }

                    }
                    if (cmbFindBy.Text == "Module")
                    {
                        if (txtFilter.Text != "")
                        {
                            strSQL = "(select * from  UIPROFILEMAINTAINANCE where MODULE LIKE '" + txtFilter.Text.Trim() + "%')";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }
                        else
                        {
                            strSQL = "select * from  UIPROFILEMAINTAINANCE";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }
                    }
                    if (cmbFindBy.Text == "Screen")
                    {
                        if (txtFilter.Text != "")
                        {
                            strSQL = "(select * from  UIPROFILEMAINTAINANCE where SCREENNAME LIKE '" + txtFilter.Text.Trim() + "%')";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }
                        else
                        {
                            strSQL = "select * from  UIPROFILEMAINTAINANCE";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }
                    }
                    if (cmbFindBy.Text == "Button")
                    {
                        if (txtFilter.Text != "")
                        {
                            strSQL = "(select * from  UIPROFILEMAINTAINANCE where BUTTON LIKE '" + txtFilter.Text.Trim() + "%')";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }
                        else
                        {
                            strSQL = "select * from  UIPROFILEMAINTAINANCE";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }
                    }

                }
                if (cmbStartWith.Text == "Contains")
                {
                    if (cmbFindBy.Text == "Show All Records")
                    {
                        if (txtFilter.Text != "")
                        {
                            strSQL = "(select * from  UIPROFILEMAINTAINANCE where MODULE LIKE '%" + txtFilter.Text.Trim() + "%')";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }
                        else
                        {
                            strSQL = "select * from  UIPROFILEMAINTAINANCE";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }

                    }
                    if (cmbFindBy.Text == "Module")
                    {
                        if (txtFilter.Text != "")
                        {
                            strSQL = "(select * from  UIPROFILEMAINTAINANCE where MODULE LIKE '%" + txtFilter.Text.Trim() + "%')";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }
                        else
                        {
                            strSQL = "select * from  UIPROFILEMAINTAINANCE";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }
                    }
                    if (cmbFindBy.Text == "Screen")
                    {
                        if (txtFilter.Text != "")
                        {
                            strSQL = "(select * from  UIPROFILEMAINTAINANCE where SCREENNAME LIKE '%" + txtFilter.Text.Trim() + "%')";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }
                        else
                        {
                            strSQL = "select * from  UIPROFILEMAINTAINANCE";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }
                    }
                    if (cmbFindBy.Text == "Button")
                    {
                        if (txtFilter.Text != "")
                        {
                            strSQL = "(select * from  UIPROFILEMAINTAINANCE where BUTTON LIKE '%" + txtFilter.Text.Trim() + "%')";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }
                        else
                        {
                            strSQL = "select * from  UIPROFILEMAINTAINANCE";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }
                    }
                }


                if (sourceForm == "AssignProfileID")
                {
                    if (cmbFindBy.Text == "Show All Records")
                    {
                        if (txtFilter.Text != "")
                        {
                            strSQL = "(select * from  UIPROFILEMAINTAINANCE where ProfileID LIKE '%" + txtFilter.Text.Trim() + "%')";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }
                        else
                        {
                            strSQL = "select * from  UIPROFILEMAINTAINANCE";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }

                    }
                    if (cmbFindBy.Text == " Profile ID")
                    {
                        if (txtFilter.Text != "")
                        {
                            strSQL = "(select * from  UIPROFILEMAINTAINANCE where ProfileID LIKE '%" + txtFilter.Text.Trim() + "%')";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }
                        else
                        {
                            strSQL = "select * from  UIPROFILEMAINTAINANCE";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }
                    }
                    if (cmbFindBy.Text == "Button")
                    {
                        if (txtFilter.Text != "")
                        {
                            strSQL = "(select * from  UIPROFILEMAINTAINANCE where BUTTON LIKE '%" + txtFilter.Text.Trim() + "%')";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }
                        else
                        {
                            strSQL = "select * from  UIPROFILEMAINTAINANCE";
                            dataLoadList(strSQL, "UIPROFILEMAINTAINANCE");
                        }
                    }                    
                }


            }          
        }

        public void dataLoadList(string strSQL, string myString)
        {
            DataTable dtable = new DataTable();
            dataProvider lstData = new dataProvider();

            dtable = lstData.getDataTable(strSQL, myString);

            lstUiProfile.Items.Clear();

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    ListViewItem lvi = new ListViewItem(drow["SLNO"].ToString().Trim());
                    lvi.SubItems.Add(drow["MODULE"].ToString().Trim());
                    lvi.SubItems.Add(drow["SCREENNAME"].ToString().Trim());
                    lvi.SubItems.Add(drow["BUTTON"].ToString().Trim());
                    //lvi.SubItems.Add(drow["SLNO"].ToString().Trim());
                    lvi.SubItems.Add(drow["FormName"].ToString().Trim());
                    //lvi.SubItems.Add(drow["GRDGROUP"].ToString().Trim());
                    lstUiProfile.Items.Add(lvi);
                }
            }
        }

        public void dataLoadListForProfile(string strSQL, string myString)
        {
            DataTable dtable = new DataTable();
            dataProvider lstData = new dataProvider();

            dtable = lstData.getDataTable(strSQL, myString);

            lstUiProfile.Items.Clear();

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    ListViewItem lvi = new ListViewItem(drow["SLNO"].ToString().Trim());
                    lvi.SubItems.Add(drow["MODULE"].ToString().Trim());
                    lvi.SubItems.Add(drow["SCREENNAME"].ToString().Trim());
                    lvi.SubItems.Add(drow["PROFILEID"].ToString().Trim());                   
                    lvi.SubItems.Add(drow["FormName"].ToString().Trim());                    
                    lstUiProfile.Items.Add(lvi);
                }
            }
        }

        public void dataLoadListProfileID(string strSQL, string myString)
        {
            DataTable dtable = new DataTable();
            dataProvider lstData = new dataProvider();

            dtable = lstData.getDataTable(strSQL, myString);

            lstUiProfile.Items.Clear();

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    ListViewItem lvi = new ListViewItem(drow["PROFILEID"].ToString().Trim());
                    lvi.SubItems.Add(drow["BUTTON"].ToString().Trim());
                    lstUiProfile.Items.Add(lvi);
                }
            }
        }


    }
}
