using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using RDP;
using RDP.Finder;
using RDP.FInder;


namespace RDP
{
    public partial class frmUserCreation : Form
    {
        string firstUId;
        string fullName, CheckActive, passday;
        public string txtMainUserName { get; set; }
        string showLogInInfo { get; set; }
        string strSQL, sqlStrUIPROFILEMAINTAINANCE;
        dataProvider dp = new dataProvider();
        int activeStatus;
        public frmUserCreation()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string AUDTDATE = DateTime.Now.ToString("yyyyMMdd");
                string AUDTTIME = DateTime.Now.ToString("HHmmss");
                string Uid = "";
                string passexpiaryday = "";
                dataProvider DBexe = new dataProvider();                            

                if (txtUserID.Text == "")
                {
                    MessageBox.Show("Enter User ID");
                    this.ActiveControl = txtUserID;
                    return;
                }
                //if (chkEmp.Checked == true)
                //{
                //    if (txtEmployeeSlNo.Text == "")
                //    {
                //        MessageBox.Show("Please Provide Employee ID .");
                //        return;                        
                //    }
                //}
                //if (chkApprover.Checked == true)
                //{
                //    if (txtApproverPIN.Text == "")
                //    {
                //        MessageBox.Show("Please Provide Approver PIN Code.");
                //        return;
                //    }
                //}
               
                if (txtUserName.Text == "")
                {
                    MessageBox.Show("Enter  User Name");
                    this.ActiveControl = txtUserName;
                    return;
                }
                if (txtPassword.Text == "")
                {
                    MessageBox.Show("Enter  Password");
                    this.ActiveControl = txtPassword;
                    return;
                }
                if (txtPasswordVerify.Text == "")
                {
                    MessageBox.Show("Enter  Verify Password");
                    this.ActiveControl = txtPasswordVerify;
                    return;
                }
                if (txtPassExpireDay.Text == "")
                {
                    //MessageBox.Show("Enter Password Expiry Day");
                    //this.ActiveControl = txtPassExpireDay;
                    //return;
                    passexpiaryday = DBexe.getResultString("select PASSLIFETIMEDAYS from PASSWORDPOLICY where SLNO='1'");
                }
                else
                {
                    passexpiaryday = txtPassExpireDay.Text;
                }
                if (txtPasswordVerify.Text != txtPassword.Text)
                {
                    MessageBox.Show("Password does not match");
                    this.ActiveControl = txtPasswordVerify;
                    return;
                }
                //if (cmbUserType.Text == "Select")
                //{
                //    MessageBox.Show("Select user type");
                //    this.ActiveControl = cmbUserType;
                //    return;
                //}
                if (EmailIsValid(txtEmail.Text)==false)
                {
                    MessageBox.Show("Enter  Valid Email");
                    this.ActiveControl = txtEmail;
                    return;
                }

                strSQL = "Select USERID from UMUSER where USERID='" + txtUserID.Text.Trim().ToUpper() + "'";
                DataTable dtable = new DataTable();
                dataProvider dtprovider = new dataProvider();

                dtable = dtprovider.getDataTable(strSQL, "UMUSER");

                for (int i = 0; i < dtable.Rows.Count; i++)
                {
                    DataRow drow = dtable.Rows[i];

                    if (drow.RowState != DataRowState.Deleted)
                    {
                        Uid = drow["USERID"].ToString().Trim();
                    }
                }

                if (chkAcIsDisable.Checked == true)
                {
                    activeStatus = 1;
                }
                else
                {
                    activeStatus = 0;
                }

