﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebClientMVC.Models;
using WebClientMVC.SenderServiceReference1;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace WebClientMVC.Controllers
{
    public class SenderController : Controller
    {
        public readonly ISenderService _proxy;

        public SenderController(ISenderService proxy)
        {
            this._proxy = proxy;
        }

        // GET: Sender
        public ActionResult Index()
        {
            if (Request.Cookies.Get("login") == null)
            {
                return View();
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
        [HttpPost]
        public ActionResult Index(LoginModel user)
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
                    return RedirectToAction("LoggedInPost", userToPass);
                }
                else
                {
                    return View(user);
                }
            }
            else return View(user);
        }

        // GET: Sender/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sender/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sender/Create
        [HttpPost]
        public ActionResult Create(RegisterModel reg)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("Create", reg);
                SenderModel sender = new SenderModel(reg.Cpr, reg.FirstName, reg.LastName, reg.PhoneNumber, reg.Email, reg.Address, reg.ZipCode, reg.City) { AccountType = (int)AccountTypeEnum.SENDER, Password = reg.Password, Username = reg.Username, Points = 0 };
                _proxy.AddSender(new DeliveryService.UserModel { AccountType = sender.AccountType, Address = sender.Address, City = sender.City, Cpr = sender.Cpr, Email = sender.Email, FirstName = sender.FirstName, LastName = sender.LastName, Password = sender.Password, PhoneNumber = sender.PhoneNumber, Points = sender.Points, Username = sender.Username, ZipCode = sender.ZipCode });
                return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // GET: Sender/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        public ActionResult Logout()
        {
            try
            {
                HttpCookie cookie = new HttpCookie("login");
                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
        // POST: Sender/Edit/5
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

        // GET: Sender/Delete/5
        public ActionResult Delete(int id)
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

        // POST: Sender/Delete/5
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
    }
}
