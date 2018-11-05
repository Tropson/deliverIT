using DeliveryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;


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
            return View();
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
                    return View("Create", app);
                _proxy.AddApplication(new DeliveryService.ApplicationModel { Address = app.Address, City = app.City, Cpr = app.Cpr, Email = app.Email, FirstName = app.FirstName, LastName = app.LastName, PhoneNumber = app.PhoneNumber,  ZipCode = app.ZipCode, CVPath=app.CVPath,IDPicturePath=app.IDPicturePath,YellowCardPath=app.YellowCardPath});
                return RedirectToAction("Index");
            }
            catch
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
    }
}
