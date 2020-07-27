using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Vidly2.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a movie's name.")]
        [StringLength(50)]
        public string Name { get; set; }
        
        public Genre Genre { get; set; }

        [Display(Name = "Release Date")]
        
        public DateTime ReleaseDate { get; set; }
        public DateTime DateAdded { get; set; }

        [Display(Name = "Number in Stock")]

        [Range(1,20)]
        public byte NumberInStock { get; set; }

        public byte NumberAvailable { get; set; }

        [Display(Name = "Genre")]
        [Required(ErrorMessage = "Please select an appropriate type of Genre.")]
        public byte GenreId { get; set; }
        

        
       
    }
}