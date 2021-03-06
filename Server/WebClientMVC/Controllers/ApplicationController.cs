﻿using System;
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
                PassSalt=userFromDB.PassSalt,
                AccountType = userFromDB.AccountType
            };
            if (Request.Cookies.Get("login") != null)
            {
                if (userFromDB.Password == Request.Cookies.Get("login").Values["pirosPorzeczka"])
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
        public ActionResult DeliveredPackages(LoginPassModel user)
        {
            var userFromDB = _proxy.GetAllUsers().SingleOrDefault(x => x.Username == user.Username);
            var packages = _proxy.GetAllPackages().Where(x => x.CourierID == userFromDB.IDInDB).Select(x => new WebClientMVC.Models.PackageModel
            {
                Barcode = x.barcode,
                Distance = _proxy.GetDeliveryByPackageBarcode(x.barcode).Distance.ToString(),
                Price = _proxy.GetDeliveryByPackageBarcode(x.barcode).Price.ToString(),
                ReceiverFirstName = x.ReceiverFirstName,
                ReceiverLastName = x.ReceiverLastName,
                StatusID = x.StatusID
            });
            return View(packages);
        }
        
        public ActionResult ChangeStatus(PackagePassModel package)
        {
            _proxy.ChangeStatus(package.Barcode);
            return RedirectToAction("DeliveredPackages",new LoginPassModel() { Username = package.Username });
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
                FtpClient client = new FtpClient("deliverit.westeurope.cloudapp.azure.com");
                client.Connect();
                client.CreateDirectory($"public_html/Files/{guid}");
                /*foreach (var a in app.files)
                {
                    FileStream fs = System.IO.File.Create(path + $"/{guid}/{a.FileName}");
                    a.InputStream.CopyTo(fs);
                    fs.Close();
                }*/
                client.Upload(app.files[0].InputStream, $"public_html/Files/{guid}/{cv}");
                client.Upload(app.files[1].InputStream, $"public_html/Files/{guid}/{idpic}");
                client.Upload(app.files[2].InputStream, $"public_html/Files/{guid}/{yellow}");
                var result = _proxy.AddApplication(new ApplicationResource { Address = app.Address, City = app.City, Cpr = app.Cpr, Email = app.Email, FirstName = app.FirstName, LastName = app.LastName, PhoneNumber = app.PhoneNumber, ZipCode = app.ZipCode, CVPath = cv, IDPicturePath = idpic, YellowCardPath = yellow, GuidLine = guid });
                client.Disconnect();
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
            var packages = _proxy.GetAllPackages().Select(x=>new Models.PackageModel { FromAddress=x.FromAddress,ToAddress=x.ToAddress,Height=x.Height,Weight=x.Weight,Width=x.Width,Price=_proxy.GetDeliveryByPackageBarcode(x.barcode).Price+"",Distance=_proxy.GetDeliveryByPackageBarcode(x.barcode).Distance + "",Barcode=x.barcode,SenderID=x.SenderID,CourierID=(x.CourierID==null)?0:(int)x.CourierID,ReceiverFirstName=x.ReceiverFirstName,ReceiverLastName=x.ReceiverLastName}).ToList();
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

    }
    public static class Methods
    {
        
    }
}
