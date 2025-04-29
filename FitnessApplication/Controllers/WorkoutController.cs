using FitnessApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims; 

namespace FitnessApplication.Controllers
{
    [Authorize]
    public class WorkoutController : Controller
    {
        private readonly FitnessContext _context;
        private readonly ILogger<WorkoutController> _logger;

        public WorkoutController(FitnessContext context, ILogger<WorkoutController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);           
            var workouts = _context.Workouts.ToList();
            var favoriteWorkoutIds = _context.UserFavoriteWorkouts
                                            .Where(fav => fav.UserId == userId)
                                            .Select(fav => fav.WorkoutId)
                                            .ToList();


            ViewBag.FavoriteWorkouts = favoriteWorkoutIds;
       
            return View(workouts);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
           public IActionResult Create(Workout workout)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(workout.VideoUrl))
                {
                    string videoId = ExtractYouTubeVideoId(workout.VideoUrl);
                    if (!string.IsNullOrEmpty(videoId))
                    {
                        // uses the video ID to generate a thumbnail image URL
                        workout.ThumbnailUrl = $"https://img.youtube.com/vi/{videoId}/0.jpg";
                    }
                }

                _context.Workouts.Add(workout);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workout);
        }


        private string ExtractYouTubeVideoId(string url)
        {
            try
            {
                Uri uri;
                //checks if the URL is valid
                if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
                    return null;

                if (uri.Host.Contains("youtube.com"))
                {
                    var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
                    string videoId = query["v"];
                    return videoId;
                }

                if (uri.Host.Contains("youtu.be"))
                {
                    string videoId = uri.AbsolutePath.Substring(1);
                    return videoId;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public IActionResult Gallery(string searchString, string workoutType)
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);
            var workoutsQuery = _context.Workouts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                workoutsQuery = workoutsQuery.Where(w => w.WorkoutName.Contains(searchString) || w.Description.Contains(searchString));
            }
            if(!string.IsNullOrEmpty(workoutType) && workoutType != "All")
            {
                workoutsQuery = workoutsQuery.Where(w => w.Category == workoutType); 
            }

            var workouts = workoutsQuery.ToList();
            var favoriteWorkoutIds = _context.UserFavoriteWorkouts
                                             .Where(fav => fav.UserId == userId)
                                             .Select(fav => fav.WorkoutId)
                                             .ToList();
            var workoutTypes = _context.Workouts
                .Select(w => w.Category)
                .Distinct()
                .ToList();


            ViewBag.FavoriteWorkouts = favoriteWorkoutIds;
            ViewBag.WorkoutTypes = workoutTypes;
      
            return View(workouts);
        }




        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var workout = _context.Workouts.Find(id);
            if (workout == null)
            {
                return NotFound();
            }
            return View(workout);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Workout workout)
        {
            if (ModelState.IsValid)
            {
                var existingWorkout = _context.Workouts.Find(workout.WorkoutId);
                if (existingWorkout == null)
                {
                    return NotFound();
                }

                existingWorkout.WorkoutName = workout.WorkoutName;
                existingWorkout.Category = workout.Category;
                existingWorkout.Duration = workout.Duration;
                existingWorkout.VideoUrl = workout.VideoUrl;

                if (!string.IsNullOrEmpty(workout.VideoUrl))
                {
                    string videoId = ExtractYouTubeVideoId(workout.VideoUrl);
                    if (!string.IsNullOrEmpty(videoId))
                    {
                        existingWorkout.ThumbnailUrl = $"https://img.youtube.com/vi/{videoId}/0.jpg";
                    }
                    else
                    {
                        existingWorkout.ThumbnailUrl = null;
                    }
                }
                else
                {
                    existingWorkout.ThumbnailUrl = null;
                }

                //_context.Workouts.Update(workout);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workout);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var workout = _context.Workouts.Find(id);
            if (workout == null)
            {
                return NotFound();
            }
            return View(workout);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var workout = _context.Workouts.Find(id);
            if (workout != null)
            {
                _context.Workouts.Remove(workout);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        [Authorize]
        public async Task<IActionResult> MyFavorites()
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
            {
                return RedirectToAction("Login", "Account");
            }
            int userId = int.Parse(userIdClaim.Value);
            var favoriteWorkouts = await _context.UserFavoriteWorkouts
                .Where(f => f.UserId == userId)
                .Include(f => f.Workout)
                .Select(f => f.Workout)
                .ToListAsync();
            return View(favoriteWorkouts);
        }
       
        [HttpPost]
        public IActionResult AddToFavorites(int workoutId)
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);

            var favorite = _context.UserFavoriteWorkouts
                                   .FirstOrDefault(f => f.UserId == userId && f.WorkoutId == workoutId);

            if (favorite == null)
            {
                _context.UserFavoriteWorkouts.Add(new UserFavoriteWorkout
                {
                    UserId = userId,
                    WorkoutId = workoutId
                });
                _context.SaveChanges();
            }

            return RedirectToAction("Gallery");
        }

        [HttpPost]
        public IActionResult RemoveFromFavorites(int workoutId, string returnUrl = null)
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);

            var favorite = _context.UserFavoriteWorkouts
                                   .FirstOrDefault(f => f.UserId == userId && f.WorkoutId == workoutId);

            if (favorite != null)
            {
                _context.UserFavoriteWorkouts.Remove(favorite);
                _context.SaveChanges();
            }
            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Gallery");
        }
    }
}