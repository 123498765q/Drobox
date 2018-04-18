using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class SharedFilesData
    {
        public string sub { get; set; }
        public string[] Files { get; set; }

        public SharedFilesData()
        {
        }

        public SharedFilesData(string sub, string[] files)
        {
            this.sub = sub;
            Files = files;
        }
    }
}