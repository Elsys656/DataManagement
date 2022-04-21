using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataManagement
{
    public partial class PBG : Form
    {
        public PBG()
        {
            InitializeComponent();
        }

        public static void dbWorker(IProgress<String> tProgress, IProgress<int> vProgress)
        {
            using (var dc = new DataClasses1DataContext())
            {
                var query = dc.ProductUPCs;
                var count = 0;
                var total = query.Count();
                foreach (var item in query)
                {
                    count++;
                    vProgress.Report((count / total) * 100);
                    tProgress.Report("Rows: " + count.ToString() + "/" + total.ToString());
                    BarcodeLib.Barcode.Linear barcode = BarcodeHelper.LinearBarcode();
                    if (item.UpcAType != 0 && item.UpcAType == null)
                    {
                        //If row not initialized or otherwise set to other than 0 for general merchandise
                        item.UpcAType = 0;
                    }
                    if (item.UpcACompany == null)
                    {
                        //If company not otherwise known for database product assign company from app config.
                        item.UpcACompany = Int32.Parse(ConfigurationManager.AppSettings.Get("companyUPCID"));
                    }
                    barcode.Data = item.UpcAType.ToString() + item.UpcACompany.ToString() + item.UpcID.ToString();
                    //Store upc image at database as bin
                    item.UpcImage = barcode.drawBarcodeAsBytes().ToArray();
                    //Update database every 1000 records for speed
                if (count % 1000 == 0)
                    {
                        dc.SubmitChanges();
                    }
                }
                //Finalize database update ie whatevers left to process
                dc.SubmitChanges();
            }
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            var tProgress = new Progress<String>(s => label1.Text = s);
            var vProgress = new Progress<int>(i => progressBar1.Value = i);
            await Task.Factory.StartNew(() => dbWorker(tProgress, vProgress), TaskCreationOptions.LongRunning);
            button1.Enabled = true;
        }
    }
}
