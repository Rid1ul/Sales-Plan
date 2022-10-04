using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP.DAL
{
    class dataProvider
    {
        SqlConnection con = new SqlConnection();
        dbConnection dbCon = new dbConnection();
        SqlDataAdapter daGen;
        DataSet dsGen;

        // :::::::::::: Use this method to Execute a query string ::::::::::::
        public void ExecuteCommand(string MyQuery)
        {
            con = dbCon.dbConnect();
            con.Open();
            SqlCommand sqlComm = new SqlCommand(MyQuery, con);
            sqlComm.CommandTimeout = 600;  //Time out set= 4 minutes
            sqlComm.ExecuteNonQuery();
            con.Close();
        }

        // :::::::::::: Use this method to return a DataSet from query string ::::::::::::
        public DataSet getDataSet(string MyQuery, string MyString)
        {
            SqlConnection con = new SqlConnection();
            dbConnection dbCon = new dbConnection();
            SqlDataAdapter daGen;
            DataSet dsGen;
            dsGen = new DataSet();
            con = dbCon.dbConnect();
            daGen = new SqlDataAdapter(MyQuery, con);
            daGen.Fill(dsGen, MyString);

            return dsGen;
        }

        // :::::::::::: Use this method to execute query by DataReader ::::::::::::
        public static SqlDataReader getDataReader(string MyQuery)
        {
            SqlConnection con = new SqlConnection();
            dbConnection dbCon = new dbConnection();
            con = dbCon.dbConnect();
            con.Open();
            SqlCommand cmd = new SqlCommand(MyQuery, con);

            return cmd.ExecuteReader();
        }

        // :::::::::::: Use this method to return a DataTable from query string ::::::::::::
        public DataTable getDataTable(string MyQuery, string MyTable)
        {
            DataTable dt = new DataTable();

            SqlConnection con1 = new SqlConnection();
            dbConnection dbCon1 = new dbConnection();
            SqlDataAdapter daGen1;
            DataSet dsGen1;

            dsGen1 = new DataSet();
            con1 = dbCon1.dbConnect();
            daGen1 = new SqlDataAdapter(MyQuery, con1);
            daGen1.Fill(dsGen1, MyTable);
            dt = dsGen1.Tables[MyTable];
            dsGen1.Tables.Remove(MyTable);

            return dt;
        }

        public int getResultBit(string MyQuery)
        {
            DataTable dt = new DataTable();
            int resultBit;

            dsGen = new DataSet();
            con = dbCon.dbConnect();
            daGen = new SqlDataAdapter(MyQuery, con);
            daGen.Fill(dsGen);
            dt = dsGen.Tables[0];
            resultBit = Convert.ToInt16(dt.Rows[0][0]);

            return resultBit;
        }

        public string getResultString(string MyQuery)
        {
            DataTable dt = new DataTable();
            string resultString;

            dsGen = new DataSet();
            con = dbCon.dbConnect();
            daGen = new SqlDataAdapter(MyQuery, con);
            daGen.Fill(dsGen);
            dt = dsGen.Tables[0];
            resultString = dt.Rows[0][0].ToString();

            return resultString;
        }

        public static DataTable getResultStringConvert(string MyQuery)
        {
            DataTable dt = new DataTable();

            SqlConnection con1 = new SqlConnection();
            dbConnection dbCon1 = new dbConnection();
            SqlDataAdapter daGen1;
            DataSet dsGen1;

            dsGen1 = new DataSet();
            con1 = dbCon1.dbConnect();
            daGen1 = new SqlDataAdapter(MyQuery, con1);
            daGen1.Fill(dsGen1);
            dt = dsGen1.Tables[0];

            return dt;
        }
    }
}
