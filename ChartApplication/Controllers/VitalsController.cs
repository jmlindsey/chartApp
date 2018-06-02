using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChartApplication.Models;

namespace ChartApplication.Controllers
{
    public class VitalsController : Controller
    {
        private ChartEntities db = new ChartEntities();

        // GET: Vitals
        [ChildActionOnly]
        public ActionResult History()
        {
            var vitals = db.Vitals.Include(v => v.Patient);
            return View(vitals.ToList());
        }



        // GET: Vitals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vital vital = db.Vitals.Find(id);
            if (vital == null)
            {
                return HttpNotFound();
            }
            return View(vital);
        }

        // GET: Vitals/Create
        //public ActionResult Create()
        //{
        //    ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "PatientFirst");
        //    return View(new Vital { DateDone = DateTime.Now.ToShortDateString(), TimeDone = DateTime.Now.ToShortTimeString() });
        //}

        // POST: Vitals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VitalId,PatientId,DateDone,TimeDone,UpperLeftSound,UpperRightSound,LowerLeftSound,LowerRightSound,MiddleSound,RR,HR,BP,Saturation,Cough,O2Device,Flow")] Vital vital)
        {
            if (ModelState.IsValid)
            {
                db.Vitals.Add(vital);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "PatientFirst", vital.PatientId);
            return View(vital);
        }

        // GET: Vitals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vital vital = db.Vitals.Find(id);
            if (vital == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "PatientFirst", vital.PatientId);
            return View(vital);
        }

        // POST: Vitals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VitalId,PatientId,DateDone,TimeDone,UpperLeftSound,UpperRightSound,LowerLeftSound,LowerRightSound,MiddleSound,RR,HR,BP,Saturation,Cough,O2Device,Flow")] Vital vital)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vital).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "PatientFirst", vital.PatientId);
            return View(vital);
        }

        // GET: Vitals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vital vital = db.Vitals.Find(id);
            if (vital == null)
            {
                return HttpNotFound();
            }
            return View(vital);
        }

        // POST: Vitals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vital vital = db.Vitals.Find(id);
            db.Vitals.Remove(vital);
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
