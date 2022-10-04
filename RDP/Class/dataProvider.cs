using System;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using OfficeOpenXml;

using System.Windows.Forms;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Globalization;

namespace RDP
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

        public void ExportToExcel(string transno, int version, string userid,string batch)
        {
            
            dataProvider DBexe = new dataProvider();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();

            //string strSQL = "select BatchNo, BATCHDESC, BATCHDATE, RDPPERIOD,[Status], Sum(cast(BATCHTOTAL as int)) as BatchTotal,count(TransNo) as NoofEntry,min(TRANSNO) as TransNo, max(RDPDATE) as LastEdited from ITEM_Production_Header where BATCHNO = '"+batch+"' and status = 0 group by BATCHDESC,BatchNo,BATCHDATE,RDPPERIOD,[Status]";
            string strSQL = "select BatchNo, BATCHDESC, BATCHDATE, RDPPERIOD,[Status], Sum(cast(BATCHTOTAL as int)) as BatchTotal,count(TransNo) as NoofEntry,min(TRANSNO) as TransNo, max(RDPDATE) as LastEdited from ITEM_Production_Header where BATCHNO = '" + batch + "' group by BATCHDESC,BatchNo,BATCHDATE,RDPPERIOD,[Status]";

            dt = DBexe.getDataTable(strSQL, "ITEM_Production_Header");


            //dt2 = DBexe.getDataTable("select * from item_production_planning where TransNo = '" + transno + "' and version = '" + version + "'", "item_production_planning");

            strSQL = "select batchno, TransNo,Executive,production,avgSalesPeriodFrom, avgSalesPeriodTo, RDPDate, ItemID, ItemName,sector, plant, productType,uom, tp, TongiStock, RupgonjStock, TotalFGStock,tdclstock, jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec, AvgSales, prevQty, SystemQty, ActualQty, RDPMonth2, RDPMonth3, RSPMonth1, RSPMonth2, RSPMonth3, ExpDeliveryDate, Remark, Status, CreatedDate,CreatedBy, stock, Version, Trend from ITEM_Production_Planning where TransNo = '"+transno+"' and version = '"+version+"'";

            dt2 = DBexe.getDataTable(strSQL, "item_production_planning");

            strSQL = "select itemid, RDP_MONTH, SAMPLE_ID,SAMPLE_NAME,UOM,MKT_UOM,STATUS, STKQTY, Month1,Month2, Month3, AUDTUSER from RSP_Monthly where transno = '"+transno+"' and version = '"+version+"' ";
            dt3 = DBexe.getDataTable(strSQL, "RSP_Monthly");

            CultureInfo cultures = new CultureInfo("en-US");
            DataTable dtClone = dt2.Clone();
            dtClone.Columns[4].DataType = typeof(string);
            dtClone.Columns[5].DataType = typeof(string);
            dtClone.Columns[6].DataType = typeof(string);

            foreach (DataRow row in dt2.Rows)
            {
                dtClone.ImportRow(row);
            }

            for(int i=0;i<dtClone.Rows.Count;i++)
            {
                dtClone.Rows[i][4] = Convert.ToDateTime(dtClone.Rows[i][4].ToString()).ToString("MM/dd/yyyy");
                dtClone.Rows[i][5] = Convert.ToDateTime(dtClone.Rows[i][5].ToString()).ToString("MM/dd/yyyy");
                dtClone.Rows[i][6] = Convert.ToDateTime(dtClone.Rows[i][6].ToString()).ToString("MM/dd/yyyy");
            }

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    var fileInfo = new FileInfo(sfd.FileName);
                    using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                    {
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Batch_Info");
                        worksheet.Cells.LoadFromDataTable(dt.Copy(), true);

                        ExcelWorksheet worksheet2 = excelPackage.Workbook.Worksheets.Add("Entry_Info");
                        //worksheet2.Cells.LoadFromDataTable(dt2.Copy(), true);

                        // Color Column
                        //using (ExcelRange rng = worksheet2.Cells["AI:AI"])
                        //{

                        //worksheet2.Cells["F:H"].Style.Numberformat.Format = "yyyy-MM-dd";

                        worksheet2.Cells["AH:AH"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet2.Cells["AH:AH"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet2.Cells["AH:AH"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet2.Cells["AH:AH"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet2.Cells["AH:AH"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet2.Cells["AH:AH"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(116, 222, 215));

                        worksheet2.Cells["AI:AI"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet2.Cells["AI:AI"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet2.Cells["AI:AI"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet2.Cells["AI:AI"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet2.Cells["AI:AI"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet2.Cells["AI:AI"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(116, 222, 215));

                        worksheet2.Cells["AJ:AJ"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet2.Cells["AJ:AJ"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet2.Cells["AJ:AJ"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet2.Cells["AJ:AJ"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet2.Cells["AJ:AJ"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet2.Cells["AJ:AJ"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(116, 222, 215));

                        

                        

                        worksheet2.Cells.LoadFromDataTable(dtClone.Copy(), true);


                        ExcelWorksheet worksheet3 = excelPackage.Workbook.Worksheets.Add("RSP_Info");
                        worksheet3.Cells.LoadFromDataTable(dt3.Copy(), true);

                        excelPackage.Save();
                        MessageBox.Show("Generated Excel File Successfully");
                    }
                }
            }


        }
    }
}
