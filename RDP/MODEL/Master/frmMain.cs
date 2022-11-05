using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using RDP.MODEL;
using System.IO;
using RDP;
using RDP.MODEL.ProductionPLanning;
using RDP.MODEL.UserManagement;

namespace RDP
{
    public partial class frmMain : Form
    {
        TreeNode _selectedNode = null;
        DataTable _acountsTb = null;
        SqlConnection _connection;
        SqlCommand _command;
        SqlDataAdapter _adapter;

        string sql;
        string firstUId;
        string fullName, CheckActive, passday;
        public string txtMainUserName { get; set; }
        public frmMain()
        {
            InitializeComponent();
            //_newNode = _thisLevel = _update = false;
            _acountsTb = new DataTable();
           _connection = new SqlConnection("Data Source=.;Initial Catalog=RDPLOCAL;user=sa;pwd=erp");
          //   _connection = new SqlConnection("Data Source=DESKTOP-PK5HOSK\\SQL2019;Initial Catalog=ARCHIVESKF;user=sa;pwd=erp");

         //   _connection = new SqlConnection("Data Source=10.168.90.212;Initial Catalog=ARCHIVESKF;user=sa;pwd=erp@123") ;
            _command = new SqlCommand();
            _command.Connection = _connection;
        }
        


        async void  timeInfo()
        {
            lblDate.Text = DateTime.Now.ToShortDateString();
            while (true)
            {
                lblTime.Text = DateTime.Now.ToLongTimeString();
                await Task.Delay(1000);
            }          
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            timeInfo();

            lblUser.Text = txtMainUserName.ToUpper();

            String sql = "SELECT distinct *  FROM [UAUTH] where type='Parent Account' AND [UID]='" + txtMainUserName + "'";  // UAUTH is a view
            try
            {
                _connection.Open();
                _adapter = new SqlDataAdapter(sql, _connection);

                _adapter.Fill(_acountsTb);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _connection.Close();
            }

            PopulateTreeView(0, null);
            //----------
            listView1.View = View.LargeIcon;
            //listView1.View = View.Details;
            listView1.Columns.Add("", 150);
            listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);  
        }
        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {

            TreeNode childNode;
            foreach (DataRow dr in _acountsTb.Select("[parent]=" + parentId))
            {
                TreeNode t = new TreeNode();
               // t.Text = dr["code"].ToString() + " - " + dr["ac_name"].ToString();
                 t.Text =  dr["ac_name"].ToString();
                t.Name = dr["code"].ToString();
                t.Tag = _acountsTb.Rows.IndexOf(dr);
                if (parentNode == null)
                {
                    treeView1.Nodes.Add(t);
                    childNode = t;
                }
                else
                {
                    parentNode.Nodes.Add(t);
                    childNode = t;
                }
                PopulateTreeView(Convert.ToInt32(dr["code"].ToString()), childNode);
            }
        }        
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {                 
            //listView1.Items.Clear();          
            //listView1.SmallImageList = imgs;

            _selectedNode = treeView1.SelectedNode;
            ShowNodeData(_selectedNode);
        }
        private void ShowNodeData(TreeNode nod)
        {
            DataRow r = _acountsTb.Rows[int.Parse(nod.Tag.ToString())];
            string id = r["code"].ToString();
            string name = r["ac_name"].ToString();

            sql = "select * from [UAUTH] where [UID]='" + txtMainUserName + "' and parent='" + id + "'";
            dataLoadList(sql, "UAUTH");

            autoResizeColumns(listView1);
            //listView1.Items.Add("User", 0);
        }
        public void dataLoadList(string sql, string myString)
        {//populate List view

            ImageList imgs = new ImageList();
            imgs.ImageSize = new Size(25, 25);

            string[] paths = { };
            //paths = Directory.GetFiles(@"F:\RDP\Repository_HRMS\Images\ico");
            //paths = Directory.GetFiles(@"D:\RDP\ECARE\Images\ico");
          //  paths = Directory.GetFiles(@"E:\RDP Versions\v1\RDP-New\Images\ico");
            paths = Directory.GetFiles(@"ico");

            try
            {
                foreach (string path in paths)
                {
                    imgs.Images.Add(Image.FromFile(path));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // End Populating Listview
            DataTable dtable = new DataTable();
            dataProvider lstData = new dataProvider();

            dtable = lstData.getDataTable(sql, myString);

            listView1.Items.Clear();

            //string activeStatus;

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    ListView lv = this.listView1;
                    lv.View = View.LargeIcon;

                   // Set the reference to your same member variable:
                   // lv.SmallImageList = imgs;
                   // lv.Items.Clear();

                    listView1.LargeImageList = imgs;
                    int imgno = Convert.ToInt32(drow["IMGNMBR"].ToString().Trim());
                    ListViewItem lvi = new ListViewItem(drow["ac_name"].ToString().Trim(), imgno);
                    lvi.SubItems.Add(drow["ac_name"].ToString().Trim());
                    //listView1.Items.Add("User", 0);
                    listView1.Items.Add(lvi);
                    
                }
            }
           
        }
        public static void autoResizeColumns(ListView lv)
        {
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            ListView.ColumnHeaderCollection cc = lv.Columns;
            for (int i = 0; i < cc.Count; i++)
            {
                int colWidth = TextRenderer.MeasureText(cc[i].Text, lv.Font).Width + 10;
                if (colWidth > cc[i].Width)
                {
                    cc[i].Width = colWidth;
                }
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {                        
            try
            {
                string selected = listView1.SelectedItems[0].SubItems[0].Text;

                if (selected == "Eskayef Pharmaceuticals Ltd")   //Parent Company Name
                {
                    listView1.Items.Clear();
                    listView1.Items.Add("User Management", 4);
                    listView1.Items.Add("Target Management", 4);

                }

                if (selected == "Administrative Service") // Parent UM
                {
                    listView1.Items.Clear();

                    listView1.Items.Add("User", 0); //Here 0 is Image Id  
                    listView1.Items.Add("Authorization", 1);
                    listView1.Items.Add("Security Group", 1);
                    listView1.Items.Add("UI Profile Maintenance", 2);
                    listView1.Items.Add("Assign UI Profile", 5);
                }

                //**** Child UM**************************
                if (selected == "User")
                {
                    frmUserCreation UserCreation = new frmUserCreation();
                    UserCreation.Owner = this;
                    UserCreation.txtMainUserName = txtMainUserName;
                    UserCreation.ShowDialog();
                }
                if (selected == "Authorization")
                {
                    frmUserAuthorization AdminUserAuthorization = new frmUserAuthorization();
                    AdminUserAuthorization.Owner = this;
                    AdminUserAuthorization.txtMainUserName = txtMainUserName;
                    AdminUserAuthorization.ShowDialog();
                }
                if (selected == "UI Profile Maintenance")
                {
                    frmUIProfileMaintenance UIProfile = new frmUIProfileMaintenance();
                    UIProfile.Owner = this;
                    UIProfile.txtMainUserName = txtMainUserName;
                    UIProfile.ShowDialog();
                }
                if (selected == "Assign UI Profile")
                {
                    frmAssignUIProfile AssignUI = new frmAssignUIProfile();
                    AssignUI.Owner = this;
                    AssignUI.txtMainUserName = txtMainUserName;
                    AssignUI.ShowDialog();
                }
                if (selected == "User Group")
                {
                    frmUserGroup userGroup = new frmUserGroup();
                    userGroup.Owner = this;
                    userGroup.txtMainUserName = txtMainUserName;
                    userGroup.ShowDialog();
                }
                if (selected == "Security Group")
                {
                    frmUserGroup userGroup = new frmUserGroup();
                    userGroup.Owner = this;
                    userGroup.txtMainUserName = txtMainUserName;
                    userGroup.ShowDialog();
                }
                //**** Child UM**************************
                if (selected == "Target Management") // Parent UM
                {
                    listView1.Items.Clear();

                    listView1.Items.Add("AHD Production Planning", 0); //Here 0 is Image Id  
                }
                // **********************Production Planning**********************
                if (selected == "P/P Batch Listing")
                {
                    BatchList bl = new BatchList(this);
                    bl.Owner = this;
              //      bl.txtMainUserName = txtMainUserName;
                    bl.ShowDialog();
                }
                if (selected == "Sales Planning") // Parent UM
                {
                    listView1.Items.Clear();

                    listView1.Items.Add("Item Setup", 0); //Here 0 is Image Id  
                    listView1.Items.Add("Sample Setup", 1);
                    listView1.Items.Add("Batch List", 1);
                    
                }
                if (selected == "Item Setup")
                {
                    frmItemSetup item = new frmItemSetup();
                    item.Owner = this;
                    //      bl.txtMainUserName = txtMainUserName;
                    item.ShowDialog();
                }

                if (selected == "Item Sample Setup")
                {
                    ItemSampleSetup sample = new ItemSampleSetup();
                    
                    sample.Owner = this;
                    
                    sample.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //if (e.Item.Focused)
            //e.Item.Selected = true;
        }

        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            //e.DrawDefault = true;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
