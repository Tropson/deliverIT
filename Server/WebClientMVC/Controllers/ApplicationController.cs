using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebClientMVC.SenderServiceReference1;
using System.Web.Mvc;
using System.Net;
using System.IO;
using WebClientMVC.Models;
using System.Text;
using System.Net.Mail;
using FluentFTP;
using System.Security.Cryptography;

namespace WebClientMVC.Controllers
{
    public class ApplicationController : Controller
    {
        public readonly ISenderService _proxy;

        public ApplicationController(ISenderService proxy)
        {
            this._proxy = proxy;
        }
        // GET: Application
        public ActionResult Index()
        {
            if (Request.Cookies.Get("login") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string userName = Request.Cookies.Get("login").Values["feketePorzeczka"];
                LoginPassModel obj = new LoginPassModel { Username = userName };
                return RedirectToAction("LoggedInPost", obj);
            }
        }

        public ActionResult LoggedInPost(LoginPassModel user)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");
            var userFromDB = _proxy.GetAllUsers().SingleOrDefault(x => x.Username == user.Username);
            Models.SenderModel userToPass = new Models.SenderModel(userFromDB.Cpr, userFromDB.FirstName, userFromDB.LastName, userFromDB.PhoneNumber, userFromDB.Email, userFromDB.Address, userFromDB.ZipCode, userFromDB.City)
            {
                Username = userFromDB.Username,
                Password = userFromDB.Password,
                Points = userFromDB.Points,
                AccountType = userFromDB.AccountType
            };
            if (Request.Cookies.Get("login") != null)
            {
                if (HashString(userToPass.Password) == Request.Cookies.Get("login").Values["pirosPorzeczka"])
                {
                    return View("LoggedIn", userToPass);
                }
                else return RedirectToAction("Index");
            }
            else return RedirectToAction("Index");
        }
        public ActionResult Logout()
        {
            try
            {
                HttpCookie cookie = new HttpCookie("login");
                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
        // GET: Application/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Application/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Application/Create
        [HttpPost]
        public ActionResult Create(Models.ApplicationModel app)
        {

            try
            {
                if (!ModelState.IsValid)
                    return View("Create",app);
                string cv = app.files[0].FileName;
                string idpic = app.files[1].FileName;
                string yellow = app.files[2].FileName;
                var guid = Guid.NewGuid().ToString();
                //FtpClient client = new FtpClient("files.000webhost.com");
                //client.Credentials = new NetworkCredential("tropson", "GTAvcsa345");
                //client.Connect();
                string path = Server.MapPath("~/Download");
                Directory.CreateDirectory(path + $"/{guid}");
                //client.CreateDirectory($"public_html/Files/{guid}");
                foreach (var a in app.files)
                {
                    FileStream fs = System.IO.File.Create(path + $"/{guid}/{a.FileName}");
                    a.InputStream.CopyTo(fs);
                    fs.Close();
                }
                //client.Upload(app.files[0].InputStream, $"public_html/Files/{guid}/{cv}");
                //client.Upload(app.files[1].InputStream, $"public_html/Files/{guid}/{idpic}");
                //client.Upload(app.files[2].InputStream, $"public_html/Files/{guid}/{yellow}");
                var result = _proxy.AddApplication(new ApplicationResource { Address = app.Address, City = app.City, Cpr = app.Cpr, Email = app.Email, FirstName = app.FirstName, LastName = app.LastName, PhoneNumber = app.PhoneNumber, ZipCode = app.ZipCode, CVPath = cv, IDPicturePath = idpic, YellowCardPath = yellow, GuidLine = guid });
                //client.Disconnect();
                if (result == 1)
                {
                    return RedirectToAction("Index");
                }

                else return RedirectToAction("Create", app);

                
            }
            catch(Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // GET: Application/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Application/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Application/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Application/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Deliveries()
        {
            var packages = _proxy.GetAllPackages().Select(x=>new Models.PackageModel { FromAddress=x.FromAddress,ToAddress=x.ToAddress,Height=x.Height,Weight=x.Weight,Width=x.Width,Price=_proxy.GetDeliveryByPackageBarcode(x.barcode).Price+"",Distance=_proxy.GetDeliveryByPackageBarcode(x.barcode).Distance + "",Barcode=x.barcode,SenderID=x.SenderID,CourierID=(x.CourierID==null)?0:(int)x.CourierID }).ToList();
            return View(packages);
        }

        public ActionResult TakePackage(PackagePassModel packageToTake)
        {
            var result = _proxy.TakePackage(packageToTake.Barcode, packageToTake.CourierID);
            if (result == 1)
            {
                return RedirectToAction("Deliveries");
            }
            else if (result == 0)
            {
                var packages = _proxy.GetAllPackages().Select(x => new Models.PackageModel { FromAddress = x.FromAddress, ToAddress = x.ToAddress, Height = x.Height, Weight = x.Weight, Width = x.Width, Price = _proxy.GetDeliveryByPackageBarcode(x.barcode).Price + "", Distance = _proxy.GetDeliveryByPackageBarcode(x.barcode).Distance + "", Barcode = x.barcode, SenderID = x.SenderID, CourierID = (x.CourierID == null) ? 0 : (int)x.CourierID }).ToList();
                ModelState.AddModelError(string.Empty, "The package you have selected was already taken by the moment.");
                return View("Deliveries",packages);
            }
            else return RedirectToAction("Deliveries");
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
    public static class Methods
    {
        
    }
}
