using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChartApplication.Models;
using ChartApplication.Models.ViewModels;

namespace ChartApplication.Controllers
{
    [Authorize]
    public class VisitsController : Controller
    {
        private ChartEntities db = new ChartEntities();

        // GET: Visits
        public ActionResult Index()
        {
            var visits = db.Visits.Where(x => x.DischargeDate == null).Include(v => v.Patient);
            ViewBag.AddPatient = new SelectList(db.Patients, "PatientId", "PatientFirst");

            return View(visits.ToList());
        }

        // GET: Visits/History/5
        public ActionResult History(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var patient = db.Patients.Single(x => x.PatientId == id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            var visitHistoryVM = new VisitHistoryViewModel
            {
                PatientId = id,
                PatientName = patient.PatientLast + ", " + patient.PatientFirst,
                VisitHistory = db.Visits.Where(x => x.DischargeDate != null).Where(x => x.PatientId == id).Include(v => v.Patient)
            };
            return View(visitHistoryVM);
        }

        // GET: Visits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visit visit = db.Visits.Find(id);
            if (visit == null)
            {
                return HttpNotFound();
            }
            return View(visit);
        }

        // GET: Visits/Create
        public ActionResult Create()
        {
            ViewBag.VisitorId = new SelectList(db.Patients, "PatientId", "PatientFirst");
            return View();
        }

        // POST: Visits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Room,VisitDate,DischargeDate,Room")] Visit visit)
        {
            if (ModelState.IsValid)
            {
                db.Visits.Add(visit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VisitorId = new SelectList(db.Patients, "PatientId", "PatientFirst", visit.PatientId);
            return View(visit);
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
