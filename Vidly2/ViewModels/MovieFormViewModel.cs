using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Web;
using Vidly2.Models;

namespace Vidly2.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        //public Movie Movie { get; set; }

        public int? Id { get; set; }

        [Required(ErrorMessage = "Please enter a movie's name.")]
        [StringLength(50)]
        public string Name { get; set; }
        

        [Display(Name = "Release Date")]
        [Required(ErrorMessage = "Enter a valid release date.")]
        public DateTime? ReleaseDate { get; set; }
        
        [Display(Name = "Number in Stock")]
        [Range(1,20)]
        [Required(ErrorMessage = "Enter a number of items in stock.")]
        public int? NumberInStock { get; set; }

        [Display(Name = "Genre")]
        [Required(ErrorMessage = "Please select an appropriate type of Genre.")]
        public byte? GenreId { get; set; }

        public string Title
        {
            get
            {
                return Id != 0 ? "Edit Movie" : "New Movie";
            }
        }

        public MovieFormViewModel()
        {
            Id = 0;
        }
        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            NumberInStock = movie.NumberInStock;
            ReleaseDate = movie.ReleaseDate;
            GenreId = movie.GenreId;
        }
    }
}