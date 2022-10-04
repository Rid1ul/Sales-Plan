using ExcelDataReader;
using OfficeOpenXml;
using RDP.DAL;
using RDP.MODEL.ProductionPLanning ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RDP.UI.Target_Management
{
    public partial class frmTargetManagement : Form
    {
        int top = 0;
        int left = 0;
        int height = 0;
        int width1 = 0;
        int vs;
        string user;
        string strSQL;
        string userName;
        string frmdatedata = "";
        public string transNo;
        string todayDate;
        string serial;
        float trend;
        int avgSales;
        int commaStatus=0;
        double sysQty;
        int rdpTotao, rspTotal, rdpRsptotal;
        public string entryNo;
        public string batchTotal;
        public string batchDesc;
        public string batchNo;
        public int transLoadStatus = 0;
        string status="0";
        public int batchStatus;
        public string batchDate;
        


        string AUDTDATE = DateTime.Now.ToString("yyyyMMdd");
        string AUDTTIME = DateTime.Now.ToString("HHmmss");
        DataTable bat = new DataTable();
        DataTable load = new DataTable();
        DataTable PostDeleteStatus = new DataTable();
        DataTable RSPSL = new DataTable();
        DataTable RSPLoad = new DataTable();


        dataProvider provider = new dataProvider();
        private frmMain master;
        public frmTargetManagement(string userName1, string TransNo, int EntryNo)
        {
            dataProvider DBexe = new dataProvider();

            InitializeComponent();
            lblEntryNo.Text = EntryNo.ToString();

            if(TransNo=="0")
            {
                transLoadStatus = 1;
                txtTransNo.Text = "****NEW****";
            }
            else
            {
                txtTransNo.Text = TransNo.ToString();
            }
            
            userName = userName1;

            //       string userName = DBexe.getResultString("select userfullname from umuser where USERID ='" + userName1 +"'");

            if (TransNo == "0" && EntryNo == 0)
            {
                cmbSector.SelectedIndex = 0;
                cmbVersion.SelectedIndex = 0;
                lblTotalItem.Text = "";
                DateTime date = dtpRDPPeriod.Value;
                DateTime datetime = date.AddMonths(-3);
                var month = datetime.Month;
                dtpAvgSales.Value = datetime;
                btnLoad.Visible = true;
                btnAdd.Visible = true;
                btnSave.Visible = false;
                DateTime dateTime1 = date.AddMonths(-1);
                dtpTo.Value = dateTime1;
                dtpAvgSales.Value = datetime;
                frmdatedata = dtpTo.Value.Date.ToString("yyyyMMdd");

                txtBatchNo.Text = "****NEW****";               
            }
            else
            {
                //cmbSector.SelectedIndex = 0;
                //cmbVersion.SelectedIndex = 0;
                //lblTotalItem.Text = "";

                strSQL = "select top 1 avgSalesPeriodFrom, avgSalesPeriodTo,RDPDate from ITEM_Production_Planning"; // where TransNo = '" + txtTransNo.Text + "' ";
                DataTable dates = new DataTable();
                dates = provider.getDataTable(strSQL, "ITEM_Production_Planning");

                dtpAvgSales.Value = Convert.ToDateTime(dates.Rows[0][2].ToString());
                dtpAvgSales.Value = Convert.ToDateTime(dates.Rows[0][0].ToString());
                dtpTo.Value = Convert.ToDateTime(dates.Rows[0][1].ToString());
            }            

            this.transNo = TransNo;
            cmbSector.SelectedIndex = 0;


            if(TransNo=="0")
            {
                string Name = DBexe.getResultString("select userfullname from umuser where USERID ='" + userName1 + "'");
                lblUserID.Text = userName + " - " + Name;
                txtUser.Text = userName;
            }
            else
            {
                string Name = DBexe.getResultString("select top 1 executive from Item_Production_Header where " +
                    "TRANSNO='" + txtTransNo.Text +"'");
                lblUserID.Text =  Name;
                txtUser.Text =Name;
               // LoadRSP();

            }
            

            if (EntryNo != 0)
            {
                txtBatchNo.Text = EntryNo.ToString("D4");
                txtBatchDesc.ReadOnly = true;
            }
            else
            {
                txtBatchNo.Text = "****NEW****";
                txtBatchDesc.ReadOnly = false;
            }




            if (TransNo != "0")
            {
                strSQL = "select top 1 status from Item_Production_Header where BATCHNO = '" + entryNo + "'";
                PostDeleteStatus = provider.getDataTable(strSQL, "Item_Production_Header");
                batchStatus = Int32.Parse(PostDeleteStatus.Rows[0][0].ToString());

                if(batchStatus ==1 || batchStatus == 2)
                {
                    btnAdd.Visible = false;
                    btnAdd.Enabled = false;

                    btnSave.Visible = true;
                    btnSave.Enabled = false;
                    btnPost.Enabled = false;
                    
                }
                else
                {
                    btnAdd.Visible = false;
                    btnAdd.Enabled = false;

                    btnSave.Visible = true;
                    btnSave.Enabled = true;
                }
                
            }            

        }

        private List<TopHeader> _headers = new List<TopHeader>();
        public List<TopHeader> Headers
        {
            get { return _headers; }
        }
        public struct TopHeader
        {
            public TopHeader(int index, int span, string text)
            {
                this.Index = index;
                this.Span = span;
                this.Text = text;
            }
            public int Index;
            public int Span;
            public string Text;
        }

        private void frmTargetManagement_Load(object sender, EventArgs e)
        {
            cmbRange.SelectedIndex = 0;
            //lblUserID.Text = userName;
            DataTable dtable = new DataTable();
            dataProvider lstData = new dataProvider();

            if (batchTotal != null)
            {
                txtBatchTotal.Text = batchTotal;
            }
            if (batchDesc != null)
            {
                txtBatchDesc.Text = batchDesc;
            }
            if (batchDate != null)
            {
                dtpBatchDate.Value = Convert.ToDateTime(batchDate);
            }
            


            dgvRandom.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;


            //dgvRandom.Columns[11].DefaultCellStyle.Format = "#,##0";

            this.dgvGPMItemInfo.RowsDefaultCellStyle.BackColor = Color.White;
            this.dgvGPMItemInfo.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;


            //this.WindowState = FormWindowState.Maximized;
            frmTargetManagement helper = new frmTargetManagement(this.dgvRandom);
            helper.Headers.Add(new frmTargetManagement.TopHeader(11, 1, "FG Info."));
            helper.Headers.Add(new frmTargetManagement.TopHeader(19, 1, "Sales Info."));
            helper.Headers.Add(new frmTargetManagement.TopHeader(21, 5, "RDP Target"));                                   ////******CHANGE*****/////
            helper.Headers.Add(new frmTargetManagement.TopHeader(26, 5, "RSP Target"));


            this.dgvRandom.ColumnHeadersHeight = 50;
            dgvRandom.ColumnHeadersDefaultCellStyle.BackColor = Color.White;

            //dgvRandom.EnableHeadersVisualStyles = false;
            // dgvRandom.Columns[0].HeaderCell.Style.BackColor = Color.WhiteSmoke;

            dgvRandom.Columns[4].Visible = false;
            dgvRandom.Columns[5].Visible = false;
            dgvRandom.Columns[6].Visible = false;
            dgvRandom.Columns[8].Visible = false;
            dgvRandom.Columns[9].Visible = false;                                                                             ////******CHANGE*****/////
            dgvRandom.Columns[10].Visible = false;
            dgvRandom.Columns[12].Visible = false;
            dgvRandom.Columns[13].Visible = false;
            dgvRandom.Columns[14].Visible = false;
            dgvRandom.Columns[15].Visible = false;
            dgvRandom.Columns[16].Visible = false;
            dgvRandom.Columns[17].Visible = false;


            btnPrint.Visible = false;
            if (txtTransNo.Text != "****NEW****")
            {
                btnPrint.Visible = true;
            }

            foreach (DataGridViewRow Myrow in dgvRandom.Rows)
            {
                Myrow.Cells[0].Style.BackColor = Color.LightGray;
                Myrow.Cells[1].Style.BackColor = Color.LightGray;
                Myrow.Cells[2].Style.BackColor = Color.LightGray;
                Myrow.Cells[3].Style.BackColor = Color.LightGray;                                                         ////******CHANGEd*****/////
                Myrow.Cells[4].Style.BackColor = Color.LightGray;
                Myrow.Cells[5].Style.BackColor = Color.LightGray;
                Myrow.Cells[6].Style.BackColor = Color.LightGray;
                Myrow.Cells[7].Style.BackColor = Color.LightGray;
                Myrow.Cells[8].Style.BackColor = Color.LightGray;
                Myrow.Cells[9].Style.BackColor = Color.LightGray;
                Myrow.Cells[10].Style.BackColor = Color.LightGray;
                Myrow.Cells[11].Style.BackColor = Color.LightGray;
                Myrow.Cells[12].Style.BackColor = Color.LightGray;
                Myrow.Cells[13].Style.BackColor = Color.LightGray;
                Myrow.Cells[14].Style.BackColor = Color.LightGray;
                Myrow.Cells[15].Style.BackColor = Color.LightGray;
                Myrow.Cells[16].Style.BackColor = Color.LightGray;
                Myrow.Cells[17].Style.BackColor = Color.LightGray;
                Myrow.Cells[18].Style.BackColor = Color.LightGray;
                Myrow.Cells[19].Style.BackColor = Color.LightGray;
                Myrow.Cells[20].Style.BackColor = Color.LightGray;
                Myrow.Cells[21].Style.BackColor = Color.LightGray;
         //       Myrow.Cells[22].Style.BackColor = Color.LightGray;
                Myrow.Cells[27].Style.BackColor = Color.LightGray;

                Myrow.Cells[28].Style.BackColor = Color.LightGray;
                Myrow.Cells[29].Style.BackColor = Color.LightGray;
                Myrow.Cells[30].Style.BackColor = Color.LightGray;



                //  LoadGPMItemInfo();
            }
            CalculateTotal();
            if (transNo != "0")
            {
                //AddComma(12);
                //AddComma(13);
                //AddComma(14);
                //AddComma(15);
                //AddComma(16);
                //AddComma(17);
                //AddComma(19);
                //AddComma(22);

                //AddComma(12);
                //AddComma(13);
                //AddComma(14);
                //AddComma(15);
                //AddComma(16);
                //AddComma(17);
                //AddComma(18);
                //AddComma(19);
                //AddComma(20);
                //AddComma(21);
                //AddComma(22);

            }

            //commaStatus = 1;
            LoadGPMItemInfo();
        }

        private void LoadRSP()
        {
            int v;
            if(cmbVersion.Text == "Select Option" || cmbVersion.Text =="")
            {
                v = 0;
            }
            else
            {
                v = Int32.Parse(cmbVersion.Text);
            }
                
            string str = "select * from RSP_monthly where transno = '" + txtTransNo.Text + "' and version ='"+v+"'";
            RSPLoad = provider.getDataTable(str, "RSP_Monthly");
        }


        private void LoadGPMItemInfo()
        {
            DataTable dtable = new DataTable();
            strSQL = "Select tblItem.*,isnull(tblTrans.CompletedItem, 0) as CompletedItem from (select GPMID, GPMNAME, COUNT(ITEMNO) AS TotalItem " +
                "from PRINFOSKF   WHERE STATUS = 1 AND GPMID IS NOT NULL AND GPMID <> 1 GROUP BY GPMID, GPMNAME) AS tblItem left join " +
                "(SELECT T1.GPMID, CompletedItem FROM (select LEFT(CreatedBy,4) as GPMID,Max(Version) as Version from ITEM_Production_Planning where " +
                "convert(varchar(7),RDPDate,23) = '" + dtpRDPPeriod.Value.ToString("yyyy-MM")+"' group by LEFT(CreatedBy, 4)) as T1 inner join (select LEFT(CreatedBy,4) as GPMID,Version,Count(ItemID) as" +
                " CompletedItem from ITEM_Production_Planning  where convert(varchar(7),RDPDate,23) = '" + dtpRDPPeriod.Value.ToString("yyyy-MM") + "' and ActualQty<>'' group by LEFT(CreatedBy, 4),Version) as T2 " +
                "on(T1.GPMID = T2.GPMID) AND (T1.VERSION = T2.VERSION)) as tblTrans on(tblItem.GPMID = tblTrans.GPMID)";

            dtable = provider.getDataTable(strSQL, "ITEM_Production_Planning");

            dgvGPMItemInfo.AutoGenerateColumns = false;
            dgvGPMItemInfo.DataSource = dtable;

            double total, completed, percent;
            for (int i = 0; i < dgvGPMItemInfo.Rows.Count; i++)
            {
                total = Convert.ToDouble(dgvGPMItemInfo.Rows[i].Cells[2].Value);
                completed = Convert.ToDouble(dgvGPMItemInfo.Rows[i].Cells[3].Value);
                percent = (completed / total) * 100;
                dgvGPMItemInfo.Rows[i].Cells[4].Value = Math.Round(percent, 2)+" %";
            }

            //this.dgvGPMItemInfo.RowsDefaultCellStyle.BackColor = Color.White;
            //this.dgvGPMItemInfo.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

        }

        private void CalculateTotal()
        {
            string rspValue, rdpValue, grandTotal;
            double rsp, rdp;
            double rspTot = 0, rdpTot = 0;

            for (int i = 0; i < dgvRandom.Rows.Count; i++)
            {
                if (dgvRandom.Rows[i].Cells[28].Value == null || dgvRandom.Rows[i].Cells[28].Value == DBNull.Value || String.IsNullOrWhiteSpace(dgvRandom.Rows[i].Cells[28].Value.ToString()))
                {
                    rsp = 0;
                }
                else
                {
                    rsp = Convert.ToDouble(dgvRandom.Rows[i].Cells[28].Value.ToString());
                }

                rspTot += (Convert.ToDouble(dgvRandom.Rows[i].Cells[7].Value.ToString()) * rsp);
            }

            rspValue = String.Format("{0:n0}", rspTot);
            lblRspTot.Text = rspValue.ToString();

            for (int i = 0; i < dgvRandom.Rows.Count; i++)
            {
                if (dgvRandom.Rows[i].Cells[23].Value == null || dgvRandom.Rows[i].Cells[23].Value == DBNull.Value || String.IsNullOrWhiteSpace(dgvRandom.Rows[i].Cells[23].Value.ToString()))
                {
                    rdp = 0;
                }
                else
                {
                    rdp = Convert.ToDouble(dgvRandom.Rows[i].Cells[23].Value.ToString().Replace(",",""));
                }

                rdpTot += (Convert.ToDouble(dgvRandom.Rows[i].Cells[7].Value.ToString()) * rdp);

            }
            rdpValue = string.Format("{0:n0}", rdpTot);
            lblRdpTot.Text = rdpValue.ToString();

            grandTotal = string.Format("{0:n0}", Math.Round(rdpTot + rspTot));
            lblGrandTot.Text = grandTotal.ToString();
        }

        private void CalculateSystemQty()
        {
            double avg=0;
            for (int i = 0; i < dgvRandom.Rows.Count; i++)
            {
                //value = dgvRandom.Rows[i].Cells[v].Value.ToString();
                //value = String.Format("{0:n0}", Int32.Parse(value));

                //avgSales = Int32.Parse(dgvRandom.Rows[i].Cells[19].Value.ToString());
                //avgSales = Math.Round( avgSales * 1.5);
                //dgvRandom.Rows[i].Cells[22].Value = Math.Round(avgSales * 1.5);

                avg = Convert.ToDouble(dgvRandom.Rows[i].Cells[19].Value.ToString());
                avg = avg * 1.5;
                dgvRandom.Rows[i].Cells[22].Value = string.Format("{0:n0}", avg);
                avg = 0;

            }
        }


        public frmTargetManagement(DataGridView gridview)
        {
            gridview.CellPainting += new DataGridViewCellPaintingEventHandler(dgvRandom_CellPainting);
        }

        private void dgvRandom_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            #region Redraw the datagridview header
            DataGridView dgv = (DataGridView)(sender);
            if (e.RowIndex != -1) return;
            foreach (TopHeader item in Headers)
            {
                if (e.ColumnIndex >= item.Index && e.ColumnIndex < item.Index + item.Span)
                {
                    if (e.ColumnIndex == item.Index)
                    {
                        top = e.CellBounds.Top;
                        left = e.CellBounds.Left;
                        height = e.CellBounds.Height;
                    }
                    int width = 0;//total length
                    for (int i = item.Index; i < item.Span + item.Index; i++)
                    {
                        width += dgv.Columns[i].Width;
                    }
                    Rectangle rect = new Rectangle(left, top, width, e.CellBounds.Height);
                    using (Brush backColorBrush = new SolidBrush(e.CellStyle.BackColor)) //Cell background color
                    {
                        //Erase the original cell background
                        e.Graphics.FillRectangle(backColorBrush, rect);
                    }
                    using (Pen gridLinePen = new Pen(dgv.GridColor)) //Pen color
                    {
                        e.Graphics.DrawLine(gridLinePen, left, top, left + width, top);
                        e.Graphics.DrawLine(gridLinePen, left, top + height / 2, left + width, top + height / 2);
                        e.Graphics.DrawLine(gridLinePen, left, top + height - 1, left + width, top + height - 1); //Customize the lower horizontal line of the area
                        width1 = 0;
                        e.Graphics.DrawLine(gridLinePen, left - 1, top, left - 1, top + height);
                        for (int i = item.Index; i < item.Span + item.Index; i++)
                        {
                            if (i == 1 || i == 2)
                            {
                                width1 += dgv.Columns[i].Width - 1; //The first column of the separated area
                            }
                            else
                            {
                                width1 += dgv.Columns[i].Width;
                            }
                            e.Graphics.DrawLine(gridLinePen, left + width1, top + height / 2, left + width1, top + height);
                        }
                        SizeF sf = e.Graphics.MeasureString(item.Text, e.CellStyle.Font);
                        float lstr = (width - sf.Width) / 2;
                        float rstr = (height / 2 - sf.Height) / 2;
                        //Draw the text box
                        if (item.Text != "")
                        {
                            e.Graphics.DrawString(item.Text, e.CellStyle.Font,
                                                        new SolidBrush(e.CellStyle.ForeColor),
                                                            left + lstr,
                                                            top + rstr);
                        }
                        width = 0;
                        width1 = 0;
                        for (int i = item.Index; i < item.Span + item.Index; i++)
                        {
                            string columnValue = dgv.Columns[i].HeaderText;
                            width1 = dgv.Columns[i].Width;
                            sf = e.Graphics.MeasureString(columnValue, e.CellStyle.Font);
                            lstr = (width1 - sf.Width) / 2;
                            rstr = (height / 2 - sf.Height) / 2;
                            if (columnValue != "")
                            {
                                e.Graphics.DrawString(columnValue, e.CellStyle.Font,
                                                            new SolidBrush(e.CellStyle.ForeColor),
                                                                left + width + lstr,
                                                                top + height / 2 + rstr);
                            }
                            width += dgv.Columns[i].Width;

                        }
                    }
                    e.Handled = true;
                }
            }
            #endregion
        }


        private void chkStockDetails_CheckedChanged(object sender, EventArgs e)
        {
            frmTargetManagement helper = new frmTargetManagement(this.dgvRandom);

            if (chkStockDetails.Checked == true)
            {
                dgvRandom.Columns[8].Visible = true;
                dgvRandom.Columns[9].Visible = true;                                                   ////******CHANGE*****/////
                dgvRandom.Columns[10].Visible = true;
                helper.Headers.Add(new frmTargetManagement.TopHeader(8, 4, "FG Info."));
                this.dgvRandom.ColumnHeadersHeight = 50;
            }
            else
            {

                dgvRandom.Columns[8].Visible = false;
                dgvRandom.Columns[9].Visible = false;
                dgvRandom.Columns[10].Visible = false;

                helper.Headers.Add(new frmTargetManagement.TopHeader(11, 1, "FG Info."));
                this.dgvRandom.ColumnHeadersHeight = 50;
            }
        }

        private void chkSalesDetails_CheckedChanged(object sender, EventArgs e)
        {
            frmTargetManagement helper = new frmTargetManagement(this.dgvRandom);
            if (chkSalesDetails.Checked == true)
            {
                dgvRandom.Columns[12].Visible = true;
                dgvRandom.Columns[13].Visible = true;
                dgvRandom.Columns[14].Visible = true;
                dgvRandom.Columns[15].Visible = true;                                                        ////******CHANGE*****/////
                dgvRandom.Columns[16].Visible = true;
                dgvRandom.Columns[17].Visible = true;
                helper.Headers.Add(new frmTargetManagement.TopHeader(12, 9, "Sales Info."));
                this.dgvRandom.ColumnHeadersHeight = 50;
            }
            else
            {
                dgvRandom.Columns[12].Visible = false;
                dgvRandom.Columns[13].Visible = false;
                dgvRandom.Columns[14].Visible = false;
                dgvRandom.Columns[15].Visible = false;
                dgvRandom.Columns[16].Visible = false;
                dgvRandom.Columns[17].Visible = false;
                helper.Headers.Add(new frmTargetManagement.TopHeader(19, 1, "Sales Info."));
                this.dgvRandom.ColumnHeadersHeight = 50;
            }
        }

        private void chkOthersInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOthersInfo.Checked == true)
            {
                dgvRandom.Columns[4].Visible = true;                                                           ////******CHANGE*****/////
                dgvRandom.Columns[5].Visible = true;
                dgvRandom.Columns[6].Visible = true;
            }
            else
            {
                dgvRandom.Columns[4].Visible = false;
                dgvRandom.Columns[5].Visible = false;
                dgvRandom.Columns[6].Visible = false;
            }
        }


        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dtpAvgSales_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            DataTable dtable = new DataTable();
            dgvRandom.AutoGenerateColumns = false;
            dgvRandom.DataSource = dtable;
            dataProvider lstData = new dataProvider();
            frmdatedata = dtpAvgSales.Value.Date.ToString("yyyyMMdd");
            if (cmbSector.Text == "All")
            {
                strSQL = "declare @fromdate as varchar(8)" +
                        "set @fromdate = '" + frmdatedata + "' " +
                        "declare @Today as Date = '" + dtpRDPPeriod.Value.ToString("yyyyMMdd") + "'" +
                        "declare @StartOfMonth as Date = DateAdd(day, 1 - Day(@Today), @Today) declare @EndOfMonth as Date = DateAdd(day, -1, DateAdd(month, 1, @StartOfMonth))" +
                         "select t1.ITEMNO as Itemid,t1.ItemName " +
                        ",t1.PACKSIZE as UOM,t1.TradePrice " +
                       ",cast(isNull(tblstock.TDCLStock, 0) as int) as TDCLStock,isnull(tblstock.TongiStock, 0) as TongiStock ,ISNULL(tblstock.RupgonjStock, 0) AS RupgonjStock " +
                     "  ,isNull([01],0) as [01],isnull( [02],0) as [02],isNull([03],0) as [03],isNull([04],0) as [04],isNull([05],0) as [05],isNull([06],0) as [06],isNull([07],0) as [07],isNull([08],0) as [08],isNull([09],0) as [09],isNull([10],0) as [10],isNull([11],0) as [11],isNull([12],0) as [12] " +
                     " from " +
                    " (select * from PRINFOSKF  " +
                  " where status=1 and GPMID Like '%" + userName + "%' and ITEMNO not in (select itemid from ITEM_Production_Planning where  status <> 2 and RDPDate <= @EndOfMonth and RDPDate >= @StartOfMonth ) ) as t1 " +
                "left join " +
                  "(select ITEMNO " +
                 ", SUM(CASE WHEN AUDtORG<> 'SKFDAT' THEN QTYONHAND ELSE 0 END) AS TDCLStock " +
                 ", SUM(CASE WHEN AUDtORG = 'SKFDAT' AND[LOCATION] = 'U303' THEN QTYONHAND ELSE 0 END) AS TongiStock " +
                 " , SUM(CASE WHEN AUDtORG = 'SKFDAT' AND[LOCATION] = 'U514' THEN QTYONHAND ELSE 0 END) AS RupgonjStock " +
                 "from[ICStockStatusCurrentLot] " +
                 "group by itemno)tblstock " +
                  "on(t1.ITEMNO = tblstock.ITEMNO) " +

                   "left join " +
                  " (SELECT* FROM ( " +
                 " SELECT ITEM,  TMonth,QTYSHIPPED  " +
                   " FROM Monthly_Item_Sales " +
               "  WHERE YearMonth >= left(@fromdate,6) " +
                ") AS T1 " +
               " PIVOT( " +
                " SUM(QTYSHIPPED) " +
                     " FOR TMonth in ( " +
                   "[01], [02],[03],[04],[05],[06],[07],[08],[09],[10],[11],[12]" +
                    "   )" +
                  " )AS T2)AS tblSales" +
                   " on(t1.ITEMNO = tblSales.ITEM)";
            }
            else
            {
                strSQL = "declare @fromdate as varchar(8)" +
                            "set @fromdate = '" + frmdatedata + "' " +
                              "declare @Today as Date = '" + dtpRDPPeriod.Value.ToString("yyyyMMdd") + "'" +
                            "declare @StartOfMonth as Date = DateAdd(day, 1 - Day(@Today), @Today) declare @EndOfMonth as Date = DateAdd(day, -1, DateAdd(month, 1, @StartOfMonth))" +

                             "select t1.Itemid,t1.ItemName " +
                            ",t1.UOM,t1.TradePrice,t1.Sector,t1.plant,t1.itemType,t1.productionHead,t1.concernExecutive " +
                           ",isNull(tblstock.TDCLStock, 0) as TDCLStock,isnull(tblstock.TongiStock, 0) as TongiStock ,ISNULL(tblstock.RupgonjStock, 0) AS RupgonjStock " +
                         "  ,isNull([01],0) as [01],isnull( [02],0) as [02],isNull([03],0) as [03],isNull([04],0) as [04],isNull([05],0) as [05],isNull([06],0) as [06],isNull([07],0) as [07],isNull([08],0) as [08],isNull([09],0) as [09],isNull([10],0) as [10],isNull([11],0) as [11],isNull([12],0) as [12] " +
                         " from " +
                        " (select * from itemInfo " +
                      " where concernExecutive Like '%" + userName + "%' and Sector='" + cmbSector.Text + "' and itemid not in (select itemid from ITEM_Production_Planning where RDPDate <= @EndOfMonth and RDPDate >= @StartOfMonth ) ) as t1 " +
                    "left join " +
                   "(select ITEMNO " +
                  ", SUM(CASE WHEN AUDtORG<> 'SKFDAT' THEN QTYONHAND ELSE 0 END) AS TDCLStock " +
                ", SUM(CASE WHEN AUDtORG = 'SKFDAT' AND[LOCATION] = 'U303' THEN QTYONHAND ELSE 0 END) AS TongiStock " +
               " , SUM(CASE WHEN AUDtORG = 'SKFDAT' AND[LOCATION] = 'U514' THEN QTYONHAND ELSE 0 END) AS RupgonjStock " +
               "from[ICStockStatusCurrentLot] " +
               "group by itemno)tblstock " +
               "on(t1.itemId = tblstock.ITEMNO) " +

                "left join " +
                " (SELECT* FROM ( " +
             " SELECT ITEM, SUBSTRING(RTRIM(transdate),5,2)as TMonth,CAST(QTYSHIPPED as INT) as QTYSHIPPED " +
               " FROM OESalesDetails " +
                "  WHERE TRANSDATE >= @fromdate " +
                 ") AS T1 " +
                " PIVOT( " +
                  " SUM(QTYSHIPPED) " +
                         " FOR TMonth in ( " +
                       "[01], [02],[03],[04],[05],[06],[07],[08],[09],[10],[11],[12]" +
                        "   )" +
                      " )AS T2)AS tblSales" +
                       " on(t1.itemId = tblSales.ITEM)";
            }
            // strSQL = "Select * from itemInfo WHERE concernExecutive = '" + master.userName + "'";
            dtable = lstData.getDataTable(strSQL, "itemInfo");

            string s = dtable.Columns[4].DataType.Name;
            dgvRandom.AutoGenerateColumns = false;
            dgvRandom.DataSource = dtable;
            DateTime datetime1 = dtpAvgSales.Value;

            

            DateTime datetimeCurrent = dtpRDPPeriod.Value;

            dgvRandom.Columns[23].HeaderText = datetimeCurrent.ToString("MMM yyyy")+" CM";

            var yr = datetime1.Year;

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DateTime datetime = datetime1.AddMonths(+i);
                var month = datetime.Month;

                if (i < 6)
                {
                    if (month == 01)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Jan " + datetime.Year;                                  ////******CHANGE*****/////
                    }
                    else if (month == 02)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Feb " + datetime.Year;
                    }
                    else if (month == 03)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Mar " + datetime.Year;
                    }
                    else if (month == 04)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Apr " + datetime.Year;
                    }
                    else if (month == 05)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "May " + datetime.Year;
                    }
                    else if (month == 06)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Jun " + datetime.Year;
                    }
                    else if (month == 07)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Jul " + datetime.Year;
                    }
                    else if (month == 08)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Aug " + datetime.Year;
                    }
                    else if (month == 09)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Sep " + datetime.Year;
                    }
                    else if (month == 10)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Oct " + datetime.Year;
                    }
                    else if (month == 11)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Nov " + datetime.Year;
                    }
                    else if (month == 12)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Dec " + datetime.Year;

                    }
                }


                DateTime datetime4 = dtpAvgSales.Value;

                DateTime month1 = datetime4.AddMonths(0);
                DateTime month2 = datetime4.AddMonths(1);
                DateTime month3 = datetime4.AddMonths(2);
                DateTime month4 = datetime4.AddMonths(3);
                DateTime month5 = datetime4.AddMonths(4);
                DateTime month6 = datetime4.AddMonths(5);
                for (int j = 0; j < dgvRandom.Rows.Count; j++)
                {
                    DataRow drow = dtable.Rows[i];
                    if (drow.RowState != DataRowState.Deleted)
                    {

                        if (month1.Month < 10)
                        {
                            dgvRandom.Rows[i].Cells[12].Value = drow["0" + month1.Month + ""].ToString().Trim();                   ////******CHANGEd*****/////

                        }
                        else
                        {
                            dgvRandom.Rows[i].Cells[12].Value = drow["" + month1.Month + ""].ToString().Trim();
                            dgvRandom.Columns[12].DefaultCellStyle.Format = "C1";
                        }
                        if (month2.Month < 10)
                        {
                            dgvRandom.Rows[i].Cells[13].Value = drow["0" + month2.Month + ""].ToString().Trim();
                        }
                        else
                        {
                            dgvRandom.Rows[i].Cells[13].Value = drow["" + month2.Month + ""].ToString().Trim();
                        }
                        if (month3.Month < 10)
                        {
                            dgvRandom.Rows[i].Cells[14].Value = drow["0" + month3.Month + ""].ToString().Trim();
                        }
                        else
                        {
                            dgvRandom.Rows[i].Cells[14].Value = drow["" + month3.Month + ""].ToString().Trim();
                        }
                        if (month4.Month < 10)
                        {
                            dgvRandom.Rows[i].Cells[15].Value = drow["0" + month4.Month + ""].ToString().Trim();
                        }
                        else
                        {
                            dgvRandom.Rows[i].Cells[15].Value = drow["" + month4.Month + ""].ToString().Trim();
                        }
                        if (month5.Month < 10)
                        {
                            dgvRandom.Rows[i].Cells[16].Value = drow["0" + month5.Month + ""].ToString().Trim();
                        }
                        else
                        {
                            dgvRandom.Rows[i].Cells[16].Value = drow["" + month5.Month + ""].ToString().Trim();
                        }
                        if (month6.Month < 10)
                        {
                            dgvRandom.Rows[i].Cells[17].Value = drow["0" + month6.Month + ""].ToString().Trim();
                        }
                        else
                        {
                            dgvRandom.Rows[i].Cells[17].Value = drow["" + month6.Month + ""].ToString().Trim();
                        }
                    }
                }
            }

            // Calculate Average
            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                dgvRandom.Rows[i].Cells[0].Value = i + 1;
                dgvRandom.Rows[i].Cells[10].Value = Convert.ToInt32(dgvRandom.Rows[i].Cells[8].Value) + Convert.ToInt32(dgvRandom.Rows[i].Cells[9].Value);              

                var vol = Convert.ToInt32(dgvRandom.Rows[i].Cells[12].Value) + Convert.ToInt32(dgvRandom.Rows[i].Cells[13].Value) + Convert.ToInt32(dgvRandom.Rows[i].Cells[14].Value) + Convert.ToInt32(dgvRandom.Rows[i].Cells[15].Value) + Convert.ToInt32(dgvRandom.Rows[i].Cells[16].Value) + Convert.ToInt32(dgvRandom.Rows[i].Cells[17].Value);
                dgvRandom.Rows[i].Cells[19].Value = vol / 6;
                //  dgvRando = false;
                // dgvRandom.Columns[12].DefaultCellStyle.Format = "C1";
            }

            DateTime datetime2 = dtpAvgSales.Value;

            DateTime datetime3 = datetime2.AddMonths(+2);
            var montha = datetime3.Month;
            var yr1 = datetime3.Year;

            foreach (DataGridViewRow Myrow in dgvRandom.Rows)
            {
                Myrow.Cells[0].Style.BackColor = Color.LightGray;
                Myrow.Cells[1].Style.BackColor = Color.LightGray;
                Myrow.Cells[2].Style.BackColor = Color.LightGray;
                Myrow.Cells[3].Style.BackColor = Color.LightGray;                                                         ////******CHANGEd*****/////
                Myrow.Cells[4].Style.BackColor = Color.LightGray;
                Myrow.Cells[5].Style.BackColor = Color.LightGray;
                Myrow.Cells[6].Style.BackColor = Color.LightGray;
                Myrow.Cells[7].Style.BackColor = Color.LightGray;
                Myrow.Cells[8].Style.BackColor = Color.LightGray;
                Myrow.Cells[9].Style.BackColor = Color.LightGray;
                Myrow.Cells[10].Style.BackColor = Color.LightGray;
                Myrow.Cells[11].Style.BackColor = Color.LightGray;
                Myrow.Cells[12].Style.BackColor = Color.LightGray;
                Myrow.Cells[13].Style.BackColor = Color.LightGray;
                Myrow.Cells[14].Style.BackColor = Color.LightGray;
                Myrow.Cells[15].Style.BackColor = Color.LightGray;
                Myrow.Cells[16].Style.BackColor = Color.LightGray;
                Myrow.Cells[17].Style.BackColor = Color.LightGray;
                Myrow.Cells[18].Style.BackColor = Color.LightGray;
                Myrow.Cells[19].Style.BackColor = Color.LightGray;
                Myrow.Cells[20].Style.BackColor = Color.LightGray;
                Myrow.Cells[21].Style.BackColor = Color.LightGray;
    //            Myrow.Cells[22].Style.BackColor = Color.LightGray;
                Myrow.Cells[27].Style.BackColor = Color.LightGray;

                Myrow.Cells[28].Style.BackColor = Color.LightGray;
                Myrow.Cells[29].Style.BackColor = Color.LightGray;
                Myrow.Cells[30].Style.BackColor = Color.LightGray;
                // Myrow.Cells[20].Style.BackColor = Color.LightGray;
            }

            lblTotalItem.Text = dtable.Rows.Count.ToString();

            DateTime date = dtpRDPPeriod.Value;
            DateTime date1 = date.AddMonths(-1);
            DataTable dt1 = new DataTable();
            string id = null;
            string sel = "";
            if (cmbSector.Text == "All")
            {
                sel = " select MAX( transNo) as TransNo from ITEM_Production_Planning where Executive='" + lblUserID.Text + "'";

            }
            else
            {
                sel = " select MAX( transNo) as TransNo from ITEM_Production_Planning where Executive='" + lblUserID.Text + "' and sector='" + cmbSector.Text + "' ";

            }
            dt1 = provider.getDataTable(sel, "ITEM_Production_Planning");
            foreach (DataRow row in dt1.Rows)
            {
                if (dt1.Rows.Count != null)
                {
                    transNo = row["TransNo"].ToString();
                    id = transNo;
                }
            }
            if (id != null)
            {
                DataTable dt2 = new DataTable();

                string sel2 = " select COALESCE(RDPMonth2,0) as RDPMonth2,itemID,COALESCE(RDPMonth3,0)as RDPMonth3,RDPDate,COALESCE(ActualQty,0)as ActualQty,COALESCE(RSPMonth2,0)as RSPMonth2,COALESCE(RSPMonth3,0)as RSPMonth3  from ITEM_Production_Planning where Executive='" + lblUserID.Text + "' and  TransNo='" + transNo + "' ";
                dt2 = provider.getDataTable(sel2, "ITEM_Production_Planning");
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    DataRow drow = dt2.Rows[i];
                    if (drow.RowState != DataRowState.Deleted)
                    {
                        string PrevQTY = drow["RDPMonth2"].ToString();
                        string itemid = drow["itemID"].ToString();
                        string rdpMonth3 = drow["RDPMonth3"].ToString();
                        string rspMonth2 = drow["RSPMonth2"].ToString();
                        string rspMonth3 = drow["RSPMonth3"].ToString();

                        string actualQty = drow["ActualQty"].ToString();
                        DateTime month = Convert.ToDateTime(drow["RDPDate"].ToString());
                        DateTime dtMonth = dtpRDPPeriod.Value;
                        //if (month.Month == dtMonth.Month)
                        //{
                        //    for (int j = 0; j < dt2.Rows.Count; j++)
                        //    {
                        //        if (Convert.ToInt32(dgvRandom.Rows[j].Cells[1].Value) == Convert.ToInt32(itemid))
                        //        {
                        //            dgvRandom.Rows[j].Cells[19].Value = actualQty;
                        //            dgvRandom.Rows[j].Cells[20].Value = (Convert.ToInt32(PrevQTY) + Convert.ToInt32(rdpMonth3)) - Convert.ToInt32(dgvRandom.Rows[j].Cells[11].Value);

                        //        }
                        //    }
                        //}
                        //else { 

                        //*******//
                        //for (int j = 0; j < dgvRandom.Rows.Count; j++)
                        //{
                        //    if (Convert.ToInt32(dgvRandom.Rows[j].Cells[1].Value) == Convert.ToInt32(itemid))
                        //    {
                        //        dgvRandom.Rows[j].Cells[21].Value = PrevQTY;
                        //        int vol = (Convert.ToInt32(PrevQTY) + Convert.ToInt32(rdpMonth3)) - Convert.ToInt32(dgvRandom.Rows[j].Cells[11].Value);
                        //        if (vol > 0)
                        //        {
                        //            dgvRandom.Rows[j].Cells[22].Value = vol;
                        //        }
                        //        else
                        //        {
                        //            dgvRandom.Rows[j].Cells[22].Value = 0;                                        ////******CHANGE*****/////
                        //        }

                        //        dgvRandom.Rows[j].Cells[24].Value = rdpMonth3;
                        //        dgvRandom.Rows[j].Cells[29].Value = rspMonth2;
                        //        dgvRandom.Rows[j].Cells[30].Value = rspMonth3;
                        //    }
                        //}
                    }
                }
            }
            datetime1 = dtpRDPPeriod.Value;
            for (int i = 0; i < 3; i++)
            {

                DateTime datetime4 = datetime1.AddMonths(+i);



                //DateTime month1 = datetime4.AddMonths(1);
                //DateTime month2 = datetime4.AddMonths(2);
                //DateTime month3 = datetime4.AddMonths(3);

                //string vol1 = month1.ToString("MMM");
                //string vol2 = month2.ToString("MMM");
                //string vol3 = month3.ToString("MMM");
                if (i == 0)
                {

                    dgvRandom.Columns[28].HeaderText = datetime4.AddMonths(0).ToString("MMM") + " " + datetime4.Year;             ////******CHANGEd*****/////

                }
                else if (i == 1)
                {
                    dgvRandom.Columns[24].HeaderText = datetime4.AddMonths(0).ToString("MMM") + " " + datetime4.Year;
                    dgvRandom.Columns[29].HeaderText = datetime4.AddMonths(0).ToString("MMM") + " " + datetime4.Year;

                }
                else if (i == 2)
                {
                    dgvRandom.Columns[25].HeaderText = datetime4.AddMonths(0).ToString("MMM") + " " + datetime4.Year;
                    dgvRandom.Columns[30].HeaderText = datetime4.AddMonths(0).ToString("MMM") + " " + datetime4.Year;

                }
            }
            // Trend //
            float sale;
            for (int i = 0; i < dgvRandom.Rows.Count; i++)
            {
                todayDate = DateTime.Today.ToString("dd");
                if(todayDate=="01")
                {
                    sale = 02;
                }
                else
                {
                   sale = float.Parse(todayDate) - 1;
                }
                 
                sale = (float.Parse(dgvRandom.Rows[i].Cells[15].Value.ToString()) / sale) * 30;
                dgvRandom.Rows[i].Cells[20].Value = sale.ToString("0");
            }

            //   CalculateSystemQty();
            // System Qty //
            for (int i = 0; i < dgvRandom.Rows.Count; i++)
            {
                avgSales = Int32.Parse(dgvRandom.Rows[i].Cells[19].Value.ToString());
                dgvRandom.Rows[i].Cells[22].Value = Math.Round(avgSales * 1.5);
            }

            //dgvRandom.Columns[11].DefaultCellStyle.Format = "#,##0";
            //AddComma(11);
            //AddComma(12);
            //AddComma(13);
            //AddComma(14);
            //AddComma(15);
            //AddComma(16);
            //AddComma(17);
            //AddComma(18);
            //AddComma(19);
            //AddComma(20);
            //AddComma(21);
            //AddComma(22);
            // AddComma(11);

            ///***///
            //AddComma(23);
            //AddComma(24);
            //AddComma(25);
            if(commaStatus==0)
            {
                //  AddComma(11);
                
                AddComma(12);
                AddComma(13);
                AddComma(14);
                AddComma(15);
                AddComma(16);
                AddComma(17);
                AddComma(18);
                AddComma(19);
                AddComma(20);
               // AddComma(21);
                AddComma(22);
            }
            commaStatus = 1;


        }

        private void AddComma(int v)
        {
            string value;

            for (int i = 0; i < dgvRandom.Rows.Count; i++)
            {
                if (dgvRandom.Rows[i].Cells[v].Value == null || dgvRandom.Rows[i].Cells[v].Value == DBNull.Value || String.IsNullOrWhiteSpace(dgvRandom.Rows[i].Cells[v].Value.ToString()))
                {

                }
                else
                {
                    value = dgvRandom.Rows[i].Cells[v].Value.ToString();
                    value = String.Format("{0:n0}", Int32.Parse(value));
                    dgvRandom.Rows[i].Cells[v].Value = value.ToString();
                }
            
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            //if(btnPost.Enabled = false)
            //{
            //    MessageBox.Show("Posted batch cannot be updated!", "Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            transNo = txtTransNo.Text;
            // entryNo = lblEntryNo.Text;
            entryNo = txtBatchNo.Text;


            /////////******////

            string UOM, itemName;

            int version = Int32.Parse(provider.getResultString("select max([version]) from ITEM_Production_Planning where TransNo = '" + transNo + "' and BatchNo = '" + entryNo + "'"));
            version++;


            DateTime datetime4 = dtpAvgSales.Value;

            DateTime month1 = datetime4.AddMonths(0);
            DateTime month2 = datetime4.AddMonths(1);
            DateTime month3 = datetime4.AddMonths(2);
            DateTime month4 = datetime4.AddMonths(3);
            DateTime month5 = datetime4.AddMonths(4);
            DateTime month6 = datetime4.AddMonths(5);
            string vol1 = month1.ToString("MMM");
            string vol2 = month2.ToString("MMM");
            string vol3 = month3.ToString("MMM");
            string vol4 = month4.ToString("MMM");
            string vol5 = month5.ToString("MMM");
            string vol6 = month6.ToString("MMM");
            for (int i = 0; i < dgvRandom.Rows.Count; i++)
            {
                UOM = dgvRandom.Rows[i].Cells[3].Value.ToString().Replace("'", "''");
                itemName = dgvRandom.Rows[i].Cells[2].Value.ToString().Replace("'", "''");
                string cmd = "insert into ITEM_Production_Planning (id,TransNo	,BatchNo,Executive,production,avgSalesPeriodFrom,avgSalesPeriodTo,RDPDate" +
                    ",ItemID,ItemName,sector,plant,productType,UOM,TP,TongiStock,RupgonjStock,TotalFGStock,TDCLStock," + vol1 + "," + vol2 + "," + vol3 + "," + vol4 + "," + vol5 + "," + vol6 + "" +
                    ",AvgSales, prevQty, SystemQty,ActualQty, RDPMonth2, RDPMonth3, RSPMonth1, RSPMonth2, RSPMonth3, ExpDeliveryDate, Remark, Status, CreatedDate, CreatedBy, stock ,version)" +
                    " values('" + (i + 1) + "','" + transNo + "','"
                                                         + entryNo + "','"
                                                         + lblUserID.Text + "','"
                                                         + dgvRandom.Rows[i].Cells[6].Value + "','"
                                                         + dtpAvgSales.Value.ToString("yyyy-MM-dd") + "','"
                                                         + dtpTo.Value.ToString("yyyy-MM-dd") + "','"
                                                         + dtpRDPPeriod.Value.ToString("yyyy-MM-dd") + "','"
                                                         + dgvRandom.Rows[i].Cells[1].Value + "','"
                                                         + itemName + "','"  //dgvRandom.Rows[i].Cells[2].Value
                                                         + dgvRandom.Rows[i].Cells[4].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[5].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[6].Value + "','"     //dgvRandom.Rows[i].Cells[3].Value
                                                         + UOM + "','"
                                                         + dgvRandom.Rows[i].Cells[7].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[8].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[9].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[10].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[11].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[12].Value.ToString().Replace(",", "") + "','"
                                                         + dgvRandom.Rows[i].Cells[13].Value.ToString().Replace(",", "") + "','"
                                                         + dgvRandom.Rows[i].Cells[14].Value.ToString().Replace(",", "") + "','"
                                                         + dgvRandom.Rows[i].Cells[15].Value.ToString().Replace(",", "") + "','"
                                                         + dgvRandom.Rows[i].Cells[16].Value.ToString().Replace(",", "") + "','"
                                                         + dgvRandom.Rows[i].Cells[17].Value.ToString().Replace(",", "") + "','"                             ////******CHANGE*****/////
                                                         + dgvRandom.Rows[i].Cells[19].Value.ToString().Replace(",", "") + "','"
                                                         + dgvRandom.Rows[i].Cells[21].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[22].Value.ToString().Replace(",", "") + "','"
                                                         + dgvRandom.Rows[i].Cells[23].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[24].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[25].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[28].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[29].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[30].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[31].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[32].Value + "','"
                                                         + 0 + "','"
                                                         + DateTime.Now + "','"
                                                         + lblUserID.Text + "','" + dgvRandom.Rows[i].Cells[28].Value + "'," + version + ")";

                provider.ExecuteCommand(cmd);

            }

            //strSQL = "update Item_Production_Header set transdesc = '"+txtTransDesc.Text+"' , batchtotal = '" + lblGrandTot.Text.Replace(",", "") + "' , RDPDATE = '" + dtpRDPPeriod.Value + "',  where transno = '" + txtTransNo.Text + "' ";

            strSQL = "update Item_Production_Header set transdesc = '" + txtTransDesc.Text + "' , batchtotal = '" + lblGrandTot.Text.Replace(",", "") + "' , RDPDATE = '" + dtpRDPPeriod.Value + "', RDPTotal = '" + lblRdpTot.Text.Replace(",", "") + "' , RSPTotal = '" + lblRspTot.Text.Replace(",", "") + "'  where transno = '" + txtTransNo.Text + "' ";
            provider.ExecuteCommand(strSQL);


            for (int i = 0; i < RSPSL.Rows.Count; i++)
            {
                strSQL = "update RSP_Monthly set batchno = '" + entryNo + "', transno = '" + transNo + "' where sl = '" + RSPSL.Rows[i][0].ToString() + "'";
                provider.ExecuteCommand(strSQL);
            }

            RSPSL.Clear();


            for (int i = 0; i < RSPLoad.Rows.Count; i++)
            {
              strSQL= "insert into RSP_Monthly(BATCHNO, TRANSNO, ITEMID, RDP_MONTH, SAMPLE_ID, SAMPLE_NAME, UOM, UOMF, MKT_UOM, MKT_UOMF, status, Month1, Month2, Month3, AUDTUSER, AUDTDATE, AUDTTIME, LASTMNUSER, LASTMNDATE, Version) values('"+txtBatchNo.Text+"', '"+txtTransNo.Text+"', '"+RSPLoad.Rows[i][3].ToString()+ "', '" + RSPLoad.Rows[i][4].ToString() + "', '" + RSPLoad.Rows[i][5].ToString() + "', '" + RSPLoad.Rows[i][6].ToString().Replace("'","''") + "', '" + RSPLoad.Rows[i][7].ToString() + "', '" + RSPLoad.Rows[i][8].ToString() + "', '" + RSPLoad.Rows[i][9].ToString() + "', '" + RSPLoad.Rows[i][10].ToString() + "', 0, '" + RSPLoad.Rows[i][12].ToString() + "', '" + RSPLoad.Rows[i][14].ToString() + "', '" + RSPLoad.Rows[i][15].ToString() + "', '"+userName+"', "+AUDTDATE+", "+AUDTTIME+", '"+userName+"', "+AUDTDATE+", '"+version+"')";

                provider.ExecuteCommand(strSQL);
            }


            MessageBox.Show("Updated Successfully!");

            cmbVersion.DataSource = null;
            strSQL = "select distinct version from ITEM_Production_Planning where TransNo='" + transNo + "' and BatchNo='" + entryNo + "'";
            DataTable dt2 = new DataTable();
            dt2 = provider.getDataTable(strSQL, "ITEM_Production_Planning");

            
            if (dt2.Rows.Count > 0)
            {
                cmbVersion.DataSource = dt2;
                cmbVersion.DisplayMember = "version";
                cmbVersion.SelectedIndex = dt2.Rows.Count - 1;
            }
            
            string batchTot = provider.getResultString("select Sum(cast(BATCHTOTAL as int)) from Item_Production_Header where BATCHNO = '"+txtBatchNo.Text+"';");
            txtBatchTotal.Text = string.Format("{0:n0}", Int32.Parse(batchTot));
            RSPSL.Clear();
            RSPLoad.Clear();
            LoadRSP();
            LoadGPMItemInfo();
        }

        

        private void txtTransNo_TextChanged(object sender, EventArgs e)            //******* TransNo TextChange   ******//
        {
            if (transLoadStatus == 0)
            {
                int version = 0;
                string ver;


                transNo = txtTransNo.Text;

                if (entryNo == "" || entryNo == null)
                {
                    entryNo = Int32.Parse(lblEntryNo.Text).ToString("D4");
                }


                if (this.transNo != "****NEW****")
                {
                    strSQL = "select BATCHDESC,TRANSDESC,BATCHDATE, BATCHTOTAL  from Item_Production_Header where BATCHNO = '" + entryNo + "' and TRANSNO='" + transNo + "'";
                    DataTable dt3 = new DataTable();
                    dt3 = provider.getDataTable(strSQL, "ITEM_Production_Planning");


                    txtBatchDesc.Text = dt3.Rows[0][0].ToString();
                    txtTransDesc.Text = dt3.Rows[0][1].ToString();
                    //value = String.Format("{0:n0}", Int32.Parse(value));
                    //txtBatchTotal.Text = string.Format("{0:n0}", Int32.Parse(dt3.Rows[0][3].ToString()));
                    dtpBatchDate.Value = new DateTime(Convert.ToInt16(dt3.Rows[0]["BATCHDATE"].ToString().Substring(0, 4)), Convert.ToInt16(dt3.Rows[0]["BATCHDATE"].ToString().Substring(4, 2)), Convert.ToInt16(dt3.Rows[0]["BATCHDATE"].ToString().Substring(6, 2)));

                }

                strSQL = "select distinct version from ITEM_Production_Planning where TransNo='" + transNo + "' and BatchNo='" + entryNo + "'";
                DataTable dt2 = new DataTable();
                dt2 = provider.getDataTable(strSQL, "ITEM_Production_Planning");
                vs = 0;
                if (dt2.Rows.Count > 0)
                {
                    cmbVersion.DataSource = dt2;
                    cmbVersion.DisplayMember = "version";
                }


                ver = provider.getResultString("select max([version]) from ITEM_Production_Planning where TransNo = '" + transNo + "' and BatchNo = '" + entryNo + "'");
                if (ver == null || ver == "")
                {
                    strSQL = "Select * from ITEM_Production_Planning where TransNo='" + transNo + "' and BatchNo='" + entryNo + "'";
                }
                else
                {
                    version = Int32.Parse(ver);
                    strSQL = "Select * from ITEM_Production_Planning where TransNo='" + transNo + "' and BatchNo='" + entryNo + "' and version = " + version + "";
                    //  strSQL = "Select * from ITEM_Production_Planning where TransNo='" + transNo + "' and BatchNo='" + entryNo + "' and version = " + version +;
                }

                DataTable dt = new DataTable();
                dt = provider.getDataTable(strSQL, "ITEM_Production_Planning");
                lblTotalItem.Text = dt.Rows.Count.ToString();


                cmbVersion.Text = version.ToString();

                btnAdd.Visible = false;
                btnLoad.Visible = true;   //////////// Change ! !
                btnSave.Visible = true;
                int status = 0;
                SQL sq = new SQL();
                DataTable dt1 = new DataTable();
                dt1 = sq.get_rs("Select distinct RDPDate,* from ITEM_Production_Planning  where TransNo='" + txtTransNo.Text + "' and BatchNo='" + lblEntryNo.Text + "'");
                foreach (DataRow r in dt1.Rows)
                {
                    cmbSector.SelectedItem = r["sector"].ToString();
                    dtpRDPPeriod.Value = Convert.ToDateTime(r["RDPDate"].ToString());
                    dtpAvgSales.Value = Convert.ToDateTime(r["avgSalesPeriodFrom"].ToString());
                    dtpTo.Value = Convert.ToDateTime(r["avgSalesPeriodTo"].ToString());
                    status = Convert.ToInt32(r["status"].ToString());
                }
                //string month="";
                if (status != 0)
                {
                    btnSave.Visible = false;
                    btnPrint.Visible = false;
                    btnPost.Visible = false;
                }

                if (dgvRandom.DataSource!=null)
                {
                    dgvRandom.DataSource = null;
                }
                else
                {
                    dgvRandom.Rows.Clear();
                }


                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DateTime datetime4 = dtpAvgSales.Value;

                    DateTime month1 = datetime4.AddMonths(0);
                    DateTime month2 = datetime4.AddMonths(1);
                    DateTime month3 = datetime4.AddMonths(2);
                    DateTime month4 = datetime4.AddMonths(3);
                    DateTime month5 = datetime4.AddMonths(4);
                    DateTime month6 = datetime4.AddMonths(5);
                    string vol1 = month1.ToString("MMM");
                    string vol2 = month2.ToString("MMM");
                    string vol3 = month3.ToString("MMM");
                    string vol4 = month4.ToString("MMM");
                    string vol5 = month5.ToString("MMM");
                    string vol6 = month6.ToString("MMM");



                    DataRow drow = dt.Rows[i];
                    if (drow.RowState != DataRowState.Deleted)
                    {
                        dgvRandom.Rows.Add();
                        dgvRandom.Rows[i].Cells[0].Value = i + 1;
                        dgvRandom.Rows[i].Cells[1].Value = drow["ItemID"].ToString().Trim();
                        dgvRandom.Rows[i].Cells[2].Value = drow["ItemName"].ToString().Trim();
                        dgvRandom.Rows[i].Cells[3].Value = drow["UOM"].ToString().Trim();
                        dgvRandom.Rows[i].Cells[4].Value = drow["sector"].ToString().Trim();
                        dgvRandom.Rows[i].Cells[5].Value = drow["plant"].ToString().Trim();                                       ////******CHANGE*****/////
                        dgvRandom.Rows[i].Cells[6].Value = drow["production"].ToString().Trim();
                        dgvRandom.Rows[i].Cells[7].Value = drow["TP"].ToString().Trim();
                        dgvRandom.Rows[i].Cells[8].Value = drow["TongiStock"].ToString().Trim();
                        dgvRandom.Rows[i].Cells[9].Value = drow["RupgonjStock"].ToString().Trim();
                        dgvRandom.Rows[i].Cells[10].Value = drow["TotalFGStock"].ToString().Trim();
                        dgvRandom.Rows[i].Cells[11].Value = drow["TDCLStock"].ToString().Trim();
                        dgvRandom.Rows[i].Cells[12].Value = drow["" + vol1 + ""].ToString().Trim();
                        dgvRandom.Rows[i].Cells[13].Value = drow["" + vol2 + ""].ToString().Trim();
                        dgvRandom.Rows[i].Cells[14].Value = drow["" + vol3 + ""].ToString().Trim();
                        dgvRandom.Rows[i].Cells[15].Value = drow["" + vol4 + ""].ToString().Trim();
                        dgvRandom.Rows[i].Cells[16].Value = drow["" + vol5 + ""].ToString().Trim();
                        dgvRandom.Rows[i].Cells[17].Value = drow["" + vol6 + ""].ToString().Trim();
                        dgvRandom.Rows[i].Cells[19].Value = drow["AvgSales"].ToString().Trim();
                        dgvRandom.Rows[i].Cells[20].Value = drow["Trend"].ToString().Trim();
                        dgvRandom.Rows[i].Cells[21].Value = drow["prevQty"].ToString().Trim();
                        dgvRandom.Rows[i].Cells[22].Value = drow["SystemQty"].ToString().Trim();
                        dgvRandom.Rows[i].Cells[23].Value = drow["ActualQty"].ToString().Trim();
                        dgvRandom.Rows[i].Cells[24].Value = drow["RDPMonth2"].ToString().Trim();
                        dgvRandom.Rows[i].Cells[25].Value = drow["RDPMonth3"].ToString().Trim();
                        dgvRandom.Rows[i].Cells[27].Value = drow["Stock"].ToString().Trim();
                        dgvRandom.Rows[i].Cells[28].Value = drow["RSPMonth1"].ToString().Trim();
                        dgvRandom.Rows[i].Cells[29].Value = drow["RSPMonth2"].ToString().Trim();

                        dgvRandom.Rows[i].Cells[30].Value = drow["RSPMonth3"].ToString().Trim();
                        dgvRandom.Rows[i].Cells[31].Value = drow["ExpDeliveryDate"].ToString().Trim();
                        dgvRandom.Rows[i].Cells[32].Value = drow["Remark"].ToString().Trim();
                        // dgvRandom.Rows[i].Cells[30].Value = i + 1;
                    }

                }

                if (commaStatus == 0)
                {
                  //  AddComma(11);
                    AddComma(12);
                    AddComma(13);
                    AddComma(14);
                    AddComma(15);
                    AddComma(16);
                    AddComma(17);
                    AddComma(18);
                    AddComma(19);
                    AddComma(20);
                 //   AddComma(21);
                    AddComma(22);
                }

                DateTime datetimeCurrent = dtpRDPPeriod.Value;

                dgvRandom.Columns[23].HeaderText = datetimeCurrent.ToString("MMM yyyy")+" CM";

                for (int i = 0; i < 6; i++)
                {
                    if (i < 6)
                    {
                        DateTime datetime1 = dtpAvgSales.Value;
                        DateTime datetime = datetime1.AddMonths(+i);
                        var month = datetime.Month;
                        var yr = datetime.Year;
                        if (month == 01)
                        {
                            dgvRandom.Columns[12 + i].HeaderText = "Jan " + datetime.Year;                                 ////******CHANGE*****/////

                        }
                        else if (month == 02)
                        {
                            dgvRandom.Columns[12 + i].HeaderText = "Feb " + datetime.Year;

                        }
                        else if (month == 03)
                        {
                            dgvRandom.Columns[12 + i].HeaderText = "Mar " + datetime.Year;

                        }
                        else if (month == 04)
                        {
                            dgvRandom.Columns[12 + i].HeaderText = "Apr " + datetime.Year;

                        }
                        else if (month == 05)
                        {
                            dgvRandom.Columns[12 + i].HeaderText = "May " + datetime.Year;

                        }
                        else if (month == 06)
                        {
                            dgvRandom.Columns[12 + i].HeaderText = "Jun " + datetime.Year;

                        }
                        else if (month == 07)
                        {
                            dgvRandom.Columns[12 + i].HeaderText = "Jul " + datetime.Year;

                        }
                        else if (month == 08)
                        {
                            dgvRandom.Columns[12 + i].HeaderText = "Aug " + datetime.Year;

                        }
                        else if (month == 09)
                        {
                            dgvRandom.Columns[12 + i].HeaderText = "Sep " + datetime.Year;

                        }
                        else if (month == 10)
                        {
                            dgvRandom.Columns[12 + i].HeaderText = "Oct " + datetime.Year;

                        }
                        else if (month == 11)
                        {
                            dgvRandom.Columns[12 + i].HeaderText = "Nov " + datetime.Year;

                        }
                        else if (month == 12)
                        {
                            dgvRandom.Columns[12 + i].HeaderText = "Dec " + datetime.Year;
                        }
                    }

                }

                DateTime datetime11 = dtpRDPPeriod.Value;
                for (int i = 0; i < 3; i++)
                {

                    DateTime datetime4 = datetime11.AddMonths(+i);

                    if (i == 0)
                    {

                        dgvRandom.Columns[28].HeaderText = datetime4.AddMonths(0).ToString("MMM") + " " + datetime4.Year;                        ////******CHANGE*****/////

                    }
                    else if (i == 1)
                    {
                        dgvRandom.Columns[24].HeaderText = datetime4.AddMonths(0).ToString("MMM") + " " + datetime4.Year;
                        dgvRandom.Columns[29].HeaderText = datetime4.AddMonths(0).ToString("MMM") + " " + datetime4.Year;

                    }
                    else if (i == 2)
                    {
                        dgvRandom.Columns[25].HeaderText = datetime4.AddMonths(0).ToString("MMM") + " " + datetime4.Year;
                        dgvRandom.Columns[30].HeaderText = datetime4.AddMonths(0).ToString("MMM") + " " + datetime4.Year;
                    }

                    //string batchTot = provider.getResultString("select Sum(cast(BATCHTOTAL as int)) from Item_Production_Header where BATCHNO = '" + txtBatchNo.Text + "';");
                    //txtBatchTotal.Text = string.Format("{0:n0}", Int32.Parse(batchTot));

                }
                vs = 1;
                GrayColumn();
            }
            
        }

        private void BatchStatusButton()
        {
            btnAdd.Enabled = false;
            btnSave.Enabled = false;
            //btnPost.Enabled = false;
        }

        private void cmbRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime date = dtpRDPPeriod.Value;
            DateTime dateTime1, datetime;
            


            if(cmbRange.SelectedIndex==0)
            {
                datetime = date.AddMonths(-3);
                var month = datetime.Month;
                dtpAvgSales.Value = datetime;

                 dateTime1 = date.AddMonths(-1);
                dtpTo.Value = dateTime1;
            }
            if (cmbRange.SelectedIndex == 1)
            {
                datetime = date.AddMonths(-6);
                var month = datetime.Month;
                dtpAvgSales.Value = datetime;

                dateTime1 = date.AddMonths(-1);
                dtpTo.Value = dateTime1;
            }
            if (cmbRange.SelectedIndex == 2)
            {
                dtpAvgSales.Enabled = true;
                dtpTo.Enabled = true;
            }
        }

        private void dtpRDPPeriod_ValueChanged(object sender, EventArgs e)
        { 
            DateTime date = dtpRDPPeriod.Value;
            DateTime datetime = date.AddMonths(-3);
            // var month = datetime.Month;
            dtpAvgSales.Value = datetime;

            DateTime dateTime1 = date.AddMonths(-1);
            dtpTo.Value = dateTime1;

        }

        private void btnAdd_Click(object sender, EventArgs e)                    // Add button
        {

            if(txtBatchDesc.Text == "")
            {
                MessageBox.Show("Please enter batch description");
                this.ActiveControl = txtBatchDesc;
                return;
            }
            if(dgvRandom.Rows.Count<1)
            {
                MessageBox.Show("Please load items before add");
                return;
            }

            string UOM, itemName;
            DataTable dt1 = new DataTable();
            string sel = "SELECT MAX(TransNo) as TransNo from ITEM_Production_Planning ";
            dt1 = provider.getDataTable(sel, "ITEM_Production_Planning");

            foreach (DataRow row in dt1.Rows)
            {
                transNo = row["TransNo"].ToString();

                if (transNo == null || transNo == "")
                {
                    transNo = "10000001";
                }
                else
                {
                    int ID = Convert.ToInt32(transNo) + Convert.ToInt32(1);
                    transNo = ID.ToString("D8");
                }
            }

            if (txtBatchNo.Text == "****NEW****")
            {
                int check = CheckBatch();
                if (check == 0)
                {
                    DataTable dt2 = new DataTable();
                    string sel1 = "SELECT MAX(BatchNo) as BatchNo from ITEM_Production_Planning ";
                    dt2 = provider.getDataTable(sel1, "ITEM_Production_Planning");

                    foreach (DataRow row1 in dt2.Rows)
                    {
                        entryNo = row1["BatchNo"].ToString();

                        if (entryNo == null || entryNo == "")
                        {
                            entryNo = "0001";
                        }
                        else
                        {
                            int ID = Convert.ToInt32(entryNo) + Convert.ToInt32(1);
                            entryNo = ID.ToString("D4");
                        }
                    }
                }
                else
                {
                    entryNo = check.ToString();
                }
            }
            else
            {
                entryNo = txtBatchNo.Text;
            }



            DateTime datetime4 = dtpAvgSales.Value;

            DateTime month1 = datetime4.AddMonths(0);
            DateTime month2 = datetime4.AddMonths(1);
            DateTime month3 = datetime4.AddMonths(2);
            DateTime month4 = datetime4.AddMonths(3);
            DateTime month5 = datetime4.AddMonths(4);
            DateTime month6 = datetime4.AddMonths(5);
            string vol1 = month1.ToString("MMM");
            string vol2 = month2.ToString("MMM");
            string vol3 = month3.ToString("MMM");
            string vol4 = month4.ToString("MMM");
            string vol5 = month5.ToString("MMM");
            string vol6 = month6.ToString("MMM");


            //strSQL = "insert into ITEM_Production_Header(RDPPERIOD,BATCHNO,TRANSNO,BATCHDESC,TRANSDESC,BATCHDATE,TRANSDATE,STATUS,BATCHTOTAL,AUDTDATE,AUDTTIME,AUDTUSER,LASTMNDATE," +
            //    "LASTMNUSER,RDPDATE,Executive) VALUES('" + dtpRDPPeriod.Text + "', '" + entryNo + "','" + transNo + "' ,'" + txtBatchDesc.Text + "', '" + txtTransDesc.Text + "', " + dtpBatchDate.Value.ToString("yyyyMMdd") + ", " + dtpTransDate.Value.ToString("yyyyMMdd") + "," +
            //    " 0,'" + (lblGrandTot.Text).Replace(",", "") + "' ," + AUDTDATE + ", " + AUDTTIME + ", '" + userName + "', " + AUDTTIME + ", '" + userName + "','" + dtpRDPPeriod.Value + "','"+lblUserID.Text+"')";


            strSQL = "insert into Item_Production_Header (RDPPERIOD, BATCHNO, TRANSNO, BATCHDESC, TRANSDESC, BATCHDATE, TRANSDATE, STATUS, BATCHTOTAL, AUDTDATE, AUDTTIME, AUDTUSER, LASTMNDATE, LASTMNUSER, RDPDATE, Executive, RDPTotal, RSPTotal) values('" + dtpRDPPeriod.Text + "', '" + entryNo + "', '" + transNo + "', '" + txtBatchDesc.Text + "', '" + txtTransDesc.Text + "', "+ dtpBatchDate.Value.ToString("yyyyMMdd") +", " + dtpTransDate.Value.ToString("yyyyMMdd") + ","
                + " 0, '" + (lblGrandTot.Text).Replace(",", "") + "', '"+AUDTDATE+"', '"+AUDTTIME+"', '"+userName+"', '"+AUDTDATE+"', '"+userName+ "', '" + dtpRDPPeriod.Value + "', '" + lblUserID.Text + "', '"+lblRdpTot.Text.Replace(",","")+"', '"+lblRspTot.Text.Replace(",","")+"')";



            provider.ExecuteCommand(strSQL);


            for (int i = 0; i < dgvRandom.Rows.Count; i++)
            {
                UOM = dgvRandom.Rows[i].Cells[3].Value.ToString().Replace("'", "''");
                itemName = dgvRandom.Rows[i].Cells[2].Value.ToString().Replace("'", "''");
                string cmd = "insert into ITEM_Production_Planning (id,TransNo	,BatchNo,Executive,production,avgSalesPeriodFrom,avgSalesPeriodTo,RDPDate" +
                    ",ItemID,ItemName,sector,plant,productType,UOM,TP,TongiStock,RupgonjStock,TotalFGStock,TDCLStock," + vol1 + "," + vol2 + "," + vol3 + "," + vol4 + "," + vol5 + "," + vol6 + ", AvgSales, prevQty, SystemQty,ActualQty, RDPMonth2, RDPMonth3, RSPMonth1,RSPMonth2, RSPMonth3, ExpDeliveryDate, Remark, Status, CreatedDate, CreatedBy, stock , version, trend)" +
                    " values('" + i + 1 + "','" + transNo + "','"
                                                         + entryNo + "','"
                                                         + lblUserID.Text + "','"
                                                         + dgvRandom.Rows[i].Cells[6].Value + "','"
                                                         + dtpAvgSales.Value.ToString("yyyy-MM-dd") + "','"
                                                         + dtpTo.Value.ToString("yyyy-MM-dd") + "','"
                                                         + dtpRDPPeriod.Value.ToString("yyyy-MM-dd") + "','"
                                                         + dgvRandom.Rows[i].Cells[1].Value + "','"
                                                         + itemName + "','"  //dgvRandom.Rows[i].Cells[2].Value
                                                         + dgvRandom.Rows[i].Cells[4].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[5].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[6].Value + "','"     //dgvRandom.Rows[i].Cells[3].Value
                                                         + UOM + "','"
                                                         + dgvRandom.Rows[i].Cells[7].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[8].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[9].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[10].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[11].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[12].Value.ToString().Replace(",", "") + "','"
                                                         + dgvRandom.Rows[i].Cells[13].Value.ToString().Replace(",", "") + "','"
                                                         + dgvRandom.Rows[i].Cells[14].Value.ToString().Replace(",", "") + "','"
                                                         + dgvRandom.Rows[i].Cells[15].Value.ToString().Replace(",", "") + "','"
                                                         + dgvRandom.Rows[i].Cells[16].Value.ToString().Replace(",", "") + "','"
                                                         + dgvRandom.Rows[i].Cells[17].Value.ToString().Replace(",", "") + "','"                             ////******CHANGE*****/////
                                                         + dgvRandom.Rows[i].Cells[19].Value.ToString().Replace(",", "") + "','"
                                                         + dgvRandom.Rows[i].Cells[21].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[22].Value.ToString().Replace(",", "") + "','"
                                                         + dgvRandom.Rows[i].Cells[23].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[24].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[25].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[28].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[29].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[30].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[31].Value + "','"
                                                         + dgvRandom.Rows[i].Cells[32].Value + "','"
                                                         + 0 + "','"
                                                         + DateTime.Now + "','"
                                                         + userName + "','" + dgvRandom.Rows[i].Cells[27].Value + "',1,'" + dgvRandom.Rows[i].Cells[20].Value.ToString().Replace(",", "") + "')";

                provider.ExecuteCommand(cmd);

            }

            for(int i=0; i<RSPSL.Rows.Count; i++)
            {
                strSQL = "update RSP_Monthly set batchno = '"+ entryNo + "', transno = '"+transNo+"' where sl = '"+ RSPSL.Rows[i][0].ToString() + "'";
                provider.ExecuteCommand(strSQL);
            }

            RSPSL.Clear();
            RSPLoad.Clear();
            LoadRSP();
            
            MessageBox.Show("Added Successfully!");

            transLoadStatus = 0;
            txtTransNo.Text = transNo;

            
            if (txtBatchNo.Text == "****NEW****")
            {
                txtBatchNo.Text = entryNo;
            }
            AfterAdd();

            batchTotal = provider.getResultString("select sum(cast(batchtotal as int)) from item_production_header where BATCHNO = '" + txtBatchNo.Text + "'");
            txtBatchTotal.Text= String.Format("{0:n0}", Int32.Parse(batchTotal));            

            LoadGPMItemInfo();
        }
        

        public int CheckBatch()
        {

            SqlConnection conn = new SqlConnection("Data Source=.;Integrated Security=false;Initial Catalog=RDPLOCAL;user=sa;pwd=erp");
            // string applyId = Request.Form["txtApplyId"];

            string Query = "declare @Today as Date = '" + dtpRDPPeriod.Value.ToString("yyyy-MM-dd") + "' declare @StartOfMonth as Date = DateAdd(day, 1 - Day(@Today), @Today) declare @EndOfMonth as Date = DateAdd(day, -1, DateAdd(month, 1, @StartOfMonth)) select distinct BatchNo from ITEM_Production_Planning where RDPDate <= @EndOfMonth and RDPDate >= @StartOfMonth";
            conn.Open();
            SqlCommand Command = new SqlCommand(Query, conn);
            SqlDataReader reader;
            reader = Command.ExecuteReader();
            while (reader.Read())
            {
                int vol = Convert.ToInt32(reader["BatchNo"].ToString());
                return vol;
            }
            conn.Close();

            return 0;
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            dgvRandom.Rows.Clear();

            dtpRDPPeriod.Value = DateTime.Now;
            btnAdd.Visible = true;
            btnSave.Visible = false;
            btnLoad.Visible = true;
            cmbSector.SelectedIndex = 0;
            cmbVersion.SelectedIndex = 0;
            lblTotalItem.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            provider.ExportToExcel(txtTransNo.Text,Int32.Parse(cmbVersion.Text), userName,txtBatchNo.Text );
                        

            // ExportToexcel();
        }
        
        private void btnPost_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            try
            {
                DialogResult dialogresult = MessageBox.Show("Do you want to post ?", "Post", MessageBoxButtons.YesNo);

                if (dialogresult == DialogResult.Yes)
                {
                    strSQL = "Update ITEM_Production_Header set Status='1' where BatchNo='" + txtBatchNo.Text + "'";
                    provider.ExecuteCommand(strSQL);


                    strSQL = "Update ITEM_Production_Planning set Status='1' where BatchNo='" + txtBatchNo.Text + "'";
                    provider.ExecuteCommand(strSQL);



                    //string logUser = "Update for  " + txtTransNo.Text;
                    //strSQL = "INSERT INTO LogDetails (CreateBy,Description,CreateDateTime,Action) VALUES ('" + lblUserID.Text + "', '" + logUser + "' ,'" + DateTime.Now + "', 'Update')";
                    //provider.ExecuteCommand(strSQL);

                    MessageBox.Show("Posted Successfully");
                    btnAdd.Visible = false;
                    btnAdd.Enabled = false;
                    btnSave.Enabled = false;
                    btnPost.Enabled = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                    
        }

        public void Clear()
        {
            txtTransNo.Text = "";
            lblEntryNo.Text = "";
            lblUserID.Text = "";
            lblTotalItem.Text = "0";
            cmbSector.SelectedIndex = 0;
            cmbVersion.SelectedIndex = 0;
            dtpRDPPeriod.Value = DateTime.Today;
        }

        private void cmbVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (vs == 1)
            {
                commaStatus = 0;
                VersionLoad(cmbVersion.SelectedIndex);
                commaStatus = 1;
                GrayColumn();
            }

            //transLoadStatus = 1;
            //string trNo = txtTransNo.Text;
            //txtTransNo.Text = "";
            //transLoadStatus = 0;
            //txtTransNo.Text= trNo;
            //GrayColumn();


        }

        private void VersionLoad(int version)
        {
            version++;
            dgvRandom.Rows.Clear();

            strSQL = "Select * from ITEM_Production_Planning where TransNo='" + transNo + "' and BatchNo='" + entryNo + "' and version = " + version + "";

            DataTable dt = new DataTable();
            dt = provider.getDataTable(strSQL, "ITEM_Production_Planning");

            int status = 0;
            SQL sq = new SQL();
            DataTable dt1 = new DataTable();
            dt1 = sq.get_rs("Select distinct RDPDate,* from ITEM_Production_Planning  where TransNo='" + txtTransNo.Text + "' and BatchNo='" + lblEntryNo.Text + "'");
            foreach (DataRow r in dt1.Rows)
            {
                cmbSector.SelectedItem = r["sector"].ToString();
                dtpRDPPeriod.Value = Convert.ToDateTime(r["RDPDate"].ToString());
                dtpAvgSales.Value = Convert.ToDateTime(r["avgSalesPeriodFrom"].ToString());
                dtpTo.Value = Convert.ToDateTime(r["avgSalesPeriodTo"].ToString());
                status = Convert.ToInt32(r["status"].ToString());

            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                DateTime datetime4 = dtpAvgSales.Value;

                DateTime month1 = datetime4.AddMonths(0);
                DateTime month2 = datetime4.AddMonths(1);
                DateTime month3 = datetime4.AddMonths(2);
                DateTime month4 = datetime4.AddMonths(3);
                DateTime month5 = datetime4.AddMonths(4);
                DateTime month6 = datetime4.AddMonths(5);
                string vol1 = month1.ToString("MMM");
                string vol2 = month2.ToString("MMM");
                string vol3 = month3.ToString("MMM");
                string vol4 = month4.ToString("MMM");
                string vol5 = month5.ToString("MMM");
                string vol6 = month6.ToString("MMM");


                DataRow drow = dt.Rows[i];
                if (drow.RowState != DataRowState.Deleted)
                {
                    dgvRandom.Rows.Add();
                    dgvRandom.Rows[i].Cells[0].Value = i + 1;
                    dgvRandom.Rows[i].Cells[1].Value = drow["ItemID"].ToString().Trim();
                    dgvRandom.Rows[i].Cells[2].Value = drow["ItemName"].ToString().Trim();
                    dgvRandom.Rows[i].Cells[3].Value = drow["UOM"].ToString().Trim();
                    dgvRandom.Rows[i].Cells[4].Value = drow["sector"].ToString().Trim();
                    dgvRandom.Rows[i].Cells[5].Value = drow["plant"].ToString().Trim();                                       ////******CHANGE*****/////
                    dgvRandom.Rows[i].Cells[6].Value = drow["production"].ToString().Trim();
                    dgvRandom.Rows[i].Cells[7].Value = drow["TP"].ToString().Trim();
                    dgvRandom.Rows[i].Cells[8].Value = drow["TongiStock"].ToString().Trim();
                    dgvRandom.Rows[i].Cells[9].Value = drow["RupgonjStock"].ToString().Trim();
                    dgvRandom.Rows[i].Cells[10].Value = drow["TotalFGStock"].ToString().Trim();
                    dgvRandom.Rows[i].Cells[11].Value = drow["TDCLStock"].ToString().Trim();
                    dgvRandom.Rows[i].Cells[12].Value = drow["" + vol1 + ""].ToString().Trim();
                    dgvRandom.Rows[i].Cells[13].Value = drow["" + vol2 + ""].ToString().Trim();
                    dgvRandom.Rows[i].Cells[14].Value = drow["" + vol3 + ""].ToString().Trim();
                    dgvRandom.Rows[i].Cells[15].Value = drow["" + vol4 + ""].ToString().Trim();
                    dgvRandom.Rows[i].Cells[16].Value = drow["" + vol5 + ""].ToString().Trim();
                    dgvRandom.Rows[i].Cells[17].Value = drow["" + vol6 + ""].ToString().Trim();
                    dgvRandom.Rows[i].Cells[19].Value = drow["AvgSales"].ToString().Trim();
                    dgvRandom.Rows[i].Cells[21].Value = drow["prevQty"].ToString().Trim();
                    dgvRandom.Rows[i].Cells[22].Value = drow["SystemQty"].ToString().Trim();
                    dgvRandom.Rows[i].Cells[23].Value = drow["ActualQty"].ToString().Trim();
                    //  dgvRandom.Rows[i].Cells[22].Value = drow["RDPMonth1"].ToString().Trim();
                    dgvRandom.Rows[i].Cells[24].Value = drow["RDPMonth2"].ToString().Trim();
                    dgvRandom.Rows[i].Cells[25].Value = drow["RDPMonth3"].ToString().Trim();
                    dgvRandom.Rows[i].Cells[27].Value = drow["Stock"].ToString().Trim();
                    dgvRandom.Rows[i].Cells[28].Value = drow["RSPMonth1"].ToString().Trim();
                    dgvRandom.Rows[i].Cells[29].Value = drow["RSPMonth2"].ToString().Trim();
                    dgvRandom.Rows[i].Cells[30].Value = drow["RSPMonth3"].ToString().Trim();
                    dgvRandom.Rows[i].Cells[31].Value = drow["ExpDeliveryDate"].ToString().Trim();
                    dgvRandom.Rows[i].Cells[32].Value = drow["Remark"].ToString().Trim();
                    // dgvRandom.Rows[i].Cells[30].Value = i + 1;
                }
            }

            if(commaStatus==0)
            {
                
                AddComma(12);
                AddComma(13);
                AddComma(14);
                AddComma(15);
                AddComma(16);
                AddComma(17);
                AddComma(18);
                AddComma(19);
                AddComma(20);
              //  AddComma(21);
                AddComma(22);
            }
            


            for (int i = 0; i < 6; i++)
            {
                if (i < 6)
                {
                    DateTime datetime1 = dtpAvgSales.Value;
                    DateTime datetime = datetime1.AddMonths(+i);
                    var month = datetime.Month;
                    var yr = datetime.Year;
                    if (month == 01)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Jan " + datetime.Year;                                 ////******CHANGE*****/////

                    }
                    else if (month == 02)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Feb " + datetime.Year;

                    }
                    else if (month == 03)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Mar " + datetime.Year;

                    }
                    else if (month == 04)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Apr " + datetime.Year;

                    }
                    else if (month == 05)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "May " + datetime.Year;

                    }
                    else if (month == 06)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Jun " + datetime.Year;

                    }
                    else if (month == 07)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Jul " + datetime.Year;

                    }
                    else if (month == 08)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Aug " + datetime.Year;

                    }
                    else if (month == 09)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Sep " + datetime.Year;

                    }
                    else if (month == 10)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Oct " + datetime.Year;

                    }
                    else if (month == 11)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Nov " + datetime.Year;

                    }
                    else if (month == 12)
                    {
                        dgvRandom.Columns[12 + i].HeaderText = "Dec " + datetime.Year;

                    }
                }

            }

            DateTime datetime11 = dtpRDPPeriod.Value;
            for (int i = 0; i < 3; i++)
            {

                DateTime datetime4 = datetime11.AddMonths(+i);

                if (i == 0)
                {

                    dgvRandom.Columns[28].HeaderText = datetime4.AddMonths(0).ToString("MMM") + " " + datetime4.Year;                        ////******CHANGE*****/////

                }
                else if (i == 1)
                {
                    dgvRandom.Columns[24].HeaderText = datetime4.AddMonths(0).ToString("MMM") + " " + datetime4.Year;
                    dgvRandom.Columns[29].HeaderText = datetime4.AddMonths(0).ToString("MMM") + " " + datetime4.Year;

                }
                else if (i == 2)
                {
                    dgvRandom.Columns[25].HeaderText = datetime4.AddMonths(0).ToString("MMM") + " " + datetime4.Year;
                    dgvRandom.Columns[30].HeaderText = datetime4.AddMonths(0).ToString("MMM") + " " + datetime4.Year;

                }
            }           

            vs = 1;


            //CalculateTotal();            

        }
        private void dgvRandom_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 28 || e.ColumnIndex == 23)
                {
                    CalculateTotal();
                }

            }

            
        }
      

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (RSPSL.Rows.Count > 0)
            {
                DialogResult dialogresult = MessageBox.Show("Are you sure to close? You haven't added any entry", "Warning",MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogresult == DialogResult.Yes)
                {
                    for (int i = 0; i < RSPSL.Rows.Count; i++)
                    {
                        strSQL = "delete from RSP_Monthly where sl = '" + RSPSL.Rows[i][0] + "'";
                        provider.ExecuteCommand(strSQL);
                    }
                }
                else
                {
                    this.Close();
                }
            }            
        }

        private void btnNextEntry_Click(object sender, EventArgs e)              // Next Entry
        {
            strSQL = "select top 1 transno,executive from Item_Production_Header where TRANSNO>'" + txtTransNo.Text + "' and batchno= '" + txtBatchNo.Text + "' order by TRANSNO asc";
            load = provider.getDataTable(strSQL, "Item_Production_Header");
            if (userName=="admin" && load.Rows.Count > 0 )
            {
                lblUserID.Text = load.Rows[0][1].ToString();
                commaStatus = 0;
                txtTransNo.Text = load.Rows[0][0].ToString();
                commaStatus = 1;
            }

        }

        private void btnPreviousEntry_Click(object sender, EventArgs e)          // Previous Entry
        {
            strSQL = "select top 1 transno, executive from Item_Production_Header where TRANSNO<'" + txtTransNo.Text + "' and batchno= '" + txtBatchNo.Text + "' order by TRANSNO desc";
            load = provider.getDataTable(strSQL, "Item_Production_Header");
            if (userName == "admin" && load.Rows.Count > 0)
            {
                lblUserID.Text = load.Rows[0][1].ToString();
                commaStatus = 0;
                txtTransNo.Text = load.Rows[0][0].ToString();
                commaStatus = 1;
            }
        }
        

        private void btnLastEntry_Click(object sender, EventArgs e)              // Last Entry
        {
            strSQL = "select top 1 transno, executive from Item_Production_Header where batchno= '" + txtBatchNo.Text + "' order by TRANSNO desc";
            load = provider.getDataTable(strSQL, "Item_Production_Header");
            if (userName == "admin" && load.Rows.Count > 0)
            {
                lblUserID.Text = load.Rows[0][1].ToString();
                commaStatus = 0;
                txtTransNo.Text = load.Rows[0][0].ToString();
                commaStatus = 1;
            }
        }

        private void btnFirstEntry_Click(object sender, EventArgs e)             // First Entry
        {
            strSQL = "select top 1 transno, executive from Item_Production_Header where batchno= '" + txtBatchNo.Text + "' order by TRANSNO asc";
            load = provider.getDataTable(strSQL, "Item_Production_Header");
            if (userName == "admin" && load.Rows.Count > 0)
            {
                lblUserID.Text = load.Rows[0][1].ToString();
                commaStatus = 0;
                txtTransNo.Text = load.Rows[0][0].ToString();
                commaStatus = 1;
            }
        }

        private void btnNewEntryNo_Click(object sender, EventArgs e)
        {
            dgvRandom.Rows.Clear();

            btnSave.Enabled = false;
            btnSave.Visible = false;
            btnAdd.Enabled = true;
            btnAdd.Visible = true;

            lblRspTot.Text = "0";
            lblRdpTot.Text = "0";
            lblGrandTot.Text = "0";

            txtTransDesc.Text = "";

            transLoadStatus = 1;
            txtTransNo.Text="";
            transLoadStatus = 0;
            cmbVersion.SelectedIndex = -1;

            string N = provider.getResultString("select userfullname from umuser where userid = '"+userName+"'");
            txtUser.Text = N;
            lblUserID.Text = userName+ " - "+N;

        }

       



        // RSP Button
        private void dgvRandom_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (dgvRandom.Columns[e.ColumnIndex].Name == "RSP")
            {
                frmRSPCalculation rsp = new frmRSPCalculation();
                rsp.month1 = dgvRandom.Columns[28].HeaderText.ToString();
                rsp.month2 = dgvRandom.Columns[29].HeaderText.ToString();
                rsp.month3 = dgvRandom.Columns[30].HeaderText.ToString();
                rsp.itemID = dgvRandom.Rows[e.RowIndex].Cells[1].Value.ToString();                                
                rsp.rdp_Period = dtpRDPPeriod.Text;
                rsp.username = userName;
               
                
                
                if (cmbVersion.Text == "Select Option"|| cmbVersion.Text=="")
                {
                    rsp.version = 1;
                }
                else
                {
                    rsp.version = Int32.Parse( cmbVersion.Text);
                }
                rsp.ShowDialog();

                if(rsp.m1tot!=0)
                {
                   dgvRandom.Rows[e.RowIndex].Cells[28].Value = rsp.m1tot;
                }
                if (rsp.m2tot != 0)
                {
                    dgvRandom.Rows[e.RowIndex].Cells[29].Value = rsp.m2tot;
                }
                if (rsp.m3tot != 0)
                {
                    dgvRandom.Rows[e.RowIndex].Cells[30].Value = rsp.m3tot;
                }
                if (rsp.stock != 0)
                {
                    dgvRandom.Rows[e.RowIndex].Cells[27].Value = rsp.stock;
                }

                if(rsp.ItemRSP!=null)
                {
                    if (rsp.ItemRSP.Rows.Count > 0)
                    {
                        RSPSL.Merge(rsp.ItemRSP);
                    }
                }
                
               

                rsp.Close();
            }         
        }

        private void btnNextBatch_Click(object sender, EventArgs e)
        {
            strSQL = "select top 1 batchno,transno, BATCHDESC,TRANSDESC,RDPDATE from Item_Production_Header where batchno >'" + txtBatchNo.Text + "' order by TRANSNO asc";
            LoadBatch();
        }

        private void btnPreviousBatch_Click(object sender, EventArgs e)
        {
            strSQL = "select top 1 batchno,transno, BATCHDESC,TRANSDESC,RDPDATE from Item_Production_Header where batchno <'" + txtBatchNo.Text + "' order by TRANSNO desc";
            LoadBatch();
        }


        private void LoadBatch()
        {
            bat = provider.getDataTable(strSQL, "Item_Production_Header");
            if (bat.Rows.Count > 0)
            {
                txtBatchNo.Text = bat.Rows[0][0].ToString();
                txtBatchDesc.Text = bat.Rows[0][2].ToString();
                txtTransDesc.Text = bat.Rows[0][3].ToString();

                lblEntryNo.Text = bat.Rows[0][0].ToString();

                txtTransNo.Text = bat.Rows[0][1].ToString();

            }
        }

        private void dgvRandom_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if(e.ColumnIndex==11)
            //{
            //    e.Value = string.Format("{0:n0}", Convert.ToInt32(e.Value.ToString()));
            //}
        }
        
        
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)                // Export
        {
            provider.ExportToExcel(txtTransNo.Text, Int32.Parse(cmbVersion.Text), userName,txtBatchNo.Text);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)               // Close
        {
            this.Close();
        }

       
        
        DataTableCollection dataTableCollection;
        private void importToolStripMenuItem_Click(object sender, EventArgs e)             // Import
        {

            if (txtBatchDesc.Text == "")          // Check batch description
            {
                MessageBox.Show("Please enter batch description"); 
                this.ActiveControl = txtBatchDesc;
                return;
            }

            if (dgvRandom.Rows.Count<1)      // Check batch
            {
                MessageBox.Show("Please load items before import");
                this.ActiveControl = btnLoad;
                return;
            }

            string trno;
            int ver;

            if(txtTransNo.Text=="****NEW****")
            {
                trno = provider.getResultString("select max(transno) from item_production_header");
                if (trno == "" || trno == null)
                {
                    trno = "10000001";
                }
                else
                {
                    trno = (Int32.Parse(trno) + 1).ToString();
                }
            }
            else
            {
                trno = txtTransNo.Text;
            }            

            string batchNo;
            if (txtBatchNo.Text == "****NEW****")
            {
                batchNo = provider.getResultString("select max(batchno) from item_production_header");
                if (batchNo == "" || batchNo == null)
                {
                    batchNo = "0001";
                }
                else
                {
                    batchNo = (Int32.Parse(batchNo) + 1).ToString("D4");
                }
            }
            else
            {
                batchNo = txtBatchNo.Text;
            }

            if(cmbVersion.Text==""|| cmbVersion.Text=="Select Option")
            {
                ver = 1;
            }
            else
            {
                ver = Int32.Parse(cmbVersion.Text) + 1;
            }

            string fileName;

            using (OpenFileDialog openfd = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls" })      // Excell Sheet Import
            {
                if (openfd.ShowDialog() == DialogResult.OK)
                {
                    fileName = openfd.FileName;

                    using (var stream = File.Open(openfd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });
                            dataTableCollection = result.Tables;
                            cmbTableItem.Items.Clear();
                            foreach (DataTable table in dataTableCollection)
                            {
                                cmbTableItem.Items.Add(table.TableName);
                            }
                        }
                    }
                }
            }
            cmbTableItem.SelectedIndex = 0;
            System.Data.DataTable dtHeader = dataTableCollection[cmbTableItem.SelectedItem.ToString()];

            cmbTableItem.SelectedIndex = 1;
            System.Data.DataTable dtPlan = dataTableCollection[cmbTableItem.SelectedItem.ToString()];

            cmbTableItem.SelectedIndex = 2;
            System.Data.DataTable dtRSP = dataTableCollection[cmbTableItem.SelectedItem.ToString()];


            DataView dv = new DataView(dtRSP);
            DataTable itemList = dv.ToTable(true, "itemid");

            //DataView dv2 = new DataView(dtPlan);
            //DataTable itemList2 = dv2.ToTable(true, "itemid");

            //dgvProjects.SelectionMode = DataGridViewSelectionMode.FullRowSelect;           
            

            if (dtPlan.Rows.Count!= Int32.Parse(lblTotalItem.Text))
            {
                MessageBox.Show("Item number does not match ! Please check your excel file");
                dtHeader.Clear();
                dtPlan.Clear();
                dtRSP.Clear();
                itemList.Clear();                
                return;
            }

            if(dtHeader.Rows.Count<1 || dtHeader.Rows.Count > 1)
            {
                MessageBox.Show("Wrong Batch Info! Please check your excel file ");
                dtHeader.Clear();
                dtPlan.Clear();
                dtRSP.Clear();
                itemList.Clear();                
                return;
            }

            int countDGV = dgvRandom.Rows.Count;
            int countDTPlan = dtPlan.Rows.Count;
            string itemName ="";
            for(int i=0; i< countDGV; i++)
            {
                itemName = dgvRandom.Rows[i].Cells[1].Value.ToString(); 

                for(int j=0; j< dtPlan.Rows.Count; j++)
                {
                    if(dtPlan.Rows[j][7].ToString() == itemName)
                    {
                        break;
                    }     
                    
                    if(j==countDTPlan-1)
                    {
                        MessageBox.Show("Item Number " + itemName + " missing in the file");
                        dtHeader.Clear();
                        dtPlan.Clear();
                        dtRSP.Clear();
                        itemList.Clear();                        
                        return;
                    }
                
                }
                
            }


            // Check for RSP items
            string RSPitem = "";
            for(int i=0; i<itemList.Rows.Count; i++)
            {
                RSPitem = itemList.Rows[i][0].ToString();

                for(int j = 0; j< dgvRandom.Rows.Count; j++)
                {
                    if(RSPitem == dgvRandom.Rows[j].Cells[1].Value.ToString())
                    {
                        break;
                    }
                    else
                    {
                        if (j == dgvRandom.Rows.Count - 1)
                        {
                            MessageBox.Show("RSP item ID " + RSPitem + " was not found in the item list");
                            dtHeader.Clear();
                            dtPlan.Clear();
                            dtRSP.Clear();
                            itemList.Clear();
                            
                            return;
                        }
                    }                    
                }
            }

            string item;
            for (int i = 0; i < itemList.Rows.Count; i++)
            {
                item = dgvRandom.Rows[i].Cells[1].Value.ToString();

                for (int j = 0; j < dtPlan.Rows.Count; j++)
                {
                    if (item == dtPlan.Rows[j][7].ToString())
                    {
                        break;
                    }
                    else
                    {
                        if (j == dgvRandom.Rows.Count - 1)
                        {
                            MessageBox.Show("Item ID " + item + " was not found in the item list of entry info");
                            dtHeader.Clear();
                            dtPlan.Clear();
                            dtRSP.Clear();
                            itemList.Clear();
                            
                            return;
                        }
                    }
                }
            }

            if (txtTransNo.Text == "****NEW****")
            {
                strSQL = "insert into ITEM_Production_Header(RDPPERIOD,BATCHNO,TRANSNO,BATCHDESC,TRANSDESC,BATCHDATE,TRANSDATE,STATUS,BATCHTOTAL,AUDTDATE,AUDTTIME,AUDTUSER,LASTMNDATE," +
                "LASTMNUSER,RDPDATE,Executive) VALUES('" + dtHeader.Rows[0][3].ToString() + "', '" + batchNo + "','" + trno + "' ,'" + txtBatchDesc.Text + "', '" + txtTransDesc.Text + "', " + dtHeader.Rows[0][2].ToString() + ", " + DateTime.Today.ToString("yyyyMMdd") + "," +
                " 0,'" + dtHeader.Rows[0][5].ToString() + "' ," + AUDTDATE + ", " + AUDTTIME + ", '" + userName + "', " + AUDTTIME + ", '" + userName + "','" + dtpRDPPeriod.Value + "','" + lblUserID.Text + "')";

                provider.ExecuteCommand(strSQL);
            }
            else
            {
                strSQL = "update Item_Production_Header set transdesc = '" + txtTransDesc.Text + "' , batchtotal = '" + dtHeader.Rows[0][5].ToString() + "'  where transno = '" + txtTransNo.Text + "' ";
                provider.ExecuteCommand(strSQL);
            }

            for (int i = 0; i < dtPlan.Rows.Count; i++)
            {
                strSQL = "insert into ITEM_Production_Planning (id,BatchNo,TransNo,Executive,production,avgSalesPeriodFrom,avgSalesPeriodTo,RDPDate,ItemID,ItemName,sector, plant,productType,uom,tp,TongiStock,RupgonjStock,TotalFGStock,TDCLStock,jan,feb,mar,apr,May,jun,jul,aug,sep,oct,nov,[dec],AvgSales,prevQty,SystemQty, ActualQty, RDPMonth2,RDPMonth3,RSPMonth1,RSPMonth2,RSPMonth3,ExpDeliveryDate,Remark,Status,CreatedDate,CreatedBy,Stock,Version,Trend) " +
                "values ('" + (i + 1) + "', '" + batchNo + "', '" + trno + "', '" + lblUserID.Text + "', '" + dtPlan.Rows[i][3].ToString() + "', '" + dtPlan.Rows[i][4].ToString() + "', '" + dtPlan.Rows[i][5].ToString() + "', '" + dtPlan.Rows[i][6].ToString() + "', '" + dtPlan.Rows[i][7].ToString() + "', '" + dtPlan.Rows[i][8].ToString().Replace("'", "''") + "','" + dtPlan.Rows[i][9].ToString() + "', '" + dtPlan.Rows[i][10].ToString() + "', '" + dtPlan.Rows[i][11].ToString() + "', '" + dtPlan.Rows[i][12].ToString().Replace("'", "''") + "', '" + dtPlan.Rows[i][13].ToString() + "', '" + dtPlan.Rows[i][14].ToString() + "', '" + dtPlan.Rows[i][15].ToString() + "', '" + dtPlan.Rows[i][16].ToString() + "', '" + dtPlan.Rows[i][17].ToString() + "', '" + dtPlan.Rows[i][18].ToString() + "', '" + dtPlan.Rows[i][19].ToString() + "', '" + dtPlan.Rows[i][20].ToString() + "', '" + dtPlan.Rows[i][21].ToString() + "', '" + dtPlan.Rows[i][22].ToString() + "', '" + dtPlan.Rows[i][23].ToString() + "', '" + dtPlan.Rows[i][24].ToString() + "', '" + dtPlan.Rows[i][25].ToString() + "', '" + dtPlan.Rows[i][26].ToString() + "', '" + dtPlan.Rows[i][27].ToString() + "', '" + dtPlan.Rows[i][28].ToString() + "', '" + dtPlan.Rows[i][29].ToString() + "', '" + dtPlan.Rows[i][30].ToString() + "', '" + dtPlan.Rows[i][31].ToString() + "', '" + dtPlan.Rows[i][32].ToString() + "', '" + dtPlan.Rows[i][33].ToString() + "', '" + dtPlan.Rows[i][34].ToString() + "', '" + dtPlan.Rows[i][35].ToString() + "', '" + dtPlan.Rows[i][36].ToString() + "', '" + dtPlan.Rows[i][37].ToString() + "', '" + dtPlan.Rows[i][38].ToString() + "', '" + dtPlan.Rows[i][39].ToString() + "','" + dtPlan.Rows[i][40].ToString() + "' , '0', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + userName + "', '" + dtPlan.Rows[i][44].ToString() + "', '" + ver + "', '" + dtPlan.Rows[i][46].ToString() + "')";

                provider.ExecuteCommand(strSQL);
            }

            for (int i = 0; i < dtRSP.Rows.Count; i++)
            {
                strSQL = "INSERT INTO RSP_Monthly(batchno,transno,ITEMID, RDP_MONTH, SAMPLE_ID, SAMPLE_NAME, UOM, UOMF, MKT_UOM, MKT_UOMF, STATUS, STKQTY, MONTH1, MONTH2," +
                   " MONTH3, AUDTUSER, AUDTDATE, AUDTTIME, LASTMNUSER, LASTMNDATE,version) VALUES ('" + batchNo + "','" + trno + "','" + dtRSP.Rows[i][0].ToString() + "','" + dtpRDPPeriod.Value.ToString("MMM-yyyy") + "','" + dtRSP.Rows[i][2].ToString() + "','" + dtRSP.Rows[i][3].ToString().Replace("'", "''") + "','" + dtRSP.Rows[i][4].ToString() + "','" + dtRSP.Rows[i][4].ToString().Replace("s", "") + "','" + dtRSP.Rows[i][5].ToString() + "','" + dtRSP.Rows[i][5].ToString().Replace("s", "") + "','0','" + dtRSP.Rows[i][7].ToString() + "','" + dtRSP.Rows[i][8].ToString() + "','" + dtRSP.Rows[i][9].ToString() + "','" + dtRSP.Rows[i][10].ToString() + "','" + userName + "','" + AUDTDATE + "','" + AUDTTIME + "','" + userName + "','" + AUDTDATE + "','" + ver + "')";

                provider.ExecuteCommand(strSQL);
            }




            DataView dv2 = new DataView(dtRSP);
            dv.Sort = "itemid asc";



            int stk = 0, month1 = 0, month2 = 0, month3 = 0;
            int uom, mkt_uom, amount;


            for (int j = 0; j < itemList.Rows.Count; j++)
            {
                dv2.RowFilter = "itemid = " + itemList.Rows[j][0].ToString();


                for (int i = 0; i < dv2.Count; i++)
                {
                    uom = Int32.Parse(dv2[i][4].ToString().Replace("s", ""));
                    mkt_uom = Int32.Parse(dv2[i][5].ToString().Replace("s", ""));           // Stock
                    amount = Int32.Parse(dv2[i][7].ToString().Replace("s", ""));

                    stk += (uom * amount / mkt_uom);
                }

                for (int i = 0; i < dv2.Count; i++)
                {
                    uom = Int32.Parse(dv2[i][4].ToString().Replace("s", ""));
                    mkt_uom = Int32.Parse(dv2[i][5].ToString().Replace("s", ""));      // RSP Month1
                    amount = Int32.Parse(dv2[i][8].ToString().Replace("s", ""));

                    month1 += (uom * amount / mkt_uom);
                }
                for (int i = 0; i < dv2.Count; i++)
                {
                    uom = Int32.Parse(dv2[i][4].ToString().Replace("s", ""));
                    mkt_uom = Int32.Parse(dv2[i][5].ToString().Replace("s", ""));     //RSP Month2
                    amount = Int32.Parse(dv2[i][9].ToString().Replace("s", ""));

                    month2 += (uom * amount / mkt_uom);
                }
                for (int i = 0; i < dv2.Count; i++)
                {
                    uom = Int32.Parse(dv2[i][4].ToString().Replace("s", ""));
                    mkt_uom = Int32.Parse(dv2[i][5].ToString().Replace("s", ""));    // RSP Month3
                    amount = Int32.Parse(dv2[i][10].ToString().Replace("s", ""));

                    month3 += (uom * amount / mkt_uom);
                }

                strSQL = "update item_production_planning set stock= '" + stk + "', RSPMonth1 = '" + month1 + "', RSPMonth2='" + month2 + "' , RSPMonth3='" + month3 + "' where transno = '" + trno + "' and itemid = '" + dv2[0][0].ToString() + "' and version = '"+ver+"'";
                provider.ExecuteCommand(strSQL);

                stk = 0;
                month1 = 0;
                month2 = 0;
                month3 = 0;

            }

            MessageBox.Show("Imported Successfully");

            transNo = trno;
            if (txtBatchNo.Text == "****NEW****")
            {
                entryNo = batchNo;
                txtBatchNo.Text = batchNo;
            }
            //batchTotal = provider.getResultString("select sum(cast(batchtotal as int)) from item_production_header where BATCHNO = '" + txtBatchNo.Text + "'");
            //txtBatchTotal.Text = String.Format("{0:n0}", Int32.Parse(batchTotal));


            transLoadStatus = 0;
            commaStatus = 0;
            if (txtTransNo.Text != "****NEW****")
            {
                transLoadStatus = 1;
                txtTransNo.Text = "";
                transLoadStatus = 0;
                txtTransNo.Text = trno;
            }
            txtTransNo.Text = trno;
            commaStatus = 1;

            //strSQL = "update item_production_header set batchtotal = '" + lblGrandTot.Text.Replace(",", "") + "' where batchno = '" + txtBatchNo.Text + "'";
            strSQL = "update item_production_header set batchtotal = '" + lblGrandTot.Text.Replace(",", "") + "', RDPTotal = '" + lblRdpTot.Text.Replace(",", "") + "', RSPTotal = '" + lblRspTot.Text.Replace(",", "") + "'  where transno = '" + txtTransNo.Text + "'";
            provider.ExecuteCommand(strSQL);

            batchTotal = provider.getResultString("select sum(cast(batchtotal as int)) from item_production_header where batchno = '" + txtBatchNo.Text + "'");
            txtBatchTotal.Text = String.Format("{0:n0}", Int32.Parse(batchTotal));

            LoadGPMItemInfo();
        }



        private void frmTargetManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(RSPSL.Rows.Count>0)
            {
                DialogResult dialogresult = MessageBox.Show("Are you sure to close? You haven't added any entry", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogresult == DialogResult.Yes)
                {
                    for(int i=0; i<RSPSL.Rows.Count;i++)
                    {
                        strSQL = "delete from RSP_Monthly where sl = '" + RSPSL.Rows[i][0] + "'";
                        provider.ExecuteCommand(strSQL);
                    }                    
                }

                if(dialogresult== DialogResult.No)
                {
                    e.Cancel=true;
                }
            }
        }

        private void AfterAdd()
        {
            btnAdd.Enabled = false;
            btnAdd.Visible = false;

            btnSave.Visible = true;
            btnSave.Enabled = true;
        }

        private void GrayColumn()
        {
            foreach (DataGridViewRow Myrow in dgvRandom.Rows)
            {
                Myrow.Cells[0].Style.BackColor = Color.LightGray;
                Myrow.Cells[1].Style.BackColor = Color.LightGray;
                Myrow.Cells[2].Style.BackColor = Color.LightGray;
                Myrow.Cells[3].Style.BackColor = Color.LightGray;                                                         ////******CHANGEd*****/////
                Myrow.Cells[4].Style.BackColor = Color.LightGray;
                Myrow.Cells[5].Style.BackColor = Color.LightGray;
                Myrow.Cells[6].Style.BackColor = Color.LightGray;
                Myrow.Cells[7].Style.BackColor = Color.LightGray;
                Myrow.Cells[8].Style.BackColor = Color.LightGray;
                Myrow.Cells[9].Style.BackColor = Color.LightGray;
                Myrow.Cells[10].Style.BackColor = Color.LightGray;
                Myrow.Cells[11].Style.BackColor = Color.LightGray;
                Myrow.Cells[12].Style.BackColor = Color.LightGray;
                Myrow.Cells[13].Style.BackColor = Color.LightGray;
                Myrow.Cells[14].Style.BackColor = Color.LightGray;
                Myrow.Cells[15].Style.BackColor = Color.LightGray;
                Myrow.Cells[16].Style.BackColor = Color.LightGray;
                Myrow.Cells[17].Style.BackColor = Color.LightGray;
                Myrow.Cells[18].Style.BackColor = Color.LightGray;
                Myrow.Cells[19].Style.BackColor = Color.LightGray;
                Myrow.Cells[20].Style.BackColor = Color.LightGray;
                Myrow.Cells[21].Style.BackColor = Color.LightGray;
          //      Myrow.Cells[22].Style.BackColor = Color.LightGray;
                Myrow.Cells[27].Style.BackColor = Color.LightGray;

                Myrow.Cells[28].Style.BackColor = Color.LightGray;
                Myrow.Cells[29].Style.BackColor = Color.LightGray;
                Myrow.Cells[30].Style.BackColor = Color.LightGray;
                // Myrow.Cells[20].Style.BackColor = Color.LightGray;
            }
            
        }

    }

}