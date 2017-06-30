using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data.DataSources
{
    class HockeyReferenceRequest
    {
        public static string MakeRequest(string requestUrl)
        {
            // configure the request to go to hockey reference
            HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            // read the html as a string
            string responseText = null;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                while (!reader.EndOfStream)
                {
                    responseText += reader.ReadLine().Trim();
                }
            }

            return responseText;
        }

    }
}
