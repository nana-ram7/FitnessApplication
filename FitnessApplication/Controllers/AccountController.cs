using System.Security.Claims;
using FitnessApplication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly FitnessContext _context;
        public AccountController(FitnessContext fitnessContext)
        {
            _context = fitnessContext;
        }


        public IActionResult Index()
        {
            return View(_context.Users.ToList());
        }



        public IActionResult LogIn()
        {
            return View();
        }


        [HttpPost]
        public IActionResult LogIn(LogIn model)
        {
            //checks if form data is valid
            if (ModelState.IsValid)
            {
                var user = _context.Users.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();
                if (user != null)
                {
                    //Success, create a cookie if user is found
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),   //save the users email
                        new Claim("UserId", user.UserId.ToString()), // saves the user ID
                        new Claim("Name", user.FirstName), // save the users first name
                        new Claim(ClaimTypes.Role, "User"), //set default role as user
                        new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" :"User") //gives admin role
                    };
                    // creates identity using details
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    // sign the user in and store their infor in a cookie
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("SecurePage");
                }
                else
                {
                    //if user wasn't found show an error message 
                    ModelState.AddModelError("", "Email or Password is not correct");
                }
            }
            return View(model);
        }


        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        //a page that only users that are logged in can see
        [Authorize]
        public IActionResult SecurePage()
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
            {
                return RedirectToAction("LogIn"); 
            }

            int userId = int.Parse(userIdClaim.Value);
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound();
            }

            if (user.Weight != null && user.HeightFeet != null && user.HeightInches != null && !string.IsNullOrEmpty(user.FitnessGoal))
            {
                ViewBag.ProfileComplete = true;
            }
            else
            {
                ViewBag.ProfileComplete = false;
            }
            return View(user);
        }


        public IActionResult SignUp()
        {
            return View();
        }
        

        [HttpPost]
        public IActionResult SignUp(SignUp model)
        {
            if (ModelState.IsValid)
            {
                //create a new user account with the data recieved 
                User account = new User();
                account.Email = model.Email;
                account.FirstName = model.FirstName;
                account.LastName = model.LastName;
                account.Password = model.Password;

                try
                {
                    _context.Users.Add(account);
                    _context.SaveChanges();
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, account.Email),
                        new Claim("UserId", account.UserId.ToString()), 
                        new Claim("Name", account.FirstName),
                        new Claim(ClaimTypes.Role, "User"),
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("SecurePage"); 
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.InnerException?.Message ?? ex.Message);
                    return View(model);

                }
              
            }
            return View(model);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CompleteProfile(User updatedUser, IFormFile ProfilePicture )
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
            {
                return RedirectToAction("LogIn");
            }

            int userId = int.Parse(userIdClaim.Value);
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound();
            }

            user.Gender = updatedUser.Gender;
            user.HeightFeet = updatedUser.HeightFeet;
            user.HeightInches = updatedUser.HeightInches;
            user.Weight = updatedUser.Weight;
            user.FitnessGoal = updatedUser.FitnessGoal;

            if(ProfilePicture != null && ProfilePicture.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/uploads");
                if(!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfilePicture.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using(var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfilePicture.CopyToAsync(stream);
                }
                user.ProfilePicture = uniqueFileName; 
            }

            _context.SaveChanges();

            return RedirectToAction("SecurePage");   
        }
       
    }
}
