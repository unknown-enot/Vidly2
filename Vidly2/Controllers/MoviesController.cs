using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly2.Models;
using Vidly2.ViewModels;

namespace Vidly2.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext context;

        public MoviesController()
        {
            context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        //private IEnumerable<Movie> GetMovies()
        //{
        //    return new List<Movie>
        //    {
        //        new Movie {Id = 1, Name = "Shrek"},
        //        new Movie {Id = 2, Name = "Wall-e"}    
        //    };
        //}
        
        // GET: Movies
        public ActionResult Index()
        {
            //var movies = context.Movies.Include(g=>g.Genre).ToList();
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List"); //return View(movies);

            return View("ReadOnlyList");

            //return Content("Hello World!");
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new {page = 1, sortBy = "name"});

        }

        public ActionResult Details(int id)
        {
            var movie = context.Movies.Include(g => g.Genre).SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genreTypes = context.Genres.ToList();
            var viewModel = new MovieFormViewModel
            {
                Genres = genreTypes,
                //Movie = new Movie()
            };

            return View("MovieForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                   Genres = context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }


    }
}