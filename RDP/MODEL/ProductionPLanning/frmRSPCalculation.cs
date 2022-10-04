using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RDP.Class;

namespace RDP.MODEL.ProductionPLanning
{
    public partial class frmRSPCalculation : Form
    {
        public int m1tot, m2tot, m3tot, stock,sampleCount;
        public int version;
        string strSQL;
        public DataTable ItemRSP;
        dataProvider dp = new dataProvider();
        DataTable dtable = new DataTable();
        int row, col;

        DataTable dt = new DataTable();
        dataProvider dpr = new dataProvider();

        public string month1, month2, month3, itemID, rdp_Period,username,transno;
        

        string AUDTDATE = DateTime.Now.ToString("yyyyMMdd");
        string AUDTTIME = DateTime.Now.ToString("HHmmss");

        
        public frmRSPCalculation()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void frmRSPCalculation_Load(object sender, EventArgs e)
        {

            dgvRSP.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRSP.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRSP.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;      




            //m1.Text = itemID;

            //dgvRSP.ColumnHeadersHeight = dgvRSP.ColumnHeadersHeight * 3;
            //dgvRSP.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            //dgvRSP.CellPainting += new DataGridViewCellPaintingEventHandler(dgvRSP_CellPainting);
            //dgvRSP.Paint += new PaintEventHandler(dgvRSP_Paint);
            //dgvRSP.Scroll += new ScrollEventHandler(dgvRSP_Scroll);
            //dgvRSP.ColumnWidthChanged += new DataGridViewColumnEventHandler(dgvRSP_ColumnWidthChanged);

            btnSave.Enabled = false;
            btnSave.Visible = false;

            dgvRSP.Columns[6].HeaderText = month1;
            dgvRSP.Columns[7].HeaderText = month2;
            dgvRSP.Columns[8].HeaderText = month3;

            //lblM1.Text = month1+":";
            //lblM2.Text = month2+":";
            //lblM3.Text = month3+":";            


            strSQL = "select * from RSP_Monthly where ITEMID = '" + itemID + "' and RDP_MONTH = '" + rdp_Period + "'  and version = "+version+" and status = 0";
            dtable = dp.getDataTable(strSQL, "RSP_Monthly");

            if(dtable.Rows.Count<1)
            {
                strSQL = "SELECT t1.*,ISNULL(T2.STKQTY,0) AS STKQTY FROM (select * from item_sample where mkt_id = '" + itemID + "') AS T1 " +
                "LEFT JOIN " +
                "(select ITEMNO, SUM(QTYONHAND) AS STKQTY from [10.168.90.213].ARCPMSTORE.DBO.ICHISTORICALSTOCK WHERE AUDTDATE = '20220506' GROUP BY ITEMNO)" +
                " AS T2 ON(T1.SAMPLE_ID = T2.ITEMNO) ";

                

                dt = dpr.getDataTable(strSQL, "item_sample");
                dgvRSP.AutoGenerateColumns = false;
                dgvRSP.DataSource = dt;

                //Serial no.
                for (int i = 0; i < dgvRSP.Rows.Count - 1; i++)
                {
                    dgvRSP.Rows[i].Cells[0].Value = i + 1;
                }
            }  
            else
            {
                dgvRSP.AutoGenerateColumns = false;
                dgvRSP.DataSource = dtable;

                //Serial no.
                for (int i = 0; i < dgvRSP.Rows.Count - 1; i++)
                {
                    dgvRSP.Rows[i].Cells[0].Value = i + 1;
                }

              //  CalculateTotal();
            }

            //  AddComma(5);

            AddComma(6);
            AddComma(7);
            AddComma(8);


        }

        private void AddComma(int v)
        {
            string value;

            for (int i = 0; i < dgvRSP.Rows.Count-1; i++)
            {
                if (dgvRSP.Rows[i].Cells[v].Value == null || dgvRSP.Rows[i].Cells[v].Value == DBNull.Value || String.IsNullOrWhiteSpace(dgvRSP.Rows[i].Cells[v].Value.ToString()))
                {

                }
                else
                {
                    value = dgvRSP.Rows[i].Cells[v].Value.ToString();
                    value = String.Format("{0:n0}", Int32.Parse(value));
                    dgvRSP.Rows[i].Cells[v].Value = value; //.ToString();
                }

            }

        }

