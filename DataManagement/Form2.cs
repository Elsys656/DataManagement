using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataManagement
{
    public partial class DMMain : Form
    {
        private static PBG pbg = null;
        
        public static PBG PBGGetInstance()
        {
            if (pbg == null)
            {
                pbg = new PBG();
                pbg.FormClosed += delegate { pbg = null; };
            }
            return pbg;
        }
        public DMMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form pbg = PBGGetInstance();
            pbg.Show();
        }
    }
}
