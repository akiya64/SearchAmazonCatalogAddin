using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Interop.Excel;

namespace SearchAmazonData
{
    public partial class Ribbon2
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void button1_Click_1(object sender, RibbonControlEventArgs e)
        {
            Range CurrentSelection = (Range)Globals.ThisAddIn.Application.Selection;

            foreach (Range r in CurrentSelection)
            {
                string Jan = r.Value;

                try
                {
                    AmazonApi AmazonData = new AmazonApi(Jan);

                    String[] Data = AmazonData.SearchCatalog();

                    r.Resize[0, 1].Value = Data[0];
                    r.Offset[0, 2].Value = Data[1];
                    r.Offset[0, 3].Value = Data[2];
                    r.Offset[0, 4].Value = r.Address;
                }
                catch
                {
                    r.Offset[0, 1].Value = "取得失敗";
                    r.Offset[0, 4].Value = r.Address;
                    continue;
                }
            }
        }
    }
}
