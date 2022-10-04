using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace RDP.Class
{
    class SQL
    {
        public SqlDataAdapter SqlDtAdptr;
        public DataTable DtTbl;
        public string condb;
        
        public DataTable get_rs(string STRSQL)
        {
            SqlConnection SqlConn = new SqlConnection(ConfigurationSettings.AppSettings.Get("SqlString"));            
            try
            {
                SqlConn.Open();                
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                //Environment.Exit(0);
            }
           
            SqlCommand SqlCmd = new SqlCommand();

            SqlCmd.CommandText = STRSQL;
            SqlCmd.Connection = SqlConn;
            SqlCmd.CommandTimeout = 600;

            SqlDtAdptr = new SqlDataAdapter(SqlCmd);
            DtTbl = new DataTable();
            SqlDtAdptr.Fill(DtTbl);
            SqlConn.Close();  

            return DtTbl;
        }
    }
 }

