using System;
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Interop.Excel;


namespace SearchAmazonData
{
    public partial class Ribbon1
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            Range Selection = (Range)Globals.ThisAddIn.Application.Selection;

            foreach (Range r in Selection){
                String Jan = r.Value;                

                 ItemLookup AmazonData = new ItemLookup(Jan);

                r.Offset[0, 1].Value = AmazonData; 
       
            }

        }
    }
}
