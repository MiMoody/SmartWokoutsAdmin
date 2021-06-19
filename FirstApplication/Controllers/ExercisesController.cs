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

        public string getFileExtension(string fileName) // Получение типа фотографии
        {
            return fileName.Substring(fileName.LastIndexOf(".") + 1);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Exercises exercises, HttpPostedFileBase Pic)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;
                if (Pic != null)
                {
                    fileName = Path.GetFileName(Pic.FileName);
                    string extensionImage = getFileExtension(fileName);
                    fileName = Guid.NewGuid() + "." + extensionImage;
                    var path = Path.Combine(Server.MapPath("~/Content/Images/ExerciseGifs"), fileName);
                    //var path = Path.Combine("https://disk.yandex.ru/d/DXlvv0vSsvgswQ?w=1", fileName);
                    Pic.SaveAs(path);
                }
                exercises.PicturePath = fileName;
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
        public ActionResult Edit(Exercises exercises, HttpPostedFileBase Pic)
            {
            if (ModelState.IsValid)
            {
                string fileName = null;
                if (Pic != null)
                {
                    fileName = Path.GetFileName(Pic.FileName);
                    string extensionImage = getFileExtension(fileName);
                    fileName = Guid.NewGuid() + "." + extensionImage;
                    var path = Path.Combine(Server.MapPath("~/Content/Images/ExerciseGifs"), fileName);
                    //var path = Path.Combine("https://disk.yandex.ru/d/DXlvv0vSsvgswQ?w=1", fileName);
                    Pic.SaveAs(path);
                    exercises.PicturePath = fileName;
                }
                else
                {
                    using (SmartWorkouts_newEntities bd = new SmartWorkouts_newEntities())
                    {
                        exercises.PicturePath = bd.Exercises.Where(p => p.ID_Exercise == exercises.ID_Exercise).FirstOrDefault().PicturePath;
                    }
                }
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
