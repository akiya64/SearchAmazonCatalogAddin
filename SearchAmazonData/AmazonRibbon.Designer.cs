namespace SearchAmazonData
{
    partial class AmazonAPI : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public AmazonAPI()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary>
        /// デザイナーのサポートに必要なメソッドです。
        /// このメソッドの内容をコード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.AmazonApiTab = this.Factory.CreateRibbonTab();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.button1 = this.Factory.CreateRibbonButton();
            this.button2 = this.Factory.CreateRibbonButton();
            this.AmazonApiTab.SuspendLayout();
            this.group2.SuspendLayout();
            this.SuspendLayout();
            // 
            // AmazonApiTab
            // 
            this.AmazonApiTab.Groups.Add(this.group2);
            this.AmazonApiTab.Label = "Amazon API";
            this.AmazonApiTab.Name = "AmazonApiTab";
            // 
            // group2
            // 
            this.group2.Items.Add(this.button1);
            this.group2.Items.Add(this.button2);
            this.group2.Label = "Adevertising API";
            this.group2.Name = "group2";
            // 
            // button1
            // 
            this.button1.Label = "商品情報＆画像取得";
            this.button1.Name = "button1";
            this.button1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Label = "画像ダウンロード";
            this.button2.Name = "button2";
            this.button2.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button2_Click);
            // 
            // AmazonAPI
            // 
            this.Name = "AmazonAPI";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.AmazonApiTab);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
            this.AmazonApiTab.ResumeLayout(false);
            this.AmazonApiTab.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public Microsoft.Office.Tools.Ribbon.RibbonTab AmazonApiTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button2;
    }

    partial class ThisRibbonCollection
    {
        internal AmazonAPI Ribbon1
        {
            get { return this.GetRibbon<AmazonAPI>(); }
        }
    }
}
