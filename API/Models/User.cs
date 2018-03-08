using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class User
    {
        [Required]
        public Guid Id { get; private set; }

        [Required]
        public string FullName { get; private set; }

        [Required]
        public string Email { get; private set; }

        [Required]
        public string ImageUrl { get; private set; }
    }
}