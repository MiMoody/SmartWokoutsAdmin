using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FirstApplication.Models;

namespace FirstApplication.Controllers{
    public class WorkoutsController : Controller
    {
        private SmartWorkouts_newEntities db = new SmartWorkouts_newEntities();

        // GET: Workouts
        public ActionResult Index()
        {
            var workouts = db.Workouts.Include(w => w.Premium_Works).Include(w => w.Types_Workout);
            return View(workouts.ToList());
        }
        [HttpPost]
        public ActionResult LoadWork(string idWorkout,string idDeleteExercise = null, string idAddExercise = null)
        {
            int idWork = Convert.ToInt32(idWorkout);
            if(idDeleteExercise != null)
            {
                int idExer = Convert.ToInt32(idDeleteExercise);
                WorkoutElements elements = db.WorkoutElements.Where(o => o.ID_Workout == idWork && o.ID_Exercises == idExer).FirstOrDefault();
                db.WorkoutElements.Remove(elements);
                db.SaveChanges();
            }
            if(idAddExercise!=null)
            {
                int idExer = Convert.ToInt32(idAddExercise);
                WorkoutElements elements = db.WorkoutElements.Where(o => o.ID_Workout == idWork && o.ID_Exercises == idExer).FirstOrDefault();
                if(elements==null)
                {
                    WorkoutElements workouts = new WorkoutElements();
                    workouts.ID_Exercises = idExer;
                    workouts.ID_Workout = idWork;
                    db.WorkoutElements.Add(workouts);
                    db.SaveChanges();
                }
            }
            var workoutsElements = db.WorkoutElements.Where(w => w.ID_Workout == idWork).Include(w => w.Exercises);
            return PartialView(workoutsElements);
        }

        // GET: Workouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workouts workouts = db.Workouts.Find(id);
            if (workouts == null)
            {
                return HttpNotFound();
            }
            ViewBag.idWork = workouts.ID_Workout;
            ViewBag.Exercises = new SelectList(db.Exercises, "ID_Exercise", "Name_Exercise");
            return View(workouts);
        }

        // GET: Workouts/Create
        public ActionResult Create()
        {
            ViewBag.Number_Premium_Workout = new SelectList(db.Premium_Works, "Number_Premium_Work", "Name_Premium_Work");
            ViewBag.Type_Workout = new SelectList(db.Types_Workout, "Number_Type", "Name_Type");
            return View();
        }

        // POST: Workouts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Workout,Name_Workout,Description_Workout,Number_Premium_Workout,Type_Workout")] Workouts workouts)
        {
            if (ModelState.IsValid)
            {
                db.Workouts.Add(workouts);
                    db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Number_Premium_Workout = new SelectList(db.Premium_Works, "Number_Premium_Work", "Name_Premium_Work", workouts.Number_Premium_Workout);
            ViewBag.Type_Workout = new SelectList(db.Types_Workout, "Number_Type", "Name_Type", workouts.Type_Workout);
            return View(workouts);
        }

        // GET: Workouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workouts workouts = db.Workouts.Find(id);
            if (workouts == null)
            {
                return HttpNotFound();
            }
            ViewBag.Number_Premium_Workout = new SelectList(db.Premium_Works, "Number_Premium_Work", "Name_Premium_Work", workouts.Number_Premium_Workout);
            ViewBag.Type_Workout = new SelectList(db.Types_Workout, "Number_Type", "Name_Type", workouts.Type_Workout);
            return View(workouts);
        }

        // POST: Workouts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Workout,Name_Workout,Description_Workout,Number_Premium_Workout,Type_Workout")] Workouts workouts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workouts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Number_Premium_Workout = new SelectList(db.Premium_Works, "Number_Premium_Work", "Name_Premium_Work", workouts.Number_Premium_Workout);
            ViewBag.Type_Workout = new SelectList(db.Types_Workout, "Number_Type", "Name_Type", workouts.Type_Workout);
            return View(workouts);
        }

        // GET: Workouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workouts workouts = db.Workouts.Find(id);
            if (workouts == null)
            {
                return HttpNotFound();
            }
            return View(workouts);
        }

        // POST: Workouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Workouts workouts = db.Workouts.Find(id);
            db.Workouts.Remove(workouts);
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
