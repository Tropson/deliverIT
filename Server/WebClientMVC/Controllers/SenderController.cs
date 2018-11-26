using System;
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
                int ret = _proxy.AddSender(new DeliveryService.UserModel { AccountType = sender.AccountType, Address = sender.Address, City = sender.City, Cpr = sender.Cpr, Email = sender.Email, FirstName = sender.FirstName, LastName = sender.LastName, Password = sender.Password, PhoneNumber = sender.PhoneNumber, Points = sender.Points, Username = sender.Username, ZipCode = sender.ZipCode });
                switch (ret)
                {
                    case 1: return RedirectToAction("Index");
                    case 0: return View("Create", reg);
                    case -1:
                    {
                         ModelState.AddModelError("Cpr", "This CPR is already registered");
                         return View("Create", reg);
                    }
                    case -2:
                    {
                         ModelState.AddModelError("Email", "This email is already registered");
                         return View("Create", reg);
                    }
                    case -3:
                    {
                         ModelState.AddModelError("Username", "This username is taken");
                         return View("Create", reg);
                    }
                       
                }
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
                return RedirectToAction("Index","Home");
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

        public ActionResult Points()
        {
            return View();
        }

        public ActionResult Voucher()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Voucher(VoucherModel voucherm)
        {
            try
            {
                int status = 0;
                string username = Request.Cookies.Get("login").Values["feketePorzeczka"];
                var usedVouchers = _proxy.GetAllUsedVouchers();
                var voucher = _proxy.GetAllVouchers().Select(x => new VoucherModel { amount = x.amount, code = x.code }).SingleOrDefault(x => x.code == voucherm.code);
                if (voucher != null)
                {
                    foreach (var a in usedVouchers)
                    {
                        if (a.code == voucherm.code && a.username == username)
                        {
                            status = 1;
                        }
                    }
                    if (status == 0)
                    {
                        _proxy.UseVoucher(username, voucherm.code);
                    }
                    else
                    {
                        ModelState.AddModelError("code", "This voucher was already used.");
                        return View("Voucher", voucherm);
                    }
                }
                else
                {
                    ModelState.AddModelError("code", "This voucher doesn't exist");
                    return View("Voucher", voucherm);
                }
                return RedirectToAction("Index");
            }
            catch {
                return View();
            }
        }
    }
}
