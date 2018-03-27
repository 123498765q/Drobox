using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.Classes
{
    public class UserInfo
    {
        public string sub { get; set; }
        public string name { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string profile { get; set; }
        public string picture { get; set; }
        public string gender { get; set; }
        public string locale { get; set; }
    }
}