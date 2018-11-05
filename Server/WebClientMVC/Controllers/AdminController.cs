using WebClientMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeliveryService;

namespace WebClientMVC.Controllers
{
    public class AdminController : Controller
    {

        private readonly ISenderService _proxy;

        public AdminController(ISenderService proxy)
        {
            this._proxy = proxy;
        }
        // GET: Admin
        //TODO : NEED TO BE REVISED BECAUSE OF THE HTTTP FILES LIST 
        public ActionResult Index()
        {
            List<DeliveryService.ApplicationModel> list = _proxy.GetApplications();

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
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
