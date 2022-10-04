using System.Data.SqlClient;
using System.Configuration;

namespace RDP
{
    class dbConnection
    {
        public string cnnStr = "";
        public SqlConnection dbConnect()
        {
            cnnStr = ConfigurationSettings.AppSettings.Get("SqlString");           
            return new SqlConnection(cnnStr);
        }
    }
}
