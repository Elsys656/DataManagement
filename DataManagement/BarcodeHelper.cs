using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarcodeLib.Barcode;

namespace DataManagement
{
    internal static class BarcodeHelper
    {
        public static Linear LinearBarcode()
        {
            Linear barcode = new Linear();
            barcode.Type = BarcodeType.UPCA;
            barcode.UOM = UnitOfMeasure.PIXEL;
            barcode.BarWidth = 1;
            barcode.BarHeight = 80;
            barcode.LeftMargin = 10;
            barcode.RightMargin = 10;
            barcode.TopMargin = 10;
            barcode.BottomMargin = 10;
            barcode.AddCheckSum = true;
            return barcode;
        }
    }
}
