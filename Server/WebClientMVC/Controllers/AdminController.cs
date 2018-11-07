using WebClientMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebClientMVC.SenderServiceReference1;
using AccountTypeEnum = WebClientMVC.Models.AccountTypeEnum;
using System.Web.Security;
using System.Net;
using System.Net.Mail;

namespace WebClientMVC.Controllers
{
    public class AdminController : Controller
    {

        public readonly ISenderService _proxy;

        public AdminController(ISenderService proxy)
        {
            this._proxy = proxy;
        }
        // GET: Admin
        //TODO : NEED TO BE REVISED BECAUSE OF THE HTTTP FILES LIST 
        public ActionResult Index()
        {
            DeliveryService.ApplicationModel[] list = _proxy.GetAllApplications();
            IEnumerable<Models.ApplicationModel> applications = list.Select(x => new Models.ApplicationModel { Cpr = x.Cpr, FirstName = x.FirstName, LastName = x.LastName, PhoneNumber = x.PhoneNumber, Email = x.Email, Address = x.Address, ZipCode = x.ZipCode, City = x.City, files = new HttpPostedFileBase[3] });
            return View(applications);
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult CreateOnAccept(Models.ApplicationModel app)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("Create", app);

                string generPassword = Membership.GeneratePassword(6, 2);
                SenderModel courier = new SenderModel(app.Cpr, app.FirstName, app.LastName, app.PhoneNumber, app.Email, app.Address, app.ZipCode, app.City) { AccountType = (int)AccountTypeEnum.COURIER, Points = 0 };
                _proxy.AddCourier(new DeliveryService.UserModel {AccountType= courier.AccountType, Address=courier.Address,City=courier.City,ZipCode=courier.ZipCode,Cpr=courier.Cpr,Email=courier.Email,FirstName=courier.FirstName,LastName=courier.LastName,PhoneNumber=courier.PhoneNumber,Points=courier.Points,Username=courier.Email, Password=generPassword});


                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult DelteWhenDecline(Models.ApplicationModel app)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("Create", app);

                

                return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
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

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
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
