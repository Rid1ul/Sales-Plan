using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RDP.Class;
using RDP.FInder;

namespace RDP.MODEL.ProductionPLanning
{
    public partial class ItemSampleSetup : Form
    {
        string strSQL;
        public ItemSampleSetup()
        {
            InitializeComponent();
        }


        private void ItemSampleSetup_Load(object sender, EventArgs e)
        {
            btnSave.Visible = true;
            btnSave.Enabled = true;
        }


        private void btnSampleFinder_Click(object sender, EventArgs e)
        {
            finderFrmItemSample sample = new finderFrmItemSample();
            sample.Owner = this;
            strSQL = "SELECT * FROM ITEM_SAMPLE";
            
            sample.dataLoadListSample(strSQL, "MSBRANDM");
            sample.ItemSampleSetup();
            sample.ShowDialog();
            //EditButton();
        }

        private void txtSampleID_TextChanged(object sender, EventArgs e)
        {
            strSQL = "SELECT * FROM ITEM_SAMPLE WHERE SAMPLE_ID = '" + txtSampleID.Text + "'";

            dataProvider dp = new dataProvider();
            DataTable dt = new DataTable();
            dt = dp.getDataTable(strSQL, "ITEM_SAMPLE");

            txtSampleName.Text = dt.Rows[0][1].ToString();
            txtSampleUOM.Text = dt.Rows[0][2].ToString();

            txtMktID.Text = dt.Rows[0][4].ToString();
            txtMktName.Text = dt.Rows[0][5].ToString(); ;
            txtMktUOM.Text = dt.Rows[0][6].ToString();
            


        }

        private void txtMktID_TextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            btnAdd.Visible = false;

            btnSave.Visible = true;
            btnSave.Enabled = true;

        }

        
    }
}
