using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebClientMVC.SenderServiceReference1;
using WebClientMVC.Models;
using System.Security.Cryptography;
using System.Text;

namespace WebClientMVC.Controllers
{
    public class HomeController : Controller
    {
        public readonly ISenderService _proxy;

        public HomeController(ISenderService proxy)
        {
            _proxy = proxy;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            if (Request.Cookies.Get("login") == null)
            {
                return View();
            }
            else
            {
                string userName = Request.Cookies.Get("login").Values["feketePorzeczka"];
                int userType = _proxy.GetAllUsers().SingleOrDefault(x => x.Username == userName).AccountType;
                LoginPassModel obj = new LoginPassModel { Username = userName };
                if (userType == 1)
                {
                    return RedirectToAction("LoggedInPost", "Sender", obj);
                }
                else if (userType == 2)
                {
                    return RedirectToAction("LoggedInPost", "Application", obj);
                }
                else return View();
            }
        }
        [HttpPost]
        public ActionResult Login(LoginModel user)
        {
            if (!ModelState.IsValid)
                return View(user);
            var userFromDB = _proxy.GetAllUsers().SingleOrDefault(x => x.Username == user.Username);
            if (userFromDB == null)
            {
                userFromDB = _proxy.GetAllUsers().SingleOrDefault(x => x.Email == user.Username);
            }
            if (userFromDB == null)
            {
                return View(user);
            }
            string hash = HashString(user.Password);
            if (userFromDB.Password == user.Password)
            {
                LoginPassModel userToPass = new LoginPassModel { Username = userFromDB.Username };
                if (userFromDB != null)
                {
                    HttpCookie cookie = new HttpCookie("login");
                    cookie.Values.Add("feketePorzeczka", userToPass.Username);
                    cookie.Values.Add("pirosPorzeczka", hash);
                    cookie.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(cookie);
                    int userType = _proxy.GetAllUsers().SingleOrDefault(x => x.Username == userToPass.Username).AccountType;
                    if (userType == 1)
                    {
                        return RedirectToAction("LoggedInPost", "Sender", userToPass);
                    }
                    else if (userType == 2)
                    {
                        return RedirectToAction("LoggedInPost", "Application", userToPass);
                    }
                    else return View();
                }
                else
                {
                    return View(user);
                }
            }
            else
            {
                ModelState.AddModelError("Password", "Wrong password");
                return View(user);
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        public string HashString(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)

            {

                sb.Append(hash[i].ToString("x2"));

            }

            return sb.ToString();
        }

    }
}