        private void CalculateTotal()
        {
            int m1, m2, m3, stk;

            m1tot = 0;
            m2tot = 0;
            m3tot = 0;
            stock = 0;

            for (int i = 0; i< dgvRSP.Rows.Count-1;i++)
            {
                if (dgvRSP.Rows[i].Cells[6].Value == null || dgvRSP.Rows[i].Cells[6].Value == DBNull.Value || String.IsNullOrWhiteSpace(dgvRSP.Rows[i].Cells[6].Value.ToString()))
                {
                    m1 = 0;
                }
                else m1 = Int32.Parse(dgvRSP.Rows[i].Cells[6].Value.ToString());

                m1tot += (m1* Int32.Parse(dt.Rows[i][3].ToString()))/ Int32.Parse(dt.Rows[i][6].ToString());      
            }


        }

        private void dgvRSP_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //if(e.RowIndex==-1 && e.ColumnIndex>-1)
            //{
            //    Rectangle r2 = e.CellBounds;
            //    r2.Y += e.CellBounds.Height/2;
            //    r2.Height = e.CellBounds.Height /2;
            //    e.PaintBackground(r2, true);
            //    e.PaintContent(r2);
            //    e.Handled = true; 
            //}

        }

        private void dgvRSP_Paint(object sender, PaintEventArgs e)
        {
            //Rectangle r1 = dgvRSP.GetCellDisplayRectangle(6, -1, true);
            //int w2 = dgvRSP.GetCellDisplayRectangle(8, -1, true).Width;
            //r1.X += 1;
            //r1.Y += 1;
            //r1.Width = r1.Width + w2 - 2;
            //r1.Height = r1.Height / 2 - 2;
            //e.Graphics.FillRectangle(new SolidBrush(dgvRSP.ColumnHeadersDefaultCellStyle.BackColor),r1);

            //StringFormat format = new StringFormat();
            //format.Alignment = StringAlignment.Center;
            //format.LineAlignment = StringAlignment.Center;

            //e.Graphics.DrawString("RSP Target", dgvRSP.ColumnHeadersDefaultCellStyle.Font,
            //    new SolidBrush(dgvRSP.ColumnHeadersDefaultCellStyle.ForeColor),r1, format);


        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            m1tot = 0;
            m2tot = 0;
            m3tot = 0;
            stock = 0;

            int count = 0;

            string UOMF, MKT_UOMF;
           
            if(dgvRSP.DataSource== dtable)
            {
                for (int i = 0; i < dgvRSP.Rows.Count - 1; i++)
                {
                    m1tot += (Int32.Parse(dgvRSP.Rows[i].Cells[6].Value.ToString().Replace(",","")) * Int32.Parse(dtable.Rows[i][6].ToString())) / Int32.Parse(dtable.Rows[i][8].ToString());
                }


                for (int i = 0; i < dgvRSP.Rows.Count - 1; i++)
                {
                    m2tot += (Int32.Parse(dgvRSP.Rows[i].Cells[7].Value.ToString().Replace(",", "")) * Int32.Parse(dtable.Rows[i][6].ToString())) / Int32.Parse(dtable.Rows[i][8].ToString());
                }


                for (int i = 0; i < dgvRSP.Rows.Count - 1; i++)
                {
                    m3tot += (Int32.Parse(dgvRSP.Rows[i].Cells[8].Value.ToString().Replace(",", "")) * Int32.Parse(dtable.Rows[i][6].ToString())) / Int32.Parse(dtable.Rows[i][8].ToString());
                }


                for (int i = 0; i < dgvRSP.Rows.Count - 1; i++)
                {
                    stock += (Int32.Parse(dgvRSP.Rows[i].Cells[5].Value.ToString().Replace(",", "")) * Int32.Parse(dtable.Rows[i][6].ToString())) / Int32.Parse(dtable.Rows[i][8].ToString());
                }

            }
            else
            {
                for (int i = 0; i < dgvRSP.Rows.Count - 1; i++)
                {
                    m1tot += (Int32.Parse(dgvRSP.Rows[i].Cells[6].Value.ToString()) * Int32.Parse(dt.Rows[i][3].ToString())) / Int32.Parse(dt.Rows[i][6].ToString());
                }


                for (int i = 0; i < dgvRSP.Rows.Count - 1; i++)
                {
                    m2tot += (Int32.Parse(dgvRSP.Rows[i].Cells[7].Value.ToString()) * Int32.Parse(dt.Rows[i][3].ToString())) / Int32.Parse(dt.Rows[i][6].ToString());
                }


                for (int i = 0; i < dgvRSP.Rows.Count - 1; i++)
                {
                    m3tot += (Int32.Parse(dgvRSP.Rows[i].Cells[8].Value.ToString()) * Int32.Parse(dt.Rows[i][3].ToString())) / Int32.Parse(dt.Rows[i][6].ToString());
                }


                for (int i = 0; i < dgvRSP.Rows.Count - 1; i++)
                {
                    stock += (Int32.Parse(dgvRSP.Rows[i].Cells[5].Value.ToString()) * Int32.Parse(dt.Rows[i][3].ToString())) / Int32.Parse(dt.Rows[i][6].ToString());
                }

            }


            //strSQL = "delete from RSP_Monthly where ITEMID = '" + itemID + "' and RDP_MONTH = '" + rdp_Period + "'";  // Delete prev values
            //dp.ExecuteCommand(strSQL);

            for (int i = 0; i < dgvRSP.Rows.Count - 1; i++)     // Insert new values
            {
                UOMF = dgvRSP.Rows[i].Cells["UOM"].Value.ToString().Remove(dgvRSP.Rows[i].Cells["UOM"].Value.ToString().Length - 1);
                MKT_UOMF = dgvRSP.Rows[i].Cells["MKT_UOM"].Value.ToString().Remove(dgvRSP.Rows[i].Cells["MKT_UOM"].Value.ToString().Length - 1);

                strSQL = "INSERT INTO RSP_Monthly(BATCHNO,TRANSNO,ITEMID, RDP_MONTH, SAMPLE_ID, SAMPLE_NAME, UOM, UOMF, MKT_UOM, MKT_UOMF, STATUS, STKQTY, MONTH1, MONTH2," +
                    " MONTH3, AUDTUSER, AUDTDATE, AUDTTIME, LASTMNUSER, LASTMNDATE,version) VALUES" +
                "('','','" + itemID + "', " +
                "'" + rdp_Period + "', " +
                "'" + dgvRSP.Rows[i].Cells["SAMPLE_ID"].Value + "', " +
                "'" + dgvRSP.Rows[i].Cells["SAMPLE_Name"].Value.ToString().Replace("'","''") + "', " +
                "'" + dgvRSP.Rows[i].Cells["UOM"].Value + "'," +
                " '" + UOMF + "', " +
                "'" + dgvRSP.Rows[i].Cells["MKT_UOM"].Value.ToString() + "'," +
                " '" + MKT_UOMF + "'," +
                " 0, " +
                "'" + dgvRSP.Rows[i].Cells["STKQTY"].Value.ToString().Replace(",","") + "', " +
                "'" + dgvRSP.Rows[i].Cells[6].Value.ToString().Replace(",", "") + "'," +
                " '" + dgvRSP.Rows[i].Cells[7].Value.ToString().Replace(",", "") + "'," +
                " '" + dgvRSP.Rows[i].Cells[8].Value.ToString().Replace(",", "") + "', " +
                
                " '" + username + "'," +
                " '" + AUDTDATE + "'," +
                
                " '" + AUDTTIME + "','"+username+"' ,"+AUDTDATE+" ,"+version+")";

                dp.ExecuteCommand(strSQL);
                count++;
            }

            strSQL = "select top " + count + " sl from RSP_Monthly order by sl desc";
            dataProvider rsp = new dataProvider();

            ItemRSP = rsp.getDataTable(strSQL, "RSP_Monthly");

            this.Hide();            

        }

        private void dgvRSP_Scroll(object sender, ScrollEventArgs e)
        {
            //Rectangle rtHeader = dgvRSP.DisplayRectangle;
            //rtHeader.Height = dgvRSP.ColumnHeadersHeight / 2;
            //dgvRSP.Invalidate(rtHeader);



        }

        private void dgvRSP_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            Rectangle rtHeader = dgvRSP.DisplayRectangle;
            rtHeader.Height = dgvRSP.ColumnHeadersHeight / 2;
            dgvRSP.Invalidate(rtHeader);
        }
    }
}
