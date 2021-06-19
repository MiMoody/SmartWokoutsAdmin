using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FirstApplication.Models;

namespace FirstApplication.Controllers
{
    public class Premium_WorksController : Controller
    {
        private SmartWorkouts_newEntities db = new SmartWorkouts_newEntities();

        public string getFileExtension(string fileName) // Получение типа фотографии
        {
            return fileName.Substring(fileName.LastIndexOf(".") + 1);
        }
        // GET: Premium_Works
        public ActionResult Index()
        {
            return View(db.Premium_Works.ToList());
        }

        public ActionResult CreatePdf()
        {
            string filePath = Server.MapPath("~/Content/1.pdf");
            string contentType = "application/pdf";

            ////Parameters to file are
            ////1. The File Path on the File Server
            ////2. The content type MIME type
            ////3. The parameter for the file save by the browser
            //var a = File(filePath, contentType, "Report.pdf");
            return null;
        }

        // GET: Premium_Works/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Premium_Works premium_Works = db.Premium_Works.Find(id);
            if (premium_Works == null)
            {
                return HttpNotFound();
            }
            return View(premium_Works);
        }

        // GET: Premium_Works/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Premium_Works/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Premium_Works premium_Works, HttpPostedFileBase Pic)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;
                if (Pic != null)
                {
                    fileName = Path.GetFileName(Pic.FileName);
                    string extensionImage = getFileExtension(fileName);
                    fileName = Guid.NewGuid() + "." + extensionImage;
                    var path = Path.Combine(Server.MapPath("~/Content/Images/PremiumWorks"), fileName);
                    //var path = Path.Combine("https://disk.yandex.ru/d/DXlvv0vSsvgswQ?w=1", fileName);
                    Pic.SaveAs(path);
                }
                premium_Works.PicturePath = fileName;
                db.Premium_Works.Add(premium_Works);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(premium_Works);
        }

        // GET: Premium_Works/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Premium_Works premium_Works = db.Premium_Works.Find(id);
            if (premium_Works == null)
            {
                return HttpNotFound();
            }
            return View(premium_Works);
        }

        // POST: Premium_Works/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Premium_Works premium_Works, HttpPostedFileBase Pic)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;
                if (Pic != null)
                {
                    fileName = Path.GetFileName(Pic.FileName);
                    string extensionImage = getFileExtension(fileName);
                    fileName = Guid.NewGuid() + "." + extensionImage;
                    var path = Path.Combine(Server.MapPath("~/Content/Images/PremiumWorks"), fileName);
                    //var path = Path.Combine("https://disk.yandex.ru/d/DXlvv0vSsvgswQ?w=1", fileName);
                    Pic.SaveAs(path);
                    premium_Works.PicturePath = fileName;
                }
                else
                {
                    using (SmartWorkouts_newEntities bd = new SmartWorkouts_newEntities())
                    {
                        premium_Works.PicturePath = bd.Premium_Works.Where(p => p.Number_Premium_Work == premium_Works.Number_Premium_Work).FirstOrDefault().PicturePath;
                    }
                }
                db.Entry(premium_Works).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(premium_Works);
        }

        // GET: Premium_Works/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Premium_Works premium_Works = db.Premium_Works.Find(id);
            if (premium_Works == null)
            {
                return HttpNotFound();
            }
            return View(premium_Works);
        }

        // POST: Premium_Works/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Premium_Works premium_Works = db.Premium_Works.Find(id);
            db.Premium_Works.Remove(premium_Works);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
