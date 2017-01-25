﻿/**********************************************************************************************
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
    class AmazonApi
    {
        private const string MY_AWS_ACCESS_KEY_ID = "YOUR_MY_AWS_ACCESS_KEY_ID";
        private const string MY_AWS_SECRET_KEY = "YOUR_SECERET_KEY";
        private const string AMAZON_LOCALE = "webservices.amazon.co.jp";
        private const string ASSOCIATE_TAG = "autumnsky-20";

        private const string NAMESPACE = "http://webservices.amazon.com/AWSECommerceService/2011-08-01";

        public String Jan;
        public String Asin;
        public String Content;
        public String Title;
        public String Spec;
        public String ImgUrL;

        public AmazonApi(String Jan)
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


            SignedRequestHelper helper = new SignedRequestHelper(MY_AWS_ACCESS_KEY_ID, MY_AWS_SECRET_KEY, AMAZON_LOCALE, ASSOCIATE_TAG);

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

                XmlNode AsinNode = doc.GetElementsByTagName("Asin").Item(0);
                this.Asin = AsinNode.InnerText;

                XmlNode LageImageNode = doc.SelectNodes("LargeImage/URL").Item(0);
                this.ImgUrL = LageImageNode.InnerText;

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
    }
}
