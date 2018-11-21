using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebClientMVC.SenderServiceReference1;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Mail;
using FluentFTP;


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
                _proxy.AddApplication(new DeliveryService.ApplicationModel { Address = app.Address, City = app.City, Cpr = app.Cpr, Email = app.Email, FirstName = app.FirstName, LastName = app.LastName, PhoneNumber = app.PhoneNumber, ZipCode = app.ZipCode, CVPath = cv, IDPicturePath = idpic, YellowCardPath = yellow, GuidLine = guid });
                //client.Disconnect();
                return RedirectToAction("Create");
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
       
    }
    public static class Methods
    {
        
    }
}
