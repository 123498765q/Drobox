using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UWP.Classes
{
    class GPS
    {
        public string GetUserCountryByIp(string ip)
        {
            IpInfo ipInfo = new IpInfo();
            try
            {
                string info = new WebClient().DownloadString("http://ipinfo.io/" + ip);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
            }
            catch (Exception)
            {
                ipInfo.Country = null;
            }
            return ipInfo.Country + ", " + ipInfo.City + "(" + ipInfo.Loc + ")";
        }

        //public string GetLocalIPAddress()
        //{
        //    var host = Dns.GetHostEntry(Dns.GetHostName());
        //    foreach (var ip in host.AddressList)
        //    {
        //        if (ip.AddressFamily == AddressFamily.InterNetwork)
        //        {
        //            return ip.ToString();
        //        }
        //    }
        //    throw new Exception("No network adapters with an IPv4 address in the system!");
        //}



        public string GetPublicIP()
        {
            string url = "http://checkip.dyndns.org";
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            string a4 = a3[0];
            return a4;
        }


        //public string GetLocalIPv4(NetworkInterfaceType _type)
        //{
        //    string output = "";
        //    foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        //    {
        //        if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
        //        {
        //            foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
        //            {
        //                if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
        //                {
        //                    output = ip.Address.ToString();
        //                }
        //            }
        //        }
        //    }
        //    return output;
        //}
    }
}
