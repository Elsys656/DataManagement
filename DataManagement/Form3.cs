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
    public partial class PDT : Form
    {
        int productContext = 0;

        public int GetProductContext()
        {
            return productContext;
        }

        public void SetProductContext(int pc)
        {
            productContext = pc;
        }
        public PDT()
        {
            InitializeComponent();
        }

        private void PDT_Load(object sender, EventArgs e)
        {
            using (var dc = new DataClasses1DataContext())
            {
                var query = dc.ProductUPCs.Where(x => x.ProductId == productContext).First();
                ImageConverter converter = new ImageConverter();
                pictureBox1.Image = (Image)converter.ConvertFrom(query.UpcImage.ToArray());
                textBox2.Text = query.Product.ProductID.ToString();
                textBox3.Text = query.Product.Name;
                textBox6.Text = query.Product.Weight.ToString();
                textBox4.Text = Math.Round(query.Product.ListPrice,2).ToString();
                var query1 = dc.ProductInventories.Where(x => x.ProductID == productContext);
                int quanity = 0;
                //enumerate each location with like product to get inventory total
                foreach(var item in query1)
                {
                    quanity += item.Quantity;
                }
                textBox5.Text = quanity.ToString();
                var query3 = dc.vProductAndDescriptions.Where(x => x.ProductID == productContext).First();
                textBox1.Text = query3.Description.ToString();
            }
        }
    }
}
