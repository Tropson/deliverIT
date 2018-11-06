using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebClientMVC.SenderServiceReference;
using System.Web.Mvc;
using Limilabs.FTP.Client;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Mail;

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
                Ftp client = new Ftp();
                client.Connect("files.000webhost.com");
                client.Login("tropson", "GTAvcsa345");
                client.CreateFolder("public_html/Files/" + guid);
                client.Upload($"public_html/Files/{guid}/{cv}", app.files[0].InputStream);
                client.Upload($"public_html/Files/{guid}/{idpic}", app.files[1].InputStream);
                client.Upload($"public_html/Files/{guid}/{yellow}", app.files[2].InputStream);
                _proxy.AddApplication(new DeliveryService.ApplicationModel { Address = app.Address, City = app.City, Cpr = app.Cpr, Email = app.Email, FirstName = app.FirstName, LastName = app.LastName, PhoneNumber = app.PhoneNumber, ZipCode = app.ZipCode, CVPath = cv, IDPicturePath = idpic, YellowCardPath = yellow, GuidLine = guid });

                MailMessage mail = new MailMessage("piotr.gzubicki97@gmail.com", app.Email);
                SmtpClient mailClient = new SmtpClient();
                mailClient.Port = 587;
                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mailClient.Credentials = new NetworkCredential("tropson90@gmail.com", "gtavcsa45");
                mailClient.Host = "smtp.gmail.com";
                mailClient.EnableSsl = true;
                mail.Subject = "Your application has been sent!";
                mail.Body = "We got your application. Wait for the admin to accpet you.";
                
                mailClient.Send(mail);

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
