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
using Microsoft.AspNet.Identity;

namespace ChartApplication.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private ChartEntities db;

        public PatientsController(ChartEntities db)
        {
            this.db = db;
        }


        // GET: Patients
        public ActionResult Index()
        {
            return View(db.Patients.ToList());
        }

        // GET: Interventions/Create
        public ActionResult Chart(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var visit = db.Visits.Find(id);
            var patient = db.Patients.Find(visit.PatientId);
            if (patient == null)
            {
                return HttpNotFound();
            }
            var vitals = db.Vitals.Where(x => x.PatientId == patient.PatientId).Where(v => v.DateTimeDone >= visit.VisitDate);

            var userId = User.Identity.GetUserId();
            var interventions = db.Interventions.Where(x => x.PatientId == patient.PatientId).Where(i => i.DateTimeDone >= visit.VisitDate);
            var employee = db.Employees.FirstOrDefault(e => e.AccountId == userId);

            HistoryViewModel histVM = new HistoryViewModel
            {
                History = vitals.Join(interventions,
                v => v.DateTimeDone, i => i.DateTimeDone,
                (v, i) =>
                    new ChartViewModelInstance
                    {
                        EmployeeName = i.Employee.LastName + ", " + i.Employee.FirstName,
                        EmployeeInitials = i.Employee.FirstName.Substring(0, 1) + i.Employee.LastName.Substring(0, 1),
                        Credentials = i.Employee.LicenseCredentials,

                        Activity1 = i.Activity1,
                        Activity2 = i.Activity2,
                        Activity3 = i.Activity3,
                        Activity4 = i.Activity4,
                        BP = v.BP,
                        Cough = v.Cough,
                        DateDone = v.DateTimeDone,
                        TimeDone = v.DateTimeDone,
                        Flow = v.Flow,
                        HowTolerated = i.HowTolerated,
                        HR = v.HR,
                        LowerLeftSound = v.LowerLeftSound,
                        LowerRightSound = v.LowerRightSound,
                        MiddleSound = v.MiddleSound,
                        O2Device = v.O2Device,
                        PatientId = patient.PatientId,
                        PatientName = v.Patient.PatientLast + ", " + v.Patient.PatientFirst,
                        ResponseToTreatment = i.ResponeToTreatment,
                        RouteGiven = i.RouteGiven,
                        RR = v.RR,
                        Saturation = v.Saturation,
                        UpperLeftSound = v.UpperLeftSound,
                        UpperRightSound = v.UpperRightSound
                    })
            };
            ChartViewModel chartVM = new ChartViewModel
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.LastName + ", " + employee.FirstName,
                EmployeeInitials = employee.FirstName.Substring(0, 1) + employee.LastName.Substring(0, 1),
                Credentials = employee.LicenseCredentials,
                PatientId = patient.PatientId,
                PatientName = patient.PatientLast + ", " + patient.PatientFirst,
                historyVM = histVM
            };

            chartVM.historyVM.History = chartVM.historyVM.History.OrderBy(x => x.DateDone).OrderBy(x => x.TimeDone);
            return View(chartVM);
        }



        // POST: Interventions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Chart(ChartViewModel chartVM)
        {
            if (ModelState.IsValid)
            {
                var intervention = new Intervention
                {
                    EmployeeId = chartVM.EmployeeId,
                    DateTimeDone = new DateTime(chartVM.DateDone.Year, chartVM.DateDone.Month, chartVM.DateDone.Day,
                                                chartVM.TimeDone.Hour, chartVM.TimeDone.Minute, chartVM.TimeDone.Second),
                    Activity1 = chartVM.Activity1,
                    Activity2 = chartVM.Activity2,
                    Activity3 = chartVM.Activity3,
                    Activity4 = chartVM.Activity4,
                    HowTolerated = chartVM.HowTolerated,
                    PatientId = chartVM.PatientId,
                    ResponeToTreatment = chartVM.ResponseToTreatment,
                    RouteGiven = chartVM.RouteGiven
                };

                var vital = new Vital
                {
                    EmployeeId = chartVM.EmployeeId,
                    BP = chartVM.BP,
                    RR = chartVM.RR,
                    Saturation = chartVM.Saturation,
                    DateTimeDone = new DateTime(chartVM.DateDone.Year, chartVM.DateDone.Month, chartVM.DateDone.Day,
                                                chartVM.TimeDone.Hour, chartVM.TimeDone.Minute, chartVM.TimeDone.Second),
                    Cough = chartVM.Cough,
                    HR = chartVM.HR,
                    Flow = chartVM.Flow,
                    O2Device = chartVM.O2Device,
                    PatientId = chartVM.PatientId,
                    UpperLeftSound = chartVM.UpperLeftSound,
                    UpperRightSound = chartVM.UpperRightSound,
                    LowerLeftSound = chartVM.LowerLeftSound,
                    LowerRightSound = chartVM.LowerRightSound,
                    MiddleSound = chartVM.MiddleSound
                };
                db.Interventions.Add(intervention);
                db.Vitals.Add(vital);
                db.SaveChanges();
                return RedirectToAction("Index", "Visits");
            }
            return View(chartVM);
        }

        [ChildActionOnly]
        public ActionResult ChartHistory()
        {
            return View();
        }

        // Get
        public ActionResult History(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var visit = db.Visits.Find(id);
            var patient = db.Patients.Find(visit.PatientId);
            if (patient == null)
            {
                return HttpNotFound();
            }
            var vitals = db.Vitals.Where(x => x.PatientId == patient.PatientId).Where(v => v.DateTimeDone >= visit.VisitDate).Where(v => v.DateTimeDone <= visit.DischargeDate);

            var userId = User.Identity.GetUserId();
            var interventions = db.Interventions.Where(x => x.PatientId == patient.PatientId).Where(i => i.DateTimeDone >= visit.VisitDate).Where(v => v.DateTimeDone <= visit.DischargeDate);
            var employee = db.Employees.FirstOrDefault(e => e.AccountId == userId);

            HistoryViewModel histVM = new HistoryViewModel
            {
                History = vitals.Join(interventions,
                v => v.DateTimeDone, i => i.DateTimeDone,
                (v, i) =>
                    new ChartViewModelInstance
                    {
                        EmployeeName = i.Employee.LastName + ", " + i.Employee.FirstName,
                        EmployeeInitials = i.Employee.FirstName.Substring(0, 1) + i.Employee.LastName.Substring(0, 1),
                        Credentials = i.Employee.LicenseCredentials,

                        Activity1 = i.Activity1,
                        Activity2 = i.Activity2,
                        Activity3 = i.Activity3,
                        Activity4 = i.Activity4,
                        BP = v.BP,
                        Cough = v.Cough,
                        DateDone = v.DateTimeDone,
                        TimeDone = v.DateTimeDone,
                        Flow = v.Flow,
                        HowTolerated = i.HowTolerated,
                        HR = v.HR,
                        LowerLeftSound = v.LowerLeftSound,
                        LowerRightSound = v.LowerRightSound,
                        MiddleSound = v.MiddleSound,
                        O2Device = v.O2Device,
                        PatientId = patient.PatientId,
                        PatientName = v.Patient.PatientLast + ", " + v.Patient.PatientFirst,
                        ResponseToTreatment = i.ResponeToTreatment,
                        RouteGiven = i.RouteGiven,
                        RR = v.RR,
                        Saturation = v.Saturation,
                        UpperLeftSound = v.UpperLeftSound,
                        UpperRightSound = v.UpperRightSound
                    })
            };
            ChartViewModel chartVM = new ChartViewModel
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.LastName + ", " + employee.FirstName,
                EmployeeInitials = employee.FirstName.Substring(0, 1) + employee.LastName.Substring(0, 1),
                Credentials = employee.LicenseCredentials,
                PatientId = patient.PatientId,
                PatientName = patient.PatientLast + ", " + patient.PatientFirst,
                historyVM = histVM
            };

            chartVM.historyVM.History = chartVM.historyVM.History.OrderBy(x => x.DateDone).OrderBy(x => x.TimeDone);
            return View(chartVM);
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatientId,PatientFirst,PatientLast,BirthDate,Allergies1,Allergies2,Allergies3,Allergies4,Allergies5")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatientId,PatientFirst,PatientLast,BirthDate,Allergies1,Allergies2,Allergies3,Allergies4,Allergies5")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
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
