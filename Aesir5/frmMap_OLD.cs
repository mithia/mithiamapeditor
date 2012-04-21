using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Aesir5
{
    public partial class frmMap_OLD : Form
    {
        private frmMain m_parent;
        public frmMap_OLD(frmMain frm)
        {
            InitializeComponent();
            m_parent = frm;
        }

        private void frmMap_Load(object sender, EventArgs e)
        {
            //this.ControlBox = false;
            //this.WindowState = FormWindowState.Maximized;

        }

        private void frmMap_Activated(object sender, EventArgs e)
        {
            
        }
    }
}
