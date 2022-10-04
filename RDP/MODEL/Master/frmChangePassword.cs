using System;
using System.Data;
using System.Windows.Forms;

namespace RDP
{
    public partial class frmChangePassword : Form
    {
        public string txtMainUserName { get; set; }
        DateTime passCreation;
        string strSQL;
        dataProvider DBexe = new dataProvider();
        public string userId;
        
        public frmChangePassword()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {


            try
            {
                string AUDTDATE = DateTime.Now.ToString("yyyyMMdd");
                string AUDTTIME = DateTime.Now.ToString("HHmmss");
                string Uid = "";

                strSQL = "select USERPASS from USERTBL where USERID='" + txtUserID.Text.Trim().ToUpper() + "'";
                DataTable dtable = new DataTable();
                dataProvider dtprovider = new dataProvider();

                dtable = dtprovider.getDataTable(strSQL, "USERTBL");

                for (int i = 0; i < dtable.Rows.Count; i++)
                {
                    DataRow drow = dtable.Rows[i];

                    if (drow.RowState != DataRowState.Deleted)
                    {
                        Uid = drow["USERPASS"].ToString().Trim();
                    }
                }
                string strPassword = txtOldPassword.Text;
                var conv = System.Text.Encoding.UTF8.GetBytes(strPassword);
                var decUserPassword = System.Convert.ToBase64String(conv);

                if (decUserPassword != Uid)
                {
                    MessageBox.Show("Wrong Old Password");
                    return;
                }
                if (txtPasswordVerify.Text =="" )
                {
                    MessageBox.Show("Provide Verification password");
                    return;
                }
                if (txtPasswordVerify.Text != txtNewPassword.Text)
                {
                    MessageBox.Show("Verification password is not same");
                    return;
                }

                string strPasswordNew = txtNewPassword.Text;
                var convNew = System.Text.Encoding.UTF8.GetBytes(strPasswordNew);
                var decUserPasswordNew = System.Convert.ToBase64String(convNew);


                if (txtOldPassword.Text != txtNewPassword.Text)
                {
                    strSQL = "update USERTBL set USERPASS = '" + decUserPasswordNew + "', PASSCREATIONDATE = '"
                            + DateTime.Today + "', DATELASTMN = '"
                            + AUDTDATE + "',AUDTTIME='" + AUDTTIME + "', LSTUSER = '" + txtMainUserName + "' where USERID = '" + txtUserID.Text.Trim().ToUpper() + "'";

                    DBexe.ExecuteCommand(strSQL);

                    MessageBox.Show("Password Updated Successful....");
                    this.Close();


                }
                else
                {
                    MessageBox.Show("Please provide different Password");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error from Save Event" + ex.Message);
            }
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            txtUserID.Text = userId;
        }
    }
}
