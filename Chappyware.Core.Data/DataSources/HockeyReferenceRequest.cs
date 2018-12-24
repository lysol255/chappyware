﻿using System;
using System.IO;
using System.Net;

namespace Chappyware.Data.DataSources
{
    class HockeyReferenceRequest
    {
        public static string MakeRequest(string requestUrl)
        {
            string responseText = null;
            try
            {
                // configure the request to go to hockey reference
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                // read the html as a string

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    while (!reader.EndOfStream)
                    {
                        responseText += reader.ReadLine().Trim();
                    }
                }

            }
            catch(Exception)
            {
                
            }
            return responseText;

        }

    }
}