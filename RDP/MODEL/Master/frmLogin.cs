using System;
using System.Data;
using System.Windows.Forms;

namespace RDP
{
    public partial class frmLogin : Form
    {
        public string txtMainUserName { get; set; }
        DateTime passCreation;
        string strSQL;
        int ExpireDay;
        dataProvider DBexe = new dataProvider();
        public frmLogin()
        {
            InitializeComponent();
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            cmbCompany.Items.Add("Eskayef Pharmaceuticals Limited");
            cmbCompany.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCompany.SelectedIndex = 0;
            dtpSessionDate.Value = DateTime.Now;

            this.AcceptButton = btnOK;            
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime dateLogin = DateTime.Today;
            
            if(txtUserID.Text == "")
            {
                MessageBox.Show("Enter User ID");
                return;
            }

            if (txtPassword.Text =="")
            {
                MessageBox.Show("Enter Password");
                return;
            }
            string strSQL;
            string UserID = "";
            string UserPass = "";
            string accntDisable = "";
            string textPass = txtPassword.Text;            
            
            var conv = System.Text.Encoding.UTF8.GetBytes(textPass);
            var decUserPassword = System.Convert.ToBase64String(conv);
            strSQL = "Select * from UMUSER   where USERID='"+ txtUserID.Text.ToUpper() +"'";

            DataTable dtable = new DataTable();           
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UMUSER");
            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    UserID = drow["USERID"].ToString().Trim();
                    UserPass = drow["USERPASS"].ToString().Trim();
                    accntDisable = drow["SWACTV"].ToString().Trim();
                    ExpireDay = Convert.ToInt32(drow["PASSEXPIREDAY"].ToString().Trim());
                    passCreation = Convert.ToDateTime(drow["PASSCREATIONDATE"].ToString().Trim());
                   ////////////////////
                    //var conv1 = System.Text.Encoding.UTF8.GetBytes(UserPass);
                    //var decUserPassword1 = System.Convert.ToBase64String(conv1);

                }
            }
            if (txtUserID.Text.ToUpper() != UserID)
            {
                MessageBox.Show("Invalid User");
                return;
            }
            if (Convert.ToInt16(accntDisable) == 1)
            {
                MessageBox.Show("Account is Disable");
                return;
            }
            if (decUserPassword != UserPass)
            {
                MessageBox.Show("Invalid Password");
                return;
            }

            TimeSpan ts = dateLogin - passCreation;
            int differenceInDays = ts.Days;

            if (differenceInDays >= ExpireDay)
            {
                MessageBox.Show("Change Password");
                txtUserID.Enabled = false;
                btnChangePassword.Enabled = true;
                return;
            }
            
            btnChangePassword.Enabled = true;
            //frmMain MasterForm = new frmMain();
            if (txtUserID.Text == "developer")
            {
               // frmAdminPanel masterForm1 = new frmAdminPanel();
               // this.Hide();
               //// MasterForm1.txtMainUserName = txtUserID.Text;
               // masterForm1.Show();
            }
            else             
            {
                frmMain MasterForm = new frmMain();
                 //   frmMainModule MasterForm = new frmMainModule();
                 this.Hide();
                 MasterForm.txtMainUserName = txtUserID.Text;


                 //Log details
                 //string logUser = "Loging For  "  + txtUserID.Text;                
                 //strSQL = "INSERT INTO LogDetails (CreateBy,Description,Action,Form) VALUES ('" + txtUserID.Text + "', '"+logUser+"' , 'Login','Login')";
                 //DBexe.ExecuteCommand(strSQL);

                
                MasterForm.Show();           
            }
           
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
       

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword ChangePassword = new frmChangePassword();
            ChangePassword.Owner = this;
            ChangePassword.txtMainUserName = txtMainUserName;
            ChangePassword.userId = txtUserID.Text;
            //ChangePassword.ChangePass = true;
            ChangePassword.ShowDialog();                                                   
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            DateTime dateLogin = DateTime.Today;

            //if (txtUserID.Text == "")
            //{
            //    MessageBox.Show("Enter User ID");
            //    return;
            //}

            //if (txtPassword.Text == "")
            //{
            //    MessageBox.Show("Enter Password");
            //    return;
            //}
            string strSQL;
            string UserID = "";
            string UserPass = "";
            string accntDisable = "";
            string textPass = txtPassword.Text;

            var conv = System.Text.Encoding.UTF8.GetBytes(textPass);
            var decUserPassword = System.Convert.ToBase64String(conv);
            strSQL = "Select * from UMUSER   where USERID='" + txtUserID.Text.ToUpper() + "'";

            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UMUSER");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    UserID = drow["USERID"].ToString().Trim();
                    UserPass = drow["USERPASS"].ToString().Trim();
                    accntDisable = drow["SWACTV"].ToString().Trim();
                    ExpireDay = Convert.ToInt32(drow["PASSEXPIREDAY"].ToString().Trim());
                    passCreation = Convert.ToDateTime(drow["PASSCREATIONDATE"].ToString().Trim());
                }
            }
            //if (txtUserID.Text.ToUpper() != UserID)
            //{
            //    MessageBox.Show("Invalid User");
            //    return;
            //}
            //if (Convert.ToInt16(accntDisable) == 1)
            //{
            //    MessageBox.Show("Account is Disable");
            //    return;
            //}
            if (decUserPassword != UserPass)
            {
                btnChangePassword.Enabled = true;
            }

            //TimeSpan ts = dateLogin - passCreation;
            //int differenceInDays = ts.Days;

            //if (differenceInDays >= ExpireDay)
            //{
            //    MessageBox.Show("Change Password");
            //    txtUserID.Enabled = false;
            //    btnChangePassword.Enabled = true;
            //    return;
            //}
           
            //frmMain MasterForm = new frmMain();
            //this.Hide();
            //MasterForm.txtMainUserName = txtUserID.Text;
            //MasterForm.Show();
        }        
    }
}
