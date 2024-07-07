using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using static Android.Provider.UserDictionary;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace MyRESTfulDB
{
    internal class Transact
    {
        private StreamReader reader;
        private HttpWebResponse response;
        private HttpWebRequest request;
        private JsonElement root;
        private String sID, userName, passWord, name, school, country, gender, res, ipAdd;

        public Transact(string ipAdd, string uName, string pWord)
        {
            this.ipAdd = ipAdd;
            this.userName = uName;
            this.passWord = pWord;
        }

        public Transact(string ipAdd, string name, string school, string country, string gender)
        {
            this.ipAdd = ipAdd;
            this.name = name;
            this.school = school;
            this.country = country;
            this.gender = gender;
        }

        public Transact(string ipAdd, string sID)
        {
            this.ipAdd = ipAdd;
            this.sID = sID;
        }

        public Transact(string ipAdd, string sID, string name, string school, string country, string gender)
        {
            this.ipAdd = ipAdd;
            this.sID = sID;
            this.name = name;
            this.school = school;
            this.country = country;
            this.gender = gender;
        }

        public string LogRequest()
        {
            request = (HttpWebRequest)WebRequest.Create($"http://{ipAdd}/IT123P/admin_login.php?uname={userName}&pword={passWord}");
            response = (HttpWebResponse)request.GetResponse();
            reader = new StreamReader(response.GetResponseStream());
            res = reader.ReadToEnd();
            return res;
        }

        public string AddRequest()
        {
            request = (HttpWebRequest)WebRequest.Create($"http://{ipAdd}/IT123P/add_record.php?name={name}&school={school}&country={country}&gender={gender}");
            response = (HttpWebResponse)request.GetResponse();
            reader = new StreamReader(response.GetResponseStream());
            res = reader.ReadToEnd();
            return res;
        }

        public JsonElement SearchRequest()
        {
            request = (HttpWebRequest)WebRequest.Create($"http://{ipAdd}/IT123P/search_record.php?sID={sID}");
            response = (HttpWebResponse)request.GetResponse();
            res = response.ProtocolVersion.ToString();
            reader = new StreamReader(response.GetResponseStream());

            var result = reader.ReadToEnd();
            using JsonDocument doc = JsonDocument.Parse(result);
            root = doc.RootElement.Clone();
            return root[0];
        } 

        public string UpdateRequest()
        {
            request = (HttpWebRequest)WebRequest.Create($"http://{ipAdd}/IT123P/update_record.php");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            string postData = $"sID={Uri.EscapeDataString(sID)}" +
                              $"&name={Uri.EscapeDataString(name)}" +
                              $"&school={Uri.EscapeDataString(school)}" +
                              $"&country={Uri.EscapeDataString(country)}" +
                              $"&gender={Uri.EscapeDataString(gender)}";

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteArray.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            using (response = (HttpWebResponse)request.GetResponse())
            {
                using (reader = new StreamReader(response.GetResponseStream()))
                {
                    string responseText = reader.ReadToEnd();
                    return responseText;
                }
            }
        }
    }
}