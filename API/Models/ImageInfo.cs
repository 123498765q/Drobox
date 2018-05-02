using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class ImageInfo
    {
        [Key]
        public string Path { get; set; }
        [Required]
        public string Sub { get; set; }
        public string Location { get; set; }
        public string Message { get; set; }
        
    }
}