                //if (fromAdd == true)
                //{
                getIpAddress();
                showLogInInfo = showLogInInfo;
                if (txtUserID.Text != Uid)
                {
                    if (Condition1.ForeColor==System.Drawing.Color.Green && Condition2.ForeColor == System.Drawing.Color.Green && Condition3.ForeColor == System.Drawing.Color.Green && Condition4.ForeColor == System.Drawing.Color.Green)
                    {
                        string strPassword = txtPassword.Text;
                        //string strEncryptPassword = clsEncryptDecrypt.base64Encode(strPassword);

                        var conv = System.Text.Encoding.UTF8.GetBytes(strPassword);
                        var decUserPassword = System.Convert.ToBase64String(conv);

                        //string hashedpassword = FormsAuthentication.HashPasswordForStoringInConfigFile("your password", "SHA1");

                        strSQL = "INSERT INTO UMUSER (AUDTDATE,AUDTTIME,AUDTUSER,USERID,PINCODE,USERFULLNAME,USERPASS,SWACTV,PASSCREATIONDATE,PASSEXPIREDAY,DATELASTMN,LSTUSER,USERTYPE,EMAIL,INACTIVEREASON)"
                                + " VALUES ('" + AUDTDATE + "','" + AUDTTIME + "', '" + txtMainUserName + "', '" + txtUserID.Text.Trim().ToUpper() + "','" + txtApproverPIN.Text.Trim() + "',"
                                + "'" + txtUserName.Text.Trim() + "','" + decUserPassword + "','" + activeStatus + "','" + DateTime.Today + "','" + passexpiaryday + "','" + AUDTDATE + "', '" + txtMainUserName + "'," +
                                "'" + cmbUserType.Text + "','" + txtEmail.Text + "','" + cmbInactiveReason.Text + "')";

                        DBexe.ExecuteCommand(strSQL);

                        //Log details
                        //string logUser = "Created User- UserID- " + txtUserID.Text.Trim() + " -UserName- " + txtUserName.Text.Trim();
                        //strSQL = "INSERT INTO LogDetails (CreateBy,Description,Action,Form) VALUES ('" + txtMainUserName + "', '" + logUser + "' , 'Add','User Creation')";
                        //DBexe.ExecuteCommand(strSQL);
                        //end Log Details

                        MessageBox.Show("Saved Successfully");
                        ClearAll();

                        //CreateBankId();

                        //strSQL = "Select * from BANK";
                        //master.dataLoadList(strSQL, "BANK");
                        //TextClean();
                    }
                    else
                    {
                        MessageBox.Show("Your password have to fullfill required password policy.","Stop",MessageBoxButtons.OK,MessageBoxIcon.Hand);
                    }

                }
                else if (txtUserID.Text == Uid)
                {
                    //Log details                   
                    string logUser = "Updated User- UserID" + txtUserID.Text.Trim() + " -UserName- " + txtUserName.Text.Trim();
                    strSQL = "INSERT INTO LogDetails (CreateBy,Description,Action,Form) VALUES ('" + txtMainUserName + "', '" + logUser + "' , 'Update','Creation')";
                    DBexe.ExecuteCommand(strSQL);
                    //end Log Details

                    string strPassword = txtPassword.Text;
                    //string strEncryptPassword = clsEncryptDecrypt.base64Encode(strPassword);
                    var conv = System.Text.Encoding.UTF8.GetBytes(strPassword);
                    var decUserPassword = System.Convert.ToBase64String(conv);

                    strSQL = "Update UMUSER set PINCODE='" + txtApproverPIN.Text.Trim() + "',USERFULLNAME = '" + txtUserName.Text + "'," +
                                "USERPASS='" + decUserPassword + "',SWACTV='" + activeStatus + "'," + "PASSEXPIREDAY='" + txtPassExpireDay.Text + "', " +
                                "PASSCREATIONDATE = '" + DateTime.Today + "' ,DATELASTMN = '" + AUDTDATE + "'," +
                                "AUDTTIME='" + AUDTTIME + "', LSTUSER = '" + txtMainUserName + "',USERTYPE='"+cmbUserType.Text+"',EMAIL='"+txtEmail.Text+"',INACTIVEREASON='"+cmbInactiveReason.Text+"' where SLNO = '" + txtSlNo.Text.Trim() + "' ";

                    DBexe.ExecuteCommand(strSQL);


                    MessageBox.Show("Updated Successfully");                    
                }
                    ClearAll();                                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error from Save Event" + ex.Message);
            }
        }
        public static bool EmailIsValid(string email)
        {
            string expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            if (System.Text.RegularExpressions.Regex.IsMatch(email, expression))
            {
                if (System.Text.RegularExpressions.Regex.Replace(email, expression, string.Empty).Length == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void getIpAddress()
        {
            string LocalIp = string.Empty;
            DateTime DaTi = DateTime.Now;

            string Domain = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;

            string Host = System.Net.Dns.GetHostName();

            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {

               MessageBox.Show("null value");
               return;

            }

            System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());

            foreach (System.Net.IPAddress ip in host.AddressList)
            {

                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {

                    LocalIp = ip.ToString();

                    break;
                }
            }

            string IpWidHost = String.Format("[Domain-{0} : Host-{1} : IP-{2}]", Domain, Host, LocalIp) +" Date: "+DaTi;
            showLogInInfo = IpWidHost;               
        }

        private void TextClean()
        {
            throw new NotImplementedException();
        }

        private void btnNewEntryNo_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            txtUserID.Text = "";
            //txtEmployeeSlNo.Text = "";
            //chkEmp.Checked = false;
            txtEmail.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtPasswordVerify.Text = "";
            txtPassExpireDay.Text = "";
            btnAdd.Text = "&Ok";
            txtSlNo.Text = "";
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

            FillData();
        }

        private void FillData()
        {           
            strSQL = "select * from UMUSER where USERID='" + txtUserID.Text.ToUpper() +"' ";
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();

            dtable = dtprovider.getDataTable(strSQL, "UMUSER");
            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];
                if (drow.RowState != DataRowState.Deleted)
                {
                    txtSlNo.Text = drow["SLNO"].ToString();
                    fullName = drow["USERFULLNAME"].ToString().Trim();
                    CheckActive = drow["SWACTV"].ToString().Trim();
                    passday = drow["PASSEXPIREDAY"].ToString().Trim();
                    cmbUserType.Text = drow["USERTYPE"].ToString().Trim();
                    txtEmail.Text = drow["EMAIL"].ToString().Trim();

                    if (dtable.Rows[0]["INACTIVEREASON"].ToString() != "")
                    {
                        cmbInactiveReason.Visible = true;
                        cmbInactiveReason.Text = drow["INACTIVEREASON"].ToString();
                        chkAcIsDisable.Checked = true;
                    }
                    if (dtable.Rows[0]["INACTIVEREASON"].ToString() == "")
                    {
                        cmbInactiveReason.Visible = false;
                        cmbInactiveReason.Text = drow["INACTIVEREASON"].ToString();
                        chkAcIsDisable.Checked = false;
                    }
                    if (dtable.Rows[0]["PINCODE"].ToString() != "")
                    {
                        txtApproverPIN.Visible = true;
                        txtApproverPIN.Text = drow["PINCODE"].ToString();
                        chkAppover.Checked = true;
                    }
                    if (dtable.Rows[0]["PINCODE"].ToString() == "")
                    {
                        txtApproverPIN.Visible = false;
                        chkAppover.Checked = false;
                        txtApproverPIN.Text = "";
                        txtApproverPIN.Text = drow["PINCODE"].ToString();

                    }
                }
            }
            if (CheckActive == "0")
            {
                chkAcIsDisable.Checked = false;
            }
            if (CheckActive == "1")
            {
                chkAcIsDisable.Checked = true;
            }

            txtUserName.Text = fullName;
            txtPassExpireDay.Text = passday;

            if (txtUserID.Text == "")
            {
                btnAdd.Text = "&Ok";
            }
            else
            {
                btnAdd.Text = "&Update";
            }            
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
            FillData();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            strSQL = "Select top 1(USERID)as USERID from (select USERID from  UMUSER where USERID<'"+txtUserID.Text+"') as UMUSER group by USERID order by USERID desc";
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
            FillData();
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
            FillData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUserCreation_Load(object sender, EventArgs e)
        {
            //getIpAddress();

            cmbUserType.SelectedIndex = 0;
            buttonPrivilege();

            string status = dp.getResultString("Select PASSMINLENSTATUS from PASSWORDPOLICY WHERE SLNO = '1'").Trim();
            if (status == "1")
            {
                Condition4.Visible = true;
                Condition4.Text = "* Minimum length should be " + PasswordLength().ToString() + " characters";
            }
            else
            {
                Condition4.Visible = false;
            }

            txtPassword.MaxLength = Convert.ToInt32(PasswordLength().ToString());
            txtPasswordVerify.MaxLength = Convert.ToInt32(PasswordLength().ToString());

            cmbInactiveReason.Visible = false;
            chkAcIsDisable.Checked = false;
            txtApproverPIN.Visible = false;
            label8.Visible = false;
        }
        private void buttonPrivilege()
        {
            strSQL = "select PROFILEID from UIASSIGNPROFILE where USERID='" + txtMainUserName + "'";
            DataTable dtableBtn = new DataTable();
            dataProvider dtproviderBtn = new dataProvider();
            dtableBtn = dtproviderBtn.getDataTable(strSQL, "UIASSIGNPROFILE");
            for (int i = 0; i < dtableBtn.Rows.Count; i++)
            {
                DataRow drow = dtableBtn.Rows[i];
                if (drow.RowState != DataRowState.Deleted)
                {
                    string proId = drow["PROFILEID"].ToString().Trim();
                    sqlStrUIPROFILEMAINTAINANCE = "select BUTTON from UIPROFILEMAINTAINANCE where PROFILEID='" + proId + "' and FORMNAME='" + this.Name + "'";
                    DataTable dtableUIPROFILEMAINTAINANCE = new DataTable();                    
                    dtableUIPROFILEMAINTAINANCE = dtproviderBtn.getDataTable(sqlStrUIPROFILEMAINTAINANCE, "UIPROFILEMAINTAINANCE");
                    for (int p = 0; p < dtableUIPROFILEMAINTAINANCE.Rows.Count; p++)
                    {
                        DataRow drowUIPROFILEMAINTAINANCE = dtableUIPROFILEMAINTAINANCE.Rows[p];

                        if (drowUIPROFILEMAINTAINANCE.RowState != DataRowState.Deleted)
                        {
                            string buttnAll = drowUIPROFILEMAINTAINANCE["Button"].ToString().Trim();
                            //if (buttnAll == "btnAdd")
                            //{
                            //    btnAdd.Visible = false;
                            //}
                            //if (buttnAll == "btnClose")
                            //{
                            //    btnClose.Visible = false;
                            //}
                        }
                    }
                }
            }
        }

        private void btnEntryNoFinder_Click(object sender, EventArgs e)
        {
            finderFrmUser userFinder = new finderFrmUser();
            userFinder.Owner = this;
            strSQL = "Select USERID,USERFULLNAME from UMUSER where SWACTV='0'";
            userFinder.sourceForm = "UserTable";
            userFinder.dataLoadList(strSQL, "UMUSER");
            userFinder.UserType();
            userFinder.ShowDialog();

            //txtUserID.Text = firstUId;
            FillData();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();
            strSQL = "Select * from PASSWORDPOLICY WHERE SLNO = '1'";
            dtable = dtprovider.getDataTable(strSQL, "PASSWORDPOLICY");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    if (drow["COMPLEXPASSSTATUS"].ToString().Trim() != "0")
                    {
                        if (HasSpecialChars(txtPassword.Text) == true)
                        {
                            Condition1.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            Condition1.ForeColor = System.Drawing.Color.Red;
                        }
                        if (HasCapital(txtPassword.Text) == true)
                        {
                            Condition3.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            Condition3.ForeColor = System.Drawing.Color.Red;
                        }
                        if (HasNumber(txtPassword.Text) == true)
                        {
                            Condition2.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            Condition2.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        Condition1.Visible = false;
                        Condition2.Visible = false;
                        Condition3.Visible = false;
                    }
                    
                    if (drow["PASSMINLENSTATUS"].ToString().Trim() != "0")
                    {
                        if (txtPassword.Text.Length >= Convert.ToInt32(drow["PASSMINLEN"].ToString().Trim()))
                        {
                            Condition4.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            Condition4.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            
        }

        private void chkEmp_Click(object sender, EventArgs e)
        {
            //if (chkEmp.Checked == true)
            //{
            //    txtEmployeeFullName.ReadOnly = true;
            //    txtEmployeeSlNo.Visible = true;
            //    btnFinder.Visible = true;
            //}
            //else
            //{
            //    txtEmployeeFullName.ReadOnly = false;
            //    txtEmployeeSlNo.Visible = false;
            //    btnFinder.Visible = false;

            //}
        }

        private void btnFinder_Click(object sender, EventArgs e)
        {
            //frmEmpSearch employeeSearch = new frmEmpSearch(null, null, null, null, null);
            //employeeSearch.Owner = this;
            //strSQL = "SELECT empId,fullName,designation,dept,Grade,SLNO from employeePrimaryInfo";
            //employeeSearch.loadListViewEmpSearch(strSQL, "employeePrimaryInfo");
            //employeeSearch.sourceForm = "EmployeeSearch";
            //employeeSearch.EmployeeType();
            //employeeSearch.ShowDialog();  
        }

        private void chkApprover_Click(object sender, EventArgs e)
        {
            //if (chkApprover.Checked == true)
            //{
            //    txtApproverPIN.Visible = true;
            //}
            //else
            //{
            //    txtApproverPIN.Visible = false;

            //    //txtEmployeeFullName.ReadOnly = false;
            //    //txtEmployeeSlNo.Visible = false;
            //    //btnFinder.Visible = false;

            //}
        }

        public bool HasSpecialChars(string yourString)
        {
            return yourString.Any(ch => !Char.IsLetterOrDigit(ch));
        }

        private void chkAcIsDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAcIsDisable.Checked==true)
            {
                cmbInactiveReason.Visible = true;
                chkAcIsDisable.Text = "Inactive";
            }
            else
            {
                cmbInactiveReason.Visible = false;
                chkAcIsDisable.Text = "Active";
            }
        }

        private void chkAppover_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAppover.Checked==true)
            {
                txtApproverPIN.Visible = true;
                label8.Visible = true;
            }
            else
            {
                txtApproverPIN.Visible = false;
                label8.Visible = false;
            }
        }

        private void cmbUserType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public bool HasNumber(string yourString)
        {
            return yourString.Any(ch => Char.IsDigit(ch));
        }
        public bool HasCapital(string yourString)
        {
            return yourString.Any(ch => Char.IsUpper(ch));
        }

        public int PasswordLength()
        {
            int length = 0;
            DataTable dtable = new DataTable();
            dataProvider dtprovider = new dataProvider();
            strSQL = "Select * from PASSWORDPOLICY WHERE SLNO = '1'";
            dtable = dtprovider.getDataTable(strSQL, "PASSWORDPOLICY");

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];

                if (drow.RowState != DataRowState.Deleted)
                {
                    if (drow["PASSMINLENSTATUS"].ToString().Trim() != "0")
                    {
                        length = Convert.ToInt32(drow["PASSMINLEN"].ToString());
                    }
                }
            }
            return length;
        }
    }
}
