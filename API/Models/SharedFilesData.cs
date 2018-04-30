using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class SharedFilesData
    {
        public string sourcePath { get; set; }
        public string destPath { get; set; }

        public SharedFilesData()
        {
        }
    }
}