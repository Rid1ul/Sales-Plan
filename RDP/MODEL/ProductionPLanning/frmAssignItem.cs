using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDP.UI.Master_Setup
{
    public partial class frmAssignItem : Form
    {
        public frmAssignItem()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            btnSave.Visible = false;
            cmbItemName.SelectedIndex = 0;
            cmbUser.SelectedIndex = 0;
        }
    }
}
