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
    public class Types_WorkoutController : Controller
    {
        private SmartWorkouts_newEntities db = new SmartWorkouts_newEntities();

        // GET: Types_Workout
        public ActionResult Index()
        {
            return View(db.Types_Workout.ToList());
        }

        // GET: Types_Workout/Details/5

        // GET: Types_Workout/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Types_Workout/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Number_Type,Name_Type")] Types_Workout types_Workout)
        {
            if (ModelState.IsValid)
            {
                db.Types_Workout.Add(types_Workout);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(types_Workout);
        }

        // GET: Types_Workout/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Types_Workout types_Workout = db.Types_Workout.Find(id);
            if (types_Workout == null)
            {
                return HttpNotFound();
            }
            return View(types_Workout);
        }

        // POST: Types_Workout/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Number_Type,Name_Type")] Types_Workout types_Workout)
        {
            if (ModelState.IsValid)
            {
                db.Entry(types_Workout).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(types_Workout);
        }

        // GET: Types_Workout/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Types_Workout types_Workout = db.Types_Workout.Find(id);
            if (types_Workout == null)
            {
                return HttpNotFound();
            }
            return View(types_Workout);
        }

        // POST: Types_Workout/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Types_Workout types_Workout = db.Types_Workout.Find(id);
            db.Types_Workout.Remove(types_Workout);
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
