/**********************************************************************************************
 * Copyright 2009 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"). You may not use this file 
 * except in compliance with the License. A copy of the License is located at
 *
 *       http://aws.amazon.com/apache2.0/
 *
 * or in the "LICENSE.txt" file accompanying this file. This file is distributed on an "AS IS"
 * BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under the License. 
 *
 * ********************************************************************************************
 *
 *  Amazon Product Advertising API
 *  Signed Requests Sample Code
 *
 *  API Version: 2009-03-31
 *
 */

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml;

namespace SearchAmazonData
{
    class AmazonApiData
    {

        private const string AMAZON_LOCALE = "webservices.amazon.co.jp";
        private const string NAMESPACE = "http://webservices.amazon.com/AWSECommerceService/2011-08-01";

        private string KeyId = Properties.Settings.Default.AwsAccessKey;
        private string SecretKey = Properties.Settings.Default.MyAwsSecretKey;
        private string ASSOCIATE_TAG = Properties.Settings.Default.AssociateTag;

        public String Jan;
        public String Asin;
        public String Content;
        public String Title;
        public String Spec;
        public String ImgUrl;

        public AmazonApiData(String Jan)
        {

            this.Jan = Jan;
            SearchCatalog();

        }

        private void SearchCatalog()
        {
            /*
             * Here is an ItemLookup example where the request is stored as a dictionary.
             */
            IDictionary<string, string> elements = new Dictionary<string, String>();

            elements["Service"] = "AWSECommerceService";
            elements["SearchIndex"] = "All";
            elements["Operation"] = "ItemSearch";
            elements["Keywords"] = Jan;
            elements["ResponseGroup"] = "EditorialReview,Images,ItemAttributes";

            /* Random params for testing */


            SignedRequestHelper helper = new SignedRequestHelper(KeyId, SecretKey, AMAZON_LOCALE, ASSOCIATE_TAG);

            String requestUrl;
            requestUrl = helper.Sign(elements);

            FetchAmazonData(requestUrl);

        }

        private void FetchAmazonData(string url)
        {
            try
            {
                WebRequest request = HttpWebRequest.Create(url);
                WebResponse response = request.GetResponse();
                XmlDocument doc = new XmlDocument();
                doc.Load(response.GetResponseStream());

                XmlNodeList errorMessageNodes = doc.GetElementsByTagName("Message", NAMESPACE);
                if (errorMessageNodes != null && errorMessageNodes.Count > 0)
                {
                    String message = errorMessageNodes.Item(0).InnerText;
                    String[] ErrorMessage = new string[1];
              
                    ErrorMessage[0] = "Error: " + message + " (but signature worked)";

                }

                XmlNode TitleNode = doc.GetElementsByTagName("Title", NAMESPACE).Item(0);
                this.Title = TitleNode.InnerText;

                XmlNode ContentNode = doc.GetElementsByTagName("Content", NAMESPACE).Item(0);
                this.Content = ContentNode.InnerText;

                XmlNode AsinNode = doc.GetElementsByTagName("ASIN").Item(0);
                this.Asin = AsinNode.InnerText;

                XmlNode LageImageNode = doc.GetElementsByTagName("LargeImage").Item(0);
                this.ImgUrl = LageImageNode.FirstChild.InnerText;

                XmlNodeList Features = doc.GetElementsByTagName("Feature", NAMESPACE);
                StringBuilder Spec = new StringBuilder();

                foreach (XmlNode Feature in Features)
                {
                    Spec.AppendLine(Feature.InnerText);
                };

                this.Spec = Spec.ToString();

            }

            catch (Exception e)
            {
                System.Console.WriteLine("Caught Exception: " + e.Message);
                System.Console.WriteLine("Stack Trace: " + e.StackTrace);
            }

        }

        public static void DownLoadImage(String ImageUrl, String FileName)
        {
            if ( !(ImageUrl == null) && !(FileName == null))
            {
                WebClient dl = new WebClient();

                String SavePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures) + "\\" + FileName + ".jpg";

                dl.DownloadFile(ImageUrl,SavePath);
            }
        }
    }
}
