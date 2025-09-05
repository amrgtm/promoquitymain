using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public class SendSmsApi
    {


        public string SendSparrow(  string to, string sms)
        {

            string responseMessage = "";

            string fromIdentity = "TheAlert";
            string token = "v2_hDpewICarsb43bN37evVqRUu3hN.Hhih";

            using (var client = new System.Net.WebClient())
            {
                string parameters = "http://api.sparrowsms.com/v2/sms/?";
                parameters += "from=" + fromIdentity;
                parameters += "&to=" + to;
                parameters += "&text=" + sms.Trim();
                parameters += "&token=" + token;
                var responseString = client.DownloadString(parameters);
                var jo = Newtonsoft.Json.Linq.JObject.Parse(responseString);
                responseMessage = jo["response_code"].ToString();

            }

            //    break;
            //case "aakash":
            //case "akash":
            //    using (var client = new WebClient())
            //    {
            //        var values = new NameValueCollection();
            //        values["auth_token"] = dtSmsProviders.Rows[0]["Token"].ToString();
            //        values["to"] = to;
            //        values["text"] = sms;
            //        var response = client.UploadValues("http://sms.aakashsms.com/sms/v3/send/", "Post", values);
            //        var responseString = Encoding.Default.GetString(response);

            //        var jo = Newtonsoft.Json.Linq.JObject.Parse(responseString);
            //        if (jo["error"].ToString().ToLower() == "false")
            //        {
            //            responseMessage = "200";
            //        }
            //        else
            //        {
            //            responseMessage = "500";
            //        }

            //    }
            //    break;

            //default:
            //    using (var client = new System.Net.WebClient())
            //    {
            //        string parameters = "http://api.sparrowsms.com/v2/sms/?";
            //        parameters += "from=" + dtSmsProviders.Rows[0]["Identity"].ToString();
            //        parameters += "&to=" + to;
            //        parameters += "&text=" + sms.Trim();
            //        parameters += "&token=" + dtSmsProviders.Rows[0]["Token"].ToString();
            //        var responseString = client.DownloadString(parameters);
            //        var jo = Newtonsoft.Json.Linq.JObject.Parse(responseString);
            //        responseMessage = jo["response_code"].ToString();

            //    }
            //    break;

            return responseMessage;
        }

    }
}
