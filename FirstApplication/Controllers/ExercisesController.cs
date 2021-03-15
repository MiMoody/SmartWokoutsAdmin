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
    public class ExercisesController : Controller
    {
        private SmartWorkouts_newEntities db = new SmartWorkouts_newEntities();

        // GET: Exercises
        public ActionResult Index()
        {
            var exercises = db.Exercises.Include(e => e.Premium_Works).Include(e => e.Types_Workout);
            return View(exercises.ToList());
        }

        // GET: Exercises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercises exercises = db.Exercises.Find(id);
            if (exercises == null)
            {
                return HttpNotFound();
            }
            return View(exercises);
        }

        // GET: Exercises/Create
        public ActionResult Create()
        {
            ViewBag.Premium_Work_Number = new SelectList(db.Premium_Works, "Number_Premium_Work", "Name_Premium_Work");
            ViewBag.Type_Exercise = new SelectList(db.Types_Workout, "Number_Type", "Name_Type");
            return View();
        }

        // POST: Exercises/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Exercise,Name_Exercise,Description_Exercise,Duration_Exercise,Type_Exercise,Premium_Work_Number")] Exercises exercises)
        {
            if (ModelState.IsValid)
            {
                db.Exercises.Add(exercises);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Premium_Work_Number = new SelectList(db.Premium_Works, "Number_Premium_Work", "Name_Premium_Work", exercises.Premium_Work_Number);
            ViewBag.Type_Exercise = new SelectList(db.Types_Workout, "Number_Type", "Name_Type", exercises.Type_Exercise);
            return View(exercises);
        }

        // GET: Exercises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercises exercises = db.Exercises.Find(id);
            if (exercises == null)
            {
                return HttpNotFound();
            }
            ViewBag.Premium_Work_Number = new SelectList(db.Premium_Works, "Number_Premium_Work", "Name_Premium_Work", exercises.Premium_Work_Number);
            ViewBag.Type_Exercise = new SelectList(db.Types_Workout, "Number_Type", "Name_Type", exercises.Type_Exercise);
            return View(exercises);
        }

        // POST: Exercises/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Exercise,Name_Exercise,Description_Exercise,Duration_Exercise,Type_Exercise,Premium_Work_Number")] Exercises exercises)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exercises).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Premium_Work_Number = new SelectList(db.Premium_Works, "Number_Premium_Work", "Name_Premium_Work", exercises.Premium_Work_Number);
            ViewBag.Type_Exercise = new SelectList(db.Types_Workout, "Number_Type", "Name_Type", exercises.Type_Exercise);
            return View(exercises);
        }

        // GET: Exercises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercises exercises = db.Exercises.Find(id);
            if (exercises == null)
            {
                return HttpNotFound();
            }
            return View(exercises);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exercises exercises = db.Exercises.Find(id);
            db.Exercises.Remove(exercises);
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
