using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebClientMVC.Models;
using WebClientMVC.ServiceReference;

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
            return View();
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
        public ActionResult Create(Models.SenderModel sender)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("Index");

                _proxy.AddSender(sender);

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
