﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly2.Models
{
    public class Genre
    {
        public byte Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}