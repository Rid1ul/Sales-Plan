using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDP.FInder
{


    public partial class finderFrmItemSample : Form
    {
        public string txtMainUserName { get; set; }
        private string strSQL;
        

        public finderFrmItemSample()
        {
            InitializeComponent();

            lstFinder.View = View.Details;
            lstFinder.LabelEdit = true;
            lstFinder.AllowColumnReorder = true;
            lstFinder.FullRowSelect = true;
            lstFinder.GridLines = true;
            lstFinder.Sorting = SortOrder.Ascending;
        }

        private void finderFrmItemSample_Load(object sender, EventArgs e)
        { 
            
        }

       public void ItemSampleSetup()
        {
            lstFinder.Columns.Add("Sample ID", 80, HorizontalAlignment.Left);
            lstFinder.Columns.Add("Sample Name", 240, HorizontalAlignment.Left);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void dataLoadListSample(string strSQL, string myString)
        {
            DataTable dtable = new DataTable();
            dataProvider lstData = new dataProvider();

            dtable = lstData.getDataTable(strSQL, myString);
            lstFinder.Items.Clear();
            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    ListViewItem lvi = new ListViewItem(drow["SAMPLE_ID"].ToString().Trim());
                    lvi.SubItems.Add(drow["SAMPLE_Name"].ToString());

                    lstFinder.Items.Add(lvi);
                }
            }
        }

        public void SendText()
        {
            try
            {                
                this.Owner.Controls.Find("txtSampleID", true).First().Text = lstFinder.SelectedItems[0].Text;
              //  this.Owner.Controls.Find("txtSampleName", true).First().Text = lstFinder.SelectedItems[0].SubItems[1].Text;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SendText();            
            this.Close();
        }

        private void lstFinder_DoubleClick(object sender, EventArgs e)
        {
            this.btnSelect.PerformClick();
        }
    }
}
