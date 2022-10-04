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
using RDP.Class;
using RDP.Finder;

namespace RDP.MODEL.UserManagement
{
    public partial class frmUserAuthorization : Form
    {
        string sqlStr, sqlStrUIPROFILEMAINTAINANCE;
        SQL sq = new SQL();
        TreeNode _selectedNode = null;
        DataTable _acountsTb = null;
        SqlConnection _connection;
        SqlCommand _command;
        SqlDataAdapter _adapter;
        bool _newNode, _thisLevel, _update;
        int _parent = -1;
        string v_ac_name, v_type, v_fixed, v_direct, v_open_bal, v_UID, v_code, v_parent, v_levelno, v_active;
        int Row=0,Col=0;
        string id, description;

        string AUDTDATE = DateTime.Now.ToString("yyyyMMdd");
        string AUDTTIME = DateTime.Now.ToString("HHmmss");
        dataProvider DBexe = new dataProvider();

        // int  v_parent, v_levelno, v_active;
        //dt

        string sql;
        string firstUId;
        string fullName, CheckActive, passday;

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string col2="", col3="";
            int chkstatus = 0;
            if (txtUserID.Text != "")
            {
                strSQL = "delete from UMUSERAUTH where USERID='"+txtUserID.Text+"'";
                DBexe.ExecuteCommand(strSQL);

                for (int i = 0; i < dgvUserAuthorization.Rows.Count; i++)
                {
                    if (dgvUserAuthorization.Rows[i].Cells["Column2"].Value==null && dgvUserAuthorization.Rows[i].Cells["Column3"].Value == null)
                    {
                        chkstatus = 0;
                        col2 = "";
                        col3 = "";
                    }
                    else
                    {
                        if (dgvUserAuthorization.Rows[i].Cells["Column2"].Value.ToString() != "" && dgvUserAuthorization.Rows[i].Cells["Column3"].Value.ToString() != "")
                        {
                            col2 = dgvUserAuthorization.Rows[i].Cells["Column2"].Value.ToString();
                            col3 = dgvUserAuthorization.Rows[i].Cells["Column3"].Value.ToString();
                            chkstatus = 1;
                        }
                        else
                        {
                            chkstatus = 0;
                            col2 = "";
                            col3 = "";
                        }
                    }

                    strSQL = "insert into UMUSERAUTH (USERID,MODULE,GROUPID,GROUPDES,SELECTION,AUDTDATE,AUDTTIME,AUDTUSER,LASTMNDATE,LASTMNUSER) values(" +
                            "'" + txtUserID.Text + "','" + dgvUserAuthorization.Rows[i].Cells["Column1"].Value.ToString() + "','" + col2 + "'," +
                            "'" + col3 + "','"+chkstatus+"','" + AUDTDATE + "','" + AUDTTIME + "','" + txtMainUserName + "','" + AUDTDATE + "','" + txtMainUserName + "')";
                    DBexe.ExecuteCommand(strSQL);

                }
                MessageBox.Show("Saved Successfully");
            }
        }

        private void txtUserID_TextChanged(object sender, EventArgs e)
        {
            LaodUserDescription();
            LaodData();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadModule();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            strSQL = "delete from UMUSERAUTH where USERID='" + txtUserID.Text + "'";
            DBexe.ExecuteCommand(strSQL);
            LaodUserDescription();
            LaodData();
        }

        private void txtHideGroupID_TextChanged(object sender, EventArgs e)
        {
            if (txtHideGroupID.Text!="")
            {
                dgvUserAuthorization.Rows[Row].Cells["Column2"].Value = txtHideGroupID.Text;
                dgvUserAuthorization.Rows[Row].Cells["Column3"].Value = txtHideGroupDescription.Text;
                txtHideGroupID.Text = "";
            }

        }
       

        private void dgvUserAuthorization_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (txtUserID.Text != "")
            {
                if (dgvUserAuthorization.Columns[e.ColumnIndex].Name == "Column2")
                {
                    Row = e.RowIndex;
                    frmFinderUserGroup userGroup = new frmFinderUserGroup();
                    userGroup.Owner = this;
                    strSQL = "Select distinct GROUPID,GRPDESCRIPTION from UMUGRUP";
                    userGroup.sourceForm = "UserAuthentication";
                    userGroup.dataLoadList(strSQL, "UMUGRUP");
                    userGroup.UserType();
                    userGroup.ShowDialog();

                }
            }
            else
            {
                MessageBox.Show("Select User Id First.","Info",MessageBoxButtons.OK,MessageBoxIcon.Hand);
            }
            
        }

        public string txtMainUserName { get; set; }
        string showLogInInfo { get; set; }
        string strSQL;
        int activeStatus;
        public frmUserAuthorization()
        {
            InitializeComponent();
        }

        private void frmUserAuthorization_Load(object sender, EventArgs e)
        {
            LoadModule();
        }
        private void btnUserFinder_Click(object sender, EventArgs e)
        {
            finderFrmUser userFinder = new finderFrmUser();
            userFinder.Owner = this;
            strSQL = "Select USERID,USERFULLNAME from UMUSER where SWACTV='0'";
            userFinder.sourceForm = "UserAuthorization";
            userFinder.dataLoadList(strSQL, "UMUSER");
            userFinder.UserType();
            userFinder.ShowDialog();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            strSQL = "select top 1(USERID) from (select distinct USERID from UMUSER where USERID >'" + txtUserID.Text + "')as UMUSER ";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UMUSER");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    firstUId = drow["USERID"].ToString().Trim();
                }
            }

            txtUserID.Text = firstUId;
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            strSQL = "select max(USERID) as USERID from UMUSER";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UMUSER");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    firstUId = drow["USERID"].ToString().Trim();
                }
            }
            txtUserID.Text = firstUId;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            strSQL = "Select top 1(USERID)as USERID from (select USERID from  UMUSER where USERID<'" + txtUserID.Text + "') as UMUSER group by USERID order by USERID desc";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UMUSER");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    firstUId = drow["USERID"].ToString().Trim();
                }
            }
            txtUserID.Text = firstUId;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            strSQL = "select top 1 USERID from UMUSER";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UMUSER");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    firstUId = drow["USERID"].ToString().Trim();
                }
            }
            txtUserID.Text = firstUId;
        }

        public void LoadModule()
        {
            strSQL = "select distinct MODULE from ADMINBUTTONSETUP";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "ADMINBUTTONSETUP");

            dgvUserAuthorization.DataSource = null;
            dgvUserAuthorization.DataSource = dtable;
            dgvUserAuthorization.AutoGenerateColumns = false;

        }
        public void LaodUserDescription()
        {
            strSQL = "select USERFULLNAME from UMUSER where USERID='"+txtUserID.Text+"'";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UMUSER");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    txtUserName.Text = drow["USERFULLNAME"].ToString().Trim();
                }
            }
        }
        public void LaodData()
        {
            strSQL = "select MODULE,GROUPID,GROUPDES from UMUSERAUTH where USERID='"+txtUserID.Text+"'";
            DataTable dt = new DataTable();
            dt = DBexe.getDataTable(strSQL, "UMUSERAUTH");
     
            if (dt.Rows.Count>0)
            {
                dgvUserAuthorization.DataSource = dt;
                btnSave.Text = "Save";
            }
            else
            {
                btnSave.Text = "Add";
                LoadModule();
            }
        }
    }
}
