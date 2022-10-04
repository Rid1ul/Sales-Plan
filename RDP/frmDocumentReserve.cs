using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using RDP.Class;

namespace RDP
{
    public partial class frmDocumentReserve : Form
    {
        private SqlCommand cmd;
        string strSQL, sqlStrUIPROFILEMAINTAINANCE;
        public string txtMainUserName { get; set; }
        dataProvider DBexe = new dataProvider();       

        string AUDTDATE = DateTime.Now.ToString("yyyyMMdd");
        string AUDTTIME = DateTime.Now.ToString("HHmmss");

        DataTable dtable = new DataTable();
        SQL sq = new SQL();
        public frmDocumentReserve()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                //UserSelectedFilePath = txtBrowsePath.Text;

                OpenFileDialog openFileDialog = new OpenFileDialog();
                DialogResult dr = openFileDialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    //UserSelectedFilePath = openFileDialog.FileName;
                    //btnUpload.PerformClick();

                    txtBrowsePath.Text = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("You must select Excel file which name Batch Upload" + ex.Message);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {           

        }
    }
}
