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
using FluentFTP;
using System.Net.Mail;
using System.IO;

namespace WebClientMVC.Controllers
{
    public class AdminController : Controller
    {

        public readonly ISenderService _proxy;
        public FtpClient client;
        
        public AdminController(ISenderService proxy)
        {
            this._proxy = proxy;
            client = new FtpClient("files.000webhost.com");
            client.Credentials = new NetworkCredential("tropson", "GTAvcsa345");
        }
        // GET: Admin
        //TODO : NEED TO BE REVISED BECAUSE OF THE HTTTP FILES LIST 
        public ActionResult Index()
        {
            client.Connect();
            DeliveryService.ApplicationModel[] list = _proxy.GetAllApplications();
            IEnumerable<Models.ApplicationModel> applications = list.Select(x => new Models.ApplicationModel { Cpr = x.Cpr, FirstName = x.FirstName, LastName = x.LastName, PhoneNumber = x.PhoneNumber, Email = x.Email, Address = x.Address, ZipCode = x.ZipCode, City = x.City,cv=x.CVPath,idcard=x.IDPicturePath,yellow=x.YellowCardPath,GuidLine=x.GuidLine, files = new HttpPostedFileBase[3] });
            return View(applications);
        }
        public ActionResult Download(string guid,string file)
        {


            //string path = $"public_html/Files/{guid}/{file}";
            string path = Server.MapPath($"/Download/{guid}/{file}");
            System.IO.MemoryStream mem = new System.IO.MemoryStream();
            //client.Download(mem, path);
            new System.IO.FileStream(path, FileMode.Open, FileAccess.Read).CopyTo(mem);
            byte[] fileBytes = new byte[mem.Length];
            fileBytes = mem.ToArray();
            string fileName = file;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
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
        
        public ActionResult CreateOnAccept(string Cpr)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("Index");
                var app = _proxy.GetAllApplications().SingleOrDefault(x => x.Cpr == Cpr);
                string generPassword = Membership.GeneratePassword(10, 0);
                SenderModel courier = new SenderModel(app.Cpr, app.FirstName, app.LastName, app.PhoneNumber, app.Email, app.Address, app.ZipCode, app.City) { AccountType = (int)AccountTypeEnum.COURIER, Points = 0 };
                _proxy.AddCourier(new DeliveryService.UserModel { AccountType = courier.AccountType, Address = courier.Address, City = courier.City, ZipCode = courier.ZipCode, Cpr = courier.Cpr, Email = courier.Email, FirstName = courier.FirstName, LastName = courier.LastName, PhoneNumber = courier.PhoneNumber, Points = courier.Points, Username = courier.Email, Password = generPassword });
                DeliveryService.ApplicationModel appToDelete = new DeliveryService.ApplicationModel { Cpr = app.Cpr };
                _proxy.DeleteApplication(appToDelete,false);
                //client.Disconnect();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult DeleteOnDecline(Models.ApplicationModel app)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("Index");
                DeliveryService.ApplicationModel appToDelete = new DeliveryService.ApplicationModel { Cpr = app.Cpr };
                _proxy.DeleteApplication(appToDelete,true);
                //client.Disconnect();
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
