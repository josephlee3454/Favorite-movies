
using System.Transactions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using favoriteMovies.Models;


namespace favoriteMovies.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyContext _context;
         public HomeController(MyContext context)
        {
            _context = context;//alows data though out all methods
        }
        // public Boolean CheckedLoggedIn()
        // {
        //     int? userId = HttpContext.Session.GetInt32("UserId");
        //     return userId != null;

            
        // }

        //helper
        public User GetCurrentUser()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if(userId == null)
            {
                return null;
            }
            return _context.Users.First(u=> u.UserId == userId);
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/register")]
        public IActionResult Register(User userToRegister)
        {
            
            if(_context.Users.Any(u => u.Email == userToRegister.Email))
            {

                ModelState.AddModelError("Email", "Please use a different email.");

            }

            if (ModelState.IsValid)
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                userToRegister.Password =Hasher.HashPassword(userToRegister,userToRegister.Password);

                _context.Add(userToRegister);
                _context.SaveChanges();


                //sesssions
                HttpContext.Session.SetInt32("UserId",userToRegister.UserId);

                return RedirectToAction("Dashboard");
            }
            return View("Index");

        }

        [HttpPost("login")]
        public IActionResult Login(LoginUser userToLogin)
        {
             if (ModelState.IsValid)
            {
            var foundUser = _context.Users.FirstOrDefault(u => u.Email == userToLogin.LoginEmail);
            

            if(foundUser==null)
            {

                ModelState.AddModelError("LoginEmail", "please check your email and password");

                return View("Index");        

            }

            var hasher = new PasswordHasher<LoginUser>();
            Console.WriteLine(foundUser.Password);
            Console.WriteLine(userToLogin.LoginPassword);
            var result = hasher.VerifyHashedPassword(userToLogin, foundUser.Password, userToLogin.LoginPassword);

            if(result == 0 )
            {
                ModelState.AddModelError("LoginEmail","Please check your eamil and password");

                return View("Index");
            }

            HttpContext.Session.SetInt32("UserId",foundUser.UserId);
            return RedirectToAction("Dashboard");

        }
            return View("Index");
    
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            //get the users info 
            // int? userId = HttpContext.Session.GetInt32("UserId");

            // if(userId == null)
            // {
            //     return RedirectToAction("Index");
            // }


            var currentUser = GetCurrentUser();

            if(currentUser==null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.CurrentUser = currentUser;


            ViewBag.AllMovies = _context
                .Movies.Include(movies => movies.PostedBy)
                .Include(movie => movie.Likes)
                .OrderByDescending(movie => movie.Likes.Count);





            return View();

        }



        [HttpPost("movies/{movieId}/delete")]
        public IActionResult DeleteMovie(int movieId)
        {
            var movieToDelete = _context
                .Movies
                .First(m => m.MovieId == movieId);
            _context.Remove(movieToDelete);
            _context.SaveChanges();


            return RedirectToAction("Dashboard");
        }


        [HttpPost("movies/{movieId}/likes/delete")]
        public IActionResult DeleteLike(int movieId)
        {
            var currentUser = GetCurrentUser();


            var likeToDelete = _context.Likes.First(like=>like.MovieId == movieId && like.UserId == currentUser.UserId);


            _context.Remove(likeToDelete);

            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }


        [HttpPost("movies/{movieId}/likes")]
        public IActionResult AddLike(int movieId)
        {
            var likeToAdd = new Like {
                UserId = GetCurrentUser().UserId,
                MovieId = movieId
            };

            _context.Add(likeToAdd);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }


        [HttpGet("movies/new")]
        public IActionResult NewMoviePage()
        {
            var currentUser = GetCurrentUser();

            if(currentUser==null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.CurrentUser = currentUser;
            return View();
        }

        [HttpPost("movies")]
        public IActionResult CreateMovie(Movie movieFromForm)
        {   if(movieFromForm.ReleaseDate > DateTime.Now)

            {
                ModelState.AddModelError("ReleaseDate", "Please ensure that the date is in the past!");

            }


            if(ModelState.IsValid)
            {
                movieFromForm.UserId = (int)HttpContext.Session.GetInt32("UserId");                
                _context.Add(movieFromForm);
                _context.SaveChanges();


                return Redirect($"/movies/{movieFromForm.MovieId}");
            }

            return View("NewMoviePage");

        }


        [HttpGet("movies/{movieId}")]

        public IActionResult SingleMoviePage(int movieId)
        {
            var currentUser = GetCurrentUser();

            if(currentUser==null)
            {
                return RedirectToAction("Index");
            }


            //////////
            ViewBag.Movie = _context
                .Movies
                .Include(m => m.PostedBy)
                .Include(m => m.Likes)// pull all the likes
                    .ThenInclude(like => like.UserWhoLikes)//within each
                .First(m => m.MovieId == movieId);

                ViewBag.CurrentUser = currentUser;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        // }
    }
}
