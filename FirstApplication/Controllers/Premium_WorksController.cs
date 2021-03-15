using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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

        // GET: Premium_Works
        public ActionResult Index()
        {
            return View(db.Premium_Works.ToList());
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
        public ActionResult Create([Bind(Include = "Number_Premium_Work,Name_Premium_Work,Description_Premium_Work,Price_Premium_Work")] Premium_Works premium_Works)
        {
            if (ModelState.IsValid)
            {
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
        public ActionResult Edit([Bind(Include = "Number_Premium_Work,Name_Premium_Work,Description_Premium_Work,Price_Premium_Work")] Premium_Works premium_Works)
        {
            if (ModelState.IsValid)
            {
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
