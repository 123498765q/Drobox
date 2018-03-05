using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Person
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Key]
        public int Id { get; private set; }

        [Required]
        public int Age { get; set; }
    }
}