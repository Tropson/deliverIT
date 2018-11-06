using WebClientMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeliveryService;
using Limilabs.FTP.Client;
using System.IO;

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
            List<Models.ApplicationModel> list = _proxy.GetApplications().Select(x => new Models.ApplicationModel {
                Address = x.Address,
                City = x.City,
                Cpr = x.Cpr,
                Email = x.Email,
                GuidLine = x.GuidLine,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                ZipCode = x.ZipCode,
                files = new HttpPostedFileBase[3]
            }).ToList();
            Ftp client = new Ftp();
            client.Connect("files.000webhost.com");
            client.Login("tropson", "GTAvcsa345");
            foreach (var a in list)
            {
                var guid = a.GuidLine;
                string cvName = _proxy.GetApplications().SingleOrDefault(x => x.Cpr == a.Cpr).CVPath;
                string idName = _proxy.GetApplications().SingleOrDefault(x => x.Cpr == a.Cpr).IDPicturePath;
                string yellowName = _proxy.GetApplications().SingleOrDefault(x => x.Cpr == a.Cpr).YellowCardPath;
                if (client.FolderExists($"/public_html/Files/{guid}"))
                {
                    byte[] streamCv = client.Download($"/public_html/Files/{guid}/{cvName}");
                    if (streamCv.Length == 0)
                    {
                        streamCv = client.Download($"/public_html/Files/{guid}/FtpTrial-{cvName}");
                    }
                    a.files[0] = new MemoryPostedFile(streamCv,cvName);
                    byte[] streamID = client.Download($"/public_html/Files/{guid}/{idName}");
                    if (streamID.Length == 0)
                    {
                        streamID = client.Download($"/public_html/Files/{guid}/FtpTrial-{cvName}");
                    }
                    a.files[1] = new MemoryPostedFile(streamID,cvName);
                    byte[] streamYellow = client.Download($"/public_html/Files/{guid}/{yellowName}");
                    if (streamID.Length == 0)
                    {
                        streamID = client.Download($"/public_html/Files/{guid}/FtpTrial-{cvName}");
                    }
                    a.files[2] = new MemoryPostedFile(streamYellow,cvName);
                }
            }
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

                string cv = app.files[0].FileName;
                string idpic = app.files[1].FileName;
                string yellow = app.files[2].FileName;
                _proxy.AddCourier();
                    //new DeliveryService.ApplicationModel { Address = app.Address, City = app.City, Cpr = app.Cpr, Email = app.Email, FirstName = app.FirstName, LastName = app.LastName, PhoneNumber = app.PhoneNumber, ZipCode = app.ZipCode, CVPath = cv, IDPicturePath = idpic, YellowCardPath = yellow });

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
        public class MemoryPostedFile : HttpPostedFileBase
        {
            private readonly byte[] fileBytes;

            public MemoryPostedFile(byte[] fileBytes, string fileName)
            {
                this.fileBytes = fileBytes;
                this.FileName = fileName;
                this.InputStream = new MemoryStream(fileBytes);
            }

            public override int ContentLength => fileBytes.Length;

            public override string FileName { get; }

            public override Stream InputStream { get; }
        }
    }
}
