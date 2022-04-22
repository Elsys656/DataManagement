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
        private static PDT pdt = null;
        
        public static PBG PBGGetInstance()
        {
            if (pbg == null)
            {
                pbg = new PBG();
                pbg.FormClosed += delegate { pbg = null; };
            }
            return pbg;
        }

        public static PDT PDTGetInstance()
        {
            if (pdt == null)
            {
                pdt = new PDT();
                pdt.FormClosed += delegate { pdt = null; };
            }
            return pdt;
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

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            PDT pdt = PDTGetInstance();
            pdt.SetProductContext((int)dataGridView1.SelectedRows[0].Cells[0].Value);
            pdt.Show();
        }

        private void DMMain_Load(object sender, EventArgs e)
        {
            var dc = new DataClasses1DataContext();
            dataGridView1.DataSource = dc.vProductAndDescriptions;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
    }
}
