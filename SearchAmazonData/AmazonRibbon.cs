using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Interop.Excel;

namespace SearchAmazonData
{
    public partial class AmazonAPI
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void button1_Click_1(object sender, RibbonControlEventArgs e)
        {
            Range CurrentSelection = Globals.ThisAddIn.Application.Selection;
            Worksheet CurrentSheet = Globals.ThisAddIn.Application.ActiveSheet;

            foreach (Range r in CurrentSelection)
            {
                string Jan = r.Value;

                try
                {
                    AmazonApiData AmazonData = new AmazonApiData(Jan);

                    if (AmazonData.Asin == null)
                    {
                        r.Offset[0, 7].Value = "検索ヒットなし";
                        continue;
                    }

                    r.Offset[0, 1].Value = AmazonData.Asin;
                    r.Offset[0, 2].Value = AmazonData.Title;
                    r.Offset[0, 3].Value = AmazonData.Spec;
                    r.Offset[0, 4].Value = AmazonData.Content;
                    r.Offset[0, 5].Value = AmazonData.ImgUrl;

                    CurrentSheet.Hyperlinks.Add(r.Offset[0, 6], "https://www.amazon.co.jp/dp/" + AmazonData.Asin);

                    AmazonApiData.DownLoadImage(AmazonData.ImgUrl, AmazonData.Jan);

                }
                catch
                {
                    r.Offset[0, 7].Value = "検索失敗";
                    continue;
                }
            }
        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            Range CurrentSelection = (Range)Globals.ThisAddIn.Application.Selection;

            foreach (Range r in CurrentSelection)
            {
                string Jan = r.Value;

                try
                {
                    String FileName = r.Value;
                    String Url = r.Offset[0, 5].Value;

                    AmazonApiData.DownLoadImage(Url, FileName);

                }
                catch
                {
                    r.Offset[0, 8].Value = "画像取得失敗";
                    continue;
                }
            }
        }
    }
}
