using RDP.DAL;
//using RDP.UI.Finder;
//using RDP.UI.Home;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDP
{
    public partial class frmItemSetup : Form
    {
        SQL conn = new SQL();
        string strSQL;
        public string txtMainUserName
        {
            get;set;
        }

        dataProvider aProvider = new dataProvider();
        public frmItemSetup()
        {
           
            InitializeComponent();
          this.StartPosition = FormStartPosition.CenterScreen;
            // this.WindowState = FormWindowState.Maximized;
            btnSave.Visible = false;
            cmbItemType.SelectedIndex = 0;
            cmbPlant.SelectedIndex = 0;
            cmbSector.SelectedIndex = 0;
            cmbProduction.SelectedIndex = 0;
            cmbExecutive.SelectedIndex = 0;
            lblItemNo.Visible = false;
            Itemload();
          

        }

        public void Itemload()
        {
            SQL sq = new SQL();
            DataTable dt = new DataTable();
            dt = sq.get_rs("Select ITEMNAME from PRINFOSKF Where EXECUTIVE_NAME LIKE '%" + txtMainUserName+"%' order by ITEMNAME asc ");
            foreach (DataRow r in dt.Rows)
            {
                cmbItem.Items.Add(r["ITEMNAME"].ToString());
            }
            cmbItem.SelectedIndex = 0;

        }
        public int CheckItem()
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Integrated Security=false;Initial Catalog=ARCHIVESKF;user=sa;pwd=erp");
          //  dbConnection cnn = new dbConnection();
            string itemName =cmbItem.Text ;

            string Query = "select * from ItemInfo where itemName LIKE '%" + itemName + "%' ";
            conn.Open();
            SqlCommand Command = new SqlCommand(Query, conn);
            SqlDataReader reader;
            reader = Command.ExecuteReader();
            while (reader.Read())
            {
                return 1;
            }
            conn.Close();


            return 0;
           
        }
       

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int Status = 0;
            if (cmbPlant.Text=="Select Option")
            {

                MessageBox.Show("Please Select Plant!");
                this.ActiveControl = cmbPlant;
                return;
            }
            if (cmbProduction.Text == "Select Option")
            {
                MessageBox.Show("Please Select Production Head!");
                this.ActiveControl = cmbProduction;
                return;
            }
            if (chkStatus.Checked)
            {
                Status = 1;
            }
            else
            {
                Status = 0;
            }
            int i = CheckItem();
            if (i == 0)
            {
                strSQL = "Insert Into Iteminfo ([itemId],[itemName],[UOM],[tradePrice],[sector],[plant],[itemType]," +
                    "[productionHead],[concernExecutive],[status],[createdBy],[createdDate]) VALUES ('"+lblItemNo.Text+"','"+cmbItem.Text+"'" +
                    ",'"+txtUOM.Text+"','"+txtTP.Text+"','"+cmbSector.Text+"','"+cmbPlant.Text+"','"+cmbItemType.Text+"'," +
                    "'"+cmbProduction.Text+"','"+cmbExecutive.Text+"','"+Status+"','"+txtMainUserName+ "','"+DateTime.Now+"')";
                aProvider.ExecuteCommand(strSQL);
                string logUser = "Insert ItemID  " + lblItemNo.Text;
                strSQL = "INSERT INTO LogDetails (CreateBy,Description,CreateDateTime,Action) VALUES ('" + txtMainUserName + "', '" + logUser + "' ,'" + DateTime.Now + "', 'Insert')";
                aProvider.ExecuteCommand(strSQL);
                MessageBox.Show("Item Add Successfully!");
                TextClear();
            }


        }
        public void TextClear()
        {
            cmbItem.SelectedIndex = 0;
            cmbExecutive.SelectedIndex = 0;
            cmbItemType.SelectedIndex = 0;
            cmbPlant.SelectedIndex = 0;
            cmbSector.SelectedIndex = 0;
            cmbProduction.SelectedIndex = 0;
            txtTP.Text = "";
            txtUOM.Text = "";
            chkStatus.Checked = false;
           
            btnSave.Visible = false;
            txtItemNo.Text = "";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        } 

        private void cmbItem_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            DataTable dtable = new DataTable();
            dataProvider lstData = new dataProvider();


            strSQL = "Select * from PRINFOHND WHERE ITEMNAME LIKE '" + cmbItem.Text + "%'";
            dtable = lstData.getDataTable(strSQL, "PRINFOHND");
            int check = CheckItem();
            if (check == 0)
            {
                for (int i = 0; i < dtable.Rows.Count; i++)
                {
                    DataRow drow = dtable.Rows[i];
                    if (drow.RowState != DataRowState.Deleted)
                    {
                        lblItemNo.Text = drow["ItemNo"].ToString().Trim();
                        txtTP.Text = drow["TRADEPRICE"].ToString().Trim();
                        txtUOM.Text = drow["PACKSIZE"].ToString().Trim();
                        cmbSector.Text = drow["SEGMENT01"].ToString().Trim();
                        cmbItemType.Text = drow["DOGES"].ToString().Trim();
                        cmbExecutive.Text = drow["EXECUTIVE_NAME"].ToString().Trim();
                    }
                }
                txtItemNo.Text = "";
                cmbPlant.SelectedIndex = 0;
                cmbProduction.SelectedIndex = 0;
             
                btnAdd.Visible = true;
                btnSave.Visible = false;
            }
            else if (check == 1)
            {
                DataTable dtable1 = new DataTable();
                strSQL = "Select * from itemInfo WHERE itemName LIKE '%" + cmbItem.Text + "%'";
                dtable1 = lstData.getDataTable(strSQL, "itemInfo");
                for (int i = 0; i < dtable1.Rows.Count; i++)
                {
                    DataRow drow = dtable1.Rows[i];
                    if (drow.RowState != DataRowState.Deleted)
                    {
                        txtItemNo.Text= drow["itemId"].ToString().Trim();
                        txtTP.Text = drow["tradePrice"].ToString().Trim();
                        txtUOM.Text = drow["UOM"].ToString().Trim();
                        cmbSector.Text = drow["sector"].ToString().Trim();
                        cmbItemType.Text = drow["itemType"].ToString().Trim();
                        cmbPlant.Text= drow["plant"].ToString().Trim();
                        cmbProduction.Text= drow["productionHead"].ToString().Trim();
                        cmbExecutive.Text= drow["concernExecutive"].ToString().Trim();

                    }
                }
                btnAdd.Visible = false;
                btnSave.Visible = true;
            }
          
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            strSQL = "Select top 1 itemId from (select itemId from itemInfo where itemId>'" + txtItemNo.Text + "') as itemInfo group by itemId order by itemId asc";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "itemInfo");
            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];
                if (drow.RowState != DataRowState.Deleted)
                {
                    txtItemNo.Text = drow["itemId"].ToString().Trim();
                }
            }
        }

        private void txtItemNo_TextChanged(object sender, EventArgs e)
        {
            if (txtItemNo != null)
            {
                DataTable dtable1 = new DataTable();
                dataProvider lstData = new dataProvider();
                strSQL = "Select * from itemInfo WHERE itemId = '" + txtItemNo.Text + "'";
                dtable1 = lstData.getDataTable(strSQL, "itemInfo");
                for (int i = 0; i < dtable1.Rows.Count; i++)
                {
                    DataRow drow = dtable1.Rows[i];
                    if (drow.RowState != DataRowState.Deleted)
                    {
                        txtItemNo.Text = drow["itemId"].ToString().Trim();
                        cmbItem.Text= drow["itemName"].ToString().Trim();
                        txtTP.Text = drow["tradePrice"].ToString().Trim();
                        txtUOM.Text = drow["UOM"].ToString().Trim();
                        cmbSector.Text = drow["sector"].ToString().Trim();
                        cmbItemType.Text = drow["itemType"].ToString().Trim();
                        cmbPlant.Text = drow["plant"].ToString().Trim();
                        cmbProduction.Text = drow["productionHead"].ToString().Trim();
                        cmbExecutive.Text = drow["concernExecutive"].ToString().Trim();
                    }
                }
                btnAdd.Visible = false;
                btnSave.Visible = true;
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            strSQL = "Select top 1 itemId from itemInfo ORDER BY itemId DESC";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "itemInfo");
            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];
                if (drow.RowState != DataRowState.Deleted)
                {
                    txtItemNo.Text = drow["itemId"].ToString().Trim();
                }
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            strSQL = "Select top 1(itemId)as itemId from (select itemId from  itemInfo where itemId<'" + txtItemNo.Text + "') as itemInfo group by itemId order by itemId desc";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "itemInfo");
            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];
                if (drow.RowState != DataRowState.Deleted)
                {
                    txtItemNo.Text = drow["itemId"].ToString().Trim();
                }
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            strSQL = "Select top 1 itemId from itemInfo ORDER BY itemId ASC";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "itemInfo");
            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];
                if (drow.RowState != DataRowState.Deleted)
                {
                    txtItemNo.Text = drow["itemId"].ToString().Trim();
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
           
            TextClear();
            btnSave.Visible = false;
            btnAdd.Visible = true;
        }

        private void btnFinder_Click(object sender, EventArgs e)
        {
            //frmFinderMasterCommon finderCommon = new frmFinderMasterCommon();
            //finderCommon.Owner = this;
            //strSQL = "Select * from itemInfo ";
            //finderCommon.dataLoadListForUser(strSQL, "itemInfo");
            //finderCommon.sourceForm = "ItemUI";
            //finderCommon.FinderUIType();
            //finderCommon.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmItemSetup_Load(object sender, EventArgs e)
        {

        }
    }
}
