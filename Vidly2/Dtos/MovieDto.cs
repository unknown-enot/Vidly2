using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly2.Models;

namespace Vidly2.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        public DateTime DateAdded { get; set; }
        
        [Range(1,20)]
        public int NumberInStock { get; set; }
        
        [Required]
        public byte GenreId { get; set; }

        public GenreDto Genre { get; set; }
    }
}